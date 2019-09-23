namespace JT1078.Flv
{
    public class FlvTag
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
        public uint Timestamp { get; set; } = 0;
        public byte TimestampExt { get; set; } = 0;
        /// <summary>
        /// stream id 总是0
        /// 3个字节
        /// </summary>
        public uint StreamId { get; set; } = 0;
        /// <summary>
        /// 根据tag类型
        /// </summary>
        public byte[] TagData { get; set; }
    }
}