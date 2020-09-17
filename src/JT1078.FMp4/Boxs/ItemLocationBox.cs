using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    public class ItemLocationBox : FullBox
    {
        public ItemLocationBox(byte version, uint flags=0) : base("iloc", version, flags)
        {
        }
        /// <summary>
        /// 0, 4, 8
        /// </summary>
        public byte OffsetSize { get; set; }
        /// <summary>
        /// 0, 4, 8
        /// </summary>
        public byte LengthSize { get; set; }
        /// <summary>
        /// 0, 4, 8
        /// </summary>
        public byte BaseOffsetSize { get; set; }
        /// <summary>
        /// version == 1
        /// </summary>
        public byte IndexSize{ get; set; }
        public byte Reserved { get; set; }
        public ushort ItemCount { get; set; }
        public List<ItemLocation> ItemLocations { get; set; }
        public class ItemLocation
        {
            public ushort ItemID { get; set; }
            /// <summary>
            /// if (version == 1) 16-12
            /// </summary>
            public byte Reserved { get; set; } = 12;
            /// <summary>
            /// if (version == 1) 16-4
            /// </summary>
            public byte ConstructionMethod { get; set; }
            public ushort DataReferenceIndex { get; set; }
            public ulong BaseOffset { get; set; }
            public ushort ExtentCount { get; set; }
            public List<ItemLocationExtentInfo> ItemLocationExtentInfos { get; set; }
            public class ItemLocationExtentInfo
            {
                /// <summary>
                /// if ((version == 1) && (index_size > 0))
                /// </summary>
                public ulong ExtentIndex { get; set; }
                public ulong ExtentOffset { get; set; }
                public ulong ExtentLength { get; set; }
            }
        }
    }
}
