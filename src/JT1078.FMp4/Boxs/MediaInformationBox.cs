using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class MediaInformationBox : Mp4Box
    {
        public MediaInformationBox() : base("minf")
        {
        }
        public FullBox MediaHeaderBox { get; set; }
        public DataInformationBox DataInformationBox { get; set; }
        public SampleTableBox SampleTableBox { get; set; }
    }
}
