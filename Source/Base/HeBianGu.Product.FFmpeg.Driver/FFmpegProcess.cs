using HeBianGu.General.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.Driver
{
    public class FFmpegProcess
    {
        public string Execute(string parameters, string exePath = @"F:\GitHub\WPF-MediaConverter\Product\Dll\ffmpeg.exe")
        {
            string result = String.Empty;

            string err = string.Empty;

            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = exePath;
                p.StartInfo.Arguments = parameters;
                p.Start();
                p.WaitForExit();
                result = p.StandardOutput.ReadLine();


                Debug.WriteLine("\n运行结束...\n");
            }

            return result;

        }

        public void ExecuteWithRecevied(string parameters, Action<string> OutputDataReceived, Action<string> ErrorDataReceived, Action<int> Exited, string exePath = @"F:\GitHub\WPF-MediaConverter\Product\Dll\ffmpeg.exe")
        {

            ProcessStartInfo startInfo = new ProcessStartInfo(exePath, parameters);
            startInfo.CreateNoWindow = true;   //不创建窗口
            startInfo.UseShellExecute = false;
            //不使用系统外壳程序启动,重定向输出的话必须设为false
            startInfo.RedirectStandardOutput = true; //重定向输出，
            startInfo.RedirectStandardError = true;


            try
            {
                Process process = Process.Start(startInfo);
                process.OutputDataReceived += (s, _e) =>
                {

                    Debug.WriteLine("OutputDataReceived：" + _e.Data);

                    Log4Servcie.Instance.Info("OutputDataReceived：" + _e.Data);

                    if (OutputDataReceived != null)
                    {
                        OutputDataReceived(_e.Data);
                    }
                };

                process.ErrorDataReceived += (s, _e) =>
                {
                    Debug.WriteLine("ErrorDataReceived：" + _e.Data);

                    Log4Servcie.Instance.Info("ErrorDataReceived：" + _e.Data);

                    if (ErrorDataReceived != null)
                    {
                        ErrorDataReceived(_e.Data);
                    }
                };
                //当EnableRaisingEvents为true，进程退出时Process会调用下面的委托函数
                process.Exited += (s, _e) =>
                {
                    Debug.WriteLine("Exited:" + process.ExitCode);

                    Log4Servcie.Instance.Info("Exited:" + process.ExitCode);

                    if (Exited != null)
                    {
                        Exited(process.ExitCode);
                    }
                };
                process.EnableRaisingEvents = true;
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                //process.WaitForExit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


        public string ExecuteWithErr(string parameters, string exePath = @"F:\GitHub\WPF-MediaConverter\Product\Dll\ffmpeg.exe")
        {
            string result = String.Empty;

            using (Process p = new Process())
            {

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;

                p.StartInfo.FileName = exePath;
                p.StartInfo.Arguments = parameters;
                p.Start();
                p.WaitForExit();

                result = p.StandardError.ReadToEnd();

                Debug.WriteLine("\n运行结束...\n");
            }

            return result;

        }

        public string ExecuteWithOutWait(string parameters, string exePath = @"F:\GitHub\WPF-MediaConverter\Product\Dll\ffmpeg.exe")
        {
            string result = String.Empty;

            using (Process p = new Process())
            {

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = exePath;
                p.StartInfo.Arguments = parameters;
                p.Start();

                result = p.StandardOutput.ReadToEnd();

                Console.WriteLine("\n运行结束...\n");
            }

            return result;

        }

    }
}
