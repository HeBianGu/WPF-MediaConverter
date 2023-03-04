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
        //protected override FFMpegArgumentProcessor CreateProcessor()
        //{
        //    //FFMpegHelper.ExtensionExceptionCheck(OutputPath, FileExtension.Mp3);
        //    return FFMpegArguments.FromFileInput(FilePath).OutputToFile(OutputPath, overwrite: true,
        //        delegate (FFMpegArgumentOptions options)
        //     {
        //         options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).CopyChannel(Channel.Audio).DisableChannel(Channel.Video);
        //     });
        //}

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).CopyChannel(Channel.Audio).DisableChannel(Channel.Video);
        }

        protected override string CreateOutputPath(string groupPath)
        {
            string extension = this.OutputMediaInfo.Model.PrimaryAudioStream.CodecName;
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(FilePath) + "."+ extension);
        }
    }
}
