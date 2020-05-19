using JT1078.Hls.Descriptors;
using JT1078.Hls.Enums;
using JT1078.Hls.MessagePack;
using JT1078.Protocol;
using JT1078.Protocol.Extensions;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace JT1078.Hls.Test
{
    /// <summary>
    /// 使用doc/video/demo0.ts
    /// </summary>
    public class TS_Package_Test
    {
        [Fact]
        public void ToBufferTest1()
        {
            FileStream fileStream = null;
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.ts");
                File.Delete(filepath);
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.txt"));
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                TSEncoder tSEncoder = new TSEncoder();
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        var pat = tSEncoder.CreatePAT(fullpackage);
                        fileStream.Write(pat);
                        var pmt = tSEncoder.CreatePMT(fullpackage);
                        fileStream.Write(pmt);
                        var pes = tSEncoder.CreatePES(fullpackage);
                        fileStream.Write(pes);
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
        public void ToBufferTest3()
        {
            FileStream fileStream = null;
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.ts");
                File.Delete(filepath);
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.txt"));
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                bool isNeedFirstHeadler = true;
                TSEncoder tSEncoder = new TSEncoder();
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        if (isNeedFirstHeadler)
                        {
                            fileStream.Write(tSEncoder.CreatePAT(fullpackage));
                            fileStream.Write(tSEncoder.CreatePMT(fullpackage));
                            fileStream.Write(tSEncoder.CreatePES(fullpackage,1888));
                            isNeedFirstHeadler = false;
                        }
                        else
                        {
                            fileStream.Write(tSEncoder.CreatePES(fullpackage, 1888));
                        }              
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
    }
}
