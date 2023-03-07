using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "m3u8爬取", Icon = "\xe754", GroupName = "视频", Order = 6, Description = "爬取网络m3u8数据")]
    public class CrawlerConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
