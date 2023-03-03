using FFMpegCore;
using System.IO;
using System.Linq;

namespace HeBianGu.App.Converter
{
    public class GifConverterItem : ProcessorConverterItemBase
    {
        public GifConverterItem(string filePath) : base(filePath)
        {

        }

        public override MediaInfo CreateOutputMediaInfo(IMediaAnalysis mediaInfo)
        {
            var result = new MediaInfo(mediaInfo, this.FilePath);
            result.Size = new FileInfo(this.FilePath).Length;
            result.Codecs = FFMpeg.GetCodecs().Where(x => x.Name == "gif").ToList().AsReadOnly();
            result.ContainerFormats = FFMpeg.GetContainerFormats().Where(x => x.Name == "gif").ToList().AsReadOnly();
            result.PixelFormats = FFMpeg.GetPixelFormats();
            result.ContainerFormat = OutputMediaInfo.ContainerFormats?.FirstOrDefault();
            result.Codec = OutputMediaInfo.Codecs?.FirstOrDefault();

            result.StartTime = mediaInfo.Format.StartTime;
            result.EndTime = mediaInfo.Format.Duration;
            return result;
        }

        //protected override object CreateSetter()
        //{
        //    return Setter;
        //}

        //public SubTimeSpanSetter Setter { get; } = new SubTimeSpanSetter();

        //public class SubTimeSpanSetter : NotifyPropertyChangedBase
        //{
        //    private TimeSpan _startTime;
        //    [Display(Name = "起始时间")]
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
        //    [ReadOnly(true)]
        //    [Display(Name = "视频时长")]
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

        //    private long _span;
        //    [Display(Name = "时长(s)")]
        //    [Range(1, 60)]
        //    //[TypeConverter(typeof(TimeSpanConverter))]
        //    public long Span
        //    {
        //        get { return _span; }
        //        set
        //        {
        //            _span = value;
        //            RaisePropertyChanged();
        //        }
        //    }
        //}
    }
}
