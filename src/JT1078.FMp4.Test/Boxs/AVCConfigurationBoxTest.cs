using JT1078.FMp4.MessagePack;
using JT1078.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JT1078.FMp4.Test.Boxs
{
    public class AVCConfigurationBoxTest
    {
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact]
        public void Test1()
        {
            //0000002f617663430164001fffe100176764001facd940881e684000f4240037b40883c60c658001000568efbcb000
            //00 00 00 2f
            //61 76 63 43
            //01
            //64
            //00
            //1f
            //ff
            //e1 numOfSequenceParameterSets 1
            //00 17 
            //67 64 00 1f 
            //ac d9 40 88
            //1e 68 40 00
            //f4 24 00 37
            //b4 08 83 c6
            //0c 65 80 
            //01 NumOfPictureParameterSets 1
            //00 05
            //68 ef bc b0 00
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
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(new byte[0x0000002f]);
            aVCConfigurationBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000002f617663430164001fffe100176764001facd940881e684000f4240037b40883c60c658001000568efbcb000".ToUpper(), hex);
        }
    }
}
