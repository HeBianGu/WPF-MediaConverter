using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace HeBianGu.Domain.Converter
{
    public class VideoAnalysis : AnalysisBase
    {
        public VideoAnalysis(IMediaAnalysis model, string filePath) : base(model)
        {
            string extension = Path.GetExtension(filePath);
            //  ToDo：model.Format.FormatName表示可以转换的格式列表？ 
            string fn = null;
            var splits = model.Format.FormatName.Split(',');
            if (splits.Count() > 1)
                fn = splits.FirstOrDefault(x => extension.ToUpper().EndsWith(x.ToUpper()));

            if (string.IsNullOrEmpty(fn))
                fn = splits[0];

            ContainerFormat = FFMpeg.GetContainerFormat(fn);
            Codec = FFMpeg.GetCodec(model.PrimaryVideoStream.CodecName);
            FrameRate = model.PrimaryVideoStream.FrameRate;
            PixelFormat = FFMpeg.GetPixelFormat(model.PrimaryVideoStream.PixelFormat);
            BitRate = model.PrimaryVideoStream.BitRate;
            var find = Enum.GetValues<VideoSize>().FirstOrDefault(x => (int)x == model.PrimaryVideoStream.Height);
            VideoSize = find == default ? VideoSize.FullHd : find;
            StartTime = model.PrimaryVideoStream.StartTime;
            EndTime = model.PrimaryVideoStream.Duration;
            MetaData = model.PrimaryVideoStream.Tags ?? new Dictionary<string, string>();
        }

        public VideoAnalysis() : base(null)
        {

        }

        private PixelFormat _pixelFormat;
        [Displayer(Name = "像素格式", GroupName = "视频,输入参数,输出参数", Description = "像素格式", Icon = "\xe7a9")]
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
        [Displayer(Name = "清晰度", GroupName = "视频,输出参数", Description = "清晰度", Icon = "\xe7ac")]
        public VideoSize VideoSize
        {
            get { return _videoSize; }
            set
            {
                _videoSize = value;
                RaisePropertyChanged();
            }
        }


        private double _frameRate;
        [Displayer(Name = "帧率", GroupName = "视频,输入参数,输出参数", Description = "值越大效果越好，占用资源越多", Icon = "\xe7a3")]
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

        private bool _useDrawText;
        [Display(Name = "显示水印", GroupName = "水印", Description = "显示水印")]
        public bool UseDrawText
        {
            get { return _useDrawText; }
            set
            {
                _useDrawText = value;
                RaisePropertyChanged();
            }
        }


        private string _text = "HeBianGu";
        [DefaultValue("HeBianGu")]
        [Display(Name = "默认水印文本", GroupName = "水印", Description = "默认水印文本")]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        private int _fontSize = 50;
        [DefaultValue(50)]
        [Display(Name = "默认文本大小", GroupName = "水印", Description = "默认文本大小")]
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }


        private string _fontcolor = "white";
        [DefaultValue("white")]
        [Display(Name = "默认文本颜色", GroupName = "水印", Description = "默认文本颜色")]
        public string Fontcolor
        {
            get { return _fontcolor; }
            set
            {
                _fontcolor = value;
                RaisePropertyChanged();
            }
        }


        private bool _box = true;
        [DefaultValue(true)]
        [Display(Name = "显示背景颜色", GroupName = "水印", Description = "显示背景颜色")]
        public bool Box
        {
            get { return _box; }
            set
            {
                _box = value;
                RaisePropertyChanged();
            }
        }


        private string _boxcolor = "black@0.2";
        [DefaultValue("black@0.2")]
        [Display(Name = "默认背景颜色", GroupName = "水印", Description = "默认背景颜色")]
        public string Boxcolor
        {
            get { return _boxcolor; }
            set
            {
                _boxcolor = value;
                RaisePropertyChanged();
            }
        }


        private int _boxborderw = 15;
        [Range(0, 1000)]
        [DefaultValue(15)]
        [Display(Name = "背景内边距", GroupName = "水印", Description = "背景内边距")]
        public int Boxborderw
        {
            get { return _boxborderw; }
            set
            {
                _boxborderw = value;
                RaisePropertyChanged();
            }
        }


        private string _x = "w-text_w-10";
        [DefaultValue("w-text_w-10")]
        [Display(Name = "默认X位置", GroupName = "水印", Description = "默认X位置")]
        public string X
        {
            get { return _x; }
            set
            {
                _x = value;
                RaisePropertyChanged();
            }
        }


        private string _y = "10";
        [DefaultValue("10")]
        [Display(Name = "默认Y位置", GroupName = "水印", Description = "默认Y位置")]
        public string Y
        {
            get { return _y; }
            set
            {
                _y = value;
                RaisePropertyChanged();
            }
        }



        public override void CreateArguments(FFMpegArgumentOptions options)
        {
            options.WithVideoCodec(Codec)
                               .WithFramerate(FrameRate)
                               .WithAudioCodec(AudioCodec.Aac)
                               .WithVideoBitrate((int)BitRate)
                               .ForcePixelFormat(PixelFormat)
                               .WithSpeedPreset(Speed)
                               .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
                               //.UsingShortest(this.UsingShortest)
                               .WithConstantRateFactor(ConstantRateFactor)
                               //.Resize(this.OutVideoFormat.Width, this.OutVideoFormat.Height)
                               .Seek(StartTime)
                               .EndSeek(EndTime)
                               .WithVariableBitrate(VariableBitrate)
                               .WithVideoFilters(filterOptions =>
                               {
                                   filterOptions.Scale(VideoSize);
                                   if (UseDrawText && !string.IsNullOrEmpty(Text))
                                   {
                                       filterOptions
                                       .DrawText(DrawTextOptions
                                       .Create(Text, "")
                                       .WithParameter("fontcolor", Fontcolor)
                                       .WithParameter("fontsize", FontSize.ToString())
                                       .WithParameter("box", Box ? "1" : "0")
                                       .WithParameter("boxcolor", Boxcolor)
                                       .WithParameter("boxborderw", Boxborderw.ToString())
                                       .WithParameter("x", X)
                                       .WithParameter("y", Y));
                                   }
                               })
                               .WithFastStart();
        }

    }
}
