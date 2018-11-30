using System;
using System.Collections.Generic;
using System.Linq;
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
    /// MediaSplitControl.xaml 的交互逻辑
    /// </summary>
    public partial class MediaSplitControl : UserControl
    {
        public MediaSplitControl()
        {
            InitializeComponent();
        }

        public string StartTime
        {
            get { return (string)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.Register("StartTime", typeof(string), typeof(MediaSplitControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 MediaSplitControl control = d as MediaSplitControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));

        public string EndTime
        {
            get { return (string)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.Register("EndTime", typeof(string), typeof(MediaSplitControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 MediaSplitControl control = d as MediaSplitControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));
        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(MediaSplitControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 MediaSplitControl control = d as MediaSplitControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));

        //声明和注册路由事件
        public static readonly RoutedEvent SumitClickRoutedEvent =
            EventManager.RegisterRoutedEvent("SumitClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(MediaSplitControl));
        //CLR事件包装
        public event RoutedEventHandler SumitClick
        {
            add { this.AddHandler(SumitClickRoutedEvent, value); }
            remove { this.RemoveHandler(SumitClickRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSumitClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(SumitClickRoutedEvent, this);
            this.RaiseEvent(args);
        }

        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            this.StartTime = this.txt_start.Text;
            this.EndTime = this.txt_end.Text;

            this.OnSumitClick();

            this.Visibility = Visibility.Collapsed;
            this.media.Stop();
        }
    }
}
