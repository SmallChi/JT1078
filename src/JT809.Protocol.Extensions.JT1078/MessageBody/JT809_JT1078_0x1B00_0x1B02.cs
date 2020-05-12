using System.Text.Json;
using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.Interfaces;
using JT809.Protocol.MessagePack;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像下载通知消息
    /// </summary>
    public class JT809_JT1078_0x1B00_0x1B02 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x1B00_0x1B02>, IJT809Analyze
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public JT809_JT1078_0x1B00_0x1B02_Result Result { get; set; }
        /// <summary>
        /// 对应平台文件上传消息的流水号
        /// </summary>
        public ushort SessionId { get; set; }
        /// <summary>
        /// FTP服务器ip地址
        /// </summary>
        public string ServerIp { get; set; }
        /// <summary>
        /// FTP服务器端口
        /// </summary>
        public ushort TcpPort { get; set; }
        /// <summary>
        /// FTP用户名
        /// 49
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// FTP密码
        /// 22
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 文件存储路径
        /// 200
        /// </summary>
        public string FilePath { get; set; }

        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.远程录像下载通知消息.ToUInt16Value();

        public override string Description { get; } = "远程录像下载通知消息";

        public void Analyze(ref JT809MessagePackReader reader, Utf8JsonWriter writer, IJT809Config config)
        {
            JT809_JT1078_0x1B00_0x1B02 value = new JT809_JT1078_0x1B00_0x1B02();
            value.Result = (JT809_JT1078_0x1B00_0x1B02_Result)reader.ReadByte();
            writer.WriteString($"[{((byte)value.Result).ReadNumber() }]应答结果", value.Result.ToString());
            value.SessionId = reader.ReadUInt16();
            writer.WriteNumber($"[{value.SessionId.ReadNumber()}]对应平台文件上传消息的流水号", value.SessionId);
            var virtualHex = reader.ReadVirtualArray(32);
            value.ServerIp = reader.ReadString(32);
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]FTP服务器ip地址", value.ServerIp);
            value.TcpPort = reader.ReadUInt16();
            writer.WriteNumber($"[{value.TcpPort.ReadNumber()}]FTP服务器端口", value.TcpPort);
            virtualHex = reader.ReadVirtualArray(49);
            value.UserName = reader.ReadString(49);
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]FTP用户名", value.UserName);
            virtualHex = reader.ReadVirtualArray(22);
            value.Password = reader.ReadString(22);
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]FTP密码", value.Password);
            virtualHex = reader.ReadVirtualArray(200);
            value.FilePath = reader.ReadString(200);
            writer.WriteString($"[{virtualHex.ToArray().ToHexString()}]文件存储路径", value.FilePath);
        }

        public JT809_JT1078_0x1B00_0x1B02 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1B00_0x1B02 value = new JT809_JT1078_0x1B00_0x1B02();
            value.Result = (JT809_JT1078_0x1B00_0x1B02_Result)reader.ReadByte();
            value.SessionId = reader.ReadUInt16();
            value.ServerIp = reader.ReadString(32);
            value.TcpPort = reader.ReadUInt16();
            value.UserName = reader.ReadString(49);
            value.Password = reader.ReadString(22);
            value.FilePath = reader.ReadString(200);
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1B00_0x1B02 value, IJT809Config config)
        {
            writer.WriteByte((byte)value.Result);
            writer.WriteUInt16(value.SessionId);
            writer.WriteStringPadLeft(value.ServerIp, 32);
            writer.WriteUInt16(value.TcpPort);
            writer.WriteStringPadLeft(value.UserName, 49);
            writer.WriteStringPadLeft(value.Password, 22);
            writer.WriteStringPadLeft(value.FilePath, 200);
        }
    }
}