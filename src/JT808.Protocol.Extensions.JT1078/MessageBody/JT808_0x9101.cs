using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 实时音视频传输请求
    /// </summary>
    [JT808Formatter(typeof(JT808_0x9101_Formatter))]
    public class JT808_0x9101:JT808Bodies
    {
        /// <summary>
        /// 服务器IP地址长度
        /// </summary>
        public byte ServerIPAddressLength { get; set; }
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIPAddress { get; set; }
        /// <summary>
        /// 服务器视频通道监听端口号(TCP)
        /// </summary>
        public ushort ServerVideoChannelTcpPort { get; set; }
        /// <summary>
        /// 服务器视频通道监听端口号（UDP）
        /// </summary>
        public ushort ServerVideoChannelUdpPort { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicalChannelNo { get; set; }
        /// <summary>
        /// 数据类型
        /// 0:音视频
        /// 1:视频
        /// 2:双向对讲
        /// 3:监听
        /// 4:中心广播
        /// 5:透传
        /// </summary>
        public byte DataType { get; set; }
        /// <summary>
        /// 码流类型
        /// 0:主码流
        /// 1:子码流
        /// </summary>
        public byte StreamType { get; set; }
    }
}
