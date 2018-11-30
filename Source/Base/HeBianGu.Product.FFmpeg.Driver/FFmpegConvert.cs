using HeBianGu.Product.FFmpeg.Base.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.Driver
{
    public class FFmpegConvert
    {
        public MediaEntity GetMediaEntity(string txt)
        {

            //            Metadata:
            //            major_brand: isom
            //       minor_version   : 512
            //    compatible_brands: isomiso2avc1mp41
            //    title           : EV褰曞睆3.8.4杞欢褰曞埗
            //    encoder         : Lavf56.38.102
            //    comment: 鏈棰戠敱婀栧崡涓€鍞俊鎭鎶€寮€鍙戠殑EV褰曞睆杞欢褰曞埗锛寃ww.ieway.cn
            //Duration: 00:09:12.24, start: 0.014125, bitrate: 415 kb / s
            //    Stream #0:0(und): Video: h264 (Constrained Baseline) (avc1 / 0x31637661), yuv420p, 1920x1080, 284 kb/s, 14.68 fps, 15 tbr, 15360 tbn, 30 tbc (default)
            //    Metadata:
            //      handler_name: VideoHandler
            //Stream #0:1(und): Audio: aac (LC) (mp4a / 0x6134706D), 48000 Hz, stereo, fltp, 127 kb/s (default)
            //    Metadata:
            //            handler_name: SoundHandler


            MediaEntity entity = new MediaEntity();

            //从视频信息中解析时长
            string regexDuration = "Duration: (.*?), start: (.*?), bitrate: (\\d*) kb\\/s";

            Regex r = new Regex(regexDuration);

            var result = r.Match(txt);

            var arr = result.Value.Split(',');

            entity.Duration = arr[0].Remove(0, arr[0].IndexOf(':') + 1);
            entity.Start = arr[1].Split(':')[1];
            entity.Bitrate = arr[2].Split(':')[1].Trim().Split(' ')[0];

            string regexVideo = "Video: (.*?), (.*?), (.*?), (.*?), (.*?)[,\\s]";

            Regex r1 = new Regex(regexVideo);

            result = r1.Match(txt);


            arr = result.Value.Substring(result.Value.IndexOf(':') + 1).Split(',');

            entity.MediaCode = arr[0].Trim().Split(' ')[0];
            entity.MediaType = entity.MediaCode;
            entity.Resoluction = arr[1].Trim();
            entity.Aspect = arr[2].Trim();
            entity.Rate = arr[4].Trim().Split(' ')[0];
            return entity;
        }


        /// <summary> frame= 2833 fps=110 q=24.8 size=   12560kB time=00:03:09.05 bitrate= 544.3kbits/s dup=52 drop=0   </summary>
        public string GetProgress(string str)
        {
            if (str == null) return string.Empty;

            if (!str.StartsWith("frame=")) return string.Empty;

            var result = str.Split(' ', '=').ToList();

            result.RemoveAll(l => string.IsNullOrEmpty(l));

            return result[9];
        }


        public List<SupportFormatEntity> GetFomarts(string source)
        {
            return this.GetSupportFormatEntity(source, "--");
        }

        public List<SupportFormatEntity> GetCodecs(string source)
        {
            return this.GetSupportFormatEntity(source, "-------");
        }


        List<SupportFormatEntity> GetSupportFormatEntity(string source, string flag = "--")
        {
            var result = source.Split('\r').Select(l => l.Trim('\n')).ToList();

            var kk = result.FindIndex(l => l.Trim() == flag);

            var v = result.Skip(kk + 1).ToList();

            string[] arr = new string[] { "    " };

            List<SupportFormatEntity> collection = new List<SupportFormatEntity>();

            foreach (var item in v)
            {
                var cc = item.Split(arr, StringSplitOptions.RemoveEmptyEntries);

                if (cc.Length != 2) continue;

                var vv = cc[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (vv.Length != 2) continue;

                SupportFormatEntity entity = new SupportFormatEntity();

                entity.Supported = vv[0];
                entity.Name = vv[1];
                entity.ToolTip = cc[1];

                collection.Add(entity);

            }


            Debug.WriteLine(source);

            return collection;
        }

    }

    public static class FFmpegConvertExtention
    {
        public static string GetCommandParameter(this string str, List<FFmpegCommandCheckParameter> checks)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in checks)
            {
                if (!item.IsChecked) continue;

                sb.Append(" " + item.Command);
            }

            return str + sb.ToString();
        }

        public static string GetCommandParameter(this string str, List<FFmpegCommandTextParameter> texts)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in texts)
            {
                if (!item.IsChecked) continue;

                sb.Append(" " + item.Command + " " + item.Parameter);
            }

            return str + sb.ToString();
        }
    }
}
