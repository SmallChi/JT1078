using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using JT1078.Flv;
using JT1078.Flv.MessagePack;
using JT1078.FMp4;
using JT1078.Protocol;
using JT1078.Protocol.H264;
using JT1078.Protocol.MessagePack;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JT1078.AV.Benchmark
{
    [Config(typeof(JT1078AVEncoderConfig))]
    [MarkdownExporterAttribute.GitHub]
    [MemoryDiagnoser]
    public class JT1078AVEncoderContext
    {
        JT1078Package Package;
        List<H264NALU> H264NALUs;
        List<H264NALU> FMp4H264NALUs;
        H264NALU SPSNALu;
        H264Decoder h264Decoder = new H264Decoder();
        FlvEncoder flvEncoder = new FlvEncoder();
        FMp4Encoder fmp4Encoder = new FMp4Encoder();

        [Params(100, 10000, 100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JT1078_1.txt"));
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[6].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                Package = JT1078Serializer.Merge(package);
            }
            H264NALUs = h264Decoder.ParseNALU(Package);
            SPSNALu = H264NALUs.FirstOrDefault(f => f.NALUHeader.NalUnitType == NalUnitType.SPS);
            SPSNALu.RawData = h264Decoder.DiscardEmulationPreventionBytes(SPSNALu.RawData);

            List<JT1078Package> packages = new List<JT1078Package>();
            var lines3 = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_3.txt"));
            int mergeBodyLength = 0;
            foreach (var line in lines3)
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
            List<H264NALU> nalus = new List<H264NALU>();
            bool segmentFlag = false;
            foreach (var package in packages)
            {
                if (segmentFlag)
                {
                    break;
                }
                List<H264NALU> h264NALUs = h264Decoder.ParseNALU(package);
            }
        }

        [Benchmark(Description = "EXPGolombReader")]
        public void EXPGolombReaderTest()
        {
            for (var i = 0; i < N; i++)
            {
                ExpGolombReader h264GolombReader = new ExpGolombReader(SPSNALu.RawData);
                h264GolombReader.ReadSPS();
            }
        }

        [Benchmark(Description = "H264Decoder")]
        public void H264Decoder()
        {
            for (var i = 0; i < N; i++)
            {
                var nalus = h264Decoder.ParseNALU(Package);
            }
        }

        [Benchmark(Description = "FMp4Encoder")]
        public void FMp4Encoder()
        {
            for (var i = 0; i < N; i++)
            {
                //todo:OtherVideoBox
                //var buffer = fmp4Encoder.OtherVideoBox(FMp4H264NALUs);
            }
        }
    }

    public class JT1078AVEncoderConfig : ManualConfig
    {
        public JT1078AVEncoderConfig()
        {
            AddJob(Job.Default.WithGcServer(false).WithToolchain(CsProjCoreToolchain.NetCoreApp60).WithPlatform(Platform.AnyCpu));
        }
    }
}
