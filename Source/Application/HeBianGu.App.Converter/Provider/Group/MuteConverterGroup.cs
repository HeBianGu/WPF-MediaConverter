using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "去除音频", Icon = "\xe751", GroupName = "Processor", Order = 7, Description = "处理器相关数据监控")]
    public class MuteConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new MuteConverterItem(filePath);
        }
    }
}
