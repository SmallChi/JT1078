using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// tfhd
    /// </summary>
    public class TrackFragmentHeaderBox : FullBox, IFMp4MessagePackFormatter
    {
        /// <summary>
        /// tfhd
        /// </summary>
        /// <param name="version"></param>
        /// <param name="flags"></param>
        public TrackFragmentHeaderBox(byte version, uint flags) : base("tfhd", version, flags)
        {
        }
        /// <summary>
        /// tfhd
        /// </summary>
        /// <param name="flags"></param>
        public TrackFragmentHeaderBox(uint flags) : this(0, flags)
        {
        }

        public uint TrackID { get; set; }

        #region  all the following are optional fields
        public ulong BaseDataOffset { get; set; }
        public uint SampleDescriptionIndex { get; set; }
        public uint DefaultSampleDuration { get; set; }
        public uint DefaultSampleSize { get; set; }
        public uint DefaultSampleFlags { get; set; }
        #endregion
        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(TrackID);
            //todo:all the following are optional fields
            End(ref writer);
        }
    }
}
