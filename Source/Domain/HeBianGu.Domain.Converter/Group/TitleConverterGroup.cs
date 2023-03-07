using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "字幕/贴图", Icon = "\xe7e7", GroupName = "视频", Order = 12, Description = "给视频增加字幕和贴图")]
    public class TitleConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
