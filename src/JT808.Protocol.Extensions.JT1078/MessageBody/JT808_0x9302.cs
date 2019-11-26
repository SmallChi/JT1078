using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 云台调整焦距控制
    /// </summary>
    public class JT808_0x9302 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9302>
    {
        public override ushort MsgId => 0x9302;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 焦距调整方向
        /// </summary>
        public byte FocusAdjustmentDirection { get; set; }

        public JT808_0x9302 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9302 jT808_0x9302 = new JT808_0x9302();
            jT808_0x9302.LogicChannelNo = reader.ReadByte();
            jT808_0x9302.FocusAdjustmentDirection = reader.ReadByte();
            return jT808_0x9302;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9302 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.FocusAdjustmentDirection);
        }
    }
}
