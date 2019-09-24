using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using JT1078.Flv.Extensions;

namespace JT1078.Flv.Test
{

    public class FlvMessagePackWriter_Amf3_Test
    {
        [Fact]
        public void FlvMessagePackWriter_Amf3_Test_1() {
            Span<byte> buffer = new byte[1024];
            FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
            flvMessagePackWriter.WriteAmf3(new Flv.Metadata.Amf3 { 
                     Amf3Metadatas=new List<Flv.Metadata.IAmf3Metadata> {
                         new Amf3Metadata_Duration{
                          Value=7.22100
                         },
                         new Amf3Metadata_FileSize{ 
                          Value=2005421.00000
                         }
                     }
            });


            Amf3Metadata_Duration amf3Metadata = new Amf3Metadata_Duration();
            amf3Metadata.Value = 7.22100;
            var hex = amf3Metadata.ToBuffer().ToArray().ToHexString();
            Assert.Equal("00086475726174696F6E00401CE24DD2F1A9FC", buffer.Slice(5, hex.Length/2).ToArray().ToHexString());


            Amf3Metadata_FileSize amf3Metadata_FileSize = new Amf3Metadata_FileSize();
            amf3Metadata_FileSize.Value = 2005421.00000;
            var hex1 = amf3Metadata_FileSize.ToBuffer().ToArray().ToHexString();
            Assert.Equal("000866696C6573697A6500413E99AD00000000", buffer.Slice(hex.Length/2+5, hex1.Length/2).ToArray().ToHexString());

        }
    }
}
