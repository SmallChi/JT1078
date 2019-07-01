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
    public class JT809_JT1078_0x1900Test
    {
        JT809Serializer JT809Serializer;
        public JT809_JT1078_0x1900Test()
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
            JT809_JT1078_0x1900 jT809_JT1078_0x1900 = new JT809_JT1078_0x1900()
            {
              VehicleNo="粤B12345",
               VehicleColor= Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType= (ushort)JT809_JT1078_SubBusinessType.主动上传音视频资源目录信息消息, 
                 SubBodies=  new JT809_JT1078_0x1900_0x1901() {
                     ItemNum=2,
                      ItemList=new List<JT809_JT1078_0x1900_Record> {
                          new JT809_JT1078_0x1900_Record{
                               AlarmType=1,
                                AVItemType=2,
                                 ChannelId=3, 
                                  EndTime=Convert.ToDateTime("2019-07-16 10:10:10"),
                                   FileSize=4,
                                    MemType=5,
                                     StartTime=Convert.ToDateTime("2019-07-15 10:10:10"),
                                      StreamType=6
                           },
                          new JT809_JT1078_0x1900_Record{
                                     AlarmType=11,
                                AVItemType=21,
                                 ChannelId=31,
                                  EndTime=Convert.ToDateTime("2019-06-16 10:10:10"),
                                   FileSize=41,
                                    MemType=51,
                                     StartTime=Convert.ToDateTime("2019-06-15 10:10:10"),
                                      StreamType=61
                          }
                      }
                 }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x1900);
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1900).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000021901000000440000000203000000005D2BE082000000005D2D32020000000000000001020605000000041F000000005D045382000000005D05A502000000000000000B153D3300000029", hex);
        }

        [Fact]
        public void Test2()
        {
            var str = "{\"VehicleNo\":\"粤B12345\",\"VehicleColor\":2,\"SubBusinessType\":6401,\"DataLength\":68,\"SubBodies\":{\"ItemNum\":2,\"ItemList\":[{\"ChannelId\":3,\"StartTime\":\"2019-07-15 10:10:10\",\"EndTime\":\"2019-07-16 10:10:10\",\"AlarmType\":1,\"AVItemType\":2,\"StreamType\":6,\"MemType\":5,\"FileSize\":4},{\"ChannelId\":31,\"StartTime\":\"2019-06-15 10:10:10\",\"EndTime\":\"2019-06-16 10:10:10\",\"AlarmType\":11,\"AVItemType\":21,\"StreamType\":61,\"MemType\":51,\"FileSize\":41}]}}";
            var jT809_JT1078_0x1900 = JT809Serializer.Deserialize<JT809_JT1078_0x1900>("D4C142313233343500000000000000000000000000021901000000440000000203000000005D2BE082000000005D2D32020000000000000001020605000000041F000000005D045382000000005D05A502000000000000000B153D3300000029".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x1900), str);
        }

        [Fact]
        public void Test3()
        {
            JT809_JT1078_0x1900 jT809_JT1078_0x1900 = new JT809_JT1078_0x1900()
            {
                VehicleNo = "粤B12345",
                VehicleColor = Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType = (ushort)JT809_JT1078_SubBusinessType.查询音视频资源目录应答消息,
                SubBodies = new JT809_JT1078_0x1900_0x1902() {
                     Result=1,
                      ItemNum=2,
                       ItemList = new List<JT809_JT1078_0x1900_Record> {
                          new JT809_JT1078_0x1900_Record{
                               AlarmType=1,
                                AVItemType=2,
                                 ChannelId=3,
                                  EndTime=Convert.ToDateTime("2019-07-16 10:10:10"),
                                   FileSize=4,
                                    MemType=5,
                                     StartTime=Convert.ToDateTime("2019-07-15 10:10:10"),
                                      StreamType=6
                           },
                          new JT809_JT1078_0x1900_Record{
                                     AlarmType=11,
                                AVItemType=21,
                                 ChannelId=31,
                                  EndTime=Convert.ToDateTime("2019-06-16 10:10:10"),
                                   FileSize=41,
                                    MemType=51,
                                     StartTime=Convert.ToDateTime("2019-06-15 10:10:10"),
                                      StreamType=61
                          }
                      }
                }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x1900);
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1900).ToHexString();
            Assert.Equal("D4C14231323334350000000000000000000000000002190200000045010000000203000000005D2BE082000000005D2D32020000000000000001020605000000041F000000005D045382000000005D05A502000000000000000B153D3300000029", hex);
        }

        [Fact]
        public void Test4()
        {
            var str = "{\"VehicleNo\":\"粤B12345\",\"VehicleColor\":2,\"SubBusinessType\":6402,\"DataLength\":69,\"SubBodies\":{\"Result\":1,\"ItemNum\":2,\"ItemList\":[{\"ChannelId\":3,\"StartTime\":\"2019-07-15 10:10:10\",\"EndTime\":\"2019-07-16 10:10:10\",\"AlarmType\":1,\"AVItemType\":2,\"StreamType\":6,\"MemType\":5,\"FileSize\":4},{\"ChannelId\":31,\"StartTime\":\"2019-06-15 10:10:10\",\"EndTime\":\"2019-06-16 10:10:10\",\"AlarmType\":11,\"AVItemType\":21,\"StreamType\":61,\"MemType\":51,\"FileSize\":41}]}}";
            var jT809_JT1078_0x1900 = JT809Serializer.Deserialize<JT809_JT1078_0x1900>("D4C14231323334350000000000000000000000000002190200000045010000000203000000005D2BE082000000005D2D32020000000000000001020605000000041F000000005D045382000000005D05A502000000000000000B153D3300000029".ToHexBytes());
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT809_JT1078_0x1900), str);
        }
    }
}
