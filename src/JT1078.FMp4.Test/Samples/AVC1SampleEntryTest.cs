using JT1078.FMp4.MessagePack;
using JT1078.FMp4.Samples;
using JT1078.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JT1078.FMp4.Test.Samples
{
    public class AVC1SampleEntryTest
    {
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact]
        public void Test1()
        {
            //0000008561766331000000000000000100000000000000000000000000000000022003C0004800000048000000000000000100000000000000000000000000000000000000000000000000000000000000000018FFFF0000002F617663430164001FFFE100176764001FACD940881E684000F4240037B40883C60C658001000568EFBCB000
            //1.avc1
            //0000008561766331000000000000000100000000000000000000000000000000022003C0004800000048000000000000000100000000000000000000000000000000000000000000000000000000000000000018FFFF
            //2.avc1->avcc
            //0000002F617663430164001FFFE100176764001FACD940881E684000F4240037B40883C60C658001000568EFBCB000
            //0000008561766331000000000000000100000000000000000000000000000000022003c0004800000048000000000000000100000000000000000000000000000000000000000000000000000000000000000018ffff
            
            //-------------avc1
            //00 00 00 85
            //61 76 63 31
            //00 00 00
            //00 00 00
            //00 01
            //00 00
            //00 00
            //00 00 00 00
            //00 00 00 00
            //00 00 00 00
            //02 20
            //03 c0
            //00 48 00 00
            //00 48 00 00
            //00 00 00 00
            //00 01
            //00
            //00 00 00 00
            //00 00 00 00
            //00 00 00 00
            //00 00 00 00
            //00 00 00 00
            //00 00 00 00
            //00 00 00 00
            //00 00 00
            //00 18
            //ff ff
            AVC1SampleEntry aVC1SampleEntry = new AVC1SampleEntry();
            aVC1SampleEntry.Width = 0x0220;
            aVC1SampleEntry.Height = 0x03c0;
            AVCConfigurationBox aVCConfigurationBox = new AVCConfigurationBox();
            aVCConfigurationBox.AVCProfileIndication = 0x64;
            aVCConfigurationBox.ProfileCompatibility = 0;
            aVCConfigurationBox.AVCLevelIndication = 0x1f;
            aVCConfigurationBox.LengthSizeMinusOne = 0xff;
            aVCConfigurationBox.SPSs = new List<byte[]>()
            {
                "6764001facd940881e684000f4240037b40883c60c6580".ToHexBytes()
            };
            aVCConfigurationBox.PPSs = new List<byte[]>()
            {
                "68efbcb000".ToHexBytes()
            };
            aVC1SampleEntry.AVCConfigurationBox = aVCConfigurationBox;
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(new byte[0x00000085]);
            aVC1SampleEntry.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000008561766331000000000000000100000000000000000000000000000000022003C0004800000048000000000000000100000000000000000000000000000000000000000000000000000000000000000018FFFF0000002F617663430164001FFFE100176764001FACD940881E684000F4240037B40883C60C658001000568EFBCB000".ToUpper(), hex);
        }
    }
}
