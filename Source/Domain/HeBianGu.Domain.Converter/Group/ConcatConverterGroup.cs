using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "视频合并", Icon = "\xe67c", GroupName = "视频", Order = 3, Description = "将多个视频合并")]
    public class ConcatConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }

    //[Displayer(Name = "视频合并", Icon = "\xe751", GroupName = "Processor", Order = 3, Description = "处理器相关数据监控")]
    //public class DemuxConcatConverterGroup : ConverterGroupBase
    //{
    //    protected override ConverterItemBase CreateConverterItem(string filePath)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}
