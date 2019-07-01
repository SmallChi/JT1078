using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x9207_Formatter : IJT808MessagePackFormatter<JT808_0x9207>
    {
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
