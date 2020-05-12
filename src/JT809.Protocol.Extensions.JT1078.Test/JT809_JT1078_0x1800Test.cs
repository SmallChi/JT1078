using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Extensions.JT1078.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace JT809.Protocol.Extensions.JT1078.Test
{
    public class JT809_JT1078_0x1800Test
    {
        JT809Serializer JT809Serializer;
        public JT809_JT1078_0x1800Test()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1
                            .AddJT809Configure()
                            .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<IJT809Config>();
             JT809Serializer = defaultConfig.GetSerializer();
        }



        [Fact]
        public void Test1()
        {
            JT809_JT1078_0x1800 jT809_JT1078_0x1800 = new JT809_JT1078_0x1800()
            {
                VehicleNo="粤B12345",
                VehicleColor= Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType= (ushort)JT809_JT1078_SubBusinessType.实时音视频请求应答消息, 
                SubBodies=  new JT809_JT1078_0x1800_0x1801() {
                    ServerIp="127.0.0.1",
                    ServerPort=8080,
                    Result= JT809_JT1078_0x1800_0x1801_Result.失败
                 }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1800).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000021801000000230100000000000000000000000000000000000000000000003132372E302E302E311F90", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT809_JT1078_0x1800 = JT809Serializer.Deserialize<JT809_JT1078_0x1800>("D4C142313233343500000000000000000000000000021801000000230100000000000000000000000000000000000000000000003132372E302E302E311F90".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x1800.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x1800.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.实时音视频请求应答消息, jT809_JT1078_0x1800.SubBusinessType);
            var jT809_JT1078_0x1800_0x1801 = jT809_JT1078_0x1800.SubBodies as JT809_JT1078_0x1800_0x1801;
            Assert.Equal("127.0.0.1", jT809_JT1078_0x1800_0x1801.ServerIp);
            Assert.Equal(8080, jT809_JT1078_0x1800_0x1801.ServerPort);
            Assert.Equal(1, (byte)jT809_JT1078_0x1800_0x1801.Result);
        }

        [Fact]
        public void Test3()
        {
            JT809_JT1078_0x1800 jT809_JT1078_0x1800 = new JT809_JT1078_0x1800()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.主动请求停止实时音视频传输应答消息,
                SubBodies = new JT809_JT1078_0x1800_0x1802() {
                     Result= JT809_JT1078_0x1800_0x1802_Result.失败
                }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1800).ToHexString();
            Assert.Equal("D4C1423132333435000000000000000000000000000218020000000101", hex);
        }

        [Fact]
        public void Test4()
        {
            var jT809_JT1078_0x1800 = JT809Serializer.Deserialize<JT809_JT1078_0x1800>("D4C1423132333435000000000000000000000000000218020000000101".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x1800.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x1800.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.主动请求停止实时音视频传输应答消息, jT809_JT1078_0x1800.SubBusinessType);
            var jT809_JT1078_0x1800_0x1802 = jT809_JT1078_0x1800.SubBodies as JT809_JT1078_0x1800_0x1802;
            Assert.Equal(1, (byte)jT809_JT1078_0x1800_0x1802.Result);
        }
    }
}
