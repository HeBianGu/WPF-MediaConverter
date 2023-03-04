using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "自定义命令行", Icon = "\xe751", GroupName = "Processor", Order = 13, Description = "处理器相关数据监控")]
    public class CmdConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new AudioConverterItem(filePath);
        }
    }
}
