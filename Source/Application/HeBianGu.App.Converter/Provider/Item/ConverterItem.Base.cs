using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using Microsoft.Extensions.Options;
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

namespace HeBianGu.App.Converter
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
            result.VedioAnalysis.Codecs = FFMpeg.GetCodecs().Where(x => x.Type == CodecType.Video).ToList().AsReadOnly();
            //  Do ：假定DemuxingSupported==true表示音频
            result.VedioAnalysis.ContainerFormats = FFMpeg.GetContainerFormats().Where(x => x.DemuxingSupported == false).ToList().AsReadOnly();
            result.VedioAnalysis.PixelFormats = FFMpeg.GetPixelFormats();
            result.VedioAnalysis.Size = result.Size;

            result.AudioAnalysis.Codecs = FFMpeg.GetCodecs().Where(x => x.Type == CodecType.Audio).ToList().AsReadOnly();
            //  Do ：假定DemuxingSupported==true表示音频
            result.AudioAnalysis.ContainerFormats = FFMpeg.GetContainerFormats().Where(x => x.DemuxingSupported == true).ToList().AsReadOnly();

            result.AudioAnalysis.Size = result.Size;

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


        [Displayer(Name = "播放", Icon = Icons.Play, GroupName = "操作", Description = "播放")]
        public RelayCommand PlayCommand => new RelayCommand(async (s, e) =>
        {
            Process.Start(new ProcessStartInfo("ffplay.exe", this.FilePath) { UseShellExecute = true });
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
