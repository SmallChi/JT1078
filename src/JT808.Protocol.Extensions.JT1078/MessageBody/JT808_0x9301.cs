using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 云台旋转
    /// </summary>
    public class JT808_0x9301 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9301>
    {
        public override string Description => "云台旋转";
        public override ushort MsgId => 0x9301;
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

        public JT808_0x9301 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9301 jT808_0x9301 = new JT808_0x9301();
            jT808_0x9301.LogicChannelNo = reader.ReadByte();
            jT808_0x9301.Direction = reader.ReadByte();
            jT808_0x9301.Speed = reader.ReadByte();
            return jT808_0x9301;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9301 value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.Direction);
            writer.WriteByte(value.Speed);
        }
    }
}
