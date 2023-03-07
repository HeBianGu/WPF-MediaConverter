using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "视频分割", Icon = "\xe7d9", GroupName = "视频", Order = 2, Description = "截取视频中的一段视频")]
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
            return new SubVideoConverterItem(filePath) { UseCopyChannel = UseCopyChannel };
        }
    }
}
