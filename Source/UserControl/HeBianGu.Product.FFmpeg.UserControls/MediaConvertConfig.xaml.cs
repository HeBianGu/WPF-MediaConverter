using HeBianGu.Product.FFmpeg.Base.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Xml;
using System.Xml.Serialization;
using HeBianGu.Product.FFmpeg.Driver;

namespace HeBianGu.Product.FFmpeg.UserControls
{
    /// <summary>
    /// MediaConvertConfig.xaml 的交互逻辑
    /// </summary>
    public partial class MediaConvertConfig : UserControl
    {
        public MediaConvertConfig()
        {
            InitializeComponent();


        }

        //声明和注册路由事件
        public static readonly RoutedEvent SumitClickRoutedEvent =
            EventManager.RegisterRoutedEvent("SumitClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(MediaConvertConfig));
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.CommandString = this.ConvertToCommand();

            this.OnSumitClick();
        }



        public string CommandString
        {
            get { return (string)GetValue(CommandStringProperty); }
            set { SetValue(CommandStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandStringProperty =
            DependencyProperty.Register("CommandString", typeof(string), typeof(MediaConvertConfig), new PropertyMetadata(default(string), (d, e) =>
             {
                 MediaConvertConfig control = d as MediaConvertConfig;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));



        public string ConvertToCommand()
        {
            XmlDataProvider xml = this.Resources["Source.XmlDataProvider.FFmpegConfig.General"] as XmlDataProvider;

            XmlElement root = xml.Document.DocumentElement as XmlElement;

            XmlNodeList texts = xml.Document.DocumentElement.SelectNodes("FFmpegCommandTextParameter") as XmlNodeList;

            XmlNodeList checks = xml.Document.DocumentElement.SelectNodes("FFmpegCommandCheckParameter") as XmlNodeList;

            List<FFmpegCommandTextParameter> textModels = new List<FFmpegCommandTextParameter>();

            foreach (XmlElement item in texts)
            {
                FFmpegCommandTextParameter model = new FFmpegCommandTextParameter();

                model.Text = item.GetAttribute("Text");
                model.ToolTip = item.GetAttribute("ToolTip");
                model.Command = item.GetAttribute("Command");
                model.Parameter = item.GetAttribute("Parameter");
                model.IsChecked =bool.Parse(item.GetAttribute("IsChecked"));

                textModels.Add(model);
            }

            List<FFmpegCommandCheckParameter> checkModels = new List<FFmpegCommandCheckParameter>();

            foreach (XmlElement item in checks)
            {
                FFmpegCommandCheckParameter model = new FFmpegCommandCheckParameter();

                model.Text = item.GetAttribute("Text");
                model.ToolTip = item.GetAttribute("ToolTip");
                model.Command = item.GetAttribute("Command");
                model.IsChecked = bool.Parse(item.GetAttribute("IsChecked"));

                checkModels.Add(model);
            }


            string result = string.Empty.GetCommandParameter(checkModels).GetCommandParameter(textModels);


            return result;

        }
    }


}
