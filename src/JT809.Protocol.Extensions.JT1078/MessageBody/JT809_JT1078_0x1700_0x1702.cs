using JT809.Protocol.Extensions.JT1078.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 时效口令请求消息
    /// </summary>
    public class JT809_JT1078_0x1700_0x1702 : JT809SubBodies
    {
        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.时效口令请求消息.ToUInt16Value();

        public override string Description => "时效口令请求消息";

        public override bool SkipSerialization { get; set; } = true;
    }
}