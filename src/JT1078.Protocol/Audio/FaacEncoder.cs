using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;

namespace JT1078.Protocol.Audio
{
    public interface IFaacEncoder:IDisposable
    {
        byte[] Encode(byte[] bytes);
    }
    public class FaacEncoder_x86 : IFaacEncoder
    {
        private IntPtr faacEncHandle = IntPtr.Zero;
        private readonly int inputSamples;
        private readonly int maxOutput;
        public readonly int frameSize;
        private List<byte> frameCache = new List<byte>();
        public FaacEncoder_x86(int sampleRate, int channels, int sampleBit, bool adts = false)
        {
            var inputSampleBytes = new byte[4];
            var maxOutputBytes = new byte[4];
            faacEncHandle = FaacEncOpen(sampleRate, channels, inputSampleBytes, maxOutputBytes);
            inputSamples = BitConverter.ToInt32(inputSampleBytes, 0);
            maxOutput = BitConverter.ToInt32(maxOutputBytes, 0);
            frameSize = inputSamples * channels * sampleBit / 8;

            var ptr = FaacEncGetCurrentConfiguration(faacEncHandle);
            var configuration = InteropExtensions.IntPtrToStruct<FaacEncConfiguration>(ptr);
            configuration.inputFormat = 1;
            configuration.outputFormat = adts ? 1 : 0;
            configuration.useTns = 0;
            configuration.useLfe = 0;
            configuration.aacObjectType = 2;
            configuration.shortctl = 0;
            configuration.quantqual = 100;
            configuration.bandWidth = 0;
            configuration.bitRate = 0;
            InteropExtensions.IntPtrSetValue(ptr, configuration);

            if (FaacEncSetConfiguration(faacEncHandle, ptr) < 0) throw new Exception("set faac configuration failed!");
        }

        public byte[] Encode(byte[] bytes)
        {
            frameCache.AddRange(bytes);
            if (frameCache.Count() < frameSize)//faac必须达到一帧数据后才能正常编码
                return new byte[0];
            var outputBytes = new byte[maxOutput];
            var len = FaacEncEncode(faacEncHandle, frameCache.Take(frameSize).ToArray(), inputSamples, outputBytes, maxOutput);
            frameCache = frameCache.Skip(frameSize).ToList();
            if (len <= 0)
                return new byte[0];
            return outputBytes.Take(len).ToArray();
        }

        public void Dispose()
        {
            frameCache = null;
            if (faacEncHandle != IntPtr.Zero)
            {
                FaacEncClose(faacEncHandle);
                faacEncHandle = IntPtr.Zero;
            }
        }

        const string DLLFile = @"/nativelibs/x86/libfaac.dll";

        [DllImport(DLLFile, EntryPoint = "faacEncGetVersion", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncGetVersion(char **faac_id_string, char **faac_copyright_string);
        private extern static int FaacEncGetVersion(ref IntPtr faac_id_string, ref IntPtr faac_copyright_string);

        [DllImport(DLLFile, EntryPoint = "faacEncGetCurrentConfiguration", CallingConvention = CallingConvention.StdCall)]
        //faacEncConfigurationPtr FAACAPI faacEncGetCurrentConfiguration(faacEncHandle hEncoder);
        private extern static IntPtr FaacEncGetCurrentConfiguration(IntPtr hEncoder);


        [DllImport(DLLFile, EntryPoint = "faacEncSetConfiguration", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncSetConfiguration(faacEncHandle hEncoder,faacEncConfigurationPtr config);
        private extern static int FaacEncSetConfiguration(IntPtr hEncoder, IntPtr config);

        [DllImport(DLLFile, EntryPoint = "faacEncOpen", CallingConvention = CallingConvention.StdCall)]
        //faacEncHandle FAACAPI faacEncOpen(unsigned long sampleRate, unsigned int numChannels, unsigned long *inputSamples, unsigned long *maxOutputBytes);
        private extern static IntPtr FaacEncOpen(int sampleRate, int numChannels, byte[] inputSamples, byte[] maxOutputBytes);


        [DllImport(DLLFile, EntryPoint = "faacEncGetDecoderSpecificInfo", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncGetDecoderSpecificInfo(faacEncHandle hEncoder, unsigned char **ppBuffer,unsigned long *pSizeOfDecoderSpecificInfo);
        private extern static IntPtr FaacEncGetDecoderSpecificInfo(IntPtr hEncoder, ref IntPtr ppBuffer, ref int pSizeOfDecoderSpecificInfo);


        [DllImport(DLLFile, EntryPoint = "faacEncEncode", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncEncode(faacEncHandle hEncoder, int32_t * inputBuffer, unsigned int samplesInput, unsigned char *outputBuffer, unsigned int bufferSize);
        private extern static int FaacEncEncode(IntPtr hEncoder, IntPtr inputBuffer, int samplesInput, IntPtr outputBuffer, int bufferSize);
        [DllImport(DLLFile, EntryPoint = "faacEncEncode", CallingConvention = CallingConvention.StdCall)]
        private extern static int FaacEncEncode(IntPtr hEncoder, byte[] inputBuffer, int samplesInput, byte[] outputBuffer, int bufferSize);

        [DllImport(DLLFile, EntryPoint = "faacEncClose", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncClose(faacEncHandle hEncoder);
        private extern static IntPtr FaacEncClose(IntPtr hEncoder);

        #region 配置结构
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct FaacEncConfiguration
        {
            /* config version */

            public int version;

            /* library version */
            public IntPtr name;

            /* copyright string */
            public IntPtr copyright;

            /* MPEG version, 2 or 4 */
            public uint mpegVersion;

            /* AAC object type
             *  #define MAIN 1
                #define LOW  2
                #define SSR  3
                #define LTP  4
             * */

            public uint aacObjectType;

            /* Allow mid/side coding */
            public uint allowMidside;

            /* Use one of the channels as LFE channel */
            public uint useLfe;

            /* Use Temporal Noise Shaping */
            public uint useTns;

            /* bitrate / channel of AAC file */
            public uint bitRate;

            /* AAC file frequency bandwidth */
            public uint bandWidth;

            /* Quantizer quality */
            public uint quantqual;

            /* Bitstream output format (0 = Raw; 1 = ADTS) */
            public int outputFormat;

            /* psychoacoustic model list */
            public IntPtr psymodellist;

            /* selected index in psymodellist */
            public int psymodelidx;

            /*
                PCM Sample Input Format
                0	FAAC_INPUT_NULL			invalid, signifies a misconfigured config
                1	FAAC_INPUT_16BIT		native endian 16bit
                2	FAAC_INPUT_24BIT		native endian 24bit in 24 bits		(not implemented)
                3	FAAC_INPUT_32BIT		native endian 24bit in 32 bits		(DEFAULT)
                4	FAAC_INPUT_FLOAT		32bit floating point
            */
            public int inputFormat;

            /* block type enforcing (SHORTCTL_NORMAL/SHORTCTL_NOSHORT/SHORTCTL_NOLONG) */
            // #define FAAC_INPUT_NULL    0
            //#define FAAC_INPUT_16BIT   1
            //#define FAAC_INPUT_24BIT   2
            //#define FAAC_INPUT_32BIT   3
            //#define FAAC_INPUT_FLOAT   4

            //#define SHORTCTL_NORMAL    0
            //#define SHORTCTL_NOSHORT   1
            //#define SHORTCTL_NOLONG    2
            public int shortctl;

            /*
                Channel Remapping

                Default			0, 1, 2, 3 ... 63  (64 is MAX_CHANNELS in coder.h)

                WAVE 4.0		2, 0, 1, 3
                WAVE 5.0		2, 0, 1, 3, 4
                WAVE 5.1		2, 0, 1, 4, 5, 3
                AIFF 5.1		2, 0, 3, 1, 4, 5 
            */
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I4, SizeConst = 64)]
            public int[] channel_map;

        }
        #endregion
    }
    public class FaacEncoder_x64 : IFaacEncoder
    {
        private IntPtr faacEncHandle = IntPtr.Zero;
        private readonly int inputSamples;
        private readonly int maxOutput;
        public readonly int frameSize;
        private List<byte> frameCache = new List<byte>();
        public FaacEncoder_x64(int sampleRate, int channels, int sampleBit, bool adts = false)
        {
            var inputSampleBytes = new byte[4];
            var maxOutputBytes = new byte[4];
            faacEncHandle = FaacEncOpen(sampleRate, channels, inputSampleBytes, maxOutputBytes);
            inputSamples = BitConverter.ToInt32(inputSampleBytes, 0);
            maxOutput = BitConverter.ToInt32(maxOutputBytes, 0);
            frameSize = inputSamples * channels * sampleBit / 8;

            var ptr = FaacEncGetCurrentConfiguration(faacEncHandle);
            var configuration = InteropExtensions.IntPtrToStruct<FaacEncConfiguration>(ptr);
            configuration.inputFormat = 1;
            configuration.outputFormat = adts ? 1 : 0;
            configuration.useTns = 0;
            configuration.useLfe = 0;
            configuration.aacObjectType = 2;
            configuration.shortctl = 0;
            configuration.quantqual = 100;
            configuration.bandWidth = 0;
            configuration.bitRate = 0;
            InteropExtensions.IntPtrSetValue(ptr, configuration);

            if (FaacEncSetConfiguration(faacEncHandle, ptr) < 0) throw new Exception("set faac configuration failed!");
        }

        public byte[] Encode(byte[] bytes)
        {
            frameCache.AddRange(bytes);
            if (frameCache.Count() < frameSize)//faac必须达到一帧数据后才能正常编码
                return new byte[0];
            var outputBytes = new byte[maxOutput];
            var len = FaacEncEncode(faacEncHandle, frameCache.Take(frameSize).ToArray(), inputSamples, outputBytes, maxOutput);
            frameCache = frameCache.Skip(frameSize).ToList();
            if (len <= 0)
                return new byte[0];
            return outputBytes.Take(len).ToArray();
        }

        public void Dispose()
        {
            frameCache = null;
            if (faacEncHandle != IntPtr.Zero)
            {
                FaacEncClose(faacEncHandle);
                faacEncHandle = IntPtr.Zero;
            }
        }

        const string DLLFile = @"/nativelibs/x64/libfaac.dll";

        [DllImport(DLLFile, EntryPoint = "faacEncGetVersion", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncGetVersion(char **faac_id_string, char **faac_copyright_string);
        private extern static int FaacEncGetVersion(ref IntPtr faac_id_string, ref IntPtr faac_copyright_string);

        [DllImport(DLLFile, EntryPoint = "faacEncGetCurrentConfiguration", CallingConvention = CallingConvention.StdCall)]
        //faacEncConfigurationPtr FAACAPI faacEncGetCurrentConfiguration(faacEncHandle hEncoder);
        private extern static IntPtr FaacEncGetCurrentConfiguration(IntPtr hEncoder);

        [DllImport(DLLFile, EntryPoint = "faacEncSetConfiguration", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncSetConfiguration(faacEncHandle hEncoder,faacEncConfigurationPtr config);
        private extern static int FaacEncSetConfiguration(IntPtr hEncoder, IntPtr config);

        [DllImport(DLLFile, EntryPoint = "faacEncOpen", CallingConvention = CallingConvention.StdCall)]
        //faacEncHandle FAACAPI faacEncOpen(unsigned long sampleRate, unsigned int numChannels, unsigned long *inputSamples, unsigned long *maxOutputBytes);
        private extern static IntPtr FaacEncOpen(int sampleRate, int numChannels, byte[] inputSamples, byte[] maxOutputBytes);


        [DllImport(DLLFile, EntryPoint = "faacEncGetDecoderSpecificInfo", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncGetDecoderSpecificInfo(faacEncHandle hEncoder, unsigned char **ppBuffer,unsigned long *pSizeOfDecoderSpecificInfo);
        private extern static IntPtr FaacEncGetDecoderSpecificInfo(IntPtr hEncoder, ref IntPtr ppBuffer, ref int pSizeOfDecoderSpecificInfo);


        [DllImport(DLLFile, EntryPoint = "faacEncEncode", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncEncode(faacEncHandle hEncoder, int32_t * inputBuffer, unsigned int samplesInput, unsigned char *outputBuffer, unsigned int bufferSize);
        private extern static int FaacEncEncode(IntPtr hEncoder, IntPtr inputBuffer, int samplesInput, IntPtr outputBuffer, int bufferSize);
        [DllImport(DLLFile, EntryPoint = "faacEncEncode", CallingConvention = CallingConvention.StdCall)]
        private extern static int FaacEncEncode(IntPtr hEncoder, byte[] inputBuffer, int samplesInput, byte[] outputBuffer, int bufferSize);

        [DllImport(DLLFile, EntryPoint = "faacEncClose", CallingConvention = CallingConvention.StdCall)]
        //int FAACAPI faacEncClose(faacEncHandle hEncoder);
        private extern static IntPtr FaacEncClose(IntPtr hEncoder);

        #region 配置结构
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct FaacEncConfiguration
        {
            /* config version */

            public int version;

            /* library version */
            public IntPtr name;

            /* copyright string */
            public IntPtr copyright;

            /* MPEG version, 2 or 4 */
            public uint mpegVersion;

            /* AAC object type
             *  #define MAIN 1
                #define LOW  2
                #define SSR  3
                #define LTP  4
             * */

            public uint aacObjectType;

            /* Allow mid/side coding */
            public uint allowMidside;

            /* Use one of the channels as LFE channel */
            public uint useLfe;

            /* Use Temporal Noise Shaping */
            public uint useTns;

            /* bitrate / channel of AAC file */
            public uint bitRate;

            /* AAC file frequency bandwidth */
            public uint bandWidth;

            /* Quantizer quality */
            public uint quantqual;

            /* Bitstream output format (0 = Raw; 1 = ADTS) */
            public int outputFormat;

            /* psychoacoustic model list */
            public IntPtr psymodellist;

            /* selected index in psymodellist */
            public int psymodelidx;

            /*
                PCM Sample Input Format
                0	FAAC_INPUT_NULL			invalid, signifies a misconfigured config
                1	FAAC_INPUT_16BIT		native endian 16bit
                2	FAAC_INPUT_24BIT		native endian 24bit in 24 bits		(not implemented)
                3	FAAC_INPUT_32BIT		native endian 24bit in 32 bits		(DEFAULT)
                4	FAAC_INPUT_FLOAT		32bit floating point
            */
            public int inputFormat;

            /* block type enforcing (SHORTCTL_NORMAL/SHORTCTL_NOSHORT/SHORTCTL_NOLONG) */
            // #define FAAC_INPUT_NULL    0
            //#define FAAC_INPUT_16BIT   1
            //#define FAAC_INPUT_24BIT   2
            //#define FAAC_INPUT_32BIT   3
            //#define FAAC_INPUT_FLOAT   4

            //#define SHORTCTL_NORMAL    0
            //#define SHORTCTL_NOSHORT   1
            //#define SHORTCTL_NOLONG    2
            public int shortctl;

            /*
                Channel Remapping

                Default			0, 1, 2, 3 ... 63  (64 is MAX_CHANNELS in coder.h)

                WAVE 4.0		2, 0, 1, 3
                WAVE 5.0		2, 0, 1, 3, 4
                WAVE 5.1		2, 0, 1, 4, 5, 3
                AIFF 5.1		2, 0, 3, 1, 4, 5 
            */
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I4, SizeConst = 64)]
            public int[] channel_map;

        }
        #endregion
    }
}
