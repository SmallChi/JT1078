using System;
using System.Collections.Generic;
using System.Text;
using System.Buffers.Binary;

namespace JT1078.Flv
{
    public struct FlvHeader
    {
        public FlvHeader(bool hasVideo,bool hasAudio)
        {
            byte audioFlag = 0x01 << 2;
            byte videoFlag = 0x01;
            Flags = 0x00;
            if (hasAudio) Flags |= audioFlag;
            if (hasVideo) Flags |= videoFlag;
        }

        private byte Flags;

        public ReadOnlySpan<byte> ToArray()
        {
            Span<byte> tmp = new byte[9];
            tmp[0] = 0x46;
            tmp[1] = 0x4c;
            tmp[2] = 0x56;
            tmp[3] = 0x01;
            tmp[4] = Flags;
            BinaryPrimitives.WriteInt32BigEndian(tmp.Slice(5), 9);
            return tmp;
        }
    }
}
