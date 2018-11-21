using HeBianGu.General.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.Product.FFmpeg.MediaConverter
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            Log4Servcie.Instance.Error((Exception)e.ExceptionObject);

            Application.Current.Dispatcher.Invoke
                (
                () => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作")
                );

        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {

            Log4Servcie.Instance.Error(e.Exception);

            Application.Current.Dispatcher.Invoke(() => MessageBox.Show(e.Exception.Message));

            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            string exeFileFullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileNameWithoutExtension(exeFileFullPath);
            string binPath = Path.GetDirectoryName(exeFileFullPath);

            binPath = Path.GetDirectoryName(binPath);

            string logFilePath = Path.GetDirectoryName(binPath);

            var exe = System.Diagnostics.Process.GetCurrentProcess();

            if (exe == null) return;

            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string logPath = Path.Combine(documentPath, "Logs", exeName);

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            //  初始化日志
            Log4Servcie.Instance.InitLogger(logPath, System.Diagnostics.Process.GetCurrentProcess().ProcessName);

            MainWindow window = new MainWindow();
            window.Show();

        }
    }
}
