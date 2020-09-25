using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackExtendsBox : FullBox, IFMp4MessagePackFormatter
    {
        public TrackExtendsBox(byte version=0, uint flags=0) : base("trex", version, flags)
        {
        }

        public uint TrackID { get; set; }
        public uint DefaultSampleDescriptionIndex { get; set; }
        public uint DefaultSampleDuration { get; set; }
        public uint DefaultSampleSize { get; set; }
        public uint DefaultSampleFlags { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(TrackID);
            writer.WriteUInt32(DefaultSampleDescriptionIndex);
            writer.WriteUInt32(DefaultSampleDuration);
            writer.WriteUInt32(DefaultSampleSize);
            writer.WriteUInt32(DefaultSampleFlags);
            End(ref writer);
        }
    }
}
