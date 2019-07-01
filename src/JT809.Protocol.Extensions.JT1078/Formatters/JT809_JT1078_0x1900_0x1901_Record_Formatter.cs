using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x1900_0x1901_Record_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x1900_Record>
    {
        public JT809_JT1078_0x1900_Record Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1900_Record jT808_JT1078_0x1900_0x1901_Record = new JT809_JT1078_0x1900_Record();
            jT808_JT1078_0x1900_0x1901_Record.ChannelId = reader.ReadByte();
            jT808_JT1078_0x1900_0x1901_Record.StartTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x1900_0x1901_Record.EndTime = reader.ReadUTCDateTime();
            jT808_JT1078_0x1900_0x1901_Record.AlarmType = reader.ReadUInt64();
            jT808_JT1078_0x1900_0x1901_Record.AVItemType = reader.ReadByte();
            jT808_JT1078_0x1900_0x1901_Record.StreamType = reader.ReadByte();
            jT808_JT1078_0x1900_0x1901_Record.MemType = reader.ReadByte();
            jT808_JT1078_0x1900_0x1901_Record.FileSize = reader.ReadUInt32();
            return jT808_JT1078_0x1900_0x1901_Record;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1900_Record value, IJT809Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteUTCDateTime(value.StartTime);
            writer.WriteUTCDateTime(value.EndTime);
            writer.WriteUInt64(value.AlarmType);
            writer.WriteByte(value.AVItemType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemType);
            writer.WriteUInt32(value.FileSize);
        }
    }
}
