using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 查询终端音视频属性
    /// </summary>
    public class JT808_0x9003:JT808Bodies
    {
        public override ushort MsgId => 0x9003;
        public override bool SkipSerialization { get; set; } = true;
    }
}
