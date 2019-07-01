using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


namespace JT1078.Protocol.MessagePack
{
    ref struct JT1078MessagePackReader
    {
        public ReadOnlySpan<byte> Reader { get; private set; }
        public ReadOnlySpan<byte> SrcBuffer { get; }
        public int ReaderCount { get; private set; }
        public JT1078MessagePackReader(ReadOnlySpan<byte> srcBuffer)
        {
            SrcBuffer = srcBuffer;
            ReaderCount = 0;
            Reader = srcBuffer;
        }
        public ushort ReadUInt16()
        {
            var readOnlySpan = GetReadOnlySpan(2);
            return BinaryPrimitives.ReadUInt16BigEndian(readOnlySpan.Slice(0, 2));
        }
        public uint ReadUInt32()
        {
            var readOnlySpan = GetReadOnlySpan(4);
            return BinaryPrimitives.ReadUInt32BigEndian(readOnlySpan.Slice(0, 4));
        }
        public int ReadInt32()
        {
            var readOnlySpan = GetReadOnlySpan(4);
            return BinaryPrimitives.ReadInt32BigEndian(readOnlySpan.Slice(0, 4));
        }
        public ulong ReadUInt64()
        {
            var readOnlySpan = GetReadOnlySpan(8);
            return BinaryPrimitives.ReadUInt64BigEndian(readOnlySpan.Slice(0, 8));
        }
        public byte ReadByte()
        {
            var readOnlySpan = GetReadOnlySpan(1);
            return readOnlySpan[0];
        }
        /// <summary>
        /// 数字编码 大端模式、高位在前
        /// </summary>
        /// <param name="len"></param>
        public string ReadBigNumber(int len)
        {
            ulong result = 0;
            var readOnlySpan = GetReadOnlySpan(len);
            for (int i = 0; i < len; i++)
            {
                ulong currentData = (ulong)readOnlySpan[i] << (8 * (len - i - 1));
                result += currentData;
            }
            return result.ToString();
        }
        public ReadOnlySpan<byte> ReadArray(int start,int end)
        {
            return Reader.Slice(start,end);
        }
        public string ReadBCD(int len)
        {
            int count = len / 2;
            var readOnlySpan = GetReadOnlySpan(count);
            StringBuilder bcdSb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                bcdSb.Append(readOnlySpan[i].ToString("X2"));
            }
            return bcdSb.ToString();
        }
        public ReadOnlySpan<byte> ReadRemainArray()
        {
            return Reader.Slice(ReaderCount);
        }
        private ReadOnlySpan<byte> GetReadOnlySpan(int count)
        {
            ReaderCount += count;
            return Reader.Slice(ReaderCount - count);
        }
    }
}
