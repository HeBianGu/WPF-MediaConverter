using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "自定义命令行", Icon = IconAll.Cmd, GroupName = "视频,音频", Order = 13, Description = "自定义命令行处理视频音频数据")]
    public class CmdConverterGroup : ConverterGroupBase
    {
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new AudioConverterItem(filePath);
        }
    }
}
