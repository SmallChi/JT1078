using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 时效口令上报消息
    /// </summary>
    public class JT809_JT1078_0x1700_0x1701 : JT809SubBodies,IJT809MessagePackFormatter<JT809_JT1078_0x1700_0x1701>
    {
        /// <summary>
        /// 企业视频监控平台唯一编码，平台所属企业行政区域代码+平台公共编号
        /// </summary>
        public byte[] PlateFormId { get; set; }
        /// <summary>
        /// 归属地区政府平台使用的时效口令
        /// </summary>
        public byte[] AuthorizeCode1 { get; set; }
        /// <summary>
        /// 跨域地区政府平台使用的时效口令
        /// </summary>
        public byte[] AuthorizeCode2 { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.时效口令上报消息.ToUInt16Value();

        public override string Description { get; }= "时效口令上报消息";

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