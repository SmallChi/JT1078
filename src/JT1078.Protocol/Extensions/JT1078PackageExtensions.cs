using JT1078.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol
{
    /// <summary>
    /// 1078扩展类
    /// </summary>
    public static class JT1078PackageExtensions
    {
        /// <summary>
        /// 将音频数据包转换到1078包
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sim"></param>
        /// <param name="channelNo"></param>
        /// <param name="jT1078AVType"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static List<JT1078Package> ConvertAudio(this byte[] value,string sim,int channelNo, JT1078AVType jT1078AVType, ulong timestamp)
        {
            List<JT1078Package> jT1078Packages = new List<JT1078Package>();
            var buffer=Slice(value);
            if (buffer.Count == 1)
            {
                JT1078Package jT1078Package = new JT1078Package();
                jT1078Package.SIM = sim;
                jT1078Package.LogicChannelNumber = (byte)channelNo;
                jT1078Package.SN = SeqUtil.Increment(sim);
                jT1078Package.Timestamp = timestamp;
                jT1078Package.Label2 = new JT1078Label2(1, jT1078AVType);
                jT1078Package.Label3 = new JT1078Label3(JT1078DataType.音频帧, JT1078SubPackageType.原子包_不可被拆分);
                jT1078Package.Bodies = buffer[0];
                jT1078Packages.Add(jT1078Package);
            }
            else if(buffer.Count == 2)
            {
                JT1078Package jT1078Package1 = new JT1078Package();
                jT1078Package1.SIM = sim;
                jT1078Package1.LogicChannelNumber = (byte)channelNo;
                jT1078Package1.SN = SeqUtil.Increment(sim);
                jT1078Package1.Timestamp = timestamp;
                jT1078Package1.Label2 = new JT1078Label2(0, jT1078AVType);
                jT1078Package1.Label3 = new JT1078Label3(JT1078DataType.音频帧, JT1078SubPackageType.分包处理时的第一个包);
                jT1078Package1.Bodies = buffer[0];
                jT1078Packages.Add(jT1078Package1);
                JT1078Package jT1078Package2= new JT1078Package();
                jT1078Package2.SIM = sim;
                jT1078Package2.LogicChannelNumber = (byte)channelNo;
                jT1078Package2.SN = SeqUtil.Increment(sim);
                jT1078Package2.Timestamp = timestamp;
                jT1078Package2.Label2 = new JT1078Label2(1, jT1078AVType);
                jT1078Package2.Label3 = new JT1078Label3(JT1078DataType.音频帧, JT1078SubPackageType.分包处理时的最后一个包);
                jT1078Package2.Bodies = buffer[1];
                jT1078Packages.Add(jT1078Package2);
            }
            else
            {
                for (var i = 0; i < buffer.Count; i++)
                {
                    JT1078Package jT1078Package = new JT1078Package();
                    jT1078Package.SIM = sim;
                    jT1078Package.LogicChannelNumber = (byte)channelNo;
                    jT1078Package.SN = SeqUtil.Increment(sim);
                    jT1078Package.Timestamp = timestamp;
                    jT1078Package.Label2 = new JT1078Label2(0, jT1078AVType);
                    jT1078Package.Label3 = new JT1078Label3(JT1078DataType.音频帧);
                    jT1078Package.Bodies = buffer[i];
                    if (i == 0)
                    {
                        jT1078Package.Label3.SubpackageType = JT1078SubPackageType.分包处理时的第一个包;
                    }
                    else if (i == (buffer.Count - 1))
                    {
                        jT1078Package.Label2.M = 1;
                        jT1078Package.Label3.SubpackageType = JT1078SubPackageType.分包处理时的最后一个包;
                    }
                    else
                    {
                        jT1078Package.Label3.SubpackageType = JT1078SubPackageType.分包处理时的中间包;
                    }
                    jT1078Packages.Add(jT1078Package);
                }
            }
            return jT1078Packages;
        }

        /// <summary>
        /// 将视频数据包转换到1078包
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sim"></param>
        /// <param name="channelNo"></param>
        /// <param name="jT1078AVType"></param>
        /// <param name="jT1078DataType"></param>
        /// <param name="timestamp"></param>
        /// <param name="lastIFrameInterval"></param>
        /// <param name="lastFrameInterval"></param>
        /// <returns></returns>
        public static List<JT1078Package> ConvertVideo(this byte[] value, string sim, 
            int channelNo,
            JT1078AVType jT1078AVType,
            JT1078DataType jT1078DataType, 
            ulong timestamp,
            int lastIFrameInterval,
            int lastFrameInterval)
        {
            List<JT1078Package> jT1078Packages = new List<JT1078Package>();
            var buffer = Slice(value);
            if (buffer.Count == 1)
            {
                JT1078Package jT1078Package = new JT1078Package();
                jT1078Package.SIM = sim;
                jT1078Package.LogicChannelNumber = (byte)channelNo;
                jT1078Package.SN = SeqUtil.Increment(sim);
                jT1078Package.Timestamp = timestamp;
                jT1078Package.LastIFrameInterval = (ushort)lastIFrameInterval;
                jT1078Package.LastFrameInterval = (ushort)lastFrameInterval;
                jT1078Package.Label2 = new JT1078Label2(1, jT1078AVType);
                jT1078Package.Label3 = new JT1078Label3(jT1078DataType, JT1078SubPackageType.原子包_不可被拆分);
                jT1078Package.Bodies = buffer[0];
                jT1078Packages.Add(jT1078Package);
            }
            else if (buffer.Count == 2)
            {
                JT1078Package jT1078Package1 = new JT1078Package();
                jT1078Package1.SIM = sim;
                jT1078Package1.LogicChannelNumber = (byte)channelNo;
                jT1078Package1.SN = SeqUtil.Increment(sim);
                jT1078Package1.Timestamp = timestamp;
                jT1078Package1.LastIFrameInterval = (ushort)lastIFrameInterval;
                jT1078Package1.LastFrameInterval = (ushort)lastFrameInterval;
                jT1078Package1.Label2 = new JT1078Label2(0, jT1078AVType);
                jT1078Package1.Label3 = new JT1078Label3(jT1078DataType, JT1078SubPackageType.分包处理时的第一个包);
                jT1078Package1.Bodies = buffer[0];
                jT1078Packages.Add(jT1078Package1);
                JT1078Package jT1078Package2 = new JT1078Package();
                jT1078Package2.SIM = sim;
                jT1078Package2.LogicChannelNumber = (byte)channelNo;
                jT1078Package2.SN = SeqUtil.Increment(sim);
                jT1078Package2.Timestamp = timestamp;
                jT1078Package2.LastIFrameInterval = (ushort)lastIFrameInterval;
                jT1078Package2.LastFrameInterval = (ushort)lastFrameInterval;
                jT1078Package2.Label2 = new JT1078Label2(1, jT1078AVType);
                jT1078Package2.Label3 = new JT1078Label3(jT1078DataType, JT1078SubPackageType.分包处理时的最后一个包);
                jT1078Package2.Bodies = buffer[1];
                jT1078Packages.Add(jT1078Package2);
            }
            else
            {
                for (var i = 0; i < buffer.Count; i++)
                {
                    JT1078Package jT1078Package = new JT1078Package();
                    jT1078Package.SIM = sim;
                    jT1078Package.LogicChannelNumber = (byte)channelNo;
                    jT1078Package.SN = SeqUtil.Increment(sim);
                    jT1078Package.Timestamp = timestamp;
                    jT1078Package.LastIFrameInterval = (ushort)lastIFrameInterval;
                    jT1078Package.LastFrameInterval = (ushort)lastFrameInterval;
                    jT1078Package.Label2 = new JT1078Label2(0, jT1078AVType);
                    jT1078Package.Label3 = new JT1078Label3(jT1078DataType);
                    jT1078Package.Bodies = buffer[i];
                    if (i == 0)
                    {
                        jT1078Package.Label3.SubpackageType = JT1078SubPackageType.分包处理时的第一个包;
                    }
                    else if (i == (buffer.Count - 1))
                    {
                        jT1078Package.Label2.M = 1;
                        jT1078Package.Label3.SubpackageType = JT1078SubPackageType.分包处理时的最后一个包;
                    }
                    else
                    {
                        jT1078Package.Label3.SubpackageType = JT1078SubPackageType.分包处理时的中间包;
                    }
                    jT1078Packages.Add(jT1078Package);
                }
            }
            return jT1078Packages;
        }

        /// <summary>
        /// 将透传数据包转换到1078包
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sim"></param>
        /// <param name="channelNo"></param>
        /// <returns></returns>
        public static List<JT1078Package> ConvertPassthrough(this byte[] value, string sim, int channelNo)
        {
            List<JT1078Package> jT1078Packages = new List<JT1078Package>();
            var buffer = Slice(value);
            if (buffer.Count == 1)
            {
                JT1078Package jT1078Package = new JT1078Package();
                jT1078Package.SIM = sim;
                jT1078Package.LogicChannelNumber = (byte)channelNo;
                jT1078Package.SN = SeqUtil.Increment(sim);
                jT1078Package.Label2 = new JT1078Label2(1,  JT1078AVType.透传);
                jT1078Package.Label3 = new JT1078Label3(JT1078DataType.透传数据, JT1078SubPackageType.原子包_不可被拆分);
                jT1078Package.Bodies = buffer[0];
                jT1078Packages.Add(jT1078Package);
            }
            else if (buffer.Count == 2)
            {
                JT1078Package jT1078Package1 = new JT1078Package();
                jT1078Package1.SIM = sim;
                jT1078Package1.LogicChannelNumber = (byte)channelNo;
                jT1078Package1.SN = SeqUtil.Increment(sim);
                jT1078Package1.Label2 = new JT1078Label2(0, JT1078AVType.透传);
                jT1078Package1.Label3 = new JT1078Label3(JT1078DataType.透传数据, JT1078SubPackageType.分包处理时的第一个包);
                jT1078Package1.Bodies = buffer[0];
                jT1078Packages.Add(jT1078Package1);
                JT1078Package jT1078Package2 = new JT1078Package();
                jT1078Package2.SIM = sim;
                jT1078Package2.LogicChannelNumber = (byte)channelNo;
                jT1078Package2.SN = SeqUtil.Increment(sim);
                jT1078Package2.Label2 = new JT1078Label2(1, JT1078AVType.透传);
                jT1078Package2.Label3 = new JT1078Label3(JT1078DataType.透传数据, JT1078SubPackageType.分包处理时的最后一个包);
                jT1078Package2.Bodies = buffer[1];
                jT1078Packages.Add(jT1078Package2);
            }
            else
            {
                for (var i = 0; i < buffer.Count; i++)
                {
                    JT1078Package jT1078Package = new JT1078Package();
                    jT1078Package.SIM = sim;
                    jT1078Package.LogicChannelNumber = (byte)channelNo;
                    jT1078Package.SN = SeqUtil.Increment(sim);
                    jT1078Package.Label2 = new JT1078Label2(0, JT1078AVType.透传);
                    jT1078Package.Label3 = new JT1078Label3(JT1078DataType.透传数据);
                    jT1078Package.Bodies = buffer[i];
                    if (i == 0)
                    {
                        jT1078Package.Label3.SubpackageType = JT1078SubPackageType.分包处理时的第一个包;
                    }
                    else if (i == (buffer.Count - 1))
                    {
                        jT1078Package.Label2.M = 1;
                        jT1078Package.Label3.SubpackageType = JT1078SubPackageType.分包处理时的最后一个包;
                    }
                    else
                    {
                        jT1078Package.Label3.SubpackageType = JT1078SubPackageType.分包处理时的中间包;
                    }
                    jT1078Packages.Add(jT1078Package);
                }
            }
            return jT1078Packages;
        }

        /// <summary>
        /// 切分数据包
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<byte[]> Slice(byte[] value)
        {
            const int MAXCONTENTLENGTH = 950;
            if (value.Length <= MAXCONTENTLENGTH)
            {
                return new List<byte[]>() { value };
            }
            List<byte[]> s = new List<byte[]>();
            var quotient = value.Length / MAXCONTENTLENGTH;
            var remainder = value.Length % MAXCONTENTLENGTH;
            if (remainder != 0)
            {
                quotient = quotient + 1;
            }
            ReadOnlySpan<byte> valueReadOnlySpan = value;
            for (int i = 1; i <= quotient; i++)
            {
                if (i == quotient)
                {
                    s.Add(valueReadOnlySpan.Slice((i - 1) * MAXCONTENTLENGTH).ToArray());
                }
                else
                {
                    s.Add(valueReadOnlySpan.Slice((i - 1) * MAXCONTENTLENGTH, MAXCONTENTLENGTH).ToArray());
                }
            }
            return s;
        }
    }
}
