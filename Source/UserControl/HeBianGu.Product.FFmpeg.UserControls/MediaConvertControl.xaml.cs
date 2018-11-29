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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Product.FFmpeg.UserControls
{
    /// <summary>
    /// MediaConvertControl.xaml 的交互逻辑
    /// </summary>
    public partial class MediaConvertControl : UserControl
    {

        MediaConvertNotifyClass _vm = new MediaConvertNotifyClass();

        public MediaConvertControl()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }
    }



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

                MediaConvertEntityNotifyClass entity = new MediaConvertEntityNotifyClass();

                var model = FFmpegService.Instance.GetMediaEntity(open.FileName);
                MediaEntityViewModel from = new MediaEntityViewModel();

                from.Name= System.IO.Path.GetFileName(open.FileName);
                from.Path = System.IO.Path.GetDirectoryName(open.FileName);
                from.Extend = System.IO.Path.GetExtension(open.FileName).Trim('.').ToUpper();
                //from.MediaCode = model.MediaCode;
                //from.MediaType = model.MediaType;
                //from.Resoluction = model.Resoluction;
                //from.Start = model.Start;
                //from.Bitrate = model.Bitrate;
                //from.Duration = model.Duration;

                from.Size = "00MB";
                from.CopyFromObj(model);
                from.MediaCode = model.MediaCode.Trim().Split(' ')[0];
                entity.From = from;

                MediaEntityViewModel to = new MediaEntityViewModel();
                to.Name = System.IO.Path.GetFileName(open.FileName);
                to.Path = System.IO.Path.GetDirectoryName(open.FileName);
                to.Extend = System.IO.Path.GetExtension(open.FileName).Trim('.').ToUpper();
                to.CopyFromObj(model);
                to.MediaCode = model.MediaCode.Trim().Split(' ')[0];

                to.Size = "00MB";
                entity.To = to;

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
