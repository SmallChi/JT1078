using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 云台调整焦距控制
    /// </summary>
    [JT808Formatter(typeof(JT808_0x9302_Formatter))]
    public class JT808_0x9302 : JT808Bodies
    {
          /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 焦距调整方向
        /// </summary>
        public byte FocusAdjustmentDirection { get; set; }
    }
}
