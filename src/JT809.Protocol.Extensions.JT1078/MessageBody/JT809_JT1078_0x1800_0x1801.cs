using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 实时音视频请求应答消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x1800_0x1801_Formatter))]
    public class JT809_JT1078_0x1800_0x1801 : JT809SubBodies
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public byte Result { get; set; }
        /// <summary>
        /// 企业视频服务器ip地址
        /// 32
        /// </summary>
        public string ServerIp { get; set; }
        /// <summary>
        /// 企业视频服务器端口号
        /// </summary>
        public ushort ServerPort { get; set; }
    }
}