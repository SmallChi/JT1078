using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using JT1078.Protocol.Enums;
using JT1078.Protocol.Extensions;
using JT1078.Protocol.MessagePack;

namespace JT1078.Protocol
{
    public static class JT1078Serializer
    {
        private readonly static ConcurrentDictionary<string, JT1078Package> livePackageGroup = new(StringComparer.OrdinalIgnoreCase);
        private readonly static ConcurrentDictionary<string, JT1078Package> historyPackageGroup = new(StringComparer.OrdinalIgnoreCase);
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

        /// <summary>
        /// merge package
        /// <para>due to the agreement cannot distinguish the Stream type, so need clear Stream type at the time of merger, avoid confusion</para>
        /// </summary>
        /// <param name="jT1078Package">package of jt1078</param>
        /// <param name="channelType">package type is live or history</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static JT1078Package Merge(JT1078Package jT1078Package,JT808ChannelType channelType)
        {
            string cacheKey = jT1078Package.GetKey();
            var packageGroup = channelType switch
            {
                JT808ChannelType.Live=>livePackageGroup,
                JT808ChannelType.History=>historyPackageGroup,
                _=>throw new Exception("channel type error")
            };
            if (jT1078Package.Label3.SubpackageType == JT1078SubPackageType.分包处理时的第一个包)
            {
                packageGroup.TryRemove(cacheKey, out _);
                packageGroup.TryAdd(cacheKey, jT1078Package);
                return default;
            }
            else if (jT1078Package.Label3.SubpackageType == JT1078SubPackageType.分包处理时的中间包)
            {
                if (packageGroup.TryGetValue(cacheKey, out var tmpPackage))
                {
                    var totalLength = tmpPackage.Bodies.Length + jT1078Package.Bodies.Length;
                    byte[] poolBytes = JT1078ArrayPool.Rent(totalLength);
                    Span<byte> tmpSpan = poolBytes;
                    tmpPackage.Bodies.CopyTo(tmpSpan);
                    jT1078Package.Bodies.CopyTo(tmpSpan.Slice(tmpPackage.Bodies.Length));
                    tmpPackage.Bodies = tmpSpan.Slice(0, totalLength).ToArray();
                    JT1078ArrayPool.Return(poolBytes);
                    packageGroup[cacheKey] = tmpPackage;
                }
                return default;
            }
            else if (jT1078Package.Label3.SubpackageType == JT1078SubPackageType.分包处理时的最后一个包)
            {
                if (packageGroup.TryRemove(cacheKey, out var tmpPackage))
                {
                    var totalLength = tmpPackage.Bodies.Length + jT1078Package.Bodies.Length;
                    byte[] poolBytes = JT1078ArrayPool.Rent(totalLength);
                    Span<byte> tmpSpan = poolBytes;
                    tmpPackage.Bodies.CopyTo(tmpSpan);
                    jT1078Package.Bodies.CopyTo(tmpSpan.Slice(tmpPackage.Bodies.Length));
                    tmpPackage.Bodies = tmpSpan.Slice(0, totalLength).ToArray();
                    JT1078ArrayPool.Return(poolBytes);
                    return tmpPackage;
                }
                return default;
            }
            else
            {
                return jT1078Package;
            }
        }
        public static byte[] AnalyzeJsonBuffer(ReadOnlySpan<byte> bytes, JsonWriterOptions options = default)
        {
            JT1078MessagePackReader jT1078MessagePackReader = new JT1078MessagePackReader(bytes);
            using (MemoryStream memoryStream = new MemoryStream())
            using (Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream, options))
            {
                writer.WriteStartObject();
                var header = jT1078MessagePackReader.ReadUInt32();
                writer.WriteNumber($"[{header.ReadNumber()}]头部", header);
                var val1 = jT1078MessagePackReader.ReadByte();
                var label1 = new JT1078Label1(val1);
                var labelSpan = val1.ReadBinary();
                writer.WriteStartObject($"[{labelSpan.ToString()}]object1[{val1.ReadNumber()}]");
                writer.WriteNumber($"({labelSpan.Slice(0,2).ToString()})V[固定为2]", label1.V);
                writer.WriteNumber($"({labelSpan[2]})P[固定为0]", label1.P);
                writer.WriteNumber($"({labelSpan[3]})X[RTP头是否需要扩展位固定为0]", label1.X);
                writer.WriteNumber($"({labelSpan.Slice(4).ToString()})CC[固定为1]", label1.CC);
                writer.WriteEndObject();

                var val2 = jT1078MessagePackReader.ReadByte();
                var label2 = new JT1078Label2(val2);
                var label2Span = val2.ReadBinary();
                writer.WriteStartObject($"[{label2Span.ToString()}]object2[{val2.ReadNumber()}]");
                writer.WriteNumber($"({label2Span.Slice(0, 4).ToString()})M[确定是否是完整数据帧的边界]", label2.M);
                writer.WriteString($"({label2Span.Slice(4).ToString()})PT[负载类型]", label2.PT.ToString());
                writer.WriteEndObject();

                var sn = jT1078MessagePackReader.ReadUInt16();
                writer.WriteNumber($"{sn.ReadNumber()}[序列号]", sn);
                var sim = jT1078MessagePackReader.ReadBCD(12);
                writer.WriteString($"[终端设备SIM卡号]", sim);
                var logicChannelNumber = jT1078MessagePackReader.ReadByte();
                writer.WriteNumber($"{logicChannelNumber.ReadNumber()}[逻辑通道号]", logicChannelNumber);

                var val3 = jT1078MessagePackReader.ReadByte();
                var label3 = new JT1078Label3(val3);
                var label3Span = val3.ReadBinary();
                writer.WriteStartObject($"[{label3Span.ToString()}]object3[{val3.ReadNumber()}]");
                writer.WriteString($"({label3Span.Slice(0, 4).ToString()})[数据类型]", label3.DataType.ToString());
                writer.WriteString($"({label3Span.Slice(4).ToString()})[分包处理标记]", label3.SubpackageType.ToString());
                writer.WriteEndObject();
                if (label3.DataType != JT1078DataType.透传数据)
                {
                    var timestamp = jT1078MessagePackReader.ReadUInt64();
                    writer.WriteNumber($"{timestamp.ReadNumber()}[标识此RTP数据包当前帧的相对时间,单位毫秒(ms)]", timestamp);
                }
                if (label3.DataType != JT1078DataType.透传数据 &&
                    label3.DataType != JT1078DataType.音频帧)
                {
                    var lastIFrameInterval = jT1078MessagePackReader.ReadUInt16();
                    writer.WriteNumber($"{lastIFrameInterval.ReadNumber()}[该帧与上一个关键帧之间的时间间隔,单位毫秒(ms)]", lastIFrameInterval);
                    var lastFrameInterval = jT1078MessagePackReader.ReadUInt16();
                    writer.WriteNumber($"{lastFrameInterval.ReadNumber()}[该帧与上一个关键帧之间的时间间隔,单位毫秒(ms)]", lastFrameInterval);
                }
                var dataBodyLength = jT1078MessagePackReader.ReadUInt16();
                writer.WriteNumber($"{dataBodyLength.ReadNumber()}[数据体长度]", dataBodyLength);
                var bodies = jT1078MessagePackReader.ReadRemainArray().ToArray();
                writer.WriteString("[数据体]", string.Join(" ", (bodies).Select(p => p.ToString("X2"))));
                writer.WriteEndObject();
                writer.Flush();
                return memoryStream.ToArray();
            }
        }
        public static string Analyze(ReadOnlySpan<byte> bytes,JsonWriterOptions options = default)
        {
            string json = Encoding.UTF8.GetString(AnalyzeJsonBuffer(bytes, options));
            return json;
        }
    }
}
