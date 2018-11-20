using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeBianGu.Product.FFmpeg.Driver;

namespace UnitTest.Driver
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string ss= FFmpegService.Instance.Formats();
        }

        [TestMethod]
        public void TestMethod2()
        {

            string filePath = @"F:\GitHub\WPF-MediaConverter\Document\from.mp4";


            //string ss = FFmpegService.Instance.GetDetail(filePath);


            FFmpegService.Instance.GetMediaEntity(filePath);



        }

       
    }
}
