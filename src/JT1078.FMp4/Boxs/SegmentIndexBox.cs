using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// sidx
    /// </summary>
    public class SegmentIndexBox : FullBox,IFMp4MessagePackFormatter
    {
        /// <summary>
        /// sidx
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public SegmentIndexBox(byte version, uint flags=0) : base("sidx", version, flags)
        {
            ReferencedSizePositions = new List<int>();
        }
        /// <summary>
        /// 
        /// </summary>
        public uint ReferenceID { get; set; } = 1;
        /// <summary>
        /// 
        /// </summary>
        public uint Timescale { get; set; } = 1000;
        /// <summary>
        /// pts
        /// if(version==0) 
        /// version==0 32 bit
        /// version>0 64 bit
        /// </summary>
        public ulong EarliestPresentationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ulong FirstOffset { get; set; } = 0;
        private ushort Reserved { get; set; }
        //private ushort ReferenceCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SegmentIndex> SegmentIndexs { get; set; }

        public List<int> ReferencedSizePositions { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(ReferenceID);
            writer.WriteUInt32(Timescale);
            if (Version == 0)
            {
                writer.WriteUInt32((uint)EarliestPresentationTime);
                writer.WriteUInt32((uint)FirstOffset);
            }
            else
            {
                writer.WriteUInt64(EarliestPresentationTime);
                writer.WriteUInt64(FirstOffset);
            }
            writer.WriteUInt16(Reserved);
            if(SegmentIndexs!=null && SegmentIndexs.Count > 0)
            {
                writer.WriteUInt16((ushort)SegmentIndexs.Count);
                foreach(var si in SegmentIndexs)
                {
                    if (si.ReferenceType)
                    {
                        ReferencedSizePositions.Add(writer.GetCurrentPosition());
                        writer.WriteUInt32(si.ReferencedSize | 0x80000000);
                    }
                    else
                    {
                        ReferencedSizePositions.Add(writer.GetCurrentPosition());
                        writer.WriteUInt32(si.ReferencedSize);
                    }
                    writer.WriteUInt32(si.SubsegmentDuration);
                    if (si.StartsWithSAP)
                    {
                        writer.WriteUInt32((si.SAPDeltaTime | 0x80000000 | (uint)(si.SAPType << 28 & 0x70000000)));
                    }
                    else
                    {
                        writer.WriteUInt32((si.SAPDeltaTime | (uint)((si.SAPType <<28) & 0x70000000)));
                    }
                }
            }
            else
            {
                writer.WriteUInt16(0);
            }
            End(ref writer);
        }

        public class SegmentIndex
        {
            /// <summary>
            /// 4byte 32 - 1
            /// </summary>
            public bool ReferenceType { get; set; } = false;
            /// <summary>
            /// 4byte 32 - 31
            /// ReferencedSize=(moof size) + (mdat size)
            /// </summary>
            public uint ReferencedSize { get; set; } = 0;
            /// <summary>
            /// 
            /// </summary>
            public uint SubsegmentDuration { get; set; }
            /// <summary>
            /// 4byte 32 - 1
            /// </summary>
            public bool StartsWithSAP { get; set; } = true;
            /// <summary>
            /// 4byte 32 - 3
            /// </summary>
            public byte SAPType { get; set; } = 1;
            /// <summary>
            /// 4byte 32 - 28
            /// </summary>
            public uint SAPDeltaTime { get; set; } = 0;
        }
    }
}
