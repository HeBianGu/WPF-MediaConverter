using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// MediaConvertConfig.xaml 的交互逻辑
    /// </summary>
    public partial class MediaConvertConfig : UserControl, ICommandSource
    {
        public MediaConvertConfig()
        {
            InitializeComponent();


        }

        [Bindable(true), Category("Action"), Localizability(LocalizationCategory.NeverLocalize)]
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(MediaConvertConfig), new PropertyMetadata(default(object)));



        [Bindable(true), Category("Action")]
        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(MediaConvertConfig), new PropertyMetadata(default(IInputElement)));


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MediaConvertConfig), new PropertyMetadata(default(ICommand), (d, e) =>
             {
                 MediaConvertConfig control = d as MediaConvertConfig;

                 if (control == null) return;

                 ICommand config = e.NewValue as ICommand;

             }));



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
            this.OnSumitClick();
        }
    }
}
