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
    public class JT808_0x9205Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9205Test()
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
            JT808_0x9205 jT808_0x9205 = new JT808_0x9205()
            {
                AlarmFlag=1,
                AVResourceType=2,
                BeginTime= Convert.ToDateTime("2019-07-16 10:10:10"),
                EndTime= Convert.ToDateTime("2019-07-16 10:10:11"),
                LogicChannelNo=3,
                MemoryType=4,
                StreamType =5
            };
            var hex = JT808Serializer.Serialize(jT808_0x9205).ToHexString();
            Assert.Equal("0319071610101019071610101100000001020504", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT808_0x9205 = JT808Serializer.Deserialize<JT808_0x9205>("0319071610101019071610101100000001020504".ToHexBytes());
            Assert.Equal(1u, jT808_0x9205.AlarmFlag);
            Assert.Equal(2, jT808_0x9205.AVResourceType);
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:10:10"), jT808_0x9205.BeginTime);
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:10:11"), jT808_0x9205.EndTime);
            Assert.Equal(3, jT808_0x9205.LogicChannelNo);
            Assert.Equal(4, jT808_0x9205.MemoryType);
            Assert.Equal(5, jT808_0x9205.StreamType);
        }
    }
}