using FFMpegCore.Extensions.System.Drawing.Common;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Domain.Converter
{

    public class VideoConverterItem : VideoConverterItemBase
    {
        public VideoConverterItem(string filePath, Action<ConverterItemBase> builder = null) : base(filePath, builder)
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


        //protected override async Task<bool> Set()
        //{
        //    return await MessageProxy.Presenter.Show(VideoFormats, null, "选择格式");
        //}

    }
}
