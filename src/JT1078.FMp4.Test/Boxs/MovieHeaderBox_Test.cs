using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;

namespace JT1078.FMp4.Test.Boxs
{
    public class MovieHeaderBox_Test
    {
        /// <summary>
        /// 使用doc/video/demo.mp4
        /// </summary>
        [Fact]
        public void Test1()
        {
            //00 00 00 6c--box size
            //6d 76 68 64--box type mvhd
            //00--version
            //00 00 00--flags
            //d9 3e 0b 8e--creation time
            //d9 3e 0b 8e--modification time
            //00 01 5f 90--time scale
            //00 09 c7 e4 --duration
            //00 01 00 00--rate
            //01 00--volume
            //00 00 00 00 00 00 00 00 00 00--保留位 10位
            //00 01 00 00 00 00 00 00 00 00 00 00--matrix 视频变换矩阵36 -12
            //00 00 00 00 00 01 00 00 00 00 00 00--matrix 视频变换矩阵36 -24
            //00 00 00 00 00 00 00 00 40 00 00 00--matrix 视频变换矩阵36 -36
            //00 00 00 00 00 00 00 00--pre - defined 24 - 8
            //00 00 00 00 00 00 00 00--pre - defined 24 - 16
            //00 00 00 00 00 00 00 00--pre - defined 24 - 24
            //00 00 00 03--next track id
            MovieHeaderBox movieHeaderBox = new MovieHeaderBox(version:0);
            movieHeaderBox.CreationTime = 0xd93e0b8e;
            movieHeaderBox.ModificationTime = 0xd93e0b8e;
            movieHeaderBox.Timescale = 0x00015f90;
            movieHeaderBox.Duration = 0x0009c7e4;
            movieHeaderBox.NextTrackID = 3;
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x6c]);
            movieHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("00 00 00 6c 6d 76 68 64 00 00 00 00 d9 3e 0b 8e d9 3e 0b 8e 00 01 5f 90 00 09 c7 e4 00 01 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 40 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 03".Replace(" ","").ToUpper(), hex);
        }
    }
}
