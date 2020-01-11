using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传控制
    /// </summary>
    public class JT808_0x9207 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9207>
    {
        public override string Description => "文件上传控制";
        public override ushort MsgId => 0x9207;
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MgsNum { get; set; }
        /// <summary>
        /// 上传控制
        /// </summary>
        public byte UploadControl { get; set; }
        public JT808_0x9207 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9207 jT808_0x9207 = new JT808_0x9207();
            jT808_0x9207.MgsNum = reader.ReadUInt16();
            jT808_0x9207.UploadControl = reader.ReadByte();
            return jT808_0x9207;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9207 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MgsNum);
            writer.WriteByte(value.UploadControl);
        }
    }
}
