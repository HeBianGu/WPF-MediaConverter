using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频爬取", Icon = "\xe751", GroupName = "Processor", Order = 6, Description = "处理器相关数据监控")]
    public class CrawlerConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
