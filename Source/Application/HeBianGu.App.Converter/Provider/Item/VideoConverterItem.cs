using FFMpegCore;
using HeBianGu.Base.WpfBase;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HeBianGu.App.Converter
{
    public abstract class VideoConverterItemBase : ProcessorConverterItemBase
    {
        public VideoConverterItemBase(string filePath) : base(filePath)
        {
            this.InputAnalysis = this.InputMediaInfo.VedioAnalysis;
            this.OutputAnalysis = this.OutputMediaInfo.VedioAnalysis;
        }

        [Displayer(Name = "设置视频输出参数", Icon = Icons.Set, GroupName = "操作,输出", Description = "设置视频输出参数")]
        public RelayCommand OutputVedioSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(this.OutputMediaInfo.VedioAnalysis, null, "设置视频输出参数");

            //(视频码率 + 音频码率) * 时长 / 8 = 文件大小
            this.OutputMediaInfo.Size = (long)((this.OutputMediaInfo.VedioAnalysis.BitRate + this.OutputMediaInfo.VedioAnalysis.BitRate) * this.OutputMediaInfo.Model.Duration.TotalSeconds / 8);
        });


        [Displayer(Name = "设置水印", Icon = Icons.Set, GroupName = "操作,输出", Description = "设置水印")]
        public RelayCommand OutputWaterSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(this.OutputMediaInfo.VedioAnalysis, null, "设置视频输出参数");

            //(视频码率 + 音频码率) * 时长 / 8 = 文件大小
            this.OutputMediaInfo.Size = (long)((this.OutputMediaInfo.VedioAnalysis.BitRate + this.OutputMediaInfo.VedioAnalysis.BitRate) * this.OutputMediaInfo.Model.Duration.TotalSeconds / 8);
        });

        [Displayer(Name = "设置音频输出参数", Icon = Icons.Set, GroupName = "操作,输出", Description = "设置音频输出参数")]
        public RelayCommand OutputAudioSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(this.OutputMediaInfo.AudioAnalysis, null, "设置音频输出参数");
        });

        [Displayer(Name = "查看视频参数", Icon = "\xe76b", GroupName = "操作,输入", Description = "查看视频参数")]
        public RelayCommand InputVideoViewCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowView(this.OutputMediaInfo.VedioAnalysis, null, "查看视频参数", x => x.UseEnumerator = true);
        });

        [Displayer(Name = "查看音频参数", Icon = "\xe76b", GroupName = "操作,输入", Description = "查看音频参数")]
        public RelayCommand InputAudioViewCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowView(this.InputMediaInfo.AudioAnalysis, null, "查看音频参数", x => x.UseEnumerator = true);
        });

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            this.OutputMediaInfo.VedioAnalysis.CreateArguments(options);
        }

        protected override string CreateOutputPath(string groupPath)
        {
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(this.FilePath), DateTime.Now.ToString("yyyyMMddHHmmssfff") + this.OutputMediaInfo.VedioAnalysis.ContainerFormat.Extension);
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


        //protected override async Task<bool> Set()
        //{
        //    return await MessageProxy.Presenter.Show(VideoFormats, null, "选择格式");
        //}

    }
}
