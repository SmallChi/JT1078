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
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078.txt"));
            int mergeBodyLength = 0;
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[5].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                Package = JT1078Serializer.Merge(package);
            }
            Flv.H264.H264Decoder decoder = new Flv.H264.H264Decoder();
            var nalus = decoder.ParseNALU(Package);
            foreach(var item in nalus)
            {
                item.RawData = decoder.DiscardEmulationPreventionBytes(item.RawData);
            }
            Assert.Equal(4, nalus.Count);

            FlvEncoder encoder = new FlvEncoder();
            var contents = encoder.FlvFirstFrame(nalus);
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078.flv");
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
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078.txt"));
                int mergeBodyLength = 0;
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[5].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    mergeBodyLength += package.DataBodyLength;
                    Package = JT1078Serializer.Merge(package);
                }
                Flv.H264.H264Decoder decoder = new Flv.H264.H264Decoder();
                var nalus = decoder.ParseNALU(Package);
                foreach (var item in nalus)
                {
                    item.RawData = decoder.DiscardEmulationPreventionBytes(item.RawData);
                }

                Assert.Equal(4, nalus.Count);

                FlvEncoder encoder = new FlvEncoder();
                var contents = encoder.FlvFirstFrame(nalus);
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.flv");
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                fileStream.Write(contents);

                var lines2 = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.txt"));
                JT1078Package Package2 = null;
                List<H264NALU> IOrIDRNALus = new List<H264NALU>();
                foreach (var line in lines2)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    Package2 = JT1078Serializer.Merge(package);
                    if (Package2 != null)
                    {
                        var nalus2 = decoder.ParseNALU(Package2)
                                            .Where(w => (w.NALUHeader.NalUnitType == 1 ||
                                            w.NALUHeader.NalUnitType == 5))
                                            .ToList()
                                            ;
                        IOrIDRNALus = IOrIDRNALus.Concat(nalus2).ToList();
                    }
                }
                var totalPage = (IOrIDRNALus.Count + 10 - 1) / 10;
                for(var i=0;i< totalPage; i++)
                {
                    var flv2 = encoder.FlvOtherFrame(IOrIDRNALus.Skip(i * 10).Take(10).ToList());
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
