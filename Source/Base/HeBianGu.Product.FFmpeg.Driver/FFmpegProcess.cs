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


                //Debug.WriteLine("说明"+ result);

                //StreamReader SR = p.StandardOutput;

                //while (!SR.EndOfStream)
                //{
                //    var s = SR.ReadLineAsync();

                //    s.ContinueWith(l =>
                //    {
                //        Debug.WriteLine("说明" + l);
                //    });
                    

                //    Thread.Sleep(1000);

                //}


                Debug.WriteLine("\n运行结束...\n");
            }

            return result;

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
