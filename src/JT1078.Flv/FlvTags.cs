using JT1078.Flv.Enums;
using JT1078.Flv.Metadata;

namespace JT1078.Flv
{
    public class FlvTags
    {
        public TagType Type { get; set; }
        /// <summary>
        /// Tag Data部分大小
        /// 3个字节
        /// </summary>
        public int DataSize { get; set; }
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
        public VideoTags VideoTagsData { get; set; }
        /// <summary>
        /// 音频数据
        /// </summary>
        public AudioTags AudioTagsData { get; set; }
        /// <summary>
        /// 根据tag类型
        /// </summary>
        public Amf3 DataTagsData { get; set; }
    }
}