using FFMpegCore;
using FFMpegCore.Enums;
using System.IO;
using System.Linq;

namespace HeBianGu.App.Converter
{
    public class GifConverterItem : VideoConverterItemBase
    {
        public GifConverterItem(string filePath) : base(filePath)
        {

        }

        public override MediaInfo CreateOutputMediaInfo(IMediaAnalysis mediaInfo)
        {
            var result = new MediaInfo(mediaInfo, this.FilePath);
            result.Size = new FileInfo(this.FilePath).Length;
            result.VedioAnalysis.Codecs = FFMpeg.GetCodecs().Where(x => x.Name == "gif").ToList().AsReadOnly();
            result.VedioAnalysis.ContainerFormats = FFMpeg.GetContainerFormats().Where(x => x.Name == "gif").ToList().AsReadOnly();
            result.VedioAnalysis.PixelFormats = FFMpeg.GetPixelFormats();
            result.VedioAnalysis.ContainerFormat = result.VedioAnalysis.ContainerFormats?.FirstOrDefault();
            result.VedioAnalysis.Codec = result.VedioAnalysis.Codecs?.FirstOrDefault();

            result.VedioAnalysis.StartTime = mediaInfo.Format.StartTime;
            result.VedioAnalysis.EndTime = mediaInfo.Format.Duration;
            return result;
        }
    }
}
