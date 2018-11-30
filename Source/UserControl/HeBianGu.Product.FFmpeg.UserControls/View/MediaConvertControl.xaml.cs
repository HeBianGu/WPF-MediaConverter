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
}
