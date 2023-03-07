using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using HeBianGu.Domain.Converter;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "视频水印", Icon = "\xe649", GroupName = "视频", Order = 11, Description = "给视频添加水印")]
    public class DrawTextConverterGroup : ConverterGroupBase
    {
        private string _text = "HeBianGu";
        [DefaultValue("HeBianGu")]
        [Display(Name = "水印文本", GroupName = "配置", Description = "默认水印文本")]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        private int _fontSize = 50;
        [DefaultValue(50)]
        [Display(Name = "文本大小", GroupName = "配置", Description = "默认文本大小")]
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }


        private string _fontcolor = "white@0.8";
        [DefaultValue("white@0.8")]
        [Display(Name = "文本颜色", GroupName = "配置", Description = "默认文本颜色")]
        public string Fontcolor
        {
            get { return _fontcolor; }
            set
            {
                _fontcolor = value;
                RaisePropertyChanged();
            }
        }


        private bool _box = true;
        [DefaultValue(true)]
        [Display(Name = "显示背景", GroupName = "配置", Description = "显示背景颜色")]
        public bool Box
        {
            get { return _box; }
            set
            {
                _box = value;
                RaisePropertyChanged();
            }
        }


        private string _boxcolor = "black@0.2";
        [DefaultValue("black@0.2")]
        [Display(Name = "背景颜色", GroupName = "配置", Description = "默认背景颜色")]
        public string Boxcolor
        {
            get { return _boxcolor; }
            set
            {
                _boxcolor = value;
                RaisePropertyChanged();
            }
        }


        private int _boxborderw = 15;
        [Range(0, 1000)]
        [DefaultValue(15)]
        [Display(Name = "背景内边距", GroupName = "配置", Description = "背景内边距")]
        public int Boxborderw
        {
            get { return _boxborderw; }
            set
            {
                _boxborderw = value;
                RaisePropertyChanged();
            }
        }


        private string _x = "w-text_w-10";
        [DefaultValue("w-text_w-10")]
        [Display(Name = "X位置", GroupName = "配置", Description = "默认X位置")]
        public string X
        {
            get { return _x; }
            set
            {
                _x = value;
                RaisePropertyChanged();
            }
        }


        private string _y = "10";
        [DefaultValue("10")]
        [Display(Name = "Y位置", GroupName = "配置", Description = "默认Y位置")]
        public string Y
        {
            get { return _y; }
            set
            {
                _y = value;
                RaisePropertyChanged();
            }
        }


        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            var result = new DrawTextConverterItem(filePath);
            result.OutputMediaInfo.VedioAnalysis.Text = Text;
            result.OutputMediaInfo.VedioAnalysis.UseDrawText = true;
            result.OutputMediaInfo.VedioAnalysis.FontSize = FontSize;
            result.OutputMediaInfo.VedioAnalysis.Box = Box;
            result.OutputMediaInfo.VedioAnalysis.Boxborderw = Boxborderw;
            result.OutputMediaInfo.VedioAnalysis.Boxcolor = Boxcolor;
            result.OutputMediaInfo.VedioAnalysis.Fontcolor = Fontcolor;
            result.OutputMediaInfo.VedioAnalysis.X = X;
            result.OutputMediaInfo.VedioAnalysis.Y = Y;
            return result;
        }
    }
}
