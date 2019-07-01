using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public  class JT808_0x1003Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x1003Test()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1
                            .AddJT808Configure()
                            .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<IJT808Config>();
            JT808Serializer = defaultConfig.GetSerializer();
        }

        [Fact]
        public void Test1()
        {
            JT808_0x1003 jT808_0x1003 = new JT808_0x1003()
            {
                AudioFrameLength = 1,
                EnterAudioChannelsNumber = 2,
                EnterAudioEncoding = 3,
                EnterAudioSampleDigits = 4,
                EnterAudioSampleRate = 5,
                IsSupportedAudioOutput = 1,
                VideoEncoding = 6,
                TerminalSupportedMaxNumberOfAudioPhysicalChannels = 7,
                TerminalSupportedMaxNumberOfVideoPhysicalChannels = 8
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x1003);
            var hex = JT808Serializer.Serialize(jT808_0x1003).ToHexString();
            Assert.Equal("03020504000101060708", hex);
        }

        [Fact]
        public void Test2()
        {
            var str = "{\"EnterAudioEncoding\":3,\"EnterAudioChannelsNumber\":2,\"EnterAudioSampleRate\":5,\"EnterAudioSampleDigits\":4,\"AudioFrameLength\":1,\"IsSupportedAudioOutput\":1,\"VideoEncoding\":6,\"TerminalSupportedMaxNumberOfAudioPhysicalChannels\":7,\"TerminalSupportedMaxNumberOfVideoPhysicalChannels\":8,\"SkipSerialization\":false}";
            JT808_0x1003 jT808_0x1003 = JT808Serializer.Deserialize<JT808_0x1003>("03020504000101060708".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x1003), str);
        }
    }
}
