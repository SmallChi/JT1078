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
    public class TS_PAT_Package_Test
    {
        [Fact]
        public void ToBufferTest()
        {
            //47 40 00 10 00 00 B0 0D 00 01 C1 00 00 00 01 F0 00 2A B1 04 B2 FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF
            //----------PAT
            //47 
            //40 00 
            //10 
            //00 
            //00 
            //B0 0D 
            //00 01 
            //C1 
            //00 
            //00 
            //00 01 
            //F0 00 
            //2A B1 04 B2
            TS_PAT_Package package = new TS_PAT_Package();
            package.Header = new TS_Header();
            package.Header.PID = 0;
            package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
            package.Header.ContinuityCounter = 0;
            package.TableId = 0;
            package.TransportStreamId = 0x0001;
            package.VersionNumber = 0;
            package.Programs = new List<TS_PAT_Program>();
            package.Programs.Add(new TS_PAT_Program()
            {
                ProgramNumber = 0x0001,
                PID = 0x1000,
            });
            TSMessagePackWriter writer = new TSMessagePackWriter(new byte[188]);
            package.ToBuffer(ref writer);
            var patData=writer.FlushAndGetArray().ToHexString();
            Assert.Equal("47 40 00 10 00 00 B0 0D 00 01 C1 00 00 00 01 F0 00 2A B1 04 B2 FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF".Replace(" ",""), patData);
        }
    }
}
