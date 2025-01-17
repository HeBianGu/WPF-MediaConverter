using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System;

namespace HeBianGu.Domain.Converter
{
    [Vip(3)]
    [Displayer(Name = "背景音乐", Icon = "\xe6ba", GroupName = "视频,音频", Order = 10, Description = "添加背景音乐")]
    public class BackgroundConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
