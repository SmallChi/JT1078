using JT1078.FMp4.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT1078.Protocol.Extensions;
using JT1078.FMp4.Enums;
using JT1078.FMp4.Samples;
using System.Buffers.Binary;
using System.IO;
using System.Linq;

namespace JT1078.FMp4.Test
{
    public class FMp4Box_Test
    {
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "ftyp")]
        public void ftyp_test()
        {
            var MinorVersion = Encoding.ASCII.GetString(new byte[4] { 0,0,2,0});
            FileTypeBox fileTypeBox = new FileTypeBox();
            fileTypeBox.MajorBrand = "isom";
            fileTypeBox.MinorVersion = "\0\0\u0002\0";
            fileTypeBox.CompatibleBrands.Add("isom");
            fileTypeBox.CompatibleBrands.Add("iso2");
            fileTypeBox.CompatibleBrands.Add("avc1");
            fileTypeBox.CompatibleBrands.Add("iso6");
            fileTypeBox.CompatibleBrands.Add("mp41");
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x25]);
            fileTypeBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("000000246674797069736f6d0000020069736f6d69736f326176633169736f366d703431".ToUpper(), hex);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_mvhd")]
        public void moov_mvhd_test()
        {
            MovieHeaderBox movieHeaderBox = new MovieHeaderBox(0);
            movieHeaderBox.CreationTime = 0;
            movieHeaderBox.ModificationTime = 0;
            movieHeaderBox.Timescale = 1000;
            movieHeaderBox.Duration = 0;
            movieHeaderBox.NextTrackID = 2;
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            movieHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000006c6d766864000000000000000000000000000003e8000000000001000001000000000000000000000000010000000000000000000000000000000100000000000000000000000000004000000000000000000000000000000000000000000000000000000000000002".ToUpper(), hex);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_trak_tkhd")]
        public void moov_trak_tkhd_test()
        {   
            TrackHeaderBox trackHeaderBox = new TrackHeaderBox(0,3);
            trackHeaderBox.CreationTime = 0;
            trackHeaderBox.ModificationTime = 0;
            trackHeaderBox.TrackID = 1;
            trackHeaderBox.Duration = 0;
            trackHeaderBox.Width = 544u;
            trackHeaderBox.Height = 960u;
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            trackHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000005C746B68640000000300000000000000000000000100000000000000000000000000000000000000000000000000010000000000000000000000000000000100000000000000000000000000004000000000000220000003C0".ToUpper(), hex);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_trak_mdia")]
        public void moov_trak_mdia_test()
        {


        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_trak_mdia_mdhd")]
        public void moov_trak_mdia_mdhd_test()
        {  
            MediaHeaderBox mediaHeaderBox = new MediaHeaderBox(0, 0);
            mediaHeaderBox.CreationTime = 0;
            mediaHeaderBox.ModificationTime = 0;
            mediaHeaderBox.Timescale = 0x00124f80;
            mediaHeaderBox.Duration = 0;
            mediaHeaderBox.Language = "und";
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            mediaHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            //000000206d64686400000000000000000000000000124f800000000055c40000
            //00000020
            //6d646864
            //00000000
            //00000000
            //00000000
            //00124f80
            //00000000
            //55c4
            //0000
            Assert.Equal("000000206d64686400000000000000000000000000124f800000000055c40000".ToUpper(), hex);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_trak_mdia_hdlr")]
        public void moov_trak_mdia_hdlr_test()
        {
            HandlerBox handlerBox = new HandlerBox(0, 0);
            handlerBox.HandlerType = Enums.HandlerType.vide;
            handlerBox.Name = "VideoHandler";
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            handlerBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000002C68646C72000000000000000076696465000000000000000000000000566964656F48616E646C6572".ToUpper(), hex);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_trak_mdia_hdlr_minf")]
        public void moov_trak_mdia_hdlr_minf_test()
        {
            HandlerBox handlerBox = new HandlerBox(0, 0);
            handlerBox.HandlerType = Enums.HandlerType.vide;
            handlerBox.Name = "VideoHandler\0";
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            handlerBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("0000002D68646C72000000000000000076696465000000000000000000000000566964656F48616E646C657200".ToUpper(), hex);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_trak_mdia_hdlr_minf_vmhd")]
        public void moov_trak_mdia_hdlr_minf_vmhd_test()
        {
            VideoMediaHeaderBox videoMediaHeaderBox = new VideoMediaHeaderBox();
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            videoMediaHeaderBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("00000014766d6864000000010000000000000000".ToUpper(), hex);
        }        
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_trak_mdia_hdlr_minf_dinf")]
        public void moov_trak_mdia_hdlr_minf_dinf_test()
        {
            DataInformationBox dataInformationBox = new DataInformationBox();
            DataReferenceBox dataReferenceBox = new DataReferenceBox();
            dataReferenceBox.DataEntryBoxes = new List<DataEntryBox>();
            dataReferenceBox.DataEntryBoxes.Add(new DataEntryUrlBox(version: 0, flags: 1));
            dataInformationBox.DataReferenceBox = dataReferenceBox;
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[10240]);
            dataInformationBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            //0000002464696e660000001c6472656600000000000000010000000c75726c2000000001
            Assert.Equal("0000002464696e660000001c6472656600000000000000010000000c75726c2000000001".ToUpper(), hex);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_trak_mdia_hdlr_minf_stbl")]
        public void moov_trak_mdia_hdlr_minf_stbl_test()
        {
            //000000e17374626c000000957374736400000000000000010000008561766331000000000000000100000000000000000000000000000000022003c0004800000048000000000000000100000000000000000000000000000000000000000000000000000000000000000018ffff0000002f617663430164001fffe100176764001facd940881e684000f4240037b40883c60c658001000568efbcb0000000001073747473000000000000000000000010737473630000000000000000000000147374737a000000000000000000000000000000107374636f0000000000000000
            //stbl
            SampleTableBox sampleTableBox = new SampleTableBox();
            //stbl->stsd
            SampleDescriptionBox sampleDescriptionBox = new SampleDescriptionBox();
            //stbl->stsd->avc1
            AVC1SampleEntry aVC1SampleEntry = new AVC1SampleEntry();
            aVC1SampleEntry.Width = 0x0220;
            aVC1SampleEntry.Height = 0x03c0;
            //stbl->stsd->avc1->avcc
            AVCConfigurationBox aVCConfigurationBox = new AVCConfigurationBox();
            aVCConfigurationBox.AVCProfileIndication = 0x64;
            aVCConfigurationBox.ProfileCompatibility = 0;
            aVCConfigurationBox.AVCLevelIndication = 0x1f;
            aVCConfigurationBox.LengthSizeMinusOne = 0xff;
            aVCConfigurationBox.SPSs = new List<byte[]>()
            {
                "6764001facd940881e684000f4240037b40883c60c6580".ToHexBytes()
            };
            aVCConfigurationBox.PPSs = new List<byte[]>()
            {
                "68efbcb000".ToHexBytes()
            };
            aVC1SampleEntry.AVCConfigurationBox = aVCConfigurationBox;
            sampleDescriptionBox.SampleEntries = new List<SampleEntry>()
            {
                aVC1SampleEntry
            };
            sampleTableBox.SampleDescriptionBox = sampleDescriptionBox;
            //stbl->stts
            sampleTableBox.TimeToSampleBox = new TimeToSampleBox();
            //stbl->stsc
            sampleTableBox.SampleToChunkBox = new SampleToChunkBox();
            //stbl->stsz
            sampleTableBox.SampleSizeBox = new SampleSizeBox();
            //stbl->stco
            sampleTableBox.ChunkOffsetBox = new ChunkOffsetBox();
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x000000e1]);
            sampleTableBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("000000e17374626c000000957374736400000000000000010000008561766331000000000000000100000000000000000000000000000000022003c0004800000048000000000000000100000000000000000000000000000000000000000000000000000000000000000018ffff0000002f617663430164001fffe100176764001facd940881e684000f4240037b40883c60c658001000568efbcb0000000001073747473000000000000000000000010737473630000000000000000000000147374737a000000000000000000000000000000107374636f0000000000000000".ToUpper(), hex);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_mvex")]
        public void trak_mvex_test()
        {
            //moov->mvex
            MovieExtendsBox movieExtendsBox = new MovieExtendsBox();
            //moov->mvex->trex
            movieExtendsBox.TrackExtendsBoxs = new List<TrackExtendsBox>();
            TrackExtendsBox trackExtendsBox1 = new TrackExtendsBox();
            trackExtendsBox1.TrackID = 0x01;
            trackExtendsBox1.DefaultSampleDescriptionIndex = 0x01;
            movieExtendsBox.TrackExtendsBoxs.Add(trackExtendsBox1);
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x00000028]);
            movieExtendsBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("000000286d7665780000002074726578000000000000000100000001000000000000000000000000".ToUpper(), hex);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moov_udta")]
        public void moov_udta_test()
        {
            //todo:moov->udta

        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "moof")]
        public void moof_test()
        {
            //000006d8747261660000002474666864000000390000000100000000000002fc0000bb80000066ee01010000
            //00 00 06 d8
            //74 72 61 66
            //00 00 00 24
            //74 66 68 64
            //00
            //00 00 39
            //00 00 00 01
            //00 00 00 00 00 00 02 fc
            //00 00 bb 80
            //00 00 66 ee
            //01 01 00 00
            //moof
            MovieFragmentBox movieFragmentBox = new MovieFragmentBox();
            //moof->mfhd
            movieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
            movieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = 0x01;
            //moof->traf
            movieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
            //moof->traf->tfhd
            movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox(0x000039);
            movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = 0x01;
            movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.BaseDataOffset = 0x00000000000002fc;
            movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = 0x0000bb80;
            movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = 0x000066ee;
            movieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = 0x01010000;
            //moof->tfdt
            movieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
            //moof->trun
            //000006987472756E00000305000000D0000006F802000000
            //00 00 06 98
            //74 72 75 6E
            //00
            //00 03 05
            //00 00 00 D0                           SampleCount
            //00 00 06 F8                           DataOffset
            //02 00 00 00                           FirstSampleFlags
            //fragmented_demo_trun.txt              TrackRunInfos
            movieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(0, 0x00000305);
            movieFragmentBox.TrackFragmentBox.TrackRunBox.SampleCount = 0x000000D0;
            movieFragmentBox.TrackFragmentBox.TrackRunBox.DataOffset = 0x000006F8;
            movieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = 0x02000000;
            movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = new List<TrackRunBox.TrackRunInfo>();
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FMP4", "fragmented_demo_trun.txt"));
            var buffers = lines.Where(w => !string.IsNullOrEmpty(w)).Select(s => s.ToHexBytes()).ToList();
            //SampleDuration
            //SampleSize
            foreach (var buffer in buffers)
            {
                var mod = buffer.Length / 8;
                for (int i = 0; i < mod; i++)
                {
                    TrackRunBox.TrackRunInfo trackRunInfo = new TrackRunBox.TrackRunInfo();
                    trackRunInfo.SampleDuration = BinaryPrimitives.ReadUInt32BigEndian(buffer.Skip(i * 4).Take(4).ToArray());
                    trackRunInfo.SampleSize= BinaryPrimitives.ReadUInt32BigEndian(buffer.Skip((i + 1)*4).Take(4).ToArray());
                    movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Add(trackRunInfo);
                }
            }
            Assert.Equal(0x00000698, movieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Count*8+4*6);
        }
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "mdat")]
        public void mdat_test()
        {
            MediaDataBox mediaDataBox = new MediaDataBox();
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FMP4", "fragmented_demo_mdat.txt"));
            var buffers = lines.Where(w => !string.IsNullOrEmpty(w)).Select(s => s.ToHexBytes()).ToList();
            List<byte> data = new List<byte>();
            foreach (var buffer in buffers)
            {
                data = data.Concat(buffer).ToList();
            }
            //00 0E 3C 9C  Size
            //6D 64 61 74  BoxType
            Assert.Equal(0x000E3C9C-8, data.Count);
        }  
        /// <summary>
        /// 使用doc/video/fragmented_demo.mp4
        /// </summary>
        [Fact(DisplayName = "mfra")]
        public void mfra_test()
        {
            //000000436d6672610000002b7466726101000000000000010000000000000001000000000000000000000000000002fc010101000000106d66726f0000000000000043
            //00 00 00 43
            //6d 66 72 61
            //00 00 00 2b
            //74 66 72 61
            //01
            //00 00 00
            //00 00 00 01
            //00 00 00 00
            //00 00 00 01
            //00 00 00 00 00 00 00 00 
            //00 00 00 00 00 00 02 fc 
            //01 01 01 
            //00 00 00 10 
            //6d 66 72 6f
            //00
            //00 00 00 
            //00 00 00 43
            //mfra
            MovieFragmentRandomAccessBox movieFragmentRandomAccessBox = new MovieFragmentRandomAccessBox();
            //mfra->tfra
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox = new TrackFragmentRandomAccessBox(1);
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox.TrackID = 0x01;
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfos = new List<TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfo>();
            TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfo trackFragmentRandomAccessInfo1 = new TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfo();
            trackFragmentRandomAccessInfo1.Time = 0;
            trackFragmentRandomAccessInfo1.MoofOffset = 0x00000000000002fc;
            trackFragmentRandomAccessInfo1.TrafNumber = 0x01;
            trackFragmentRandomAccessInfo1.TrunNumber = 0x01;
            trackFragmentRandomAccessInfo1.SampleNumber = 0x01;
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfos.Add(trackFragmentRandomAccessInfo1);
           //mfra->mfro
            movieFragmentRandomAccessBox.MovieFragmentRandomAccessOffsetBox = new MovieFragmentRandomAccessOffsetBox(0);
            movieFragmentRandomAccessBox.MovieFragmentRandomAccessOffsetBox.MfraSize = 0x00000043;
            FMp4MessagePackWriter writer = new MessagePack.FMp4MessagePackWriter(new byte[0x00000043]);
            movieFragmentRandomAccessBox.ToBuffer(ref writer);
            var hex = writer.FlushAndGetArray().ToHexString();
            Assert.Equal("000000436d6672610000002b7466726101000000000000010000000000000001000000000000000000000000000002fc010101000000106d66726f0000000000000043".ToUpper(), hex);
        }
    }
}
