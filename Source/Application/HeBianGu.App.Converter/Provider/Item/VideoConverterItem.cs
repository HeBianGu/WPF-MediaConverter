using FFMpegCore;
using HeBianGu.Base.WpfBase;
using System.IO;
using System.Threading.Tasks;

namespace HeBianGu.App.Converter
{
    public abstract class VideoConverterItemBase : ProcessorConverterItemBase
    {
        public VideoConverterItemBase(string filePath) : base(filePath)
        {

        }

        [Displayer(Name = "设置输出参数", Icon = Icons.Set, GroupName = "操作", Description = "设置输出参数")]
        public RelayCommand OutPutSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(this.OutputMediaInfo.VedioAnalysis, null, "设置输出参数");

            //(视频码率 + 音频码率) * 时长 / 8 = 文件大小
            this.OutputMediaInfo.Size = (long)((this.OutputMediaInfo.VedioAnalysis.BitRate + this.OutputMediaInfo.VedioAnalysis.BitRate) * this.OutputMediaInfo.Model.Duration.TotalSeconds / 8);
        });

        [Displayer(Name = "查看视频参数", Icon = "\xe76b", GroupName = "操作", Description = "查看视频参数")]
        public RelayCommand InputViewCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowView(this.OutputMediaInfo.VedioAnalysis, null, "查看视频参数", x => x.UseEnumerator = true);
        });

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            this.OutputMediaInfo.VedioAnalysis.CreateArguments(options);
        }

        protected override string CreateOutputPath(string groupPath)
        {
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(this.FilePath) + this.OutputMediaInfo.VedioAnalysis.ContainerFormat.Extension);
        }

    }
    public class VideoConverterItem : VideoConverterItemBase
    {
        public VideoConverterItem(string filePath) : base(filePath)
        {

        }

        private VideoFormats _videoFormats = new VideoFormats();
        /// <summary> 说明  </summary>
        public VideoFormats VideoFormats
        {
            get { return _videoFormats; }
            set
            {
                _videoFormats = value;
                RaisePropertyChanged();
            }
        }

        protected override async Task<bool> Set()
        {
            return await MessageProxy.Presenter.Show(VideoFormats, null, "选择格式");
        }

    }
}
