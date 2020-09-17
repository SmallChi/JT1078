using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class StereoVideoBox : FullBox
    {
        public StereoVideoBox(byte version=0, uint flags=0) : base("stvi", version, flags)
        {
        }
        /// <summary>
        ///4btye 32 - 30
        /// </summary>
        public uint Reserved { get; set; }
        /// <summary>
        ///4btye 32 - 2
        /// </summary>
        public byte SingleViewAllowed { get; set; }
        public uint StereoScheme { get; set; }
        public uint Length { get; set; }
        /// <summary>
        /// length:Length
        /// </summary>
        public byte[] StereoIndicationType { get; set; }
        public List<Mp4Box> AnyBox { get; set; }
    }
}
