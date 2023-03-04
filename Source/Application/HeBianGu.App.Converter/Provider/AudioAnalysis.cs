using FFMpegCore;
using FFMpegCore.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace HeBianGu.App.Converter
{
    public class AudioAnalysis : AnalysisBase
    {
        public AudioAnalysis(IMediaAnalysis model, string filePath) : base(model)
        {
            string extension = Path.GetExtension(filePath);
            //  ToDo：model.Format.FormatName表示可以转换的格式列表？ 
            string fn = null;
            var splits = model.Format.FormatName.Split(',');
            if (splits.Count() > 1)
                fn = splits.FirstOrDefault(x => extension.ToUpper().EndsWith(x.ToUpper()));

            if (string.IsNullOrEmpty(fn))
                fn = splits[0];

            this.ContainerFormat = FFMpeg.GetContainerFormat(fn);
            this.Codec = FFMpeg.GetCodec(model.PrimaryAudioStream.CodecName);
            this.BitRate = model.PrimaryAudioStream.BitRate;
            this.StartTime = model.PrimaryAudioStream.StartTime;
            this.EndTime = model.PrimaryAudioStream.Duration;
            this.MetaData = model.PrimaryAudioStream.Tags ?? new Dictionary<string, string>();
        }

        public AudioAnalysis() : base(null)
        {

        }

        private int _samplingRate = 48000;
        [Display(Name = "采样率", GroupName = "视频", Description = "采样率")]
        public int SamplingRate
        {
            get { return _samplingRate; }
            set
            {
                _samplingRate = value;
                RaisePropertyChanged();
            }
        }


        private string _activationBytes;
        [Display(Name = "激活字节", GroupName = "视频", Description = "激活字节")]
        public string ActivationBytes
        {
            get { return _activationBytes; }
            set
            {
                _activationBytes = value;
                RaisePropertyChanged();
            }
        }

        private AudioQuality _audioQuality = AudioQuality.Normal;
        [Display(Name = "音频质量", GroupName = "视频", Description = "音频质量")]
        public AudioQuality AudioQuality
        {
            get { return _audioQuality; }
            set
            {
                _audioQuality = value;
                RaisePropertyChanged();
            }
        }

        public override void CreateArguments(FFMpegArgumentOptions options)
        {
            options.WithAudioCodec(this.Codec)
                .WithAudioBitrate(this.AudioQuality)
                .WithAudioSamplingRate(this.SamplingRate)
                //.WithAudioFilters
                .WithAudibleActivationBytes(this.ActivationBytes)
          //.WithAudibleEncryptionKeys
          .WithSpeedPreset(this.Speed)
          .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
          .WithConstantRateFactor(this.ConstantRateFactor)
          .Seek(this.StartTime)
          .EndSeek(this.EndTime)
          .WithVariableBitrate(this.VariableBitrate)
          .WithFastStart();
        }
    }
}
