using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.Interfaces;
using JT809.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像回放控制消息
    /// </summary>
    public class JT809_JT1078_0x9A00_0x9A02 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9A00_0x9A02>, IJT809Analyze
    {
        /// <summary>
        /// 控制类型
        /// </summary>
        public ControlType ControlType { get; set; }
        /// <summary>
        /// 快进或倒退倍数
        /// </summary>
        public FastTime FastTime { get; set; }
        /// <summary>
        /// 拖动位置
        /// </summary>
        public DateTime DateTime { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.远程录像回放控制消息.ToUInt16Value();

        public override string Description { get; } = "远程录像回放控制消息";

        public void Analyze(ref JT809MessagePackReader reader, Utf8JsonWriter writer, IJT809Config config)
        {
            JT809_JT1078_0x9A00_0x9A02 value = new JT809_JT1078_0x9A00_0x9A02();
            value.ControlType = (ControlType)reader.ReadByte();
            writer.WriteString($"[{((byte)value.ControlType).ReadNumber()}]控制类型", value.ControlType.ToString());
            value.FastTime = (FastTime)reader.ReadByte();
            writer.WriteString($"[{((byte)value.FastTime).ReadNumber()}]快进或倒退倍数", value.FastTime.ToString());
            var virtualHex = reader.ReadVirtualArray(8);
            value.DateTime = reader.ReadUTCDateTime();
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]拖动位置", value.DateTime);
        }

        public JT809_JT1078_0x9A00_0x9A02 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9A00_0x9A02 value = new JT809_JT1078_0x9A00_0x9A02();
            value.ControlType = (ControlType)reader.ReadByte();
            value.FastTime = (FastTime)reader.ReadByte();
            value.DateTime = reader.ReadUTCDateTime();
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9A00_0x9A02 value, IJT809Config config)
        {
            writer.WriteByte((byte)value.ControlType);
            writer.WriteByte((byte)value.FastTime);
            writer.WriteUTCDateTime(value.DateTime);
        }
    }
}