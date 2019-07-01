using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 实时音视频请求消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x9800_0x9801_Formatter))]
    public class JT809_JT1078_0x9800_0x9801 : JT809SubBodies
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 音视频类型
        /// </summary>
        public byte AVitemType { get; set; }
        /// <summary>
        /// 时效口令
        /// </summary>
        public byte[] AuthorizeCode { get; set; }
        /// <summary>
        /// 车辆进入跨域地区后5min之内的任何位置，仅跨域访问请求时使用此字段
        /// </summary>
        public byte[] GnssData { get; set; }
    }
}