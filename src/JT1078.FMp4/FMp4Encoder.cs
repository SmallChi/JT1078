using JT1078.FMp4.Enums;
using JT1078.FMp4.MessagePack;
using JT1078.FMp4.Samples;
using JT1078.Protocol;
using JT1078.Protocol.Enums;
using JT1078.Protocol.H264;
using JT1078.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JT1078.FMp4
{
    /// <summary>
    /// FMp4编码
    /// fmp4
    /// stream data 
    /// ftyp
    /// moov
    /// styp 1
    /// sidx 1
    /// moof 1
    /// mdat 1
    /// ...
    /// styp n
    /// sidx n
    /// moof n
    /// mdat n
    /// ref: https://www.w3.org/TR/mse-byte-stream-format-isobmff/#movie-fragment-relative-addressing
    /// </summary>
    public class FMp4Encoder
    {
        Dictionary<string, TrackInfo> TrackInfos;

        const uint TfhdFlags = FMp4Constants.TFHD_FLAG_DEFAULT_BASE_IS_MOOF |
                               FMp4Constants.TFHD_FLAG_DEFAULT_SIZE|
                               FMp4Constants.TFHD_FLAG_SAMPLE_DESCRIPTION_INDEX|
                               FMp4Constants.TFHD_FLAG_DEFAULT_FLAGS;

        const uint TrunFlags = FMp4Constants.TRUN_FLAG_DATA_OFFSET_PRESENT|
                               FMp4Constants.TRUN_FLAG_FIRST_SAMPLE_FLAGS_PRESENT|
                               FMp4Constants.TRUN_FLAG_SAMPLE_DURATION_PRESENT|
                               FMp4Constants.TRUN_FLAG_SAMPLE_SIZE_PRESENT;

        const uint SampleDescriptionIndex = 1;

        const uint TrackID = 1;

        H264Decoder h264Decoder = new H264Decoder();

        /// <summary>
        /// 
        /// </summary>
        public FMp4Encoder()
        {
            TrackInfos = new Dictionary<string, TrackInfo>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 编码ftyp盒子
        /// </summary>
        /// <returns></returns>
        public byte[] FtypBox()
        {
            byte[] buffer = FMp4ArrayPool.Rent(1024);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                //ftyp
                FileTypeBox fileTypeBox = new FileTypeBox();
                fileTypeBox.MajorBrand = "msdh";
                fileTypeBox.MinorVersion = "\0\0\0\0";
                fileTypeBox.CompatibleBrands.Add("isom");
                fileTypeBox.CompatibleBrands.Add("mp42");
                fileTypeBox.CompatibleBrands.Add("msdh");
                fileTypeBox.CompatibleBrands.Add("msix");
                // default‐base is‐moof flag
                fileTypeBox.CompatibleBrands.Add("iso5");
                // styp
                fileTypeBox.CompatibleBrands.Add("iso6");
                fileTypeBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码moov盒子
        /// </summary>
        /// <returns></returns>
        public byte[] MoovBox(in H264NALU sps, in H264NALU pps)
        {
            byte[] buffer = FMp4ArrayPool.Rent(sps.RawData.Length + pps.RawData.Length + 1024);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                ExpGolombReader h264GolombReader = new ExpGolombReader(sps.RawData);
                var spsInfo = h264GolombReader.ReadSPS();
                //moov
                MovieBox movieBox = new MovieBox();
                movieBox.MovieHeaderBox = new MovieHeaderBox(0, 2);
                movieBox.MovieHeaderBox.CreationTime = 0;
                movieBox.MovieHeaderBox.ModificationTime = 0;
                movieBox.MovieHeaderBox.Duration = 0;
                movieBox.MovieHeaderBox.Timescale = 1000;
                movieBox.MovieHeaderBox.NextTrackID = 99;
                movieBox.TrackBox = new TrackBox();
                movieBox.TrackBox.TrackHeaderBox = new TrackHeaderBox(0, 3);
                movieBox.TrackBox.TrackHeaderBox.CreationTime = 0;
                movieBox.TrackBox.TrackHeaderBox.ModificationTime = 0;
                movieBox.TrackBox.TrackHeaderBox.TrackID = TrackID;
                movieBox.TrackBox.TrackHeaderBox.Duration = 0;
                movieBox.TrackBox.TrackHeaderBox.TrackIsAudio = false;
                movieBox.TrackBox.TrackHeaderBox.Width = (uint)spsInfo.width;
                movieBox.TrackBox.TrackHeaderBox.Height = (uint)spsInfo.height;
                movieBox.TrackBox.MediaBox = new MediaBox();
                movieBox.TrackBox.MediaBox.MediaHeaderBox = new MediaHeaderBox();
                movieBox.TrackBox.MediaBox.MediaHeaderBox.CreationTime = 0;
                movieBox.TrackBox.MediaBox.MediaHeaderBox.ModificationTime = 0;
                movieBox.TrackBox.MediaBox.MediaHeaderBox.Timescale = 1000;
                movieBox.TrackBox.MediaBox.MediaHeaderBox.Duration = 0;
                movieBox.TrackBox.MediaBox.HandlerBox = new HandlerBox();
                movieBox.TrackBox.MediaBox.HandlerBox.HandlerType = HandlerType.vide;
                movieBox.TrackBox.MediaBox.HandlerBox.Name = "VideoHandler";
                movieBox.TrackBox.MediaBox.MediaInformationBox = new MediaInformationBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.VideoMediaHeaderBox = new VideoMediaHeaderBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.DataInformationBox = new DataInformationBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.DataInformationBox.DataReferenceBox = new DataReferenceBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.DataInformationBox.DataReferenceBox.DataEntryBoxes = new List<DataEntryBox>();
                movieBox.TrackBox.MediaBox.MediaInformationBox.DataInformationBox.DataReferenceBox.DataEntryBoxes.Add(new DataEntryUrlBox(1));
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox = new SampleTableBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleDescriptionBox = new SampleDescriptionBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleDescriptionBox.SampleEntries = new List<SampleEntry>();
                AVC1SampleEntry avc1 = new AVC1SampleEntry();
                avc1.AVCConfigurationBox = new AVCConfigurationBox();
                //h264
                avc1.Width = (ushort)movieBox.TrackBox.TrackHeaderBox.Width;
                avc1.Height = (ushort)movieBox.TrackBox.TrackHeaderBox.Height;
                avc1.AVCConfigurationBox.AVCLevelIndication = spsInfo.levelIdc;
                avc1.AVCConfigurationBox.AVCProfileIndication = spsInfo.profileIdc;
                avc1.AVCConfigurationBox.ProfileCompatibility = (byte)spsInfo.profileCompat;
                avc1.AVCConfigurationBox.PPSs = new List<byte[]>() { pps.RawData };
                avc1.AVCConfigurationBox.SPSs = new List<byte[]>() { sps.RawData };
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleDescriptionBox.SampleEntries.Add(avc1);
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.TimeToSampleBox = new TimeToSampleBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SyncSampleBox = new SyncSampleBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleToChunkBox = new SampleToChunkBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleSizeBox = new SampleSizeBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.ChunkOffsetBox = new ChunkOffsetBox();
                movieBox.MovieExtendsBox = new MovieExtendsBox();
                movieBox.MovieExtendsBox.TrackExtendsBoxs = new List<TrackExtendsBox>();
                TrackExtendsBox trex = new TrackExtendsBox();
                trex.TrackID = TrackID;
                trex.DefaultSampleDescriptionIndex = SampleDescriptionIndex;
                trex.DefaultSampleDuration = 0;
                trex.DefaultSampleSize = 0;
                trex.DefaultSampleFlags = 0;
                movieBox.MovieExtendsBox.TrackExtendsBoxs.Add(trex);
                movieBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// styp
        /// </summary>
        /// <returns></returns>
        public byte[] StypBox()
        {
            byte[] buffer = FMp4ArrayPool.Rent(1024);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                SegmentTypeBox stypTypeBox = new SegmentTypeBox();
                stypTypeBox.MajorBrand = "msdh";
                stypTypeBox.MinorVersion = "\0\0\0\0";
                stypTypeBox.CompatibleBrands.Add("isom");
                stypTypeBox.CompatibleBrands.Add("mp42");
                stypTypeBox.CompatibleBrands.Add("msdh");
                stypTypeBox.CompatibleBrands.Add("msix");
                stypTypeBox.CompatibleBrands.Add("iso5");
                stypTypeBox.CompatibleBrands.Add("iso6");
                stypTypeBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码其他视频数据盒子
        /// 注意：固定I帧解析 
        /// I P P P P I P P P P I P P P P
        /// todo:50ms或者一个关键帧进行切片
        /// todo:优化编码
        /// </summary>
        /// <returns></returns>
        public byte[] OtherVideoBox(in List<JT1078Package> nalus)
        {
            byte[] buffer = FMp4ArrayPool.Rent(nalus.Sum(s => s.Bodies.Length) + 4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                var truns = new List<TrackRunBox.TrackRunInfo>();
                string key = string.Empty;
                ulong timestamp = 0;
                uint subsegmentDuration=0;
                for (var i = 0; i < nalus.Count; i++)
                {
                    var nalu = nalus[i];
                    if (string.IsNullOrEmpty(key))
                    {
                        key = nalu.GetKey();
                    }
                    uint duration = 0;
                    if (timestamp>0)
                    {
                        duration=(uint)(nalu.Timestamp-timestamp);
                    }
                    truns.Add(new TrackRunBox.TrackRunInfo()
                    {
                        SampleDuration = duration,
                        SampleSize = (uint)(nalu.Bodies.Length),
                    });
                    subsegmentDuration+=duration;
                    timestamp=nalu.Timestamp;
                }
                if (!TrackInfos.TryGetValue(key, out TrackInfo trackInfo))
                {
                    trackInfo = new TrackInfo { SN = 1, DTS = 0, SubsegmentDuration=0 };
                    TrackInfos.Add(key, trackInfo);
                }
                if (trackInfo.SN == uint.MaxValue)
                {
                    trackInfo.SN = 1;
                }
                trackInfo.SN++;
                SegmentIndexBox segmentIndexBox = new SegmentIndexBox(1);
                segmentIndexBox.ReferenceID = 1;
                segmentIndexBox.EarliestPresentationTime = trackInfo.SubsegmentDuration;
                segmentIndexBox.SegmentIndexs = new List<SegmentIndexBox.SegmentIndex>()
                {
                     new SegmentIndexBox.SegmentIndex
                     {
                          SubsegmentDuration=subsegmentDuration
                     }
                };

                segmentIndexBox.ToBuffer(ref writer);

                var current1 = writer.GetCurrentPosition();
                //moof
                var movieFragmentBox = new MovieFragmentBox();
                movieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
                movieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = trackInfo.SN;
                movieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox(TfhdFlags);
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = TrackID;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.SampleDescriptionIndex = SampleDescriptionIndex;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = truns[0].SampleSize;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = FMp4Constants.TFHD_FLAG_VIDEO_TPYE;
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime =  trackInfo.SubsegmentDuration;
                trackInfo.SubsegmentDuration+=subsegmentDuration;
                TrackInfos[key] = trackInfo;
                //trun
                movieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(1, flags: TrunFlags);
                movieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = FMp4Constants.TREX_FLAG_SAMPLE_DEPENDS_ON_I_PICTURE;
                movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = truns;
                movieFragmentBox.ToBuffer(ref writer);
                //mdat
                var mediaDataBox = new MediaDataBox();
                mediaDataBox.Data=new List<byte[]>();
                foreach(var nalu in nalus)
                {
                    List<H264NALU> h264NALUs = h264Decoder.ParseNALU(nalu);
                    if (h264NALUs!=null)
                    {
                        foreach(var n in h264NALUs)
                        {
                            mediaDataBox.Data.Add(n.RawData);
                        }
                    }
                }
                mediaDataBox.ToBuffer(ref writer);

                var current2 = writer.GetCurrentPosition();
                foreach (var postion in segmentIndexBox.ReferencedSizePositions)
                {
                    writer.WriteUInt32Return((uint)(current2 - current1), postion);
                }

                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }

        struct TrackInfo
        {
            public uint SN { get; set; }
            public ulong DTS { get; set; }
            public ulong SubsegmentDuration { get; set; }
        }
    }
}
