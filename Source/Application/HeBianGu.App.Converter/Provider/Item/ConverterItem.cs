using FFMpegCore;
using HeBianGu.Base.WpfBase;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        protected override string CreateOutputPath(string groupPath)
        {
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(FilePath) + OutputMediaInfo.ContainerFormat.Extension);
        }

        protected virtual void AddArguments(FFMpegArgumentOptions options)
        {
            OutputMediaInfo.AddArguments(options);
        }

        protected virtual FFMpegArgumentProcessor CreateProcessor()
        {
            return FFMpegArguments.FromFileInput(this.FilePath).OutputToFile(this.OutputPath, false, AddArguments);
        }

        protected override bool Start(IRelayCommand s, object e)
        {
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
            process.NotifyOnProgress(x =>
            {
                Value = x;
                if (x == 100.0)
                {
                    Success = true;
                    Message = "完成";
                }
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

            });

            process.CancellableThrough(out _cancel, 10000).ProcessSynchronously();
            return true;
        }

    }


    public abstract class ItemBase : NotifyPropertyChangedBase
    {

    }

    public abstract class ConverterItemBase : ItemBase
    {
        public ConverterItemBase(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            Extend = Path.GetExtension(filePath);
            CreateMediaInfo(filePath);
            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            //{
            //    this.CreateVideoFormat(filePath);
            //}));
        }

        public virtual void CreateMediaInfo(string filePath)
        {
            var mediaInfo = FFProbe.Analyse(filePath);
            this.InputMediaInfo = this.CreateInputMediaInfo(mediaInfo);
            this.OutputMediaInfo = this.CreateOutputMediaInfo(mediaInfo);
        }

        public virtual MediaInfo CreateInputMediaInfo(IMediaAnalysis mediaInfo)
        {
            var result = new MediaInfo(mediaInfo, this.FilePath);
            //this.InVideoFormat.Codecs = FFMpeg.GetCodecs();
            //this.InVideoFormat.ContainerFormats = FFMpeg.GetContainerFormats();
            //this.InVideoFormat.PixelFormats = FFMpeg.GetPixelFormats();
            result.Size = new FileInfo(this.FilePath).Length;
            return result;
        }

        public virtual MediaInfo CreateOutputMediaInfo(IMediaAnalysis mediaInfo)
        {
            var result = new MediaInfo(mediaInfo, this.FilePath);
            result.Size = new FileInfo(this.FilePath).Length;
            result.Codecs = FFMpeg.GetCodecs();
            result.ContainerFormats = FFMpeg.GetContainerFormats();
            result.PixelFormats = FFMpeg.GetPixelFormats();
            return result;
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                RaisePropertyChanged();
            }
        }

        private string _extend;
        public string Extend
        {
            get { return _extend; }
            set
            {
                _extend = value;
                RaisePropertyChanged();
            }
        }

        private double _value;
        [Browsable(false)]
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }


        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }


        private bool _isBuzy;
        [Browsable(false)]
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged();
            }
        }


        private string _outputPath;
        public string OutputPath
        {
            get { return _outputPath; }
            set
            {
                _outputPath = value;
                RaisePropertyChanged();
            }
        }


        private MediaInfo _inputMediaInfo;
        public MediaInfo InputMediaInfo
        {
            get { return _inputMediaInfo; }
            set
            {
                _inputMediaInfo = value;
                RaisePropertyChanged();
            }
        }

        private MediaInfo _outputMediaInfo;
        public MediaInfo OutputMediaInfo
        {
            get { return _outputMediaInfo; }
            set
            {
                _outputMediaInfo = value;
                RaisePropertyChanged();
            }
        }


        //[Displayer(Name = "选择格式", Icon = Icons.Set, GroupName = "操作", Description = "处理器相关数据监控")]
        //public RelayCommand SetCommand => new RelayCommand(async (s, e) =>
        //{
        //    await Set();
        //});

        //protected virtual object CreateSetter()
        //{
        //    return null;
        //}

        protected virtual async Task<bool> Set()
        {
            //var setter = CreateSetter();
            //if (setter == null)
            //    return false;
            return await MessageProxy.PropertyGrid.ShowEdit(this, null, "参数设置");
        }

        [Displayer(Name = "开始转换", Icon = Icons.Play, GroupName = "转换", Description = "开始转换")]
        public RelayCommand StartCommand => new RelayCommand(async (s, e) =>
        {
            await StartAsync(s, e);
        });

        [Displayer(Name = "设置输出参数", Icon = Icons.Set, GroupName = "操作", Description = "设置输出参数")]
        public RelayCommand OutPutSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(this.OutputMediaInfo, null, "设置输出参数");

            //(视频码率 + 音频码率) * 时长 / 8 = 文件大小
            this.OutputMediaInfo.Size = (long)((this.OutputMediaInfo.BitRate + this.OutputMediaInfo.BitRate) * this.OutputMediaInfo.Model.Duration.TotalSeconds / 8);
        });

        [Displayer(Name = "查看视频参数", Icon = "\xe76b", GroupName = "操作", Description = "查看视频参数")]
        public RelayCommand InputViewCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowView(this.InputMediaInfo, null, "查看视频参数");
        });

        protected virtual bool Start(IRelayCommand s, object e)
        {
            return true;
        }

        protected virtual string CreateOutputPath(string groupPath)
        {
            return groupPath;
        }

        protected virtual async Task<bool> StartAsync(IRelayCommand s, object e)
        {
            Value = 0.0;
            Message = "正在开始...";
            if (e is GroupBase group)
            {
                IsBuzy = true;
                s.IsBusy = true;
                OutputPath = CreateOutputPath(group.OutPath);
                return await Task.Run(() =>
                 {
                     try
                     {
                         return Start(s, e);

                     }
                     catch (Exception ex)
                     {
                         Logger.Instance?.Error(ex);
                         Message = ex.Message;
                         Success = false;
                         MessageProxy.Snacker.ShowTime(ex.Message);
                         return false;
                     }
                     finally
                     {
                         IsBuzy = false;
                         s.IsBusy = false;
                     }
                 });
            }
            return false;
        }

        [Displayer(Name = "停止转换", Icon = Icons.Stop, GroupName = "转换", Description = "处理器相关数据监控")]
        public RelayCommand StopCommand => new RelayCommand(async (s, e) =>
        {
            s.IsBusy = true;
            await StopAsnyc(s, e);
            this.IsBuzy = false;
            s.IsBusy = false;
        }, (s, e) => this.IsBuzy == true);

        protected abstract Task<bool> StopAsnyc(IRelayCommand s, object e);

        [Displayer(Name = "打开文件夹", Icon = Icons.OpenFolder, GroupName = "操作", Description = "处理器相关数据监控")]
        public RelayCommand OpenCommand => new RelayCommand((s, e) =>
        {
            if (!File.Exists(_outputPath))
            {
                MessageProxy.Snacker.ShowTime("文件不存在，请先转换");
                return;
            }
            Process.Start(new ProcessStartInfo(Path.GetDirectoryName(_outputPath)) { UseShellExecute = true });

        });
        [Displayer(Name = "删除", Icon = IconAll.Close, GroupName = "操作", Description = "处理器相关数据监控")]
        public RelayCommand DeleteCommand => new RelayCommand(async (s, e) =>
        {
            s.IsBusy = true;
            var r = await StopAsnyc(s, e);
            if (r == false)
                return;
            if (e is FrameworkElement element)
            {
                var items = element.GetParent<ItemsControl>().GetParent<ItemsControl>();
                if (items.ItemsSource is IList list)
                {
                    list.Remove(this);
                }
            }
            s.IsBusy = false;
        });

        [Displayer(Name = "彻底删除", Icon = Icons.Delete, GroupName = "操作", Description = "处理器相关数据监控")]
        public RelayCommand DeleteFileCommand => new RelayCommand(async (s, e) =>
        {
            s.IsBusy = true;
            var r = await StopAsnyc(s, e);
            if (r == false)
                return;
            try
            {
                File.Delete(_outputPath);
                if (e is FrameworkElement element)
                {
                    var items = element.GetParent<ItemsControl>().GetParent<ItemsControl>();
                    if (items.ItemsSource is IList list)
                    {
                        list.Remove(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageProxy.Snacker.ShowTime(ex.Message);
            }
            finally
            {
                s.IsBusy = false;
            }
        });

        private bool? _success;
        /// <summary> 说明  </summary>
        public bool? Success
        {
            get { return _success; }
            set
            {
                _success = value;
                RaisePropertyChanged();
            }
        }
    }
}
