using JT808.Protocol.Attributes;
using JT808.Protocol.Extensions.JT1078.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传指令
    /// </summary>
    [JT808Formatter(typeof(JT808_0x9206_Formatter))]
    public class JT808_0x9206 : JT808Bodies
    {
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
    }
}
