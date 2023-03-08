using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System;
using System.IO;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "屏幕录像", Icon = "\xe781", GroupName = "视频", Order = 8, Description = "屏幕录制功能")]
    public class ScreenConverterGroup : GroupBase
    {
        private ObservableSource<ConverterItemBase> _collection = new ObservableSource<ConverterItemBase>();
        /// <summary> 说明  </summary>
        public ObservableSource<ConverterItemBase> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        [Displayer(Name = "添加录制", Icon = "\xe6e1", GroupName = "工具栏", Description = "开始录制")]
        public RelayCommand StartCommand => new RelayCommand(async (s, e) =>
        {
            string filePath = Path.Combine(this.OutPath, DateTime.Now.ToString("yyyyMMddHHmmssff") + VideoType.Mp4.Extension);
            var item = new ScreenConverterItem(filePath);
            this.Collection.Add(item);
            //await item.StartAsync(s,e);
        });

        //[Displayer(Name = "停止录制", Icon = "\xe6e1", GroupName = "工具栏", Description = "开始录制")]
        //public RelayCommand StopCommand => new RelayCommand(async (s, e) =>
        //{
            
        //});
    }
}
