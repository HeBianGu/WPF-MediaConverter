using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Builders.MetaData;
using FFMpegCore.Enums;
using System;
using System.IO;

namespace HeBianGu.Domain.Converter
{
    /// <summary>
    /// https://download.blender.org/peach/bigbuckbunny_movies/
    /// http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_h264.mov
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_1080p_h264.mov 
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_1080p_stereo.avi 
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_1080p_stereo.ogg
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_1080p_surround.avi
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_h264.mov
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_stereo.avi
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_stereo.ogg
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround.avi
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_720p_h264.mov
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_720p_stereo.avi
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_720p_stereo.ogg
    /// http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_720p_surround.avi
    /// </summary>
    public class CrawlerConverterItem : CrawlerConverterItemBase
    {
        public CrawlerConverterItem(string filePath, Action<ConverterItemBase> builder = null) : base(filePath, builder)
        {
            this.UseOutToolCommadNames = $"{nameof(PlayOutputCommand)},{nameof(OpenCommand)},{nameof(DeleteCommand)},{nameof(DeleteFileCommand)}";
        }

        protected override FFMpegArgumentProcessor CreateProcessor()
        {
            return FFMpegArguments.FromFileInput(FilePath,false).OutputToFile(OutputPath, true);
        }

        protected override string CreateOutputPath(string groupPath)
        {
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(FilePath), DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".mp4");

        }
    }
}
