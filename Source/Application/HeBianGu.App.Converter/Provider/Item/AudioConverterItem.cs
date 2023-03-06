using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Helpers;
using HeBianGu.Base.WpfBase;
using System.IO;

namespace HeBianGu.App.Converter
{
    public class AudioConverterItemBase : ProcessorConverterItemBase
    {
        public AudioConverterItemBase(string filePath) : base(filePath)
        {
            this.InputAnalysis = this.InputMediaInfo.AudioAnalysis;
            this.OutputAnalysis = this.OutputMediaInfo.AudioAnalysis;
        }


        [Displayer(Name = "设置输出参数", Icon = Icons.Set, GroupName = "操作,输出", Description = "设置输出参数")]
        public RelayCommand OutPutSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(this.OutputMediaInfo.AudioAnalysis, null, "设置输出参数");

            //(视频码率 + 音频码率) * 时长 / 8 = 文件大小
            this.OutputMediaInfo.Size = (long)((this.OutputMediaInfo.AudioAnalysis.BitRate + this.OutputMediaInfo.AudioAnalysis.BitRate) * this.OutputMediaInfo.Model.Duration.TotalSeconds / 8);
        });

        [Displayer(Name = "查看音频参数", Icon = "\xe76b", GroupName = "操作,输入", Description = "查看音频参数")]
        public RelayCommand InputViewCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowView(this.InputMediaInfo.AudioAnalysis, null, "查看音频参数", x => x.UseEnumerator = true);
        });

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            this.OutputMediaInfo.AudioAnalysis.CreateArguments(options);
        }

        protected override string CreateOutputPath(string groupPath)
        {
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(this.FilePath) + this.OutputMediaInfo.AudioAnalysis.Codec.Name);
        }
    }

    public class AudioConverterItem : AudioConverterItemBase
    {
        public AudioConverterItem(string filePath) : base(filePath)
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

   
    }
}
