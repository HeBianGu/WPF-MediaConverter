using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Converter
{
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
