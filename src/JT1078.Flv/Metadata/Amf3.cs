using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv.Metadata
{
    public class Amf3
    {
        /// <summary>
        /// AMF3数据类型
        /// </summary>
        public byte DataType { get; set; } = 0x08;
        /// <summary>
        /// 元素个数
        /// </summary>
        public uint Count { get; set; }
        public List<IAmf3Metadata> Amf3Metadatas { get; set; }
    }
}
