using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class SampleAuxiliaryInformationSizesBox : FullBox
    {
        public SampleAuxiliaryInformationSizesBox(byte version=0, uint flags=0) : base("saiz", version, flags)
        {
        }
        /// <summary>
        /// if (flags & 1)
        /// </summary>
        public uint AuxInfoType { get; set; }
        /// <summary>
        /// if (flags & 1)
        /// </summary>
        public uint AuxInfoTypeParameter { get; set; }
        public byte DefaultSampleInfoSize { get; set; }
        public uint SampleCount { get; set; }
        /// <summary>
        /// default_sample_info_size==0
        ///  length:sample_count
        /// </summary>
        public byte[] SampleInfoSize { get; set; }
    }
}
