using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    /// <summary>
    /// HintSampleEntry
    /// </summary>
    public class HintSampleEntry : SampleEntry
    {
        /// <summary>
        /// HintSampleEntry
        /// </summary>
        /// <param name="protocol"></param>
        public HintSampleEntry(string protocol) : base(protocol)
        {
        }
        public List<byte> Data { get; set; } = new List<byte>();

        public override void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            WriterSampleEntryToBuffer(ref writer);
            if (Data != null && Data.Count > 0)
            {
                foreach (var item in Data)
                {
                    writer.WriteByte(item);
                }
            }
            End(ref writer);
        }
    }
}
