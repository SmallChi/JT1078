using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Flv.Extensions;
using JT1078.Protocol;
using System.IO;
using System.Linq;
using JT1078.Protocol.Enums;

namespace JT1078.Flv.Test
{
    public class H264ToFlvTest
    {
        /// <summary>
        /// jt1078 转成 H264数据包
        /// </summary>
        [Fact]
        public void Test_1()
        {
            FileStream fileStream = null;
            try
            {
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.txt"));
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.h264");
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    fileStream.Write(package.Bodies);
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
