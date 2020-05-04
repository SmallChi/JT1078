using JT1078.Hls.Descriptors;
using JT1078.Hls.Enums;
using JT1078.Hls.MessagePack;
using JT1078.Protocol.Extensions;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using Xunit;

namespace JT1078.Hls.Test
{
    /// <summary>
    /// 使用demo0.ts
    /// </summary>
    public class TS_Package_Test
    {
        [Fact]
        public void ToBufferTest()
        {
            TS_Package package = new TS_Package();
            package.Header = new TS_Header();
            package.Header.PID = 4096;
            package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
            package.Header.PayloadUnitStartIndicator = 1;
            package.Header.ContinuityCounter = 0;
            TSMessagePackWriter writer = new TSMessagePackWriter(new byte[188]);
            package.ToBuffer(ref writer);
            var psData = writer.FlushAndGetArray().ToHexString();
        }
    }
}
