using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace HeBianGu.Domain.Converter
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

            ContainerFormat = FFMpeg.GetContainerFormat(fn);
            if (model.PrimaryAudioStream != null)
            {
                Codec = FFMpeg.GetCodec(model.PrimaryAudioStream?.CodecName);
                BitRate = model.PrimaryAudioStream.BitRate;
                StartTime = model.PrimaryAudioStream.StartTime;
                EndTime = model.PrimaryAudioStream.Duration;
                MetaData = model.PrimaryAudioStream.Tags ?? new Dictionary<string, string>();
            }
        }

        public AudioAnalysis() : base(null)
        {

        }

        private int _samplingRate = 48000;
        [Displayer(Name = "采样率", GroupName = "音频,输入参数,输出参数", Description = "采样率", Icon = "\xe7a6")]
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
        [Displayer(Name = "激活字节", GroupName = "音频,输入参数,输出参数", Description = "激活字节", Icon = "\xe7a4")]
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
        [Displayer(Name = "音频质量", GroupName = "音频,输入参数,输出参数", Description = "音频质量", Icon = "\xe7c1")]
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
            options.WithAudioCodec(Codec)
                .WithAudioBitrate(AudioQuality)
                .WithAudioSamplingRate(SamplingRate)
                //.WithAudioFilters
                .WithAudibleActivationBytes(ActivationBytes)
          //.WithAudibleEncryptionKeys
          .WithSpeedPreset(Speed)
          .UsingMultithreading(FFmpegSetting.Instance.UsingMultithreading)
          .WithConstantRateFactor(ConstantRateFactor)
          .Seek(StartTime)
          .EndSeek(EndTime)
          .WithVariableBitrate(VariableBitrate)
          .WithFastStart();
        }
    }
}
