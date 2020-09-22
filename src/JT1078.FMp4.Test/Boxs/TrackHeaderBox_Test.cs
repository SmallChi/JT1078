using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;

namespace JT1078.FMp4.Test.Boxs
{
    public class TrackHeaderBox_Test
    {
        /// <summary>
        /// 使用doc/video/demo.mp4
        /// </summary>
        [Fact]
        public void Test1()
        {
            //00 00 00 5c--box size 92
            //74 6b 68 64--box type tkhd
            //00--version
            //00 00 01--flags
            //d9 3e 0b 8e--creation_time
            //d9 3e 0b 8e--modification_time
            //00 00 00 01--track_ID
            //00 00 00 00--reserved1 保留位1
            //00 09 ab 00--duration
            //00 00 00 00 00 00 00 00-- reserved2 保留位2
            //00 00--layer
            //00 00--alternate_group
            //01 00--volume
            //00 00-- reserved3 保留位3
            //00 01 00 00 00 00 00 00 00 00 00 00--matrix 视频变换矩阵36 -12
            //00 00 00 00 00 01 00 00 00 00 00 00--matrix 视频变换矩阵36 -24
            //00 00 00 00 00 00 00 00 40 00 00 00--matrix 视频变换矩阵36 -36
            //00 00 00 00--width
            //00 00 00 00--height
            TrackHeaderBox trackHeaderBox = new TrackHeaderBox(version:0,flags:1);
            trackHeaderBox.CreationTime = 0xd93e0b8e;
            trackHeaderBox.ModificationTime = 0xd93e0b8e;
            trackHeaderBox.TrackID = 1;
            trackHeaderBox.TrackIsAudio = true;
            trackHeaderBox.Duration = 0x0009ab00;
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x6c]);
            trackHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000005c746b686400000001d93e0b8ed93e0b8e00000001000000000009ab00000000000000000000000000010000000001000000000000000000000000000000010000000000000000000000000000400000000000000000000000".ToUpper(), hex);
        }
    }
}
