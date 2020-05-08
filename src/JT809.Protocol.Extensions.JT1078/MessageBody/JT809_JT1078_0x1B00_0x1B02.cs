using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像下载通知消息
    /// </summary>
    public class JT809_JT1078_0x1B00_0x1B02 : JT809SubBodies, IJT809MessagePackFormatter<JT809_JT1078_0x1B00_0x1B02>
    {
        /// <summary>
        /// 应答结果
        /// </summary>
        public byte Result { get; set; }
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

        public JT809_JT1078_0x1B00_0x1B02 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_JT1078_0x1B00_0x1B02 jT808_JT1078_0x1B00_0x1B02 = new JT809_JT1078_0x1B00_0x1B02();
            jT808_JT1078_0x1B00_0x1B02.Result = reader.ReadByte();
            jT808_JT1078_0x1B00_0x1B02.SessionId = reader.ReadUInt16();
            jT808_JT1078_0x1B00_0x1B02.ServerIp = reader.ReadString(32);
            jT808_JT1078_0x1B00_0x1B02.TcpPort = reader.ReadUInt16();
            jT808_JT1078_0x1B00_0x1B02.UserName = reader.ReadString(49);
            jT808_JT1078_0x1B00_0x1B02.Password = reader.ReadString(22);
            jT808_JT1078_0x1B00_0x1B02.FilePath = reader.ReadString(200);
            return jT808_JT1078_0x1B00_0x1B02;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_JT1078_0x1B00_0x1B02 value, IJT809Config config)
        {
            writer.WriteByte(value.Result);
            writer.WriteUInt16(value.SessionId);
            writer.WriteStringPadLeft(value.ServerIp, 32);
            writer.WriteUInt16(value.TcpPort);
            writer.WriteStringPadLeft(value.UserName, 49);
            writer.WriteStringPadLeft(value.Password, 22);
            writer.WriteStringPadLeft(value.FilePath, 200);
        }
    }
}