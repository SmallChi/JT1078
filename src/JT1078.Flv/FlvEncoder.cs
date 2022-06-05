using JT1078.Flv.Enums;
using JT1078.Flv.MessagePack;
using JT1078.Flv.Metadata;
using JT1078.Protocol.Enums;
using JT1078.Protocol.H264;
using JT1078.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JT1078.Protocol;
using JT1078.Protocol.Audio;

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
    /// 0、<see cref="EncoderFlvHeader"/>
    /// 1、插入  PriviousTagSize =0  always equal 0
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
    public class FlvEncoder
    {
        readonly H264Decoder h264Decoder;
        readonly AudioCodecFactory audioCodecFactory;
        //public FlvEncoder(int sampleRate = 8000, int channels = 1, int sampleBit = 16, bool adts = false)
        /// <summary>
        /// 
        /// </summary>
        public FlvEncoder()
        {
            audioCodecFactory = new AudioCodecFactory();
            h264Decoder = new H264Decoder();
        }

        /// <summary>
        /// 编码flv头
        /// </summary>
        /// <param name="hasVideo">是否有视频</param>
        /// <param name="hasAudio">是否有音频</param>
        /// <returns></returns>
        public byte[] EncoderFlvHeader(bool hasVideo = true, bool hasAudio = false)
        {
            var flvHeader = new FlvHeader(hasVideo, hasAudio);
            return flvHeader.ToArray().ToArray();
        }

        /// <summary>
        /// 编码脚本Tag
        /// </summary>
        /// <param name="spsInfo">解析后的sps信息</param>
        /// <param name="hasAudio">是否有音频</param>
        /// <param name="frameRate">帧率 默认25d 即每秒25帧</param>
        /// <returns></returns>
        [Obsolete("use EncoderScriptTag(JT1078AVFrame avframe, bool hasAudio = false, double frameRate = 25d)")]
        public byte[] EncoderScriptTag(SPSInfo spsInfo, bool hasAudio = false, double frameRate = 25d)
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
                            new Amf3Metadata_Width(){
                             Value=spsInfo.width
                            },
                            new Amf3Metadata_Height(){
                             Value=spsInfo.height
                            },
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
                flvMessagePackWriter.WriteFlvTag(flvTags);
                flvMessagePackWriter.WriteUInt32((uint)(flvTags.DataSize + 11));
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码脚本Tag
        /// </summary>
        /// <param name="avframe">解析后的av信息</param>
        /// <param name="hasAudio">是否有音频</param>
        /// <param name="frameRate">帧率 默认25d 即每秒25帧</param>
        /// <returns></returns>

        public byte[] EncoderScriptTag(JT1078AVFrame avframe, bool hasAudio = false, double frameRate = 25d)
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
                            new Amf3Metadata_Width(){
                                Value=avframe.Width
                            },
                            new Amf3Metadata_Height(){
                                Value=avframe.Height
                            },
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
                flvMessagePackWriter.WriteFlvTag(flvTags);
                flvMessagePackWriter.WriteUInt32((uint)(flvTags.DataSize + 11));
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        /// <summary>
        ///  编码首帧视频，即videoTag[0]
        /// </summary>
        /// <param name="spsInfo">sps 解析后的数据</param>
        /// <param name="sps"></param>
        /// <param name="pps"></param>
        /// <param name="sei"></param>
        /// <returns></returns>
        [Obsolete("use EncoderFirstVideoTag(JT1078AVFrame avframe)")]
        public byte[] EncoderFirstVideoTag(SPSInfo spsInfo, H264NALU sps, H264NALU pps, H264NALU sei)
        {
            byte[] buffer = FlvArrayPool.Rent(4096);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
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
                flvMessagePackWriter.WriteFlvTag(flvTags);
                flvMessagePackWriter.WriteUInt32((uint)(flvTags.DataSize + 11));
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        /// <summary>
        ///  编码首帧视频，即videoTag[0]
        /// </summary>
        /// <param name="avframe"></param>
        /// <returns></returns>
        public byte[] EncoderFirstVideoTag(JT1078AVFrame avframe)
        {
            byte[] buffer = FlvArrayPool.Rent(4096);
            try
            {
                FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
                //flv body video tag
                //flv body tag header
                FlvTags flvTags = new FlvTags
                {
                    Type = TagType.Video,
                    Timestamp = (uint)avframe.SPS.Timestamp,
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
                    AVCProfileIndication = avframe.ProfileIdc,
                    ProfileCompatibility = avframe.ProfileCompat,
                    AVCLevelIndication = avframe.LevelIdc,
                    NumOfPictureParameterSets = 1,
                    PPSBuffer = avframe.PPS.RawData,
                    SPSBuffer = avframe.SPS.RawData
                };
                flvTags.VideoTagsData.VideoData.AVCDecoderConfiguration = aVCDecoderConfigurationRecord;
                flvMessagePackWriter.WriteFlvTag(flvTags);
                flvMessagePackWriter.WriteUInt32((uint)(flvTags.DataSize + 11));
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码首帧音频，即audioTag[0]
        /// </summary>
        /// <param name="timestamp"></param>
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
                flvMessagePackWriter.WriteFlvTag(flvTags);
                flvMessagePackWriter.WriteUInt32((uint)(flvTags.DataSize + 11));
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码视频
        /// </summary>
        /// <remarks><paramref name="package"/>必须是组包后的数据</remarks>
        /// <param name="package">1078包</param>
        /// <param name="needVideoHeader">是否需要首帧视频</param>
        /// <returns></returns>
        public byte[] EncoderVideoTag(JT1078Package package, bool needVideoHeader = false)
        {
            if (package.Label3.DataType == JT1078DataType.音频帧) return default;
            byte[] buffer = FlvArrayPool.Rent(package.Bodies.Length*2+4096);
            FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(buffer);
            var nalus = h264Decoder.ParseNALU(package);
            if (nalus != null && nalus.Count > 0)
            {
                var sei = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == NalUnitType.SEI);
                var sps = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == NalUnitType.SPS);
                var pps = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == NalUnitType.PPS);
                nalus.Remove(sps);
                nalus.Remove(pps);
                nalus.Remove(sei);
                if (needVideoHeader)
                {
                    //flv header
                    var flvHeader = EncoderFlvHeader(true, false);
                    flvMessagePackWriter.WriteArray(flvHeader);
                    // always 0
                    flvMessagePackWriter.WriteUInt32(0);
                    //解析sps
                    if (sps == null)
                    {
                        return null;
                    }
                    var rawData = h264Decoder.DiscardEmulationPreventionBytes(sps.RawData);
                    ExpGolombReader h264GolombReader = new ExpGolombReader(rawData);
                    SPSInfo spsInfo = h264GolombReader.ReadSPS();
                    //script tag
                    var scriptTag = EncoderScriptTag(spsInfo);
                    flvMessagePackWriter.WriteArray(scriptTag);
                    // first video tag
                    var firstVideoTag = EncoderFirstVideoTag(spsInfo, sps, pps, sei);
                    flvMessagePackWriter.WriteArray(firstVideoTag);
                }
                foreach (var naln in nalus)
                {
                    var otherVideoTag = EncoderOtherVideoTag(naln);
                    flvMessagePackWriter.WriteArray(otherVideoTag);
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
        [Obsolete("音频暂时去掉")]
        public byte[] EncoderAudioTag(JT1078Package package, bool needAacHeader = false)
        {
            if (package.Label3.DataType != JT1078DataType.音频帧) throw new Exception("Incorrect parameter, package must be audio frame");
            FlvMessagePackWriter flvMessagePackWriter = new FlvMessagePackWriter(new byte[65536]);
            if (needAacHeader)
            {
                flvMessagePackWriter.WriteArray(EncoderFirstAudioTag(package.Timestamp));
            }
            byte[] aacFrameData = audioCodecFactory.Encode(package.Label2.PT, package.Bodies);
            if (aacFrameData != null && aacFrameData.Any())//编码成功，此时为一帧aac音频数据
            {
                // Data Tag Frame
                flvMessagePackWriter.WriteArray(EncoderAacAudioTag((uint)package.Timestamp, aacFrameData));
            }
            return flvMessagePackWriter.FlushAndGetArray();
        }
        /// <summary>
        /// 编码非首帧视频
        /// </summary>
        /// <param name="nALU"></param>
        /// <returns></returns>
        public byte[] EncoderOtherVideoTag(H264NALU nALU)
        {
            byte[] buffer = FlvArrayPool.Rent(nALU.RawData.Length*2+4096);
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
                if (nALU.NALUHeader.NalUnitType == NalUnitType.IDR)
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
                flvMessagePackWriter.WriteUInt32((uint)(flvTags.DataSize + 11));
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }

        byte[] EncoderAacAudioTag(uint timestamp, byte[] aacFrameData)
        {
            byte[] buffer = FlvArrayPool.Rent(aacFrameData.Length);
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
                flvMessagePackWriter.WriteUInt32((uint)(flvTags.DataSize + 11));
                return flvMessagePackWriter.FlushAndGetArray();
            }
            finally
            {
                FlvArrayPool.Return(buffer);
            }
        }
    }
}
