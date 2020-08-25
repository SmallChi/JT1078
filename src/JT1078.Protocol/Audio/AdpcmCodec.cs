using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol.Audio
{
    public class AdpcmCodec: IAudioCodec
    {
        static readonly int[] indexTable = {
            -1, -1, -1, -1, 2, 4, 6, 8,
            -1, -1, -1, -1, 2, 4, 6, 8
        };

        static readonly int[] stepsizeTable = {
            7, 8, 9, 10, 11, 12, 13, 14, 16, 17,
            19, 21, 23, 25, 28, 31, 34, 37, 41, 45,
            50, 55, 60, 66, 73, 80, 88, 97, 107, 118,
            130, 143, 157, 173, 190, 209, 230, 253, 279, 307,
            337, 371, 408, 449, 494, 544, 598, 658, 724, 796,
            876, 963, 1060, 1166, 1282, 1411, 1552, 1707, 1878, 2066,
            2272, 2499, 2749, 3024, 3327, 3660, 4026, 4428, 4871, 5358,
            5894, 6484, 7132, 7845, 8630, 9493, 10442, 11487, 12635, 13899,
            15289, 16818, 18500, 20350, 22385, 24623, 27086, 29794, 32767
        };

        //public static byte[] ToAdpcm(short[] indata, AdpcmState state)
        //{
        //    int val;            /* Current input sample value */
        //    int sign;           /* Current adpcm sign bit */
        //    int delta;          /* Current adpcm output value */
        //    int diff;           /* Difference between val and valprev */
        //    int step;           /* Stepsize */
        //    int valpred;        /* Predicted output value */
        //    int vpdiff;         /* Current change to valpred */
        //    int index;          /* Current step change index */
        //    int outputbuffer = 0;       /* place to keep previous 4-bit value */
        //    int bufferstep;     /* toggle between outputbuffer/output */

        //    List<byte> outp = new List<byte>();
        //    short[] inp = indata;
        //    var len = indata.Length;
        //    valpred = state.Valprev;
        //    index = state.Index;
        //    step = stepsizeTable[index];

        //    bufferstep = 1;

        //    int k = 0;
        //    for (int i = 0; len > 0; len--, i++)
        //    {
        //        val = inp[i];

        //        /* Step 1 - compute difference with previous value */
        //        diff = val - valpred;
        //        sign = (diff < 0) ? 8 : 0;
        //        if (sign != 0) diff = (-diff);

        //        /* Step 2 - Divide and clamp */
        //        /* Note:
        //        ** This code *approximately* computes:
        //        **    delta = diff*4/step;
        //        **    vpdiff = (delta+0.5)*step/4;
        //        ** but in shift step bits are dropped. The net result of this is
        //        ** that even if you have fast mul/div hardware you cannot put it to
        //        ** good use since the fixup would be too expensive.
        //        */
        //        delta = 0;
        //        vpdiff = (step >> 3);

        //        if (diff >= step)
        //        {
        //            delta = 4;
        //            diff -= step;
        //            vpdiff += step;
        //        }
        //        step >>= 1;
        //        if (diff >= step)
        //        {
        //            delta |= 2;
        //            diff -= step;
        //            vpdiff += step;
        //        }
        //        step >>= 1;
        //        if (diff >= step)
        //        {
        //            delta |= 1;
        //            vpdiff += step;
        //        }

        //        /* Step 3 - Update previous value */
        //        if (sign != 0)
        //            valpred -= vpdiff;
        //        else
        //            valpred += vpdiff;

        //        /* Step 4 - Clamp previous value to 16 bits */
        //        if (valpred > 32767)
        //            valpred = 32767;
        //        else if (valpred < -32768)
        //            valpred = -32768;

        //        /* Step 5 - Assemble value, update index and step values */
        //        delta |= sign;

        //        index += indexTable[delta];
        //        if (index < 0) index = 0;
        //        if (index > 88) index = 88;
        //        step = stepsizeTable[index];

        //        /* Step 6 - Output value */
        //        if (bufferstep != 0)
        //        {
        //            outputbuffer = (delta << 4) & 0xf0;
        //        }
        //        else
        //        {
        //            outp.Add((byte)((delta & 0x0f) | outputbuffer));
        //        }
        //        bufferstep = bufferstep == 0 ? 1 : 0;
        //    }

        //    /* Output last step, if needed */
        //    if (bufferstep == 0)
        //        outp.Add((byte)outputbuffer);

        //    state.Valprev = (short)valpred;
        //    state.Index = (byte)index;
        //    return outp.ToArray();
        //}

        /// <summary>
        /// 将adpcm转为pcm
        /// </summary>
        /// <see cref="https://github.com/ctuning/ctuning-programs/blob/master/program/cbench-telecom-adpcm-d/adpcm.c"/>
        /// <param name="audio"></param>
        /// <param name="audioAttachData"></param>
        /// <returns></returns>
        public byte[] ToPcm(byte[] audio, IAudioAttachData audioAttachData)
        {
            AdpcmState state = (AdpcmState)audioAttachData;
            // signed char *inp;		/* Input buffer pointer */
            // short *outp;		/* output buffer pointer */
            int sign;           /* Current adpcm sign bit */
            int delta;          /* Current adpcm output value */
            int step;           /* Stepsize */
            int valpred = state.Valprev;        /* Predicted value */
            int vpdiff;         /* Current change to valpred */
            byte index = state.Index;          /* Current step change index */
            int inputbuffer = 0;        /* place to keep next 4-bit value */
            bool bufferstep = false;     /* toggle between inputbuffer/input */

            step = stepsizeTable[index];

            var outdata = new List<byte>();
            var len = audio.Length * 2;
            for (int i = 0; len > 0; len--)
            {
                /* Step 1 - get the delta value */
                if (bufferstep)
                {
                    delta = inputbuffer & 0xf;
                }
                else
                {
                    inputbuffer = audio[i++];
                    delta = (inputbuffer >> 4) & 0xf;
                }
                bufferstep = !bufferstep;

                /* Step 2 - Find new index value (for later) */
                index += (byte)indexTable[delta];
                if (index < 0) index = 0;
                if (index > 88) index = 88;

                /* Step 3 - Separate sign and magnitude */
                sign = delta & 8;
                delta &= 7;

                /* Step 4 - Compute difference and new predicted value */
                /*
                ** Computes 'vpdiff = (delta+0.5)*step/4', but see comment
                ** in adpcm_coder.
                */
                vpdiff = step >> 3;
                if ((delta & 4) > 0) vpdiff += step;
                if ((delta & 2) > 0) vpdiff += step >> 1;
                if ((delta & 1) > 0) vpdiff += step >> 2;

                if (sign != 0)
                    valpred -= vpdiff;
                else
                    valpred += vpdiff;

                /* Step 5 - clamp output value */
                if (valpred > 32767)
                    valpred = 32767;
                else if (valpred < -32768)
                    valpred = -32768;

                /* Step 6 - Update step value */
                step = stepsizeTable[index];

                /* Step 7 - Output value */
                outdata.AddRange(BitConverter.GetBytes((short)valpred));
            }
            state.Valprev = (short)valpred;
            state.Index = index;
            return outdata.ToArray();
        }
    }
    public class AdpcmState : IAudioAttachData
    {
        /// <summary>
        /// 上一个采样数据，当index为0是该值应该为未压缩的原数据
        /// </summary>
        public short Valprev { get; set; }

        /// <summary>
        /// 保留数据（未使用）
        /// </summary>
        public byte Reserved { get; set; }

        /// <summary>
        /// 上一个block最后一个index，第一个block的index=0
        /// </summary>
        public byte Index { get; set; }
    }
    public static class AdpcmDecoderExtension
    {
        /// <summary>
        /// 添加wav头
        /// 仅用于测试pcm是否转成成功，因此没考虑性能，因为播放器可播——#
        /// </summary>
        /// <param name="input">pcm数据</param>
        /// <param name="frequency">采样率</param>
        /// <param name="bitDepth">位深</param>
        /// <returns></returns>
        public static byte[] ToWav(this byte[] input, uint frequency, byte bitDepth = 16)
        {
            byte[] output = new byte[input.Length + 44];
            Array.Copy(Encoding.ASCII.GetBytes("RIFF"), 0, output, 0, 4);
            WriteUint(4, (uint)output.Length - 8, output);
            Array.Copy(Encoding.ASCII.GetBytes("WAVE"), 0, output, 8, 4);
            Array.Copy(Encoding.ASCII.GetBytes("fmt "), 0, output, 12, 4);
            WriteUint(16, 16, output); //Header size
            output[20] = 1; //PCM
            output[22] = 1; //1 channel
            WriteUint(24, frequency, output); //Sample Rate
            WriteUint(28, (uint)(frequency * (bitDepth / 8)), output); //Bytes per second
            output[32] = (byte)(bitDepth >> 3); //Bytes per sample
            output[34] = bitDepth; //Bits per sample
            Array.Copy(Encoding.ASCII.GetBytes("data"), 0, output, 36, 4);
            WriteUint(40, (uint)output.Length, output); //Date size
            Array.Copy(input, 0, output, 44, input.Length);
            return output;
        }

        private static void WriteUint(uint offset, uint value, byte[] destination)
        {
            for (int i = 0; i < 4; i++)
            {
                destination[offset + i] = (byte)(value & 0xFF);
                value >>= 8;
            }
        }
    }
}
