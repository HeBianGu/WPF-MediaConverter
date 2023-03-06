using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频压缩", Icon = "\xe751", GroupName = "Processor", Order = 5, Description = "处理器相关数据监控")]
    public class CompressConverterGroup : ConverterGroupBase
    {
        private double _persent = 0.5;
        [Range(0.1, 1.0)]
        [DefaultValue(0.5)]
        [Display(Name = "压缩系数", GroupName = "配置", Description = "压缩比率")]
        public double Persent
        {
            get { return _persent; }
            set
            {
                _persent = value;
                RaisePropertyChanged();
            }
        }

        private Speed _speed = Speed.VerySlow;
        [Display(Name = "编码速度", GroupName = "配置", Description = "编码速度越慢，则压缩效果及画质越好。preset选项的默认参数为medium")]
        public Speed Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                RaisePropertyChanged();
            }
        }
        private int _constantRateFactor = 40;
        [Display(Name = "恒定速率因子", GroupName = "配置", Description = "它的值越小，画质越高，占用的空间越大。它的可选项为0 ~51，默认为28.当crf在20以下的时候，就能实现视觉上的无损")]
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

        private VideoSize _videoSize = VideoSize.Ld;
        [Display(Name = "清晰度", GroupName = "配置", Description = "清晰度")]
        public VideoSize VideoSize
        {
            get { return _videoSize; }
            set
            {
                _videoSize = value;
                RaisePropertyChanged();
            }
        }

        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            var video = new VideoConverterItem(filePath);
            video.OutputMediaInfo.VedioAnalysis.BitRate = (int)video.OutputMediaInfo.VedioAnalysis.BitRate * this.Persent;
            video.OutputMediaInfo.VedioAnalysis.FrameRate = (int)video.OutputMediaInfo.VedioAnalysis.FrameRate * this.Persent;
            video.OutputMediaInfo.VedioAnalysis.VideoSize = this.VideoSize;
            //video.OutVideoFormat.PixelFormat=FFMpeg.GetPixelFormat()
            video.OutputMediaInfo.VedioAnalysis.ConstantRateFactor = this.ConstantRateFactor;
            //  Do ：编码速度越慢，则压缩效果及画质越好。preset选项的默认参数为medium
            video.OutputMediaInfo.VedioAnalysis.Speed = this.Speed;
            return video;
        }
    }
}
