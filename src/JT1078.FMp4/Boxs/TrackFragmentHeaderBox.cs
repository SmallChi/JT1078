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
        public TrackFragmentHeaderBox(uint flags = 0) : this(0, flags)
        {
        }

        public uint TrackID { get; set; }

        #region  all the following are optional fields
        /// <summary>
        /// TFHD_FLAG_BASE_DATA_OFFSET
        /// </summary>
        public ulong BaseDataOffset { get; set; }
        /// <summary>
        /// TFHD_FLAG_SAMPLE_DESCRIPTION_INDEX
        /// </summary>
        public uint SampleDescriptionIndex { get; set; }
        /// <summary>
        /// TFHD_FLAG_DEFAULT_DURATION
        /// </summary>
        public uint DefaultSampleDuration { get; set; }
        /// <summary>
        /// TFHD_FLAG_DEFAULT_SIZE
        /// H.264 NALU SIZE
        /// </summary>
        public uint DefaultSampleSize { get; set; }
        /// <summary>
        /// TFHD_FLAG_DEFAULT_FLAGS
        /// MOV_AUDIO == handler_type ? TFHD_FLAG_AUDIO_TPYE : TFHD_FLAG_VIDEO_TPYE;
        /// </summary>
        public uint DefaultSampleFlags { get; set; }
        #endregion
        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterFullBoxToBuffer(ref writer);
            writer.WriteUInt32(TrackID);
            if ((FMp4Constants.TFHD_FLAG_BASE_DATA_OFFSET & Flags) > 0)
            {
                if (BaseDataOffset > 0)
                {
                    writer.WriteUInt64(BaseDataOffset);
                }
                else
                {
                    //程序自动计算
                    writer.CreateMoofOffsetPosition();
                    writer.Skip(8, out _);
                }
            }
            if ((FMp4Constants.TFHD_FLAG_SAMPLE_DESCRIPTION_INDEX & Flags) > 0)
            {
                writer.WriteUInt32(SampleDescriptionIndex);
            }
            if ((FMp4Constants.TFHD_FLAG_DEFAULT_DURATION & Flags) > 0)
            {
                writer.WriteUInt32(DefaultSampleDuration);
            }
            if ((FMp4Constants.TFHD_FLAG_DEFAULT_SIZE & Flags) > 0)
            {
                writer.WriteUInt32(DefaultSampleSize);
            }
            if ((FMp4Constants.TFHD_FLAG_DEFAULT_FLAGS & Flags) > 0)
            {
                writer.WriteUInt32(DefaultSampleFlags);
            }
            End(ref writer);
        }
    }
}
