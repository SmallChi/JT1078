using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class FilePartitionBox : FullBox
    {
        public FilePartitionBox(byte version=0, uint flags=0) : base("fpar", version, flags)
        {
        }
        public ushort ItemID { get; set; }
        public ushort PacketPayloadSize { get; set; }
        public byte Reserved { get; set; }
        public byte FECEncodingID { get; set; }
        public ushort FECInstanceID { get; set; }
        public ushort MaxSourceBlockLength { get; set; }
        public ushort EncodingSymbolLength { get; set; }
        public ushort MaxNumberOfEncodingSymbols { get; set; }
        /// <summary>
        /// 以null结尾
        /// </summary>
        public string SchemeSpecificInfo { get; set; }
        public ushort EntryCount { get; set; }
        public List<FilePartitionInfo> FilePartitionInfos { get; set; }
        public class FilePartitionInfo
        {
            public ushort BlockCount { get; set; }
            public ushort BlockSize  { get; set; }
        }
    }
}
