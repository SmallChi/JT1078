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
    /// 远程录像回放请求应答消息
    /// </summary>
    public class JT809_JT1078_0x1A00_0x1A01 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x1A00_0x1A01>, IJT809Analyze
    {
        /// <summary>
        /// 企业视频服务器ip地址
        /// 32
        /// </summary>
        public string ServerIp { get; set; }
        /// <summary>
        /// 企业视频服务器端口号
        /// </summary>
        public ushort ServerPort { get; set; }
        /// <summary>
        /// 应答结果
        /// </summary>
        public JT809_JT1078_0x1A00_0x1A01_Result Result { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.远程录像回放请求应答消息.ToUInt16Value();

        public override string Description { get; } = "远程录像回放请求应答消息";

        public void Analyze(ref JT809MessagePackReader reader, Utf8JsonWriter writer, IJT809Config config)
        {
            JT809_JT1078_0x1A00_0x1A01 value = new JT809_JT1078_0x1A00_0x1A01();
            var virtualHex = reader.ReadVirtualArray(32);
            value.ServerIp = reader.ReadString(32);
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]企业视频服务器ip地址", value.ServerIp);
            value.ServerPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ServerPort.ReadNumber()}]企业视频服务器端口号", value.ServerPort);
            value.Result = (JT809_JT1078_0x1A00_0x1A01_Result)reader.ReadByte();
            writer.WriteString($"[{((byte)value.Result).ReadNumber()}]应答结果", value.Result.ToString());
        }

        public JT809_JT1078_0x1A00_0x1A01 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1A00_0x1A01 value = new JT809_JT1078_0x1A00_0x1A01();
            value.ServerIp = reader.ReadString(32);
            value.ServerPort = reader.ReadUInt16();
            value.Result = (JT809_JT1078_0x1A00_0x1A01_Result)reader.ReadByte();
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1A00_0x1A01 value, IJT809Config config)
        {
            writer.WriteStringPadLeft(value.ServerIp, 32);
            writer.WriteUInt16(value.ServerPort);
            writer.WriteByte((byte)value.Result);
        }
    }
}