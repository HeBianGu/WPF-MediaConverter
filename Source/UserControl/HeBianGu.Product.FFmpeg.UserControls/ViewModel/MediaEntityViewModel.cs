using HeBianGu.Base.WpfBase;
using HeBianGu.Product.FFmpeg.Base.Model;
using HeBianGu.Product.FFmpeg.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.UserControls
{
    partial class MediaEntityViewModel
    {
        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");

                this.RefreshFullPath();
            }
        }

        private string _path;
        /// <summary> 说明  </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged("Path");

                this.RefreshFullPath();
            }
        }

        private string _fullPath;
        /// <summary> 说明  </summary>
        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                _fullPath = value;
                RaisePropertyChanged("FullPath");
            }
        }

        private string _size;
        /// <summary> 说明  </summary>
        public string Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged("Size");
            }
        }

        private string _duration;
        /// <summary> 总时间  </summary>
        [Description(FFmpegParameter.ffmpeg_time)]
        public string Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                RaisePropertyChanged("Duration");
            }
        }

        private string _start;
        /// <summary> 开始时间  </summary>
        [Description(FFmpegParameter.ffmpeg_start)]
        public string Start
        {
            get { return _start; }
            set
            {
                _start = value;
                RaisePropertyChanged("Start");
            }
        }

        private string _bitrate;
        /// <summary> 比特率  </summary>
        [Description(FFmpegParameter.ffmpeg_bitrate)]
        public string Bitrate
        {
            get { return _bitrate; }
            set
            {
                _bitrate = value;
                RaisePropertyChanged("Bitrate");
            }
        }

        private string _rate;
        /// <summary> 比特率  </summary>
        [Description(FFmpegParameter.ffmpeg_rate)]
        public string Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                RaisePropertyChanged("Rate");
            }
        }

        private string _mediaCode;
        /// <summary> 编码格式  </summary>
        public string MediaCode
        {
            get { return _mediaCode; }
            set
            {
                _mediaCode = value;
                RaisePropertyChanged("MediaCode");
            }
        }

        private string _mediaType;
        /// <summary> 视频格式 yuv420p  </summary>
        [Description(FFmpegParameter.ffmpeg_vcodec)]
        public string MediaType
        {
            get { return _mediaType; }
            set
            {
                _mediaType = value;
                RaisePropertyChanged("MediaType");
            }
        }

        private string _extend;
        /// <summary> 说明  </summary>
        public string Extend
        {
            get { return _extend; }
            set
            {
                _extend = value;

                RaisePropertyChanged("Extend");

                this.RefeshName();
            }
        }

        private string _resoluction;
        /// <summary> 分辨率  </summary>
        [Description(FFmpegParameter.ffmpeg_pix_fmt)]
        public string Resoluction
        {
            get { return _resoluction; }
            set
            {
                _resoluction = value;
                RaisePropertyChanged("Resoluction");
            }
        }
        void RefreshFullPath()
        {
            if (string.IsNullOrEmpty(this.Name)) return;
            if (string.IsNullOrEmpty(this.Path)) return;

            this.FullPath = System.IO.Path.Combine(this.Path, this.Name);

            this.FullPathChanged?.Invoke();


        }

        public event Action FullPathChanged;

        void RefeshName()
        {
            this.Name = System.IO.Path.GetFileNameWithoutExtension(this.Name) + "." + this.Extend;
        }

        public string GetFileOptions()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.GetType().GetProperties())
            {
                var attr = item.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attr == null || attr.Count() == 0) continue;

                if (item.GetValue(this) == null) continue;

                if (item.Name == "Start" || item.Name == "Duration") continue;

                DescriptionAttribute desc = attr.First() as DescriptionAttribute;

                string current = string.Format(desc.Description, item.GetValue(this));

                sb.Append(" " + current.Trim());
            }

            return sb.ToString();
        }


        public string GetCutSpace()
        {
            List<PropertyInfo> ps = new List<PropertyInfo>();

            ps.Add(this.GetType().GetProperty("Start"));
            ps.Add(this.GetType().GetProperty("Duration"));

            StringBuilder sb = new StringBuilder();

            foreach (var item in ps)
            {
                var attr = item.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attr == null || attr.Count() == 0) continue;

                if (item.GetValue(this) == null) continue;

                DescriptionAttribute desc = attr.First() as DescriptionAttribute;

                string current = string.Format(desc.Description, item.GetValue(this));

                sb.Append(" " + current.Trim());
            }

            return sb.ToString();

            
        }

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {
                Debug.WriteLine("Sumit");
            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }

    partial class MediaEntityViewModel : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public MediaEntityViewModel()
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
