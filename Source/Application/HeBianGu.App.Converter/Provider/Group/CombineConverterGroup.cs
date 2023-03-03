using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频合并", Icon = "\xe751", GroupName = "Processor", Order = 3, Description = "处理器相关数据监控")]
    public class CombineConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
