using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Message;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Survey;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace HeBianGu.App.Converter
{
    /// <summary> 说明</summary>
    public class MediaInfo : ModelViewModel<IMediaAnalysis>
    {
        public MediaInfo(IMediaAnalysis model, string filePath) : base(model)
        {
            this.VedioAnalysis = new VedioAnalysis(model, filePath);
            this.AudioAnalysis = new AudioAnalysis(model, filePath);

            //string extension = Path.GetExtension(filePath);
            ////  ToDo：model.Format.FormatName表示可以转换的格式列表？ 
            //string fn = null;
            //var splits = model.Format.FormatName.Split(',');
            //if (splits.Count() > 1)
            //    fn = splits.FirstOrDefault(x => extension.ToUpper().EndsWith(x.ToUpper()));

            //if (string.IsNullOrEmpty(fn))
            //    fn = splits[0];

            //this.ContainerFormat = FFMpeg.GetContainerFormat(fn);
            //this.Codec = FFMpeg.GetCodec(model.PrimaryVideoStream.CodecName);
            //this.FrameRate = model.PrimaryVideoStream.FrameRate;
            //this.PixelFormat = FFMpeg.GetPixelFormat(model.PrimaryVideoStream.PixelFormat);
            //this.BitRate = model.PrimaryVideoStream.BitRate;
            ////this.Height = model.PrimaryVideoStream.Height;
            ////this.Width = model.PrimaryVideoStream.Width;
            //var find = Enum.GetValues<VideoSize>().FirstOrDefault(x => (int)x == model.PrimaryVideoStream.Height);
            //this.VideoSize = find == default ? VideoSize.FullHd : find;
            //this.StartTime = model.Format.StartTime;
            //this.EndTime = model.Format.Duration;

            this.MetaData = model.Format.Tags ?? new Dictionary<string, string>();
            //this.AudioMetaData = model.PrimaryAudioStream.Tags ?? new Dictionary<string, string>();
            //this.VedioMetaData = model.PrimaryVideoStream.Tags ?? new Dictionary<string, string>();

        }

        public MediaInfo() : base(null)
        {

        }

        private Dictionary<string, string> _metaData;
        [Display(Name = "文件元数据", Description = "文件元数据")]
        [PropertyItemType(typeof(DictionaryPropertyViewItem))]
        public Dictionary<string, string> MetaData
        {
            get { return _metaData; }
            set
            {
                _metaData = value;
                RaisePropertyChanged();
            }
        }

        //private Dictionary<string, string> _audiMetaData;
        //[Display(Name = "音频元数据", GroupName = "音频", Description = "音频元数据")]
        //[PropertyItemType(typeof(DictionaryPropertyViewItem))]
        //public Dictionary<string, string> AudioMetaData
        //{
        //    get { return _audiMetaData; }
        //    set
        //    {
        //        _audiMetaData = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private Dictionary<string, string> _vedioMetaData;
        //[Display(Name = "视频元数据", GroupName = "视频", Description = "视频元数据")]
        //[PropertyItemType(typeof(DictionaryPropertyViewItem))]
        //public Dictionary<string, string> VedioMetaData
        //{
        //    get { return _vedioMetaData; }
        //    set
        //    {
        //        _vedioMetaData = value;
        //        RaisePropertyChanged();
        //    }
        //}


        //private ContainerFormat _containerFormat;
        //[Display(Name = "文件格式", Description = "文件格式")]
        //[BindingGetSelectSourceProperty(nameof(ContainerFormats))]
        //[PropertyItemType(typeof(OnlyComboBoxSelectSourcePropertyItem))]
        //public ContainerFormat ContainerFormat
        //{
        //    get { return _containerFormat; }
        //    set
        //    {
        //        _containerFormat = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private IReadOnlyList<ContainerFormat> _containerFormats = new List<ContainerFormat>();
        //[Browsable(false)]
        //public IReadOnlyList<ContainerFormat> ContainerFormats
        //{
        //    get { return _containerFormats; }
        //    set
        //    {
        //        _containerFormats = value;
        //        RaisePropertyChanged();
        //    }
        //}



        //private Codec _codec;
        //[Display(Name = "视频编码方式", GroupName = "视频", Description = "编码方式")]
        //[BindingGetSelectSourceProperty(nameof(Codecs))]
        //[PropertyItemType(typeof(OnlyComboBoxSelectSourcePropertyItem))]
        //public Codec Codec
        //{
        //    get { return _codec; }
        //    set
        //    {
        //        _codec = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private IReadOnlyList<Codec> _codecs = new List<Codec>();
        //[Browsable(false)]
        //public IReadOnlyList<Codec> Codecs
        //{
        //    get { return _codecs; }
        //    set
        //    {
        //        _codecs = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private PixelFormat _pixelFormat;
        //[Display(Name = "像素格式", GroupName = "视频", Description = "像素格式")]
        //[BindingGetSelectSourceProperty(nameof(PixelFormats))]
        //[PropertyItemType(typeof(OnlyComboBoxSelectSourcePropertyItem))]
        //public PixelFormat PixelFormat
        //{
        //    get { return _pixelFormat; }
        //    set
        //    {
        //        _pixelFormat = value;
        //        RaisePropertyChanged();
        //    }
        //}


        //private IReadOnlyList<PixelFormat> _pixelFormats = new List<PixelFormat>();
        //[Browsable(false)]
        //public IReadOnlyList<PixelFormat> PixelFormats
        //{
        //    get { return _pixelFormats; }
        //    set
        //    {
        //        _pixelFormats = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private VideoSize _videoSize = VideoSize.Original;
        //[Display(Name = "清晰度", GroupName = "视频", Description = "清晰度")]
        //public VideoSize VideoSize
        //{
        //    get { return _videoSize; }
        //    set
        //    {
        //        _videoSize = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private Speed _speed = Speed.Medium;
        //[Display(Name = "编码速度", GroupName = "视频,音频", Description = "编码速度越慢，则压缩效果及画质越好。preset选项的默认参数为medium")]
        //public Speed Speed
        //{
        //    get { return _speed; }
        //    set
        //    {
        //        _speed = value;
        //        RaisePropertyChanged();
        //    }
        //}



        //private int _constantRateFactor = 28;
        //[Display(Name = "恒定速率因子", GroupName = "视频,音频", Description = "它的值越小，画质越高，占用的空间越大。它的可选项为0 ~51，默认为28.当crf在20以下的时候，就能实现视觉上的无损")]
        ///// <summary>
        /////crf是Constant Rate Factor的缩写，它的值越小，画质越高，占用的空间越大。它的可选项为0 ~51，默认为28.当crf在20以下的时候，就能实现视觉上的无损
        ///// </summary>
        //[Range(0, 51, ErrorMessage = "0-51")]
        //public int ConstantRateFactor
        //{
        //    get { return _constantRateFactor; }
        //    set
        //    {
        //        _constantRateFactor = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private double _frameRate;
        //[Display(Name = "帧率", GroupName = "视频", Description = "值越大效果越好，占用资源越多")]
        //[Range(0.0, 30.0, ErrorMessage = "0.0 - 30.0")]
        //public double FrameRate
        //{
        //    get { return _frameRate; }
        //    set
        //    {
        //        _frameRate = value;
        //        RaisePropertyChanged();
        //    }
        //}




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

        //private double _bitRate;
        //[Display(Name = "码率", GroupName = "视频,音频", Description = "值越大效果越好，占用资源越多")]
        ////[Range(0, 8000 * 1000, ErrorMessage = "512-8000")]
        //public double BitRate
        //{
        //    get { return _bitRate; }
        //    set
        //    {
        //        _bitRate = value;
        //        RaisePropertyChanged();
        //    }
        //}


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


        //private int _variableBitrate = 4;
        //[Display(Name = "可变比特率", GroupName = "视频,音频", Description = "可变比特率 1-5")]
        //[Range(1, 5, ErrorMessage = "1-5")]
        //public int VariableBitrate
        //{
        //    get { return _variableBitrate; }
        //    set
        //    {
        //        _variableBitrate = value;
        //        RaisePropertyChanged();
        //    }
        //}


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


        //private TimeSpan _startTime;
        //[Display(Name = "截取起始时间", GroupName = "视频,音频")]
        //[TypeConverter(typeof(TimeSpanConverter))]
        //public TimeSpan StartTime
        //{
        //    get { return _startTime; }
        //    set
        //    {
        //        _startTime = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private TimeSpan _endTime;
        ////[ReadOnly(true)]
        //[Display(Name = "截取结束时间", GroupName = "视频,音频")]
        //[TypeConverter(typeof(TimeSpanConverter))]
        //public TimeSpan EndTime
        //{
        //    get { return _endTime; }
        //    set
        //    {
        //        _endTime = value;
        //        RaisePropertyChanged();
        //    }
        //}

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


        //private bool _usingMultithreading;
        //[DefaultValue(true)]
        //[Display(Name = "启用多线程")]
        //public bool UsingMultithreading
        //{
        //    get { return _usingMultithreading; }
        //    set
        //    {
        //        _usingMultithreading = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private int _threads;
        //[DefaultValue(50)]
        //[Display(Name = "线程数")]
        //public int Threads
        //{
        //    get { return _threads; }
        //    set
        //    {
        //        _threads = value;
        //        RaisePropertyChanged();
        //    }
        //}


        //private bool _usingShortest;
        //[DefaultValue(false)]
        //[Display(Name = "按音频最短时间",Description = "音频文件结束，输出视频就结束")]
        //public bool UsingShortest
        //{
        //    get { return _usingShortest; }
        //    set
        //    {
        //        _usingShortest = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //public virtual void CreateArguments(FFMpegArgumentOptions options)
        //{
        //    options.WithVideoCodec(Codec)
        //                       .WithFramerate(FrameRate)
        //                       .WithAudioCodec(AudioCodec.Aac)
        //                       .WithVideoBitrate((int)BitRate)
        //                       .ForcePixelFormat(PixelFormat)
        //                       .WithSpeedPreset(Speed)
        //                       .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
        //                       //.UsingShortest(this.UsingShortest)
        //                       .WithConstantRateFactor(ConstantRateFactor)
        //                       //.Resize(this.OutVideoFormat.Width, this.OutVideoFormat.Height)
        //                       .Seek(StartTime)
        //                       .EndSeek(EndTime)
        //                       .WithVariableBitrate(VariableBitrate)
        //                       .WithVideoFilters(filterOptions => filterOptions
        //                       .Scale(VideoSize))
        //                       .WithFastStart();
        //}


        private VedioAnalysis _vedioAnalysis;
        /// <summary> 说明  </summary>
        public VedioAnalysis VedioAnalysis
        {
            get { return _vedioAnalysis; }
            set
            {
                _vedioAnalysis = value;
                RaisePropertyChanged();
            }
        }


        private AudioAnalysis _audioAnalysis;
        /// <summary> 说明  </summary>
        public AudioAnalysis AudioAnalysis
        {
            get { return _audioAnalysis; }
            set
            {
                _audioAnalysis = value;
                RaisePropertyChanged();
            }
        }

    }

    public abstract class AnalysisBase : ModelViewModel<IMediaAnalysis>
    {
        public AnalysisBase(IMediaAnalysis model) : base(model)
        {


        }

        private Dictionary<string, string> _metaData;
        [Display(Name = "基础信息", GroupName = "视频", Description = "基础信息")]
        [PropertyItemType(typeof(DictionaryPropertyViewItem))]
        public Dictionary<string, string> MetaData
        {
            get { return _metaData; }
            set
            {
                _metaData = value;
                RaisePropertyChanged();
            }
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
        [Display(Name = "编码方式", GroupName = "视频", Description = "编码方式")]
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


        private Speed _speed = Speed.Medium;
        [Display(Name = "编码速度", GroupName = "视频,音频", Description = "编码速度越慢，则压缩效果及画质越好。preset选项的默认参数为medium")]
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
        [Display(Name = "恒定速率因子", GroupName = "视频,音频", Description = "它的值越小，画质越高，占用的空间越大。它的可选项为0 ~51，默认为28.当crf在20以下的时候，就能实现视觉上的无损")]
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
        [Display(Name = "码率", GroupName = "视频,音频", Description = "值越大效果越好，占用资源越多")]
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


        private int _variableBitrate = 4;
        [Display(Name = "可变比特率", GroupName = "视频,音频", Description = "可变比特率 1-5")]
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

        private TimeSpan _startTime;
        [Display(Name = "截取起始时间", GroupName = "视频,音频")]
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
        //[ReadOnly(true)]
        [Display(Name = "截取结束时间", GroupName = "视频,音频")]
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

        public abstract void CreateArguments(FFMpegArgumentOptions options);
    }
}
