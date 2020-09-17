using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.MessagePack
{
    public ref struct FMp4MessagePackReader
    {
        public ReadOnlySpan<byte> Reader { get; private set; }
        public ReadOnlySpan<byte> SrcBuffer { get; }
        public int ReaderCount { get; private set; }
        public FMp4MessagePackReader(ReadOnlySpan<byte> srcBuffer)
        {
            SrcBuffer = srcBuffer;
            ReaderCount = 0;
            Reader = srcBuffer;
        }

        public ushort ReadUInt16()
        {
            return BinaryPrimitives.ReadUInt16BigEndian(GetReadOnlySpan(2));
        }

        /// <summary>
        /// 
        /// <see cref="https://github.com/sannies/JT1078.FMp4/blob/master/isoparser/src/main/java/org/JT1078.FMp4/tools/IsoTypeReader.java"/>
        /// </summary>
        /// <param name="language"></param>
        public string  ReadIso639()
        {
            int bits = ReadUInt16();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                int c = (bits >> (2 - i) * 5) & 0x1f;
                sb.Append((char)(c + 0x60));
            }
            return sb.ToString();
        }

        private ReadOnlySpan<byte> GetReadOnlySpan(int count)
        {
            ReaderCount += count;
            return Reader.Slice(ReaderCount - count);
        }
    }
}
