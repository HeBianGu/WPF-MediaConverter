using HeBianGu.Product.FFmpeg.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.Driver
{
    public class FFmpegConvert
    {
        public  MediaEntity GetMediaEntity(string txt)
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

            entity.Video = arr[0];
            entity.MediaType = arr[1];
            entity.Resoluction = arr[2];

            return entity;
        }
    }
}
