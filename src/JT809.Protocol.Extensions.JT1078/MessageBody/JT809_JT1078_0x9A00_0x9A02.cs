using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像回放控制消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x9A00_0x9A02_Formatter))]
    public class JT809_JT1078_0x9A00_0x9A02 : JT809SubBodies
    {
        /// <summary>
        /// 控制类型
        /// </summary>
        public byte ControlType { get; set; }
        /// <summary>
        /// 快进或倒退倍数
        /// </summary>
        public byte FastTime { get; set; }
        /// <summary>
        /// 拖动位置
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}