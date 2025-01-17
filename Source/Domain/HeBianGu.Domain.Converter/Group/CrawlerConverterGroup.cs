using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "视频爬取", Icon = "\xe754", GroupName = "视频", Order = 9, Description = "爬取网络m3u8,rtsp,rtmp,mp4等网络数据")]
    public class CrawlerConverterGroup : GroupBase
    {
        UriInfo _info = new UriInfo();
        protected override async Task CreateConverterItemAsync()
        {
            var r = await MessageProxy.PropertyGrid.ShowEdit(_info, x =>
            {
                return true;
            }, "添加URI");
            if (!r)
                return;
            var item = new CrawlerConverterItem(_info.Path);
            this.Collection.Add(item);
        }
    }

    /// <summary>
    /// rtsp://192.168.3.205:5555/test
    /// </summary>
    public class UriInfo
    {
        [Display(Name = "Uri")]
        [Required]
        public string Path { get; set; } = "http://hls1.addictradio.net/addictrock_aac_hls/playlist.m3u8";
    }
}
