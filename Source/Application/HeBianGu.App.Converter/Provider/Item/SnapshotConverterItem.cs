using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using FFMpegImage = FFMpegCore.Extensions.System.Drawing.Common.FFMpegImage;

namespace HeBianGu.App.Converter
{
    public class SnapshotConverterItem : ConverterItemBase
    {
        public SnapshotConverterItem(string filePath) : base(filePath)
        {

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

        protected override async Task<bool> Set()
        {
            return await MessageProxy.Presenter.Show(this, null, "选择格式");
        }

        protected override async Task<bool> StopAsnyc(IRelayCommand s, object e)
        {
            return await MessageProxy.Presenter.Show(this, null, "选择格式");
        }

        protected override bool Start(IRelayCommand s, object e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Collection.Clear();
            });

            var span = InputMediaInfo.Model.Duration.Ticks / 10;
            for (long i = 0; i < InputMediaInfo.Model.Duration.Ticks; i = i + span)
            {
                var bitmap = FFMpegImage.Snapshot(FilePath, new System.Drawing.Size(400, 400), TimeSpan.FromTicks(i));
                var source = ImageService.BitmapToBitmapImage(bitmap);
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                          {
                              Collection.Add(source);
                              Value = i * 1.0 / InputMediaInfo.Model.Duration.Ticks * 1.0 * 100.0;
                              if (i == InputMediaInfo.Model.Duration.Ticks)
                                  Message = "完成";
                              else
                                  Message = Math.Round(Value, 2) + "/100";
                          }));
            }
            return true;
        }
    }
}
