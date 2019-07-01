using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x1003_Formatter : IJT808MessagePackFormatter<JT808_0x1003>
    {
        public JT808_0x1003 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1003 jT808_0x1003 = new JT808_0x1003();
            jT808_0x1003.EnterAudioEncoding = reader.ReadByte();
            jT808_0x1003.EnterAudioChannelsNumber = reader.ReadByte();
            jT808_0x1003.EnterAudioSampleRate = reader.ReadByte();
            jT808_0x1003.EnterAudioSampleDigits = reader.ReadByte();
            jT808_0x1003.AudioFrameLength = reader.ReadUInt16();
            jT808_0x1003.IsSupportedAudioOutput = reader.ReadByte();
            jT808_0x1003.VideoEncoding = reader.ReadByte();
            jT808_0x1003.TerminalSupportedMaxNumberOfAudioPhysicalChannels = reader.ReadByte();
            jT808_0x1003.TerminalSupportedMaxNumberOfVideoPhysicalChannels = reader.ReadByte();
            return jT808_0x1003;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x1003 value, IJT808Config config)
        {
            writer.WriteByte(value.EnterAudioEncoding);
            writer.WriteByte(value.EnterAudioChannelsNumber);
            writer.WriteByte(value.EnterAudioSampleRate);
            writer.WriteByte(value.EnterAudioSampleDigits);
            writer.WriteUInt16(value.AudioFrameLength);
            writer.WriteByte(value.IsSupportedAudioOutput);
            writer.WriteByte(value.VideoEncoding);
            writer.WriteByte(value.TerminalSupportedMaxNumberOfAudioPhysicalChannels);
            writer.WriteByte(value.TerminalSupportedMaxNumberOfVideoPhysicalChannels);
        }
    }
}
