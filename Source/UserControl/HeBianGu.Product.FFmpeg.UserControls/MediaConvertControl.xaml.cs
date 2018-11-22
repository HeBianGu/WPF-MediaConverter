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
                from.Extend = System.IO.Path.GetExtension(open.FileName);
                //from.MediaCode = model.MediaCode;
                //from.MediaType = model.MediaType;
                //from.Resoluction = model.Resoluction;
                //from.Start = model.Start;
                //from.Bitrate = model.Bitrate;
                //from.Duration = model.Duration;

                from.Size = "00MB";
                from.CopyFromObj(model);
                entity.From = from;

                MediaEntityViewModel to = new MediaEntityViewModel();
                to.Name = System.IO.Path.GetFileName(open.FileName);
                to.Path = System.IO.Path.GetDirectoryName(open.FileName);
                to.Extend = System.IO.Path.GetExtension(open.FileName);
                to.CopyFromObj(model);
                
                to.Size = "00MB";
                entity.To = to;

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
        private MediaEntityViewModel _from;
        /// <summary> 说明  </summary>
        public MediaEntityViewModel From
        {
            get { return _from; }
            set
            {
                _from = value;
                RaisePropertyChanged("From");
            }
        }


        private MediaEntityViewModel _to;
        /// <summary> 说明  </summary>
        public MediaEntityViewModel To
        {
            get { return _to; }
            set
            {
                _to = value;
                RaisePropertyChanged("To");
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

        private string _cmdState = "开始转换";
        /// <summary> 说明  </summary>
        public string CmdState
        {
            get
            {
                _cmdState = this.GetState();

                return _cmdState;
            }
            set
            {
                _cmdState = value;

                RaisePropertyChanged("CmdState");
            }
        }

        private string _cmdParams;
        /// <summary> 说明  </summary>
        public string CmdParams
        {
            get { return _cmdParams; }
            set
            {
                _cmdParams = value;
                RaisePropertyChanged("CmdParams");
            }
        }


        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            Debug.WriteLine(command);

            //  Do：应用
            if (command == "开始转换")
            {

                Debug.WriteLine("btn_convert");

                TimeSpan total = TimeSpan.Parse(this.To.Duration);

                Action<string> reciveAction = l =>
                  {
                      if (string.IsNullOrEmpty(l)) return;

                      TimeSpan time = TimeSpan.Parse(l);

                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          this.ProgressValue = (time.TotalSeconds / total.TotalSeconds) * 100;
                      });

                  };

                Action<int> existAction = l =>
                  {
                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          MessageBox.Show("运行结束");

                          this.IsBuzy = false;

                          this.CmdState = this.GetState();
                      });

                  };

                //FFmpegService.Instance.MediaToWmv(this.From_FilePath, this.To_FilePath, reciveAction, existAction);

                string ccc = string.Format("-i {0} {1} ", this.From.FullPath, this.To.FullPath);

                string sss = ccc + this.CmdParams;

                Log4Servcie.Instance.Info(sss);

                Debug.WriteLine(sss);


                FFmpegService.Instance.ConvertWithParams(sss, reciveAction, existAction);
               

                this.IsBuzy = true;

            }
            //  Do：取消
            else if (command == "取消")
            {


            }
            //  Do：取消
            else if (command == "打开")
            {
                Process.Start(this.To.FullPath);

            }
        }


        public string GetState()
        {

            string result;

            if (this.IsBuzy)
            {
                result = "取消";
            }
            else
            {
                if (File.Exists(this.To.FullPath))
                {
                    result = "打开";
                }
                else
                {
                    result = "开始转换";
                }
            }

            return result;
        }
    }

    partial class MediaConvertEntityNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        FileWatcher watcher;

        public MediaConvertEntityNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
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
