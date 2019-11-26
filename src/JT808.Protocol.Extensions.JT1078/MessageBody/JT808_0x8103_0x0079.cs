using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 特殊报警录像参数设置
    /// 0x8103_0x0079
    /// </summary>
    public class JT808_0x8103_0x0079 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0079>
    {
        public override uint ParamId { get; set; } = 0x0079;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 3;
        /// <summary>
        /// 特殊报警录像存储阈值
        /// </summary>
        public byte StorageThresholds  { get; set; }
        /// <summary>
        /// 特殊报警录像持续时间
        /// </summary>
        public byte Duration { get; set; }
        /// <summary>
        /// 特殊报警标识起始时间
        /// 分钟min
        /// </summary>
        public byte BeginMinute { get; set; }

        public JT808_0x8103_0x0079 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0079 jT808_0x8103_0x0079 = new JT808_0x8103_0x0079();
            jT808_0x8103_0x0079.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x0079.ParamLength = reader.ReadByte();
            jT808_0x8103_0x0079.StorageThresholds = reader.ReadByte();
            jT808_0x8103_0x0079.Duration = reader.ReadByte();
            jT808_0x8103_0x0079.BeginMinute = reader.ReadByte();
            return jT808_0x8103_0x0079;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0079 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.StorageThresholds);
            writer.WriteByte(value.Duration);
            writer.WriteByte(value.BeginMinute);
        }
    }
}
