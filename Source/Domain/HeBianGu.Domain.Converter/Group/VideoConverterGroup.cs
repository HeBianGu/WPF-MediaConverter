using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "视频转换", Icon = "\xe8dc", GroupName = "视频", Order = 0, Description = "支持文件格式，编码方式，清晰度，帧率，像素格式，码率，切割，水印，章节/标题等转换")]
    public class VideoConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new VideoConverterItem(filePath);
        }
    }
}
