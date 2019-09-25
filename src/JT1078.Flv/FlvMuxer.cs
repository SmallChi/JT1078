using JT1078.Flv.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv
{
    public class FlvMuxer
    {
        private readonly FlvHeader VideoFlvHeader = new FlvHeader(true, false);
        public byte[] FlvFirstFrame()
        {
            byte[] buffer = FlvArrayPool.Rent(10240);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv header
                flvMessagePackWriter.WriteArray(VideoFlvHeader.ToArray());
                //flv body
                //flv body PreviousTagSize
                flvMessagePackWriter.WriteUInt32(0);
                //flv body tag

                //flv body tag header

                //flv body tag body

                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }
    }
}
