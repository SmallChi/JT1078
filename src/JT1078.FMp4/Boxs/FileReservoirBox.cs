using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class FileReservoirBox : FullBox
    {
        public FileReservoirBox(byte version=0, uint flags=0) : base("fire", version, flags)
        {
        }
        public ushort EntryCount { get; set; }

        public List<FileReservoirInfo> FileReservoirInfos { get; set; }

        public class FileReservoirInfo
        {
            public ushort ItemID { get; set; }
            public uint SymbolCount { get; set; }
        }
    }
}
