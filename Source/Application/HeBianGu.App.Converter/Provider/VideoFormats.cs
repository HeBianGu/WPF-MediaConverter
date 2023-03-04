using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HeBianGu.App.Converter
{
    public class VideoFormats : NotifyPropertyChangedBase
    {
        public VideoFormats()
        {
            //var collection = GetVideoFormats();
            //IEnumerable<IGrouping<ContainerFormat, MediaInfo>> groups = collection.GroupBy(x => x.VedioAnalysis.ContainerFormat);
            //Collection = groups.ToObservable();
        }

        private ObservableCollection<IGrouping<ContainerFormat, MediaInfo>> _collection = new ObservableCollection<IGrouping<ContainerFormat, MediaInfo>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IGrouping<ContainerFormat, MediaInfo>> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        private MediaInfo _selectedItem;
        /// <summary> 说明  </summary>
        public MediaInfo SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }


        IEnumerable<MediaInfo> GetVideoFormats()
        {
            //var codecs = typeof(VideoCodec).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var types = typeof(VideoType).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var sizes = Enum.GetValues(typeof(VideoSize));

            foreach (var t in types)
            {
                var tv = t.GetValue(null) as ContainerFormat;
                ////foreach (var c in codecs)
                ////{
                ////    var cv = c.GetValue(null) as Codec;
                ////    foreach (var s in sizes)
                ////    {
                ////        yield return new VideoFormat() { VideoType = tv, Codec = cv, VideoSize = (VideoSize)s };
                ////    }
                ////}
                ////var m = new ContainerFormatPresenter(tv);
                //foreach (var s in sizes)
                //{
                //    yield return new MediaInfo() { ContainerFormat = tv, VideoSize = (VideoSize)s };
                //}

                foreach (var s in sizes)
                {
                    yield return new MediaInfo();
                }
            }
        }
    }

}
