using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// tfra
    /// </summary>
    public class TrackFragmentRandomAccessBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// tfra
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public TrackFragmentRandomAccessBox(byte version, uint flags=0) : base("tfra", version, flags)
        {
        }
        public uint TrackID { get; set; }
        /// <summary>
        /// 26bit
        /// </summary>
        private uint Reserved { get; set; } = 0;
        /// <summary>
        /// 2bit 
        /// </summary>
        public byte LengthSizeOfTrafNum { get; set; }
        /// <summary>
        /// 2bit
        /// </summary>
        public byte LengthSizeOfTrunNum { get; set; }
        /// <summary>
        /// 2bit 
        /// </summary>
        public byte LengthSizeOfSampleNum { get; set; }
        public uint NumberOfEntry { get; set; }
        /// <summary>
        /// (moof+mdta)N
        /// </summary>
        public List<TrackFragmentRandomAccessInfo> TrackFragmentRandomAccessInfos { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(TrackID);
            writer.WriteUInt32((uint)(LengthSizeOfSampleNum | (LengthSizeOfTrunNum << 2) | (LengthSizeOfTrafNum << 4)));
            if(TrackFragmentRandomAccessInfos!=null && TrackFragmentRandomAccessInfos.Count > 0)
            {
                writer.WriteUInt32((uint)TrackFragmentRandomAccessInfos.Count);
                foreach (var item in TrackFragmentRandomAccessInfos)
                {
                    if (Version == 1) 
                    {
                        writer.WriteUInt64(item.Time);
                        writer.WriteUInt64(item.MoofOffset);
                    } 
                    else 
                    {
                        writer.WriteUInt32((uint)item.Time);
                        writer.WriteUInt32((uint)item.MoofOffset);
                    }
                    var length_size_of_traf_num = LengthSizeOfTrafNum + 1;
                    ControlSizeOf(ref writer, item.TrafNumber, length_size_of_traf_num);
                    var length_size_of_trun_num = LengthSizeOfTrunNum + 1;
                    ControlSizeOf(ref writer, item.TrunNumber, length_size_of_trun_num);
                    var length_size_of_sample_num = LengthSizeOfSampleNum + 1;
                    ControlSizeOf(ref writer, item.SampleNumber, length_size_of_sample_num);
                }
            }
            else
            {
                writer.WriteUInt32(0);
            }
            End(ref writer);
        }

        private void ControlSizeOf(ref FMp4MessagePackWriter writer,uint value,int length)
        {
            switch (length)
            {
                case 1:
                    writer.WriteByte((byte)value);
                    break;
                case 2:
                    writer.WriteUInt16((ushort)value);
                    break;
                case 3:
                    writer.WriteUInt24(value);
                    break;
                case 4:
                    writer.WriteUInt32(value);
                    break;
            }
        }

        public class TrackFragmentRandomAccessInfo
        {
            public ulong Time { get; set; }
            /// <summary>
            /// 需要定位到当前 moof offset
            /// </summary>
            public ulong MoofOffset { get; set; }
            public uint TrafNumber { get; set; }
            public uint TrunNumber { get; set; }
            public uint SampleNumber { get; set; }
        }
    }
}
