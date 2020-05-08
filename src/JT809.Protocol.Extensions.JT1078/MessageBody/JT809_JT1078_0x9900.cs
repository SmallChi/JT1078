using JT809.Protocol.Enums;
using JT809.Protocol.Exceptions;
using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 从链路远程录像检索交互消息
    /// </summary>
    public class JT809_JT1078_0x9900 : JT809ExchangeMessageBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9900>
    {
        public override ushort MsgId { get; } = JT809_JT1078_BusinessType.从链路远程录像检索业务类.ToUInt16Value();

        public override JT809_LinkType LinkType { get; } = JT809_LinkType.subordinate;

        public override string Description { get; } = "从链路远程录像检索交互消息";

        public JT809_JT1078_0x9900 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9900 value = new JT809_JT1078_0x9900();
            value.VehicleNo = reader.ReadString(21);
            value.VehicleColor = (JT809VehicleColorType)reader.ReadByte();
            value.SubBusinessType = reader.ReadUInt16();
            value.DataLength = reader.ReadUInt32();
            try
            {
                if (config.SubBusinessTypeFactory.TryGetValue(value.SubBusinessType, out object instance))
                {
                    if (instance is JT809SubBodies subBodies)
                    {
                        if (!subBodies.SkipSerialization)
                        {
                            value.SubBodies = JT809MessagePackFormatterResolverExtensions.JT809DynamicDeserialize(
                                instance,
                                ref reader, config);
                        }
                    }
                }
            }
            catch
            {
                throw new JT809Exception(JT809ErrorCode.SubBodiesParseError, $"SubBusinessType>{value.SubBusinessType.ToString()}");
            }
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9900 value, IJT809Config config)
        {
            writer.WriteStringPadRight(value.VehicleNo, 21);
            writer.WriteByte((byte)value.VehicleColor);
            writer.WriteUInt16(value.SubBusinessType);
            try
            {
                // 先写入内容，然后在根据内容反写内容长度
                writer.Skip(4, out int subContentLengthPosition);
                if (value.SubBodies != null)
                {
                    if (!value.SubBodies.SkipSerialization)
                    {
                        JT809MessagePackFormatterResolverExtensions.JT809DynamicSerialize(
                                   value.SubBodies,
                                   ref writer, value.SubBodies,
                                   config);
                    }
                }
                writer.WriteInt32Return(writer.GetCurrentPosition() - subContentLengthPosition - 4, subContentLengthPosition);
            }
            catch
            {
                throw new JT809Exception(JT809ErrorCode.SubBodiesParseError, $"SubBusinessType>{value.SubBusinessType.ToString()}");
            }
        }
    }
}
