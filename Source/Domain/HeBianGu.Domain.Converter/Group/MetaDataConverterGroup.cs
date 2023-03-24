using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Domain.Converter;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "章节/标题", Icon = "\xe76b", GroupName = "视频,音频", Order = 9, Description = "给视频增加章节，标题，许可，作者等参数")]
    public class MetaDataConverterGroup : ConverterGroupBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();

            //this.Artists = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            this.Composers = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
            //this.AlbumArtists = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
            this.Copyright = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
            this.Software = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            this.Creation_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        private string _title = string.Empty;
        [Display(Name = "标题", GroupName = "配置", Description = "标题")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _artists = string.Empty;
        [Display(Name = "艺术家", GroupName = "配置", Description = "艺术家")]
        public string Artists
        {
            get { return _artists; }
            set
            {
                _artists = value;
                RaisePropertyChanged();
            }
        }

        private string _composers = string.Empty;
        [Display(Name = "作者", GroupName = "配置", Description = "作者")]
        public string Composers
        {
            get { return _composers; }
            set
            {
                _composers = value;
                RaisePropertyChanged();
            }
        }

        private string _albumArtists = string.Empty;
        [TextBox(TextWrapping = TextWrapping.NoWrap)]
        [Display(Name = "出版信息", GroupName = "配置", Description = "出版信息")]
        public string AlbumArtists
        {
            get { return _albumArtists; }
            set
            {
                _albumArtists = value;
                RaisePropertyChanged();
            }
        }


        private string _genres = string.Empty;
        [Display(Name = "类型", GroupName = "配置", Description = "类型")]
        public string Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                RaisePropertyChanged();
            }
        }

        private string _copyright = string.Empty;
        [TextBox(TextWrapping = System.Windows.TextWrapping.NoWrap)]
        [Display(Name = "版权", GroupName = "配置", Description = "版权")]
        public string Copyright
        {
            get { return _copyright; }
            set
            {
                _copyright = value;
                RaisePropertyChanged();
            }
        }

        private string _software;
        [TextBox(TextWrapping = TextWrapping.NoWrap)]
        [Display(Name = "创建软件", GroupName = "配置", Description = "软件")]
        public string Software
        {
            get { return _software; }
            set
            {
                _software = value;
                RaisePropertyChanged();
            }
        }

        private string _creation_time;
        [Display(Name = "创建时间", GroupName = "配置", Description = "创建时间")]
        public string Creation_time
        {
            get { return _creation_time; }
            set
            {
                _creation_time = value;
                RaisePropertyChanged();
            }
        }

        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            var result = new MetaDataConverterItem(filePath, x =>
            {
                x.OutputMediaInfo.Meta.Title = this.Title;
                x.OutputMediaInfo.Meta.Creation_time = this.Creation_time;
                x.OutputMediaInfo.Meta.Software = this.Software;
                x.OutputMediaInfo.Meta.Copyright = this.Copyright;
                x.OutputMediaInfo.Meta.Composers = this.Composers;
                x.OutputMediaInfo.Meta.Artists = this.Artists;
                x.OutputMediaInfo.Meta.AlbumArtists = this.AlbumArtists;
                x.OutputMediaInfo.Meta.Genres = this.Genres;
            });
            return result;
        }
    }
}
