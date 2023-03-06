using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频转Gif", Icon = "\xe751", GroupName = "Processor", Order = 1, Description = "处理器相关数据监控")]
    public class GifConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            var result = new GifConverterItem(filePath);
            result.OutputMediaInfo.VedioAnalysis.FrameRate = this.FrameRate;
            result.OutputMediaInfo.VedioAnalysis.VideoSize = this.VideoSize;
            result.OutputMediaInfo.VedioAnalysis.Speed = this.Speed;
            return result;
        }


        private int _frameRate = 5;
        [Displayer(Name = "默认帧率", Icon = "\xe751", GroupName = "配置", Description = "默认帧率")]
        public int FrameRate
        {
            get { return _frameRate; }
            set
            {
                _frameRate = value;
                RaisePropertyChanged();
            }
        }

        private VideoSize _videoSize = VideoSize.Ld;
        [Displayer(Name = "默认清晰度", Icon = "\xe751", GroupName = "配置", Description = "默认清晰度")]
        public VideoSize VideoSize
        {
            get { return _videoSize; }
            set
            {
                _videoSize = value;
                RaisePropertyChanged();
            }
        }


        private Speed _speed = Speed.VerySlow;
        [Display(Name = "默认编码速度", GroupName = "配置", Description = "编码速度越慢，则压缩效果及画质越好。preset选项的默认参数为medium")]
        public Speed Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                RaisePropertyChanged();
            }
        }
    }
}
