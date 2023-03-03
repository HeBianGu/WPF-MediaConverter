using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "提取音频", Icon = "\xe751", GroupName = "Processor", Order = 4, Description = "处理器相关数据监控")]
    public class ExtractAudioConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new ExtractAudioConverterItem(filePath);
        }
    }
}
