namespace HeBianGu.App.Converter
{
    public class SubVideoConverterItem : ProcessorConverterItemBase
    {
        public SubVideoConverterItem(string filePath) : base(filePath)
        {

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
