using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.Audio
{
    public class G711UCodec: IAudioCodec
    {
        /* 16384 entries per table (16 bit) */
        readonly byte[] linearToUlawTable = new byte[65536];
        /* 16384 entries per table (8 bit) */
        readonly short[] ulawToLinearTable = new short[256];
        readonly int SIGN_BIT = 0x80;
        readonly int QUANT_MASK = 0x0f;
        readonly int SEG_SHIFT = 0x04;
        readonly int SEG_MASK = 0x70;
        readonly int BIAS = 0x84;
        readonly int CLIP = 8159;
        readonly short[] seg_uend = { 0x3F, 0x7F, 0xFF, 0x1FF, 0x3FF, 0x7FF, 0xFFF, 0x1FFF };

        public G711UCodec()
        {
            // 初始化ulaw表
            for (int i = 0; i < 256; i++) ulawToLinearTable[i] = Ulaw2linear((byte)i);
            // 初始化ulaw2linear表
            for (int i = 0; i < 65535; i++) linearToUlawTable[i] = Linear2ulaw((short)i);
        }

        short Ulaw2linear(byte ulawValue)
        {
            ulawValue = (byte)(~ulawValue);
            short t = (short)(((ulawValue & QUANT_MASK) << 3) + BIAS);
            t <<= (ulawValue & SEG_MASK) >> SEG_SHIFT;

            return ((ulawValue & SIGN_BIT) > 0 ? (short)(BIAS - t) : (short)(t - BIAS));
        }

        byte Linear2ulaw(short pcmValue)
        {
            short mask;
            short seg;
            byte uval;

            pcmValue = (short)(pcmValue >> 2);
            if (pcmValue < 0)
            {
                pcmValue = (short)(-pcmValue);
                mask = 0x7f;
            }
            else
            {
                mask = 0xff;
            }

            if (pcmValue > CLIP) pcmValue = (short)CLIP;
            pcmValue += (short)(BIAS >> 2);

            seg = Search(pcmValue, seg_uend, 8);

            if (seg >= 8)
            {
                return (byte)(0x7f ^ mask);
            }
            else
            {
                uval = (byte)((seg << 4) | ((pcmValue >> (seg + 1)) & 0xF));
                return (byte)(uval ^ mask);
            }
        }

        short Search(short val, short[] table, short size)
        {
            for (short i = 0; i < size; i++)
            {
                if (val <= table[i]) return i;
            }
            return size;
        }

        byte[] UlawToPcm16(byte[] samples)
        {
            var pcmSamples = new byte[samples.Length * 2];
            for (int i = 0, k = 0; i < samples.Length; i++)
            {
                short s = ulawToLinearTable[samples[i] & 0xff];
                pcmSamples[k++] = (byte)(s & 0xff);
                pcmSamples[k++] = (byte)((s >> 8) & 0xff);
            }
            return pcmSamples;
        }

        //private byte[] Pcm16ToUlaw(byte[] pcmSamples)
        //{
        //    short[] dst = new short[pcmSamples.Length / 2];
        //    byte[] ulawSamples = new byte[pcmSamples.Length / 2];
        //    for (int i = 0, k = 0; i < pcmSamples.Length;)
        //    {
        //        dst[k++] = (short)((pcmSamples[i++] & 0xff) | ((pcmSamples[i++] & 0xff) << 8));
        //    }
        //    for (int i = 0, k = 0; i < dst.Length; i++)
        //    {
        //        ulawSamples[k++] = Linear2ulaw(dst[i]);
        //    }
        //    return ulawSamples;
        //}

        //public byte[] ToG711(byte[] data) => Pcm16ToUlaw(data);

        public byte[] ToPcm(byte[] audio, IAudioAttachData audioAttachData)
        {
            return UlawToPcm16(audio);
        }
    }
}
