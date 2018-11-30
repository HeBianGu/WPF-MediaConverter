using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.FFmpeg.UserControls
{
    internal static class Previder
    {
        public static string GetLength(this double lengthOfDocument)
        {
            if (lengthOfDocument < 1024)
                return string.Format(Math.Round(lengthOfDocument, 2).ToString() + 'B');

            else if (lengthOfDocument > 1024 && lengthOfDocument <= Math.Pow(1024, 2))
                return string.Format(Math.Round((lengthOfDocument / 1024.0), 2).ToString() + "KB");

            else if (lengthOfDocument > Math.Pow(1024, 2) && lengthOfDocument <= Math.Pow(1024, 3))
                return string.Format(Math.Round((lengthOfDocument / 1024.0 / 1024.0), 2).ToString() + "M");

            else
                return string.Format(Math.Round((lengthOfDocument / 1024.0 / 1024.0 / 1024.0), 2).ToString() + "GB");
        }

        public static string GetLength(this string filePath)
        {
            if (!File.Exists(filePath)) return null;

            FileInfo info = new FileInfo(filePath);

            double lengthOfDocument = (double)info.Length;

            if (lengthOfDocument < 1024)
                return string.Format(Math.Round(lengthOfDocument, 2).ToString() + 'B');

            else if (lengthOfDocument > 1024 && lengthOfDocument <= Math.Pow(1024, 2))
                return string.Format(Math.Round((lengthOfDocument / 1024.0), 2).ToString() + "KB");

            else if (lengthOfDocument > Math.Pow(1024, 2) && lengthOfDocument <= Math.Pow(1024, 3))
                return string.Format(Math.Round((lengthOfDocument / 1024.0 / 1024.0), 2).ToString() + "M");

            else
                return string.Format(Math.Round((lengthOfDocument / 1024.0 / 1024.0 / 1024.0), 2).ToString() + "GB");
        }
    }
}
