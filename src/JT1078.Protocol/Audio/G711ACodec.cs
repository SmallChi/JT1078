using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.Audio
{
    public class G711ACodec: IAudioCodec
    {
        private readonly int SIGN_BIT = 0x80;
        private readonly int QUANT_MASK = 0xf;
        private readonly int SEG_SHIFT = 4;
        private readonly int SEG_MASK = 0x70;
        //private readonly short[] seg_end = { 0xFF, 0x1FF, 0x3FF, 0x7FF, 0xFFF, 0x1FFF, 0x3FFF, 0x7FFF };
        short AlawToLinear(byte value)
        {
            short t;
            short seg;
            value ^= 0x55;
            t = (short)((value & QUANT_MASK) << 4);
            seg = (short)((value & SEG_MASK) >> SEG_SHIFT);
            switch (seg)
            {
                case 0:
                    t += 8;
                    break;
                case 1:
                    t += 0x108;
                    break;
                default:
                    t += 0x108;
                    t <<= seg - 1;
                    break;
            }
            return (value & SIGN_BIT) != 0 ? t : (short)-t;
        }

        public byte[] ToPcm(byte[] audio, IAudioAttachData audioAttachData)
        {
            byte[] pcmdata = new byte[audio.Length * 2];
            for (int i = 0, offset = 0; i < audio.Length; i++)
            {
                short value = AlawToLinear(audio[i]);
                pcmdata[offset++] = (byte)(value & 0xff);
                pcmdata[offset++] = (byte)((value >> 8) & 0xff);
            }
            return pcmdata;
        }

        //static short Search(short val, short[] table, short size)
        //{
        //    for (short i = 0; i < size; i++)
        //    {
        //        if (val <= table[i])
        //        {
        //            return i;
        //        }
        //    }
        //    return size;
        //}

        //byte LinearToAlaw(short pcm_val)
        //{
        //    short mask;
        //    short seg;
        //    char aval;
        //    if (pcm_val >= 0)
        //    {
        //        mask = 0xD5;
        //    }
        //    else
        //    {
        //        mask = 0x55;
        //        pcm_val = (short)(-pcm_val - 1);
        //        if (pcm_val < 0)
        //        {
        //            pcm_val = 32767;
        //        }
        //    }

        //    //Convert the scaled magnitude to segment number.
        //    seg = Search(pcm_val, seg_end, 8);

        //    //Combine the sign, segment, and quantization bits.
        //    if (seg >= 8)
        //    {
        //        //out of range, return maximum value.
        //        return (byte)(0x7F ^ mask);
        //    }
        //    else
        //    {
        //        aval = (char)(seg << SEG_SHIFT);
        //        if (seg < 2) aval |= (char)((pcm_val >> 4) & QUANT_MASK);
        //        else aval |= (char)((pcm_val >> (seg + 3)) & QUANT_MASK);
        //        return (byte)(aval ^ mask);
        //    }
        //}

        ///// <summary>
        ///// 转至G711
        ///// </summary>
        ///// <param name="pcmdata"></param>
        ///// <returns></returns>
        //public byte[] ToG711(byte[] pcmdata)
        //{
        //    byte[] g711data = new byte[pcmdata.Length / 2];
        //    for (int i = 0, k = 0; i < pcmdata.Length; i += 2, k++)
        //    {
        //        short v = (short)((pcmdata[i + 1] << 8) | (pcmdata[i]));
        //        g711data[k] = LinearToAlaw(v);
        //    }
        //    return g711data;
        //}
    }
}
