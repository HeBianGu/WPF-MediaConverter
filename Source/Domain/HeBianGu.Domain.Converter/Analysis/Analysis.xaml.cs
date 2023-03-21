using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Service.TypeConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Domain.Converter
{
    public abstract class AnalysisBase : ModelViewModel<IMediaAnalysis>
    {
        public AnalysisBase(IMediaAnalysis model) : base(model)
        {


        }

        private Dictionary<string, string> _metaData;
        [Display(Name = "基础信息", GroupName = "", Description = "基础信息")]
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
        [Displayer(Name = "文件格式", GroupName = "视频,音频,输入参数,输出参数", Description = "文件格式", Icon = "\xe7aa")]
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
        [Displayer(Name = "编码方式", GroupName = "视频,音频,输入参数,输出参数", Description = "编码方式", Icon = "\xe7ad")]
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
        [Displayer(Name = "编码速度", GroupName = "视频,音频,输出参数", Description = "编码速度越慢，则压缩效果及画质越好。preset选项的默认参数为medium", Icon = "\xe7ab")]
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
        [Displayer(Name = "恒定速率因子", GroupName = "视频,音频,输出参数", Description = "它的值越小，画质越高，占用的空间越大。它的可选项为0 ~51，默认为28.当crf在20以下的时候，就能实现视觉上的无损", Icon = "\xe7af")]
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
        [TypeConverter(typeof(FileSizeDisplayTypeConverter))]
        [ReadOnly(true)]
        [Displayer(Name = "文件大小", GroupName = "视频,音频,输入参数,输出参数", Description = "文件大小", Icon = "\xe7a3")]
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
        [Unit("bps")]
        [Displayer(Name = "码率", GroupName = "视频,音频,输入参数,输出参数", Description = "值越大效果越好，占用资源越多", Icon = "\xe7a5")]
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
        [Displayer(Name = "可变比特率", GroupName = "视频,音频,输出参数", Description = "可变比特率 1-5", Icon = "\xe7a4")]
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
        [Displayer(Name = "截取起始时间", GroupName = "视频,音频,输入参数,截取时间", Icon = "\xe84d")]
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
        [Displayer(Name = "截取结束时间", GroupName = "视频,音频,输入参数,截取时间", Icon = "\xe84d")]
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
