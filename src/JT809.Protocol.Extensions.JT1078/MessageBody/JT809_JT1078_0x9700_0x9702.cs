using JT809.Protocol.Extensions.JT1078.Enums;

namespace JT809.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 时效口令请求应答消息
    /// </summary>
    public class JT809_JT1078_0x9700_0x9702 : JT809SubBodies
    {
        public override ushort SubMsgId { get; } = JT809_JT1078_SubBusinessType.时效口令请求应答消息.ToUInt16Value();

        public override string Description { get; } = "时效口令请求应答消息";

        public override bool SkipSerialization { get; set; } = true;
    }
}