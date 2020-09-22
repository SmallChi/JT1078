using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;

namespace JT1078.FMp4.Test.Boxs
{
    public class MediaHeaderBox_Test
    {
        /// <summary>
        /// 使用doc/video/demo.mp4
        /// </summary>
        [Fact]
        public void Test1()
        {
            //00 00 00 20--box size 32
            //6d 64 68 64--box type mdhd
            //00--version
            //00 00 00--flags
            //d9 3e 0b 8e--creation_time
            //d9 3e 0b 8e--modification_time
            //00 00 bb 80--timescale
            //00 05 28 00--duration
            //55 c4--(pad(1) + language(15))
            //00 00--pre_defined
            MediaHeaderBox movieHeaderBox = new MediaHeaderBox(version:0);
            movieHeaderBox.CreationTime = 0xd93e0b8e;
            movieHeaderBox.ModificationTime = 0xd93e0b8e;
            movieHeaderBox.Timescale = 0x0000bb80;
            movieHeaderBox.Duration = 0x00052800;
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x6c]);
            movieHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("000000206d64686400000000d93e0b8ed93e0b8e0000bb800005280055c40000".ToUpper(), hex);
        }
    }
}
