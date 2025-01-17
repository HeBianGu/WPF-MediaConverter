using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Helpers;
using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Domain.Converter
{
    public class MuteConverterItem : VideoConverterItemBase
    {
        public MuteConverterItem(string filePath, Action<ConverterItemBase> builder = null) : base(filePath, builder)
        {
            this.UseOutToolCommadNames = $"{nameof(PlayOutputCommand)},{nameof(ViewArgumentsCommand)},{nameof(OpenCommand)},{nameof(DeleteCommand)},{nameof(DeleteFileCommand)}";

        }

        //protected override FFMpegArgumentProcessor CreateProcessor()
        //{
        //    FFMpegHelper.ConversionSizeExceptionCheck(FFProbe.Analyse(OutputPath));
        //    return FFMpegArguments.FromFileInput(FilePath).OutputToFile(OutputPath, overwrite: true, delegate (FFMpegArgumentOptions options)
        //    {
        //        options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).CopyChannel(Channel.Video).DisableChannel(Channel.Audio);
        //    });

        //}

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).CopyChannel(Channel.Video).DisableChannel(Channel.Audio);
        }
    }
}
