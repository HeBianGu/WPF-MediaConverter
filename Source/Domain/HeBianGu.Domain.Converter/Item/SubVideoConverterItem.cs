using FFMpegCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using FFMpegCore.Enums;
using System.Linq;
using System;

namespace HeBianGu.Domain.Converter
{
    public class SubVideoConverterItem : VideoConverterItemBase
    {
        public SubVideoConverterItem(string filePath, Action<ConverterItemBase> builder = null) : base(filePath, builder)
        {
            this.UseOutToolCommadNames = $"{nameof(PlayOutputCommand)},{nameof(ViewArgumentsCommand)},{nameof(OpenCommand)},{nameof(DeleteCommand)},{nameof(DeleteFileCommand)},{nameof(OutputTimePanCommand)}";
        }

        public override MediaInfo CreateOutputMediaInfo(IMediaAnalysis mediaInfo)
        {
            return new SubMediaInfo(mediaInfo, FilePath);
        }

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            //options.CopyChannel(Channel.All)
            options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
                .Seek(OutputMediaInfo.VedioAnalysis.StartTime)
                .EndSeek(OutputMediaInfo.VedioAnalysis.EndTime)
                .WithFastStart();

        }
    }

    public class SubMediaInfo : MediaInfo
    {
        public SubMediaInfo(IMediaAnalysis model, string filePath) : base(model, filePath)
        {

        }

        protected override VideoAnalysis CreateVedioAnalysis(string filePath)
        {
            return new SubVideoAnalysis(Model, filePath);
        }

    }

    public class SubVideoAnalysis : VideoAnalysis
    {
        public SubVideoAnalysis(IMediaAnalysis model, string filePath) : base(model, filePath)
        {
            StartTime = model.Format.StartTime;
            EndTime = model.Format.Duration;
        }
    }
}
