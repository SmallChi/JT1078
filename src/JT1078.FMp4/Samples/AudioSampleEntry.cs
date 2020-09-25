using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    public abstract class AudioSampleEntry : SampleEntry
    {
        public AudioSampleEntry(string codingname) : base(codingname)
        {
        }
        public uint[] Reserved1 { get; set; } = new uint[2];
        public ushort ChannelCount { get; set; } = 2;
        public ushort SampleSize { get; set; } = 16;
        public ushort PreDefined { get; set; } = 0;
        public ushort Reserved2 { get; set; } = 0;
        /// <summary>
        /// 
        ///  default samplerate of media << 16;
        /// </summary>
        public uint SampleRate{ get; set; }

        protected void WriterAudioSampleEntryToBuffer(ref FMp4MessagePackWriter writer)
        {
            foreach(var item in Reserved1)
            {
                writer.WriteUInt32(item);
            }
            writer.WriteUInt16(ChannelCount);
            writer.WriteUInt16(SampleSize);
            writer.WriteUInt16(PreDefined);
            writer.WriteUInt16(Reserved2);
            writer.WriteUInt32(SampleRate<<16);
        }
    }
}
