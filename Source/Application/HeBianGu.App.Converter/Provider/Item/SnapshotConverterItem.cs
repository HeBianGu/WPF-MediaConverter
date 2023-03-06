using FFMpegCore;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
using FFMpegImage = FFMpegCore.Extensions.System.Drawing.Common.FFMpegImage;

namespace HeBianGu.App.Converter
{
    public class SnapshotConverterItem : ConverterItemBase
    {
        public SnapshotConverterItem(string filePath) : base(filePath)
        {
            this.InputAnalysis = this.InputMediaInfo.VedioAnalysis;
            this.OutputAnalysis = this.OutputMediaInfo.VedioAnalysis;
        }


        protected override async Task<bool> StopAsnyc(IRelayCommand s, object e)
        {
            return await MessageProxy.Presenter.Show(this, null, "选择格式");
        }

        public override MediaInfo CreateOutputMediaInfo(IMediaAnalysis mediaInfo)
        {
            return new SnapshotMediaInfo(mediaInfo, this.FilePath);
        }

        protected override bool Start(IRelayCommand s, object e)
        {
            SnapshotVideoAnalysis snapshotVideoAnalysis = this.OutputAnalysis as SnapshotVideoAnalysis;

            Application.Current.Dispatcher.Invoke(() =>
            {
                snapshotVideoAnalysis.Collection.Clear();
            });

            var span = InputMediaInfo.Model.Duration.Ticks / snapshotVideoAnalysis.Count;
            for (long i = 0; i < InputMediaInfo.Model.Duration.Ticks; i = i + span)
            {
                var bitmap = FFMpegImage.Snapshot(FilePath, null, TimeSpan.FromTicks(i));
                var source = ImageService.BitmapToBitmapImage(bitmap);
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                          {
                              snapshotVideoAnalysis.Collection.Add(source);
                              Value = i * 1.0 / InputMediaInfo.Model.Duration.Ticks * 1.0 * 100.0;
                              if (i == InputMediaInfo.Model.Duration.Ticks)
                                  Message = "完成";
                              else
                                  Message = Math.Round(Value, 2) + "/100";
                          }));
            }
            return true;
        }

        [Displayer(Name = "设置输出参数", Icon = Icons.Set, GroupName = "操作,输出", Description = "设置输出参数")]
        public RelayCommand OutPutSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(this.OutputMediaInfo.VedioAnalysis, null, "设置输出参数");
        });
    }

    public class SnapshotMediaInfo : MediaInfo
    {
        public SnapshotMediaInfo(IMediaAnalysis model, string filePath) : base(model, filePath)
        {

        }

        protected override VideoAnalysis CreateVedioAnalysis(string filePath)
        {
            return new SnapshotVideoAnalysis(this.Model, filePath);
        }
    }

    public class SnapshotVideoAnalysis : VideoAnalysis
    {
        public SnapshotVideoAnalysis(IMediaAnalysis model, string filePath) : base(model, filePath)
        {
            this.StartTime = model.Format.StartTime;
            this.EndTime = model.Format.Duration;
        }


        private int _count = 10;
        [DefaultValue(10)]
        [Display(Name = "截图数量", GroupName = "视频", Description = "截图数量")]
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<ImageSource> _collection = new ObservableCollection<ImageSource>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ImageSource> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }
    }
}
