using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// stbl
    /// </summary>
    public class SampleTableBox : Mp4Box, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// stbl
        /// </summary>
        public SampleTableBox() : base("stbl")
        {
        }
        /// <summary>
        /// stsd
        /// </summary>
        public SampleDescriptionBox SampleDescriptionBox { get; set; }
        /// <summary>
        /// stts
        /// </summary>
        public TimeToSampleBox TimeToSampleBox { get; set; }
        /// <summary>
        /// stss
        /// </summary>
        public SyncSampleBox SyncSampleBox { get; set; }
        /// <summary>
        /// ctts
        /// </summary>
        public CompositionOffsetBox CompositionOffsetBox { get; set; }
        /// <summary>
        /// stsz
        /// </summary>
        public SampleSizeBox SampleSizeBox { get; set; }
        /// <summary>
        /// stsc
        /// </summary>
        public SampleToChunkBox SampleToChunkBox { get; set; }

        //public CompactSampleSizeBox CompactSampleSizeBox { get; set; }
        /// <summary>
        /// stco
        /// </summary>
        public ChunkOffsetBox ChunkOffsetBox { get; set; }
        //public ChunkLargeOffsetBox ChunkLargeOffsetBox { get; set; }
        //public ShadowSyncSampleBox ShadowSyncSampleBox { get; set; }
        //public PaddingBitsBox PaddingBitsBox { get; set; }
        //public DegradationPriorityBox DegradationPriorityBox { get; set; }
        //public SampleDependencyTypeBox SampleDependencyTypeBox { get; set; }
        //public SampleToGroupBox SampleToGroupBox { get; set; }
        //public SampleGroupDescriptionBox SampleGroupDescriptionBox { get; set; }
        //public SubSampleInformationBox SubSampleInformationBox { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            SampleDescriptionBox.ToBuffer(ref writer);
            TimeToSampleBox.ToBuffer(ref writer);
            if (SyncSampleBox != null)
            {
                SyncSampleBox.ToBuffer(ref writer);
            }
            if(CompositionOffsetBox!=null)
                CompositionOffsetBox.ToBuffer(ref writer);
            SampleToChunkBox.ToBuffer(ref writer);
            if (SampleSizeBox != null)
                SampleSizeBox.ToBuffer(ref writer);
            ChunkOffsetBox.ToBuffer(ref writer);
            End(ref writer);
        }
    }
}
