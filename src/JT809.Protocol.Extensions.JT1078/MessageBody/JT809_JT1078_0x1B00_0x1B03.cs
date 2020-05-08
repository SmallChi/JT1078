using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像下载控制应答消息
    /// </summary>
    public class JT809_JT1078_0x1B00_0x1B03 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x1B00_0x1B03>
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public byte Result { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.远程录像下载控制应答消息.ToUInt16Value();

        public override string Description { get; } = "远程录像下载控制应答消息";

        public JT809_JT1078_0x1B00_0x1B03 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1B00_0x1B03 jT808_JT1078_0x1B00_0x1B01 = new JT809_JT1078_0x1B00_0x1B03();
            jT808_JT1078_0x1B00_0x1B01.Result = reader.ReadByte();
            return jT808_JT1078_0x1B00_0x1B01;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1B00_0x1B03 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
        }
    }
}