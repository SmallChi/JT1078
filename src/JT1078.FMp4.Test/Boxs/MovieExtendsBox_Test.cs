using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;

namespace JT1078.FMp4.Test.Boxs
{
    public class MovieExtendsBox_Test
    {
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact]
        public void Test1()
        {
            //000000286d7665780000002074726578000000000000000100000001000000000000000000000000
            //00 00 00 28
            //6d 76 65 78
            //00 00 00 20
            //74 72 65 78
            //00 
            //00 00 00
            //00 00 00 01
            //00 00 00 01
            //00 00 00 00
            //00 00 00 00
            //00 00 00 00
            MovieExtendsBox movieExtendsBox = new MovieExtendsBox();
            movieExtendsBox.TrackExtendsBoxs = new List<TrackExtendsBox>();
            TrackExtendsBox trackExtendsBox1 = new TrackExtendsBox();
            trackExtendsBox1.TrackID = 0x01;
            trackExtendsBox1.DefaultSampleDescriptionIndex = 0x01;
            movieExtendsBox.TrackExtendsBoxs.Add(trackExtendsBox1);
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x00000028]);
            movieExtendsBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("000000286d7665780000002074726578000000000000000100000001000000000000000000000000".ToUpper(), hex);
        }
    }
}
