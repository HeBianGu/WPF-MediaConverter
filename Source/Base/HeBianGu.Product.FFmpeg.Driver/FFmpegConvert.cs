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

            MediaEntity entity = new MediaEntity();

            //从视频信息中解析时长
            string regexDuration = "Duration: (.*?), start: (.*?), bitrate: (\\d*) kb\\/s";

            Regex r = new Regex(regexDuration);

            var result = r.Match(txt);

            var arr = result.Value.Split(',');

            entity.Duration = arr[0].Remove(0, arr[0].IndexOf(':') + 1);
            entity.Start = arr[1].Split(':')[1];
            entity.Bitrate = arr[2].Split(':')[1];



            string regexVideo = "Video: (.*?), (.*?), (.*?)[,\\s]";

            Regex r1 = new Regex(regexVideo);

            result = r1.Match(txt);

            arr = result.Value.Split(':')[1].Split(',');

            entity.MediaCode = arr[0];
            entity.MediaType = arr[1];
            entity.Resoluction = arr[2];

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
            return  this.GetSupportFormatEntity(source, "--");
        }

        public List<SupportFormatEntity> GetCodecs(string source)
        {
            return this.GetSupportFormatEntity(source, "-------");
        }


        List<SupportFormatEntity> GetSupportFormatEntity(string source,string flag= "--")
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
