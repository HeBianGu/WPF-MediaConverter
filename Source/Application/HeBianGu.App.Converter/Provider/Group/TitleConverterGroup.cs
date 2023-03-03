using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "字幕/贴图", Icon = "\xe751", GroupName = "Processor", Order = 12, Description = "处理器相关数据监控")]
    public class TitleConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
