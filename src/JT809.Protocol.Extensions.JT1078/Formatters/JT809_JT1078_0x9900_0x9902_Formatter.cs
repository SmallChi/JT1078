using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x9900_0x9902_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x9900_0x9902>
    {
        public JT809_JT1078_0x9900_0x9902 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9900_0x9902 jT808_JT1078_0x9900_0x9902 = new JT809_JT1078_0x9900_0x9902();
            jT808_JT1078_0x9900_0x9902.ChannelId = reader.ReadByte();
#warning 此处时间8个字节，暂使用utc时间代替
            jT808_JT1078_0x9900_0x9902.StartTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x9900_0x9902.EndTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x9900_0x9902.AlarmType = reader.ReadUInt64();
            jT808_JT1078_0x9900_0x9902.AVItemType = reader.ReadByte();
            jT808_JT1078_0x9900_0x9902.StreamType = reader.ReadByte();
            jT808_JT1078_0x9900_0x9902.MemType = reader.ReadByte();
            jT808_JT1078_0x9900_0x9902.AuthorizeCode = reader.ReadArray(64).ToArray();
            jT808_JT1078_0x9900_0x9902.GnssData = reader.ReadArray(36).ToArray();
            return jT808_JT1078_0x9900_0x9902;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9900_0x9902 value, IJT809Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteUTCDateTime(value.StartTime);
            writer.WriteUTCDateTime(value.EndTime);
            writer.WriteUInt64(value.AlarmType);
            writer.WriteByte(value.AVItemType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemType);
            writer.WriteArray(value.AuthorizeCode);
            writer.WriteArray(value.GnssData);
        }
    }
}
