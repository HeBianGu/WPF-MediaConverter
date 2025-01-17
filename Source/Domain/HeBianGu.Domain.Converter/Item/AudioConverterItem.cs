using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Helpers;
using HeBianGu.Base.WpfBase;
using System;
using System.IO;

namespace HeBianGu.Domain.Converter
{
    public class AudioConverterItemBase : ProcessorConverterItemBase
    {
        public AudioConverterItemBase(string filePath, Action<ConverterItemBase> builder) : base(filePath, builder)
        {

        }

        protected override void RefreshAnalysis()
        {
            this.InputAnalysis = this.InputMediaInfo.AudioAnalysis;
            this.OutputAnalysis = this.OutputMediaInfo.AudioAnalysis;
        }

        [Displayer(Name = "设置输出参数", Icon = Icons.Set, GroupName = "操作,输出", Description = "设置输出参数")]
        public RelayCommand OutPutSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(OutputMediaInfo.AudioAnalysis, null, "设置输出参数", x => x.UseGroupNames = "输出,输出参数");

            //(视频码率 + 音频码率) * 时长 / 8 = 文件大小
            OutputMediaInfo.Size = (long)((OutputMediaInfo.AudioAnalysis.BitRate + OutputMediaInfo.AudioAnalysis.BitRate) * OutputMediaInfo.Model.Duration.TotalSeconds / 8);
        }, (s, e) => this.IsBuzy == false);

        [Displayer(Name = "查看音频参数", Icon = "\xe76b", GroupName = "操作,输入", Description = "查看音频参数")]
        public RelayCommand InputViewCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowView(InputMediaInfo.AudioAnalysis, null, "查看音频参数", x => x.UseEnumerator = true);
        }, (s, e) => this.IsBuzy == false);

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            OutputMediaInfo.AudioAnalysis.CreateArguments(options);
        }

        protected override string CreateOutputPath(string groupPath)
        {
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(FilePath) + OutputMediaInfo.AudioAnalysis.Codec.Name);
        }
    }

    public class AudioConverterItem : AudioConverterItemBase
    {
        public AudioConverterItem(string filePath, Action<ConverterItemBase> builder = null) : base(filePath, builder)
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
