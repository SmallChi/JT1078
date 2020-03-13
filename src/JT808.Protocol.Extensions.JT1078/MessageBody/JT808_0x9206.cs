using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传指令
    /// </summary>
    public class JT808_0x9206 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x9206>, IJT808Analyze
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

        public void Analyze(ref JT808MessagePackReader reader, Utf8JsonWriter writer, IJT808Config config)
        {
            JT808_0x9206 value = new JT808_0x9206();
            value.ServerIpLength = reader.ReadByte();
            writer.WriteNumber($"[{value.ServerIpLength.ReadNumber()}]服务器IP地址长度", value.ServerIpLength);
            string ipHex = reader.ReadVirtualArray(value.ServerIpLength).ToArray().ToHexString();
            value.ServerIp = reader.ReadString(value.ServerIpLength);
            writer.WriteString($"[{ipHex}]服务器IP地址", value.ServerIp);
            value.Port = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Port.ReadNumber()}]服务器端口", value.Port);
            value.UserNameLength = reader.ReadByte();
            writer.WriteNumber($"[{value.UserNameLength.ReadNumber()}]用户名长度", value.UserNameLength);
            string userNameHex = reader.ReadVirtualArray(value.UserNameLength).ToArray().ToHexString();
            value.UserName = reader.ReadString(value.UserNameLength);
            writer.WriteString($"[{userNameHex}]用户名", value.UserName);
            value.PasswordLength = reader.ReadByte();
            writer.WriteNumber($"[{value.PasswordLength.ReadNumber()}]密码长度", value.PasswordLength);
            string passwordHex = reader.ReadVirtualArray(value.PasswordLength).ToArray().ToHexString();
            value.Password = reader.ReadString(value.PasswordLength);
            writer.WriteString($"[{passwordHex}]密码", value.Password);
            value.FileUploadPathLength = reader.ReadByte();
            writer.WriteNumber($"[{value.FileUploadPathLength.ReadNumber()}]文件上传路径长度", value.FileUploadPathLength);
            string fileUploadPathHex = reader.ReadVirtualArray(value.FileUploadPathLength).ToArray().ToHexString();
            value.FileUploadPath = reader.ReadString(value.FileUploadPathLength);
            writer.WriteString($"[{fileUploadPathHex}]文件上传路径", value.FileUploadPath);

            value.LogicChannelNo = reader.ReadByte();
            writer.WriteString($"[{value.LogicChannelNo.ReadNumber()}]逻辑通道号", LogicalChannelNoDisplay(value.LogicChannelNo));
            value.BeginTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.BeginTime.ToString("yyMMddHHmmss")}]起始时间", value.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.EndTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.EndTime.ToString("yyMMddHHmmss")}]起始时间", value.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.AlarmFlag = reader.ReadUInt32();
            writer.WriteNumber($"[{value.AlarmFlag.ReadNumber()}]报警标志", value.AlarmFlag);
            value.AVResourceType = reader.ReadByte();
            writer.WriteString($"[{value.AVResourceType.ReadNumber()}]音视频类型", AVResourceTypeDisplay(value.AVResourceType));
            value.StreamType = reader.ReadByte();
            writer.WriteString($"[{value.StreamType.ReadNumber()}]码流类型", StreamTypeDisplay(value.StreamType));
            value.MemoryPositon = reader.ReadByte();
            writer.WriteString($"[{value.MemoryPositon.ReadNumber()}]存储器类型", MemoryPositonDisplay(value.MemoryPositon));
            value.TaskExcuteCondition = reader.ReadByte();
            writer.WriteString($"[{value.TaskExcuteCondition.ReadNumber()}]任务执行条件", TaskExcuteConditionDisplay(value.TaskExcuteCondition));

            string AVResourceTypeDisplay(byte AVResourceType)
            {
                switch (AVResourceType)
                {
                    case 0:
                        return "音视频";
                    case 1:
                        return "音频";
                    case 2:
                        return "视频";
                    case 3:
                        return "音频或视频";
                    default:
                        return "未知";
                }
            }
            string StreamTypeDisplay(byte StreamType)
            {
                switch (StreamType)
                {
                    case 0:
                        return "所有码流";
                    case 1:
                        return "主码流";
                    case 2:
                        return "子码流";
                    default:
                        return "未知";
                }
            }
            string MemoryPositonDisplay(byte MemoryPositon)
            {
                switch (MemoryPositon)
                {
                    case 0:
                        return "主存储器或灾备服务器";
                    case 1:
                        return "主存储器";
                    case 2:
                        return "灾备服务器";
                    default:
                        return "未知";
                }
            }
            string LogicalChannelNoDisplay(byte LogicalChannelNo)
            {
                switch (LogicalChannelNo)
                {
                    case 1:
                        return "驾驶员";
                    case 2:
                        return "车辆正前方";
                    case 3:
                        return "车前门";
                    case 4:
                        return "车厢前部";
                    case 5:
                        return "车厢后部";
                    case 7:
                        return "行李舱";
                    case 8:
                        return "车辆左侧";
                    case 9:
                        return "车辆右侧";
                    case 10:
                        return "车辆正后方";
                    case 11:
                        return "车厢中部";
                    case 12:
                        return "车中门";
                    case 13:
                        return "驾驶席车门";
                    case 33:
                        return "驾驶员";
                    case 36:
                        return "车厢前部";
                    case 37:
                        return "车厢后部";
                    default:
                        return "预留";
                }
            }
            string TaskExcuteConditionDisplay(byte TaskExcuteCondition) {
                string taskExcuteConditionDisplay = string.Empty;
                taskExcuteConditionDisplay += (TaskExcuteCondition & 0x01) == 1 ? ",WIFI":"";
                taskExcuteConditionDisplay += (TaskExcuteCondition & 0x01) == 1 ? ",LAN" : "";
                taskExcuteConditionDisplay += (TaskExcuteCondition & 0x01) == 1 ? ",3G/4G" : "";
                return taskExcuteConditionDisplay.Length > 0 ? taskExcuteConditionDisplay.Substring(1) : "";
            }
        }

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
