using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// mvhd
    /// </summary>
    public class MovieHeaderBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// mvhd
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public MovieHeaderBox(byte version, uint flags=0) : base("mvhd", version, flags)
        {
        }
        public ulong CreationTime { get; set; } 
        public ulong ModificationTime { get; set; }
        public uint Timescale { get; set;}
        public ulong Duration { get; set; }
        public int Rate { get; set; } = 0x00010000;
        public short Volume { get; set; } = 0x0100;
        public byte[] Reserved1 { get; set; } = new byte[2];
        public uint[] Reserved2 { get; set; } = new uint[2];
        public int[] Matrix { get; set; }=new int [9]{ 0x00010000, 0, 0, 0, 0x00010000, 0, 0, 0, 0x40000000 };
        public byte[] PreDefined { get; set; } = new byte[24];
        public uint NextTrackID { get; set; }= uint.MaxValue;
        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if (Version == 1)
            {
                writer.WriteUInt64(CreationTime);
                writer.WriteUInt64(ModificationTime);
                writer.WriteUInt32(Timescale);
                writer.WriteUInt64(Duration);
            }
            else
            {
                writer.WriteUInt32((uint)CreationTime);
                writer.WriteUInt32((uint)ModificationTime);
                writer.WriteUInt32(Timescale);
                writer.WriteUInt32((uint)Duration);
            }
            writer.WriteInt32(Rate);
            writer.WriteInt16(Volume);
            foreach(var r in Reserved1)
            {
                writer.WriteByte(r);
            }
            foreach (var r in Reserved2)
            {
                writer.WriteUInt32(r);
            }
            foreach(var m in Matrix)
            {
                writer.WriteInt32(m);
            }
            foreach (var pd in PreDefined)
            {
                writer.WriteByte(pd);
            }
            writer.WriteUInt32(NextTrackID);
            End(ref writer);
        }
    }
}
