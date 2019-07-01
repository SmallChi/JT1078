using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 实时音视频传输状态通知
    /// </summary>
    [JT808Formatter(typeof(JT808_0x9105_Formatter))]
    public class JT808_0x9105 : JT808Bodies
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 丢包率
        /// </summary>
        public byte DropRate  { get; set; }
    }
}
