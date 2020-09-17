using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MediaBox : Mp4Box
    {
        public MediaBox() : base("mdia")
        {
        }

        public MediaHeaderBox MediaHeaderBox { get; set; }
        public HandlerBox HandlerBox { get; set; }
        public MediaInformationBox MediaInformationBox { get; set; }
    }
}
