using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.Base.Model
{
    public class MediaEntity
    {
        /// <summary> 视频时长 </summary>
        public string Duration { get; set; }

        /// <summary> 开始时间 </summary>
        public string Start { get; set; }

        /// <summary> 比特率 </summary>
        public string Bitrate { get; set; }

        /// <summary> 编码格式 h264 (Constrained Baseline) (avc1 / 0x31637661) </summary>
        public string MediaCode { get; set; }

        /// <summary> 视频格式 </summary>
        public string MediaType { get; set; }

        /// <summary> 分辨率 yuv420p </summary>
        public string Resoluction { get; set; }

        /// <summary> 宽高比 1920x1080 </summary>
        public string Aspect { get; set; }

        /// <summary> 帧频 14.68 fps </summary>
        public string Rate { get; set; }
    }
}
