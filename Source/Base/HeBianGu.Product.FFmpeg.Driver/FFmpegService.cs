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


        public void Mp4ToWmv(string from, string to, Action<string> progressAction, Action<int> existAction)
        {
            Action<string> action = l =>
            {
                progressAction(this._ffmpegConvert.GetProgress(l));
            };

            string param = string.Format(_ffmpegParameter.mp4towmv, from, to);

            _ffmpegProcess.ExecuteWithRecevied(param, action, action, existAction);

        }

        public void MediaToMP3(string from, string to, Action<string> progressAction, Action<int> existAction)
        {

            Action<string> action = l =>
              {
                  progressAction(this._ffmpegConvert.GetProgress(l));
              };

            string param = string.Format(_ffmpegParameter.mToSound, from, to);

            _ffmpegProcess.ExecuteWithRecevied(param, action, action, existAction);

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
