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
        public TrackFragmentBaseMediaDecodeTimeBox(byte version=0, uint flags=0) : base("tfdt", version, flags)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public ulong BaseMediaDecodeTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            if (Version == 1)
            {
                writer.WriteUInt64(BaseMediaDecodeTime);
            }
            else
            {
                writer.WriteUInt32((uint)BaseMediaDecodeTime);
            }
            End(ref writer);
        }
    }
}
