using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 视频相关报警屏蔽字
    /// 0x8103_0x007A
    /// </summary>
    public class JT808_0x8103_0x007A : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x007A>
    {
        public override uint ParamId { get; set; } = 0x007A;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 视频相关屏蔽报警字
        /// </summary>
        public uint AlarmShielding { get; set; }
        public JT808_0x8103_0x007A Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x007A jT808_0x8103_0x007A = new JT808_0x8103_0x007A();
            jT808_0x8103_0x007A.ParamId = reader.ReadUInt32();
            jT808_0x8103_0x007A.ParamLength = reader.ReadByte();
            jT808_0x8103_0x007A.AlarmShielding = reader.ReadUInt32();
            return jT808_0x8103_0x007A;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x007A value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.AlarmShielding);
        }
    }
}
