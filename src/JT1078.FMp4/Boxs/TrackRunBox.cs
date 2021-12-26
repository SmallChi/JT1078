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
        /// 201 205 1
        /// 201 0010 0000 0001
        /// 205 0010 0000 0101
        /// 1   0000 0000 0001
        /// tr_flags 是用来表示下列 sample 相关的标识符是否应用到每个字段中：
        /// 0x000001-0000 0000 0001: data-offset-present，只应用 data-offset
        /// 0x000004-0000 0000 0100: 只对第一个 sample 应用对应的 flags。剩余 sample flags 就不管了。
        /// 0x000100-0001 0000 0000: 这个比较重要，表示每个 sample 都有自己的 duration，否则使用默认的
        /// 0x000200-0010 0000 0000: 每个 sample 有自己的 sample_size，否则使用默认的。
        /// 0x000400-0100 0000 0000: 对每个 sample 使用自己的 flags。否则，使用默认的。
        /// 0x000800-1000 0000 0000: 每个 sample 都有自己的 cts 值
        /// 0x000f01-1111 0000 0001
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public TrackRunBox(byte version=0, uint flags= 0x000f01) : base("trun", version, flags)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public uint SampleCount { get; set; }
        /// <summary>
        /// 可选的
        /// 用来表示和该moof配套的mdat中实际数据内容距moof开头有多少byte
        /// 相当于就是 moof.byteLength + mdat.headerSize(8)
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
            if((Flags & FMp4Constants.TRUN_FLAG_DATA_OFFSET_PRESENT) == FMp4Constants.TRUN_FLAG_DATA_OFFSET_PRESENT)
            {
                if (DataOffset > 0)
                {
                    //人工
                    writer.WriteInt32(DataOffset);
                }
                else
                {
                    //程序自动计算
                    writer.CreateTrunOffsetPosition();
                    writer.Skip(4, out _);
                }
            }
            if ((Flags & FMp4Constants.TRUN_FLAG_FIRST_SAMPLE_FLAGS_PRESENT) == FMp4Constants.TRUN_FLAG_FIRST_SAMPLE_FLAGS_PRESENT)
            {
                writer.WriteUInt32(FirstSampleFlags);
            }
            if (tmpFlag)
            {
                foreach(var trun in TrackRunInfos)
                {
                    if ((Flags & FMp4Constants.TRUN_FLAG_SAMPLE_DURATION_PRESENT) == FMp4Constants.TRUN_FLAG_SAMPLE_DURATION_PRESENT)
                    {
                        writer.WriteUInt32(trun.SampleDuration);
                    }
                    if ((Flags & FMp4Constants.TRUN_FLAG_SAMPLE_SIZE_PRESENT) == FMp4Constants.TRUN_FLAG_SAMPLE_SIZE_PRESENT)
                    {
                        writer.WriteUInt32(trun.SampleSize);
                    }
                    if ((Flags & FMp4Constants.TRUN_FLAG_SAMPLE_FLAGS_PRESENT) == FMp4Constants.TRUN_FLAG_SAMPLE_FLAGS_PRESENT)
                    {
                        writer.WriteUInt32(trun.SampleFlags);
                    }
                    if ((Flags & FMp4Constants.TRUN_FLAG_SAMPLE_COMPOSITION_TIME_OFFSET_PRESENT) == FMp4Constants.TRUN_FLAG_SAMPLE_COMPOSITION_TIME_OFFSET_PRESENT)
                    {
                        if (Version == 0)
                        {
                            writer.WriteUInt32((uint)trun.SampleCompositionTimeOffset);
                        }
                        else
                        {
                            writer.WriteInt32((int)trun.SampleCompositionTimeOffset);
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
            /// cts
            /// version == 0
            ///  0:uint
            /// >0:int
            /// </summary>
            public long SampleCompositionTimeOffset { get; set; }
        }       
    }
}
