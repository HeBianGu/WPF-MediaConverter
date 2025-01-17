using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "屏幕录像", Icon = "\xe781", GroupName = "视频", Order = 8, Description = "屏幕录制功能")]
    public class ScreenConverterGroup : GroupBase
    {
        public ScreenConverterGroup()
        {
            this.SettingVip = 6;
        }
        private bool _showRegion;
        [ReadOnly(true)]
        [Vip(6)]
        [Display(Name = "显示边界", GroupName = "配置", Description = "录屏时是否在屏幕上显示边界框。用于检查录屏区域，防止区域错误。 1显示，0不显示。-show_region 1")]
        public bool ShowRegion
        {
            get { return _showRegion; }
            set
            {
                _showRegion = value;
                RaisePropertyChanged();
            }
        }


        private bool _drawMouse;
        [ReadOnly(true)]
        [Vip(6)]
        [Display(Name = "绘制鼠标", GroupName = "配置", Description = "是否包含鼠标。0=不包含，1=包含。默认值1，视频含有鼠标 -draw_mouse 1")]
        public bool DrawMouse
        {
            get { return _drawMouse; }
            set
            {
                _drawMouse = value;
                RaisePropertyChanged();
            }
        }


        private bool _useDeskTop;
        [ReadOnly(true)]
        [Vip(6)]
        [Display(Name = "捕捉桌面", GroupName = "工具配置栏", Description = "捕捉整个桌面或者桌面的某个区域 -i desktop")]
        public bool UseDeskTop
        {
            get { return _useDeskTop; }
            set
            {
                _useDeskTop = value;
                RaisePropertyChanged();
            }
        }


        private string _title;
        [ReadOnly(true)]
        [Vip(6)]
        [Display(Name = "捕捉窗口", GroupName = "配置", Description = "捕捉指定窗口，由窗口标题栏指定窗口  -i title=window_title")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }


        private Rect _videoRect;
        /// <summary>
        /// ntsc    720x480
        //        pal        720x576
        //        qntsc    352x240
        //        qpal    352x288
        //        sntsc    640x480
        //        spal    768x576
        //        film    352x240
        //ntsc-film    352x240
        //sqcif    128x96
        //qcif    176x144
        //cif        352x288
        //4cif    704x576
        //16cif    1408x1152
        //qqvga    160x120
        //qvga    320x240
        //vga        640x480
        //svga    800x600
        //xga        1024x768
        //uxga    1600x1200
        //qxga    2048x1536
        //sxga    1280x1024
        //qsxga    2560x2048
        //hsxga    5120x4096
        //wvga    852x480
        //wxga    1366x768
        //wsxga    1600x1024
        //wuxga    1920x1200
        //woxga    2560x1600
        //wqsxga    3200x2048
        //wquxga    3840x2400
        //whsxga    6400x4096
        //whuxga    7680x4800
        //cga        320x200
        //ega        640x350
        //hd480    852x480
        //hd720    1280x720
        //hd1080    1920x1080
        //2k        2048x1080
        //2kflat    1998x1080
        //2kscope    2048x858
        //4k        4096x2160
        //4kflat    3996x2160
        //4kscope    4096x1716
        //nhd        640x360
        //hqvga    240x160
        //wqvga    400x240
        //fwqvga    432x240
        //hvga    480x320
        //qhd        960x540
        //2kdci    2048x1080
        //4kdci    4096x2160
        //uhd2160    3840x2160
        //uhd4320    7680x4320
        /// </summary>
        [ReadOnly(true)]
        [Vip(6)]
        [Display(Name = "录制范围", GroupName = "配置", Description = "-video_size cif -offset_x 10 -offset_y 20;-offset_x 10 -offset_y 20 -video_size 640x480")]
        public Rect videoRect
        {
            get { return _videoRect; }
            set
            {
                _videoRect = value;
                RaisePropertyChanged();
            }
        }
        bool _UseMicrophone = false;
        [ReadOnly(true)]
        [Vip(6)]
        [Display(Name = "启用麦克风", GroupName = "配置", Description = "是否启用麦克风")]
        public bool UseMicrophone
        {
            get { return _UseMicrophone; }
            set
            {
                _UseMicrophone = value;
                RaisePropertyChanged();
            }
        }

        private bool _useSystemAudio;
        [ReadOnly(true)]
        [Vip(6)]
        [Display(Name = "启用系统声音", GroupName = "配置", Description = "是否启用系统声音")]
        public bool UseSystemAudio
        {
            get { return _useSystemAudio; }
            set
            {
                _useSystemAudio = value;
                RaisePropertyChanged();
            }
        }


        private bool _useCamera;
        [ReadOnly(true)]
        [Vip(6)]
        [Display(Name = "启用摄像头", GroupName = "配置", Description = "是否启用摄像头")]
        public bool UseCamera
        {
            get { return _useCamera; }
            set
            {
                _useCamera = value;
                RaisePropertyChanged();
            }
        }


        //[Displayer(Name = "添加录制", Icon = "\xe6e1", GroupName = "工具栏", Description = "开始录制")]
        //public RelayCommand StartCommand => new RelayCommand(async (s, e) =>
        //{
        //    string filePath = Path.Combine(this.OutPath, DateTime.Now.ToString("yyyyMMddHHmmssff") + VideoType.Mp4.Extension);
        //    var item = new ScreenConverterItem(filePath);
        //    this.Collection.Add(item);
        //    //await item.StartAsync(s,e);
        //});

        protected override async Task CreateConverterItemAsync()
        {
            string filePath = Path.Combine(this.OutPath, DateTime.Now.ToString("yyyyMMddHHmmssff") + VideoType.Mp4.Extension);
            var item = new ScreenConverterItem(filePath);
            this.Collection.Add(item);
            await Task.CompletedTask;
        }
    }
}
