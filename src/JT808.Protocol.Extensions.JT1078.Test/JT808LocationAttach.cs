using JT808.Protocol.Extensions.JT1078.MessageBody;
using JT808.Protocol.Interfaces;
using JT808.Protocol.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace JT808.Protocol.Extensions.JT1078.Test
{
    public class JT808LocationAttach
    {
        JT808Serializer JT808Serializer;

        public JT808LocationAttach()
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
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200
            {
                AlarmFlag = 1,
                Altitude = 40,
                GPSTime = DateTime.Parse("2018-07-15 10:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                 JT808CustomLocationAttachData = new Dictionary<byte, JT808_0x0200_CustomBodyBase>()
            };
            jT808UploadLocationRequest.JT808CustomLocationAttachData.Add(0x14, new JT808_0x0200_0x14
            {
                 VideoRelateAlarm = 100
            });
            jT808UploadLocationRequest.JT808CustomLocationAttachData.Add(0x15, new JT808_0x0200_0x15
            {
                 VideoSignalLoseAlarmStatus = 100
            });
            jT808UploadLocationRequest.JT808CustomLocationAttachData.Add(0x16, new JT808_0x0200_0x16
            {
                 VideoSignalOcclusionAlarmStatus = 100
            });
            jT808UploadLocationRequest.JT808CustomLocationAttachData.Add(0x17, new JT808_0x0200_0x17
            {
                 StorageFaultAlarmStatus = 100
            });
            jT808UploadLocationRequest.JT808CustomLocationAttachData.Add(0x18, new JT808_0x0200_0x18
            {
                 AbnormalDrivingBehaviorAlarmInfo = 100
            });
            var hex = JT808Serializer.Serialize(jT808UploadLocationRequest).ToHexString();
            Assert.Equal("000000010000000200BA7F0E07E4F11C0028003C00001807151010101404000000641504000000641604000000641702006418020064", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bodys = "000000010000000200BA7F0E07E4F11C0028003C00001807151010101404000000641504000000641604000000641702006418020064".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = JT808Serializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal((uint)1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal((uint)2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal((uint)100,  JT808Serializer.Deserialize<JT808_0x0200_0x14>(jT808UploadLocationRequest.JT808CustomLocationAttachOriginalData[0x14]).VideoRelateAlarm);
            Assert.Equal((uint)100, JT808Serializer.Deserialize<JT808_0x0200_0x15>(jT808UploadLocationRequest.JT808CustomLocationAttachOriginalData[0x15]).VideoSignalLoseAlarmStatus);
            Assert.Equal((uint)100, JT808Serializer.Deserialize<JT808_0x0200_0x16>(jT808UploadLocationRequest.JT808CustomLocationAttachOriginalData[0x16]).VideoSignalOcclusionAlarmStatus);
            Assert.Equal((uint)100, JT808Serializer.Deserialize<JT808_0x0200_0x17>(jT808UploadLocationRequest.JT808CustomLocationAttachOriginalData[0x17]).StorageFaultAlarmStatus);
            Assert.Equal((uint)100, JT808Serializer.Deserialize<JT808_0x0200_0x18>(jT808UploadLocationRequest.JT808CustomLocationAttachOriginalData[0x17]).AbnormalDrivingBehaviorAlarmInfo);
        }
    }
}
