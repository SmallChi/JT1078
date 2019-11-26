using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 图像分析报警参数设置
    /// 0x8103_0x007B
    /// </summary>
    public class JT808_0x8103_0x007B : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x007B>
    {
        public override uint ParamId { get; set; } = 0x007B;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 车辆核载人数
        /// </summary>
        public byte NuclearLoadNumber { get; set; }
        /// <summary>
        /// 疲劳程度阈值
        /// </summary>
        public byte FatigueThreshold  { get; set; }

        public JT808_0x8103_0x007B Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007B jT808_0x8103_0x007B = new JT808_0x8103_0x007B();
            jT808_0x8103_0x007B.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x007B.ParamLength = reader.ReadByte();
            jT808_0x8103_0x007B.NuclearLoadNumber = reader.ReadByte();
            jT808_0x8103_0x007B.FatigueThreshold = reader.ReadByte();
            return jT808_0x8103_0x007B;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007B value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.NuclearLoadNumber);
            writer.WriteByte(value.FatigueThreshold);
        }
    }
}
