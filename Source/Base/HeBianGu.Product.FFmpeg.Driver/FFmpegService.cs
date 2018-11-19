using HeBianGu.Product.FFmpeg.Base.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.Driver
{
    public class FFmpegService
    {
        public static FFmpegService Instance = new FFmpegService();


        FFmpegProcess _ffmpegProcess = new FFmpegProcess();

        FFmpegConvert _ffmpegConvert = new FFmpegConvert();

        FFmpegParameter _ffmpegParameter = new FFmpegParameter();


        public string Mp4ToWmv(string from, string to)
        {
            string param = string.Format(_ffmpegParameter.mp4towmv, from, to);

            var result = _ffmpegProcess.Execute(param);

            return result;

        }

        public void MediaToMP3(string from, string to)
        {
            string param = string.Format(_ffmpegParameter.mToSound, from, to);

            var result = _ffmpegProcess.Execute(param);

            Console.WriteLine("\n运行结束...\n");

            Debug.WriteLine(result);

        }

        public string Formats()
        {
            string result = _ffmpegProcess.ExecuteWithOutWait(_ffmpegParameter.ffmpeg_formats);

            return result;
        }

        public string GetDetail(string from)
        {
            string param = string.Format(_ffmpegParameter.ffmpeg_detial, from);

            var result = _ffmpegProcess.ExecuteWithErr(param);

            return result;
        }


        #region - 转换命令 -

        public MediaEntity GetMediaEntity(string filePath)
        {
            string txt = this.GetDetail(filePath);

            return _ffmpegConvert.GetMediaEntity(txt);


        }

        #endregion
    }
}
