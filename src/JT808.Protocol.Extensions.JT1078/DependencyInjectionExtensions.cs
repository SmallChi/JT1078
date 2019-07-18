using JT808.Protocol.Extensions.JT1078.Enums;
using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078
{
    public static class DependencyInjectionExtensions
    {
        public static IJT808Builder AddJT1078Configure(this IJT808Builder jT808Builder)
        {
            jT808Builder.Config.Register(Assembly.GetExecutingAssembly());
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x1003>((ushort)JT808_JT1078_MsgId.终端上传音视频属性, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x1005>((ushort)JT808_JT1078_MsgId.终端上传乘客流量, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x1205>((ushort)JT808_JT1078_MsgId.终端上传音视频资源列表, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x1206>((ushort)JT808_JT1078_MsgId.文件上传完成通知, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9003>((ushort)JT808_JT1078_MsgId.查询终端音视频属性, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9101>((ushort)JT808_JT1078_MsgId.实时音视频传输请求, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9102>((ushort)JT808_JT1078_MsgId.音视频实时传输控制, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9105>((ushort)JT808_JT1078_MsgId.实时音视频传输状态通知, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9201>((ushort)JT808_JT1078_MsgId.平台下发远程录像回放请求, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9202>((ushort)JT808_JT1078_MsgId.平台下发远程录像回放控制, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9205>((ushort)JT808_JT1078_MsgId.查询资源列表, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9206>((ushort)JT808_JT1078_MsgId.文件上传指令, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9207>((ushort)JT808_JT1078_MsgId.文件上传控制, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9301>((ushort)JT808_JT1078_MsgId.云台旋转, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9302>((ushort)JT808_JT1078_MsgId.云台调整焦距控制, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9303>((ushort)JT808_JT1078_MsgId.云台调整光圈控制, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9304>((ushort)JT808_JT1078_MsgId.云台雨刷控制, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9305>((ushort)JT808_JT1078_MsgId.红外补光控制, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9306>((ushort)JT808_JT1078_MsgId.云台变倍控制, "");
            jT808Builder.Config.JT808_0X0200_Factory.JT808LocationAttachMethod.Add(JT808_JT1078_Constants.JT808_0X0200_0x14, typeof(JT808_0x0200_0x14));
            jT808Builder.Config.JT808_0X0200_Factory.JT808LocationAttachMethod.Add(JT808_JT1078_Constants.JT808_0X0200_0x15, typeof(JT808_0x0200_0x15));
            jT808Builder.Config.JT808_0X0200_Factory.JT808LocationAttachMethod.Add(JT808_JT1078_Constants.JT808_0X0200_0x16, typeof(JT808_0x0200_0x16));
            jT808Builder.Config.JT808_0X0200_Factory.JT808LocationAttachMethod.Add(JT808_JT1078_Constants.JT808_0X0200_0x17, typeof(JT808_0x0200_0x17));
            jT808Builder.Config.JT808_0X0200_Factory.JT808LocationAttachMethod.Add(JT808_JT1078_Constants.JT808_0X0200_0x18, typeof(JT808_0x0200_0x18));
            jT808Builder.Config.JT808_0X8103_Factory.ParamMethods.Add(JT808_JT1078_Constants.JT808_0X8103_0x0075, typeof(JT808_0x8103_0x0075));
            jT808Builder.Config.JT808_0X8103_Factory.ParamMethods.Add(JT808_JT1078_Constants.JT808_0X8103_0x0076, typeof(JT808_0x8103_0x0076));
            jT808Builder.Config.JT808_0X8103_Factory.ParamMethods.Add(JT808_JT1078_Constants.JT808_0X8103_0x0077, typeof(JT808_0x8103_0x0077));
            jT808Builder.Config.JT808_0X8103_Factory.ParamMethods.Add(JT808_JT1078_Constants.JT808_0X8103_0x0079, typeof(JT808_0x8103_0x0079));
            jT808Builder.Config.JT808_0X8103_Factory.ParamMethods.Add(JT808_JT1078_Constants.JT808_0X8103_0x007A, typeof(JT808_0x8103_0x007A));
            jT808Builder.Config.JT808_0X8103_Factory.ParamMethods.Add(JT808_JT1078_Constants.JT808_0X8103_0x007B, typeof(JT808_0x8103_0x007B));
            jT808Builder.Config.JT808_0X8103_Factory.ParamMethods.Add(JT808_JT1078_Constants.JT808_0X8103_0x007C, typeof(JT808_0x8103_0x007C));
            return jT808Builder;
        }
    }
}
