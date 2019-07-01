using System;

namespace JT1078.Protocol.Tools.Extensions
{
    public static partial class BinaryExtensions
    {
        public static string ToHexString(this byte[] source)
        {
            return HexUtil.DoHexDump(source, 0, source.Length).ToUpper();
        }
        /// <summary>
        /// 16进制字符串转16进制数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static byte[] ToHexBytes(this string hexString)
        {
            hexString = hexString.Replace(" ", "");
            byte[] buf = new byte[hexString.Length / 2];
            ReadOnlySpan<char> readOnlySpan = hexString.AsSpan();
            for (int i = 0; i < hexString.Length; i++)
            {
                if (i % 2 == 0)
                {
                    buf[i / 2] = Convert.ToByte(readOnlySpan.Slice(i, 2).ToString(), 16);
                }
            }
            return buf;
        }
    }

    public static class HexUtil
    {
        static readonly char[] HexdumpTable = new char[256 * 4];
        static HexUtil()
        {
            char[] digits = "0123456789ABCDEF".ToCharArray();
            for (int i = 0; i < 256; i++)
            {
                HexdumpTable[i << 1] = digits[(int)((uint)i >> 4 & 0x0F)];
                HexdumpTable[(i << 1) + 1] = digits[i & 0x0F];
            }
        }

        public static string DoHexDump(ReadOnlySpan<byte> buffer, int fromIndex, int length)
        {
            if (length == 0)
            {
                return "";
            }
            int endIndex = fromIndex + length;
            var buf = new char[length << 1];
            int srcIdx = fromIndex;
            int dstIdx = 0;
            for (; srcIdx < endIndex; srcIdx++, dstIdx += 2)
            {
                Array.Copy(HexdumpTable, buffer[srcIdx] << 1, buf, dstIdx, 2);
            }
            return new string(buf);
        }

        public static string DoHexDump(byte[] array, int fromIndex, int length)
        {
            if (length == 0)
            {
                return "";
            }
            int endIndex = fromIndex + length;
            var buf = new char[length << 1];
            int srcIdx = fromIndex;
            int dstIdx = 0;
            for (; srcIdx < endIndex; srcIdx++, dstIdx += 2)
            {
                Array.Copy(HexdumpTable, (array[srcIdx] & 0xFF) << 1, buf, dstIdx, 2);
            }
            return new string(buf);
        }
    }
}
