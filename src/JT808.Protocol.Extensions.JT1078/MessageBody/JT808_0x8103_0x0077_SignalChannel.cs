using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    public class JT808_0x8103_0x0077_SignalChannel: IJT808MessagePackFormatter<JT808_0x8103_0x0077_SignalChannel>
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 实时流编码模式
        /// </summary>
        public byte RTS_EncodeMode { get; set; }
        /// <summary>
        /// 实时流分辨率
        /// </summary>
        public byte RTS_Resolution { get; set; }
        /// <summary>
        /// 实时流关键帧间隔
        /// （范围1-1000）帧
        /// </summary>
        public ushort RTS_KF_Interval { get; set; }
        /// <summary>
        /// 实时流目标帧率
        /// </summary>
        public byte RTS_Target_FPS { get; set; }
        /// <summary>
        /// 实时流目标码率
        /// 单位未千位每秒（kbps）
        /// </summary>
        public uint RTS_Target_CodeRate { get; set; }
        /// <summary>
        /// 存储流编码模式
        /// </summary>
        public byte StreamStore_EncodeMode { get; set; }
        /// <summary>
        /// 存储流分辨率
        /// </summary>
        public byte StreamStore_Resolution { get; set; }
        /// <summary>
        /// 存储流关键帧间隔
        /// （范围1-1000）帧
        /// </summary>
        public ushort StreamStore_KF_Interval { get; set; }
        /// <summary>
        /// 存储流目标帧率
        /// </summary>
        public byte StreamStore_Target_FPS { get; set; }
        /// <summary>
        /// 存储流目标码率
        /// 单位未千位每秒（kbps）
        /// </summary>
        public uint StreamStore_Target_CodeRate { get; set; }
        /// <summary>
        ///OSD字幕叠加设置
        /// </summary>
        public ushort OSD { get; set; }
        public JT808_0x8103_0x0077_SignalChannel Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x8103_0x0077_SignalChannel jT808_0X8103_0X0077_SignalChannel = new JT808_0x8103_0x0077_SignalChannel();
            jT808_0X8103_0X0077_SignalChannel.LogicChannelNo = reader.ReadByte();
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
