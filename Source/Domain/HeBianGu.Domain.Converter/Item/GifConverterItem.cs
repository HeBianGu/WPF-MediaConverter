using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using System;
using System.IO;
using System.Linq;

namespace HeBianGu.Domain.Converter
{
    public class GifConverterItem : VideoConverterItemBase
    {
        public GifConverterItem(string filePath) : base(filePath)
        {

        }

        public override MediaInfo CreateOutputMediaInfo(IMediaAnalysis mediaInfo)
        {
            return new GifMediaInfo(mediaInfo, FilePath);
        }

        protected override string CreateOutputPath(string groupPath)
        {
            return base.CreateOutputPath(groupPath);
        }

        protected override bool Start(IRelayCommand s, object e)
        {
            string subOutputPath = Path.ChangeExtension(OutputPath, Path.GetExtension(FilePath));

            if (File.Exists(OutputPath) || File.Exists(subOutputPath))
            {
                var r = MessageProxy.Messager.ShowResult("当前输出文件已存在，点击确定删除历史文件?").Result;
                if (!r)
                {
                    Message = "用户取消操作";
                    return false;
                }
                File.Delete(OutputPath);
                File.Delete(subOutputPath);
            }
            StartTime = DateTime.Now;
            var subProcess = FFMpegArguments.FromFileInput(FilePath).OutputToFile(subOutputPath, false,
                options => options.CopyChannel(Channel.All)
                .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
                .Seek(OutputMediaInfo.VedioAnalysis.StartTime)
                .EndSeek(OutputMediaInfo.VedioAnalysis.EndTime)
                .WithFastStart());
            StartProcessor(subProcess);

            var gifProcess = FFMpegArguments.FromFileInput(subOutputPath).OutputToFile(OutputPath, false,
               options => options.DisableChannel(Channel.Audio).WithVideoCodec(OutputMediaInfo.VedioAnalysis.Codec)
                             .WithFramerate(OutputMediaInfo.VedioAnalysis.FrameRate)
                             .WithVideoBitrate((int)OutputMediaInfo.VedioAnalysis.BitRate)
                             .ForcePixelFormat(OutputMediaInfo.VedioAnalysis.PixelFormat)
                             .WithSpeedPreset(OutputMediaInfo.VedioAnalysis.Speed)
                             .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
                             .WithConstantRateFactor(OutputMediaInfo.VedioAnalysis.ConstantRateFactor)
                             .WithVariableBitrate(OutputMediaInfo.VedioAnalysis.VariableBitrate)
                             .WithVideoFilters(filterOptions => filterOptions
                             .Scale(OutputMediaInfo.VedioAnalysis.VideoSize))
                             .WithFastStart());
            StartProcessor(gifProcess);

            File.Delete(subOutputPath);
            return true;
        }

    }

    public class GifMediaInfo : MediaInfo
    {
        public GifMediaInfo(IMediaAnalysis model, string filePath) : base(model, filePath)
        {

        }
        protected override VideoAnalysis CreateVedioAnalysis(string filePath)
        {
            return new GifVedioAnalysis(Model, filePath);
        }
    }

    public class GifVedioAnalysis : SubVideoAnalysis
    {
        public GifVedioAnalysis(IMediaAnalysis model, string filePath) : base(model, filePath)
        {
            Codecs = FFMpeg.GetCodecs().Where(x => x.Name == "gif").ToList().AsReadOnly();
            ContainerFormats = FFMpeg.GetContainerFormats().Where(x => x.Name == "gif").ToList().AsReadOnly();
            PixelFormats = FFMpeg.GetPixelFormats();
            ContainerFormat = ContainerFormats?.FirstOrDefault();
            Codec = Codecs?.FirstOrDefault();
            FrameRate = 5;
            VideoSize = VideoSize.Ld;
            Speed = Speed.VerySlow;
        }
    }
}
