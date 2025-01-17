using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "视频分割", Icon = "\xe7d9", GroupName = "视频", Order = 2, Description = "截取视频中的一段视频")]
    public class SubVideoConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new SubVideoConverterItem(filePath);
        }
    }
}
