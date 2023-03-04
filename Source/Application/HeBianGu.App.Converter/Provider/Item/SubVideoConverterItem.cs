using FFMpegCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using FFMpegCore.Enums;

namespace HeBianGu.App.Converter
{
    public class SubVideoConverterItem : VideoConverterItemBase
    {
        public SubVideoConverterItem(string filePath) : base(filePath)
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
            if (this.UseCopyChannel)
            {
                options.CopyChannel(Channel.All)
                    .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
                    .Seek(this.OutputMediaInfo.VedioAnalysis.StartTime)
                    .EndSeek(this.OutputMediaInfo.VedioAnalysis.EndTime)
                    .WithFastStart();
            }
            else
            {
                base.CreateArguments(options);
            }
        }

        //protected override FFMpegArgumentProcessor CreateProcessor()
        //{
        //    return FFMpegArguments.FromFileInput(this.FilePath, true,
        //        options =>
        //        options.Seek(this.Setter.StartTime).EndSeek(this.Setter.EndTime))
        //        .OutputToFile(this.OutputPath, true, options => options.CopyChannel());
        //}

        //protected override object CreateSetter()
        //{
        //    return this.Setter;
        //}

        //public override void CreateVideoFormat(string filePath)
        //{
        //    base.CreateVideoFormat(filePath);
        //    this.Setter.StartTime = this.InVideoFormat.Model.Format.StartTime;
        //    this.Setter.EndTime = this.InVideoFormat.Model.Format.Duration;
        //}

        //public SubTimeSpanSetter Setter { get; } = new SubTimeSpanSetter();

        //public class SubTimeSpanSetter : NotifyPropertyChangedBase
        //{
        //    private TimeSpan _startTime;
        //    [TypeConverter(typeof(TimeSpanConverter))]
        //    public TimeSpan StartTime
        //    {
        //        get { return _startTime; }
        //        set
        //        {
        //            _startTime = value;
        //            RaisePropertyChanged();
        //        }
        //    }

        //    private TimeSpan _endTime;
        //    [TypeConverter(typeof(TimeSpanConverter))]
        //    public TimeSpan EndTime
        //    {
        //        get { return _endTime; }
        //        set
        //        {
        //            _endTime = value;
        //            RaisePropertyChanged();
        //        }
        //    }
        //}
    }
}
