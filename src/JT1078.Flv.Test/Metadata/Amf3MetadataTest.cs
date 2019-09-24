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

        [Fact]
        public void Amf3Metadata_FileSize_Test_1_1()
        {
            Amf3Metadata_FileSize amf3Metadata_FileSize = new Amf3Metadata_FileSize();
            amf3Metadata_FileSize.Value = 2005421.00000;
            var hex = amf3Metadata_FileSize.ToBuffer().ToArray().ToHexString();
            Assert.Equal("000866696C6573697A6500413E99AD00000000", hex);
        }

        [Fact]
        public void Amf3Metadata_FrameRate_Test_1_1()
        {
            Amf3Metadata_FrameRate amf3Metadata_FrameRate = new Amf3Metadata_FrameRate();
            amf3Metadata_FrameRate.Value = 29.16667;
            var hex = amf3Metadata_FrameRate.ToBuffer().ToArray().ToHexString();
            Assert.Equal("00096672616D657261746500403D2AAAE297396D", hex);
        }

        [Fact]
        public void Amf3Metadata_Height_Test_1_1()
        {
            Amf3Metadata_Height amf3Metadata_Height = new Amf3Metadata_Height();
            amf3Metadata_Height.Value = 960.00000;
            var hex = amf3Metadata_Height.ToBuffer().ToArray().ToHexString();
            Assert.Equal("000668656967687400408E000000000000", hex);
        }

        [Fact]
        public void Amf3Metadata_VideoCodecId_Test_1_1()
        {
            Amf3Metadata_VideoCodecId amf3Metadata_VideoCodecId = new Amf3Metadata_VideoCodecId();
            amf3Metadata_VideoCodecId.Value = 7.00000;
            var hex = amf3Metadata_VideoCodecId.ToBuffer().ToArray().ToHexString();
            Assert.Equal("000C766964656F636F646563696400401C000000000000", hex);
        }

        [Fact]
        public void Amf3Metadata_VideoDataRate_Test_1_1()
        {
            Amf3Metadata_VideoDataRate amf3Metadata_VideoDataRate = new Amf3Metadata_VideoDataRate();
            amf3Metadata_VideoDataRate.Value = 0.00000;
            var hex = amf3Metadata_VideoDataRate.ToBuffer().ToArray().ToHexString();
            Assert.Equal("000D766964656F6461746172617465000000000000000000", hex);
        }

        [Fact]
        public void Amf3Metadata_Width_Test_1_1()
        {
            Amf3Metadata_Width amf3Metadata_Width = new Amf3Metadata_Width();
            amf3Metadata_Width.Value = 544.00000;
            var hex = amf3Metadata_Width.ToBuffer().ToArray().ToHexString();
            Assert.Equal("00057769647468004081000000000000", hex);
        }
    }
}
