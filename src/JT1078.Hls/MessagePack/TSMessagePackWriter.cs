using System;
using System.Collections.Generic;
using System.Linq;
using System.Buffers.Binary;

namespace JT1078.Hls.MessagePack
{
    public ref partial struct TSMessagePackWriter
    {
        private TSBufferWriter writer;
        public TSMessagePackWriter(Span<byte> buffer)
        {
            this.writer = new TSBufferWriter(buffer);
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
        public void WriteUInt5(ulong value)
        {
            writer.Free[0] = (byte)(value >> 32);
            writer.Free[1] = (byte)(value >> 24);
            writer.Free[2] = (byte)(value >> 16);
            writer.Free[3] = (byte)(value >> 8);
            writer.Free[4] = (byte)(value);
            writer.Advance(5);
        }
        public void WriteUInt6(ulong value)
        {
            writer.Free[0] = (byte)(value >> 40);
            writer.Free[1] = (byte)(value >> 32);
            writer.Free[2] = (byte)(value >> 24);
            writer.Free[3] = (byte)(value >> 16);
            writer.Free[4] = (byte)(value >> 8);
            writer.Free[5] = (byte)(value);
            writer.Advance(6);
        }
        public void WriteInt5(long value)
        {
            writer.Free[0] = (byte)(value >> 32);
            writer.Free[1] = (byte)(value >> 24);
            writer.Free[2] = (byte)(value >> 16);
            writer.Free[3] = (byte)(value >> 8);
            writer.Free[4] = (byte)(value);
            writer.Advance(5);
        }
        public void WriteInt6(long value)
        {
            writer.Free[0] = (byte)(value >> 40);
            writer.Free[1] = (byte)(value >> 32);
            writer.Free[2] = (byte)(value >> 24);
            writer.Free[3] = (byte)(value >> 16);
            writer.Free[4] = (byte)(value >> 8);
            writer.Free[5] = (byte)(value);
            writer.Advance(6);
        }
        public void WritePCR(long value)
        {
            writer.Free[0] = (byte)(value >> 25);
            writer.Free[1] = (byte)((value >> 17) & 0xff);
            writer.Free[2] = (byte)((value >> 9) & 0xff);
            writer.Free[3] = (byte)((value >> 1) & 0xff);
            writer.Free[4] = (byte)(((value & 0x1) << 7) | 0x7e);
            writer.Free[5] = 0x00;
            writer.Advance(6);
        }
        public void WriteUInt3(uint value)
        {
            writer.Free[0] = (byte)(value >> 16);
            writer.Free[1] = (byte)(value >> 8);
            writer.Free[2] = (byte)(value);
            writer.Advance(3);
        }
        public void WriteInt3(int value)
        {
            writer.Free[0] = (byte)(value >> 16);
            writer.Free[1] = (byte)(value >> 8);
            writer.Free[2] = (byte)(value);
            writer.Advance(3);
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
        public void WriteCRC32()
        {
            if (writer.WrittenCount < 1)
            {
                throw new ArgumentOutOfRangeException($"Written<start:{writer.WrittenCount}>{1}");
            }
            //从第1位开始
            var crcSpan = writer.Written.Slice(1);
            uint crc = 0xFFFFFFFF;
            for (int i = 0; i < crcSpan.Length; i++)
            {
                crc = (crc << 8) ^ Util.crcTable[(crc >> 24) ^ crcSpan[i]];
            }
            WriteUInt32(crc);
        }
        public void WriteCRC32(int start)
        {
            if (writer.WrittenCount < start)
            {
                throw new ArgumentOutOfRangeException($"Written<start:{writer.WrittenCount}>{1}");
            }
            var crcSpan = writer.Written.Slice(start);
            uint crc = 0xFFFFFFFF;
            byte j = 0;
            for (int i = 0; i < crcSpan.Length; i++)
            {
                j = (byte)(((crc >> 24) ^ crcSpan[i]) & 0xff);
                crc = (crc << 8) ^ Util.crcTable[j];
            }
            WriteUInt32(crc);
        }
        public void WriteCRC32(int start, int end)
        {
            if (start > end)
            {
                throw new ArgumentOutOfRangeException($"start>end:{start}>{end}");
            }
            var crcSpan = writer.Written.Slice(start, end);
            uint crc = 0xFFFFFFFF;
            for (int i = 0; i < crcSpan.Length; i++)
            {
                crc = ((crc << 8) ^ Util.crcTable[(crc >> 8) ^ crcSpan[i]]);
            }
            WriteUInt32(crc);
        }
        public void WriteUInt16Return(ushort value, int position)
        {
            BinaryPrimitives.WriteUInt16BigEndian(writer.Written.Slice(position, 2), value);
        }        
        public void WriteByteReturn(byte value, int position)
        {
            writer.Written[position] = value;
        }
        public int GetCurrentPosition()
        {
            return writer.WrittenCount;
        }
        public void WriteString(string value)
        {
            byte[] codeBytes = TSConstants.Encoding.GetBytes(value);
            codeBytes.CopyTo(writer.Free);
            writer.Advance(codeBytes.Length);
        }
    }
}
