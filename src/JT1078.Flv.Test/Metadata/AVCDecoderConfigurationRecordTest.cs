using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Flv.Extensions;

namespace JT1078.Flv.Test.Metadata
{
    public class AVCDecoderConfigurationRecordTest
    {
        [Fact]
        public void Test1()
        {
            Span<byte> buffer = new byte[1024];
            FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
            AVCDecoderConfigurationRecord aVCDecoderConfigurationRecord = new AVCDecoderConfigurationRecord();
            aVCDecoderConfigurationRecord.AVCProfileIndication = 0x64;
            aVCDecoderConfigurationRecord.ProfileCompatibility = 0;
            aVCDecoderConfigurationRecord.AVCLevelIndication =0x1F;
            aVCDecoderConfigurationRecord.NumOfPictureParameterSets = 1;
            aVCDecoderConfigurationRecord.SPSBuffer = new byte[] { 
                0x67,0x64,0x00,0x1F,
                0xAC,0xD9,0x40,0x88,
                0x1E,0x68,0x40,0x00,
                0x00,0x03,0x01,0x80,
                0x00,0x00,0x57,0x83,
                0xC6,0x0C,0x65,0x80
            };
            aVCDecoderConfigurationRecord.PPSBuffer = new byte[] {
                0x68,0xEB,0xE3,0xCB,0x22,0xC0
            };
            flvMessagePackWriter.WriteAVCDecoderConfigurationRecord(aVCDecoderConfigurationRecord);
            var hexData = flvMessagePackWriter.FlushAndGetArray().ToHexString();
            Assert.Equal("0164001FFFE100186764001FACD940881E6840000003018000005783C60C658001000668EBE3CB22C0", hexData);
            //0164001FFFE100186764001FACD940881E6840000003018000005783C60C658001000668EBE3CB22C0
            //0164001FFFE100186764001FACD940881E6840000003018000005783C60C658001000668EBE3CB22C0
        }
    }
}
