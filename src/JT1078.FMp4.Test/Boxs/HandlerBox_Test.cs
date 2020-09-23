using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;
using JT1078.FMp4.Enums;

namespace JT1078.FMp4.Test.Boxs
{
    public class HandlerBox_Test
    {
        /// <summary>
        /// 使用doc/video/demo.mp4
        /// </summary>
        [Fact]
        public void Test1()
        {
            //00 00 00 35--box size
            //68 64 6c 72--box type hdlr
            //00--version
            //00 00 00--flags
            //00 00 00 00--pre_defined
            //73 6f 75 6e--handler_type
            //00 00 00 00--reserved3 - 1
            //00 00 00 00--reserved3 - 2
            //00 00 00 00--reserved3 - 3
            //42 65 6e 74 6f 34 20 53 6f 75 6e 64 20 48 61 6e 64 6c 65 72 00--Name
            HandlerBox handlerBox = new HandlerBox(version:0,flags:0);
            handlerBox.HandlerType =  HandlerType.soun;
            handlerBox.Name = Encoding.UTF8.GetString("42 65 6e 74 6f 34 20 53 6f 75 6e 64 20 48 61 6e 64 6c 65 72 00".ToHexBytes());
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x35]);
            handlerBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000003568646c720000000000000000736f756e00000000000000000000000042656e746f3420536f756e642048616e646c657200".ToUpper(), hex);
        }
    }
}
