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
    public class JT809_JT1078_0x9900Test
    {
        JT809Serializer JT809Serializer;
        public JT809_JT1078_0x9900Test()
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
            JT809_JT1078_0x9900 jT809_JT1078_0x9900 = new JT809_JT1078_0x9900()
            {
                VehicleNo="粤B12345",
                VehicleColor= Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType= (ushort)JT809_JT1078_SubBusinessType.主动上传音视频资源目录信息应答消息, 
                SubBodies=  new JT809_JT1078_0x9900_0x9901() {
                      Result= JT809_JT1078_0x9900_0x9901_Result.失败,
                      ItemNumber=2
                 }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9900).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029901000000020102", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT809_JT1078_0x9900 = JT809Serializer.Deserialize<JT809_JT1078_0x9900>("D4C142313233343500000000000000000000000000029901000000020102".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x9900.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x9900.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.主动上传音视频资源目录信息应答消息, jT809_JT1078_0x9900.SubBusinessType);
            var jT809_JT1078_0x9900_0x9901 = jT809_JT1078_0x9900.SubBodies as JT809_JT1078_0x9900_0x9901;
            Assert.Equal(1, (byte)jT809_JT1078_0x9900_0x9901.Result);
            Assert.Equal(2, (byte)jT809_JT1078_0x9900_0x9901.ItemNumber);
        }

        [Fact]
        public void Test3()
        {
            var GnssDatas = Enumerable.Range(0, 36).Select(m => (byte)m).ToArray();
            var AuthorizeCodes = "0123456789012345678901234567890123456789012345678901234567890123";
            JT809_JT1078_0x9900 jT809_JT1078_0x9900 = new JT809_JT1078_0x9900()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.查询音视频资源目录请求消息,
                SubBodies = new JT809_JT1078_0x9900_0x9902() {
                    ChannelId = 1,
                    AlarmType = 2,
                    AVItemType = 3,
                    MemType = 4,
                    StartTime = Convert.ToDateTime("2017-07-15 10:10:10"),
                    EndTime = Convert.ToDateTime("2017-07-16 10:10:10"),
                    StreamType =5,
                    AuthorizeCode= AuthorizeCodes,
                    GnssData= GnssDatas
                }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9900).ToHexString();
            Assert.Equal("D4C1423132333435000000000000000000000000000299020000008001000000005969798200000000596ACB02000000000000000203050430313233343536373839303132333435363738393031323334353637383930313233343536373839303132333435363738393031323334353637383930313233000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223", hex);
        }

        [Fact]
        public void Test4()
        {
            var GnssDatas = Enumerable.Range(0, 36).Select(m => (byte)m).ToArray();
            var AuthorizeCodes = "0123456789012345678901234567890123456789012345678901234567890123";
            var jT809_JT1078_0x9900 = JT809Serializer.Deserialize<JT809_JT1078_0x9900>("D4C1423132333435000000000000000000000000000299020000008001000000005969798200000000596ACB02000000000000000203050430313233343536373839303132333435363738393031323334353637383930313233343536373839303132333435363738393031323334353637383930313233000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x9900.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x9900.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.查询音视频资源目录请求消息, jT809_JT1078_0x9900.SubBusinessType);
            var jT809_JT1078_0x9900_0x9902 = jT809_JT1078_0x9900.SubBodies as JT809_JT1078_0x9900_0x9902;
            Assert.Equal(1, jT809_JT1078_0x9900_0x9902.ChannelId);
            Assert.Equal(2u, jT809_JT1078_0x9900_0x9902.AlarmType);
            Assert.Equal(3, jT809_JT1078_0x9900_0x9902.AVItemType);
            Assert.Equal(4, jT809_JT1078_0x9900_0x9902.MemType);
            Assert.Equal(Convert.ToDateTime("2017-07-15 10:10:10"), jT809_JT1078_0x9900_0x9902.StartTime);
            Assert.Equal(Convert.ToDateTime("2017-07-16 10:10:10"), jT809_JT1078_0x9900_0x9902.EndTime);
            Assert.Equal(5, jT809_JT1078_0x9900_0x9902.StreamType);
            Assert.Equal(AuthorizeCodes, jT809_JT1078_0x9900_0x9902.AuthorizeCode);
            Assert.Equal(GnssDatas, jT809_JT1078_0x9900_0x9902.GnssData);
        }
    }
}
