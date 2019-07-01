using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像回放请求应答消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x1A00_0x1A01_Formatter))]
    public class JT809_JT1078_0x1A00_0x1A01 : JT809SubBodies
    {
        /// <summary>
        /// 企业视频服务器ip地址
        /// 32
        /// </summary>
        public string ServerIp { get; set; }
        /// <summary>
        /// 企业视频服务器端口号
        /// </summary>
        public ushort ServerPort { get; set; }
        /// <summary>
        /// 应答结果
        /// </summary>
        public byte Result { get; set; }
    }
}