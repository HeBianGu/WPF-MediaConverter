using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频转Gif", Icon = "\xe751", GroupName = "Processor", Order = 1, Description = "处理器相关数据监控")]
    public class GifConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new GifConverterItem(filePath);
        }
    }
}
