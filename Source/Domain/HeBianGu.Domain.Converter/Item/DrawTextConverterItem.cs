using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;

namespace HeBianGu.Domain.Converter
{
    public class DrawTextConverterItem : VideoConverterItemBase
    {
        public DrawTextConverterItem(string filePath) : base(filePath)
        {

        }

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            //options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).CopyChannel(Channel.Video).DisableChannel(Channel.Audio);

            options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).WithVideoFilters(filterOptions =>
            filterOptions.DrawText(DrawTextOptions
                                       .Create(OutputMediaInfo.VedioAnalysis.Text, "")
                                       .WithParameter("fontcolor", OutputMediaInfo.VedioAnalysis.Fontcolor)
                                       .WithParameter("fontsize", OutputMediaInfo.VedioAnalysis.FontSize.ToString())
                                       .WithParameter("box", OutputMediaInfo.VedioAnalysis.Box ? "1" : "0")
                                       .WithParameter("boxcolor", OutputMediaInfo.VedioAnalysis.Boxcolor)
                                       .WithParameter("boxborderw", OutputMediaInfo.VedioAnalysis.Boxborderw.ToString())
                                       .WithParameter("x", OutputMediaInfo.VedioAnalysis.X)
                                       .WithParameter("y", OutputMediaInfo.VedioAnalysis.Y)));
            //.WithParameter("x", "(w-text_w)/2")
            //                          .WithParameter("y", "(h-text_h)/2")));
            //base.CreateArguments(options);
        }
    }
}
