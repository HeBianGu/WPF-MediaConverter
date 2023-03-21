using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HeBianGu.Domain.Converter
{
   
    [Displayer(Name = "视频转Gif", Icon = "\xe805", GroupName = "视频", Order = 1, Description = "截取视频一段数据生成gif文件")]
    public class GifConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            var result = new GifConverterItem(filePath, x =>
            {
                x.OutputMediaInfo.VedioAnalysis.FrameRate = FrameRate;
                x.OutputMediaInfo.VedioAnalysis.VideoSize = VideoSize;
                x.OutputMediaInfo.VedioAnalysis.Speed = Speed;
            });
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

        private VideoSize _videoSize = VideoSize.LD;
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
