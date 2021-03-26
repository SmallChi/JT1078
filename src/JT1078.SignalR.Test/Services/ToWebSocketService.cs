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

        public Queue<byte[]> q = new Queue<byte[]>();

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
            var ftyp = fMp4Encoder.EncoderFtypBox();
            q.Enqueue(ftyp);
            var package1 = packages[0];
            var nalus1 = h264Decoder.ParseNALU(package1);
            var moov = fMp4Encoder.EncoderMoovBox(nalus1, package1.Bodies.Length);
            q.Enqueue(moov);
            var flag = package1.Label3.DataType == Protocol.Enums.JT1078DataType.视频I帧 ? 1u : 0u;
            var moofBuffer = fMp4Encoder.EncoderMoofBox(nalus1, package1.Bodies.Length, package1.Timestamp, flag);
            q.Enqueue(moofBuffer);
            foreach (var package in packages)
            {
                var otherNalus = h264Decoder.ParseNALU(package);
                var otherMdatBuffer = fMp4Encoder.EncoderMdatBox(otherNalus, package.Bodies.Length);
                q.Enqueue(otherMdatBuffer);
            }
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            a();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (wsSession.GetCount() > 0)
                    {
                        if (q.Count > 0)
                        {
                            await _hubContext.Clients.All.SendAsync("video", q.Dequeue(), stoppingToken);
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
