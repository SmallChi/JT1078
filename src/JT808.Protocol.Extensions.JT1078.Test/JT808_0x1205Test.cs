using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public  class JT808_0x1205Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x1205Test()
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
            JT808_0x1205 jT808_0x1205 = new JT808_0x1205()
            {
                MsgNum = 1,
                AVResouceTotal = 2,
                AVResouces = new List<JT808_0x1205_AVResouce> {
                     new JT808_0x1205_AVResouce{
                          AlarmFlag=1,
                           AVResourceType=2,
                            BeginTime=Convert.ToDateTime("2019-07-16 10:20:01"),
                             EndTime=Convert.ToDateTime("2019-07-16 10:25:01"),
                              FileSize=3,
                               LogicChannelNo=4,
                                MemoryType=5,
                                 StreamType=6
                     },
                     new JT808_0x1205_AVResouce{
                             AlarmFlag=11,
                           AVResourceType=21,
                            BeginTime=Convert.ToDateTime("2019-07-16 11:20:01"),
                             EndTime=Convert.ToDateTime("2019-07-16 11:25:02"),
                              FileSize=31,
                               LogicChannelNo=41,
                                MemoryType=51,
                                 StreamType=61
                     }
                }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x1205);
            var hex = JT808Serializer.Serialize(jT808_0x1205).ToHexString();
            Assert.Equal("000100000002041907161020011907161025010000000102060500000003291907161120011907161125020000000B153D330000001F", hex);
        }

        [Fact]
        public void Test2()
        {
            var str = "{\"MsgNum\":1,\"AVResouceTotal\":2,\"AVResouces\":[{\"LogicChannelNo\":4,\"BeginTime\":\"2019-07-16 10:20:01\",\"EndTime\":\"2019-07-16 10:25:01\",\"AlarmFlag\":1,\"AVResourceType\":2,\"StreamType\":6,\"MemoryType\":5,\"FileSize\":3},{\"LogicChannelNo\":41,\"BeginTime\":\"2019-07-16 11:20:01\",\"EndTime\":\"2019-07-16 11:25:02\",\"AlarmFlag\":11,\"AVResourceType\":21,\"StreamType\":61,\"MemoryType\":51,\"FileSize\":31}],\"SkipSerialization\":false}";
            var jT808_0x1205 = JT808Serializer.Deserialize<JT808_0x1205>("000100000002041907161020011907161025010000000102060500000003291907161120011907161125020000000B153D330000001F".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x1205), str);
        }
    }
}
