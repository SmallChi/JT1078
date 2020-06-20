using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.Interfaces;
using JT809.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 主动上传音视频资源目录信息消息
    /// </summary>
    public class JT809_JT1078_0x1900_0x1901 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x1900_0x1901>, IJT809Analyze
    {
        /// <summary>
        /// 资源目录项数目
        /// </summary>
        public uint ItemNum { get; set; }
        /// <summary>
        /// 资源目录项列表
        /// </summary>
        public List<JT809_JT1078_0x1900_Record> ItemList { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.主动上传音视频资源目录信息消息.ToUInt16Value();

        public override string Description { get; } = "主动上传音视频资源目录信息消息";

        public void Analyze(ref JT809MessagePackReader reader, Utf8JsonWriter writer, IJT809Config config)
        {
            JT809_JT1078_0x1900_0x1901 value = new JT809_JT1078_0x1900_0x1901();
            value.ItemNum = reader.ReadUInt32();
            writer.WriteNumber($"[{value.ItemNum.ReadNumber() }]资源目录项数目", value.ItemNum);
            if (value.ItemNum > 0)
            {
                writer.WriteStartArray("资源目录项列表");
                var formatter = config.GetMessagePackFormatter<JT809_JT1078_0x1900_Record>();
                for (int i = 0; i < value.ItemNum; i++)
                {
                    writer.WriteStartObject();
                    formatter.Analyze(ref reader, writer, config);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
        }

        public JT809_JT1078_0x1900_0x1901 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1900_0x1901 value = new JT809_JT1078_0x1900_0x1901();
            value.ItemNum = reader.ReadUInt32();
            if (value.ItemNum > 0)
            {
                List<JT809_JT1078_0x1900_Record> jT808_JT1078_0x1900_0x1901_RecordList = new List<JT809_JT1078_0x1900_Record>();
                var formatter = config.GetMessagePackFormatter<JT809_JT1078_0x1900_Record>();
                for (int i = 0; i < value.ItemNum; i++)
                {
                    var jT808_JT1078_0x1900_0x1901_Record = formatter.Deserialize(ref reader, config);
                    jT808_JT1078_0x1900_0x1901_RecordList.Add(jT808_JT1078_0x1900_0x1901_Record);
                }
                value.ItemList = jT808_JT1078_0x1900_0x1901_RecordList;
            }
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1900_0x1901 value, IJT809Config config)
        {
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