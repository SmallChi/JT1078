using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Flv.Extensions;

namespace JT1078.Flv.Test
{
    public  class FlvMuxer_Test
    {
        [Fact]
        public void FlvMuxer_Test_1() {
            FlvMuxer flvMuxer = new FlvMuxer();
            var buff = flvMuxer.FlvFirstFrame();
           var hex= buff.ToHexString();
        }
    }
}
