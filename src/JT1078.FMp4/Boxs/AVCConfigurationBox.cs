using JT1078.FMp4.Interfaces;
using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// avcC
    /// </summary>
    public class AVCConfigurationBox : Mp4Box,IFMp4MessagePackFormatter
    {
        /// <summary>
        /// avcC
        /// </summary>
        public AVCConfigurationBox() : base("avcC")
        {
        }
        #region 
        //public const int LengthSizeMinusOnePaddingBits = 63;
        //public const int NumberOfSequenceParameterSetsPaddingBits = 7;
        //public const int ChromaFormatPaddingBits = 31;
        //public const int BitDepthLumaMinus8PaddingBits = 31;
        //public const int BitDepthChromaMinus8PaddingBits = 31;
        #endregion
        //AVCDecoderConfigurationRecord 结构的定义：
        //aligned(8) class AVCDecoderConfigurationRecord
        //{
        //unsigned int (8) configurationVersion = 1;
        //unsigned int (8) AVCProfileIndication;
        //unsigned int (8) profile_compatibility;
        //unsigned int (8) AVCLevelIndication;
        //bit(6) reserved = ‘111111’b;
        //unsigned int (2) lengthSizeMinusOne;
        //bit(3) reserved = ‘111’b;
        //unsigned int (5) numOfSequenceParameterSets;
        //for (i=0; i<numOfSequenceParameterSets; i++) {
        //unsigned int (16) sequenceParameterSetLength ;
        //bit(8*sequenceParameterSetLength) sequenceParameterSetNALUnit;
        //}
        //unsigned int (8) numOfPictureParameterSets;
        //for (i=0; i<numOfPictureParameterSets; i++) {
        //unsigned int (16) pictureParameterSetLength;
        //bit(8*pictureParameterSetLength) pictureParameterSetNALUnit;
        //}
        //}
        public byte ConfigurationVersion { get; set; } = 1;
        public byte AVCProfileIndication { get; set; }
        public byte ProfileCompatibility { get; set; }
        public byte AVCLevelIndication { get; set; }
        public int LengthSizeMinusOne { get; set; }
        public int NumOfSequenceParameterSets { get; set; }
        //public List<(ushort SequenceParameterSetLength,byte[] SequenceParameterSetNALUnit)> SPS { get; set; }
        public byte[] SPSBuffer { get; set; }
        public byte NumOfPictureParameterSets { get; set; } = 1;
        //public List<(ushort PictureParameterSetLength,byte[] PictureParameterSetNALUnit)> PPS { get; set; }
        public byte[] PPSBuffer { get; set; }

        public void ToBuffer(ref FMp4MessagePackWriter writer)
        {
            Start(ref writer);
            writer.WriteByte(ConfigurationVersion);
            writer.WriteByte(AVCProfileIndication);
            writer.WriteByte(ProfileCompatibility);
            writer.WriteByte(AVCLevelIndication);
            writer.WriteByte((byte)(0xfc | LengthSizeMinusOne));
            writer.WriteByte((byte)(0xE0 | NumOfSequenceParameterSets));
            writer.WriteUInt16((ushort)SPSBuffer.Length);
            writer.WriteArray(SPSBuffer);
            writer.WriteByte(NumOfPictureParameterSets);
            writer.WriteUInt16((ushort)PPSBuffer.Length);
            writer.WriteArray(PPSBuffer);
            End(ref writer);
        }
    }
}
