using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x8103_0x0077_SignalChannel_Formatter : IJT808MessagePackFormatter<JT808_0x8103_0x0077_SignalChannel>
    {
        public JT808_0x8103_0x0077_SignalChannel Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0077_SignalChannel jT808_0X8103_0X0077_SignalChannel = new JT808_0x8103_0x0077_SignalChannel();
            jT808_0X8103_0X0077_SignalChannel.LogicChannelNo= reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.RTS_EncodeMode = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.RTS_Resolution = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.RTS_KF_Interval = reader.ReadUInt16();
            jT808_0X8103_0X0077_SignalChannel.RTS_Target_FPS = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.RTS_Target_CodeRate = reader.ReadUInt32();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_EncodeMode = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_Resolution = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_KF_Interval = reader.ReadUInt16();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_Target_FPS = reader.ReadByte();
            jT808_0X8103_0X0077_SignalChannel.StreamStore_Target_CodeRate = reader.ReadUInt32();
            jT808_0X8103_0X0077_SignalChannel.OSD = reader.ReadUInt16();
            return jT808_0X8103_0X0077_SignalChannel;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x8103_0x0077_SignalChannel value, IJT808Config config)
        {
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteByte(value.RTS_EncodeMode);
            writer.WriteByte(value.RTS_Resolution);
            writer.WriteUInt16(value.RTS_KF_Interval);
            writer.WriteByte(value.RTS_Target_FPS);
            writer.WriteUInt32(value.RTS_Target_CodeRate);
            writer.WriteByte(value.StreamStore_EncodeMode);
            writer.WriteByte(value.StreamStore_Resolution);
            writer.WriteUInt16(value.StreamStore_KF_Interval);
            writer.WriteByte(value.StreamStore_Target_FPS);
            writer.WriteUInt32(value.StreamStore_Target_CodeRate);
            writer.WriteUInt16(value.OSD);
        }
    }
}
