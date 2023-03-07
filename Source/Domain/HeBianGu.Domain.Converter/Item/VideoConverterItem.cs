using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Builders.MetaData;
using HeBianGu.Base.WpfBase;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HeBianGu.Domain.Converter
{
    public abstract class VideoConverterItemBase : ProcessorConverterItemBase
    {
        public VideoConverterItemBase(string filePath) : base(filePath)
        {
           
        }

        public override void CreateMediaInfo(string filePath)
        {
            base.CreateMediaInfo(filePath);

            this.InputAnalysis = this.InputMediaInfo.VedioAnalysis;
            this.OutputAnalysis = this.OutputMediaInfo.VedioAnalysis;
        }

        [Displayer(Name = "设置视频输出参数", Icon = Icons.Set, GroupName = "操作,输出", Description = "设置视频输出参数")]
        public RelayCommand OutputVedioSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(OutputMediaInfo.VedioAnalysis, null, "设置视频输出参数");

            //(视频码率 + 音频码率) * 时长 / 8 = 文件大小
            OutputMediaInfo.Size = (long)((OutputMediaInfo.VedioAnalysis.BitRate + OutputMediaInfo.VedioAnalysis.BitRate) * OutputMediaInfo.Model.Duration.TotalSeconds / 8);
        });


        [Displayer(Name = "设置水印", Icon = Icons.T, GroupName = "操作,输出", Description = "设置水印")]
        public RelayCommand OutputWaterSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(OutputMediaInfo.VedioAnalysis, null, "设置视频输出参数");

            //(视频码率 + 音频码率) * 时长 / 8 = 文件大小
            OutputMediaInfo.Size = (long)((OutputMediaInfo.VedioAnalysis.BitRate + OutputMediaInfo.VedioAnalysis.BitRate) * OutputMediaInfo.Model.Duration.TotalSeconds / 8);
        });

        [Displayer(Name = "设置音频输出参数", Icon = "\xe6c0", GroupName = "操作,输出", Description = "设置音频输出参数")]
        public RelayCommand OutputAudioSetCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowEdit(OutputMediaInfo.AudioAnalysis, null, "设置音频输出参数");
        });

        [Displayer(Name = "查看视频参数", Icon = "\xeabe", GroupName = "操作,输入", Description = "查看视频参数")]
        public RelayCommand InputVideoViewCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowView(OutputMediaInfo.VedioAnalysis, null, "查看视频参数", x => x.UseEnumerator = true);
        });

        [Displayer(Name = "查看音频参数", Icon = "\xe6c0", GroupName = "操作,输入", Description = "查看音频参数")]
        public RelayCommand InputAudioViewCommand => new RelayCommand(async (s, e) =>
        {
            await MessageProxy.PropertyGrid.ShowView(InputMediaInfo.AudioAnalysis, null, "查看音频参数", x => x.UseEnumerator = true);
        });

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            OutputMediaInfo.VedioAnalysis.CreateArguments(options);

            //  Do ：设置基础信息
            var metaData = OutputMediaInfo.Meta.CreateMetaData();
            var arg = new MapMetaDataArgument(MetaDataSerializer.Instance.Serialize(metaData));
            options.WithArgument(arg);
        }

        protected override string CreateOutputPath(string groupPath)
        {
            return Path.Combine(groupPath, Path.GetFileNameWithoutExtension(FilePath), DateTime.Now.ToString("yyyyMMddHHmmssfff") + OutputMediaInfo.VedioAnalysis.ContainerFormat.Extension);
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
