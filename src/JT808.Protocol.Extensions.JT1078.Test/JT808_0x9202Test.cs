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
    public class JT808_0x9202Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9202Test()
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
            JT808_0x9202 jT808_0x9202 = new JT808_0x9202()
            {
  AVChannelNo=1,
   DragPlaybackPosition=Convert.ToDateTime("2019-07-16 10:10:10"),
    FastForwardOrFastRewindMultiples=2,
     PlayBackControl=3
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x9202);
            var hex = JT808Serializer.Serialize(jT808_0x9202).ToHexString();
            Assert.Equal("010302190716101010", hex);
        }

        [Fact]
        public void Test2()
        {
            var str = "{\"AVChannelNo\":1,\"PlayBackControl\":3,\"FastForwardOrFastRewindMultiples\":2,\"DragPlaybackPosition\":\"2019-07-16 10:10:10\",\"SkipSerialization\":false}";
            var jT808_0x9202 = JT808Serializer.Deserialize<JT808_0x9202>("010302190716101010".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x9202), str);
        }
    }
}