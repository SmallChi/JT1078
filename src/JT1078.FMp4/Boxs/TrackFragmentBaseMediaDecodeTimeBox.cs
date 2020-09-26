using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// tfdt
    /// </summary>
    public class TrackFragmentBaseMediaDecodeTimeBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// tfdt
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public TrackFragmentBaseMediaDecodeTimeBox(byte version, uint flags=0) : base("tfdt", version, flags)
        {
        }
        public uint BaseMediaDecodeTime { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(BaseMediaDecodeTime);
            End(ref writer);
        }
    }
}
