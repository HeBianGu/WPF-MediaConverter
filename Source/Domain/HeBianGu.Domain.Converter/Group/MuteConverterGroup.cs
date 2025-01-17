using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "提取视频", Icon = "\xeabe", GroupName = "视频", Order = 7, Description = "去除视频中的音频")]
    public class MuteConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new MuteConverterItem(filePath);
        }
    }
}
