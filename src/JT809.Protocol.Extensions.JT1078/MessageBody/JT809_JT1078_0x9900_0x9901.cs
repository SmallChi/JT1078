using System.Text.Json;
using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.Interfaces;
using JT809.Protocol.MessagePack;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 主动上传音视频资源目录信息应答消息
    /// </summary>
    public class JT809_JT1078_0x9900_0x9901 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9900_0x9901>, IJT809Analyze
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public JT809_JT1078_0x9900_0x9901_Result Result { get; set; }
        /// <summary>
        /// 资源目录总数
        /// </summary>
        public byte ItemNumber { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.主动上传音视频资源目录信息应答消息.ToUInt16Value();

        public override string Description { get; } = "主动上传音视频资源目录信息应答消息";

        public void Analyze(ref JT809MessagePackReader reader, Utf8JsonWriter writer, IJT809Config config)
        {
            JT809_JT1078_0x9900_0x9901 value = new JT809_JT1078_0x9900_0x9901();
            value.Result = (JT809_JT1078_0x9900_0x9901_Result)reader.ReadByte();
            writer.WriteString($"[{  (byte)value.Result }]应答结果", value.Result.ToString());
            value.ItemNumber = reader.ReadByte();
            writer.WriteNumber($"[{value.ItemNumber.ReadNumber()}]资源目录总数", value.ItemNumber);
        }

        public JT809_JT1078_0x9900_0x9901 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9900_0x9901 value = new JT809_JT1078_0x9900_0x9901();
            value.Result = (JT809_JT1078_0x9900_0x9901_Result)reader.ReadByte();
            value.ItemNumber = reader.ReadByte();
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9900_0x9901 value, IJT809Config config)
        {
            writer.WriteByte((byte)value.Result);
            writer.WriteByte(value.ItemNumber);
        }
    }
}