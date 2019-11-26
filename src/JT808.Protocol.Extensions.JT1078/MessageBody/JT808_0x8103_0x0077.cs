using JT808.Protocol.Formatters;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessagePack;
using System.Collections.Generic;
using System.Linq;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    ///单独视频通道参数设置
    /// 0x8103_0x0077
    /// </summary>
    public class JT808_0x8103_0x0077 : JT808_0x8103_BodyBase, IJT808MessagePackFormatter<JT808_0x8103_0x0077>
    {
        public override uint ParamId { get; set; } = 0x0077;
        /// <summary>
        /// 数据 长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 需单独设置视频参数的通道数量 用n表示
        /// </summary>
        public byte NeedSetChannelTotal { get; set; }

        public List<JT808_0x8103_0x0077_SignalChannel> SignalChannels { get; set; }

        public JT808_0x8103_0x0077 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0077 jT808_0X8103_0X0077 = new JT808_0x8103_0x0077();
            jT808_0X8103_0X0077.ParamId = reader.ReadUInt32();
            jT808_0X8103_0X0077.ParamLength = reader.ReadByte();
            jT808_0X8103_0X0077.NeedSetChannelTotal = reader.ReadByte();
            if (jT808_0X8103_0X0077.NeedSetChannelTotal > 0)
            {
                jT808_0X8103_0X0077.SignalChannels = new List<JT808_0x8103_0x0077_SignalChannel>();
                var formatter = config.GetMessagePackFormatter<JT808_0x8103_0x0077_SignalChannel>();
                for (int i = 0; i < jT808_0X8103_0X0077.NeedSetChannelTotal; i++)
                {
                    jT808_0X8103_0X0077.SignalChannels.Add(formatter.Deserialize(ref reader, config));
                }
            }
            return jT808_0X8103_0X0077;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0077 value, IJT808Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out var position);
            writer.WriteByte(value.NeedSetChannelTotal);
            if (value.SignalChannels.Any())
            {
                var formatter = config.GetMessagePackFormatter<JT808_0x8103_0x0077_SignalChannel>();
                foreach (var signalChannel in value.SignalChannels)
                {
                    formatter.Serialize(ref writer, signalChannel, config);
                }
            }
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - position - 1), position);
        }
    }
}
