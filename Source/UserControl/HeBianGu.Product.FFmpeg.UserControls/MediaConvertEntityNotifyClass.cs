using HeBianGu.Base.WpfBase;
using HeBianGu.General.Logger;
using HeBianGu.Product.FFmpeg.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Product.FFmpeg.UserControls
{

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

                value.FullPathChanged += () => this.CmdState = this.GetState();

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


        private bool _recover;
        /// <summary> 说明  </summary>
        public bool Recover
        {
            get { return _recover; }
            set
            {
                _recover = value;
                RaisePropertyChanged("Recover");
            }
        }



        private bool _isFollow = true;
        /// <summary> 说明  </summary>
        public bool IsFollow
        {
            get { return _isFollow; }
            set
            {
                _isFollow = value;
                RaisePropertyChanged("IsFollow");
            }
        }


        string ConvertToCommand()
        {
            //ffmpeg [global_options] {[input_file_options] -i input_url} ... {[output_file_options] output_url} 

            string global_options = this.Recover ? " -y " : "";

            if (this.IsFollow)
            {
                string input_file_options = string.Empty;

                string input_url = this.From.FullPath;

                string output_file_options = string.Empty;

                string output_url = this.To.FullPath;


                return string.Format("{0} {1} -i {2} {3} {4}", global_options, input_file_options, input_url, output_file_options, output_url);
            }
            else
            {
                string input_file_options = string.Empty;

                string input_url = this.From.FullPath;

                string output_file_options = this.To.GetFileOptions();

                string output_url = this.To.FullPath;

                return string.Format("{0} {1} -i {2} {3} {4}", global_options, input_file_options, input_url, output_file_options, output_url);
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

                //string ccc = string.Format("-i {0} {1} ", this.From.FullPath, this.To.FullPath);

                string sss = this.ConvertToCommand();

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
