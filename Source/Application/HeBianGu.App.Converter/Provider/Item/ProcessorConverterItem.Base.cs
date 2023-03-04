using FFMpegCore;
using FFMpegCore.Builders.MetaData;
using HeBianGu.Base.WpfBase;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace HeBianGu.App.Converter
{
    public abstract class ProcessorConverterItemBase : ConverterItemBase
    {
        protected Action _cancel = null;

        public ProcessorConverterItemBase(string filePath) : base(filePath)
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
            return FFMpegArguments.FromFileInput(this.FilePath).OutputToFile(this.OutputPath, false, CreateArguments);
        }

        [Displayer(Name = "查看命令参数", Icon = IconAll.Cmd, GroupName = "操作", Description = "查看命令参数")]
        public RelayCommand ViewArgumentsCommand => new RelayCommand((s, e) =>
        {
            var processor = this.CreateProcessor();

            MessageProxy.PropertyGrid.ShowView(processor, null, "查看命令参数");
        });

        [Displayer(Name = "设置文件信息", Icon = IconAll.T, GroupName = "操作", Description = "设置文件信息")]
        public RelayCommand SetMetaDataCommand => new RelayCommand((s, e) =>
        {
            MetaData metaData = new MetaData();
            MessageProxy.PropertyGrid.ShowEdit(metaData, null, "设置文件信息", x => x.UseEnumerator = true);
        });

        protected override bool Start(IRelayCommand s, object e)
        {
            this.StartTime = DateTime.Now;
            if (File.Exists(this.OutputPath))
            {
                var r = MessageProxy.Messager.ShowResult("当前输出文件已存在，点击确定删除历史文件?").Result;
                if (!r)
                {
                    this.Message = "用户取消操作";
                    return false;
                }
                File.Delete(this.OutputPath);
            }
            var process = this.CreateProcessor();

            System.Diagnostics.Debug.WriteLine(process.Arguments);
            this.Message = process.Arguments;

            process.NotifyOnProgress(x =>
            {
                Value = x;
                if (x == 100.0)
                {
                    Success = true;
                    Message = "完成";
                }
                this.Duration = DateTime.Now - this.StartTime;

                if ((this.Duration.Ticks / x) * (100 - x) > TimeSpan.MaxValue.Ticks)
                    this.DownDuration = TimeSpan.MaxValue;
                else
                    this.DownDuration = (this.Duration / x) * (100 - x);
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
            return true;
        }

    }
}
