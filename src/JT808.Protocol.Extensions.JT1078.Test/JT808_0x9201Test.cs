using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public class JT808_0x9201Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9201Test()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1
                            .AddJT808Configure()
                            .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<IJT808Config>();
            JT808Serializer = defaultConfig.GetSerializer();

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
            JT808_0x9201 jT808_0x9201 = new JT808_0x9201()
            {
                LogicChannelNo = 1,
                AVItemType = 2,
                BeginTime = Convert.ToDateTime("2019-07-16 10:10:10"),
                 EndTime = Convert.ToDateTime("2019-07-16 10:10:10"),
                  FastForwardOrFastRewindMultiples1=3,
                   FastForwardOrFastRewindMultiples2=4,
                    MemType=5,
                     PlayBackWay=6,
                      ServerIp="127.0.0.1",
                       ServerIpLength=9,
                        StreamType=7,
                         TcpPort=80,
                          UdpPort=8080
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x9201);
            var hex = JT808Serializer.Serialize(jT808_0x9201).ToHexString();
            Assert.Equal("093132372E302E302E3100501F9001020705060304190716101010190716101010", hex);
        }

        [Fact]
        public void Test2()
        {
            var str = "{\"ServerIpLength\":9,\"ServerIp\":\"127.0.0.1\",\"TcpPort\":80,\"UdpPort\":8080,\"LogicChannelNo\":1,\"AVItemType\":2,\"StreamType\":7,\"MemType\":5,\"PlayBackWay\":6,\"FastForwardOrFastRewindMultiples1\":3,\"FastForwardOrFastRewindMultiples2\":4,\"BeginTime\":\"2019-07-16 10:10:10\",\"EndTime\":\"2019-07-16 10:10:10\",\"SkipSerialization\":false}";
            var jT808_0x9201 = JT808Serializer.Deserialize<JT808_0x9201>("093132372E302E302E3100501F9001020705060304190716101010190716101010".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x9201), str);
        }
    }
}