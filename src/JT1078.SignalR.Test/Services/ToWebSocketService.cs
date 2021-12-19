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
            List<byte[]> first = new List<byte[]>();
            var ftyp = fMp4Encoder.FtypBox();
            first.Add(ftyp);
            var package1 = packages[0];
            var nalus1 = h264Decoder.ParseNALU(package1);
            var moov = fMp4Encoder.MoovBox(
              nalus1.FirstOrDefault(f => f.NALUHeader.NalUnitType == NalUnitType.SPS),
              nalus1.FirstOrDefault(f => f.NALUHeader.NalUnitType == NalUnitType.PPS));
            first.Add(moov);
            q.Add(first.SelectMany(s=>s).ToArray());
            List<JT1078Package> tmp = new List<JT1078Package>();
            //缓存组包到下一个I帧
            foreach (var package in packages)
            {
                if (package.Label3.DataType == Protocol.Enums.JT1078DataType.视频I帧)
                {
                    if (tmp.Count>0)
                    {
                        List<byte[]> buffer = new List<byte[]>();
                        buffer.Add(fMp4Encoder.StypBox());
                        buffer.Add(fMp4Encoder.OtherVideoBox(tmp));
                        q.Add(buffer.SelectMany(s => s).ToArray());
                        tmp.Clear();
                    }
                }
                tmp.Add(package);
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
                await Task.Delay(1000);
            }
        }
    }
}
