using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Product.General.MediaPlayer
{
    /// <summary>
    /// MediaPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class MediaPlayer : UserControl
    {
        public MediaPlayer()
        {
            InitializeComponent();

            this.media_media.MediaEnded += Player_MediaEnded;
            this.media_media.MediaOpened += Player_MediaOpened;
            this.media_media.MediaFailed += Player_MediaFailed;
            this.media_media.Loaded += Player_Loaded;
            this.media_media.SourceUpdated += Media_media_SourceUpdated;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Interval = 1000;

        }

        private void Media_media_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            this.FileName = ((Uri)e.Source).AbsolutePath;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.RefreshSlider();
        }

        Timer _timer = new Timer();

        private void Player_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Player_Loaded");
        }

        void InitSlider()
        {
            if (this.media_media.Source == null) return;

            if (this.media_media.NaturalDuration.HasTimeSpan)
            {
                this.media_slider.Maximum = this.media_media.NaturalDuration.TimeSpan.Ticks;

                this.TotalTime = this.media_media.NaturalDuration.TimeSpan.Ticks;
            }
        }

        void RefreshSlider()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.media_slider.Value = this.media_media.Position.Ticks;
            });

        }

        private void Player_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
            Debug.WriteLine("Player_MediaFailed");

        }

        private void Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Player_MediaOpened");

            this.InitSlider();

            this.InitSound();

            this._timer.Start();
        }

        private void Player_MediaEnded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Player_MediaEnded");

            this.Stop();
        }


        private void media_slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (this.media_media == null) return;

            this.media_media.Position = TimeSpan.FromTicks((long)this.media_slider.Value);

            this._timer.Start();
        }

        void InitSound()
        {
            this.slider_sound.Value = this.media_media.Volume;
        }

        private void slider_sound_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            this.media_media.Volume = this.slider_sound.Value;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.media_media.SpeedRatio = this.media_media.SpeedRatio / 2;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.media_media.SpeedRatio = this.media_media.SpeedRatio * 2;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Stop();
        }

        private void media_slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this._timer.Stop();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.toggle_play.IsChecked.Value)
            {
                this.Pause();
            }
            else
            {

                this.Play();
            }
        }

        private void CommandBinding_Executed_Play(object sender, ExecutedRoutedEventArgs e)
        {
            Debug.WriteLine("CommandBinding_Executed_Play");
        }

        private void CommandBinding_CanExecute_Play(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MediaBrower_PlayClick(object sender, RoutedEventArgs e)
        {
            this.media_media.Stop();
            this.media_media.Play();
            this.toggle_play.IsChecked = false;
        }

        void Play()
        {
            this.media_media.Play();
            this._timer.Start();

            this.FileName = this.media_media.Source.LocalPath;
        }

        void Pause()
        {
            this.media_media.Pause();
            this._timer.Stop();
        }

        public void Stop()
        {
            this.media_media.Position = TimeSpan.FromTicks(0);
            this.media_slider.Value = 0;
            this.media_media.Stop();
            this._timer.Stop();
            this.toggle_play.IsChecked = true;
            this.media_media.LoadedBehavior = MediaState.Manual;
        }

        public double LeftPercent
        {
            get { return (double)GetValue(LeftPercentProperty); }
            set { SetValue(LeftPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftPercent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftPercentProperty =
            DependencyProperty.Register("LeftPercent", typeof(double), typeof(MediaPlayer), new PropertyMetadata(0.2));


        public double RightPercent
        {
            get { return (double)GetValue(RightPercentProperty); }
            set { SetValue(RightPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightPercent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightPercentProperty =
            DependencyProperty.Register("RightPercent", typeof(double), typeof(MediaPlayer), new PropertyMetadata(0.8));

        private void btn_split_Click(object sender, RoutedEventArgs e)
        {
            this.LeftPercent = 0;
            this.RightPercent = 1;
        }


        public double TotalTime
        {
            get { return (double)GetValue(TotalTimeProperty); }
            set { SetValue(TotalTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalTimeProperty =
            DependencyProperty.Register("TotalTime", typeof(double), typeof(MediaPlayer), new PropertyMetadata(default(double), (d, e) =>
             {
                 MediaPlayer control = d as MediaPlayer;

                 if (control == null) return;

             }));

        private void btn_openFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            var result = open.ShowDialog();

            if (!result.HasValue) return;

            if (!result.Value) return;

            this.media_media.Source = new Uri(open.FileName, UriKind.Absolute);


            this.media_media.Stop();

            this.Play();
        }


        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(MediaPlayer), new PropertyMetadata(default(string), (d, e) =>
             {
                 MediaPlayer control = d as MediaPlayer;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));

    }


    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (parameter == null)
            {
                var d = double.Parse(value.ToString());

                var sp = TimeSpan.FromTicks((long)d);

                return sp.ToString().Split('.')[0];
            }
            else
            {
                //  Do：如果参数不为空 则按百分比方式计算
                var d = double.Parse(value.ToString());
                var t = double.Parse(parameter.ToString());

                var sp = TimeSpan.FromTicks((long)(d * t));

                return sp.ToString().Split('.')[0];
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiTimeSpanConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (value.Count() == 1)
            {
                var d = double.Parse(value.ToString());

                var sp = TimeSpan.FromTicks((long)d);

                return sp.ToString().Split('.')[0];
            }
            else if (value.Count() == 2)
            {
                //  Do：如果参数不为空 则按百分比方式计算
                var d = double.Parse(value[0].ToString());
                var t = double.Parse(value[1].ToString());

                var sp = TimeSpan.FromTicks((long)(d * t));

                return sp.ToString().Split('.')[0];
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
