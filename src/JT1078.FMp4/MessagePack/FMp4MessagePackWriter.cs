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

        private int TrunDataOffsetPosition;

        private int MfraSizePosition;
        private int MoofOffsetPosition;

        public FMp4MessagePackWriter(Span<byte> buffer)
        {
            this.writer = new FMp4BufferWriter(buffer);
            TrunDataOffsetPosition = 0;
            MfraSizePosition = 0;
            MoofOffsetPosition = 0;
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
        public void WriteInt16(short value)
        {
            BinaryPrimitives.WriteInt16BigEndian(writer.Free, value);
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

        public void WriteUInt32Little(uint value)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(writer.Free, value);
            writer.Advance(4);
        }

        public void WriteUInt24(uint value)
        {
            var span = writer.Free;
            span[0] = (byte)(value >> 16);
            span[1] = (byte)(value >> 8);
            span[2] = (byte)value;
            writer.Advance(3);
        }

        public void WriteASCII(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            data.CopyTo(writer.Free);
            writer.Advance(data.Length);
        }

        public void WriteUTF8(string value)
        {
            var data = Encoding.UTF8.GetBytes(value);
            data.CopyTo(writer.Free);
            writer.Advance(data.Length);
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
        public void WriteUInt16Return(ushort value, int position)
        {
            BinaryPrimitives.WriteUInt16BigEndian(writer.Written.Slice(position, 2), value);
        }
        public void WriteInt32Return(int value, int position)
        {
            BinaryPrimitives.WriteInt32BigEndian(writer.Written.Slice(position, 4), value);
        }
        public void WriteUInt32Return(uint value, int position)
        {
            BinaryPrimitives.WriteUInt32BigEndian(writer.Written.Slice(position, 4), value);
        }
        public void WriteUInt64Return(ulong value, int position)
        {
            BinaryPrimitives.WriteUInt64BigEndian(writer.Written.Slice(position, 8), value);
        }
        public void WriteByteReturn(byte value, int position)
        {
            writer.Written[position] = value;
        }

        public int GetCurrentPosition()
        {
            return writer.WrittenCount;
        }

        public void CreateTrunOffsetPosition()
        {
            TrunDataOffsetPosition = writer.WrittenCount;
        }

        public int GetTrunOffsetPosition()
        {
            return TrunDataOffsetPosition;
        }

        public void ClearTrunOffsetPosition()
        {
            TrunDataOffsetPosition = 0;
        }

        public void CreateMfraSizePosition()
        {
            MfraSizePosition = writer.WrittenCount;
        }

        public int GetMfraSizePosition()
        {
            return MfraSizePosition;
        }

        public void ClearMfraSizePosition()
        {
            MfraSizePosition = 0;
        }

        public void CreateMoofOffsetPosition()
        {
            MoofOffsetPosition = writer.WrittenCount;
        }

        public int GetMoofOffsetPosition()
        {
            return MoofOffsetPosition;
        }

        public void ClearMoofOffsetPosition()
        {
            MoofOffsetPosition = 0;
        }


        /// <summary>
        /// ref
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
