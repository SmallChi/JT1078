using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Flv.Extensions;
using JT1078.Protocol;
using System.IO;
using System.Linq;
using JT1078.Protocol.Enums;
using JT1078.Flv.H264;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using System.Diagnostics;

namespace JT1078.Flv.Test
{
    public class FlvEncoderTest
    {
        [Fact]
        public void FlvEncoder_Test_1()
        {
            JT1078Package Package = null;
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.txt"));
            int mergeBodyLength = 0;
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[6].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                Package = JT1078Serializer.Merge(package);
            }
            Flv.H264.H264Decoder decoder = new Flv.H264.H264Decoder();
            //7 8 6 5 1 1 1 1 1 7 8 6 5 1 1 1 1
            var nalus = decoder.ParseNALU(Package);
            Assert.Equal(4, nalus.Count);

            FlvEncoder encoder = new FlvEncoder();
            var contents = encoder.CreateFlvFrame(nalus);
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.flv");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            File.WriteAllBytes(filepath, contents);
        }

        [Fact]
        public void FlvEncoder_Test_2()
        {
            FileStream fileStream=null;
            try
            {
                JT1078Package Package = null;
                List<H264NALU> h264NALULs = new List<H264NALU>();
                Flv.H264.H264Decoder decoder = new Flv.H264.H264Decoder();
                FlvEncoder encoder = new FlvEncoder();
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_2.txt"));
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    Package = JT1078Serializer.Merge(package);
                    if (Package != null)
                    {
                        var tmp = decoder.ParseNALU(Package);
                        if (tmp != null && tmp.Count > 0)
                        {
                            h264NALULs = h264NALULs.Concat(tmp).ToList();
                        }
                    }
                }

                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_2.flv");
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                var totalPage = (h264NALULs.Count + 10 - 1) / 10;
                for(var i=0;i< totalPage; i++)
                {
                    var flv2 = encoder.CreateFlvFrame(h264NALULs.Skip(i * 10).Take(10).ToList());
                    if (flv2.Length != 0)
                    {
                        fileStream.Write(flv2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally 
            {
                fileStream?.Close();
                fileStream?.Dispose();
            }
        }

        [Fact]
        public void FlvEncoder_Test_3()
        {
            FileStream fileStream = null;
            Flv.H264.H264Decoder decoder = new Flv.H264.H264Decoder();
            List<H264NALU> h264NALULs = new List<H264NALU>();
            FlvEncoder encoder = new FlvEncoder();
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.flv");
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.txt"));
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                JT1078Package Package = null;

                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    Package = JT1078Serializer.Merge(package);
                    if (Package != null)
                    {
                        var tmp = decoder.ParseNALU(Package);
                        if (tmp != null && tmp.Count > 0)
                        {
                            h264NALULs = h264NALULs.Concat(tmp).ToList();
                        }
                    }
                }

                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                var totalPage = (h264NALULs.Count + 10 - 1) / 10;
                for (var i = 0; i < totalPage; i++)
                {
                    var flv2 = encoder.CreateFlvFrame(h264NALULs.Skip(i * 10).Take(10).ToList());
                    if (flv2.Length != 0)
                    {
                        fileStream.Write(flv2);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Throws<Exception>(()=> { });
            }
            finally
            {
                fileStream?.Close();
                fileStream?.Dispose();
            }
        }

        [Fact]
        public void FlvEncoder_Test_4()
        {
            FileStream fileStream = null;
            Flv.H264.H264Decoder decoder = new Flv.H264.H264Decoder();
            List<H264NALU> h264NALULs = new List<H264NALU>();
            FlvEncoder encoder = new FlvEncoder();
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_4.flv");
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_4.txt"));
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                JT1078Package Package = null;

                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    Package = JT1078Serializer.Merge(package);
                    if (Package != null)
                    {
                        var tmp = decoder.ParseNALU(Package);
                        if (tmp != null && tmp.Count > 0)
                        {
                            h264NALULs = h264NALULs.Concat(tmp).ToList();
                        }
                    }
                }

                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);                
                var totalPage = (h264NALULs.Count + 10 - 1) / 10;
                for (var i = 0; i < totalPage; i++)
                {
                    var flv2 = encoder.CreateFlvFrame(h264NALULs.Skip(i * 10).Take(10).ToList());
                    if (flv2.Length != 0)
                    {
                        fileStream.Write(flv2);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Throws<Exception>(() => { });
            }
            finally
            {
                fileStream?.Close();
                fileStream?.Dispose();
            }
        }

        [Fact]
        public void FlvEncoder_Test_5()
        {
            FileStream fileStream = null;
            Flv.H264.H264Decoder decoder = new Flv.H264.H264Decoder();
            List<H264NALU> h264NALULs = new List<H264NALU>();
            FlvEncoder encoder = new FlvEncoder();
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_5.flv");
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_5.txt"));
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                JT1078Package Package = null;

                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    Package = JT1078Serializer.Merge(package);
                    if (Package != null)
                    {
                        var tmp = decoder.ParseNALU(Package);
                        if (tmp != null && tmp.Count > 0)
                        {
                            h264NALULs = h264NALULs.Concat(tmp).ToList();
                        }
                    }
                }
                var tmp1 = h264NALULs.Where(w => w.NALUHeader.NalUnitType == 7).ToList();
                List<SPSInfo> tmpSpss = new List<SPSInfo>();
                List<ulong> times = new List<ulong>();
                List<ushort> lastIFrameIntervals = new List<ushort>();
                List<ushort> lastFrameIntervals = new List<ushort>();
                List<int> type = new List<int>();
                foreach (var item in h264NALULs)
                {    
                    //type.Add(item.NALUHeader.NalUnitType);
                    times.Add(item.Timestamp);
                    lastFrameIntervals.Add(item.LastFrameInterval);
                    lastIFrameIntervals.Add(item.LastIFrameInterval);
                    if(item.NALUHeader.NalUnitType == 7)
                    {
                        ExpGolombReader expGolombReader = new ExpGolombReader(item.RawData);
                        tmpSpss.Add(expGolombReader.ReadSPS());
                    }
                }
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                var totalPage = (h264NALULs.Count + 10 - 1) / 10;
                for (var i = 0; i < totalPage; i++)
                {
                    var flv2 = encoder.CreateFlvFrame(h264NALULs.Skip(i * 10).Take(10).ToList());
                    if (flv2.Length != 0)
                    {
                        //fileStream.Write(flv2);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Throws<Exception>(() => { });
            }
            finally
            {
                fileStream?.Close();
                fileStream?.Dispose();
            }
        }

        [Fact]
        public void CreateScriptTagFrameTest()
        {
            FlvEncoder flvEncoder = new FlvEncoder();
            var hexData = flvEncoder.CreateScriptTagFrame(288, 352);
            Assert.Equal(151, hexData.Length);
        }

        [Fact]
        public void CreateVideoTag0FrameTest()
        {
            FlvEncoder flvEncoder = new FlvEncoder();
            var hexData = flvEncoder.CreateVideoTag0Frame(
                new byte[] { 0x67, 0x4D, 0, 0x14, 0x95, 0xA8, 0x58, 0x25, 0x90 },
                new byte[] { 0x68, 0xEE, 0x3C, 0x80 },
                new SPSInfo { levelIdc = 0x14, profileIdc= 0x4d, profileCompat=0 });
            Assert.Equal(40, hexData.Length);
        }

        [Fact]
        public void GetFirstFlvFrameTest()
        {
            FlvEncoder flvEncoder = new FlvEncoder();
            string key = "test";
            var bufferFlvFrame = new byte[] { 0xA, 0xB, 0xC, 0xD, 0xE, 0xF };
            FlvEncoder.FirstFlvFrameCache.TryAdd(key, (2, new byte[] { 1, 2, 3, 4, 5, 6 },true));
            var buffer=flvEncoder.GetFirstFlvFrame(key, bufferFlvFrame);
            //替换PreviousTagSize 4位的长度为首帧的 PreviousTagSize
            Assert.Equal(new byte[] { 1, 2, 3, 4, 5, 6, 0, 0, 0, 2, 0xE, 0xF }, buffer);
        }
    }
}
