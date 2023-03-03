using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频水印", Icon = "\xe751", GroupName = "Processor", Order = 11, Description = "处理器相关数据监控")]
    public class WatermarkConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
