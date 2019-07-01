using JT809.Protocol.Attributes;
using JT809.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 远程录像下载通知消息
    /// </summary>
    [JT809Formatter(typeof(JT809_JT1078_0x1B00_0x1B02_Formatter))]
    public class JT809_JT1078_0x1B00_0x1B02 : JT809SubBodies
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
    }
}