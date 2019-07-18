using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Extensions.JT1078.MessageBody;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace JT809.Protocol.Extensions.JT1078.Test
{
    public class JT809_JT1078_0x9700Test
    {
        JT809Serializer JT809Serializer;
        public JT809_JT1078_0x9700Test()
        {
            IServiceCollection serviceDescriptors1 = new ServiceCollection();
            serviceDescriptors1
                            .AddJT809Configure()
                            .AddJT1078Configure();
            var ServiceProvider1 = serviceDescriptors1.BuildServiceProvider();
            var defaultConfig = ServiceProvider1.GetRequiredService<IJT809Config>();
             JT809Serializer = defaultConfig.GetSerializer();
        }



        [Fact]
        public void Test1()
        {
            JT809_JT1078_0x9700 jT809_JT1078_0x9700 = new JT809_JT1078_0x9700()
            {
                VehicleNo="粤B12345",
                VehicleColor= Protocol.Enums.JT809VehicleColorType.黄色,
                SubBusinessType= (ushort)JT809_JT1078_SubBusinessType.时效口令请求应答消息, 
                SubBodies=  new JT809_JT1078_0x9700_0x9702() 
            };
            var hex = JT809Serializer.Serialize(jT809_JT1078_0x9700).ToHexString();
            Assert.Equal("D4C14231323334350000000000000000000000000002970200000000", hex);
        }

        [Fact]
        public void Test2()
        {
            var jT809_JT1078_0x9700 = JT809Serializer.Deserialize<JT809_JT1078_0x9700>("D4C14231323334350000000000000000000000000002970200000000".ToHexBytes());
            Assert.Equal("粤B12345", jT809_JT1078_0x9700.VehicleNo);
            Assert.Equal(Protocol.Enums.JT809VehicleColorType.黄色, jT809_JT1078_0x9700.VehicleColor);
            Assert.Equal((ushort)JT809_JT1078_SubBusinessType.时效口令请求应答消息, jT809_JT1078_0x9700.SubBusinessType);
        }
    }
}
