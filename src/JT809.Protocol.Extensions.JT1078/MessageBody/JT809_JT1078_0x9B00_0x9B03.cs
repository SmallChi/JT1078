using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.Interfaces;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像下载控制消息
    /// </summary>
    public class JT809_JT1078_0x9B00_0x9B03 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9B00_0x9B03>, IJT809Analyze
    {
        /// <summary>
        /// 对应平台文件上传消息的流水号
        /// </summary>
        public ushort SessionId { get; set; }
        /// <summary>
        /// 控制类型
        /// </summary>
        public JT809_JT1078_0x9B00_0x9B03_Type Type { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.远程录像下载控制消息.ToUInt16Value();

        public override string Description { get; } = "远程录像下载控制消息";

        public void Analyze(ref JT809MessagePackReader reader, Utf8JsonWriter writer, IJT809Config config)
        {
            JT809_JT1078_0x9B00_0x9B03 value = new JT809_JT1078_0x9B00_0x9B03();
            value.SessionId = reader.ReadUInt16();
            writer.WriteNumber($"[{ value.SessionId.ReadNumber()}]对应平台文件上传消息的流水号", value.SessionId);
            value.Type = (JT809_JT1078_0x9B00_0x9B03_Type)reader.ReadByte();
            writer.WriteString($"[{((byte)value.Type).ReadNumber()}]控制类型", value.Type.ToString());
        }

        public JT809_JT1078_0x9B00_0x9B03 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9B00_0x9B03 value = new JT809_JT1078_0x9B00_0x9B03();
            value.SessionId = reader.ReadUInt16();
            value.Type = (JT809_JT1078_0x9B00_0x9B03_Type)reader.ReadByte();
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9B00_0x9B03 value, IJT809Config config)
        {
            writer.WriteUInt16(value.SessionId);
            writer.WriteByte((byte)value.Type);
        }
    }
}