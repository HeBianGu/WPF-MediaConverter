using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "提取音频", Icon = "\xe751", GroupName = "Processor", Order = 4, Description = "处理器相关数据监控")]
    public class ExtractAudioConverterGroup : ConverterGroupBase
    {
        private bool _useCopyChannel = true;
        [DefaultValue(true)]
        [Display(Name = "复制通道", GroupName = "配置", Description = "启用后不转码，运行速度快")]
        public bool UseCopyChannel
        {
            get { return _useCopyChannel; }
            set
            {
                _useCopyChannel = value;
                RaisePropertyChanged();
            }
        }
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new ExtractAudioConverterItem(filePath) { UseCopyChannel = this.UseCopyChannel };
        }
    }
}
