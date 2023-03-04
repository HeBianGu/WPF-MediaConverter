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
            video.OutputMediaInfo.VedioAnalysis.BitRate = (int)video.OutputMediaInfo.VedioAnalysis.BitRate * 0.6;
            video.OutputMediaInfo.VedioAnalysis.FrameRate = (int)video.OutputMediaInfo.VedioAnalysis.FrameRate * 0.6;
            video.OutputMediaInfo.VedioAnalysis.VideoSize = VideoSize.Ld;
            //video.OutVideoFormat.PixelFormat=FFMpeg.GetPixelFormat()
            video.OutputMediaInfo.VedioAnalysis.ConstantRateFactor = 30;
            //  Do ：编码速度越慢，则压缩效果及画质越好。preset选项的默认参数为medium
            video.OutputMediaInfo.VedioAnalysis.Speed = Speed.VerySlow;
            return video;
        }
    }
}
