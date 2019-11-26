using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 视频信号丢失报警状态
    /// 0x0200_0x15
    /// </summary>
    public class JT808_0x0200_0x15 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x15>
    {
        public override byte AttachInfoId { get; set; } = 0x15;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 视频信号丢失报警状态
        /// </summary>
        public uint VideoSignalLoseAlarmStatus { get; set; }

        public JT808_0x0200_0x15 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x15 jT808_0x0200_0x15 = new JT808_0x0200_0x15();
            jT808_0x0200_0x15.AttachInfoId = reader.ReadByte();
            jT808_0x0200_0x15.AttachInfoLength = reader.ReadByte();
            jT808_0x0200_0x15.VideoSignalLoseAlarmStatus = reader.ReadUInt32();
            return jT808_0x0200_0x15;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x15 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.VideoSignalLoseAlarmStatus);
        }
    }
}
