using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 红外补光控制
    /// </summary>
    public class JT808_0x9305 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9305>
    {
        public override ushort MsgId => 0x9305;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 启停标识
        /// </summary>
        public byte StartOrStop  { get; set; }
        public JT808_0x9305 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9305 jT808_0x9305 = new JT808_0x9305();
            jT808_0x9305.LogicChannelNo = reader.ReadByte();
            jT808_0x9305.StartOrStop = reader.ReadByte();
            return jT808_0x9305;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9305 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.StartOrStop);
        }
    }
}
