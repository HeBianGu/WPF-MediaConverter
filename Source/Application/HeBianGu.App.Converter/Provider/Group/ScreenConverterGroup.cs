using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "屏幕录像", Icon = "\xe751", GroupName = "Processor", Order = 8, Description = "处理器相关数据监控")]
    public class ScreenConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
