using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x1900_0x1902_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x1900_0x1902>
    {
        public JT809_JT1078_0x1900_0x1902 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1900_0x1902 jT808_JT1078_0x1900_0x1902 = new JT809_JT1078_0x1900_0x1902();
            jT808_JT1078_0x1900_0x1902.Result = reader.ReadByte();
            jT808_JT1078_0x1900_0x1902.ItemNum = reader.ReadUInt32();
            if (jT808_JT1078_0x1900_0x1902.ItemNum > 0)
            {
                List<JT809_JT1078_0x1900_Record> jT808_JT1078_0x1900_0x1901_RecordList = new List<JT809_JT1078_0x1900_Record>();
                var formatter = config.GetMessagePackFormatter<JT809_JT1078_0x1900_Record>();
                for (int i = 0; i < jT808_JT1078_0x1900_0x1902.ItemNum; i++)
                {
                    var jT808_JT1078_0x1900_0x1901_Record = formatter.Deserialize(ref reader, config);
                    jT808_JT1078_0x1900_0x1901_RecordList.Add(jT808_JT1078_0x1900_0x1901_Record);
                }
                jT808_JT1078_0x1900_0x1902.ItemList = jT808_JT1078_0x1900_0x1901_RecordList;
            }
            return jT808_JT1078_0x1900_0x1902;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1900_0x1902 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
            writer.WriteUInt32(value.ItemNum);
            if (value.ItemList.Count > 0)
            {
                var formatter = config.GetMessagePackFormatter<JT809_JT1078_0x1900_Record>();
                foreach (var item in value.ItemList)
                {
                    formatter.Serialize(ref writer, item, config);
                }
            }
        }
    }
}