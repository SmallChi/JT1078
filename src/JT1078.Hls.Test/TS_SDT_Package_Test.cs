using JT1078.Hls.Enums;
using JT1078.Hls.MessagePack;
using JT1078.Protocol.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT1078.Hls.Test
{
    /// <summary>
    /// 使用doc/video/demo0.ts
    /// </summary>
    public class TS_SDT_Package_Test
    {
        [Fact]
        public void ToBufferTest()
        {
            TS_SDT_Package package = new TS_SDT_Package();
            package.Header = new TS_Header();
            package.Header.PID = 0x0011;
            package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
            package.Header.ContinuityCounter = 0;
            package.TableId = 0x42;
            package.TransportStreamId = 0x0001;
            package.VersionNumber = 0;
            package.CurrentNextIndicator = 0x01;
            package.SectionNumber = 0x00;
            package.LastSectionNumber = 0x00;  
            package.OriginalNetworkId = 0xFF01;
            package.Services = new List<TS_SDT_Service>();
            package.Services.Add(new TS_SDT_Service()
            {
                ServiceId = 0x0001,
                EITScheduleFlag = 0x00,
                EITPresentFollowingFlag = 0x00,
                RunningStatus = TS_SDT_Service_RunningStatus.运行,
                FreeCAMode = 0x00,
                Descriptors = new List<Descriptors.TS_SDT_Service_Descriptor> {
                     new Descriptors.TS_SDT_Service_Descriptor{
                             Tag=0x48,
                             ServiceType= TS_SDT_Service_Descriptor_ServiceType.数字电视业务,
                             ServiceProvider="FFmpeg",
                             ServiceName="Service01"
                     }
                }
            }) ;

            TSMessagePackWriter writer = new TSMessagePackWriter(new byte[188]);
            package.ToBuffer(ref writer);
            var patData = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("47 40 11 10 00 42 F0 25 00 01 C1 00 00 FF 01 FF 00 01 FC 80 14 48 12 01 06 46 46 6D 70 65 67 09 53 65 72 76 69 63 65 30 31 77 7C 43 CA FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF".Replace(" ", ""), patData);

        }
 

    }
}
