using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Formatters;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.Formatters
{
    public class JT808_0x9206_Formatter : IJT808MessagePackFormatter<JT808_0x9206>
    {
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
