using FFMpegCore;
using System;
using System.Diagnostics;

namespace HeBianGu.Domain.Converter
{
    public class CrawlerConverterItemBase : VideoConverterItemBase
    {
        public CrawlerConverterItemBase(string filePath, Action<ConverterItemBase> builder = null) : base(filePath, builder)
        {

        }
         
        public override void CreateMediaInfo(string filePath)
        {

        }

        protected override void CreateImageSource(string filePath)
        {

        }

        protected override void RefreshAnalysis()
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
