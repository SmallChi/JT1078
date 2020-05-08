using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 主动请求停止实时音视频传输应答消息
    /// </summary>
    public class JT809_JT1078_0x1800_0x1802 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x1800_0x1802>
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public byte Result { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.主动请求停止实时音视频传输应答消息.ToUInt16Value();

        public override string Description { get; } = "主动请求停止实时音视频传输应答消息";

        public JT809_JT1078_0x1800_0x1802 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1800_0x1802 jT808_JT1078_0x1800_0x1802 = new JT809_JT1078_0x1800_0x1802();
            jT808_JT1078_0x1800_0x1802.Result = reader.ReadByte();
            return jT808_JT1078_0x1800_0x1802;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1800_0x1802 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
        }
    }
}