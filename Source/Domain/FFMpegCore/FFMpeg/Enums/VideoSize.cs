using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FFMpegCore.Enums
{
    public enum VideoSize
    {
        [Display(Name = "1080p(全高清)")]
        FD = 1080,
        [Display(Name = "720p(高清)")]
        HD = 720,
        [Display(Name = "480p(标清)")]
        ED = 480,
        [Display(Name = "360p(低清)")]
        LD = 360,
        [Display(Name = "原始数据")]
        Original = -1
    }
}
