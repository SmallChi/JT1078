using JT1078.Protocol.Enums;
using JT1078.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JT1078.Protocol.Test.Extensions
{
    public class JT1078PackageExtensionsTest
    {
        [Fact]
        public void Test()
        {
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"H264", "JT1078_1.txt"));
            JT1078Package merge = null;
            int mergeBodyLength = 0;
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[6].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                merge = JT1078Serializer.Merge(package,JT808ChannelType.Live);
            }
            var packages = merge.Bodies.ConvertVideo(merge.SIM, merge.LogicChannelNumber, merge.Label2.PT, merge.Label3.DataType, 
                merge.Timestamp, merge.LastFrameInterval, merge.LastFrameInterval);
            for(int i=0;i< packages.Count;i++)
            {
                var data = lines[i].Split(',');
                var bytes1 = data[6].ToHexBytes();
                var bytes2 = JT1078Serializer.Serialize(packages[i]);
                Assert.Equal(bytes1, bytes2);
            }
        }
    }
}
