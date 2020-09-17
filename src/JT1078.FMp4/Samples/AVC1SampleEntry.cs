using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public class AVC1SampleEntry : VisualSampleEntry
    {
        public AVC1SampleEntry() : base("avc1")
        {
        }
        public AVCConfigurationBox AVCConfigurationBox { get; set; }

        public MPEG4BitRateBox MPEG4BitRateBox { get; set; }
        //todo:public MPEG4ExtensionDescriptorsBox MPEG4BitRateBox { get; set; }
    }
}
