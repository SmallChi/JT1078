using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public class JT808_0x8103CustomId
    {
        JT808Serializer JT808Serializer;

        public JT808_0x8103CustomId()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1.AddJT808Configure(new DefaultGlobalConfig()).AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<IJT808Config>();
            JT808Serializer = new JT808Serializer(defaultConfig);
        }
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package
            {
                Header = new JT808Header
                {
                    MsgId = Enums.JT808MsgId.设置终端参数.ToUInt16Value(),
                    MsgNum = 10,
                    TerminalPhoneNo = "123456789",
                },
                Bodies = new JT808_0x8103
                {
                    ParamList = new List<JT808_0x8103_BodyBase>(),
                    CustomParamList = new List<JT808_0x8103_CustomBodyBase> {
                        new JT808_0x8103_0x0075() {
                              AudioOutputEnabled=1,
                               OSD=2,
                                RTS_EncodeMode=3,
                                 RTS_KF_Interval=4,
                                  RTS_Resolution=5,
                                   RTS_Target_CodeRate=6,
                                    RTS_Target_FPS=7,
                                     StreamStore_EncodeMode=8,
                                      StreamStore_KF_Interval=9,
                                       StreamStore_Resolution=10,
                                        StreamStore_Target_CodeRate=11,
                                         StreamStore_Target_FPS=12
                        },
                                new JT808_0x8103_0x0076() {
        AudioChannelTotal=1,
            AVChannelTotal=2,
            VudioChannelTotal=3,
             ParamLength=27,
            AVChannelRefTables=new List<JT808_0x8103_0x0076_AVChannelRefTable>{
                new JT808_0x8103_0x0076_AVChannelRefTable{  ChannelType=0, IsConnectCloudPlat=1, LogicChannelNo=2, PhysicalChannelNo=3 },

                    new JT808_0x8103_0x0076_AVChannelRefTable{ChannelType=4, IsConnectCloudPlat=5, LogicChannelNo=6, PhysicalChannelNo=7  },
                    new JT808_0x8103_0x0076_AVChannelRefTable{ChannelType=8, IsConnectCloudPlat=9, LogicChannelNo=10, PhysicalChannelNo=11  },

                    new JT808_0x8103_0x0076_AVChannelRefTable{ChannelType=12, IsConnectCloudPlat=13, LogicChannelNo=14, PhysicalChannelNo=15  },
                    new JT808_0x8103_0x0076_AVChannelRefTable{ ChannelType=16, IsConnectCloudPlat=17, LogicChannelNo=18, PhysicalChannelNo=19 },
                        new JT808_0x8103_0x0076_AVChannelRefTable{ ChannelType=20, IsConnectCloudPlat=21, LogicChannelNo=22, PhysicalChannelNo=23 }
            }
        },
                                new JT808_0x8103_0x0077{
                                        NeedSetChannelTotal=2,
                                         ParamLength=43,
                                        jT808_0X8103_0X0077_SignalChannels=new List<JT808_0x8103_0x0077_SignalChannel>{
                                            new JT808_0x8103_0x0077_SignalChannel{
                                                LogicChannelNo =1, OSD=2, RTS_EncodeMode=3, RTS_KF_Interval=4, RTS_Resolution=5,
                                                RTS_Target_CodeRate =6, RTS_Target_FPS=7, StreamStore_EncodeMode=8, StreamStore_KF_Interval=9, StreamStore_Resolution=10,
                                                StreamStore_Target_CodeRate=11, StreamStore_Target_FPS=12},
                                                new JT808_0x8103_0x0077_SignalChannel{
                                                LogicChannelNo=1, OSD=2, RTS_EncodeMode=3, RTS_KF_Interval=4, RTS_Resolution=5,
                                                RTS_Target_CodeRate =6, RTS_Target_FPS=7, StreamStore_EncodeMode=8, StreamStore_KF_Interval=9, StreamStore_Resolution=10,
                                                StreamStore_Target_CodeRate=11, StreamStore_Target_FPS=12}
                                        }
                                },
                                new JT808_0x8103_0x0079{
                                        BeginMinute=1, Duration=2, StorageThresholds=3
                                },
                                new JT808_0x8103_0x007A{
                                        AlarmShielding=1
                                },
                                new JT808_0x8103_0x007B{
                                        NuclearLoadNumber=1, FatigueThreshold=2
                                },
                                new JT808_0x8103_0x007C{
                                        SleepWakeMode=1, TimerWakeDaySet=2, WakeConditionType=3,
                                        jT808_0X8103_0X007C_TimerWakeDayParamter=new JT808_0x8103_0x007C_TimerWakeDayParamter{
                                            TimePeriod1CloseTime="12",
                                            TimePeriod1WakeTime="23",
                                                TimePeriod2CloseTime="34",
                                                TimePeriod2WakeTime="45",
                                                TimePeriod3CloseTime="56",
                                                TimePeriod3WakeTime="67",
                                                    TimePeriod4CloseTime="78",
                                                    TimePeriod4WakeTime="89",
                                                    TimerWakeEnableFlag=10
                                        }
                                }
                    }
                }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT808Package.Bodies);
            var hex = JT808Serializer.Serialize(jT808Package).ToHexString();
            Assert.Equal("7E8103009C000123456789000A070000007515030500040700000006080A00090C0000000B000201000000761B02010303020001070604050B0A08090F0E0C0D1312101117161415000000772B0201030500040700000006080A00090C0000000B000201030500040700000006080A00090C0000000B000200000079030302010000007A04000000010000007B0201020000007C140103020A00230012004500340067005600890078587E", hex);
        }

        [Fact]
        public void Test2()
        {
           var str = "{\"ParamList\":[],\"CustomParamList\":[{\"ParamId\":117,\"ParamLength\":21,\"RTS_EncodeMode\":3,\"RTS_Resolution\":5,\"RTS_KF_Interval\":4,\"RTS_Target_FPS\":7,\"RTS_Target_CodeRate\":6,\"StreamStore_EncodeMode\":8,\"StreamStore_Resolution\":10,\"StreamStore_KF_Interval\":9,\"StreamStore_Target_FPS\":12,\"StreamStore_Target_CodeRate\":11,\"OSD\":2,\"AudioOutputEnabled\":1},{\"ParamId\":118,\"ParamLength\":27,\"AVChannelTotal\":2,\"AudioChannelTotal\":1,\"VudioChannelTotal\":3,\"AVChannelRefTables\":[{\"PhysicalChannelNo\":3,\"LogicChannelNo\":2,\"ChannelType\":0,\"IsConnectCloudPlat\":1},{\"PhysicalChannelNo\":7,\"LogicChannelNo\":6,\"ChannelType\":4,\"IsConnectCloudPlat\":5},{\"PhysicalChannelNo\":11,\"LogicChannelNo\":10,\"ChannelType\":8,\"IsConnectCloudPlat\":9},{\"PhysicalChannelNo\":15,\"LogicChannelNo\":14,\"ChannelType\":12,\"IsConnectCloudPlat\":13},{\"PhysicalChannelNo\":19,\"LogicChannelNo\":18,\"ChannelType\":16,\"IsConnectCloudPlat\":17},{\"PhysicalChannelNo\":23,\"LogicChannelNo\":22,\"ChannelType\":20,\"IsConnectCloudPlat\":21}]},{\"ParamId\":119,\"ParamLength\":43,\"NeedSetChannelTotal\":2,\"jT808_0X8103_0X0077_SignalChannels\":[{\"LogicChannelNo\":1,\"RTS_EncodeMode\":3,\"RTS_Resolution\":5,\"RTS_KF_Interval\":4,\"RTS_Target_FPS\":7,\"RTS_Target_CodeRate\":6,\"StreamStore_EncodeMode\":8,\"StreamStore_Resolution\":10,\"StreamStore_KF_Interval\":9,\"StreamStore_Target_FPS\":12,\"StreamStore_Target_CodeRate\":11,\"OSD\":2},{\"LogicChannelNo\":1,\"RTS_EncodeMode\":3,\"RTS_Resolution\":5,\"RTS_KF_Interval\":4,\"RTS_Target_FPS\":7,\"RTS_Target_CodeRate\":6,\"StreamStore_EncodeMode\":8,\"StreamStore_Resolution\":10,\"StreamStore_KF_Interval\":9,\"StreamStore_Target_FPS\":12,\"StreamStore_Target_CodeRate\":11,\"OSD\":2}]},{\"ParamId\":121,\"ParamLength\":3,\"StorageThresholds\":3,\"Duration\":2,\"BeginMinute\":1},{\"ParamId\":122,\"ParamLength\":4,\"AlarmShielding\":1},{\"ParamId\":123,\"ParamLength\":2,\"NuclearLoadNumber\":1,\"FatigueThreshold\":2},{\"ParamId\":124,\"ParamLength\":20,\"SleepWakeMode\":1,\"WakeConditionType\":3,\"TimerWakeDaySet\":2,\"jT808_0X8103_0X007C_TimerWakeDayParamter\":{\"TimerWakeEnableFlag\":10,\"TimePeriod1WakeTime\":\"23\",\"TimePeriod1CloseTime\":\"12\",\"TimePeriod2WakeTime\":\"45\",\"TimePeriod2CloseTime\":\"34\",\"TimePeriod3WakeTime\":\"67\",\"TimePeriod3CloseTime\":\"56\",\"TimePeriod4WakeTime\":\"89\",\"TimePeriod4CloseTime\":\"78\"}}],\"SkipSerialization\":false}";
            byte[] bytes = "7E8103009C000123456789000A070000007515030500040700000006080A00090C0000000B000201000000761B02010303020001070604050B0A08090F0E0C0D1312101117161415000000772B0201030500040700000006080A00090C0000000B000201030500040700000006080A00090C0000000B000200000079030302010000007A04000000010000007B0201020000007C140103020A00230012004500340067005600890078587E".ToHexBytes();
            JT808Package jT808_0X8103 = JT808Serializer.Deserialize(bytes);
            Assert.Equal(Enums.JT808MsgId.设置终端参数.ToUInt16Value(), jT808_0X8103.Header.MsgId);
            Assert.Equal(10, jT808_0X8103.Header.MsgNum);
            Assert.Equal("123456789", jT808_0X8103.Header.TerminalPhoneNo);

            JT808_0x8103 JT808Bodies = (JT808_0x8103)jT808_0X8103.Bodies;
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0X8103.Bodies), str);
        }

        [Fact]
        public void Test3()
        {
            JT808_0x8103 jT808_0x8103 = new JT808_0x8103
            {
                ParamList = new List<JT808_0x8103_BodyBase>(),
                CustomParamList = new List<JT808_0x8103_CustomBodyBase> {
                        new JT808_0x8103_0x0075() {
                              AudioOutputEnabled=1,
                               OSD=2,
                                RTS_EncodeMode=3,
                                 RTS_KF_Interval=4,
                                  RTS_Resolution=5,
                                   RTS_Target_CodeRate=6,
                                    RTS_Target_FPS=7,
                                     StreamStore_EncodeMode=8,
                                      StreamStore_KF_Interval=9,
                                       StreamStore_Resolution=10,
                                        StreamStore_Target_CodeRate=11,
                                         StreamStore_Target_FPS=12
                        },
                                new JT808_0x8103_0x0076() {
        AudioChannelTotal=1,
            AVChannelTotal=2,
            VudioChannelTotal=3,
             ParamLength=27,
            AVChannelRefTables=new List<JT808_0x8103_0x0076_AVChannelRefTable>{                 
                new JT808_0x8103_0x0076_AVChannelRefTable{  ChannelType=0, IsConnectCloudPlat=1, LogicChannelNo=2, PhysicalChannelNo=3 },

                    new JT808_0x8103_0x0076_AVChannelRefTable{ChannelType=4, IsConnectCloudPlat=5, LogicChannelNo=6, PhysicalChannelNo=7  },
                    new JT808_0x8103_0x0076_AVChannelRefTable{ChannelType=8, IsConnectCloudPlat=9, LogicChannelNo=10, PhysicalChannelNo=11  },

                    new JT808_0x8103_0x0076_AVChannelRefTable{ChannelType=12, IsConnectCloudPlat=13, LogicChannelNo=14, PhysicalChannelNo=15  },
                    new JT808_0x8103_0x0076_AVChannelRefTable{ ChannelType=16, IsConnectCloudPlat=17, LogicChannelNo=18, PhysicalChannelNo=19 },
                        new JT808_0x8103_0x0076_AVChannelRefTable{ ChannelType=20, IsConnectCloudPlat=21, LogicChannelNo=22, PhysicalChannelNo=23 }
            }
        },
                                new JT808_0x8103_0x0077{
                                        NeedSetChannelTotal=2,
                                         ParamLength=43,
                                        jT808_0X8103_0X0077_SignalChannels=new List<JT808_0x8103_0x0077_SignalChannel>{
                                            new JT808_0x8103_0x0077_SignalChannel{
                                                LogicChannelNo =1, OSD=2, RTS_EncodeMode=3, RTS_KF_Interval=4, RTS_Resolution=5,
                                                RTS_Target_CodeRate =6, RTS_Target_FPS=7, StreamStore_EncodeMode=8, StreamStore_KF_Interval=9, StreamStore_Resolution=10,
                                                StreamStore_Target_CodeRate=11, StreamStore_Target_FPS=12},
                                                new JT808_0x8103_0x0077_SignalChannel{
                                                LogicChannelNo=1, OSD=2, RTS_EncodeMode=3, RTS_KF_Interval=4, RTS_Resolution=5,
                                                RTS_Target_CodeRate =6, RTS_Target_FPS=7, StreamStore_EncodeMode=8, StreamStore_KF_Interval=9, StreamStore_Resolution=10,
                                                StreamStore_Target_CodeRate=11, StreamStore_Target_FPS=12}
                                        }
                                },
                                new JT808_0x8103_0x0079{
                                        BeginMinute=1, Duration=2, StorageThresholds=3
                                },
                                new JT808_0x8103_0x007A{
                                        AlarmShielding=1
                                },
                                new JT808_0x8103_0x007B{
                                        NuclearLoadNumber=1, FatigueThreshold=2
                                },
                                new JT808_0x8103_0x007C{
                                        SleepWakeMode=1, TimerWakeDaySet=2, WakeConditionType=3,
                                        jT808_0X8103_0X007C_TimerWakeDayParamter=new JT808_0x8103_0x007C_TimerWakeDayParamter{
                                            TimePeriod1CloseTime="12",
                                            TimePeriod1WakeTime="23",
                                                TimePeriod2CloseTime="34",
                                                TimePeriod2WakeTime="45",
                                                TimePeriod3CloseTime="56",
                                                TimePeriod3WakeTime="67",
                                                    TimePeriod4CloseTime="78",
                                                    TimePeriod4WakeTime="89",
                                                    TimerWakeEnableFlag=10
                                        }
                                }
                    }
            };
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0x8103);
            var hex = JT808Serializer.Serialize(jT808_0x8103).ToHexString();
            Assert.Equal("070000007515030500040700000006080A00090C0000000B000201000000761B02010303020001070604050B0A08090F0E0C0D1312101117161415000000772B0201030500040700000006080A00090C0000000B000201030500040700000006080A00090C0000000B000200000079030302010000007A04000000010000007B0201020000007C140103020A00230012004500340067005600890078", hex);
        }

        [Fact]
        public void Test4()
        {
            byte[] bytes = "070000007515030500040700000006080A00090C0000000B000201000000761B02010303020001070604050B0A08090F0E0C0D1312101117161415000000772B0201030500040700000006080A00090C0000000B000201030500040700000006080A00090C0000000B000200000079030302010000007A04000000010000007B0201020000007C140103020A00230012004500340067005600890078".ToHexBytes();
            var jT808_0X8103 = JT808Serializer.Deserialize<JT808_0x8103>(bytes);

            Assert.Equal("{\"ParamList\":[],\"CustomParamList\":[{\"ParamId\":117,\"ParamLength\":21,\"RTS_EncodeMode\":3,\"RTS_Resolution\":5,\"RTS_KF_Interval\":4,\"RTS_Target_FPS\":7,\"RTS_Target_CodeRate\":6,\"StreamStore_EncodeMode\":8,\"StreamStore_Resolution\":10,\"StreamStore_KF_Interval\":9,\"StreamStore_Target_FPS\":12,\"StreamStore_Target_CodeRate\":11,\"OSD\":2,\"AudioOutputEnabled\":1},{\"ParamId\":118,\"ParamLength\":27,\"AVChannelTotal\":2,\"AudioChannelTotal\":1,\"VudioChannelTotal\":3,\"AVChannelRefTables\":[{\"PhysicalChannelNo\":3,\"LogicChannelNo\":2,\"ChannelType\":0,\"IsConnectCloudPlat\":1},{\"PhysicalChannelNo\":7,\"LogicChannelNo\":6,\"ChannelType\":4,\"IsConnectCloudPlat\":5},{\"PhysicalChannelNo\":11,\"LogicChannelNo\":10,\"ChannelType\":8,\"IsConnectCloudPlat\":9},{\"PhysicalChannelNo\":15,\"LogicChannelNo\":14,\"ChannelType\":12,\"IsConnectCloudPlat\":13},{\"PhysicalChannelNo\":19,\"LogicChannelNo\":18,\"ChannelType\":16,\"IsConnectCloudPlat\":17},{\"PhysicalChannelNo\":23,\"LogicChannelNo\":22,\"ChannelType\":20,\"IsConnectCloudPlat\":21}]},{\"ParamId\":119,\"ParamLength\":43,\"NeedSetChannelTotal\":2,\"jT808_0X8103_0X0077_SignalChannels\":[{\"LogicChannelNo\":1,\"RTS_EncodeMode\":3,\"RTS_Resolution\":5,\"RTS_KF_Interval\":4,\"RTS_Target_FPS\":7,\"RTS_Target_CodeRate\":6,\"StreamStore_EncodeMode\":8,\"StreamStore_Resolution\":10,\"StreamStore_KF_Interval\":9,\"StreamStore_Target_FPS\":12,\"StreamStore_Target_CodeRate\":11,\"OSD\":2},{\"LogicChannelNo\":1,\"RTS_EncodeMode\":3,\"RTS_Resolution\":5,\"RTS_KF_Interval\":4,\"RTS_Target_FPS\":7,\"RTS_Target_CodeRate\":6,\"StreamStore_EncodeMode\":8,\"StreamStore_Resolution\":10,\"StreamStore_KF_Interval\":9,\"StreamStore_Target_FPS\":12,\"StreamStore_Target_CodeRate\":11,\"OSD\":2}]},{\"ParamId\":121,\"ParamLength\":3,\"StorageThresholds\":3,\"Duration\":2,\"BeginMinute\":1},{\"ParamId\":122,\"ParamLength\":4,\"AlarmShielding\":1},{\"ParamId\":123,\"ParamLength\":2,\"NuclearLoadNumber\":1,\"FatigueThreshold\":2},{\"ParamId\":124,\"ParamLength\":20,\"SleepWakeMode\":1,\"WakeConditionType\":3,\"TimerWakeDaySet\":2,\"jT808_0X8103_0X007C_TimerWakeDayParamter\":{\"TimerWakeEnableFlag\":10,\"TimePeriod1WakeTime\":\"23\",\"TimePeriod1CloseTime\":\"12\",\"TimePeriod2WakeTime\":\"45\",\"TimePeriod2CloseTime\":\"34\",\"TimePeriod3WakeTime\":\"67\",\"TimePeriod3CloseTime\":\"56\",\"TimePeriod4WakeTime\":\"89\",\"TimePeriod4CloseTime\":\"78\"}}],\"SkipSerialization\":false}", Newtonsoft.Json.JsonConvert.SerializeObject(jT808_0X8103));
        }
    }
    class DefaultGlobalConfig : GlobalConfigBase
    {
        public override string ConfigId => "Default";
    }
}
