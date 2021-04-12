using JT1078.Hls.Descriptors;
using JT1078.Hls.Enums;
using JT1078.Hls.MessagePack;
using JT1078.Protocol;
using JT1078.Protocol.Extensions;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections;
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
            //47 41 00 30 07 50 00 00 7B 0C 7E 00 00 00 01 E0 00 00 80 C0 0A 31 00 09 08 97 11 00 07 D8 61 00 00 00 01 09 F0 00 00 00 01 67 64 00 1F AC D9 40 88 1E 68 40 00 00 03 01 80 00 00 57 83 C6 0C 65 80 00 00 00 01 68 EB E3 CB 22 C0 00 00 01 06 05 FF FF AB DC 45 E9 BD E6 D9 48 B7 96 2C D8 20 D9 23 EE EF 78 32 36 34 20 2D 20 63 6F 72 65 20 31 35 38 20 72 32 39 38 34 20 33 37 35 39 66 63 62 20 2D 20 48 2E 32 36 34 2F 4D 50 45 47 2D 34 20 41 56 43 20 63 6F 64 65 63 20 2D 20 43 6F 70 79 6C 65 66 74 20 32 30 30 33 2D 32 30 31 39 20 2D 20 68 74 74 70 3A 2F 2F 77 77 77 2E

            //47 41 00 30 07 50 00 00 7B 0C 7E 00 00 00 01 E0 00 00 80 C0 0A 31 00 09 08 97 11 00 07 D8 61 00 00 00 01 09 F0 00 00 00 01 67 64 00 1F AC D9 40 88 1E 68 40 00 00 03 01 80 00 00 57 83 C6 0C 65 80 00 00 00 01 68 EB E3 CB 22 C0 00 00 01 06 05 FF FF AB DC 45 E9 BD E6 D9 48 B7 96 2C D8 20 D9 23 EE EF 78 32 36 34 20 2D 20 63 6F 72 65 20 31 35 38 20 72 32 39 38 34 20 33 37 35 39 66 63 62 20 2D 20 48 2E 32 36 34 2F 4D 50 45 47 2D 34 20 41 56 43 20 63 6F 64 65 63 20 2D 20 43 6F 70 79 6C 65 66 74 20 32 30 30 33 2D 32 30 31 39 20 2D 20 68 74 74 70 3A 2F 2F 77 77 77 2E
            //47 41 00 30 07 50 00 00 7B 0C 7E 00 00 00 01 E0 00 00 80 C0 0A 00 00 02 04 4B 00 00 01 EC 30 00 00 00 01 09 FF 000000016764001FACD940881E6840000003018000005783C60C65800000000168EBE3CB22C00000010605FFFFABDC45E9BDE6D948B7962CD820D923EEEF78323634202D20636F7265203135382072323938342033373539666362202D20482E3236342F4D5045472D342041564320636F646563202D20436F70796C65667420323030332D32303139202D20687474703A2F2F7777772E

            //47 
            //41 00 
            //30 
            //07 
            //50 
            //00 00 7B 0C 7E 
            //00 
            //00 00 01 
            //E0 
            //00 00 
            //80 
            //C0 
            //0A 
            //31 00 09 08 97 
            //11 00 07 D8 61 
            //00 00 00 01 
            //09 
            //F0

            //00 00 00 01 67 64 00 1F AC D9 40 88 1E 68 40 00 00 03 01 80 00 00 57 83 C6 0C 65 80 00 00 00 01 68 EB E3 CB 22 C0 00 00 01 06 05 FF FF AB DC 45 E9 BD E6 D9 48 B7 96 2C D8 20 D9 23 EE EF 78 32 36 34 20 2D 20 63 6F 72 65 20 31 35 38 20 72 32 39 38 34 20 33 37 35 39 66 63 62 20 2D 20 48 2E 32 36 34 2F 4D 50 45 47 2D 34 20 41 56 43 20 63 6F 64 65 63 20 2D 20 43 6F 70 79 6C 65 66 74 20 32 30 30 33 2D 32 30 31 39 20 2D 20 68 74 74 70 3A 2F 2F 77 77 77 2E 
            TS_Package package = new TS_Package();
            package.Header = new TS_Header();
            package.Header.PID = 0x100;
            package.Header.AdaptationFieldControl = AdaptationFieldControl.同时带有自适应域和有效负载;
            package.Header.ContinuityCounter = 0;
            package.Header.PackageType = PackageType.Data_Start;
            package.Header.PayloadUnitStartIndicator = 1;
            package.Header.Adaptation = new TS_AdaptationInfo();
            package.Header.Adaptation.Timestamp = 18900000;
            package.Header.Adaptation.PCRIncluded =  PCRInclude.包含;
            package.Payload = new PES_Package();
            package.Payload.PTS = 132171;
            package.Payload.DTS = 126000;
            package.Payload.PTS_DTS_Flag =  PTS_DTS_Flags.all;
            package.Payload.Payload = new ES_Package();
            package.Payload.Payload.NALUs = new List<byte[]>();
            package.Payload.Payload.NALUs.Add("00 00 00 01 67 64 00 1F AC D9 40 88 1E 68 40 00 00 03 01 80 00 00 57 83 C6 0C 65 80 00 00 00 01 68 EB E3 CB 22 C0 00 00 01 06 05 FF FF AB DC 45 E9 BD E6 D9 48 B7 96 2C D8 20 D9 23 EE EF 78 32 36 34 20 2D 20 63 6F 72 65 20 31 35 38 20 72 32 39 38 34 20 33 37 35 39 66 63 62 20 2D 20 48 2E 32 36 34 2F 4D 50 45 47 2D 34 20 41 56 43 20 63 6F 64 65 63 20 2D 20 43 6F 70 79 6C 65 66 74 20 32 30 30 33 2D 32 30 31 39 20 2D 20 68 74 74 70 3A 2F 2F 77 77 77 2E".ToHexBytes());
            TSMessagePackWriter writer = new TSMessagePackWriter(new byte[188]);
            package.ToBuffer(ref writer);
            var patData = writer.FlushAndGetArray().ToHexString();
        }

        [Fact]
        public void ToBufferTest2()
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
                        var sdt = tSEncoder.CreateSDT();
                        string sdtHEX = sdt.ToHexString();
                        fileStream.Write(sdt);
                        var pat = tSEncoder.CreatePAT();
                        string patHEX = pat.ToHexString();
                        fileStream.Write(pat);
                        var pmt = tSEncoder.CreatePMT();
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

        /// <summary>
        /// 可以用ffplay播放的JT1078_3.ts
        /// </summary>
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
                            var sdt = tSEncoder.CreateSDT();
                            string sdtHEX = sdt.ToHexString();
                            fileStream.Write(sdt);
                            var pat = tSEncoder.CreatePAT();
                            string patHEX = pat.ToHexString();
                            fileStream.Write(pat);
                            var pmt = tSEncoder.CreatePMT();
                            fileStream.Write(pmt);
                            var pes = tSEncoder.CreatePES(fullpackage, 18888);
                            fileStream.Write(pes);
                            isNeedFirstHeadler = false;
                        }
                        else
                        {
                            fileStream.Write(tSEncoder.CreatePES(fullpackage, 18888));
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

        /// <summary>
        ///         
        /// PTS[32..30]                                3              bslbf
        /// marker_bit                                 1              bslbf
        /// PTS[29..15]                                15             bslbf
        /// marker_bit                                 1              bslbf
        /// PTS[14..0]                                 15             bslbf
        /// marker_bit                                 1              bslbf
        /// '0001'                                     4              bslbf
        /// DTS[32..30]                                3              bslbf
        /// marker_bit                                 1              bslbf
        /// DTS[29..15]                                15             bslbf
        /// marker_bit                                 1              bslbf
        /// DTS[14..0]                                 15             bslbf
        /// marker_bit                                 1              bslbf
        /// 
        /// </summary>
        [Fact]
        public void PTSTest()
        {
            //pts
            //31 00 09 08 97 
            //'0011'
            long ptsvalue = 132171;
            var str = Convert.ToString(ptsvalue, 2).PadLeft(40, '0');
            str = str.Insert(str.Length, "1");
            str = str.Insert(str.Length - 16, "1");
            str = str.Insert(str.Length - 32, "1");
            str = str.Insert(str.Length - 36, "0011");
            str = str.Substring(str.Length - 40, 40);
            var pts = Convert.ToInt64(str, 2);
            //210453989527
        }

        [Fact]
        public void DTSTest1()
        {
            //dts
            //11 00 07 D8 61 
            long value = 126000;
            var str = Convert.ToString(value, 2).PadLeft(40, '0');
            str = str.Insert(str.Length, "1");
            str = str.Insert(str.Length - 16, "1");
            str = str.Insert(str.Length - 32, "1");
            str = str.Insert(str.Length - 36, "0001");
            str = str.Substring(str.Length - 40, 40);
            var dts = Convert.ToInt64(str, 2);
        }
    }
}
