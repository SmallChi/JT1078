using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using JT1078.Flv.Extensions;

namespace JT1078.Flv.Test.MessagePack
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
                         },
                         new Amf3Metadata_FrameRate{ 
                         Value=29.16667
                         },
                         new Amf3Metadata_Height{ 
                         Value=960.00000
                         },
                         new Amf3Metadata_VideoCodecId{ 
                         Value=7.00000
                         },
                         new Amf3Metadata_VideoDataRate{ 
                         Value=0.00000
                         },
                         new Amf3Metadata_Width{ 
                         Value=544.00000
                         }
                     }
            });

            buffer = buffer.Slice(5);//amf3 5个字节
            Amf3Metadata_Duration amf3Metadata = new Amf3Metadata_Duration();
            amf3Metadata.Value = 7.22100;
            var hex = amf3Metadata.ToBuffer().ToArray().ToHexString();
            Assert.Equal("00086475726174696F6E00401CE24DD2F1A9FC", buffer.Slice(0,hex.Length/2).ToArray().ToHexString());

            buffer = buffer.Slice(hex.Length / 2);
            Amf3Metadata_FileSize amf3Metadata_FileSize = new Amf3Metadata_FileSize();
            amf3Metadata_FileSize.Value = 2005421.00000;
            var hex1 = amf3Metadata_FileSize.ToBuffer().ToArray().ToHexString();
            Assert.Equal("000866696C6573697A6500413E99AD00000000", buffer.Slice(0, hex1.Length/2).ToArray().ToHexString());

            buffer = buffer.Slice(hex1.Length / 2);
            Amf3Metadata_FrameRate amf3Metadata_FrameRate = new Amf3Metadata_FrameRate();
            amf3Metadata_FrameRate.Value = 29.16667;
            var hex2 = amf3Metadata_FrameRate.ToBuffer().ToArray().ToHexString();
            Assert.Equal("00096672616D657261746500403D2AAAE297396D", buffer.Slice(0, hex2.Length / 2).ToArray().ToHexString());

            buffer = buffer.Slice(hex2.Length / 2);
            Amf3Metadata_Height amf3Metadata_Height = new Amf3Metadata_Height();
            amf3Metadata_Height.Value = 960.00000;
            var hex3 = amf3Metadata_Height.ToBuffer().ToArray().ToHexString();
            Assert.Equal("000668656967687400408E000000000000", buffer.Slice(0, hex3.Length / 2).ToArray().ToHexString());

            buffer = buffer.Slice(hex3.Length / 2);
            Amf3Metadata_VideoCodecId amf3Metadata_VideoCodecId = new Amf3Metadata_VideoCodecId();
            amf3Metadata_VideoCodecId.Value = 7.00000;
            var hex4 = amf3Metadata_VideoCodecId.ToBuffer().ToArray().ToHexString();
            Assert.Equal("000C766964656F636F646563696400401C000000000000", buffer.Slice(0, hex4.Length / 2).ToArray().ToHexString());

            buffer = buffer.Slice(hex4.Length / 2);
            Amf3Metadata_VideoDataRate amf3Metadata_VideoDataRate = new Amf3Metadata_VideoDataRate();
            amf3Metadata_VideoDataRate.Value = 0.00000;
            var hex5 = amf3Metadata_VideoDataRate.ToBuffer().ToArray().ToHexString();
            Assert.Equal("000D766964656F6461746172617465000000000000000000", buffer.Slice(0, hex5.Length / 2).ToArray().ToHexString());

            buffer = buffer.Slice(hex5.Length / 2);
            Amf3Metadata_Width amf3Metadata_Width = new Amf3Metadata_Width();
            amf3Metadata_Width.Value = 544.00000;
            var hex6 = amf3Metadata_Width.ToBuffer().ToArray().ToHexString();
            Assert.Equal("00057769647468004081000000000000", buffer.Slice(0, hex6.Length / 2).ToArray().ToHexString());
        }
    }
}
