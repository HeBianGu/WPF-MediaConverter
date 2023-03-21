using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;
using System;
using System.Diagnostics;
using System.IO;

namespace HeBianGu.Domain.Converter
{
    //  Do ：https://blog.csdn.net/hhy321/article/details/121205424 命令介绍
    public class ScreenConverterItem : CrawlerConverterItemBase
    {
        public ScreenConverterItem(string filePath, Action<ConverterItemBase> builder = null) : base(filePath, builder)
        {

        }
        protected override void CreateArguments(FFMpegArgumentOptions options)
        {

            //base.CreateArguments(options);
            //options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).WithArgument(new DesktopArgument());

            //options.WithCustomArgument("-f dshow -i audio=\"麦克风阵列 (Synaptics Audio)\"");
            options.WithCustomArgument("-f dshow -i audio=\"virtual-audio-capturer\"");

        }

        protected override void RefreshAnalysis()
        {
            //this.InputAnalysis = this.InputMediaInfo.VedioAnalysis;
            //this.OutputAnalysis = this.OutputMediaInfo.VedioAnalysis;
        }

        protected override FFMpegArgumentProcessor CreateProcessor()
        {
            //AddDeviceInput
            return FFMpegArguments.FromDeviceInput("desktop", x =>
            {
                x.WithArgument(new GdigrabArgument());
            }).OutputToFile(this.FilePath, false, CreateArguments);

        }

        protected override string CreateOutputPath(string groupPath)
        {
            return this.FilePath;
        }

    }
}
