using HeBianGu.Base.WpfBase;
using HeBianGu.General.Logger;
using HeBianGu.Product.FFmpeg.Driver;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.UserControls
{

    partial class MediaConvertNotifyClass
    {
        void Test()
        {
            for (int i = 0; i < 10; i++)
            {
                MediaConvertEntityNotifyClass entity = new MediaConvertEntityNotifyClass();

                //entity.From_FilePath = @"F:\录屏\Rec 0001.mp4";
                //entity.From_FileName = @"Rec 0001.mp4";
                //entity.From_Resolution = @"1244*1680";
                //entity.From_Size = @"8.22MB";
                //entity.From_Time = @"F:00:01:32";
                //entity.From_Type = @"mp4";

                //entity.From_FileName = @"Rec 0001.mp4";
                //entity.From_Resolution = @"1244*1680";
                //entity.From_Size = @"8.22MB";
                //entity.From_Time = @"F:00:01:32";
                //entity.From_Type = @"mp4";

                Collection.Add(entity);

            }
        }

        private ObservableCollection<MediaConvertEntityNotifyClass> _collection = new ObservableCollection<MediaConvertEntityNotifyClass>();
        /// <summary> 说明  </summary>
        public ObservableCollection<MediaConvertEntityNotifyClass> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private MediaConvertEntityNotifyClass _current;
        /// <summary> 说明  </summary>
        public MediaConvertEntityNotifyClass Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged("Current");
            }
        }

        private SplitControlNotifyClass _splitParamater = new SplitControlNotifyClass();
        /// <summary> 说明  </summary>
        public SplitControlNotifyClass SplitParamater
        {
            get { return _splitParamater; }
            set
            {
                _splitParamater = value;
                RaisePropertyChanged("SplitParamater");
            }
        }

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            Debug.WriteLine(command);

            //  Do：应用
            if (command == "AddFile")
            {
                OpenFileDialog open = new OpenFileDialog();

                var result = open.ShowDialog();

                if (!result.HasValue) return;

                if (!result.Value) return;

                MediaConvertEntityNotifyClass entity = this.Create(open.FileName);

                this.Collection.Add(entity);

            }
            //  Do：取消
            else if (command == "btn_delete_current")
            {
                if (this.Current == null) return;

                this.Collection.Remove(this.Current);
            }

            //  Do：取消
            else if (command == "btn_clear_all")
            {
                this.Collection.Clear();
            }

            //  Do：分割请求
            else if (command == "split_SumitClick")
            {

                if (!File.Exists(this.SplitParamater.FilePath))
                {
                    Log4Servcie.Instance.Info("文件不存在");
                    return;
                }

                if (string.IsNullOrEmpty(this.SplitParamater.StartTime)) return;

                if (string.IsNullOrEmpty(this.SplitParamater.EndTime)) return;

                MediaConvertEntityNotifyClass entity = this.Create(this.SplitParamater.FilePath);

                entity.To.Start = this.SplitParamater.StartTime;
                entity.To.Duration = (TimeSpan.Parse(this.SplitParamater.EndTime) - TimeSpan.Parse(this.SplitParamater.StartTime)).ToString().Split('.')[0];

                this.Collection.Add(entity);
            }
        }

        MediaConvertEntityNotifyClass Create(string fileName)
        {
            MediaConvertEntityNotifyClass entity = new MediaConvertEntityNotifyClass();

            var model = FFmpegService.Instance.GetMediaEntity(fileName);
            MediaEntityViewModel from = new MediaEntityViewModel();

            from.Name = System.IO.Path.GetFileName(fileName);
            from.Path = System.IO.Path.GetDirectoryName(fileName);
            from.Extend = System.IO.Path.GetExtension(fileName).Trim('.').ToUpper();

            from.Size = fileName.GetLength();
            from.CopyFromObj(model);
            from.MediaCode = model.MediaCode.Trim().Split(' ')[0];
            entity.From = from;

            MediaEntityViewModel to = new MediaEntityViewModel();
            to.Path = System.IO.Path.GetDirectoryName(fileName);
            to.Extend = System.IO.Path.GetExtension(fileName).Trim('.').ToUpper();
            to.Name = "out-" + System.IO.Path.GetFileName(fileName);
            to.CopyFromObj(model);
            to.MediaCode = model.MediaCode.Trim().Split(' ')[0];

            to.Size = to.FullPath.GetLength();
            entity.To = to;

            return entity;
        }

    }

    partial class MediaConvertNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public MediaConvertNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);

            //this.Test();

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
