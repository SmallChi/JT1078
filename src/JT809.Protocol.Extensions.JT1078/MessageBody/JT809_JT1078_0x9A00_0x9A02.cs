using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像回放控制消息
    /// </summary>
    public class JT809_JT1078_0x9A00_0x9A02 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9A00_0x9A02>
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

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.远程录像回放控制消息.ToUInt16Value();

        public override string Description { get; } = "远程录像回放控制消息";
        public JT809_JT1078_0x9A00_0x9A02 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9A00_0x9A02 jT808_JT1078_0x9A00_0x9A02 = new JT809_JT1078_0x9A00_0x9A02();
            jT808_JT1078_0x9A00_0x9A02.ControlType = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A02.FastTime = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A02.DateTime = reader.ReadUTCDateTime();
            return jT808_JT1078_0x9A00_0x9A02;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9A00_0x9A02 value, IJT809Config config)
        {
            writer.WriteByte(value.ControlType);
            writer.WriteByte(value.FastTime);
            writer.WriteUTCDateTime(value.DateTime);
        }
    }
}