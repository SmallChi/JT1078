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
using JT1078.Protocol.Enums;

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

        void Init()
        {
            List<JT1078Package> packages = new List<JT1078Package>();
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_6.txt"));
            int mergeBodyLength = 0;
            foreach (var line in lines)
            {
                var bytes = line.ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                var packageMerge = JT1078Serializer.Merge(package);
                if (packageMerge != null)
                {
                    packages.Add(packageMerge);
                }
            }

            var avframe = h264Decoder.ParseAVFrame(packages[0]);
            q.Add(fMp4Encoder.FirstVideoBox(avframe));

            Queue<Mp4Frame> mp4Frames = new Queue<Mp4Frame>();
            List<NalUnitType> filter = new List<NalUnitType>();
            filter.Add(NalUnitType.SEI);
            filter.Add(NalUnitType.PPS);
            filter.Add(NalUnitType.SPS);
            filter.Add(NalUnitType.AUD);
            foreach (var package in packages)
            {
                List<H264NALU> h264NALUs = h264Decoder.ParseNALU(package);
                if (h264NALUs != null && h264NALUs.Count > 0)
                {
                    Mp4Frame mp4Frame = new Mp4Frame
                    {
                        Key = package.GetKey(),
                        KeyFrame = package.Label3.DataType == JT1078DataType.视频I帧
                    };
                    mp4Frame.NALUs = h264NALUs;
                    mp4Frames.Enqueue(mp4Frame);
                }
            }
            while (mp4Frames.TryDequeue(out Mp4Frame frame))
            {
                q.Add(fMp4Encoder.OtherVideoBox(frame.NALUs, frame.Key, frame.KeyFrame));
            }
        }

        class Mp4Frame
        {
            public string Key { get; set; }
            public bool KeyFrame { get; set; }
            public List<H264NALU> NALUs { get; set; }
        }

        public Dictionary<string,int> flag = new Dictionary<string, int>();

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Init();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    foreach(var session in wsSession.GetAll())
                    {
                        if (flag.ContainsKey(session))
                        {
                            var len = flag[session];
                            if (q.Count <= len)
                            {
                                break;
                            }
                            await _hubContext.Clients.Client(session).SendAsync("video", q[len], stoppingToken);
                            len++;
                            flag[session] = len;
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
                await Task.Delay(60);
            }
        }
    }
}
