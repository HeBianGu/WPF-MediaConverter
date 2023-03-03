using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Helpers;
using System.IO;

namespace HeBianGu.App.Converter
{
    public class ExtractAudioConverterItem : ProcessorConverterItemBase
    {
        public ExtractAudioConverterItem(string filePath) : base(filePath)
        {

        }
        protected override FFMpegArgumentProcessor CreateProcessor()
        {
            FFMpegHelper.ExtensionExceptionCheck(OutputPath, FileExtension.Mp3);
            return FFMpegArguments.FromFileInput(FilePath).OutputToFile(OutputPath, overwrite: true,
                delegate (FFMpegArgumentOptions options)
             {
                 options.DisableChannel(Channel.Video);
             });
        }

        protected override string CreateOutputPath(string groupPath)
        {
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(FilePath) + FileExtension.Mp3);
        }
    }
}
