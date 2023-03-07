using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Helpers;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Domain.Converter
{
    public class MuteConverterItem : VideoConverterItemBase
    {
        public MuteConverterItem(string filePath) : base(filePath)
        {

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
