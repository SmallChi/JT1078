using JT1078.Protocol.Enums;
using JT1078.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol
{
    public static class JT1078Serializer
    {
        public static byte[] Serialize(JT1078Package package, int minBufferSize = 4096)
        {
            byte[] buffer = JT1078ArrayPool.Rent(minBufferSize);
            try
            {
                JT1078MessagePackWriter jT1078MessagePackReader = new JT1078MessagePackWriter(buffer);
                jT1078MessagePackReader.WriteUInt32(package.FH_Flag);
                jT1078MessagePackReader.WriteByte(package.Label1.ToByte());
                jT1078MessagePackReader.WriteByte(package.Label2.ToByte());
                jT1078MessagePackReader.WriteUInt16(package.SN);
                jT1078MessagePackReader.WriteBCD(package.SIM,12);
                jT1078MessagePackReader.WriteByte(package.LogicChannelNumber);
                jT1078MessagePackReader.WriteByte(package.Label3.ToByte());
                if (package.Label3.DataType != JT1078DataType.透传数据)
                {
                    jT1078MessagePackReader.WriteUInt64(package.Timestamp);
                }
                if (package.Label3.DataType != JT1078DataType.透传数据 &&
                    package.Label3.DataType != JT1078DataType.音频帧)
                {
                    jT1078MessagePackReader.WriteUInt16(package.LastIFrameInterval);
                    jT1078MessagePackReader.WriteUInt16(package.LastFrameInterval);
                }
                jT1078MessagePackReader.WriteUInt16((ushort)package.Bodies.Length); 
                jT1078MessagePackReader.WriteArray(package.Bodies);
                return jT1078MessagePackReader.FlushAndGetArray();
            }
            finally
            {
                JT1078ArrayPool.Return(buffer);
            }
        }

        public static JT1078Package Deserialize(ReadOnlySpan<byte> bytes)
        {
            JT1078Package jT1078Package = new JT1078Package();
            JT1078MessagePackReader jT1078MessagePackReader = new JT1078MessagePackReader(bytes);
            jT1078Package.FH_Flag = jT1078MessagePackReader.ReadUInt32();
            jT1078Package.Label1 = new JT1078Label1(jT1078MessagePackReader.ReadByte());
            jT1078Package.Label2 = new JT1078Label2(jT1078MessagePackReader.ReadByte());
            jT1078Package.SN = jT1078MessagePackReader.ReadUInt16();
            jT1078Package.SIM = jT1078MessagePackReader.ReadBCD(12);
            jT1078Package.LogicChannelNumber = jT1078MessagePackReader.ReadByte();
            jT1078Package.Label3 = new JT1078Label3(jT1078MessagePackReader.ReadByte());
            if (jT1078Package.Label3.DataType != JT1078DataType.透传数据)
            {
                jT1078Package.Timestamp = jT1078MessagePackReader.ReadUInt64();
            }
            if (jT1078Package.Label3.DataType != JT1078DataType.透传数据 &&
               jT1078Package.Label3.DataType != JT1078DataType.音频帧)
            {
                jT1078Package.LastIFrameInterval = jT1078MessagePackReader.ReadUInt16();
                jT1078Package.LastFrameInterval = jT1078MessagePackReader.ReadUInt16();
            }
            jT1078Package.DataBodyLength = jT1078MessagePackReader.ReadUInt16();
            jT1078Package.Bodies = jT1078MessagePackReader.ReadRemainArray().ToArray();
            return jT1078Package;
        }
    }
}
