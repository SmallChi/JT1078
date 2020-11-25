using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using JT1078.Flv.MessagePack;
using JT1078.Protocol;
using JT1078.Protocol.H264;
using JT1078.Protocol.MessagePack;
using JT1078.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JT1078.Flv.Benchmark
{
    [Config(typeof(JT1078FlvEncoderConfig))]
    [MarkdownExporterAttribute.GitHub]
    [MemoryDiagnoser]
    public class JT1078FlvEncoderContext
    {
        JT1078Package Package;
        List<H264NALU> H264NALUs;
        H264NALU SPSNALu;
        H264Decoder h264Decoder = new H264Decoder();
        FlvEncoder flvEncoder = new FlvEncoder();

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
            SPSNALu = H264NALUs.FirstOrDefault(f => f.NALUHeader.NalUnitType == 7);
            SPSNALu.RawData = h264Decoder.DiscardEmulationPreventionBytes(SPSNALu.RawData);
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

        //[Benchmark(Description = "FlvEncoder")]
        //public void FlvEncoder()
        //{
        //    for(var i=0;i< N;i++)
        //    {
        //        var contents = flvEncoder.CreateFlvFrame(H264NALUs);
        //    }
        //}
    }

    public class JT1078FlvEncoderConfig : ManualConfig
    {
        public JT1078FlvEncoderConfig()
        {
            AddJob(Job.Default.WithGcServer(false).WithToolchain(CsProjCoreToolchain.NetCoreApp50).WithPlatform(Platform.AnyCpu));
        }
    }
}
