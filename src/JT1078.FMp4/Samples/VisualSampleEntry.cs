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
        const string COMPRESSORNAME = "jt1078&SmallChi(Koike)&TK";
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
#if DEBUG
        public string CompressorName { get; set; }
#else
        public string CompressorName { get; set; } = COMPRESSORNAME;
#endif
        public ushort Depth { get; set; } = 0x0018;
        public ushort PreDefined3 { get; set; } = 0x1111;
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
                //CompressorNameLength
                writer.WriteByte(0);
                writer.WriteArray(new byte[31]);
            }
            else
            {
                //CompressorNameLength
                writer.WriteByte((byte)CompressorName.Length);
                writer.WriteUTF8(CompressorName.PadRight(31, '\0'));
            }
            writer.WriteUInt16(Depth);
            writer.WriteUInt16(PreDefined3);
        }
    }
}
