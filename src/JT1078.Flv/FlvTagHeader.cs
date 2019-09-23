using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv
{
    public class FlvTagHeader
    {
        public byte Type { get; set; }
        /// <summary>
        /// Tag Data部分大小
        /// 3个字节
        /// </summary>
        public uint DataSize { get; set; }
        /// <summary>
        /// Tag时间戳
        /// 3个字节
        /// </summary>
        public uint Timestamp { get; set; }
        public byte TimestampExt { get; set; } = 0;
        /// <summary>
        /// stream id 总是0
        /// 3个字节
        /// </summary>
        public uint StreamId { get; set; } = 0;
    }
}
