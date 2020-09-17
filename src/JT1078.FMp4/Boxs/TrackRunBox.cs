using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackRunBox : FullBox
    {
        public TrackRunBox(byte version, uint flags) : base("trun", version, flags)
        {
        }

        public uint SampleCount { get; set; }
        /// <summary>
        /// 可选的
        /// </summary>
        public int DataOffset { get; set; }
        public uint FirstSampleFlags { get; set; }
        /// <summary>
        /// length:SampleCount
        /// </summary>
        public List<TrackRunInfo> TrackRunInfos { get; set; }
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
