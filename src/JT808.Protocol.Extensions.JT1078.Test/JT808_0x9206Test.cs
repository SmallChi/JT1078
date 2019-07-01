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
    public class JT808_0x9206Test
    {
        JT808Serializer JT808Serializer;
        public JT808_0x9206Test()
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
            JT808_0x9206 jT808_0x9206 = new JT808_0x9206()
            {
 AlarmFlag=1,
  AVResourceType=2,
   BeginTime= Convert.ToDateTime("2019-07-16 10:10:10"),
    EndTime= Convert.ToDateTime("2019-07-16 10:10:11"),
     LogicChannelNo=3,   
                StreamType =5, FileUploadPath="D://1112",
                 FileUploadPathLength=8,
                  MemoryPositon=4,
                   Password="123456",
                    PasswordLength=6,
                     Port=808,
                      ServerIp="127.0.0.1",
                       ServerIpLength=9,
                        TaskExcuteCondition=7, 
                         UserName="tk",
                          UserNameLength=2
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x9206);
            var hex = JT808Serializer.Serialize(jT808_0x9206).ToHexString();
            Assert.Equal("093132372E302E302E31032802746B0631323334353608443A2F2F31313132031907161010101907161010110000000102050407", hex);
        }

        [Fact]
        public void Test2()
        {
            var str = "{\"ServerIpLength\":9,\"ServerIp\":\"127.0.0.1\",\"Port\":808,\"UserNameLength\":2,\"UserName\":\"tk\",\"PasswordLength\":6,\"Password\":\"123456\",\"FileUploadPathLength\":8,\"FileUploadPath\":\"D://1112\",\"LogicChannelNo\":3,\"BeginTime\":\"2019-07-16 10:10:10\",\"EndTime\":\"2019-07-16 10:10:11\",\"AlarmFlag\":1,\"AVResourceType\":2,\"StreamType\":5,\"MemoryPositon\":4,\"TaskExcuteCondition\":7,\"SkipSerialization\":false}";
            var jT808_0x9206 = JT808Serializer.Deserialize<JT808_0x9206>("093132372E302E302E31032802746B0631323334353608443A2F2F31313132031907161010101907161010110000000102050407".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x9206), str);
        }
    }
}