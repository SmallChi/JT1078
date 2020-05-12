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
    public class JT809_JT1078_0x1B00Test
    {
        JT809Serializer JT809Serializer;
        public JT809_JT1078_0x1B00Test()
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
            JT809_JT1078_0x1B00 jT809_JT1078_0x1B00 = new JT809_JT1078_0x1B00()
            {
                 VehicleNo="粤B12345",
                 VehicleColor= Protocol.Enums.JT809VehicleColorType.黄色,
                 SubBusinessType= (ushort)JT809_JT1078_SubBusinessType.远程录像下载请求应答消息, 
                 SubBodies=  new JT809_JT1078_0x1B00_0x1B01()
                 {
                    Result= JT809_JT1078_0x1B00_0x1B01_Result.失败,
                    SessionId=2
                 }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1B00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000021B0100000003010002", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT809_JT1078_0x1B00 = JT809Serializer.Deserialize<JT809_JT1078_0x1B00>("D4C142313233343500000000000000000000000000021B0100000003010002".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x1B00.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x1B00.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.远程录像下载请求应答消息, jT809_JT1078_0x1B00.SubBusinessType);
            var jT809_JT1078_0x1B00_0x1B01 = jT809_JT1078_0x1B00.SubBodies as JT809_JT1078_0x1B00_0x1B01;
            Assert.Equal(1, (byte)jT809_JT1078_0x1B00_0x1B01.Result);
            Assert.Equal(2, (byte)jT809_JT1078_0x1B00_0x1B01.SessionId);
        }

        [Fact]
        public void Test3()
        {
            JT809_JT1078_0x1B00 jT809_JT1078_0x1B00 = new JT809_JT1078_0x1B00()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.远程录像下载通知消息,
                SubBodies = new JT809_JT1078_0x1B00_0x1B02() {
                    Result= JT809_JT1078_0x1B00_0x1B02_Result.失败, 
                    UserName="tk",
                    SessionId=2,
                    ServerIp="127.0.0.1",
                    FilePath="D://123/456",
                    Password="tk123456",
                    TcpPort=8080
                }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1B00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000021B020000013401000200000000000000000000000000000000000000000000003132372E302E302E311F900000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000746B0000000000000000000000000000746B313233343536000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000443A2F2F3132332F343536", hex);
        }

        [Fact]
        public void Test4()
        {
            var jT809_JT1078_0x1B00 = JT809Serializer.Deserialize<JT809_JT1078_0x1B00>("D4C142313233343500000000000000000000000000021B020000013401000200000000000000000000000000000000000000000000003132372E302E302E311F900000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000746B0000000000000000000000000000746B313233343536000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000443A2F2F3132332F343536".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x1B00.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x1B00.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.远程录像下载通知消息, jT809_JT1078_0x1B00.SubBusinessType);
            var jT809_JT1078_0x1B00_0x1B02 = jT809_JT1078_0x1B00.SubBodies as JT809_JT1078_0x1B00_0x1B02;
            Assert.Equal(1, (byte)jT809_JT1078_0x1B00_0x1B02.Result);
            Assert.Equal("tk", jT809_JT1078_0x1B00_0x1B02.UserName);
            Assert.Equal(2, jT809_JT1078_0x1B00_0x1B02.SessionId);
            Assert.Equal("127.0.0.1", jT809_JT1078_0x1B00_0x1B02.ServerIp);
            Assert.Equal("D://123/456", jT809_JT1078_0x1B00_0x1B02.FilePath);
            Assert.Equal("tk123456", jT809_JT1078_0x1B00_0x1B02.Password);
            Assert.Equal(8080, jT809_JT1078_0x1B00_0x1B02.TcpPort);
        }

        [Fact]
        public void Test5()
        {
            JT809_JT1078_0x1B00 jT809_JT1078_0x1B00 = new JT809_JT1078_0x1B00()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.远程录像下载控制应答消息,
                SubBodies = new JT809_JT1078_0x1B00_0x1B03()
                {
                    Result =  JT809_JT1078_0x1B00_0x1B03_Result.失败                   
                }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1B00).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000021B030000000101", hex);
        }

        [Fact]
        public void Test6()
        {
            var jT809_JT1078_0x1B00 = JT809Serializer.Deserialize<JT809_JT1078_0x1B00>("D4C142313233343500000000000000000000000000021B030000000101".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x1B00.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x1B00.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.远程录像下载控制应答消息, jT809_JT1078_0x1B00.SubBusinessType);
            var jT809_JT1078_0x1B00_0x1B03 = jT809_JT1078_0x1B00.SubBodies as JT809_JT1078_0x1B00_0x1B03;
            Assert.Equal(1, (byte)jT809_JT1078_0x1B00_0x1B03.Result);
        }
    }
}
