using FFMpegCore;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Message;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Survey;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace HeBianGu.App.Converter
{
    /// <summary> 说明</summary>
    public abstract class MediaInfo<IVideoAnalysis, IAudioAnalysis> : ModelViewModel<IMediaAnalysis> where IVideoAnalysis : VideoAnalysis
        where IAudioAnalysis : AudioAnalysis
    {
        public MediaInfo(IMediaAnalysis model, string filePath) : base(model)
        {
            this.VedioAnalysis = this.CreateVedioAnalysis(filePath);
            this.AudioAnalysis = this.CreateAudioAnalysis(filePath);
            this.MetaData = model.Format.Tags ?? new Dictionary<string, string>();
            this.Size = new FileInfo(filePath).Length;
            this.Meta = new MetaData(model);
        }

        private Dictionary<string, string> _metaData;
        [Display(Name = "文件元数据", Description = "文件元数据")]
        [PropertyItemType(typeof(DictionaryPropertyViewItem))]
        public Dictionary<string, string> MetaData
        {
            get { return _metaData; }
            set
            {
                _metaData = value;
                RaisePropertyChanged();
            }
        }


        protected abstract IVideoAnalysis CreateVedioAnalysis(string filePath);
        protected abstract IAudioAnalysis CreateAudioAnalysis(string filePath);

        private long _size;
        [Browsable(false)]
        public long Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }

        private VideoAnalysis _vedioAnalysis;
        /// <summary> 说明  </summary>
        public VideoAnalysis VedioAnalysis
        {
            get { return _vedioAnalysis; }
            set
            {
                _vedioAnalysis = value;
                RaisePropertyChanged();
            }
        }

        private AudioAnalysis _audioAnalysis;
        /// <summary> 说明  </summary>
        public AudioAnalysis AudioAnalysis
        {
            get { return _audioAnalysis; }
            set
            {
                _audioAnalysis = value;
                RaisePropertyChanged();
            }
        }

        private MetaData _meta;
        /// <summary> 说明  </summary>
        public MetaData Meta
        {
            get { return _meta; }
            set
            {
                _meta = value;
                RaisePropertyChanged();
            }
        }

    }


    public abstract class MediaInfo<IVideoAnalysis> : MediaInfo<IVideoAnalysis, AudioAnalysis> where IVideoAnalysis : VideoAnalysis
    {
        public MediaInfo(IMediaAnalysis model, string filePath) : base(model, filePath)
        {

        }
        protected override AudioAnalysis CreateAudioAnalysis(string filePath)
        {
            return new AudioAnalysis(this.Model, filePath);
        }
    }

    public class MediaInfo : MediaInfo<VideoAnalysis>
    {
        public MediaInfo() : base(null, null)
        {

        }

        public MediaInfo(IMediaAnalysis model, string filePath) : base(model, filePath)
        {

        }

        protected override VideoAnalysis CreateVedioAnalysis(string filePath)
        {
            return new VideoAnalysis(this.Model, filePath);
        }
    }

}
