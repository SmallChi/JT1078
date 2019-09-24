using JT1078.Flv.Metadata;
using JT1078.Flv.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JT1078.Flv.Test.Metadata
{
    public class Amf3MetadataTest
    {
        [Fact]
        public void Amf3Metadata_Duration_Test_1_1()
        {
            Amf3Metadata_Duration amf3Metadata = new Amf3Metadata_Duration();
            amf3Metadata.Value = 7.22100;
            var hex=amf3Metadata.ToBuffer().ToArray().ToHexString();
            Assert.Equal("00086475726174696F6E00401CE24DD2F1A9FC", hex);
        }

        [Fact]
        public void Test1()
        {    
            byte[] d1 = new byte[] { 0xFC, 0xA9, 0xF1, 0xD2, 0x4D, 0xE2, 0x1c, 0x40 };
            var buffer = BitConverter.GetBytes(7.22100);
            //flv需要倒序的
        }
    }
}
