using FFMpegCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using FFMpegCore.Enums;
using System.Linq;

namespace HeBianGu.App.Converter
{
    public class SubVideoConverterItem : VideoConverterItemBase
    {
        public SubVideoConverterItem(string filePath) : base(filePath)
        {

        }

        public override MediaInfo CreateOutputMediaInfo(IMediaAnalysis mediaInfo)
        {
            return new SubMediaInfo(mediaInfo, this.FilePath);
        }

        private bool _useCopyChannel = true;
        [DefaultValue(true)]
        [Display(Name = "复制通道", GroupName = "配置", Description = "启用后不转码，运行速度快")]
        public bool UseCopyChannel
        {
            get { return _useCopyChannel; }
            set
            {
                _useCopyChannel = value;
                RaisePropertyChanged();
            }
        }

        protected override void CreateArguments(FFMpegArgumentOptions options)
        {
            if (this.UseCopyChannel)
            {
                options.CopyChannel(Channel.All)
                    .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
                    .Seek(this.OutputMediaInfo.VedioAnalysis.StartTime)
                    .EndSeek(this.OutputMediaInfo.VedioAnalysis.EndTime)
                    .WithFastStart();
            }
            else
            {
                base.CreateArguments(options);
            }
        }
    }

    public class SubMediaInfo : MediaInfo
    {
        public SubMediaInfo(IMediaAnalysis model, string filePath) : base(model, filePath)
        {

        }

        protected override VideoAnalysis CreateVedioAnalysis(string filePath)
        {
            return new SubVideoAnalysis(this.Model, filePath);
        }

    }

    public class SubVideoAnalysis : VideoAnalysis
    {
        public SubVideoAnalysis(IMediaAnalysis model, string filePath) : base(model, filePath)
        {
            this.StartTime = model.Format.StartTime;
            this.EndTime = model.Format.Duration;
        }
    }
}
