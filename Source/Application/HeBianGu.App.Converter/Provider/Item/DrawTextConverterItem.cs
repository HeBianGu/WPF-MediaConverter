using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;

namespace HeBianGu.App.Converter
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
                                       .Create(this.OutputMediaInfo.VedioAnalysis.Text, "")
                                       .WithParameter("fontcolor", this.OutputMediaInfo.VedioAnalysis.Fontcolor)
                                       .WithParameter("fontsize", this.OutputMediaInfo.VedioAnalysis.FontSize.ToString())
                                       .WithParameter("box", this.OutputMediaInfo.VedioAnalysis.Box ? "1" : "0")
                                       .WithParameter("boxcolor", this.OutputMediaInfo.VedioAnalysis.Boxcolor)
                                       .WithParameter("boxborderw", this.OutputMediaInfo.VedioAnalysis.Boxborderw.ToString())
                                       .WithParameter("x", this.OutputMediaInfo.VedioAnalysis.X)
                                       .WithParameter("y", this.OutputMediaInfo.VedioAnalysis.Y)));
            //.WithParameter("x", "(w-text_w)/2")
            //                          .WithParameter("y", "(h-text_h)/2")));
            //base.CreateArguments(options);
        }
    }
}
