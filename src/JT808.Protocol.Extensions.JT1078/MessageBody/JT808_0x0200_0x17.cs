using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 存储器故障报警状态
    /// 0x0200_0x17
    /// </summary>
    public class JT808_0x0200_0x17 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x17>
    {
        public override byte AttachInfoId { get; set; } = 0x17;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 2;
        /// <summary>
        /// 存储器故障报警状态
        /// </summary>
        public ushort StorageFaultAlarmStatus{ get; set; }

        public JT808_0x0200_0x17 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x17 jT808_0x0200_0x17 = new JT808_0x0200_0x17();
            jT808_0x0200_0x17.AttachInfoId = reader.ReadByte();
            jT808_0x0200_0x17.AttachInfoLength = reader.ReadByte();
            jT808_0x0200_0x17.StorageFaultAlarmStatus = reader.ReadUInt16();
            return jT808_0x0200_0x17;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x17 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.StorageFaultAlarmStatus);
        }
    }
}
