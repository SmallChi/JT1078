using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackFragmentBox : Mp4Box
    {
        public TrackFragmentBox() : base("traf")
        {
        }
        public TrackFragmentHeaderBox TrackFragmentHeaderBox { get; set; }
        public SampleDependencyTypeBox SampleDependencyTypeBox { get; set; }
        public TrackRunBox TrackRunBox { get; set; }
        public TrackFragmentBaseMediaDecodeTimeBox TrackFragmentBaseMediaDecodeTimeBox { get; set; }
        public SampleToGroupBox SampleToGroupBox { get; set; }
        public SubSampleInformationBox SubSampleInformationBox { get; set; }
    }
}
