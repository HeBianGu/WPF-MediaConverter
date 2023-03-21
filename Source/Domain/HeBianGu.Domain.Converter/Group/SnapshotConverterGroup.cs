using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "视频截图", Icon = "\xe887", GroupName = "视频", Order = 6, Description = "从视频截取一定数量的图片")]
    public class SnapshotConverterGroup : ConverterGroupBase
    {
        private int _count = 12;
        [DefaultValue(12)]
        [Display(Name = "截图数量", GroupName = "配置", Description = "截图数量")]
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged();
            }
        }

        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            var result = new SnapshotConverterItem(filePath,x=>
            {
                if (x.OutputMediaInfo.VedioAnalysis is SnapshotVideoAnalysis analysis)
                    analysis.Count = Count;
            });
           
            return result;
        }
    }
}
