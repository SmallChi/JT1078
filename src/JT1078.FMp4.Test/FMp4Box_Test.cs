using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;

namespace JT1078.FMp4.Test
{
    public class FMp4Box_Test
    {
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "ftyp")]
        public void ftyp_test()
        {
            var MinorVersion = Encoding.ASCII.GetString(new byte[4] { 0,0,2,0});
            FileTypeBox fileTypeBox = new FileTypeBox();
            fileTypeBox.MajorBrand = "isom";
            fileTypeBox.MinorVersion = "\0\0\u0002\0";
            fileTypeBox.CompatibleBrands.Add("isom");
            fileTypeBox.CompatibleBrands.Add("iso2");
            fileTypeBox.CompatibleBrands.Add("avc1");
            fileTypeBox.CompatibleBrands.Add("iso6");
            fileTypeBox.CompatibleBrands.Add("mp41");
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x25]);
            fileTypeBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("000000246674797069736f6d0000020069736f6d69736f326176633169736f366d703431".ToUpper(), hex);
        }

        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_mvhd")]
        public void moov_mvhd_test()
        {
            MovieHeaderBox movieHeaderBox = new MovieHeaderBox(0);
            movieHeaderBox.CreationTime = 0;
            movieHeaderBox.ModificationTime = 0;
            movieHeaderBox.Timescale = 1000;
            movieHeaderBox.Duration = 0;
            movieHeaderBox.NextTrackID = 2;
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            movieHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000006c6d766864000000000000000000000000000003e8000000000001000001000000000000000000000000010000000000000000000000000000000100000000000000000000000000004000000000000000000000000000000000000000000000000000000000000002".ToUpper(), hex);
        }

        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "trak_tkhd")]
        public void trak_tkhd_test()
        {   
            TrackHeaderBox trackHeaderBox = new TrackHeaderBox(0,3);
            trackHeaderBox.CreationTime = 0;
            trackHeaderBox.ModificationTime = 0;
            trackHeaderBox.TrackID = 1;
            trackHeaderBox.Duration = 0;
            trackHeaderBox.Width = 544u;
            trackHeaderBox.Height = 960u;
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            trackHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000005C746B68640000000300000000000000000000000100000000000000000000000000000000000000000000000000010000000000000000000000000000000100000000000000000000000000004000000000000220000003C0".ToUpper(), hex);
        }

        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "trak_mdia")]
        public void trak_mdia_test()
        {


        }

        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "trak_mdia_mdhd")]
        public void trak_mdia_mdhd_test()
        {
            
            MediaHeaderBox mediaHeaderBox = new MediaHeaderBox(0, 0);
            mediaHeaderBox.CreationTime = 0;
            mediaHeaderBox.ModificationTime = 0;
            mediaHeaderBox.Timescale = 0x00124f80;
            mediaHeaderBox.Duration = 0;
            mediaHeaderBox.Language = "und";
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            mediaHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            //000000206d64686400000000000000000000000000124f800000000055c40000
            //00000020
            //6d646864
            //00000000
            //00000000
            //00000000
            //00124f80
            //00000000
            //55c4
            //0000
            Assert.Equal("000000206d64686400000000000000000000000000124f800000000055c40000".ToUpper(), hex);
        }
    }
}
