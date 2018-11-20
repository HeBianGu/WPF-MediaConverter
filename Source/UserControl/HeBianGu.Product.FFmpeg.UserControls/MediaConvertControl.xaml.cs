using HeBianGu.Base.WpfBase;
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

                entity.From_FilePath = @"F:\录屏\Rec 0001.mp4";
                entity.From_FileName = @"Rec 0001.mp4";
                entity.From_Resolution = @"1244*1680";
                entity.From_Size = @"8.22MB";
                entity.From_Time = @"F:00:01:32";
                entity.From_Type = @"mp4";

                entity.From_FileName = @"Rec 0001.mp4";
                entity.From_Resolution = @"1244*1680";
                entity.From_Size = @"8.22MB";
                entity.From_Time = @"F:00:01:32";
                entity.From_Type = @"mp4";

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

                //var file= File.Create(open.FileName);
                entity.From_FilePath = open.FileName;

                var model = FFmpegService.Instance.GetMediaEntity(open.FileName);

                entity.From_FileName = System.IO.Path.GetFileName(open.FileName);
                entity.From_Type = System.IO.Path.GetExtension(open.FileName);
                entity.From_Size = "00MB";
                entity.From_Time = model.Duration;
                entity.From_Resolution = model.Resoluction;


                entity.To_FileName = System.IO.Path.GetFileNameWithoutExtension(open.FileName) + ".wmv";
                entity.To_FilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(open.FileName), entity.To_FileName);


                this.Collection.Add(entity);

            }
            //  Do：取消
            else if (command == "config_btn_sumit")
            {


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


    partial class MediaConvertEntityNotifyClass
    {

        private string _from_filePath;
        /// <summary> 说明  </summary>
        public string From_FilePath
        {
            get { return _from_filePath; }
            set
            {
                _from_filePath = value;
                RaisePropertyChanged("From_FilePath");
            }
        }

        private string _from_FileName;
        /// <summary> 说明  </summary>
        public string From_FileName
        {
            get { return _from_FileName; }
            set
            {
                _from_FileName = value;
                RaisePropertyChanged("From_FileName");
            }
        }

        private string _from_Type;
        /// <summary> 说明  </summary>
        public string From_Type
        {
            get { return _from_Type; }
            set
            {
                _from_Type = value;
                RaisePropertyChanged("From_Type");
            }
        }

        private string _from_size;
        /// <summary> 说明  </summary>
        public string From_Size
        {
            get { return _from_size; }
            set
            {
                _from_size = value;
                RaisePropertyChanged("From_Size");
            }
        }

        private string _from_Resolution;
        /// <summary> 说明  </summary>
        public string From_Resolution
        {
            get { return _from_Resolution; }
            set
            {
                _from_Resolution = value;
                RaisePropertyChanged("From_Resolution");
            }
        }

        private string _from_Time;
        /// <summary> 说明  </summary>
        public string From_Time
        {
            get { return _from_Time; }
            set
            {
                _from_Time = value;
                RaisePropertyChanged("From_Time");
            }
        }

        private string _to_FileName;
        /// <summary> 说明  </summary>
        public string To_FileName
        {
            get { return _to_FileName; }
            set
            {
                _to_FileName = value;
                RaisePropertyChanged("To_FileName");
            }
        }

        private string _to_sixe;
        /// <summary> 说明  </summary>
        public string To_Size
        {
            get { return _to_sixe; }
            set
            {
                _to_sixe = value;
                RaisePropertyChanged("To_Size");
            }
        }

        private string _to_Type;
        /// <summary> 说明  </summary>
        public string To_Type
        {
            get { return _to_Type; }
            set
            {
                _to_Type = value;
                RaisePropertyChanged("To_Type");
            }
        }

        private string _to_Resolution;
        /// <summary> 说明  </summary>
        public string To_Resolution
        {
            get { return _to_Resolution; }
            set
            {
                _to_Resolution = value;
                RaisePropertyChanged("To_Resolution");
            }
        }


        private string _to_time;
        /// <summary> 说明  </summary>
        public string To_Time
        {
            get { return _to_time; }
            set
            {
                _to_time = value;
                RaisePropertyChanged("To_Time");
            }
        }



        private bool _isBuzy;
        /// <summary> 说明  </summary>
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged("IsBuzy");
            }
        }



        private string _to_FilePath;
        /// <summary> 说明  </summary>
        public string To_FilePath
        {
            get { return _to_FilePath; }
            set
            {
                _to_FilePath = value;
                RaisePropertyChanged("To_FilePath");
            }
        }



        private double _progressValue;
        /// <summary> 说明  </summary>
        public double ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                RaisePropertyChanged("ProgressValue");
            }
        }



        public void RelayMethod(object obj)
        {
            string command = obj.ToString();


            Debug.WriteLine(command);


            //  Do：应用
            if (command == "btn_convert")
            {

                Debug.WriteLine("btn_convert");

                TimeSpan total = TimeSpan.Parse(this.From_Time);

                Action<string> reciveAction = l =>
                  {
                      if (string.IsNullOrEmpty(l)) return;

                      TimeSpan time = TimeSpan.Parse(l);

                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          this.ProgressValue = (time.TotalSeconds / total.TotalSeconds)*100 ;
                      });

                  };

                Action<int> existAction = l =>
                  {
                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          MessageBox.Show("运行结束");

                          this.IsBuzy = false;
                      });

                  };

                FFmpegService.Instance.Mp4ToWmv(this.From_FilePath, this.To_FilePath, reciveAction, existAction);

                this.IsBuzy = true;

            }
            //  Do：取消
            else if (command == "config_btn_sumit")
            {


            }
        }
    }

    partial class MediaConvertEntityNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public MediaConvertEntityNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);

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
