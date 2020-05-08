using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像下载控制消息
    /// </summary>
    public class JT809_JT1078_0x9B00_0x9B03 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x9B00_0x9B03>
    {
        /// <summary>
        /// 对应平台文件上传消息的流水号
        /// </summary>
        public ushort SessionId { get; set; }
        /// <summary>
        /// 控制类型
        /// </summary>
        public byte Type { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.远程录像下载控制消息.ToUInt16Value();

        public override string Description { get; } = "远程录像下载控制消息";

        public JT809_JT1078_0x9B00_0x9B03 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x9B00_0x9B03 jT808_JT1078_0x9B00_0x9B03 = new JT809_JT1078_0x9B00_0x9B03();
            jT808_JT1078_0x9B00_0x9B03.SessionId = reader.ReadUInt16();
            jT808_JT1078_0x9B00_0x9B03.Type = reader.ReadByte();
            return jT808_JT1078_0x9B00_0x9B03;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x9B00_0x9B03 value, IJT809Config config)
        {
            writer.WriteUInt16(value.SessionId);
            writer.WriteByte(value.Type);
        }
    }
}