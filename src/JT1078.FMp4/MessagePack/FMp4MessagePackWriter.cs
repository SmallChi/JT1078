using JT1078.FMp4.Buffers;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.MessagePack
{
    public ref struct FMp4MessagePackWriter
    {
        private FMp4BufferWriter writer;
        public FMp4MessagePackWriter(Span<byte> buffer)
        {
            this.writer = new FMp4BufferWriter(buffer);
        }
        public byte[] FlushAndGetArray()
        {
            return writer.Written.ToArray();
        }
        public void WriteByte(byte value)
        {
            var span = writer.Free;
            span[0] = value;
            writer.Advance(1);
        }
        public void WriteUInt16(ushort value)
        {
            BinaryPrimitives.WriteUInt16BigEndian(writer.Free, value);
            writer.Advance(2);
        }
        public void WriteInt32(int value)
        {
            BinaryPrimitives.WriteInt32BigEndian(writer.Free, value);
            writer.Advance(4);
        }
        public void WriteUInt64(ulong value)
        {
            BinaryPrimitives.WriteUInt64BigEndian(writer.Free, value);
            writer.Advance(8);
        }
        public void WriteUInt32(uint value)
        {
            BinaryPrimitives.WriteUInt32BigEndian(writer.Free, value);
            writer.Advance(4);
        }
        public void WriteArray(ReadOnlySpan<byte> src)
        {
            src.CopyTo(writer.Free);
            writer.Advance(src.Length);
        }
        public void Skip(int count, out int position)
        {
            position = writer.WrittenCount;
            byte[] tmp = new byte[count];
            tmp.CopyTo(writer.Free);
            writer.Advance(count);
        }
        public int GetCurrentPosition()
        {
            return writer.WrittenCount;
        }

        /// <summary>
        /// ref
        /// <see cref="https://github.com/sannies/JT1078.FMp4/blob/master/isoparser/src/main/java/org/JT1078.FMp4/tools/IsoTypeWriter.java"/>
        /// </summary>
        /// <param name="language"></param>
        public void  WriteIso639(string language)
        {
            byte[] bytes= Encoding.UTF8.GetBytes(language);
            int bits = 0;
            for (int i = 0; i < 3; i++)
            {
                bits += (bytes[i] - 0x60) << (2 - i) * 5;
            }
            WriteUInt16((ushort)bits);
        }
    }
}
