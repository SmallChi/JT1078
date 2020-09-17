using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleTableBox : Mp4Box
    {
        public SampleTableBox() : base("stbl")
        {
        }
        public SampleDescriptionBox SampleDescriptionBox { get; set; }
        public TimeToSampleBox TimeToSampleBox { get; set; }
        public CompositionOffsetBox CompositionOffsetBox { get; set; }
        public SampleToChunkBox SampleToChunkBox { get; set; }
        public SampleSizeBox SampleSizeBox { get; set; }
        public CompactSampleSizeBox CompactSampleSizeBox { get; set; }
        public ChunkOffsetBox ChunkOffsetBox { get; set; }
        public ChunkLargeOffsetBox ChunkLargeOffsetBox { get; set; }
        public SyncSampleBox SyncSampleBox { get; set; }
        public ShadowSyncSampleBox ShadowSyncSampleBox { get; set; }
        public PaddingBitsBox PaddingBitsBox { get; set; }
        public DegradationPriorityBox DegradationPriorityBox { get; set; }
        public SampleDependencyTypeBox SampleDependencyTypeBox { get; set; }
        public SampleToGroupBox SampleToGroupBox { get; set; }
        public SampleGroupDescriptionBox SampleGroupDescriptionBox { get; set; }
        public SubSampleInformationBox SubSampleInformationBox { get; set; }
    }
}
