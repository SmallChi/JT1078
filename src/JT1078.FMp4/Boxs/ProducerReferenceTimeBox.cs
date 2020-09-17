using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ProducerReferenceTimeBox : FullBox
    {
        public ProducerReferenceTimeBox(byte version, uint flags) : base("prft", version, flags)
        {
        }
        public ushort ReferenceTrackID { get; set; }
        public uint NtpTimestamp { get; set; }
        /// <summary>
        /// if (version==0)
        /// </summary>
        public uint MediaTime { get; set; }
        public ulong MediaTimeLagre { get; set; }
    }
}
