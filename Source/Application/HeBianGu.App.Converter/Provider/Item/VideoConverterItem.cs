using HeBianGu.Base.WpfBase;
using System.Threading.Tasks;

namespace HeBianGu.App.Converter
{
    public class VideoConverterItem : ProcessorConverterItemBase
    {
        public VideoConverterItem(string filePath) : base(filePath)
        {

        }

        private VideoFormats _videoFormats = new VideoFormats();
        /// <summary> 说明  </summary>
        public VideoFormats VideoFormats
        {
            get { return _videoFormats; }
            set
            {
                _videoFormats = value;
                RaisePropertyChanged();
            }
        }

        protected override async Task<bool> Set()
        {
            return await MessageProxy.Presenter.Show(VideoFormats, null, "选择格式");
        }

        //protected override FFMpegArgumentProcessor CreateProcessor()
        //{
        //    return FFMpegArguments.FromFileInput(this.FilePath).OutputToFile(this.OutputPath, false,
        //                         options => options
        //                         .WithVideoCodec(this.OutVideoFormat.Codec)
        //                         .WithFramerate(this.OutVideoFormat.FrameRate)
        //                         .ForcePixelFormat(this.OutVideoFormat.PixelFormat)
        //                         .WithConstantRateFactor(this.OutVideoFormat.ConstantRateFactor)
        //                         .WithAudioCodec(AudioCodec.Aac)
        //                         .WithVideoBitrate((int)this.OutVideoFormat.BitRate)
        //                         .WithSpeedPreset(this.OutVideoFormat.Speed)
        //                         //.Resize(this.OutVideoFormat.Width, this.OutVideoFormat.Height)
        //                         .WithVariableBitrate((int)this.OutVideoFormat.VariableBitrate)
        //                         .WithVideoFilters(filterOptions => filterOptions
        //                         .Scale(this.OutVideoFormat.VideoSize))
        //                         .WithFastStart());
        //}


    }
}
