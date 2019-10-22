using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.MessagePack
{
    /// <summary>
    /// Exp-Golomb指数哥伦布编码
    /// </summary>
    public ref struct ExpGolombReader
    {
        public ReadOnlySpan<byte> SrcBuffer { get; }
        public int BytesAvailable { get; private set; }
        public int Word { get; private set; }
        public int BitsAvailable { get; private set; }
        public ExpGolombReader(ReadOnlySpan<byte> srcBuffer)
        {
            SrcBuffer = srcBuffer;
            BytesAvailable = srcBuffer.Length;
            Word = 0;
            BitsAvailable = 0; 
        }
        public SPSInfo ReadSPS()
        {
            int sarScale = 1;
            uint frameCropLeftOffset=0;
            uint frameCropRightOffset = 0;
            uint frameCropTopOffset = 0;
            uint frameCropBottomOffset = 0;
            ReadByte();
            //profile_idc
            byte profileIdc = ReadByte();
            //constraint_set[0-4]_flag, u(5)
            uint profileCompat = ReadBits(5);
            //reserved_zero_3bits
            SkipBits(3);
            //level_idc u(8)
            byte levelIdc = ReadByte();
            //seq_parameter_set_id
            SkipUEG();
            if (profileIdc == 100 ||
                profileIdc == 110 ||
                profileIdc == 122 ||
                profileIdc == 244 ||
                profileIdc == 44 ||
                profileIdc == 83 ||
                profileIdc == 86 ||
                profileIdc == 118 ||
                profileIdc == 128)
            {
                uint chromaFormatIdc = ReadUEG();
                if (chromaFormatIdc == 3)
                {
                    SkipBits(1); // separate_colour_plane_flag
                }
                SkipUEG(); // bit_depth_luma_minus8
                SkipUEG(); // bit_depth_chroma_minus8
                SkipBits(1); // qpprime_y_zero_transform_bypass_flag
                if (ReadBoolean())
                { // seq_scaling_matrix_present_flag
                   int scalingListCount = (chromaFormatIdc != 3) ? 8 : 12;
                    for (int i = 0; i < scalingListCount; i++)
                    {
                        if (ReadBoolean())
                        {   
                            // seq_scaling_list_present_flag[ i ]
                            if (i < 6)
                            {
                                SkipScalingList(16);
                            }
                            else
                            {
                                SkipScalingList(64);
                            }
                        }
                    }
                }
            }
            // log2_max_frame_num_minus4
            SkipUEG();
            var picOrderCntType = ReadUEG();
            if (picOrderCntType == 0)
            {
                ReadUEG(); //log2_max_pic_order_cnt_lsb_minus4
            }
            else if (picOrderCntType == 1)
            {
                SkipBits(1); // delta_pic_order_always_zero_flag
                SkipEG(); // offset_for_non_ref_pic
                SkipEG(); // offset_for_top_to_bottom_field
                uint numRefFramesInPicOrderCntCycle = ReadUEG();
                for (int i = 0; i < numRefFramesInPicOrderCntCycle; i++)
                {
                    SkipEG(); // offset_for_ref_frame[ i ]
                }
            }
            SkipUEG(); // max_num_ref_frames
            SkipBits(1); // gaps_in_frame_num_value_allowed_flag
            uint picWidthInMbsMinus1 = ReadUEG();
            uint picHeightInMapUnitsMinus1 = ReadUEG();
            uint frameMbsOnlyFlag = ReadBits(1);
            if (frameMbsOnlyFlag == 0)
            {
               SkipBits(1); // mb_adaptive_frame_field_flag
            }
            this.SkipBits(1); // direct_8x8_inference_flag
            if (ReadBoolean())
            { 
               // frame_cropping_flag
               frameCropLeftOffset   = ReadUEG();
               frameCropRightOffset  = ReadUEG();
               frameCropTopOffset    = ReadUEG();
               frameCropBottomOffset = ReadUEG();
            }
            if (ReadBoolean())
            {
                // vui_parameters_present_flag
                if (ReadBoolean())
                {
                    // aspect_ratio_info_present_flag
                    byte[] sarRatio=null;
                    byte aspectRatioIdc = ReadByte();
                    switch (aspectRatioIdc)
                    {
                        case 1: sarRatio =new byte[2] { 1, 1 }; break;
                        case 2: sarRatio =new byte[2] { 12, 11}; break;
                        case 3: sarRatio =new byte[2] { 10, 11}; break;
                        case 4: sarRatio =new byte[2] { 16, 11}; break;
                        case 5: sarRatio =new byte[2] { 40, 33}; break;
                        case 6: sarRatio =new byte[2] { 24, 11}; break;
                        case 7: sarRatio =new byte[2] { 20, 11}; break;
                        case 8: sarRatio =new byte[2] { 32, 11 }; break;
                        case 9: sarRatio = new byte[2] {80, 33 }; break;
                        case 10: sarRatio = new byte[2]{18, 11 }; break;
                        case 11: sarRatio = new byte[2]{15, 11 }; break;
                        case 12: sarRatio = new byte[2]{64, 33 }; break;
                        case 13: sarRatio = new byte[2]{160, 99 }; break;
                        case 14: sarRatio = new byte[2]{4, 3 }; break;
                        case 15: sarRatio = new byte[2]{3, 2 }; break;
                        case 16: sarRatio = new byte[2]{ 2, 1 }; break;
                        case 255:
                        {
                            sarRatio = new byte[2] { (byte)(ReadByte() << 8 | ReadByte()), (byte)(ReadByte() << 8 | ReadByte()) };
                            break;
                        }
                    }
                    if (sarRatio != null)
                    {
                        sarScale = sarRatio[0] / sarRatio[1];
                    }
                }
            }
            int width= (int)((((picWidthInMbsMinus1 + 1) * 16) - frameCropLeftOffset * 2 - frameCropRightOffset * 2) * sarScale);
            int height = (int)(((2 - frameMbsOnlyFlag) * (picHeightInMapUnitsMinus1 + 1) * 16) - ((frameMbsOnlyFlag == 1U ? 2 : 4) * (frameCropTopOffset + frameCropBottomOffset)));
            return new SPSInfo { profileIdc= profileIdc, levelIdc= levelIdc, profileCompat= profileCompat, width= width, height= height };
        }
        public void LoadWord()
        {
            var position = SrcBuffer.Length - BytesAvailable;
            int tmpAvailableBytes = BytesAvailable - 4;
            int availableBytes = Math.Min(4, BytesAvailable);
            //if (availableBytes == 0)
            //{
            //    throw new OverflowException("no bytes available");
            //}
            ReadOnlySpan<byte> workingBytes=ReadOnlySpan<byte>.Empty;
            if (tmpAvailableBytes < 0)
            {
                var buffer = new byte[4];
                Array.Copy(SrcBuffer.Slice(position, BytesAvailable).ToArray(), buffer, BytesAvailable);
                workingBytes = buffer;
            }
            else
            {
                workingBytes = SrcBuffer.Slice(position, 4);
            }
            Word = BinaryPrimitives.ReadInt32BigEndian(workingBytes);
            // track the amount of this.data that has been processed
            BitsAvailable = availableBytes * 8;
            BytesAvailable -= availableBytes;
        }
        public void SkipBits(int count)
        {
            if (BitsAvailable > count)
            {
                Word <<= count;
                BitsAvailable -= count;
            }
            else
            {
                count -= BitsAvailable;
                int skipBytes = count >> 3;
                count -= (skipBytes >> 3);
                LoadWord();
                Word <<= count;
                BitsAvailable -= count;
            }
        }
        public uint ReadBits(int size)
        {
            var bits = Math.Min(BitsAvailable, size); // :uint
            var valu = (uint)Word >> (32 - bits); // :uint
            if (size > 32)
            {
                throw new OverflowException("Cannot read more than 32 bits at a time");
            }
            BitsAvailable -= bits;
            if (BitsAvailable > 0)
            {
                Word <<= bits;
            }
            else if (BytesAvailable > 0)
            {
                LoadWord();
            }
            bits = size - bits;
            if (bits > 0)
            {
                return ((valu << bits) | ReadBits(bits));
            }
            else
            {
                return valu;
            }
        }
        public int SkipLZ()
        {
            int leadingZeroCount; // :uint
            for (leadingZeroCount = 0; leadingZeroCount < this.BitsAvailable; ++leadingZeroCount)
            {
                if (0 != (Word & (0x80000000 >> leadingZeroCount)))
                {
                    // the first bit of working word is 1
                    Word <<= leadingZeroCount;
                    BitsAvailable -= leadingZeroCount;
                    return leadingZeroCount;
                }
            }
            // we exhausted word and still have not found a 1
            LoadWord();
            return (leadingZeroCount + SkipLZ());
        }
        public void SkipUEG()
        {
            SkipBits(1 + SkipLZ());
        }
        public void SkipEG()
        {
            SkipBits(1 + SkipLZ());
        }
        public uint ReadUEG()
        {
            var clz =SkipLZ();
            return ReadBits(clz + 1) - 1;
        }
        public int ReadEG()
        {
            var valu = (int)ReadUEG(); // :int
            if ((0x01 & valu)==1)
            {
                // the number is odd if the low order bit is set
                return (1 + valu) >> 1; // add 1 to make it even, and divide by 2
            }
            else
            {
                return -1 * (valu >> 1); // divide by two then make it negative
            }
        }
        public bool ReadBoolean()
        {
            return 1 == ReadBits(1);
        }
        public byte ReadByte()
        {
            return (byte)ReadBits(8);
        }
        public ushort ReadUShort()
        {
            return (ushort)ReadBits(16);
        }
        public uint ReadUInt()
        {
            return ReadBits(32);
        }

        /// <summary>
        ///Advance the ExpGolomb decoder past a scaling list.The scaling
        ///list is optionally transmitted as part of a sequence parameter
        ///set and is not relevant to transmuxing.
        ///@param count { number}
        ///the number of entries in this scaling list
        ///@see Recommendation ITU-T H.264, Section 7.3.2.1.1.1
        /// </summary>
        /// <param name="count"></param>
        public void SkipScalingList(int count)
        {
            int lastScale = 8,
                nextScale = 8,
                j,
                deltaScale;
            for (j = 0; j < count; j++)
            {
                if (nextScale != 0)
                {
                    deltaScale = ReadEG();
                    nextScale = (lastScale + deltaScale + 256) % 256;
                }
                lastScale = (nextScale == 0) ? lastScale : nextScale;
            }
        }
    }

    public class SPSInfo
    {
        public byte profileIdc { get; set; }
        public byte levelIdc { get; set; }
        public uint profileCompat { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
