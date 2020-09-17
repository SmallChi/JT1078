using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class TrackFragmentRandomAccessBox : FullBox
    {
        public TrackFragmentRandomAccessBox(byte version, uint flags=0) : base("tfra", version, flags)
        {
        }
        public uint TrackID { get; set; }
        /// <summary>
        /// 4byte 32-26
        /// </summary>
        public uint Reserved { get; set; } = 26;
        /// <summary>
        /// 4byte 32-28
        /// </summary>
        public uint LengthSizeOfTrafNum { get; set; }
        /// <summary>
        /// 4byte 32-30
        /// </summary>
        public uint LengthSizeOfTrunNum { get; set; }
        /// <summary>
        /// 4byte 32-32
        /// </summary>
        public uint LengthSizeOfSampleNum { get; set; }
        public uint NumberOfEntry { get; set; }
        public List<TrackFragmentRandomAccessInfo> TrackFragmentRandomAccessInfos { get; set; }
        public class TrackFragmentRandomAccessInfo
        {
            public ulong Time { get; set; }
            public ulong MoofOffset { get; set; }
            public uint TrafNumber { get; set; }
            public uint TrunNumber { get; set; }
            public uint SampleNumber { get; set; }
        }
    }
}
