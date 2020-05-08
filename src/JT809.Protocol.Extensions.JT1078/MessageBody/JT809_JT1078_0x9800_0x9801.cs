using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 实时音视频请求消息
    /// </summary>
    public class JT809_JT1078_0x9800_0x9801 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9800_0x9801>
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 音视频类型
        /// </summary>
        public byte AVitemType { get; set; }
        /// <summary>
        /// 时效口令
        /// </summary>
        public byte[] AuthorizeCode { get; set; }
        /// <summary>
        /// 车辆进入跨域地区后5min之内的任何位置，仅跨域访问请求时使用此字段
        /// </summary>
        public byte[] GnssData { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.实时音视频请求消息.ToUInt16Value();

        public override string Description { get; } = "实时音视频请求消息";

        public JT809_JT1078_0x9800_0x9801 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9800_0x9801 jT808_JT1078_0x9800_0x9801 = new JT809_JT1078_0x9800_0x9801();
            jT808_JT1078_0x9800_0x9801.ChannelId = reader.ReadByte();
            jT808_JT1078_0x9800_0x9801.AVitemType = reader.ReadByte();
            jT808_JT1078_0x9800_0x9801.AuthorizeCode = reader.ReadArray(64).ToArray();
            jT808_JT1078_0x9800_0x9801.GnssData = reader.ReadArray(36).ToArray();
            return jT808_JT1078_0x9800_0x9801;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9800_0x9801 value, IJT809Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteByte(value.AVitemType);
            writer.WriteArray(value.AuthorizeCode);
            writer.WriteArray(value.GnssData);
        }
    }
}