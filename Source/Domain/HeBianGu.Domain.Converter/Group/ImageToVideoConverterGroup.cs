using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Domain.Converter
{
    [Vip(1)]
    [Displayer(Name = "图片转视频", Icon = "\xe7e7", GroupName = "视频", Order = 12, Description = "将一个JPG 图片 经过h264压缩循环输出为mp4视频")]
    public class ImageToVideoConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }

}
