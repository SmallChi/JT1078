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

        public object jT809_JT1078_0x1800 { get; private set; }

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
                                FileSize=4,
                                MemType=5,
                                StartTime=Convert.ToDateTime("2019-07-15 10:10:10"),
                                EndTime=Convert.ToDateTime("2019-07-16 10:10:10"),
                                StreamType=6
                           },
                          new JT809_JT1078_0x1900_Record{
                                AlarmType=11,
                                AVItemType=21,
                                ChannelId=31,                                
                                FileSize=41,
                                MemType=51,
                                StartTime=Convert.ToDateTime("2019-06-15 10:10:10"),
                                EndTime=Convert.ToDateTime("2019-06-16 10:10:10"),
                                StreamType=61
                          }
                      }
                 }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1900).ToHexString();
            Assert.Equal("D4C142313233343500000000000000000000000000021901000000440000000203000000005D2BE082000000005D2D32020000000000000001020605000000041F000000005D045382000000005D05A502000000000000000B153D3300000029", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT809_JT1078_0x1900 = JT809Serializer.Deserialize<JT809_JT1078_0x1900>("D4C142313233343500000000000000000000000000021901000000440000000203000000005D2BE082000000005D2D32020000000000000001020605000000041F000000005D045382000000005D05A502000000000000000B153D3300000029".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x1900.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x1900.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.主动上传音视频资源目录信息消息, jT809_JT1078_0x1900.SubBusinessType);
            var jT809_JT1078_0x1900_0x1901 = jT809_JT1078_0x1900.SubBodies as JT809_JT1078_0x1900_0x1901;
            Assert.Equal(2u, jT809_JT1078_0x1900_0x1901.ItemNum);

            Assert.Equal(1u, jT809_JT1078_0x1900_0x1901.ItemList[0].AlarmType);
            Assert.Equal(2, jT809_JT1078_0x1900_0x1901.ItemList[0].AVItemType);
            Assert.Equal(3, jT809_JT1078_0x1900_0x1901.ItemList[0].ChannelId);
            Assert.Equal(4u, jT809_JT1078_0x1900_0x1901.ItemList[0].FileSize);
            Assert.Equal(5, jT809_JT1078_0x1900_0x1901.ItemList[0].MemType);
            Assert.Equal(Convert.ToDateTime("2019-07-15 10:10:10"), jT809_JT1078_0x1900_0x1901.ItemList[0].StartTime);
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:10:10"), jT809_JT1078_0x1900_0x1901.ItemList[0].EndTime);
            Assert.Equal(6, jT809_JT1078_0x1900_0x1901.ItemList[0].StreamType);

            Assert.Equal(11u, jT809_JT1078_0x1900_0x1901.ItemList[1].AlarmType);
            Assert.Equal(21, jT809_JT1078_0x1900_0x1901.ItemList[1].AVItemType);
            Assert.Equal(31, jT809_JT1078_0x1900_0x1901.ItemList[1].ChannelId);
            Assert.Equal(41u, jT809_JT1078_0x1900_0x1901.ItemList[1].FileSize);
            Assert.Equal(51, jT809_JT1078_0x1900_0x1901.ItemList[1].MemType);
            Assert.Equal(Convert.ToDateTime("2019-06-15 10:10:10"), jT809_JT1078_0x1900_0x1901.ItemList[1].StartTime);
            Assert.Equal(Convert.ToDateTime("2019-06-16 10:10:10"), jT809_JT1078_0x1900_0x1901.ItemList[1].EndTime);
            Assert.Equal(61, jT809_JT1078_0x1900_0x1901.ItemList[1].StreamType);
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
                    Result= JT809_JT1078_0x1900_0x1902_Result.失败,
                    ItemNum=2,
                    ItemList = new List<JT809_JT1078_0x1900_Record> {
                        new JT809_JT1078_0x1900_Record{
                            AlarmType=1,
                            AVItemType=2,
                            ChannelId=3, 
                            FileSize=4,
                            MemType=5,
                            StartTime=Convert.ToDateTime("2019-07-15 10:10:10"),
                            EndTime=Convert.ToDateTime("2019-07-16 10:10:10"),
                            StreamType=6
                        },
                        new JT809_JT1078_0x1900_Record{
                            AlarmType=11,
                            AVItemType=21,
                            ChannelId=31,                            
                            FileSize=41,
                            MemType=51,
                            StartTime=Convert.ToDateTime("2019-06-15 10:10:10"),
                            EndTime=Convert.ToDateTime("2019-06-16 10:10:10"),
                            StreamType=61
                        }
                      }
                }
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x1900).ToHexString();
            Assert.Equal("D4C14231323334350000000000000000000000000002190200000045010000000203000000005D2BE082000000005D2D32020000000000000001020605000000041F000000005D045382000000005D05A502000000000000000B153D3300000029", hex);
        }

        [Fact]
        public void Test4()
        {
            var jT809_JT1078_0x1900 = JT809Serializer.Deserialize<JT809_JT1078_0x1900>("D4C14231323334350000000000000000000000000002190200000045010000000203000000005D2BE082000000005D2D32020000000000000001020605000000041F000000005D045382000000005D05A502000000000000000B153D3300000029".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x1900.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x1900.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.查询音视频资源目录应答消息, jT809_JT1078_0x1900.SubBusinessType);
            var jT809_JT1078_0x1900_0x1902 = jT809_JT1078_0x1900.SubBodies as JT809_JT1078_0x1900_0x1902;
            Assert.Equal(2u, jT809_JT1078_0x1900_0x1902.ItemNum);
            Assert.Equal(1, (byte)jT809_JT1078_0x1900_0x1902.Result);

            Assert.Equal(1u, jT809_JT1078_0x1900_0x1902.ItemList[0].AlarmType);
            Assert.Equal(2, jT809_JT1078_0x1900_0x1902.ItemList[0].AVItemType);
            Assert.Equal(3, jT809_JT1078_0x1900_0x1902.ItemList[0].ChannelId);
            Assert.Equal(4u, jT809_JT1078_0x1900_0x1902.ItemList[0].FileSize);
            Assert.Equal(5, jT809_JT1078_0x1900_0x1902.ItemList[0].MemType);
            Assert.Equal(Convert.ToDateTime("2019-07-15 10:10:10"), jT809_JT1078_0x1900_0x1902.ItemList[0].StartTime);
            Assert.Equal(Convert.ToDateTime("2019-07-16 10:10:10"), jT809_JT1078_0x1900_0x1902.ItemList[0].EndTime);
            Assert.Equal(6, jT809_JT1078_0x1900_0x1902.ItemList[0].StreamType);

            Assert.Equal(11u, jT809_JT1078_0x1900_0x1902.ItemList[1].AlarmType);
            Assert.Equal(21, jT809_JT1078_0x1900_0x1902.ItemList[1].AVItemType);
            Assert.Equal(31, jT809_JT1078_0x1900_0x1902.ItemList[1].ChannelId);
            Assert.Equal(41u, jT809_JT1078_0x1900_0x1902.ItemList[1].FileSize);
            Assert.Equal(51, jT809_JT1078_0x1900_0x1902.ItemList[1].MemType);
            Assert.Equal(Convert.ToDateTime("2019-06-15 10:10:10"), jT809_JT1078_0x1900_0x1902.ItemList[1].StartTime);
            Assert.Equal(Convert.ToDateTime("2019-06-16 10:10:10"), jT809_JT1078_0x1900_0x1902.ItemList[1].EndTime);
            Assert.Equal(61, jT809_JT1078_0x1900_0x1902.ItemList[1].StreamType);
        }
    }
}
