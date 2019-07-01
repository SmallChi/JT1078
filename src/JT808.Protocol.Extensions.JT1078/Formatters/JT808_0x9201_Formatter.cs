using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x9201_Formatter : IJT808MessagePackFormatter<JT808_0x9201>
    {
        public JT808_0x9201 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9201 jT808_0x9201 = new JT808_0x9201();
            jT808_0x9201.ServerIpLength = reader.ReadByte();
            jT808_0x9201.ServerIp = reader.ReadString(jT808_0x9201.ServerIpLength);
            jT808_0x9201.TcpPort = reader.ReadUInt16();
            jT808_0x9201.UdpPort = reader.ReadUInt16();
            jT808_0x9201.LogicChannelNo = reader.ReadByte();
            jT808_0x9201.AVItemType = reader.ReadByte();
            jT808_0x9201.StreamType = reader.ReadByte();
            jT808_0x9201.MemType = reader.ReadByte();
            jT808_0x9201.PlayBackWay = reader.ReadByte();
            jT808_0x9201.FastForwardOrFastRewindMultiples1 = reader.ReadByte();
            jT808_0x9201.FastForwardOrFastRewindMultiples2 = reader.ReadByte();
            jT808_0x9201.BeginTime = reader.ReadDateTime6();
            jT808_0x9201.EndTime = reader.ReadDateTime6();
            return jT808_0x9201;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9201 value, IJT808Config config)
        {
            writer.Skip(1,out int position);
            writer.WriteString(value.ServerIp);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);//计算完字符串后，回写字符串长度
            writer.WriteUInt16(value.TcpPort);
            writer.WriteUInt16(value.UdpPort);
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.AVItemType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemType);
            writer.WriteByte(value.PlayBackWay);
            writer.WriteByte(value.FastForwardOrFastRewindMultiples1);
            writer.WriteByte(value.FastForwardOrFastRewindMultiples2);
            writer.WriteDateTime6(value.BeginTime);
            writer.WriteDateTime6(value.EndTime);
        }
    }
}
