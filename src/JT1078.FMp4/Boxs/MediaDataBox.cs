using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MediaDataBox : Mp4Box
    {
        public MediaDataBox() : base("mdat")
        {
        }

        public byte[] Data { get; set; }
    }
}
