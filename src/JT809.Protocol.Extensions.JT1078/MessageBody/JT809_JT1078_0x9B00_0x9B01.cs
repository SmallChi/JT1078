using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像下载请求消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x9B00_0x9B01_Formatter))]
    public class JT809_JT1078_0x9B00_0x9B01 : JT809SubBodies
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 终止时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 报警类型
        /// </summary>
        public UInt64 AlarmType { get; set; }
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
        /// 文件大小 byte
        /// </summary>
        public uint FileSize { get; set; }
        /// <summary>
        /// 时效口令
        /// 64
        /// </summary>
        public byte[] AuthorizeCode { get; set; }
        /// <summary>
        /// 车辆进入跨域地区后5min之内任一位置，仅跨域访问请求时，使用此字段
        /// 36
        /// </summary>
        public byte[] GnssData { get; set; }
    }
}