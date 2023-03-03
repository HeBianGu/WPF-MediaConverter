using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频分割", Icon = "\xe751", GroupName = "Processor", Order = 2, Description = "处理器相关数据监控")]
    public class SubVideoConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new SubVideoConverterItem(filePath);
        }
    }
}
