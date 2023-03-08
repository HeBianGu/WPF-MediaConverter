using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Extensions.System.Drawing.Common;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Domain.Converter
{
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
            //CreateMediaInfo(filePath);
            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            //{
            //    this.CreateVideoFormat(filePath);
            //}));

            Task.Run(() =>
            {
                this.CreateMediaInfo(filePath);
                this.CreateImageSource(filePath);
            });
        }

        protected virtual void CreateImageSource(string filePath)
        {
            TimeSpan timeSpan = this.InputMediaInfo.VedioAnalysis.Model.Duration / 10;
            var bitmap = FFMpegImage.Snapshot(FilePath, null, timeSpan);
            var source = ImageService.BitmapToBitmapImage(bitmap, x => x.DecodePixelWidth = 140);
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ImageSource = source;
            });
        }

        public virtual void CreateMediaInfo(string filePath)
        {
            var mediaInfo = FFProbe.Analyse(filePath);
            InputMediaInfo = CreateInputMediaInfo(mediaInfo);
            OutputMediaInfo = CreateOutputMediaInfo(mediaInfo);
        }

        public virtual MediaInfo CreateInputMediaInfo(IMediaAnalysis mediaInfo)
        {
            var result = new MediaInfo(mediaInfo, FilePath);
            //this.InVideoFormat.Codecs = FFMpeg.GetCodecs();
            //this.InVideoFormat.ContainerFormats = FFMpeg.GetContainerFormats();
            //this.InVideoFormat.PixelFormats = FFMpeg.GetPixelFormats();
            result.Size = new FileInfo(FilePath).Length;
            return result;
        }

        public virtual MediaInfo CreateOutputMediaInfo(IMediaAnalysis mediaInfo)
        {
            var result = new MediaInfo(mediaInfo, FilePath);
            result.Size = new FileInfo(FilePath).Length;
            result.VedioAnalysis.Codecs = FFMpeg.GetCodecs().Where(x => x.Type == CodecType.Video).ToList().AsReadOnly();
            //  Do ：假定DemuxingSupported==true表示音频
            result.VedioAnalysis.ContainerFormats = FFMpeg.GetContainerFormats().Where(x => x.DemuxingSupported == false).ToList().AsReadOnly();
            result.VedioAnalysis.PixelFormats = FFMpeg.GetPixelFormats();
            result.VedioAnalysis.Size = result.Size;
            result.AudioAnalysis.Codecs = FFMpeg.GetCodecs().Where(x => x.Type == CodecType.Audio).ToList().AsReadOnly();
            //  Do ：假定DemuxingSupported==true表示音频
            result.AudioAnalysis.ContainerFormats = FFMpeg.GetContainerFormats().Where(x => x.DemuxingSupported == true).ToList().AsReadOnly();
            result.AudioAnalysis.Size = result.Size;


            //result.Meta.Copyright
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

        private AnalysisBase _outputAnalysis;
        /// <summary> 说明  </summary>
        public AnalysisBase OutputAnalysis
        {
            get { return _outputAnalysis; }
            set
            {
                _outputAnalysis = value;
                RaisePropertyChanged();
            }
        }

        private AnalysisBase _inputAnalysis;
        /// <summary> 说明  </summary>
        public AnalysisBase InputAnalysis
        {
            get { return _inputAnalysis; }
            set
            {
                _inputAnalysis = value;
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


        private ImageSource _imageSource;
        /// <summary> 说明  </summary>
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged();
            }
        }


        [Displayer(Name = "开始转换", Icon = Icons.Play, GroupName = "转换", Description = "开始转换")]
        public RelayCommand StartCommand => new RelayCommand(async (s, e) =>
        {
            await StartAsync(s, e);
        });


        [Displayer(Name = "播放源文件", Icon = Icons.Play, GroupName = "操作,输入", Description = "播放源文件", Order = 1)]
        public RelayCommand PlayInputCommand => new RelayCommand((s, e) =>
        {
            Process.Start(new ProcessStartInfo("ffplay.exe", FilePath) { UseShellExecute = true });
        }, (s, e) => File.Exists(FilePath));


        [Displayer(Name = "播放输出文件", Icon = Icons.Play, GroupName = "操作,输出", Description = "播放输出文件", Order = 1)]
        public RelayCommand PlayOutputCommand => new RelayCommand((s, e) =>
        {
            Process.Start(new ProcessStartInfo("ffplay.exe", OutputPath) { UseShellExecute = true });
        }, (s, e) => File.Exists(OutputPath));

        protected virtual bool Start(IRelayCommand s, object e)
        {
            return true;
        }

        protected virtual string CreateOutputPath(string groupPath)
        {
            return groupPath;
        }

        public virtual async Task<bool> StartAsync(IRelayCommand s, object e)
        {
            Value = 0.0;
            Message = "正在开始...";
            if (e is GroupBase group)
            {
                IsBuzy = true;
                s.IsBusy = true;
                OutputPath = CreateOutputPath(group.OutPath);
                if (!Directory.Exists(Path.GetDirectoryName(OutputPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(OutputPath));
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
            IsBuzy = false;
            s.IsBusy = false;
        }, (s, e) => IsBuzy == true);

        protected abstract Task<bool> StopAsnyc(IRelayCommand s, object e);

        [Displayer(Name = "信息和章节", Icon = "\xea5c", GroupName = "操作,输出", Description = "设置文件信息", Order = 96)]
        public RelayCommand SetMetaDataCommand => new RelayCommand((s, e) =>
        {
            MessageProxy.PropertyGrid.ShowEdit(OutputMediaInfo.Meta, null, "设置文件信息", x => x.UseEnumerator = true);
        });


        [Displayer(Name = "打开文件夹", Icon = Icons.OpenFolder, GroupName = "操作,输出", Description = "处理器相关数据监控", Order = 97)]
        public RelayCommand OpenCommand => new RelayCommand((s, e) =>
        {
            Process.Start(new ProcessStartInfo(Path.GetDirectoryName(_outputPath)) { UseShellExecute = true });

        }, (s, e) => File.Exists(_outputPath));

        [Displayer(Name = "删除", Icon = IconAll.Close, GroupName = "操作,输出", Description = "处理器相关数据监控", Order = 98)]
        public RelayCommand DeleteCommand => new RelayCommand(async (s, e) =>
        {
            s.IsBusy = true;
            var r = await StopAsnyc(s, e);
            if (r == false)
                return;
            if (e is FrameworkElement element)
            {
                var items = element.GetParent<ItemsControl>().GetParent<ItemsControl>();

                if (items.DataContext is ConverterGroupBase group)
                {
                    group.Collection.Remove(this);
                }
                //if (items.ItemsSource is IList list)
                //{
                //    list.Remove(this);
                //}
            }
            s.IsBusy = false;
        });

        [Displayer(Name = "彻底删除", Icon = Icons.Delete, GroupName = "操作,输出", Description = "处理器相关数据监控", Order = 99)]
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
