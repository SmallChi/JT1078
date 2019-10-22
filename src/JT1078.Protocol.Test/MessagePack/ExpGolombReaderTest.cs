using JT1078.Protocol.MessagePack;
using System.Buffers.Binary;
using Xunit;


namespace JT1078.Protocol.Test.MessagePack
{
    public class ExpGolombReaderTest
    {
        [Fact]
        public void Test1()
        {
            ExpGolombReader h264GolombReader = new ExpGolombReader(new byte[] { 103, 77, 0, 20, 149, 168, 88, 37, 144, 0 });
            var result = h264GolombReader.ReadSPS();
            Assert.Equal(77, result.profileIdc);
            Assert.Equal(0u, result.profileCompat);
            Assert.Equal(20, result.levelIdc);
            Assert.Equal(352, result.width);
            Assert.Equal(288, result.height);
            //profileIdc 77
            //profileCompat 0
            //levelIdc 20
            //picOrderCntType 2
            //picWidthInMbsMinus1 21
            //picHeightInMapUnitsMinus1 17
            //frameMbsOnlyFlag 1
            //width 352
            //height 288
        }
    }
}
