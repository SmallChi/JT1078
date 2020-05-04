using JT1078.Hls.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Hls.Descriptors
{
    /// <summary>
    /// 2.6.18 ISO 639 language descripto 
    /// </summary>
    public class ISO_639_Language_Descriptor : DescriptorBase
    {
        public override byte Tag { get; set; } = 0x0A;
        public override byte Length { get; set; }

        public List<ISO_639_Language_Info> ISO_639_Language_Infos { get; set; }

        public override void ToBuffer(ref TSMessagePackWriter writer)
        {
            writer.WriteByte(Tag);
            if (ISO_639_Language_Infos != null)
            {
                writer.Skip(1, out int DescriptorLengthPosition);
                foreach(var item in ISO_639_Language_Infos)
                {
                    writer.WriteUInt3(item.ISO_639_Language_Code>> 8);
                    writer.WriteByte(item.Audio_Type);
                }
                writer.WriteByteReturn((byte)(writer.GetCurrentPosition()- DescriptorLengthPosition-1), DescriptorLengthPosition);
            }
            else
            {
                writer.WriteByte(0);
            }
        }

        public class ISO_639_Language_Info
        {
            /// <summary>
            /// 24bit
            /// </summary>
            public uint ISO_639_Language_Code { get; set; }
            /// <summary>
            /// 8bit
            /// </summary>
            public byte Audio_Type { get; set; }
        }
    }
}
