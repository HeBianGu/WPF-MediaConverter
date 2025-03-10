﻿using HeBianGu.Base.WpfBase;
using HeBianGu.Domain.Converter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Domain.Converter
{
    [Displayer(Name = "提取音频", Icon = "\xe6ba", GroupName = "视频,音频", Order = 4, Description = "将视频中的音频提取出来")]
    public class ExtractAudioConverterGroup : ConverterGroupBase
    {
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
        protected override ConverterItemBase CreateConverterItem(string filePath)
        {
            return new ExtractAudioConverterItem(filePath) { UseCopyChannel = UseCopyChannel };
        }
    }
}
