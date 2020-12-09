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
        public TrackRunBox(byte version=0, uint flags= 0x000f01) : base("trun", version, flags)
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
            bool tmpFlag = TrackRunInfos != null && TrackRunInfos.Count > 0;
            if (tmpFlag)
            {
                writer.WriteUInt32((uint)TrackRunInfos.Count);
            }
            else
            {
                writer.WriteUInt32(0);
            }
            if((Flags & FMp4Constants.TRUN_FLAG_DATA_OFFSET_PRESENT) > 0)
            {
                writer.WriteInt32(DataOffset);
            }
            if ((Flags & FMp4Constants.TRUN_FLAG_FIRST_SAMPLE_FLAGS_PRESENT) > 0)
            {
                writer.WriteUInt32(FirstSampleFlags);
            }
            if (tmpFlag)
            {
                foreach(var trun in TrackRunInfos)
                {
                    if ((Flags & FMp4Constants.TRUN_FLAG_SAMPLE_DURATION_PRESENT) > 0)
                    {
                        writer.WriteUInt32(trun.SampleDuration);
                    }
                    if ((Flags & FMp4Constants.TRUN_FLAG_SAMPLE_SIZE_PRESENT) > 0)
                    {
                        writer.WriteUInt32(trun.SampleSize);
                    }
                    if ((Flags & FMp4Constants.TRUN_FLAG_SAMPLE_FLAGS_PRESENT) > 0)
                    {
                        writer.WriteUInt32(trun.SampleFlags);
                    }
                    if ((Flags & FMp4Constants.TRUN_FLAG_SAMPLE_COMPOSITION_TIME_OFFSET_PRESENT) > 0)
                    {
                        if (Version == 0)
                        {
                            writer.WriteUInt32(trun.SampleCompositionTimeOffset);
                        }
                        else
                        {
                            writer.WriteInt32(trun.SignedSampleCompositionTimeOffset);
                        }
                    }
                }
            }
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
