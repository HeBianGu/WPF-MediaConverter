using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频分割", Icon = "\xe751", GroupName = "Processor", Order = 2, Description = "处理器相关数据监控")]
    public class SubVideoConverterGroup : ConverterGroupBase
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
            return new SubVideoConverterItem(filePath) { UseCopyChannel = this.UseCopyChannel };
        }
    }
}
