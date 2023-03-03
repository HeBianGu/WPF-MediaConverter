using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "音频转换", Icon = "\xe751", GroupName = "Processor", Order = 9, Description = "处理器相关数据监控")]
    public class AudioConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
