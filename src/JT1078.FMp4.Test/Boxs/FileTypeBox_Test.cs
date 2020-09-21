using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;

namespace JT1078.FMp4.Test.Boxs
{
    public class FileTypeBox_Test
    {
        /// <summary>
        /// 使用doc/video/demo.mp4
        /// </summary>
        [Fact]
        public void Test1()
        {
            FileTypeBox fileTypeBox = new FileTypeBox();
            fileTypeBox.MajorBrand = "mp42";
            fileTypeBox.CompatibleBrands.Add("mp42");
            fileTypeBox.CompatibleBrands.Add("isom");
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x18]);
            fileTypeBox.ToBuffer(ref writer);
            var hex=writer.FlushAndGetArray().ToHexString();
            Assert.Equal("00000018667479706d703432000000006d70343269736f6d".ToUpper(), hex);
        }
    }
}
