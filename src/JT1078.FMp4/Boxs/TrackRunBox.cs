using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// trun
    /// </summary>
    public class TrackRunBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// trun
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public TrackRunBox(byte version, uint flags) : base("trun", version, flags)
        {
        }

        public uint SampleCount { get; set; }
        /// <summary>
        /// 可选的
        /// </summary>
        public int DataOffset { get; set; }
        /// <summary>
        /// 可选的
        /// </summary>
        public uint FirstSampleFlags { get; set; }
        /// <summary>
        /// 可选的
        /// length:SampleCount
        /// </summary>
        public List<TrackRunInfo> TrackRunInfos { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(SampleCount);
            End(ref writer);
        }

        public class TrackRunInfo
        {
            public uint SampleDuration { get; set; }
            public uint SampleSize { get; set; }
            public uint SampleFlags { get; set; }
            /// <summary>
            /// version == 0
            /// </summary>
            public uint SampleCompositionTimeOffset { get; set; }
            public int SignedSampleCompositionTimeOffset { get; set; }
        }       
    }
}
