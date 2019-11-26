using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 异常驾驶行为报警详细描述
    /// 0x0200_0x18
    /// </summary>
    public class JT808_0x0200_0x18 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x18>
    {
        public override byte AttachInfoId { get; set; } = 0x18;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 2;
        /// <summary>
        /// 异常驾驶行为报警详细描述
        /// </summary>
        public ushort AbnormalDrivingBehaviorAlarmInfo { get; set; }
        public JT808_0x0200_0x18 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x18 jT808_0x0200_0x18 = new JT808_0x0200_0x18();
            jT808_0x0200_0x18.AttachInfoId = reader.ReadByte();
            jT808_0x0200_0x18.AttachInfoLength = reader.ReadByte();
            jT808_0x0200_0x18.AbnormalDrivingBehaviorAlarmInfo = reader.ReadUInt16();
            return jT808_0x0200_0x18;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x18 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.AbnormalDrivingBehaviorAlarmInfo);
        }
    }
}
