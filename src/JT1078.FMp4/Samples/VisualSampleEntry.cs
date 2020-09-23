using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Samples
{
    /// <summary>
    /// VisualSampleEntry
    /// </summary>
    public abstract class VisualSampleEntry : SampleEntry
    {
        /// <summary>
        /// VisualSampleEntry
        /// </summary>
        /// <param name="boxType"></param>
        public VisualSampleEntry(string boxType) : base(boxType)
        {
        }

        public ushort PreDefined1 { get; set; }
        public ushort Reserved1 { get; set; }
        public uint[] PreDefined2 { get; set; } = new uint[3];
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public uint HorizreSolution { get; set; }= 0x00480000;
        public uint VertreSolution { get; set; }= 0x00480000;
        public uint Reserved3{ get; set; }
        public ushort FrameCount { get; set; } = 1;
        public string CompressorName { get; set; }
        public ushort Depth { get; set; } = 0x0018;
        public short PreDefined3 { get; set; } = 0x1111;
        /// <summary>
        /// clap
        /// optional
        /// </summary>
        public CleanApertureBox Clap { get; set; }
        /// <summary>
        /// pasp
        /// optional
        /// </summary>
        public PixelAspectRatioBox Pasp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected void WriterVisualSampleEntryToBuffer(ref FMp4MessagePackWriter writer)
        {
            writer.WriteUInt16(PreDefined1);
            writer.WriteUInt16(Reserved1);
            foreach(var item in PreDefined2)
            {
                writer.WriteUInt32(item);
            }
            writer.WriteUInt16(Width);
            writer.WriteUInt16(Height);
            writer.WriteUInt32(HorizreSolution);
            writer.WriteUInt32(VertreSolution);
            writer.WriteUInt32(Reserved3);
            writer.WriteUInt16(FrameCount);
            if (string.IsNullOrEmpty(CompressorName))
            {
                CompressorName = "";
            }
            writer.WriteUTF8(CompressorName.PadLeft(32, '\0'));
            writer.WriteUInt16(Depth);
            writer.WriteInt16(PreDefined3);
            if (Clap != null)
            {
                Clap.ToBuffer(ref writer);
            }
            if (Pasp != null)
            {
                Pasp.ToBuffer(ref writer);
            }
        }
    }
}
