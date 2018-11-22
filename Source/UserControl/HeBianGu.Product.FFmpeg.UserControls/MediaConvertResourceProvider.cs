﻿using HeBianGu.Product.FFmpeg.Base.Model;
using HeBianGu.Product.FFmpeg.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.UserControls
{
    class MediaConvertResourceProvider
    {
        public List<SupportFormatEntity> GetFormatSupportEntitys()
        {
            return FFmpegService.Instance.GetFormats();
        }

        public List<SupportFormatEntity> GetCodeSupportEntitys()
        {
            return FFmpegService.Instance.GetCodecs();
        }
    }
}
