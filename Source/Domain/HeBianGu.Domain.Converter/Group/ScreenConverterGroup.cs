using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "屏幕录像", Icon = "\xe781", GroupName = "视频", Order = 8, Description = "屏幕录制功能")]
    public class ScreenConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
