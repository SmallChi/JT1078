using JT1078.Flv.Enums;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using JT1078.Protocol.Enums;
using JT1078.Protocol.H264;
using JT1078.Protocol.MessagePack;
using JT1078.Flv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JT1078.Protocol;

[assembly: InternalsVisibleTo("JT1078.Flv.Test")]
namespace JT1078.Flv
{
    /// <summary>
    /// Flv编码器
    /// 一个客户端对应一个实例
    /// <para>
    /// 当实例不适用时，尽量手动调用下<see cref="Dispose"/>
    /// </para>
    /// 
    /// 手动编码
    /// 1、<see cref="EncoderFlvHeader"/>
    /// 2、<see cref="EncoderScriptTag"/>
    /// 3、<see cref="EncoderFirstVideoTag"/>
    /// 4、<see cref="EncoderFirstAudioTag"/>
    /// 5、<see cref="EncoderVideoTag"/>第二个参数传false
    /// 6、<see cref="EncoderAudioTag"/>第二个参数传false
    /// 自动编码
    /// 1、<see cref="EncoderFlvHeader"/>
    /// 2、<see cref="EncoderScriptTag"/>
    /// 3、<see cref="EncoderVideoTag"/>第二个参数传true
    /// 4、<see cref="EncoderAudioTag"/>第二个参数传true
    /// </summary>
    public class FlvEncoder : IDisposable
    {
        uint previousTagSize;
        FlvHeader flvHeader = new FlvHeader(true, true);
        readonly FaacEncoder faacEncoder;
        readonly H264Decoder h264Decoder = new H264Decoder();

        public FlvEncoder(int sampleRate = 8000, int channels = 1, int sampleBit = 16, bool adts = false)
        {
            faacEncoder = new FaacEncoder(sampleRate, channels, sampleBit, adts);
        }

        /// <summary>
        /// 编码flv头
        /// <para>
        /// 注意：本方法已写入<see cref="previousTagSize"/>
        /// </para>
        /// </summary>
        /// <param name="hasVideo"></param>
        /// <param name="hasAudio"></param>
        /// <returns></returns>
        public byte[] EncoderFlvHeader(bool hasVideo = true, bool hasAudio = false)
        {
            previousTagSize = 0;
            flvHeader = new FlvHeader(hasVideo, hasAudio);
            return flvHeader.ToArray().ToArray();
        }

        /// <summary>
        /// 编码脚本Tag
        /// <para>
        /// 注意：本方法已写入<see cref="previousTagSize"/>
        /// </para>
        /// </summary>
        /// <param name="width">视频宽度</param>
        /// <param name="height">视频高度</param>
        /// <param name="hasAudio">是否含有音频，如果有，则写入音频配置，后来发现即便是有音频，这里给<c>false</c>也没关系</param>
        /// <param name="frameRate">帧率</param>
        /// <returns></returns>
        public byte[] EncoderScriptTag(bool hasAudio = false, double frameRate = 25d)
        {
            byte[] buffer = FlvArrayPool.Rent(1024);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body script tag
                //flv body tag header
                FlvTags flvTags = new FlvTags
                {
                    Type = TagType.ScriptData,
                    //flv body tag body
                    DataTagsData = new Amf3
                    {
                        Amf3Metadatas = new List<IAmf3Metadata>
                        {
                            new Amf3Metadata_Duration{Value = 0d},
                            new Amf3Metadata_VideoDataRate{Value = 0d},
                            new Amf3Metadata_VideoCodecId{Value = 7d},
                            new Amf3Metadata_FrameRate{Value = frameRate},
                            new Amf3Metadata_Width(),
                            new Amf3Metadata_Height(),
                        }
                    }
                };
                if (hasAudio)
                {
                    flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_AudioCodecId());
                    flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_AudioSampleRate());
                    flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_AudioSampleSize());
                    flvTags.DataTagsData.Amf3Metadatas.Add(new Amf3Metadata_AudioStereo());
                }
                flvMessagePackWriter.WriteUInt32(previousTagSize);
                flvMessagePackWriter.WriteFlvTag(flvTags);
                var data = flvMessagePackWriter.FlushAndGetArray();
                previousTagSize = (uint)(flvTags.DataSize + 11);
                return data;
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码首帧视频，即videoTag[0]
        /// <para>
        /// 注意：本方法已写入<see cref="previousTagSize"/>
        /// </para>
        /// </summary>
        /// <param name="sps"></param>
        /// <param name="pps"></param>
        /// <param name="sei"></param>
        /// <returns></returns>
        public byte[] EncoderFirstVideoTag(H264NALU sps, H264NALU pps, H264NALU sei)
        {
            byte[] buffer = FlvArrayPool.Rent(2048);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                var rawData = h264Decoder.DiscardEmulationPreventionBytes(sps.RawData);
                ExpGolombReader h264GolombReader = new ExpGolombReader(rawData);
                SPSInfo spsInfo = h264GolombReader.ReadSPS();
                //flv body video tag
                //flv body tag header
                FlvTags flvTags = new FlvTags
                {
                    Type = TagType.Video,
                    Timestamp = (uint)sps.Timestamp,
                    TimestampExt = 0,
                    StreamId = 0,
                    //flv body tag body
                    VideoTagsData = new VideoTags()
                };
                flvTags.VideoTagsData.FrameType = FrameType.KeyFrame;
                flvTags.VideoTagsData.VideoData = new AvcVideoPacke
                {
                    AvcPacketType = AvcPacketType.SequenceHeader,
                    CompositionTime = 0
                };
                AVCDecoderConfigurationRecord aVCDecoderConfigurationRecord = new AVCDecoderConfigurationRecord
                {
                    AVCProfileIndication = spsInfo.profileIdc,
                    ProfileCompatibility = (byte)spsInfo.profileCompat,
                    AVCLevelIndication = spsInfo.levelIdc,
                    NumOfPictureParameterSets = 1,
                    PPSBuffer = pps.RawData,
                    SPSBuffer = sps.RawData
                };
                flvTags.VideoTagsData.VideoData.AVCDecoderConfiguration = aVCDecoderConfigurationRecord;
                flvMessagePackWriter.WriteUInt32(previousTagSize);
                flvMessagePackWriter.WriteFlvTag(flvTags);
                var data = flvMessagePackWriter.FlushAndGetArray();
                previousTagSize = (uint)(flvTags.DataSize + 11);
                return data;
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码首帧音频，即audioTag[0]
        /// <para>
        /// 注意：本方法已写入<see cref="previousTagSize"/>
        /// </para>
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderFirstAudioTag(ulong timestamp)
        {
            byte[] buffer = FlvArrayPool.Rent(2048);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body audio tag
                //flv body tag header
                FlvTags flvTags = new FlvTags
                {
                    Type = TagType.Audio,
                    Timestamp = (uint)timestamp,
                    //flv body tag body
                    AudioTagsData = new AudioTags(AACPacketType.AudioSpecificConfig)
                };
                flvMessagePackWriter.WriteUInt32(previousTagSize);
                flvMessagePackWriter.WriteFlvTag(flvTags);
                var data = flvMessagePackWriter.FlushAndGetArray();
                previousTagSize = (uint)(flvTags.DataSize + 11);
                return data;
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码非首帧视频
        /// </summary>
        /// <param name="package"></param>
        /// <param name="needVideoHeader">是否需要首帧视频</param>
        /// <returns></returns>
        public byte[] EncoderVideoTag(JT1078Package package, bool needVideoHeader = false)
        {
            if (package.Label3.DataType == JT1078DataType.音频帧) return default;
            byte[] buffer = FlvArrayPool.Rent(65535);
            FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
            var nalus = h264Decoder.ParseNALU(package);
            if (nalus != null && nalus.Count > 0)
            {
                var sei = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == 6);
                var sps = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == 7);
                var pps = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == 8);
                nalus.Remove(sps);
                nalus.Remove(pps);
                nalus.Remove(sei);
                if (needVideoHeader)
                {
                    var firstVideoTag = EncoderFirstVideoTag(sps, pps, sei);
                    flvMessagePackWriter.WriteArray(firstVideoTag);
                }
                foreach (var naln in nalus)
                {
                    flvMessagePackWriter.WriteUInt32(previousTagSize);
                    var videoTag = ConversionNaluToVideoTag(naln);
                    flvMessagePackWriter.WriteArray(videoTag);
                }
            }
            return flvMessagePackWriter.FlushAndGetArray();
        }

        /// <summary>
        /// 编码非首帧音频
        /// </summary>
        /// <param name="package"></param>
        /// <param name="needAacHeader">是否需要首帧音频</param>
        /// <returns></returns>
        public byte[] EncoderAudioTag(JT1078Package package, bool needAacHeader = false)
        {
            if (package.Label3.DataType != JT1078DataType.音频帧) throw new Exception("Incorrect parameter, package must be audio frame");
            FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(new byte[65536]);
            if (needAacHeader)
            {
                flvMessagePackWriter.WriteArray(EncoderFirstAudioTag(package.Timestamp));
            }
            byte[] aacFrameData = null;
            switch (package.Label2.PT)
            {
                case Jt1078AudioType.ADPCM:
                    ReadOnlySpan<byte> adpcm = package.Bodies;
                    // 海思芯片编码的音频需要移除海思头，可能还有其他的海思头
                    if (adpcm.StartsWith(new byte[] { 0x00, 0x01, 0x52, 0x00 })) adpcm = adpcm.Slice(4);
                    aacFrameData = faacEncoder.Encode(new AdpcmCodec().ToPcm(adpcm.Slice(4).ToArray(), new State()
                    {
                        Valprev = (short)((adpcm[1] << 8) | adpcm[0]),
                        Index = adpcm[2],
                        Reserved = adpcm[3]
                    })); break;
                case Jt1078AudioType.G711A:
                    aacFrameData = faacEncoder.Encode(new G711ACodec().ToPcm(package.Bodies));
                    break;
                case Jt1078AudioType.AACLC:
                    aacFrameData = package.Bodies;
                    break;
            }
            if (aacFrameData != null && aacFrameData.Any())//编码成功，此时为一帧aac音频数据
            {
                // PreviousTagSize
                flvMessagePackWriter.WriteUInt32(previousTagSize);
                // Data Tag Frame
                flvMessagePackWriter.WriteArray(ConversionAacDataToAudioTag((uint)package.Timestamp, aacFrameData));
            }
            return flvMessagePackWriter.FlushAndGetArray();
        }

        byte[] ConversionNaluToVideoTag(H264NALU nALU)
        {
            byte[] buffer = FlvArrayPool.Rent(65535);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body video tag
                //flv body tag header
                FlvTags flvTags = new FlvTags
                {
                    Type = TagType.Video,
                    //pts
                    Timestamp = (uint)nALU.Timestamp,
                    TimestampExt = 0,
                    StreamId = 0,
                    //flv body tag body
                    VideoTagsData = new VideoTags()
                };
                flvTags.VideoTagsData.VideoData = new AvcVideoPacke
                {
                    AvcPacketType = AvcPacketType.Raw
                };
                //1: keyframe (for AVC, a seekable frame) —— 即H.264的IDR帧；
                //2: inter frame(for AVC, a non - seekable frame) —— H.264的普通I帧；
                //ref:https://www.cnblogs.com/chyingp/p/flv-getting-started.html
                if (nALU.NALUHeader.NalUnitType == 5)
                {
                    flvTags.VideoTagsData.FrameType = FrameType.KeyFrame;
                }
                else
                {
                    flvTags.VideoTagsData.FrameType = FrameType.InterFrame;
                }
                flvTags.VideoTagsData.VideoData.CompositionTime = nALU.LastFrameInterval;
                flvTags.VideoTagsData.VideoData.MultiData = new List<byte[]>();
                flvTags.VideoTagsData.VideoData.MultiData.Add(nALU.RawData);
                //忽略sei
                //if (sei != null && sei.RawData != null && sei.RawData.Length > 0)
                //{
                //    flvTags.VideoTagsData.VideoData.MultiData.Add(sei.RawData);
                //}
                flvMessagePackWriter.WriteFlvTag(flvTags);
                var data = flvMessagePackWriter.FlushAndGetArray();
                previousTagSize = (uint)(flvTags.DataSize + 11);
                return data;
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        byte[] ConversionAacDataToAudioTag(uint timestamp, byte[] aacFrameData)
        {
            byte[] buffer = FlvArrayPool.Rent(65535);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body audio tag
                //flv body tag header
                FlvTags flvTags = new FlvTags
                {
                    Type = TagType.Audio,
                    Timestamp = timestamp,
                    TimestampExt = 0,
                    StreamId = 0,
                    //flv body tag body
                    AudioTagsData = new AudioTags(AACPacketType.AudioFrame, aacFrameData)
                };
                flvMessagePackWriter.WriteFlvTag(flvTags);
                previousTagSize = (uint)(flvTags.DataSize + 11);
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        public void Dispose()
        {
            faacEncoder.Dispose();
        }
    }
}
