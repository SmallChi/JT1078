using JT1078.Flv.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Flv
{
    public class FlvEncoder
    {
        private static readonly FlvHeader VideoFlvHeader = new FlvHeader(true, false);
        public byte[] FlvFirstFrame()
        {
            byte[] buffer = FlvArrayPool.Rent(10240);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv header
                flvMessagePackWriter.WriteArray(VideoFlvHeader.ToArray());
                //flv body
                //flv body PreviousTagSize awalys 0
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
        public byte[] FlvOtherFrame()
        {
            byte[] buffer = FlvArrayPool.Rent(10240);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);

#warning 目前只支持视频
                // NalUnitType == 1

                //flv body
                //flv body PreviousTagSize

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
