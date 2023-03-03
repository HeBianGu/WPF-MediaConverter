using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频截图", Icon = "\xe751", GroupName = "Processor", Order = 6, Description = "处理器相关数据监控")]
    public class ImageConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new SnapshotConverterItem(filePath);
        }
    }
}
