using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 云台旋转
    /// </summary>
    [JT808Formatter(typeof(JT808_0x9301_Formatter))]
    public class JT808_0x9301 : JT808Bodies
    {
          /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public byte Direction { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public byte Speed { get; set; }
    }
}
