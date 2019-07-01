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
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x1003>(0x1003, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x1005>(0x1005, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x1205>(0x1205, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x1206>(0x1206, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9003>(0x9003, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9101>(0x9101, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9102>(0x9102, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9105>(0x9105, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9201>(0x9201, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9202>(0x9202, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9205>(0x9205, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9206>(0x9206, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9207>(0x9207, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9301>(0x9301, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9302>(0x9302, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9303>(0x9303, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9304>(0x9304, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9305>(0x9305, "");
            jT808Builder.Config.MsgIdFactory.SetMap<JT808_0x9306>(0x9306, "");
            return jT808Builder;
        }
    }
}
