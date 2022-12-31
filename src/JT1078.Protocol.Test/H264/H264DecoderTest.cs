using JT1078.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using System.Linq;
using Newtonsoft.Json;
using JT1078.Protocol.H264;
using JT808.Protocol.Extensions;
using JT1078.Protocol.MessagePack;
using JT1078.Protocol.Enums;

namespace JT1078.Protocol.Test.H264
{
    public class H264DecoderTest
    {

        [Fact]
        public void ParseNALUTest1()
        {
            H264Decoder demuxer = new H264Decoder();
            var hexData = "3031636481e2274b01234567899901100000016cd3ed2fa900500050037f0000000161e022bfb28b470069769838a02eb3b5cc4bccd7b37eb98cd65b2a76ce85a6c82876385a8f646da3d0abb6af58ad1dc7db5dab9719577d9968f7efc9836f4bc8fb7e32f5689b190e9e6e07d2f4448efff9147175f22a4b0f83fd24374b881dddcac5998ab9b03d991eb0045ed98826699a5601cdd6bd1e4b3ca53c175ef36e9f46969018c01b7754304790a17888072647e4c473182f05922770f5aff81e02ea46637b5e90b4382f100a2b55c5f49e0fa0b8203804dcd7714fac1dbd15d0583f8ebdfdf980770d58b38349ad7ec5682f01dae88b2117c6f3b3b58b642354bc23735d9c269028df9774e8182f5d636cec09829886de3ee83406e035bffcce3ed24af0f9a2dee150fab93303f2139c8525eeb89516df169137ef46ed99962343ea7b078bcddd284e44be62936a6c44042d89acb17973eb65e1c21faee6d221627ba834d369f4c023acc122d7d3dd3f55a6c4fb2ba2854498d4041ea00b1252c6d9eb57ac0b10313e8905843054dfbf4c847180aee40980bdfe5cb53133214c77c8e95ebe7601cd0d331f75281d1fadd122985133e74855e13cbb10220bc6e0c946b519b35d933a29e38e175b1398dc0946fc58ec7b686b3aac4bfa9e42bb8e643be04dc55036289c7d378fe37c6c3e1fc06015f8b402a65afac23d78d9fb5e3e0115195c11227ee6f261b67834b51bc665e8a6803af835b5032786e58e7f66049dc767520bae35d527a72458834a846dcd1cccf198d772d3ce0a2db5eb1d8270c483840309e224b2798d9271b30729e0c4f51eb7798d0dd0d8adb3d246e686b42e47f351c482dda833747649e421bbbe04098ca220d4e0e60a3b2854029c95c70394a2991314c50199f2158b68635a622f55ebda0b5e41328843552723fba1d5228963e8562c9105838ec9d341fef19b33a438d903ebcef32cec2ad39fd32aff14c79f07de4ca75e65d20f6ee36e464ee3e5c27871ab594ae3a0b43583c6f4d55ac8b71090fca9a1f4379576e07678983935d6f3a236504c2e4d31f99d62c999f41c11b06c41a6adb07b6e62f50ec5c8dbdb6641c5bc55a695f0f7b4f4d430c1e2f38a77edfc3513f943e1c1d4c0a81ef6c26cc4e7b157d318b49802ea94aa8ab38dadd4986e5330ab49daaec010123c39da63943bfe63bdd1cdd6beec19e1469b989ac10877a24065d7b01ff9cee35632e97243eb7acd79ce9d188b786e44bbe0e922be205c1224a41f387fae0f20d4a203d0ba64a366b8adbe2b80".ToHexBytes();
            JT1078Package package = JT1078Serializer.Deserialize(hexData);
            var nalus = demuxer.ParseNALU(package);
            Assert.Single(nalus);
            var nalu = nalus[0];
            Assert.Equal(0, nalu.NALUHeader.ForbiddenZeroBit);
            Assert.Equal(3, nalu.NALUHeader.NalRefIdc);
            Assert.Equal(NalUnitType.SLICE, nalu.NALUHeader.NalUnitType);
        }

        [Fact]
        public void ParseNALUTest2()
        {
            JT1078Package Package=null;
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_1.txt"));
            int mergeBodyLength = 0;
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[6].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                Package = JT1078Serializer.Merge(package,JT808ChannelType.Live);
            }
            H264Decoder decoder = new H264Decoder();
            var nalus = decoder.ParseNALU(Package);
            Assert.Equal(4, nalus.Count);

            //SPS -> 7
            var spsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.SPS);
            Assert.NotNull(spsNALU);
            spsNALU.RawData = decoder.DiscardEmulationPreventionBytes(spsNALU.RawData);
            //"Z00AFJWoWCWQ"
            var spsRawDataHex = JsonConvert.SerializeObject(spsNALU.RawData);
            ExpGolombReader h264GolombReader = new ExpGolombReader(spsNALU.RawData);
            //(77, 20, 0, 352, 288)
            var spsInfo = h264GolombReader.ReadSPS();
            Assert.Equal(77, spsInfo.profileIdc);
            Assert.Equal(20, spsInfo.levelIdc);
            Assert.Equal(0u, spsInfo.profileCompat);
            Assert.Equal(288, spsInfo.height);
            Assert.Equal(352, spsInfo.width);

            //PPS -> 8
            var ppsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.PPS);
            Assert.NotNull(ppsNALU);

            //IDR -> 5  关键帧
            var idrNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.IDR);
            Assert.NotNull(idrNALU);

            //SEI -> 6  
            var seiNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.SEI);
            Assert.NotNull(seiNALU);
        }

        [Fact]
        public void ParseNALUTest3()
        {
            string file = "jt1078_1";
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", $"{file}.txt"));
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", $"{file}.h264");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[6].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                fileStream.Write(package.Bodies);
            }
            fileStream.Close();
        }

        [Fact]
        public void ParseNALUTest4()
        {
            string file = "jt1078_5";
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", $"{file}.txt"));
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", $"{file}.h264");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[1].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                fileStream.Write(package.Bodies);
            }
            fileStream.Close();
        }

        [Fact]
        public void ParseNALUTest5()
        {
            string file = "jt1078_6";
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", $"{file}.txt"));
            List<H264NALU> nALUs = new List<H264NALU>();
            H264Decoder decoder = new H264Decoder();
            foreach (var line in lines)
            {
                var bytes = line.ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                var packageMerge = JT1078Serializer.Merge(package,JT808ChannelType.Live);
                if (packageMerge != null)
                {
                    var nalus = decoder.ParseNALU(packageMerge);
                    nALUs = nALUs.Concat(nalus).ToList();
                }
            }
        }
    }
}
