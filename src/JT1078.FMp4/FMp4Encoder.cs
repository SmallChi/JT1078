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
        Dictionary<string, TrackInfo> TrackInfos;

        const uint DefaultSampleDuration = 48000u;
        const uint DefaultSampleFlags = 0x1010000;
        const uint FirstSampleFlags = 33554432;
        const uint TfhdFlags = 0x2003a;
        const uint TrunFlags = 0x205;
        const uint SampleDescriptionIndex = 1;
        const uint TrackID = 1;

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
        public byte[] EncoderFtypBox()
        {
            byte[] buffer = FMp4ArrayPool.Rent(1024);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                //ftyp
                FileTypeBox fileTypeBox = new FileTypeBox();
                fileTypeBox.MajorBrand = "isom";
                fileTypeBox.MinorVersion = "\0\0\u0002\0";
                fileTypeBox.CompatibleBrands.Add("isom");
                fileTypeBox.CompatibleBrands.Add("iso2");
                fileTypeBox.CompatibleBrands.Add("avc1");
                fileTypeBox.CompatibleBrands.Add("mp41");
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

        /// <summary>
        /// 编码moov盒子
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderMoovBox(in H264NALU sps, in H264NALU pps)
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
        /// 编码其他视频数据盒子
        /// </summary>
        /// <returns></returns>
        public byte[] EncoderOtherVideoBox(in List<H264NALU> nalus)
        {
            byte[] buffer = FMp4ArrayPool.Rent(nalus.Sum(s => s.RawData.Length + s.StartCodePrefix.Length) + 4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                var truns = new List<TrackRunBox.TrackRunInfo>();
                List<byte[]> rawdatas = new List<byte[]>();
                uint iSize = 0;
                ulong lastTimestamp = 0;
                string key = string.Empty;
                for (var i = 0; i < nalus.Count; i++)
                {
                    var nalu = nalus[i];
                    if (string.IsNullOrEmpty(key))
                    {
                        key = nalu.GetKey();
                    }
                    rawdatas.Add(nalu.RawData);
                    if (nalu.DataType == Protocol.Enums.JT1078DataType.视频I帧)
                    {
                        iSize += (uint)(nalu.RawData.Length + nalu.StartCodePrefix.Length);
                    }
                    else
                    {
                        if (iSize > 0)
                        {
                            truns.Add(new TrackRunBox.TrackRunInfo()
                            {
                                SampleSize = iSize,
                            });
                            iSize = 0;
                        }
                        truns.Add(new TrackRunBox.TrackRunInfo()
                        {
                            SampleSize = (uint)(nalu.RawData.Length + nalu.StartCodePrefix.Length),
                        });
                    }
                    if (i == (nalus.Count - 1))
                    {
                        lastTimestamp = nalu.Timestamp;
                    }
                }
                if (TrackInfos.TryGetValue(key, out TrackInfo trackInfo))
                {
                    if (trackInfo.SN == uint.MaxValue)
                    {
                        trackInfo.SN = 1;
                    }
                    trackInfo.SN++;
                }
                else
                {
                    trackInfo = new TrackInfo { SN = 1, DTS = 0 };
                    TrackInfos.Add(key, trackInfo);
                }
                var movieFragmentBox = new MovieFragmentBox();
                movieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
                movieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = trackInfo.SN;
                movieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox(TfhdFlags);
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = TrackID;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.SampleDescriptionIndex = SampleDescriptionIndex;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = DefaultSampleDuration;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = truns[0].SampleSize;
                movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = DefaultSampleFlags;
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
                movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = trackInfo.DTS;
                trackInfo.DTS += (ulong)(truns.Count * DefaultSampleDuration);
                TrackInfos[key] = trackInfo;
                //trun
                movieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(flags: TrunFlags);
                movieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = FirstSampleFlags;
                movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = truns;
                movieFragmentBox.ToBuffer(ref writer);

                var mediaDataBox = new MediaDataBox();
                mediaDataBox.Data = rawdatas;
                mediaDataBox.ToBuffer(ref writer);

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
        }
    }
}
