using JT1078.Hls.Descriptors;
using JT1078.Hls.Enums;
using JT1078.Hls.MessagePack;
using JT1078.Protocol.Extensions;
using System;
using System.Collections.Generic;
using Xunit;
using System.Buffers.Binary;

namespace JT1078.Hls.Test
{
    /// <summary>
    /// 使用doc/video/demo0.ts
    /// </summary>
    public class TS_PMT_Package_Test
    {
        [Fact]
        public void ToBufferTest()
        {
            //47 50 00 10 00 02 B0 1D 00 01 C1 00 00 E1 00 F0 00 1B E1 00 F0 00 0F E1 01 F0 06 0A 04 75 6E 64 00 08 7D E8 77
            //47                    
            //50 00
            //10 
            //00 
            //02
            //B0 1D 
            //00 01 
            //C1 
            //00 
            //00 
            //E1 00 
            //F0 00 
            //1B
            //E1 00 
            //  F0 00 
            //0F 
            //E1 01 
            //  F0 06 
            //      0A 
            //      04 
            //      75 6E 64 00 
            //08 7D E8 77 
            TS_PMT_Package package = new TS_PMT_Package();
            package.Header = new TS_Header();
            package.Header.PID = 4096;
            package.Header.AdaptationFieldControl = AdaptationFieldControl.无自适应域_仅含有效负载;
            package.Header.PayloadUnitStartIndicator = 1;
            package.Header.ContinuityCounter = 0;
            package.TableId = 0x02;
            package.ProgramNumber = 0x0001;
            package.PCR_PID = 256;
            package.Components = new List<TS_PMT_Component>();
            package.Components.Add(new TS_PMT_Component()
            {
                 StreamType= StreamType.h264,
                 ElementaryPID= 256,
            });
            package.Components.Add(new TS_PMT_Component()
            {
                StreamType = StreamType.aac,
                ElementaryPID = 257,
                Descriptor=new ISO_639_Language_Descriptor
                { 
                     ISO_639_Language_Infos=new List<ISO_639_Language_Descriptor.ISO_639_Language_Info>()
                     {
                         new ISO_639_Language_Descriptor.ISO_639_Language_Info
                         { 
                             Audio_Type=0,
                             ISO_639_Language_Code=BinaryPrimitives.ReadUInt32BigEndian(new byte[]{ 0x75, 0x6E, 0x64, 0x00})
                         }
                     }
                }
            });
            TSMessagePackWriter writer = new TSMessagePackWriter(new byte[188]);
            package.ToBuffer(ref writer);
            var pmtData = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("47 50 00 10 00 02 B0 1D 00 01 C1 00 00 E1 00 F0 00 1B E1 00 F0 00 0F E1 01 F0 06 0A 04 75 6E 64 00 08 7D E8 77 FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF ".Replace(" ",""), pmtData);
        }
    }
}
