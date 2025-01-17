using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;
using FFMpegCore.Extensions.System.Drawing.Common;
using HeBianGu.Base.WpfBase;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Domain.Converter
{
    //  Do ：https://blog.csdn.net/hhy321/article/details/121205424 命令介绍
    public class ScreenConverterItem : CrawlerConverterItemBase
    {
        public ScreenConverterItem(string filePath, Action<ConverterItemBase> builder = null) : base(filePath, builder)
        {
            this.UseOutToolCommadNames = $"{nameof(PlayOutputCommand)},{nameof(ViewArgumentsCommand)},{nameof(OpenCommand)},{nameof(DeleteCommand)},{nameof(DeleteFileCommand)}";

        }
        protected override void CreateArguments(FFMpegArgumentOptions options)
        {

            //base.CreateArguments(options);
            //options.UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading).WithArgument(new DesktopArgument());

            //options.WithCustomArgument("-f dshow -i audio=\"麦克风阵列 (Synaptics Audio)\"");
            //options.WithCustomArgument("-f dshow -i audio=\"virtual-audio-capturer\"");

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

        protected override bool Start(IRelayCommand s, object e)
        {
            var r = base.Start(s, e);
            if (File.Exists(this.OutputPath))
            {
                this.CreateMediaInfo(this.OutputPath);
                TimeSpan timeSpan = this.InputMediaInfo.VedioAnalysis.Model.Duration / 10;
                var bitmap = FFMpegImage.Snapshot(FilePath, null, timeSpan);
                var source = ImageService.BitmapToBitmapImage(bitmap, x => x.DecodePixelWidth = 140);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.ImageSource = source;
                });
            }
            return r;
        }

        protected override async Task<bool> StopAsnyc(IRelayCommand s, object e)
        {
            var r = await base.StopAsnyc(s, e);

            if (File.Exists(this.OutputPath))
            {
                //this.CreateMediaInfo(this.OutputPath);


                var mediaInfo = FFProbe.Analyse(this.OutputPath);
                this.InputMediaInfo = CreateInputMediaInfo(mediaInfo);
                this.OutputMediaInfo = CreateOutputMediaInfo(mediaInfo);
                this.InputAnalysis = this.InputMediaInfo.VedioAnalysis;
                //this.OutputAnalysis = this.InputMediaInfo.VedioAnalysis;

                TimeSpan timeSpan = this.InputMediaInfo.VedioAnalysis.Model.Duration / 10;
                var bitmap = FFMpegImage.Snapshot(FilePath, null, timeSpan);
                var source = ImageService.BitmapToBitmapImage(bitmap, x => x.DecodePixelWidth = 140);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.ImageSource = source;
                });
            }
            return r;
        }

    }
}
