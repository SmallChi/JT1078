using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackFragmentHeaderBox : FullBox
    {
        public TrackFragmentHeaderBox(byte version, uint flags) : base("tfhd", version, flags)
        {
        }
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
    }
}
