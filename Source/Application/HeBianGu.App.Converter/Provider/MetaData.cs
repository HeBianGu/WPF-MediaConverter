using FFMpegCore;
using FFMpegCore.Builders.MetaData;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Converter
{
    /// <summary> 说明</summary>
    public class MetaData : NotifyPropertyChangedBase
    {
        public MetaData(IMediaAnalysis model)
        {
            if (model.Format.Tags.TryGetValue("title", out var title))
                this.Title = title;
            if (model.Format.Tags.TryGetValue("artist", out var artist))
                this.Artists = artist;
            if (model.Format.Tags.TryGetValue("album_artist", out var album_artist))
                this.AlbumArtists = album_artist;
            if (model.Format.Tags.TryGetValue("composer", out var composer))
                this.Composers = composer;
            if (model.Format.Tags.TryGetValue("copyright", out var copyright))
                this.Copyright = copyright;
            //if (model.Format.Tags.TryGetValue("album", out var album))
            //    this.alb = album;
            if (model.Format.Tags.TryGetValue("genre", out var genre))
                this.Genres = genre;
            //if (model.Format.Tags.TryGetValue("comment", out var comment))
            //    this.Title = comment;
            //if (model.Format.Tags.TryGetValue("encoder", out var encoder))
            //    this.Title = encoder;
            //if (model.Format.Tags.TryGetValue("major_brand", out var major_brand))
            //    this.Title = major_brand;
            //if (model.Format.Tags.TryGetValue("minor_version", out var minor_version))
            //    this.Title = minor_version;
            //if (model.Format.Tags.TryGetValue("compatible_brands", out var compatible_brands))
            //    this.Title = compatible_brands;
            //if (model.Format.Tags.TryGetValue("creation_time", out var creation_time))
            //    this.Title = creation_time;
            //if (model.Format.Tags.TryGetValue("software", out var software))
            //    this.Title = software;

        }

        private string _title;
        [Display(Name = "标题", GroupName = "视频", Description = "标题")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }


        private string _artists;
        [Display(Name = "艺术家", GroupName = "视频", Description = "艺术家")]
        public string Artists
        {
            get { return _artists; }
            set
            {
                _artists = value;
                RaisePropertyChanged();
            }
        }

        private string _composers;
        [Display(Name = "作者", GroupName = "视频", Description = "作者")]
        public string Composers
        {
            get { return _composers; }
            set
            {
                _composers = value;
                RaisePropertyChanged();
            }
        }

        private string _albumArtists;
        [Display(Name = "出版信息", GroupName = "视频", Description = "出版信息")]
        public string AlbumArtists
        {
            get { return _albumArtists; }
            set
            {
                _albumArtists = value;
                RaisePropertyChanged();
            }
        }


        private string _genres;
        [Display(Name = "类型", GroupName = "视频", Description = "类型")]
        public string Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                RaisePropertyChanged();
            }
        }

        private string _copyright;
        [Display(Name = "版权", GroupName = "视频", Description = "版权")]
        public string Copyright
        {
            get { return _copyright; }
            set
            {
                _copyright = value;
                RaisePropertyChanged();
            }
        }


        private Dictionary<TimeSpan, string> _chapters;
        [Display(Name = "章节", GroupName = "视频", Description = "章节")]
        public Dictionary<TimeSpan, string> Chapters
        {
            get { return _chapters; }
            set
            {
                _chapters = value;
                RaisePropertyChanged();
            }
        }

        public IReadOnlyMetaData CreateMetaData()
        {
            var builder = new MetaDataBuilder()
             .WithTitle(this.Title)
             .WithArtists(this.Artists.Split(new char[] { ',', ' ' }))
             .WithComposers(this.Composers.Split(new char[] { ',', ' ' }))
             .WithAlbumArtists(this.AlbumArtists.Split(new char[] { ',', ' ' }))
             .WithGenres(this.Genres.Split(new char[] { ',', ' ' }))
             .WithCopyright(this.Copyright)
             .AddChapters(this.Chapters, x => (x.Key, x.Value));
            return builder.Build();
        }
    }

}
