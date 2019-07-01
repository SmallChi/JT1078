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
    public class JT809_JT1078_0x9B00Test
    {
        JT809Serializer JT809Serializer;
        public JT809_JT1078_0x9B00Test()
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
            var GnssDatas = Enumerable.Range(0, 36).Select(m => (byte)m).ToArray();
            var AuthorizeCodes = Enumerable.Range(0, 64).Select(m => (byte)m).ToArray();
            JT809_JT1078_0x9B00 jT809_JT1078_0x9B00 = new JT809_JT1078_0x9B00()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.远程录像下载请求消息,
                SubBodies = new JT809_JT1078_0x9B00_0x9B01() {
                    AuthorizeCode = AuthorizeCodes,
                    GnssData = GnssDatas,
                    AVItemType = 1,
                    ChannelId = 2,
                    MemType = 3,
                    StreamType = 4,
                     StartTime = Convert.ToDateTime("2017-07-16 10:10:10"),
                      EndTime = Convert.ToDateTime("2017-07-17 10:10:10"),
                       AlarmType=5,
                         FileSize=6
        }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9B00);
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9B00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029B01000000840200000000596ACB0200000000596C1C82000000000000000501040300000006000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223", hex);
        }

        [Fact]
        public void Test2()
        {
            var str = "{\"VehicleNo\":\"粤B12345\",\"VehicleColor\":2,\"SubBusinessType\":39681,\"DataLength\":132,\"SubBodies\":{\"ChannelId\":2,\"StartTime\":\"2017-07-16 10:10:10\",\"EndTime\":\"2017-07-17 10:10:10\",\"AlarmType\":5,\"AVItemType\":1,\"StreamType\":4,\"MemType\":3,\"FileSize\":6,\"AuthorizeCode\":\"AAECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxwdHh8gISIjJCUmJygpKissLS4vMDEyMzQ1Njc4OTo7PD0+Pw==\",\"GnssData\":\"AAECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxwdHh8gISIj\"}}";
            var jT809_JT1078_0x9B00 = JT809Serializer.Deserialize<JT809_JT1078_0x9B00>("D4C142313233343500000000000000000000000000029B01000000840200000000596ACB0200000000596C1C82000000000000000501040300000006000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9B00), str);
        }

        [Fact]
        public void Test3()
        {
            JT809_JT1078_0x9B00 jT809_JT1078_0x9B00 = new JT809_JT1078_0x9B00()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.远程录像下载完成通知应答消息,
                SubBodies = new JT809_JT1078_0x9B00_0x9B02() {
               Result=1,
                SessionId=2
                }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9B00);
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9B00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029B0200000003010002", hex);
        }

        [Fact]
        public void Test4()
        {
            var str = "{\"VehicleNo\":\"粤B12345\",\"VehicleColor\":2,\"SubBusinessType\":39682,\"DataLength\":3,\"SubBodies\":{\"Result\":1,\"SessionId\":2}}";
            var jT809_JT1078_0x9B00 = JT809Serializer.Deserialize<JT809_JT1078_0x9B00>("D4C142313233343500000000000000000000000000029B0200000003010002".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9B00), str);
        }
        [Fact]
        public void Test5()
        {
            JT809_JT1078_0x9B00 jT809_JT1078_0x9B00 = new JT809_JT1078_0x9B00()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.远程录像下载控制消息,
                SubBodies = new JT809_JT1078_0x9B00_0x9B03()
                {
                     Type=1,
                    SessionId = 2
                }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9B00);
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9B00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029B0300000003000201", hex);
        }

        [Fact]
        public void Test6()
        {
            var str = "{\"VehicleNo\":\"粤B12345\",\"VehicleColor\":2,\"SubBusinessType\":39683,\"DataLength\":3,\"SubBodies\":{\"SessionId\":2,\"Type\":1}}";
            var jT809_JT1078_0x9B00 = JT809Serializer.Deserialize<JT809_JT1078_0x9B00>("D4C142313233343500000000000000000000000000029B0300000003000201".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9B00), str);
        }
    }
}
