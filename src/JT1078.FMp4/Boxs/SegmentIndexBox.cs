using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SegmentIndexBox : FullBox
    {
        public SegmentIndexBox(byte version, uint flags=0) : base("sidx", version, flags)
        {
        }

        public uint ReferenceID { get; set; }
        public string Timescale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ulong EarliestPresentationTimeLarge { get; set; }
        public ulong FirstOffsetLarge { get; set; }
        /// <summary>
        /// if(version==0)
        /// </summary>
        public uint EarliestPresentationTime { get; set; }
        /// <summary>
        /// if (version==0)
        /// </summary>
        public uint FirstOffset { get; set; }
        public ushort Reserved { get; set; }
        public ushort ReferenceCount { get; set; }

        public List<SegmentIndex> SegmentIndexs { get; set; }

        public class SegmentIndex
        {
            /// <summary>
            /// 4byte 32 - 1
            /// </summary>
            public bool ReferenceType { get; set; }
            /// <summary>
            /// 4byte 32 - 31
            /// </summary>
            public uint ReferencedSize { get; set; }
            public uint SubsegmentDuration { get; set; }
            /// <summary>
            /// 4byte 32 - 1
            /// </summary>
            public bool StartsWithSAP { get; set; }
            /// <summary>
            /// 4byte 32 - 3
            /// </summary>
            public byte SAPType { get; set; }
            /// <summary>
            /// 4byte 32 - 28
            /// </summary>
            public uint SAPDeltaTime { get; set; }
        }
    }
}
