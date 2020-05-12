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
    /// 远程录像下载请求应答消息
    /// </summary>
    public class JT809_JT1078_0x1B00_0x1B01 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x1B00_0x1B01>, IJT809Analyze
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public JT809_JT1078_0x1B00_0x1B01_Result Result { get; set; }
        /// <summary>
        /// 对应平台文件上传消息的流水号
        /// </summary>
        public ushort SessionId { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.远程录像下载请求应答消息.ToUInt16Value();

        public override string Description { get; } = "远程录像下载请求应答消息";

        public void Analyze(ref JT809MessagePackReader reader, Utf8JsonWriter writer, IJT809Config config)
        {
            JT809_JT1078_0x1B00_0x1B01 value = new JT809_JT1078_0x1B00_0x1B01();
            value.Result = (JT809_JT1078_0x1B00_0x1B01_Result)reader.ReadByte();
            writer.WriteString($"[{((byte)value.Result).ReadNumber() }]应答结果", value.Result.ToString());
            value.SessionId = reader.ReadUInt16();
            writer.WriteNumber($"[{value.SessionId.ReadNumber()}]对应平台文件上传消息的流水号", value.SessionId);
        }

        public JT809_JT1078_0x1B00_0x1B01 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1B00_0x1B01 value = new JT809_JT1078_0x1B00_0x1B01();
            value.Result = (JT809_JT1078_0x1B00_0x1B01_Result)reader.ReadByte();
            value.SessionId = reader.ReadUInt16();
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1B00_0x1B01 value, IJT809Config config)
        {
            writer.WriteByte((byte)value.Result);
            writer.WriteUInt16(value.SessionId);
        }
    }
}