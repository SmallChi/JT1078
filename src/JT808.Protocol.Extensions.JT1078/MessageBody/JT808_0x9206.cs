using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传指令
    /// </summary>
    public class JT808_0x9206 : JT808Bodies,IJT808MessagePackFormatter<JT808_0x9206>
    {
        public override string Description => "文件上传指令";
        public override ushort MsgId => 0x9206;
        /// <summary>
        /// 服务器IP地址服务
        /// </summary>
        public byte ServerIpLength { get; set; }
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIp { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public ushort Port { get; set; }
        /// <summary>
        /// 用户名长度
        /// </summary>
        public byte UserNameLength { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码长度
        /// </summary>
        public byte PasswordLength { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 文件上传路径长度
        /// </summary>
        public byte FileUploadPathLength { get; set; }
        /// <summary>
        /// 文件上传路径
        /// </summary>
        public string FileUploadPath { get; set; }
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte LogicChannelNo { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 报警标志
        /// </summary>
        public uint AlarmFlag { get; set; }
        /// <summary>
        /// 音视频资源类型
        /// </summary>
        public byte AVResourceType { get; set; }
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte StreamType { get; set; }
        /// <summary>
        /// 存储位置
        /// </summary>
        public byte MemoryPositon { get; set; }
        /// <summary>
        /// 任务执行条件
        /// </summary>
        public byte TaskExcuteCondition { get; set; }

        public JT808_0x9206 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x9206 jT808_0x9206 = new JT808_0x9206();
            jT808_0x9206.ServerIpLength = reader.ReadByte();
            jT808_0x9206.ServerIp = reader.ReadString(jT808_0x9206.ServerIpLength);
            jT808_0x9206.Port = reader.ReadUInt16();
            jT808_0x9206.UserNameLength = reader.ReadByte();
            jT808_0x9206.UserName = reader.ReadString(jT808_0x9206.UserNameLength);
            jT808_0x9206.PasswordLength = reader.ReadByte();
            jT808_0x9206.Password = reader.ReadString(jT808_0x9206.PasswordLength);
            jT808_0x9206.FileUploadPathLength = reader.ReadByte();
            jT808_0x9206.FileUploadPath = reader.ReadString(jT808_0x9206.FileUploadPathLength);
            jT808_0x9206.LogicChannelNo = reader.ReadByte();
            jT808_0x9206.BeginTime = reader.ReadDateTime6();
            jT808_0x9206.EndTime = reader.ReadDateTime6();
            jT808_0x9206.AlarmFlag = reader.ReadUInt32();
            jT808_0x9206.AVResourceType = reader.ReadByte();
            jT808_0x9206.StreamType = reader.ReadByte();
            jT808_0x9206.MemoryPositon = reader.ReadByte();
            jT808_0x9206.TaskExcuteCondition = reader.ReadByte();
            return jT808_0x9206;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x9206 value, IJT808Config config)
        {
            writer.Skip(1, out int serverIpLengthposition);
            writer.WriteString(value.ServerIp);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - serverIpLengthposition - 1), serverIpLengthposition);
            writer.WriteUInt16(value.Port);
            writer.Skip(1, out int userNameLengthposition);
            writer.WriteString(value.UserName);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - userNameLengthposition - 1), userNameLengthposition);
            writer.Skip(1, out int passwordLengthLengthposition);
            writer.WriteString(value.Password);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - passwordLengthLengthposition - 1), passwordLengthLengthposition);
            writer.Skip(1, out int fileUploadPathLengthLengthposition);
            writer.WriteString(value.FileUploadPath);
            writer.WriteByteReturn((byte)(writer.GetCurrentPosition() - fileUploadPathLengthLengthposition - 1), fileUploadPathLengthLengthposition);
            writer.WriteByte(value.LogicChannelNo);
            writer.WriteDateTime6(value.BeginTime);
            writer.WriteDateTime6(value.EndTime);
            writer.WriteUInt32(value.AlarmFlag);
            writer.WriteByte(value.AVResourceType);
            writer.WriteByte(value.StreamType);
            writer.WriteByte(value.MemoryPositon);
            writer.WriteByte(value.TaskExcuteCondition);
        }
    }
}
