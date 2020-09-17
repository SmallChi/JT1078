using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class FECReservoirBox : FullBox
    {
        public FECReservoirBox(byte version=0, uint flags=0) : base("fecr", version, flags)
        {
        }

        public ushort EntryCount { get; set; }

        public List<FECReservoirInfo> FECReservoirInfos { get; set; }

        public class FECReservoirInfo
        {
            public ushort ItemID { get; set; }
            public uint SymbolCount { get; set; }
        }
    }
}
