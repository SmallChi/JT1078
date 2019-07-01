using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x9101_Formatter : IJT808MessagePackFormatter<JT808_0x9101>
    {
        public JT808_0x9101 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9101 jT808_0X9101 = new JT808_0x9101();
            jT808_0X9101.ServerIPAddressLength = reader.ReadByte();
            jT808_0X9101.ServerIPAddress = reader.ReadString(jT808_0X9101.ServerIPAddressLength);
            jT808_0X9101.ServerVideoChannelTcpPort = reader.ReadUInt16();
            jT808_0X9101.ServerVideoChannelUdpPort = reader.ReadUInt16();
            jT808_0X9101.LogicalChannelNo = reader.ReadByte();
            jT808_0X9101.DataType = reader.ReadByte();
            jT808_0X9101.StreamType = reader.ReadByte();
            return jT808_0X9101;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9101 value, IJT808Config config)
        {
            writer.Skip(1, out int position);
            writer.WriteString(value.ServerIPAddress);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);
            writer.WriteUInt16(value.ServerVideoChannelTcpPort);
            writer.WriteUInt16(value.ServerVideoChannelUdpPort);
            writer.WriteByte(value.LogicalChannelNo);
            writer.WriteByte(value.DataType);
            writer.WriteByte(value.StreamType);
        }
    }
}
