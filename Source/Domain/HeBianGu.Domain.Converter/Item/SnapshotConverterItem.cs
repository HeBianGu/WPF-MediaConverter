using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using FFMpegImage = FFMpegCore.Extensions.System.Drawing.Common.FFMpegImage;

namespace HeBianGu.Domain.Converter
{
    /// <summary>
    /// ffmpeg -i test.avi -y -f image2 -ss 8 -t 0.001 -s 350x240 test.jpg
    /// </summary>
    public class SnapshotConverterItem : ConverterItemBase
    {
        public SnapshotConverterItem(string filePath, Action<ConverterItemBase> builder) : base(filePath, builder)
        {
            this.UseOutToolCommadNames = $"{nameof(PlayOutputCommand)},{nameof(OpenCommand)},{nameof(DeleteCommand)},{nameof(DeleteFileCommand)},{nameof(OutputTimePanCommand)},{nameof(SaveCommand)}";
        }


        protected override async Task<bool> StopAsnyc(IRelayCommand s, object e)
        {
            return await MessageProxy.Presenter.Show(this, null, "选择格式");
        }

        public override MediaInfo CreateOutputMediaInfo(IMediaAnalysis mediaInfo)
        {
            return new SnapshotMediaInfo(mediaInfo, FilePath);


        }

        public override void CreateMediaInfo(string filePath)
        {
            base.CreateMediaInfo(filePath);
            InputAnalysis = InputMediaInfo.VedioAnalysis;
            OutputAnalysis = OutputMediaInfo.VedioAnalysis;
        }

        protected override bool Start(IRelayCommand s, object e)
        {
            SnapshotVideoAnalysis snapshotVideoAnalysis = OutputAnalysis as SnapshotVideoAnalysis;

            Application.Current.Dispatcher.Invoke(() =>
            {
                snapshotVideoAnalysis.Collection.Clear();
            });

            var timeSpan = this.OutputMediaInfo.VedioAnalysis.EndTime - this.OutputMediaInfo.VedioAnalysis.StartTime;
            var span = timeSpan.Ticks / snapshotVideoAnalysis.Count;
            for (long i = this.OutputMediaInfo.VedioAnalysis.StartTime.Ticks; i < this.OutputMediaInfo.VedioAnalysis.EndTime.Ticks; i = i + span)
            {
                var bitmap = FFMpegImage.Snapshot(this.FilePath, null, TimeSpan.FromTicks(i));
                ImageSource source = ImageService.BitmapToBitmapImage(bitmap);
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                          {
                              snapshotVideoAnalysis.Collection.Add(Tuple.Create(TimeSpan.FromTicks(i), source));
                              Value = i * 1.0 / timeSpan.Ticks * 1.0 * 100.0;
                              if (i == timeSpan.Ticks)
                                  Message = "完成";
                              else
                                  Message = Math.Round(Value, 2) + "/100";
                          }));
            }
            return true;
        }

        [Displayer(Name = "保存截图", Icon = "\xe8cb", GroupName = "操作,输出", Description = "保存截图")]
        public RelayCommand SaveCommand => new RelayCommand(async (s, e) =>
        {
            SnapshotVideoAnalysis snapshotVideoAnalysis = OutputAnalysis as SnapshotVideoAnalysis;
            var collection = snapshotVideoAnalysis.Collection;
            if (!Directory.Exists(this.OutputPath))
                Directory.CreateDirectory(this.OutputPath);
            await MessageProxy.Messager.ShowWaitter(() =>
              {
                  foreach (var item in collection)
                  {
                      var bitmap = ImageService.ImageSourceToBitmap(item.Item2);
                      //var p = System.IO.Path.Combine(this.OutputPath, item.Item1.ToString().Split('.')[0] + FileExtension.Png);
                      var p = System.IO.Path.Combine(this.OutputPath, item.Item1.TimespanToDislay() + FileExtension.Png);
                      bitmap.Save(System.IO.Path.Combine(this.OutputPath, p));
                  }
              });
        });

        protected override string CreateOutputPath(string groupPath)
        {
            return System.IO.Path.Combine(groupPath, System.IO.Path.GetFileNameWithoutExtension(FilePath));
        }
    }

    public class SnapshotMediaInfo : MediaInfo
    {
        public SnapshotMediaInfo(IMediaAnalysis model, string filePath) : base(model, filePath)
        {

        }

        protected override VideoAnalysis CreateVedioAnalysis(string filePath)
        {
            return new SnapshotVideoAnalysis(Model, filePath);
        }
    }

    public class SnapshotVideoAnalysis : VideoAnalysis
    {
        public SnapshotVideoAnalysis(IMediaAnalysis model, string filePath) : base(model, filePath)
        {
            StartTime = model.Format.StartTime;
            EndTime = model.Format.Duration;
        }


        private int _count = 12;
        [DefaultValue(12)]
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


        private ObservableCollection<Tuple<TimeSpan, ImageSource>> _collection = new ObservableCollection<Tuple<TimeSpan, ImageSource>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Tuple<TimeSpan, ImageSource>> Collection
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
