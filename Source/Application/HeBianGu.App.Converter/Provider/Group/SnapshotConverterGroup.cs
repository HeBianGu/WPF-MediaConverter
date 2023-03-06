using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Converter
{
    [Displayer(Name = "视频截图", Icon = "\xe751", GroupName = "Processor", Order = 6, Description = "处理器相关数据监控")]
    public class SnapshotConverterGroup : ConverterGroupBase
    {
        private int _count = 10;
        [DefaultValue(10)]
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
            var result=  new SnapshotConverterItem(filePath);
            if (result.OutputMediaInfo.VedioAnalysis is SnapshotVideoAnalysis analysis)
                analysis.Count = this.Count;
            return result;
        }
    }
}
