using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "背景音乐", Icon = "\xe751", GroupName = "Processor", Order = 10, Description = "处理器相关数据监控")]
    public class BackgroundConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
