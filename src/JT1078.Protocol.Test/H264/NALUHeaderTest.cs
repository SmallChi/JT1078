using JT1078.Protocol.H264;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT1078.Flv.Test.H264
{
    public  class NALUHeaderTest
    {
        [Fact]
        public void Test1()
        {
            NALUHeader header = new NALUHeader(0xc0);
            Assert.Equal(1, header.ForbiddenZeroBit);
            Assert.Equal(2, header.NalRefIdc);
            Assert.Equal(NalUnitType.None, header.NalUnitType);
        }
    }
}
