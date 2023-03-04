using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace HeBianGu.App.Converter
{
    public class VedioAnalysis : AnalysisBase
    {
        public VedioAnalysis(IMediaAnalysis model, string filePath) : base(model)
        {
            string extension = Path.GetExtension(filePath);
            //  ToDo：model.Format.FormatName表示可以转换的格式列表？ 
            string fn = null;
            var splits = model.Format.FormatName.Split(',');
            if (splits.Count() > 1)
                fn = splits.FirstOrDefault(x => extension.ToUpper().EndsWith(x.ToUpper()));

            if (string.IsNullOrEmpty(fn))
                fn = splits[0];

            this.ContainerFormat = FFMpeg.GetContainerFormat(fn);
            this.Codec = FFMpeg.GetCodec(model.PrimaryVideoStream.CodecName);
            this.FrameRate = model.PrimaryVideoStream.FrameRate;
            this.PixelFormat = FFMpeg.GetPixelFormat(model.PrimaryVideoStream.PixelFormat);
            this.BitRate = model.PrimaryVideoStream.BitRate;
            var find = Enum.GetValues<VideoSize>().FirstOrDefault(x => (int)x == model.PrimaryVideoStream.Height);
            this.VideoSize = find == default ? VideoSize.FullHd : find;
            this.StartTime = model.PrimaryVideoStream.StartTime;
            this.EndTime = model.PrimaryVideoStream.Duration;
            this.MetaData = model.PrimaryVideoStream.Tags ?? new Dictionary<string, string>();
        }

        public VedioAnalysis() : base(null)
        {

        }

        private PixelFormat _pixelFormat;
        [Display(Name = "像素格式", GroupName = "视频", Description = "像素格式")]
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
        [Display(Name = "清晰度", GroupName = "视频", Description = "清晰度")]
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
        [Display(Name = "帧率", GroupName = "视频", Description = "值越大效果越好，占用资源越多")]
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
                               .WithVideoFilters(filterOptions => filterOptions
                               .Scale(VideoSize))
                               .WithFastStart();
        }

    }
}
