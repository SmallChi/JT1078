using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 上传音视频资源目录项
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x1900_0x1901_Record_Formatter))]
    public class JT809_JT1078_0x1900_Record
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// UTC时间 开始
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// UTC时间 结束
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 报警标志物
        /// </summary>
        public  UInt64 AlarmType { get; set; }
        /// <summary>
        /// 音视频类型
        /// </summary>
        public byte AVItemType { get; set; }
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte StreamType { get; set; }
        /// <summary>
        /// 存储器类型
        /// </summary>
        public byte MemType { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public uint FileSize { get; set; }
    }
}
