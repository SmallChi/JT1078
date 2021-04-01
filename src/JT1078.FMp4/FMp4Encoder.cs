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
    /// moof 1
    /// mdat 1
    /// ...
    /// styp n
    /// moof n
    /// mdat n
    /// mfra
    /// ref: https://www.w3.org/TR/mse-byte-stream-format-isobmff/#movie-fragment-relative-addressing
    /// </summary>
    public class FMp4Encoder
    {
        readonly H264Decoder h264Decoder;

        /// <summary>
        /// 
        /// </summary>
        public FMp4Encoder()
        {
            h264Decoder = new H264Decoder();
        }

        /// <summary>
        /// styp
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderStypBox()
        {
            byte[] buffer = FMp4ArrayPool.Rent(4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                //styp
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
        /// 编码ftyp盒子
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderFtypBox()
        {
            byte[] buffer = FMp4ArrayPool.Rent(4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                //ftyp
                //FileTypeBox fileTypeBox = new FileTypeBox();
                //fileTypeBox.MajorBrand = "isom";
                //fileTypeBox.MinorVersion = "\0\0\u0002\0";
                //fileTypeBox.CompatibleBrands.Add("isom");
                //fileTypeBox.CompatibleBrands.Add("iso2");
                //fileTypeBox.CompatibleBrands.Add("avc1");
                //fileTypeBox.CompatibleBrands.Add("mp41");
                //fileTypeBox.CompatibleBrands.Add("iso5");
                FileTypeBox fileTypeBox = new FileTypeBox();
                fileTypeBox.MajorBrand = "msdh";
                fileTypeBox.MinorVersion = "\0\0\0\0";
                fileTypeBox.CompatibleBrands.Add("isom");
                fileTypeBox.CompatibleBrands.Add("mp42");
                fileTypeBox.CompatibleBrands.Add("msdh");
                fileTypeBox.CompatibleBrands.Add("msix");
                fileTypeBox.CompatibleBrands.Add("iso5");
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

        ulong IframeIntervalCache = 259960;

        ulong cts = 0;

        /// <summary>
        /// 编码sidx盒子
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderSidxBox(int moofAndMdatLength, ulong timestamp, uint IframeInterval, uint frameInterval)
        {
            byte[] buffer = FMp4ArrayPool.Rent(4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                SegmentIndexBox segmentIndexBox = new SegmentIndexBox(1);
                segmentIndexBox.ReferenceID = 1;
                cts = cts == 0 ? 2160000 : (cts + cts);
                segmentIndexBox.EarliestPresentationTime = cts;
                IframeIntervalCache += frameInterval;
                segmentIndexBox.SegmentIndexs = new List<SegmentIndexBox.SegmentIndex>()
                {
                     new SegmentIndexBox.SegmentIndex
                     {
                          ReferencedSize=(uint)moofAndMdatLength,
                          SubsegmentDuration=frameInterval
                     }
                };
                segmentIndexBox.ToBuffer(ref writer);
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
        public byte[] EncoderMoovBox(List<H264NALU> nalus, int naluLength)
        {
            byte[] buffer = FMp4ArrayPool.Rent(naluLength + 4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                var spsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.SPS);
                //SPS
                spsNALU.RawData = h264Decoder.DiscardEmulationPreventionBytes(spsNALU.RawData);
                var ppsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.PPS);
                ppsNALU.RawData = h264Decoder.DiscardEmulationPreventionBytes(ppsNALU.RawData);
                ExpGolombReader h264GolombReader = new ExpGolombReader(spsNALU.RawData);
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
                movieBox.TrackBox.TrackHeaderBox.TrackID = 1;
                movieBox.TrackBox.TrackHeaderBox.Duration = 0;
                movieBox.TrackBox.TrackHeaderBox.TrackIsAudio = false;
                movieBox.TrackBox.TrackHeaderBox.Width = (uint)spsInfo.width;
                movieBox.TrackBox.TrackHeaderBox.Height = (uint)spsInfo.height;
                movieBox.TrackBox.MediaBox = new MediaBox();
                movieBox.TrackBox.MediaBox.MediaHeaderBox = new MediaHeaderBox();
                movieBox.TrackBox.MediaBox.MediaHeaderBox.CreationTime = 0;
                movieBox.TrackBox.MediaBox.MediaHeaderBox.ModificationTime = 0;
                movieBox.TrackBox.MediaBox.MediaHeaderBox.Timescale = 1200000;
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
                avc1.AVCConfigurationBox.PPSs = new List<byte[]>() { ppsNALU.RawData };
                avc1.AVCConfigurationBox.SPSs = new List<byte[]>() { spsNALU.RawData };
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleDescriptionBox.SampleEntries.Add(avc1);
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.TimeToSampleBox = new TimeToSampleBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SyncSampleBox = new SyncSampleBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleToChunkBox = new SampleToChunkBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleSizeBox = new SampleSizeBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.ChunkOffsetBox = new ChunkOffsetBox();
                movieBox.MovieExtendsBox = new MovieExtendsBox();
                movieBox.MovieExtendsBox.TrackExtendsBoxs = new List<TrackExtendsBox>();
                TrackExtendsBox trex = new TrackExtendsBox();
                trex.TrackID = 1;
                trex.DefaultSampleDescriptionIndex = 1;
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
        /// 编码moov盒子
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderMoovBox(in H264NALU sps, in H264NALU pps)
        {
            byte[] buffer = FMp4ArrayPool.Rent(sps.RawData.Length+ pps.RawData.Length  + 4096);
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
                movieBox.TrackBox.TrackHeaderBox.TrackID = 1;
                movieBox.TrackBox.TrackHeaderBox.Duration = 0;
                movieBox.TrackBox.TrackHeaderBox.TrackIsAudio = false;
                movieBox.TrackBox.TrackHeaderBox.Width = (uint)spsInfo.width;
                movieBox.TrackBox.TrackHeaderBox.Height = (uint)spsInfo.height;
                movieBox.TrackBox.MediaBox = new MediaBox();
                movieBox.TrackBox.MediaBox.MediaHeaderBox = new MediaHeaderBox();
                movieBox.TrackBox.MediaBox.MediaHeaderBox.CreationTime = 0;
                movieBox.TrackBox.MediaBox.MediaHeaderBox.ModificationTime = 0;
                movieBox.TrackBox.MediaBox.MediaHeaderBox.Timescale = 1200000;
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
                trex.TrackID = 1;
                trex.DefaultSampleDescriptionIndex = 1;
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
        /// 编码Moof盒子
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderMoofBox(List<H264NALU> nalus, int naluLength, ulong timestamp, uint frameInterval, uint IframeInterval, uint keyframeFlag)
        {
            byte[] buffer = FMp4ArrayPool.Rent(naluLength + 4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                var movieFragmentBox = new MovieFragmentBox();
                movieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
                movieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = sn++;
                movieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
                //0x39 写文件
                //0x02 分段
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox(0x2003a);
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = 1;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.SampleDescriptionIndex = 1;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = frameInterval;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = (uint)naluLength;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = 0x1010000;
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = timestamp*1000;
                //trun
                //0x39 写文件
                //0x02 分段
                //0x205
                //uint flag = 0x000200 | 0x000800 | 0x000400 | 0x000100;
                uint flag = 0x0001;
                if (keyframeFlag == 1)
                {
                    flag |= 0x0004;
                }
                //flag |= 0x000200;
                movieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(flags: flag);
                movieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = 33554432;
                movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = new List<TrackRunBox.TrackRunInfo>();
                movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Add(new TrackRunBox.TrackRunInfo()
                {
                    //SampleDuration= frameInterval,
                    SampleSize = (uint)naluLength,
                    //SampleCompositionTimeOffset = frameInterval,
                    //SampleFlags = movieFragmentBox.TrackFragmentBox.TrackRunBox.Flags
                });
                movieFragmentBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码Moof盒子
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderMoofBox(List<int> naluSzies, ulong timestamp, uint frameInterval, uint IframeInterval, uint keyframeFlag)
        {
            byte[] buffer = FMp4ArrayPool.Rent(4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                var movieFragmentBox = new MovieFragmentBox();
                movieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
                movieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = sn++;
                movieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
                //0x39 写文件
                //0x02 分段
                //0x2003a
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox(0x2003a);
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = 1;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.SampleDescriptionIndex = 1;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = frameInterval;
                //movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = 48000;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = (uint)naluSzies[0];
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = 0x1010000;
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = cts;
                //trun
                //0x39 写文件
                //0x02 分段
                //0x205
                //uint flag = 0x000200 | 0x000800 | 0x000400 | 0x000100;
                uint flag = 0x0001;
                if (!first)
                {
                    flag |= 0x0004;
                    first = true;
                }
                flag |= 0x000200;
                movieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(flags: flag);
                movieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = 33554432;
                movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = new List<TrackRunBox.TrackRunInfo>();
                foreach(var size in naluSzies)
                {
                    movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Add(new TrackRunBox.TrackRunInfo()
                    {
                        //SampleDuration= frameInterval,
                        SampleSize = (uint)size,
                        //SampleCompositionTimeOffset = frameInterval,
                        //SampleFlags = movieFragmentBox.TrackFragmentBox.TrackRunBox.Flags
                    });
                }
                movieFragmentBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码Mdat盒子
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderMdatBox(List<H264NALU> nalus, int naluLength)
        {
            byte[] buffer = FMp4ArrayPool.Rent(naluLength + 4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                var mediaDataBox = new MediaDataBox();
                mediaDataBox.Data = nalus.Select(s => s.RawData).ToList();
                mediaDataBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码Mdat盒子
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderMdatBox(List<byte[]> nalus)
        {
            byte[] buffer = FMp4ArrayPool.Rent(nalus.Sum(s=>s.Length) + 4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                var mediaDataBox = new MediaDataBox();
                mediaDataBox.Data = nalus;
                mediaDataBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 编码首个视频盒子
        /// </summary>
        /// <param name="package">jt1078完整包</param>
        /// <returns></returns>
        public byte[] EncoderFirstVideoBox(JT1078Package package)
        {
            byte[] buffer = FMp4ArrayPool.Rent(package.Bodies.Length + 4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                var nalus = h264Decoder.ParseNALU(package);
                var spsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.SPS);
                //SPS
                spsNALU.RawData = h264Decoder.DiscardEmulationPreventionBytes(spsNALU.RawData);
                var ppsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.PPS);
                ppsNALU.RawData = h264Decoder.DiscardEmulationPreventionBytes(ppsNALU.RawData);
                ExpGolombReader h264GolombReader = new ExpGolombReader(spsNALU.RawData);
                var spsInfo = h264GolombReader.ReadSPS();
                //ftyp
                FileTypeBox fileTypeBox = new FileTypeBox();
                fileTypeBox.MajorBrand = "isom";
                fileTypeBox.MinorVersion = "\0\0\u0002\0";
                fileTypeBox.CompatibleBrands.Add("isom");
                fileTypeBox.CompatibleBrands.Add("iso6");
                fileTypeBox.CompatibleBrands.Add("iso2");
                fileTypeBox.CompatibleBrands.Add("avc1");
                fileTypeBox.CompatibleBrands.Add("mp41");
                //moov
                MovieBox movieBox = new MovieBox();
                movieBox.MovieHeaderBox = new MovieHeaderBox(0, 0);
                movieBox.MovieHeaderBox.CreationTime = 0;
                movieBox.MovieHeaderBox.ModificationTime = 0;
                movieBox.MovieHeaderBox.Duration = 0;
                movieBox.MovieHeaderBox.Timescale = 1000;
                movieBox.MovieHeaderBox.NextTrackID = 2;
                movieBox.TrackBox = new TrackBox();
                movieBox.TrackBox.TrackHeaderBox = new TrackHeaderBox(0, 3);
                movieBox.TrackBox.TrackHeaderBox.CreationTime = 0;
                movieBox.TrackBox.TrackHeaderBox.ModificationTime = 0;
                movieBox.TrackBox.TrackHeaderBox.TrackID = 1;
                movieBox.TrackBox.TrackHeaderBox.Duration = 0;
                movieBox.TrackBox.TrackHeaderBox.TrackIsAudio = false;
                movieBox.TrackBox.TrackHeaderBox.Width = (uint)spsInfo.width;
                movieBox.TrackBox.TrackHeaderBox.Height = (uint)spsInfo.height;
                movieBox.TrackBox.MediaBox = new MediaBox();
                movieBox.TrackBox.MediaBox.MediaHeaderBox = new MediaHeaderBox();
                movieBox.TrackBox.MediaBox.MediaHeaderBox.CreationTime = 0;
                movieBox.TrackBox.MediaBox.MediaHeaderBox.ModificationTime = 0;
                movieBox.TrackBox.MediaBox.MediaHeaderBox.Timescale = 1200000;
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
                avc1.AVCConfigurationBox.PPSs = new List<byte[]>() { ppsNALU.RawData };
                avc1.AVCConfigurationBox.SPSs = new List<byte[]>() { spsNALU.RawData };
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleDescriptionBox.SampleEntries.Add(avc1);
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.TimeToSampleBox = new TimeToSampleBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleToChunkBox = new SampleToChunkBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleSizeBox = new SampleSizeBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.ChunkOffsetBox = new ChunkOffsetBox();
                movieBox.MovieExtendsBox = new MovieExtendsBox();
                movieBox.MovieExtendsBox.TrackExtendsBoxs = new List<TrackExtendsBox>();
                TrackExtendsBox trex = new TrackExtendsBox();
                trex.TrackID = 1;
                trex.DefaultSampleDescriptionIndex = 1;
                trex.DefaultSampleDuration = 0;
                trex.DefaultSampleSize = 0;
                trex.DefaultSampleFlags = 0;
                movieBox.MovieExtendsBox.TrackExtendsBoxs.Add(trex);
                fileTypeBox.ToBuffer(ref writer);
                movieBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }

        uint sn = 1;
        bool first = false;

        /// <summary>
        /// 编码其他视频数据盒子
        /// </summary>
        /// <param name="package">jt1078完整包</param>
        /// <param name="moofOffset">moof位置</param>
        /// <returns></returns>
        public byte[] EncoderOtherVideoBox(JT1078Package package, ulong moofOffset = 0)
        {
            byte[] buffer = FMp4ArrayPool.Rent(package.Bodies.Length + 4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                var nalus = h264Decoder.ParseNALU(package);
                var movieFragmentBox = new MovieFragmentBox();
                movieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
                movieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = sn++;
                movieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
                //0x39 写文件
                //0x02 分段
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox(2);
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = 1;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.BaseDataOffset = moofOffset;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = 48000;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = (uint)package.Bodies.Length;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = 0x1010000;
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = package.Timestamp * 1000;
                //trun
                //0x39 写文件
                //0x02 分段
                uint flag = package.Label3.DataType == JT1078DataType.视频I帧 ? 1u : 0u;
                movieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(flags: 0x000400);
                movieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = 0;
                movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = new List<TrackRunBox.TrackRunInfo>();
                //movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Add(new TrackRunBox.TrackRunInfo());
                foreach (var nalu in nalus)
                {
                    movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Add(new TrackRunBox.TrackRunInfo()
                    {
                        SampleSize = (uint)nalu.RawData.Length,
                        SampleCompositionTimeOffset = package.Label3.DataType == JT1078DataType.视频I帧 ? package.LastIFrameInterval : package.LastFrameInterval,
                        SampleFlags = flag
                    });
                }
                movieFragmentBox.ToBuffer(ref writer);
                var mediaDataBox = new MediaDataBox();
                mediaDataBox.Data = nalus.Select(s => s.RawData).ToList();
                mediaDataBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }
        }
    }
}
