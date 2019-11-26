using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 云台变倍控制
    /// </summary>
    public class JT808_0x9306 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9306>
    {
        public override ushort MsgId => 0x9306;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 变倍控制
        /// </summary>
        public byte ChangeMultipleControl { get; set; }

        public JT808_0x9306 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9306 jT808_0x9306 = new JT808_0x9306();
            jT808_0x9306.LogicChannelNo = reader.ReadByte();
            jT808_0x9306.ChangeMultipleControl = reader.ReadByte();
            return jT808_0x9306;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9306 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.ChangeMultipleControl);
        }
    }
}
