using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x8103_0x0076_AVChannelRefTable_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x0076_AVChannelRefTable>
    {
        public JT808_0x8103_0x0076_AVChannelRefTable Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0076_AVChannelRefTable jT808_0X8103_0X0076_AVChannelRefTable = new JT808_0x8103_0x0076_AVChannelRefTable();
            jT808_0X8103_0X0076_AVChannelRefTable.PhysicalChannelNo = reader.ReadByte();
            jT808_0X8103_0X0076_AVChannelRefTable.LogicChannelNo = reader.ReadByte();
            jT808_0X8103_0X0076_AVChannelRefTable.ChannelType = reader.ReadByte();
            jT808_0X8103_0X0076_AVChannelRefTable.IsConnectCloudPlat = reader.ReadByte();
            return jT808_0X8103_0X0076_AVChannelRefTable;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0076_AVChannelRefTable value, IJT808Config config)
        {
            writer.WriteByte(value.PhysicalChannelNo);
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.ChannelType);
            writer.WriteByte(value.IsConnectCloudPlat);
        }
    }
}
