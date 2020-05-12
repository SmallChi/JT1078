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
            var AuthorizeCodes = "0123456789012345678901234567890123456789012345678901234567890123";
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
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9B00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029B01000000840200000000596ACB0200000000596C1C8200000000000000050104030000000630313233343536373839303132333435363738393031323334353637383930313233343536373839303132333435363738393031323334353637383930313233000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223", hex);
        }

        [Fact]
        public void Test2()
        {
            var GnssDatas = Enumerable.Range(0, 36).Select(m => (byte)m).ToArray();
            var AuthorizeCodes = "0123456789012345678901234567890123456789012345678901234567890123";
            var jT809_JT1078_0x9B00 = JT809Serializer.Deserialize<JT809_JT1078_0x9B00>("D4C142313233343500000000000000000000000000029B01000000840200000000596ACB0200000000596C1C8200000000000000050104030000000630313233343536373839303132333435363738393031323334353637383930313233343536373839303132333435363738393031323334353637383930313233000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F20212223".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x9B00.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x9B00.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.远程录像下载请求消息, jT809_JT1078_0x9B00.SubBusinessType);
            var jT809_JT1078_0x9B00_0x9B01 = jT809_JT1078_0x9B00.SubBodies as JT809_JT1078_0x9B00_0x9B01;
            Assert.Equal(AuthorizeCodes, jT809_JT1078_0x9B00_0x9B01.AuthorizeCode);
            Assert.Equal(GnssDatas, jT809_JT1078_0x9B00_0x9B01.GnssData);
            Assert.Equal(1, jT809_JT1078_0x9B00_0x9B01.AVItemType);
            Assert.Equal(2, jT809_JT1078_0x9B00_0x9B01.ChannelId);
            Assert.Equal(3, jT809_JT1078_0x9B00_0x9B01.MemType);
            Assert.Equal(4, jT809_JT1078_0x9B00_0x9B01.StreamType);
            Assert.Equal(Convert.ToDateTime("2017-07-16 10:10:10"), jT809_JT1078_0x9B00_0x9B01.StartTime);
            Assert.Equal(Convert.ToDateTime("2017-07-17 10:10:10"), jT809_JT1078_0x9B00_0x9B01.EndTime);
            Assert.Equal(5u, jT809_JT1078_0x9B00_0x9B01.AlarmType);
            Assert.Equal(6u, jT809_JT1078_0x9B00_0x9B01.FileSize);
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
                    Result= JT809_JT1078_0x9B00_0x9B02_Result.失败,
                    SessionId=2
                }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9B00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029B0200000003010002", hex);
        }

        [Fact]
        public void Test4()
        {
            var jT809_JT1078_0x9B00 = JT809Serializer.Deserialize<JT809_JT1078_0x9B00>("D4C142313233343500000000000000000000000000029B0200000003010002".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x9B00.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x9B00.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.远程录像下载完成通知应答消息, jT809_JT1078_0x9B00.SubBusinessType);
            var jT809_JT1078_0x9B00_0x9B02 = jT809_JT1078_0x9B00.SubBodies as JT809_JT1078_0x9B00_0x9B02;
            Assert.Equal(1, (byte)jT809_JT1078_0x9B00_0x9B02.Result);
            Assert.Equal(2, (byte)jT809_JT1078_0x9B00_0x9B02.SessionId);
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
                     Type= JT809_JT1078_0x9B00_0x9B03_Type.继续,
                     SessionId = 2
                }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9B00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000029B0300000003000201", hex);
        }

        [Fact]
        public void Test6()
        {
            var jT809_JT1078_0x9B00 = JT809Serializer.Deserialize<JT809_JT1078_0x9B00>("D4C142313233343500000000000000000000000000029B0300000003000201".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x9B00.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x9B00.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.远程录像下载控制消息, jT809_JT1078_0x9B00.SubBusinessType);
            var jT809_JT1078_0x9B00_0x9B03 = jT809_JT1078_0x9B00.SubBodies as JT809_JT1078_0x9B00_0x9B03;
            Assert.Equal(1, (byte)jT809_JT1078_0x9B00_0x9B03.Type);
            Assert.Equal(2, (byte)jT809_JT1078_0x9B00_0x9B03.SessionId);
        }
    }
}
