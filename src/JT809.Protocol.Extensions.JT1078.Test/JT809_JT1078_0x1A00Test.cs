using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Extensions.JT1078.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;

namespace JT809.Protocol.Extensions.JT1078.Test
{
    public class JT809_JT1078_0x1A00Test
    {
        JT809Serializer JT809Serializer;
        public JT809_JT1078_0x1A00Test()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1
                            .AddJT809Configure()
                            .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<IJT809Config>();
             JT809Serializer = defaultConfig.GetSerializer();

            Newtonsoft.Json.JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                return new Newtonsoft.Json.JsonSerializerSettings
                {
                    DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss"
                };
            });
        }



        [Fact]
        public void Test1()
        {
            JT809_JT1078_0x1A00 jT809_JT1078_0x1A00 = new JT809_JT1078_0x1A00()
            {
                 VehicleNo="粤B12345",
                 VehicleColor= Protocol.Enums.JT809VehicleColorType.黄色,
                 SubBusinessType= (ushort)JT809_JT1078_SubBusinessType.远程录像回放请求应答消息, 
                 SubBodies=  new JT809_JT1078_0x1A00_0x1A01()
                 {
                    Result= JT809_JT1078_0x1A00_0x1A01_Result.失败,
                    ServerIp="127.0.0.1",
                    ServerPort=8080
                 }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1A00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000021A010000002300000000000000000000000000000000000000000000003132372E302E302E311F9001", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT809_JT1078_0x1A00 = JT809Serializer.Deserialize<JT809_JT1078_0x1A00>("D4C142313233343500000000000000000000000000021A010000002300000000000000000000000000000000000000000000003132372E302E302E311F9001".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x1A00.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x1A00.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.远程录像回放请求应答消息, jT809_JT1078_0x1A00.SubBusinessType);
            var jT809_JT1078_0x1A00_0x1A01 = jT809_JT1078_0x1A00.SubBodies as JT809_JT1078_0x1A00_0x1A01;
            Assert.Equal(1, (byte)jT809_JT1078_0x1A00_0x1A01.Result);
            Assert.Equal("127.0.0.1", jT809_JT1078_0x1A00_0x1A01.ServerIp);
            Assert.Equal(8080, jT809_JT1078_0x1A00_0x1A01.ServerPort);
        }

        [Fact]
        public void Test3()
        {
            JT809_JT1078_0x1A00 jT809_JT1078_0x1A00 = new JT809_JT1078_0x1A00()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.远程录像回放控制应答消息,
                SubBodies = new JT809_JT1078_0x1A00_0x1A02() {
                  Result= JT809_JT1078_0x1A00_0x1A02_Result.失败
                }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1A00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000021A020000000101", hex);
        }

        [Fact]
        public void Test4()
        {
            var jT809_JT1078_0x1A00 = JT809Serializer.Deserialize<JT809_JT1078_0x1A00>("D4C142313233343500000000000000000000000000021A020000000101".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x1A00.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x1A00.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.远程录像回放控制应答消息, jT809_JT1078_0x1A00.SubBusinessType);
            var jT809_JT1078_0x1A00_0x1A02 = jT809_JT1078_0x1A00.SubBodies as JT809_JT1078_0x1A00_0x1A02;
            Assert.Equal(1, (byte)jT809_JT1078_0x1A00_0x1A02.Result);
        }
    }
}
