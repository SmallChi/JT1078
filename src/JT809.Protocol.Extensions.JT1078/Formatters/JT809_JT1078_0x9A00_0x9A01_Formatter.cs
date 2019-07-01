using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x9A00_0x9A01_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x9A00_0x9A01>
    {
        public JT809_JT1078_0x9A00_0x9A01 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9A00_0x9A01 jT808_JT1078_0x9A00_0x9A01 = new JT809_JT1078_0x9A00_0x9A01();
            jT808_JT1078_0x9A00_0x9A01.ChannelId = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A01.AVItemType = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A01.StreamType = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A01.MemType = reader.ReadByte();
            jT808_JT1078_0x9A00_0x9A01.PlayBackStartTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x9A00_0x9A01.PlayBackEndTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x9A00_0x9A01.AuthorizeCode = reader.ReadArray(64).ToArray();
            jT808_JT1078_0x9A00_0x9A01.GnssData = reader.ReadArray(36).ToArray();
            return jT808_JT1078_0x9A00_0x9A01;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9A00_0x9A01 value, IJT809Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteByte(value.AVItemType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemType);
            writer.WriteUTCDateTime(value.PlayBackStartTime);
            writer.WriteUTCDateTime(value.PlayBackEndTime);
            writer.WriteArray(value.AuthorizeCode);
            writer.WriteArray(value.GnssData);
        }
    }
}