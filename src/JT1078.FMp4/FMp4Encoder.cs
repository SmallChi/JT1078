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
        //Dictionary<string, TrackInfo> TrackInfos;

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
            //TrackInfos = new Dictionary<string, TrackInfo>(StringComparer.OrdinalIgnoreCase);
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
                fileTypeBox.CompatibleBrands.Add("iso6");
                fileTypeBox.CompatibleBrands.Add("iso2");
                fileTypeBox.CompatibleBrands.Add("avc1");
                fileTypeBox.CompatibleBrands.Add("mp41");
                //fileTypeBox.CompatibleBrands.Add("isom");
                //fileTypeBox.CompatibleBrands.Add("iso2");
                //fileTypeBox.CompatibleBrands.Add("avc1");
                //fileTypeBox.CompatibleBrands.Add("mp41");
                //fileTypeBox.CompatibleBrands.Add("iso5");
                //fileTypeBox.CompatibleBrands.Add("iso6");
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
                movieBox.MovieHeaderBox = new MovieHeaderBox(0, 0);
                movieBox.MovieHeaderBox.CreationTime = 0;
                movieBox.MovieHeaderBox.ModificationTime = 0;
                movieBox.MovieHeaderBox.Duration = 0;
                movieBox.MovieHeaderBox.Timescale = 1000;
                movieBox.MovieHeaderBox.Rate = 0x00010000;//typically 1.0  媒体速率，这个值代表原始倍速
                movieBox.MovieHeaderBox.Volume = 0x0100;// typically,1.0 full volume   媒体音量，这个值代表满音量
                movieBox.MovieHeaderBox.NextTrackID = 2;
                movieBox.TrackBox = new TrackBox();
                movieBox.TrackBox.TrackHeaderBox = new TrackHeaderBox(0, 3);
                movieBox.TrackBox.TrackHeaderBox.CreationTime = 0;
                movieBox.TrackBox.TrackHeaderBox.ModificationTime = 0;
                movieBox.TrackBox.TrackHeaderBox.TrackID = TrackID;//1
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
                //movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SyncSampleBox = new SyncSampleBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleToChunkBox = new SampleToChunkBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleSizeBox = new SampleSizeBox();
                movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.ChunkOffsetBox = new ChunkOffsetBox();
                movieBox.MovieExtendsBox = new MovieExtendsBox();
                movieBox.MovieExtendsBox.TrackExtendsBoxs = new List<TrackExtendsBox>();
                TrackExtendsBox trex = new TrackExtendsBox();
                trex.TrackID = TrackID;//1
                trex.DefaultSampleDescriptionIndex = SampleDescriptionIndex;//1
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
        public byte[] EncoderOtherVideoBox(List<H264NALU> nalus, FMp4EncoderInfo encoderInfo)
        {
            byte[] buffer= buffer = FMp4ArrayPool.Rent(nalus.Sum(m=>m.RawData.Length + m.StartCodePrefix.Length) + 4096);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(buffer);
            try
            {
                FragmentBox fragmentBox = new FragmentBox();
                fragmentBox.MovieFragmentBox = new MovieFragmentBox();
                fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
                fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = encoderInfo.SequenceNumber;
                fragmentBox.MovieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox =  new TrackFragmentHeaderBox(TfhdFlags);//0x39
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = TrackID;//1
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.BaseDataOffset = encoderInfo.SampleSize;//基于前面盒子的长度
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.SampleDescriptionIndex = SampleDescriptionIndex;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = DefaultSampleDuration;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = (uint)(nalus.Sum(m => m.RawData.Length + m.StartCodePrefix.Length));
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = DefaultSampleFlags;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = nalus[0].Timestamp * 1000;
                //trun
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(flags: 0x5);//TrunFlags
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = FirstSampleFlags;// 0;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = new List<TrackRunBox.TrackRunInfo>();
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Add(new TrackRunBox.TrackRunInfo());
                fragmentBox.MediaDataBox = new MediaDataBox();
                fragmentBox.MediaDataBox.Data = nalus.Select(s => s.RawData).ToList();
                fragmentBox.ToBuffer(ref writer);
                var data = writer.FlushAndGetArray();
                return data;
            }
            finally
            {
                FMp4ArrayPool.Return(buffer);
            }  
        }
        /// <summary>
        /// 编码mp4 视频
        /// </summary>
        /// <param name="package"></param>
        /// <param name="needVideoHeader"></param>
        /// <returns></returns>
        public byte[] EncoderVideo(JT1078Package package, FMp4EncoderInfo encoderInfo, bool needVideoHeader = false) {
            H264Decoder h264Decoder = new H264Decoder();
            byte[] buffer = FMp4ArrayPool.Rent(package.Bodies.Length * 2 + 4096);
            FMp4MessagePackWriter flvMessagePackWriter = new FMp4MessagePackWriter(buffer);
            var nalus = h264Decoder.ParseNALU(package);
            if (nalus != null && nalus.Count > 0)
            {
                try
                {
                    var sei = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == NalUnitType.SEI);
                    var sps = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == NalUnitType.SPS);
                    var pps = nalus.FirstOrDefault(x => x.NALUHeader.NalUnitType == NalUnitType.PPS);
                    nalus.Remove(sei);
                    nalus.Remove(sps);
                    nalus.Remove(pps);
                    if (needVideoHeader)
                    {
                        //mp4 header
                        //ftype
                        var ftypHeader = EncoderFtypBox();
                        encoderInfo.SampleSize += (uint)ftypHeader.Length;
                        flvMessagePackWriter.WriteArray(ftypHeader);
                        // moov
                        var moov = EncoderMoovBox(sps, pps);
                        encoderInfo.SampleSize += (uint)moov.Length;
                        flvMessagePackWriter.WriteArray(moov);
                        //解析sps
                    }
                    var otherVideoTag = EncoderOtherVideoBox(nalus, encoderInfo);
                    encoderInfo.SampleSize += (uint)otherVideoTag.Length;
                    flvMessagePackWriter.WriteArray(otherVideoTag);
                }
                finally
                {
                    FMp4ArrayPool.Return(buffer);
                }
            }
            var data = flvMessagePackWriter.FlushAndGetArray();
            return data;
        }
        //struct TrackInfo
        //{
        //    public uint SN { get; set; }
        //    public ulong DTS { get; set; }
        //}
    }
    /// <summary>
    /// 编码信息
    /// </summary>
    public class FMp4EncoderInfo
    {
        /// <summary>
        /// 样本大小，即盒子大小
        /// </summary>
        public uint SampleSize { get; set; } = 0;
        private uint sequenceNum = 0;
        /// <summary>
        /// 轨道序号
        /// </summary>
        public uint SequenceNumber
        {
            get
            {
                return sequenceNum++;
            }
        }
    }
}
