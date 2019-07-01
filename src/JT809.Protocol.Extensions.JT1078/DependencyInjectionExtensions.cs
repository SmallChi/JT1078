using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Extensions.JT1078.MessageBody;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078
{
    public static class DependencyInjectionExtensions
    {
        public static IJT809Builder AddJT1078Configure(this IJT809Builder iJT809Builder)
        {
            iJT809Builder.Config.Register(Assembly.GetExecutingAssembly());
#warning 不知道是不是jt1078协议的809结构有问题，先按交换的格式（少了数据交换的头部）
            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x1700>((ushort)JT809_JT1078_BusinessType.主链路时效口令业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1700_0x1701>((ushort)JT809_JT1078_SubBusinessType.时效口令上报消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1700_0x1702>((ushort)JT809_JT1078_SubBusinessType.时效口令请求消息);

            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x1800>((ushort)JT809_JT1078_BusinessType.主链路实时音视频业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1800_0x1801>((ushort)JT809_JT1078_SubBusinessType.实时音视频请求应答消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1800_0x1802>((ushort)JT809_JT1078_SubBusinessType.主动请求停止实时音视频传输应答消息);

            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x1900>((ushort)JT809_JT1078_BusinessType.主链路远程录像检索业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1900_0x1901>((ushort)JT809_JT1078_SubBusinessType.主动上传音视频资源目录信息消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1900_0x1902>((ushort)JT809_JT1078_SubBusinessType.查询音视频资源目录应答消息);

            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x1A00>((ushort)JT809_JT1078_BusinessType.主链路远程录像回放业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1A00_0x1A01>((ushort)JT809_JT1078_SubBusinessType.远程录像回放请求应答消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1A00_0x1A02>((ushort)JT809_JT1078_SubBusinessType.远程录像回放控制应答消息);

            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x1B00>((ushort)JT809_JT1078_BusinessType.主链路远程录像下载业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1B00_0x1B01>((ushort)JT809_JT1078_SubBusinessType.远程录像下载请求应答消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1B00_0x1B02>((ushort)JT809_JT1078_SubBusinessType.远程录像下载通知消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x1B00_0x1B03>((ushort)JT809_JT1078_SubBusinessType.远程录像下载控制应答消息);

            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x9700>((ushort)JT809_JT1078_BusinessType.从链路时效口令业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9700_0x9702>((ushort)JT809_JT1078_SubBusinessType.时效口令请求应答消息);

            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x9800>((ushort)JT809_JT1078_BusinessType.从链路实时音视频业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9800_0x9801>((ushort)JT809_JT1078_SubBusinessType.实时音视频请求消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9800_0x9802>((ushort)JT809_JT1078_SubBusinessType.主动请求停止实时音视频传输消息);

            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x9900>((ushort)JT809_JT1078_BusinessType.从链路远程录像检索业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9900_0x9901>((ushort)JT809_JT1078_SubBusinessType.主动上传音视频资源目录信息应答消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9900_0x9902>((ushort)JT809_JT1078_SubBusinessType.查询音视频资源目录请求消息);

            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x9A00>((ushort)JT809_JT1078_BusinessType.从链路远程录像回放业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9A00_0x9A01>((ushort)JT809_JT1078_SubBusinessType.远程录像回放请求消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9A00_0x9A02>((ushort)JT809_JT1078_SubBusinessType.远程录像回放控制消息);

            iJT809Builder.Config.BusinessTypeFactory.SetMap<JT809_JT1078_0x9B00>((ushort)JT809_JT1078_BusinessType.从链路远程录像下载业务类);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9B00_0x9B01>((ushort)JT809_JT1078_SubBusinessType.远程录像下载请求消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9B00_0x9B02>((ushort)JT809_JT1078_SubBusinessType.远程录像下载完成通知应答消息);
            iJT809Builder.Config.SubBusinessTypeFactory.SetMap<JT809_JT1078_0x9B00_0x9B03>((ushort)JT809_JT1078_SubBusinessType.远程录像下载控制消息);
            return iJT809Builder;
        }
    }
}
