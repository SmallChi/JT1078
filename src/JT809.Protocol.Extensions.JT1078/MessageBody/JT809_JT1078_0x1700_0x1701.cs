using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.Interfaces;
using JT809.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 时效口令上报消息
    /// </summary>
    public class JT809_JT1078_0x1700_0x1701 : JT809SubBodies,IJT809MessagePackFormatter<JT809_JT1078_0x1700_0x1701>, IJT809Analyze
    {
        /// <summary>
        /// 企业视频监控平台唯一编码，平台所属企业行政区域代码+平台公共编号
        /// </summary>
        public string PlateFormId { get; set; }
        /// <summary>
        /// 归属地区政府平台使用的时效口令
        /// </summary>
        public string AuthorizeCode1 { get; set; }
        /// <summary>
        /// 跨域地区政府平台使用的时效口令
        /// </summary>
        public string AuthorizeCode2 { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.时效口令上报消息.ToUInt16Value();

        public override string Description { get; }= "时效口令上报消息";

        public void Analyze(ref JT809MessagePackReader reader, Utf8JsonWriter writer, IJT809Config config)
        {
            JT809_JT1078_0x1700_0x1701 value = new JT809_JT1078_0x1700_0x1701();
            var virtualHex = reader.ReadVirtualArray(11);
            value.PlateFormId = reader.ReadString(11);
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]企业视频监控平台唯一编码",value.PlateFormId);
            virtualHex = reader.ReadVirtualArray(64);
            value.AuthorizeCode1 = reader.ReadString(64);
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]归属地区政府平台使用的时效口令", value.AuthorizeCode1);
            virtualHex = reader.ReadVirtualArray(64);
            value.AuthorizeCode2 = reader.ReadString(64);
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]跨域地区政府平台使用的时效口令", value.AuthorizeCode2);
        }

        public JT809_JT1078_0x1700_0x1701 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1700_0x1701 value = new JT809_JT1078_0x1700_0x1701();
            value.PlateFormId = reader.ReadString(11);
            value.AuthorizeCode1 = reader.ReadString(64);
            value.AuthorizeCode2 = reader.ReadString(64);
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1700_0x1701 value, IJT809Config config)
        {
            writer.WriteStringPadRight(value.PlateFormId,11);
            writer.WriteStringPadRight(value.AuthorizeCode1,64);
            writer.WriteStringPadRight(value.AuthorizeCode2,64);
        }
    }
}