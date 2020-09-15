using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Flv.Extensions;
using JT1078.Protocol;
using System.IO;
using System.Linq;
using JT1078.Protocol.Enums;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using System.Diagnostics;
using JT1078.Protocol.H264;
using JT1078.Protocol.MessagePack;

namespace JT1078.Flv.Test
{
    public class FlvEncoderTest
    {
        [Fact]
        public void 测试第一帧的数据()
        {
            FileStream fileStream = null;
            try
            {
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.txt"));
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.flv");
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);

                bool isNeedFirstHeadler = true;
                FlvEncoder encoder = new FlvEncoder();
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        var videoTag = encoder.EncoderVideoTag(fullpackage, isNeedFirstHeadler);
                        fileStream.Write(videoTag);
                        isNeedFirstHeadler = false;
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
        public void 测试前几帧的数据()
        {
            FileStream fileStream = null;
            try
            {
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_2.txt"));
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_2.flv");
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);

                bool isNeedFirstHeadler = true;
                FlvEncoder encoder = new FlvEncoder();
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        var videoTag = encoder.EncoderVideoTag(fullpackage, isNeedFirstHeadler);
                        fileStream.Write(videoTag);
                        isNeedFirstHeadler = false;
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
        public void 测试可以播放的Flv3()
        {
            FileStream fileStream = null;
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.flv");
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.txt"));
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                bool isNeedFirstHeadler = true;
                FlvEncoder encoder = new FlvEncoder();
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        var videoTag = encoder.EncoderVideoTag(fullpackage, isNeedFirstHeadler);
                        fileStream.Write(videoTag);
                        isNeedFirstHeadler = false;
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
        public void 测试可以播放的Flv4()
        {
            FileStream fileStream = null;
            int i = 0;
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_4.flv");
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_4.txt"));
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                bool isNeedFirstHeadler = true;
                FlvEncoder encoder = new FlvEncoder();
                foreach (var line in lines)
                {
                    i++;
                    var bytes = line.ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        var videoTag = encoder.EncoderVideoTag(fullpackage, isNeedFirstHeadler);
                        fileStream.Write(videoTag);
                        isNeedFirstHeadler = false;
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
        public void EncoderScriptTag()
        {
            FlvEncoder flvEncoder = new FlvEncoder();
            var hexData = flvEncoder.EncoderScriptTag(new SPSInfo { width = 288, height = 352 });
            Assert.Equal(155, hexData.Length);
        }

        [Fact]
        public void EncoderFirstVideoTag()
        {
            FlvEncoder flvEncoder = new FlvEncoder();
            var hexData = flvEncoder.EncoderFirstVideoTag(
                new SPSInfo { levelIdc = 0x14, profileIdc = 0x4d, profileCompat = 0 },
                new H264NALU { RawData = new byte[] { 0x67, 0x4D, 0, 0x14, 0x95, 0xA8, 0x58, 0x25, 0x90 } },
                new H264NALU { RawData = new byte[] { 0x68, 0xEE, 0x3C, 0x80 } },
                new H264NALU()
           );
            Assert.Equal(44, hexData.Length);
        }

        [Fact]
        public void EncoderVideoTagTest1()
        {
            try
            {
                var bytes = "3031636481E206AD0019013050370210000001749162905508480050000E0000000161E4025F0000030037A03031636481E206AE001901305037021000000174916290A508980050000E0000000161E4225F0000030037A0".ToHexBytes();
                FlvEncoder encoder = new FlvEncoder();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                JT1078Package fullpackage = JT1078Serializer.Merge(package);
                if (fullpackage != null)
                {
                    var videoTag = encoder.EncoderVideoTag(fullpackage, false);
                }
            }
            catch (Exception ex)
            {

            }
        }       
        [Fact]
        public void EncoderVideoTagTest2()
        {
            try
            {
                var bytes = "3031636481E2022D0019013050370210000001749179C79D05C80050000E0000000161E1E2FF000003006840".ToHexBytes();
                FlvEncoder encoder = new FlvEncoder();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                JT1078Package fullpackage = JT1078Serializer.Merge(package);
                if (fullpackage != null)
                {
                    var videoTag = encoder.EncoderVideoTag(fullpackage, false);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
