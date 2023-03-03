using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频压缩", Icon = "\xe751", GroupName = "Processor", Order = 5, Description = "处理器相关数据监控")]
    public class CompressConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            var video = new VideoConverterItem(filePath);
            video.OutputMediaInfo.BitRate = video.OutputMediaInfo.BitRate * 0.6;
            video.OutputMediaInfo.FrameRate = video.OutputMediaInfo.FrameRate * 0.6;
            video.OutputMediaInfo.VideoSize = VideoSize.Ld;
            //video.OutVideoFormat.PixelFormat=FFMpeg.GetPixelFormat()
            video.OutputMediaInfo.ConstantRateFactor = 30;
            //  Do ：编码速度越慢，则压缩效果及画质越好。preset选项的默认参数为medium
            video.OutputMediaInfo.Speed = Speed.VerySlow;
            return video;
        }
    }
}
