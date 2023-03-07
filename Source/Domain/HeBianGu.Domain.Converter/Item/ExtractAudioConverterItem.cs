using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Helpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace HeBianGu.Domain.Converter
{
    public class ExtractAudioConverterItem : AudioConverterItemBase
    {
        public ExtractAudioConverterItem(string filePath) : base(filePath)
        {

        }

        private bool _useCopyChannel = true;
        [DefaultValue(true)]
        [Display(Name = "复制通道", Description = "启用后不转码，运行速度快")]
        public bool UseCopyChannel
        {
            get { return _useCopyChannel; }
            set
            {
                _useCopyChannel = value;
                RaisePropertyChanged();
            }
        }

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            if (UseCopyChannel)
                options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).CopyChannel(Channel.Audio).DisableChannel(Channel.Video);
            else
            {
                base.CreateArguments(options);
                options.DisableChannel(Channel.Video);
            }
        }

        protected override string CreateOutputPath(string groupPath)
        {
            string extension = OutputMediaInfo.Model.PrimaryAudioStream.CodecName;
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(FilePath) + "." + extension);
        }
    }
}
