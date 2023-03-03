using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Helpers;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    public class MuteConverterItem : ProcessorConverterItemBase
    {
        public MuteConverterItem(string filePath) : base(filePath)
        {

        }

        protected override bool Start(IRelayCommand s, object e)
        {
            FFMpeg.Mute(FilePath, OutputPath);
            Message = "完成";
            return true;
        }

        protected override FFMpegArgumentProcessor CreateProcessor()
        {
            FFMpegHelper.ConversionSizeExceptionCheck(FFProbe.Analyse(OutputPath));
            return FFMpegArguments.FromFileInput(FilePath).OutputToFile(OutputPath, overwrite: true, delegate (FFMpegArgumentOptions options)
            {
                options.CopyChannel(Channel.Video).DisableChannel(Channel.Audio);
            });

        }
    }
}
