using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace HeBianGu.App.Converter
{
    /// <summary> 说明</summary>
    public class MediaInfo : ModelViewModel<IMediaAnalysis>
    {
        public MediaInfo(IMediaAnalysis model, string filePath) : base(model)
        {
            string extension = Path.GetExtension(filePath);
            //  ToDo：model.Format.FormatName表示可以转换的格式列表？ 
            string fn = model.Format.FormatName.Split(',')[0];
            ContainerFormat = FFMpeg.GetContainerFormat(fn);
            Codec = FFMpeg.GetCodec(model.PrimaryVideoStream.CodecName);
            FrameRate = model.PrimaryVideoStream.FrameRate;
            PixelFormat = FFMpeg.GetPixelFormat(model.PrimaryVideoStream.PixelFormat);
            BitRate = model.PrimaryVideoStream.BitRate;
            //this.Height = model.PrimaryVideoStream.Height;
            //this.Width = model.PrimaryVideoStream.Width;
            var find = Enum.GetValues<VideoSize>().FirstOrDefault(x => (int)x == model.PrimaryVideoStream.Height);
            VideoSize = find == default ? VideoSize.FullHd : find;
            StartTime = model.Format.StartTime;
            EndTime = model.Format.Duration;
        }

        public MediaInfo() : base(null)
        {

        }

        private ContainerFormat _containerFormat;
        [Display(Name = "文件格式", Description = "文件格式")]
        [BindingGetSelectSourceProperty(nameof(ContainerFormats))]
        [PropertyItemType(typeof(OnlyComboBoxSelectSourcePropertyItem))]
        public ContainerFormat ContainerFormat
        {
            get { return _containerFormat; }
            set
            {
                _containerFormat = value;
                RaisePropertyChanged();
            }
        }

        private IReadOnlyList<ContainerFormat> _containerFormats = new List<ContainerFormat>();
        [Browsable(false)]
        public IReadOnlyList<ContainerFormat> ContainerFormats
        {
            get { return _containerFormats; }
            set
            {
                _containerFormats = value;
                RaisePropertyChanged();
            }
        }



        private Codec _codec;
        [Display(Name = "编码方式", Description = "编码方式")]
        [BindingGetSelectSourceProperty(nameof(Codecs))]
        [PropertyItemType(typeof(OnlyComboBoxSelectSourcePropertyItem))]
        public Codec Codec
        {
            get { return _codec; }
            set
            {
                _codec = value;
                RaisePropertyChanged();
            }
        }

        private IReadOnlyList<Codec> _codecs = new List<Codec>();
        [Browsable(false)]
        public IReadOnlyList<Codec> Codecs
        {
            get { return _codecs; }
            set
            {
                _codecs = value;
                RaisePropertyChanged();
            }
        }

        private PixelFormat _pixelFormat;
        [Display(Name = "像素格式", Description = "像素格式")]
        [BindingGetSelectSourceProperty(nameof(PixelFormats))]
        [PropertyItemType(typeof(OnlyComboBoxSelectSourcePropertyItem))]
        public PixelFormat PixelFormat
        {
            get { return _pixelFormat; }
            set
            {
                _pixelFormat = value;
                RaisePropertyChanged();
            }
        }


        private IReadOnlyList<PixelFormat> _pixelFormats = new List<PixelFormat>();
        [Browsable(false)]
        public IReadOnlyList<PixelFormat> PixelFormats
        {
            get { return _pixelFormats; }
            set
            {
                _pixelFormats = value;
                RaisePropertyChanged();
            }
        }

        private VideoSize _videoSize = VideoSize.Original;
        [Display(Name = "清晰度", Description = "清晰度")]
        public VideoSize VideoSize
        {
            get { return _videoSize; }
            set
            {
                _videoSize = value;
                RaisePropertyChanged();
            }
        }

        private Speed _speed = Speed.Medium;
        [Display(Name = "编码速度", Description = "编码速度越慢，则压缩效果及画质越好。preset选项的默认参数为medium")]
        public Speed Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                RaisePropertyChanged();
            }
        }



        private int _constantRateFactor = 28;
        [Display(Name = "恒定速率因子", Description = "它的值越小，画质越高，占用的空间越大。它的可选项为0 ~51，默认为28.当crf在20以下的时候，就能实现视觉上的无损")]
        /// <summary>
        ///crf是Constant Rate Factor的缩写，它的值越小，画质越高，占用的空间越大。它的可选项为0 ~51，默认为28.当crf在20以下的时候，就能实现视觉上的无损
        /// </summary>
        [Range(0, 51, ErrorMessage = "0-51")]
        public int ConstantRateFactor
        {
            get { return _constantRateFactor; }
            set
            {
                _constantRateFactor = value;
                RaisePropertyChanged();
            }
        }

        private double _frameRate;
        [Display(Name = "帧率", Description = "值越大效果越好，占用资源越多")]
        [Range(0.0, 30.0, ErrorMessage = "0.0 - 30.0")]
        public double FrameRate
        {
            get { return _frameRate; }
            set
            {
                _frameRate = value;
                RaisePropertyChanged();
            }
        }




        private long _size;
        [Browsable(false)]
        public long Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }

        private double _bitRate;
        [Display(Name = "码率", Description = "值越大效果越好，占用资源越多")]
        //[Range(0, 8000 * 1000, ErrorMessage = "512-8000")]
        public double BitRate
        {
            get { return _bitRate; }
            set
            {
                _bitRate = value;
                RaisePropertyChanged();
            }
        }


        //private int _width;
        //[Range(10, 8000, ErrorMessage = "1-8000")]
        //public int Width
        //{
        //    get { return _width; }
        //    set
        //    {
        //        _width = value;
        //        RaisePropertyChanged();
        //    }
        //}


        private int _variableBitrate = 4;
        [Display(Name = "可变比特率", Description = "可变比特率 1-5")]
        [Range(1, 5, ErrorMessage = "1-5")]
        public int VariableBitrate
        {
            get { return _variableBitrate; }
            set
            {
                _variableBitrate = value;
                RaisePropertyChanged();
            }
        }


        //private int _height;
        //[Range(10, 8000, ErrorMessage = "1-8000")]
        //public int Height
        //{
        //    get { return _height; }
        //    set
        //    {
        //        _height = value;
        //        RaisePropertyChanged();
        //    }
        //}


        private TimeSpan _startTime;
        [Display(Name = "起始时间")]
        [TypeConverter(typeof(TimeSpanConverter))]
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _endTime;
        [ReadOnly(true)]
        [Display(Name = "视频时长")]
        [TypeConverter(typeof(TimeSpanConverter))]
        public TimeSpan EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                RaisePropertyChanged();
            }
        }

        //private long _span;
        //[Display(Name = "时长(s)")]
        //[Range(1, 60)]
        ////[TypeConverter(typeof(TimeSpanConverter))]
        //public long Span
        //{
        //    get { return _span; }
        //    set
        //    {
        //        _span = value;
        //        RaisePropertyChanged();
        //    }
        //}


        public virtual void AddArguments(FFMpegArgumentOptions options)
        {
            options.WithVideoCodec(Codec)
                               .WithFramerate(FrameRate)
                               .WithAudioCodec(AudioCodec.Aac)
                               .WithVideoBitrate((int)BitRate)
                               .ForcePixelFormat(PixelFormat)
                               .WithSpeedPreset(Speed)
                               .WithConstantRateFactor(ConstantRateFactor)
                               //.Resize(this.OutVideoFormat.Width, this.OutVideoFormat.Height)
                               .Seek(StartTime)
                               .EndSeek(EndTime)
                               .WithVariableBitrate(VariableBitrate)
                               .WithVideoFilters(filterOptions => filterOptions
                               .Scale(VideoSize))
                               .WithFastStart();
        }
    }

    public class VideoFormats : NotifyPropertyChangedBase
    {
        public VideoFormats()
        {
            var collection = GetVideoFormats();
            IEnumerable<IGrouping<ContainerFormat, MediaInfo>> groups = collection.GroupBy(x => x.ContainerFormat);
            Collection = groups.ToObservable();
        }

        private ObservableCollection<IGrouping<ContainerFormat, MediaInfo>> _collection = new ObservableCollection<IGrouping<ContainerFormat, MediaInfo>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IGrouping<ContainerFormat, MediaInfo>> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        private MediaInfo _selectedItem;
        /// <summary> 说明  </summary>
        public MediaInfo SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }


        IEnumerable<MediaInfo> GetVideoFormats()
        {
            //var codecs = typeof(VideoCodec).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var types = typeof(VideoType).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var sizes = Enum.GetValues(typeof(VideoSize));

            foreach (var t in types)
            {
                var tv = t.GetValue(null) as ContainerFormat;
                //foreach (var c in codecs)
                //{
                //    var cv = c.GetValue(null) as Codec;
                //    foreach (var s in sizes)
                //    {
                //        yield return new VideoFormat() { VideoType = tv, Codec = cv, VideoSize = (VideoSize)s };
                //    }
                //}
                //var m = new ContainerFormatPresenter(tv);
                foreach (var s in sizes)
                {
                    yield return new MediaInfo() { ContainerFormat = tv, VideoSize = (VideoSize)s };
                }
            }
        }
    }

    ///// <summary> 说明</summary>
    //public class ContainerFormatPresenter : ModelViewModel<ContainerFormat>
    //{
    //    public ContainerFormatPresenter(ContainerFormat model) : base(model)
    //    {

    //    }
    //}

}
