using JT1078.Protocol.Enums;
using JT1078.Protocol.H264;
using JT1078.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JT1078.Protocol;
using JT1078.Hls.MessagePack;

[assembly: InternalsVisibleTo("JT1078.Hls.Test")]

namespace JT1078.Hls
{
    public class TSEncoder
    {
        readonly H264Decoder h264Decoder = new H264Decoder();

        public  byte[] Create(JT1078Package package, int minBufferSize = 65535)
        {
            byte[] buffer = TSArrayPool.Rent(minBufferSize);
            try
            {
                TS_Package tS_Package = new TS_Package();
                tS_Package.Header = new TS_Header();
                tS_Package.Header.PID = (ushort)package.GetKey().GetHashCode();
                tS_Package.Header.ContinuityCounter = (byte)package.SN;
                tS_Package.Header.PayloadUnitStartIndicator = 1;
                tS_Package.Header.Adaptation = new TS_AdaptationInfo();
                tS_Package.Payload = new PES_Package();

                TSMessagePackWriter messagePackReader = new TSMessagePackWriter(buffer);
                var nalus = h264Decoder.ParseNALU(package);
                if (nalus != null && nalus.Count > 0)
                {
                    var sei = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == 6);
                    var sps = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == 7);
                    var pps = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == 8);
                    nalus.Remove(sps);
                    nalus.Remove(pps);
                    nalus.Remove(sei);

                    foreach (var naln in nalus)
                    {
                        
                    }
                }


                return messagePackReader.FlushAndGetArray();
            }
            finally
            {
                TSArrayPool.Return(buffer);
            }
        }
    }
}
