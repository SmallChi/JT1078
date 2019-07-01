using JT809.Protocol.Extensions.JT1078.MessageBody;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.Formatters
{
    public class JT809_JT1078_0x9700_0x9702_Formatter : IJT809MessagePackFormatter<JT809_JT1078_0x9700_0x9702>
    {
        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9700_0x9702 value, IJT809Config config)
        {
    
        }

        public JT809_JT1078_0x9700_0x9702 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            return new JT809_JT1078_0x9700_0x9702();
        }
    }
}