using FFMpegCore;
using FFMpegCore.Builders.MetaData;
using HeBianGu.Base.WpfBase;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace HeBianGu.Domain.Converter
{
    public abstract class ProcessorConverterItemBase : ConverterItemBase
    {
        protected Action _cancel = null;

        public ProcessorConverterItemBase(string filePath, Action<ConverterItemBase> builder) : base(filePath, builder)
        {

        }

        protected override async Task<bool> StopAsnyc(IRelayCommand s, object e)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _cancel?.Invoke();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageProxy.Snacker.ShowTime(ex.Message);
                    return false;
                }
            });

        }


        private DateTime _startTime = DateTime.Now;
        /// <summary> 说明  </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }


        private TimeSpan _duration;
        /// <summary> 说明  </summary>
        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                RaisePropertyChanged();
            }
        }


        private TimeSpan _downDuration;
        /// <summary> 说明  </summary>
        public TimeSpan DownDuration
        {
            get { return _downDuration; }
            set
            {
                _downDuration = value;
                RaisePropertyChanged();
            }
        }

        protected abstract void CreateArguments(FFMpegArgumentOptions options);

        protected virtual FFMpegArgumentProcessor CreateProcessor()
        {
            return FFMpegArguments.FromFileInput(FilePath).OutputToFile(OutputPath, false, CreateArguments);
        }

        [Displayer(Name = "查看命令参数", Icon = IconAll.Cmd, GroupName = "操作,输出", Description = "查看命令参数")]
        public RelayCommand ViewArgumentsCommand => new RelayCommand((s, e) =>
        {
            var processor = CreateProcessor();
            MessageProxy.PropertyGrid.ShowView(processor, null, "查看命令参数");
        }, (s, e) => this.IsBuzy == false);

        protected override bool Start(IRelayCommand s, object e)
        {
            if (File.Exists(OutputPath))
            {
                var r = MessageProxy.Messager.ShowResult("当前输出文件已存在，点击确定删除历史文件?").Result;
                if (!r)
                {
                    Message = "用户取消操作";
                    return false;
                }
                File.Delete(OutputPath);
            }
            StartTime = DateTime.Now;
            var process = CreateProcessor();
            StartProcessor(process);

            if(File.Exists(this.OutputPath))
            {
                this.OutputMediaInfo.VedioAnalysis.Size = new FileInfo(this.OutputPath).Length;
                this.OutputMediaInfo.AudioAnalysis.Size = new FileInfo(this.OutputPath).Length;
                this.OutputAnalysis = this.InputMediaInfo.VedioAnalysis;
                this.OutputAnalysis = this.OutputMediaInfo.VedioAnalysis;

                this.RefreshAnalysis();
            }
            return true;
        }

        protected virtual void StartProcessor(FFMpegArgumentProcessor process)
        {
            Debug.WriteLine(process.Arguments);
            Message = process.Arguments;
            process.NotifyOnProgress(x =>
            {
                Value = x;
                if (x == 100.0)
                {
                    Success = true;
                    Message = "完成";
                }
                Duration = DateTime.Now - StartTime;

                if (Duration.Ticks / x * (100 - x) > TimeSpan.MaxValue.Ticks)
                    DownDuration = TimeSpan.MaxValue;
                else
                    DownDuration = Duration / x * (100 - x);
            }, InputMediaInfo.Model.Duration);

            process.NotifyOnOutput(x =>
            {
                Message = x;
            });

            process.NotifyOnError(x =>
            {
                Message = x;
                Success = false;
                Debug.WriteLine(DateTime.Now + "   " + x);

            });

            process.NotifyOnProgress(x =>
            {
                //this.Value = x;
            });

            process.CancellableThrough(out _cancel, 10000).ProcessSynchronously();
        }
    }
}
