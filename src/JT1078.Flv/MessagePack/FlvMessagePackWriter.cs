using System;
using System.Buffers.Binary;

namespace JT1078.Flv.MessagePack
{
    ref partial struct FlvMessagePackWriter
    {
        private FlvBufferWriter writer;
        public FlvMessagePackWriter(Span<byte> buffer)
        {
            this.writer = new FlvBufferWriter(buffer);
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
    }
}
