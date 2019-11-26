using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 视频相关报警
    /// 0x0200_0x14
    /// </summary>
    public class JT808_0x0200_0x14 : JT808_0x0200_BodyBase, IJT808MessagePackFormatter<JT808_0x0200_0x14>
    {
        public override byte AttachInfoId { get; set; } = 0x14;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 4;
        /// <summary>
        /// 视频相关报警
        /// </summary>
        public uint VideoRelateAlarm { get; set; }

        public JT808_0x0200_0x14 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x0200_0x14 jT808_0x0200_0x14 = new JT808_0x0200_0x14();
            jT808_0x0200_0x14.AttachInfoId = reader.ReadByte();
            jT808_0x0200_0x14.AttachInfoLength = reader.ReadByte();
            jT808_0x0200_0x14.VideoRelateAlarm = reader.ReadUInt32();
            return jT808_0x0200_0x14;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x0200_0x14 value, IJT808Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.VideoRelateAlarm);
        }
    }
}
