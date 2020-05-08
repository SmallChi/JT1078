using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 主动请求停止实时音视频传输消息
    /// </summary>
    public class JT809_JT1078_0x9800_0x9802 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9800_0x9802>
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 音视频类型
        /// </summary>
        public byte AVitemType { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.主动请求停止实时音视频传输消息.ToUInt16Value();

        public override string Description { get; } = "主动请求停止实时音视频传输消息";

        public JT809_JT1078_0x9800_0x9802 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9800_0x9802 jT808_JT1078_0x9800_0x9802 = new JT809_JT1078_0x9800_0x9802();
            jT808_JT1078_0x9800_0x9802.ChannelId = reader.ReadByte();
            jT808_JT1078_0x9800_0x9802.AVitemType = reader.ReadByte();
            return jT808_JT1078_0x9800_0x9802;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9800_0x9802 value, IJT809Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteByte(value.AVitemType);
        }
    }
}