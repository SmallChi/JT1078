using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Extensions.JT1078.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace JT809.Protocol.Extensions.JT1078.Test
{
    public class JT809_JT1078_0x9800Test
    {
        JT809Serializer JT809Serializer;
        public JT809_JT1078_0x9800Test()
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
            var GnssDatas = Enumerable.Range(0, 36).Select(m => (byte)m).ToArray();
            var AuthorizeCodes = "0123456789012345678901234567890123456789012345678901234567890123";
            JT809_JT1078_0x9800 jT809_JT1078_0x9800 = new JT809_JT1078_0x9800()
            {
                VehicleNo="粤B12345",
                VehicleColor= Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType= (ushort)JT809_JT1078_SubBusinessType.实时音视频请求消息, 
                SubBodies=  new JT809_JT1078_0x9800_0x9801() {
                    AVitemType=1,
                    ChannelId=2,
                    GnssData= GnssDatas,
                    AuthorizeCode= AuthorizeCodes
                 }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9800).ToHexString();
            Assert.Equal("D4C14231323334350000000000000000000000000002980100000066020130313233343536373839303132333435363738393031323334353637383930313233343536373839303132333435363738393031323334353637383930313233000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223", hex);
        }

        [Fact]
        public void Test2()
        {
            var GnssDatas = Enumerable.Range(0, 36).Select(m => (byte)m).ToArray();
            var AuthorizeCodes = "0123456789012345678901234567890123456789012345678901234567890123";
            var jT809_JT1078_0x9800 = JT809Serializer.Deserialize<JT809_JT1078_0x9800>("D4C14231323334350000000000000000000000000002980100000066020130313233343536373839303132333435363738393031323334353637383930313233343536373839303132333435363738393031323334353637383930313233000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x9800.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x9800.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.实时音视频请求消息, jT809_JT1078_0x9800.SubBusinessType);
            var jT809_JT1078_0x9800_0x9801 = jT809_JT1078_0x9800.SubBodies as JT809_JT1078_0x9800_0x9801;
            Assert.Equal(1, jT809_JT1078_0x9800_0x9801.AVitemType);
            Assert.Equal(2, jT809_JT1078_0x9800_0x9801.ChannelId);
            Assert.Equal(GnssDatas, jT809_JT1078_0x9800_0x9801.GnssData);
            Assert.Equal(AuthorizeCodes, jT809_JT1078_0x9800_0x9801.AuthorizeCode);
        }

        [Fact]
        public void Test3()
        {
            JT809_JT1078_0x9800 jT809_JT1078_0x9800 = new JT809_JT1078_0x9800()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.主动请求停止实时音视频传输消息,
                SubBodies = new JT809_JT1078_0x9800_0x9802() {
                      ChannelId=1,
                      AVitemType=2
                }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9800).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029802000000020102", hex);
        }

        [Fact]
        public void Test4()
        {
            var jT809_JT1078_0x9800 = JT809Serializer.Deserialize<JT809_JT1078_0x9800>("D4C142313233343500000000000000000000000000029802000000020102".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x9800.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x9800.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.主动请求停止实时音视频传输消息, jT809_JT1078_0x9800.SubBusinessType);
            var jT809_JT1078_0x9800_0x9802 = jT809_JT1078_0x9800.SubBodies as JT809_JT1078_0x9800_0x9802;
            Assert.Equal(1, jT809_JT1078_0x9800_0x9802.ChannelId);
            Assert.Equal(2, jT809_JT1078_0x9800_0x9802.AVitemType);
        }
    }
}
