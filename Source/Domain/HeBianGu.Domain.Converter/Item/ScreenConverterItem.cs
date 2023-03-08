using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;
using System;
using System.Diagnostics;
using System.IO;

namespace HeBianGu.Domain.Converter
{
    //  Do ：https://blog.csdn.net/hhy321/article/details/121205424 命令介绍
    public class ScreenConverterItem : VideoConverterItemBase
    {
        public ScreenConverterItem(string filePath) : base(filePath)
        {

        }
        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            
            //base.CreateArguments(options);
            //options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).WithArgument(new DesktopArgument());

            //options.WithCustomArgument("-f dshow -i audio=\"麦克风阵列 (Synaptics Audio)\"");
            options.WithCustomArgument("-f dshow -i audio=\"virtual-audio-capturer\"");
            
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

        public override void CreateMediaInfo(string filePath)
        {
            
        }

        protected override void CreateImageSource(string filePath)
        {

        }

        protected override void StartProcessor(FFMpegArgumentProcessor process)
        {
            Debug.WriteLine(process.Arguments);
            Message = process.Arguments;
            //process.NotifyOnProgress(x =>
            //{
            //    Value = x;
            //    if (x == 100.0)
            //    {
            //        Success = true;
            //        Message = "完成";
            //    }
            //    Duration = DateTime.Now - StartTime;

            //    if (Duration.Ticks / x * (100 - x) > TimeSpan.MaxValue.Ticks)
            //        DownDuration = TimeSpan.MaxValue;
            //    else
            //        DownDuration = Duration / x * (100 - x);
            //}, InputMediaInfo.Model.Duration);

            process.NotifyOnOutput(x =>
            {
                Message = x;
            });

            process.NotifyOnError(x =>
            {
                Message = x;
                Success = false;
                Debug.WriteLine(DateTime.Now + "   " + x);

            });

            process.NotifyOnProgress(x =>
            {
                //this.Value = x;
            });

            process.CancellableThrough(out _cancel, 10000).ProcessSynchronously();
        }

    }
}
