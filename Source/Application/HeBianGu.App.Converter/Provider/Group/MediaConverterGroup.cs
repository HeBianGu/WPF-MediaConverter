using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频转换", Icon = "\xe751", GroupName = "Processor", Order = 0, Description = "处理器相关数据监控")]
    public class MediaConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new VideoConverterItem(filePath);
        }
    }
}
