using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using System;
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
            return new GifMediaInfo(mediaInfo, this.FilePath);
        }

        protected override string CreateOutputPath(string groupPath)
        {
            return base.CreateOutputPath(groupPath);
        }

        protected override bool Start(IRelayCommand s, object e)
        {
            string subOutputPath = Path.ChangeExtension(this.OutputPath, Path.GetExtension(this.FilePath));

            if (File.Exists(this.OutputPath) || File.Exists(subOutputPath))
            {
                var r = MessageProxy.Messager.ShowResult("当前输出文件已存在，点击确定删除历史文件?").Result;
                if (!r)
                {
                    this.Message = "用户取消操作";
                    return false;
                }
                File.Delete(this.OutputPath);
                File.Delete(subOutputPath);
            }
            this.StartTime = DateTime.Now;
            var subProcess = FFMpegArguments.FromFileInput(this.FilePath).OutputToFile(subOutputPath, false,
                options => options.CopyChannel(Channel.All)
                .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
                .Seek(this.OutputMediaInfo.VedioAnalysis.StartTime)
                .EndSeek(this.OutputMediaInfo.VedioAnalysis.EndTime)
                .WithFastStart());
            this.StartProcessor(subProcess);

            var gifProcess = FFMpegArguments.FromFileInput(subOutputPath).OutputToFile(this.OutputPath, false,
               options => options.DisableChannel(Channel.Audio).WithVideoCodec(this.OutputMediaInfo.VedioAnalysis.Codec)
                             .WithFramerate(this.OutputMediaInfo.VedioAnalysis.FrameRate)
                             .WithVideoBitrate((int)this.OutputMediaInfo.VedioAnalysis.BitRate)
                             .ForcePixelFormat(this.OutputMediaInfo.VedioAnalysis.PixelFormat)
                             .WithSpeedPreset(this.OutputMediaInfo.VedioAnalysis.Speed)
                             .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
                             .WithConstantRateFactor(this.OutputMediaInfo.VedioAnalysis.ConstantRateFactor)
                             .WithVariableBitrate(this.OutputMediaInfo.VedioAnalysis.VariableBitrate)
                             .WithVideoFilters(filterOptions => filterOptions
                             .Scale(this.OutputMediaInfo.VedioAnalysis.VideoSize))
                             .WithFastStart());
            this.StartProcessor(gifProcess);

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
            return new GifVedioAnalysis(this.Model, filePath);
        }
    }

    public class GifVedioAnalysis : SubVideoAnalysis
    {
        public GifVedioAnalysis(IMediaAnalysis model, string filePath) : base(model, filePath)
        {
            this.Codecs = FFMpeg.GetCodecs().Where(x => x.Name == "gif").ToList().AsReadOnly();
            this.ContainerFormats = FFMpeg.GetContainerFormats().Where(x => x.Name == "gif").ToList().AsReadOnly();
            this.PixelFormats = FFMpeg.GetPixelFormats();
            this.ContainerFormat = this.ContainerFormats?.FirstOrDefault();
            this.Codec = this.Codecs?.FirstOrDefault();
            this.FrameRate = 5;
            this.VideoSize = VideoSize.Ld;
            this.Speed = Speed.VerySlow;
        }
    }
}
