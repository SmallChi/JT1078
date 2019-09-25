using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using JT1078.Flv.Extensions;

namespace JT1078.Flv.Test.MessagePack
{

    public class FlvMessagePackWriter_FlvTags_Test
    {
        [Fact]
        public void FlvMessagePackWriter_FlvTags_Test_1()
        {
            Span<byte> buffer = new byte[1024];
            FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
            FlvTags flvTags = new FlvTags
            {
                Type = Enums.TagType.ScriptData,
                DataSize = 156,
                DataTagsData = new Amf3
                {
                    Amf3Metadatas = new List<Flv.Metadata.IAmf3Metadata> {
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
                }
            };
            flvMessagePackWriter.WriteFlvTag(flvTags);
            var hex=flvMessagePackWriter.FlushAndGetArray().ToHexString();
            Assert.Equal("1200009C0000000000000002000A6F6E4D65746144617461080000000700086475726174696F6E00401CE24DD2F1A9FC000866696C6573697A6500413E99AD0000000000096672616D657261746500403D2AAAE297396D000668656967687400408E000000000000000C766964656F636F646563696400401C000000000000000D766964656F646174617261746500000000000000000000057769647468004081000000000000", hex);
        }
    }
}
