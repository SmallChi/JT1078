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
                        if(tmp!=null && tmp.Count > 0)
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

            }
            finally
            {
                fileStream?.Close();
                fileStream?.Dispose();
            }
        }
    }
}
