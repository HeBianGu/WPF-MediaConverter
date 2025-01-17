using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Domain.Converter
{
    /// <summary>
    /// ffmpeg -re -i localFile.mp4 -c copy -f flv rtmp://server/live/streamName
    /// </summary>
    [Vip(5)]
    [Displayer(Name = "直播推流", Icon = "\xe7e7", GroupName = "视频", Order = 12, Description = "推送桌面，视频设备，音频设备，本地视频等到rtmp流")]
    public class RtmpConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }

}
