using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using JT1078.SignalR.Test.Hubs;
using JT1078.FMp4;
using JT1078.Protocol;
using System.IO;
using JT1078.Protocol.Extensions;
using JT1078.Protocol.H264;
using System.Net.WebSockets;

namespace JT1078.SignalR.Test.Services
{
    public  class ToWebSocketService: BackgroundService
    {
        private readonly ILogger<ToWebSocketService> logger;

        private readonly IHubContext<FMp4Hub> _hubContext;

        private readonly FMp4Encoder fMp4Encoder;

        private readonly WsSession wsSession;

        private readonly H264Decoder h264Decoder;

        public ToWebSocketService(
            H264Decoder h264Decoder,
            WsSession wsSession,
            FMp4Encoder fMp4Encoder,
            ILoggerFactory loggerFactory,
            IHubContext<FMp4Hub> hubContext)
        {
            this.h264Decoder = h264Decoder;
            logger = loggerFactory.CreateLogger<ToWebSocketService>();
            this.fMp4Encoder = fMp4Encoder;
            _hubContext = hubContext;
            this.wsSession = wsSession;
        }

        public List<byte[]> q = new List<byte[]>();

        public void a()
        {
            List<JT1078Package> packages = new List<JT1078Package>();
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_3.txt"));
            int mergeBodyLength = 0;
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[6].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                var packageMerge = JT1078Serializer.Merge(package);
                if (packageMerge != null)
                {
                    packages.Add(packageMerge);
                }
            }
            List<byte[]> first = new List<byte[]>();
            //var styp = fMp4Encoder.EncoderStypBox();
            //first.Add(styp);
            //q.Enqueue(styp);
            var ftyp = fMp4Encoder.EncoderFtypBox();
            //q.Enqueue(ftyp);
            first.Add(ftyp);
            var package1 = packages[0];
            var nalus1 = h264Decoder.ParseNALU(package1);
            var moov = fMp4Encoder.EncoderMoovBox(
              nalus1.FirstOrDefault(f => f.NALUHeader.NalUnitType == NalUnitType.SPS),
              nalus1.FirstOrDefault(f => f.NALUHeader.NalUnitType == NalUnitType.PPS));
            //q.Enqueue(moov);
            first.Add(moov);
            q.Add(first.SelectMany(s=>s).ToArray());
            List<NalUnitType> filter = new List<NalUnitType>() { NalUnitType.SEI,NalUnitType.SPS,NalUnitType.PPS,NalUnitType.AUD};
            List<H264NALU> nalus = new List<H264NALU>();
            foreach (var package in packages)
            {
                List<H264NALU> h264NALUs = h264Decoder.ParseNALU(package);
                foreach (var nalu in h264NALUs)
                {
                    if (nalu.Slice)
                    {
                        //H264 NALU slice first_mb_in_slice
                        nalus.Add(nalu);
                    }
                    else
                    {
                        if (nalus.Count > 0)
                        {
                            var otherBuffer = fMp4Encoder.EncoderOtherVideoBox(nalus);
                            q.Add(otherBuffer);
                            nalus.Clear();
                        }
                        nalus.Add(nalu);
                    }
                }
            }
        }


        public Dictionary<string,int> flag = new Dictionary<string, int>();

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            a();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    foreach(var session in wsSession.GetAll())
                    {
                        if (flag.ContainsKey(session))
                        {
                            var len = flag[session];
                            if (q.Count < len)
                            {
                                break;
                            }
                            await _hubContext.Clients.Client(session).SendAsync("video", q[len], stoppingToken);
                            flag[session] = ++len;
                        }
                        else
                        {
                            await _hubContext.Clients.Client(session).SendAsync("video", q[0], stoppingToken);
                            flag.Add(session, 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,"");
                }
                await Task.Delay(1000);
            }
        }
    }
}
