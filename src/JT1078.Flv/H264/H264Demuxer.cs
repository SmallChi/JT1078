using JT1078.Flv.Extensions;
using JT1078.Flv.MessagePack;
using JT1078.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT1078.Flv.H264
{
    public class H264Demuxer
    {
        public const string codecstring = "avc1.";


        /// <summary>
        /// Expunge any "Emulation Prevention" bytes from a "Raw Byte Sequence Payload"
        /// <see cref="https://blog.csdn.net/u011399342/article/details/80472084"/>
        /// 防止竞争插入0x03
        /// </summary>
        /// <param name="srcBuffer"></param>
        /// <returns></returns>
        public byte[] DiscardEmulationPreventionBytes(ReadOnlySpan<byte> srcBuffer)
        {
            int length = srcBuffer.Length;
            List<int> EPBPositions = new List<int>();
            int i = 1;
            // Find all `Emulation Prevention Bytes`
            while (i < length - 2)
            {
                if (srcBuffer[i] == 0 && srcBuffer[i + 1] == 0 && srcBuffer[i + 2] == 0x03)
                {
                    EPBPositions.Add(i + 2);
                    i += 2;
                }
                else
                {
                    i++;
                }
            }
            // If no Emulation Prevention Bytes were found just return the original
            // array
            if (EPBPositions.Count == 0)
            {
                return srcBuffer.ToArray();
            }
            // Create a new array to hold the NAL unit data
            int newLength = length - EPBPositions.Count;
            byte[] newBuffer = new byte[newLength];
            var sourceIndex = 0;
            for (i = 0; i < newLength; sourceIndex++, i++)
            {
                if (sourceIndex == EPBPositions[0])
                {
                    // Skip this byte
                    sourceIndex++;
                    // Remove this position index
                    EPBPositions.RemoveAt(0);
                }
                newBuffer[i] = srcBuffer[sourceIndex];
            }
            return newBuffer;
        }
    }
}
