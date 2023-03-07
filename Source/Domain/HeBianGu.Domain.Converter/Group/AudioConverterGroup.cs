using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "音频转换", Icon = "\xe6ba", GroupName = "音频", Order = 9, Description = "支持文件格式，编码方式，音频质量，采样率，码率，切割，水印，章节/标题等转换")]
    public class AudioConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new AudioConverterItem(filePath);
        }
    }
}
