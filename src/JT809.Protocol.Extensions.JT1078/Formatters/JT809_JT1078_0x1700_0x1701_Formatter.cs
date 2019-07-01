using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x1700_0x1701_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x1700_0x1701>
    {
        public JT809_JT1078_0x1700_0x1701 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1700_0x1701 jT808_JT1078_0X1701 = new JT809_JT1078_0x1700_0x1701();
            jT808_JT1078_0X1701.PlateFormId = reader.ReadArray(11).ToArray();
            jT808_JT1078_0X1701.AuthorizeCode1 = reader.ReadArray(64).ToArray();
            jT808_JT1078_0X1701.AuthorizeCode2 = reader.ReadArray(64).ToArray();
            return jT808_JT1078_0X1701;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1700_0x1701 value, IJT809Config config)
        {
            writer.WriteArray(value.PlateFormId);
            writer.WriteArray(value.AuthorizeCode1);
            writer.WriteArray(value.AuthorizeCode2);
        }
    }
}