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
        /// { default samplerate of media}<<16;
        /// </summary>
        public uint Samplerate{ get; set; }
    }
}
