﻿using HeBianGu.General.Logger;
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

        public void ConvertWithParams(string param, Action<string> progressAction, Action<int> existAction)
        {
            Action<string> action = l =>
            {
                progressAction(this._ffmpegConvert.GetProgress(l));
            };

            _ffmpegProcess.ExecuteWithRecevied(param, action, action, existAction);

        }

        public void MediaToWmv(string from, string to, Action<string> progressAction, Action<int> existAction)
        {
            Action<string> action = l =>
            {
                progressAction(this._ffmpegConvert.GetProgress(l));

            };

            string param = string.Format(_ffmpegParameter.media_to_wmv, from, to);

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

        public List<SupportFormatEntity> GetFormats()
        {
            string result = _ffmpegProcess.ExecuteWithOutWait(_ffmpegParameter.ffmpeg_formats);

            return _ffmpegConvert.GetFomarts(result);
        }

        public List<SupportFormatEntity> GetCodecs()
        {
            string result = _ffmpegProcess.ExecuteWithOutWait(_ffmpegParameter.ffmpeg_codecs);

            return _ffmpegConvert.GetCodecs(result);
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
