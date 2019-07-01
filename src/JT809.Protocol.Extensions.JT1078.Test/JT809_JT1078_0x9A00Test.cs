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
    public class JT809_JT1078_0x9A00Test
    {
        JT809Serializer JT809Serializer;
        public JT809_JT1078_0x9A00Test()
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
            JT809_JT1078_0x9A00 jT809_JT1078_0x9A00 = new JT809_JT1078_0x9A00()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.远程录像回放请求消息,
                SubBodies = new JT809_JT1078_0x9A00_0x9A01() {
                    AuthorizeCode = AuthorizeCodes,
                    GnssData = GnssDatas,
                    AVItemType = 1,
                    ChannelId = 2,
                    MemType = 3,
                    StreamType = 4,
                    PlayBackStartTime = Convert.ToDateTime("2017-07-16 10:10:10"),
                     PlayBackEndTime = Convert.ToDateTime("2017-07-17 10:10:10")
        }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9A00);
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9A00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029A01000000780201040300000000596ACB0200000000596C1C82000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223", hex);
        }

        [Fact]
        public void Test2()
        {
            var str = "{\"VehicleNo\":\"粤B12345\",\"VehicleColor\":2,\"SubBusinessType\":39425,\"DataLength\":120,\"SubBodies\":{\"ChannelId\":2,\"AVItemType\":1,\"StreamType\":4,\"MemType\":3,\"PlayBackStartTime\":\"2017-07-16 10:10:10\",\"PlayBackEndTime\":\"2017-07-17 10:10:10\",\"AuthorizeCode\":\"AAECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxwdHh8gISIjJCUmJygpKissLS4vMDEyMzQ1Njc4OTo7PD0+Pw==\",\"GnssData\":\"AAECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxwdHh8gISIj\"}}";
            var jT809_JT1078_0x9A00 = JT809Serializer.Deserialize<JT809_JT1078_0x9A00>("D4C142313233343500000000000000000000000000029A01000000780201040300000000596ACB0200000000596C1C82000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9A00), str);
        }

        [Fact]
        public void Test3()
        {
            var GnssDatas = Enumerable.Range(0, 36).Select(m => (byte)m).ToArray();
            var AuthorizeCodes = Enumerable.Range(0, 64).Select(m => (byte)m).ToArray();
            JT809_JT1078_0x9A00 jT809_JT1078_0x9A00 = new JT809_JT1078_0x9A00()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.远程录像回放控制消息,
                SubBodies = new JT809_JT1078_0x9A00_0x9A02() {
               ControlType=1,
                DateTime= Convert.ToDateTime("2017-07-17 10:10:10"),
                 FastTime=1
                }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9A00);
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9A00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029A020000000A010100000000596C1C82", hex);
        }

        [Fact]
        public void Test4()
        {
            var str = "{\"VehicleNo\":\"粤B12345\",\"VehicleColor\":2,\"SubBusinessType\":39426,\"DataLength\":10,\"SubBodies\":{\"ControlType\":1,\"FastTime\":1,\"DateTime\":\"2017-07-17 10:10:10\"}}";
            var jT809_JT1078_0x9A00 = JT809Serializer.Deserialize<JT809_JT1078_0x9A00>("D4C142313233343500000000000000000000000000029A020000000A010100000000596C1C82".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x9A00), str);
        }
    }
}
