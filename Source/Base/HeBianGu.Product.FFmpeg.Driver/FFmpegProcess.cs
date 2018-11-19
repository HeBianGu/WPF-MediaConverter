using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.Driver
{
    public class FFmpegProcess
    {

        public const string format = "-i {0} -b 1024k -acodec copy -f mp4 {1}";

        public const string mp4towmv = @"-i {0} -vcodec wmv1 {1}";

        public const string mToSound = "-i {0} -vn -ab 128k {1}";


        protected static string Execute(string parameters, string exePath = @"F:\Solution\HeBianGu.Product.FFmpeg.MediaConverter\HeBianGu.Product.FFmpeg.MediaConverter\ffmpeg.exe")
        {
            string result = String.Empty;

            using (Process p = new Process())
            {

                p.StartInfo.UseShellExecute = false;

                p.StartInfo.CreateNoWindow = true;

                //p.StartInfo.WorkingDirectory = @"F:\Solution\HeBianGu.Product.FFmpeg.MediaConverter\HeBianGu.Product.FFmpeg.MediaConverterr\";

                p.StartInfo.RedirectStandardOutput = true;

                p.StartInfo.FileName = exePath;

                p.StartInfo.Arguments = parameters;

                p.Start();

                p.WaitForExit();

                result = p.StandardOutput.ReadToEnd();

                Console.WriteLine("\n运行结束...\n");
            }

            return result;

        }


        public static void Mp4ToWmv(string from, string to)
        {
            string param = string.Format(mp4towmv, from, to);

            var result = FFmpegProcess.Execute(param);

            Debug.WriteLine(result);

        }

        public static void MediaToMP3(string from, string to)
        {
            string param = string.Format(mToSound, from, to);

            var result = FFmpegProcess.Execute(param);

            Console.WriteLine("\n运行结束...\n");

            Debug.WriteLine(result);

        }

    }
}
