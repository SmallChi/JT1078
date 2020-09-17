using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class AVCConfigurationBox : Mp4Box
    {
        public AVCConfigurationBox() : base("avcC")
        {
        }
        public AVCDecoderConfigurationRecord AVCDecoderConfigurationRecord { get; set; }
    }
}
