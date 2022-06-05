using JT1078.FMp4.Enums;
using JT1078.FMp4.MessagePack;
using JT1078.FMp4.Samples;
using JT1078.Protocol;
using JT1078.Protocol.Enums;
using JT1078.Protocol.Extensions;
using JT1078.Protocol.H264;
using JT1078.Protocol.MessagePack;
using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JT1078.FMp4.Test
{
    public class JT1078ToFMp4Box_Test
    {
        [Fact]
        public void Test1()
        {
            var jT1078Package = ParseNALUTest();
            H264Decoder decoder = new H264Decoder();
            var nalus = decoder.ParseNALU(jT1078Package);
            var spsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.SPS);
            //SPS
            spsNALU.RawData = decoder.DiscardEmulationPreventionBytes(spsNALU.RawData);
            var ppsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.PPS);
            ppsNALU.RawData = decoder.DiscardEmulationPreventionBytes(ppsNALU.RawData);
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
            movieBox.TrackBox.TrackHeaderBox.Width = 352;
            movieBox.TrackBox.TrackHeaderBox.Height = 288;
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
            //MSE codecs avc1.4D0014
            //4D 00 14
            //AVCProfileIndication profile_compability AVCLevelIndication
            avc1.AVCConfigurationBox.AVCLevelIndication = 20;
            avc1.AVCConfigurationBox.AVCProfileIndication = 77;
            avc1.AVCConfigurationBox.PPSs = new List<byte[]>() { ppsNALU.RawData };
            avc1.AVCConfigurationBox.SPSs = new List<byte[]>() { spsNALU.RawData };
            avc1.AVCConfigurationBox.ProfileCompatibility = 0;
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
            //用户自定义可以不用
            //movieBox.UserDataBox = new UserDataBox();
            //movieBox.UserDataBox.Data = "0000005a6d657461000000000000002168646c7200000000000000006d6469726170706c0000000000000000000000002d696c737400000025a9746f6f0000001d6461746100000001000000004c61766635382e34352e313030".ToHexBytes();
            //fragment moof n
            List<FragmentBox> moofs = new List<FragmentBox>();
            FragmentBox fragmentBox = new FragmentBox();
            fragmentBox.MovieFragmentBox = new MovieFragmentBox();
            fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
            fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = 1;
            fragmentBox.MovieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox(0x39);
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = 1;
            //todo:BaseDataOffset 
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.BaseDataOffset = 0x000000000000028b;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = 48000;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = (uint)jT1078Package.Bodies.Length;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = 0x1010000;
            //fragmentBox.MovieFragmentBox.TrackFragmentBox.SampleDependencyTypeBox = new SampleDependencyTypeBox();
            //fragmentBox.MovieFragmentBox.TrackFragmentBox.SampleDependencyTypeBox.SampleDependencyTypes = new List<SampleDependencyTypeBox.SampleDependencyType>();
            //todo:fragmentBox.MovieFragmentBox.TrackFragmentBox.SampleDependencyTypeBox.SampleDependencyTypes
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
            //todo:BaseMediaDecodeTime 
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = 0;
            //trun
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(flags: 0x5);
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.DataOffset = 120;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = 0;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = new List<TrackRunBox.TrackRunInfo>();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Add(new TrackRunBox.TrackRunInfo());
            fragmentBox.MediaDataBox = new MediaDataBox();
            fragmentBox.MediaDataBox.Data = nalus
                        .Select(s => s.RawData)
                        .ToList();
            moofs.Add(fragmentBox);
            //mfra
            MovieFragmentRandomAccessBox movieFragmentRandomAccessBox = new MovieFragmentRandomAccessBox();
            //mfra->tfra
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox = new TrackFragmentRandomAccessBox(1);
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox.TrackID = 0x01;
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfos = new List<TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfo>();
            TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfo trackFragmentRandomAccessInfo1 = new TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfo();
            trackFragmentRandomAccessInfo1.Time = 0;
            //todo:MoofOffset
            trackFragmentRandomAccessInfo1.MoofOffset = 0x000000000000028b;
            trackFragmentRandomAccessInfo1.TrafNumber = 0x01;
            trackFragmentRandomAccessInfo1.TrunNumber = 0x01;
            trackFragmentRandomAccessInfo1.SampleNumber = 0x01;
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfos.Add(trackFragmentRandomAccessInfo1);
            //mfra->mfro
            movieFragmentRandomAccessBox.MovieFragmentRandomAccessOffsetBox = new MovieFragmentRandomAccessOffsetBox(0);
            //todo:MfraSize 
            movieFragmentRandomAccessBox.MovieFragmentRandomAccessOffsetBox.MfraSize = 0x00000043;
            FMp4Box fMp4Box = new FMp4Box();
            fMp4Box.FileTypeBox = fileTypeBox;
            fMp4Box.MovieBox = movieBox;
            fMp4Box.FragmentBoxs = moofs;
            fMp4Box.MovieFragmentRandomAccessBox = movieFragmentRandomAccessBox;
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(new byte[65535]);
            fMp4Box.ToBuffer(ref writer);
            var buffer = writer.FlushAndGetArray();
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.mp4");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.Write(buffer);
            fileStream.Close();
        }

        [Fact]
        public void Test2()
        {
            var jT1078Package = ParseNALUTest();
            H264Decoder decoder = new H264Decoder();
            var nalus = decoder.ParseNALU(jT1078Package);
            var spsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.SPS);
            //SPS
            spsNALU.RawData = decoder.DiscardEmulationPreventionBytes(spsNALU.RawData);
            var ppsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.PPS);
            ppsNALU.RawData = decoder.DiscardEmulationPreventionBytes(ppsNALU.RawData);
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(new byte[65535]);
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
            movieBox.TrackBox.TrackHeaderBox.Width = 352;
            movieBox.TrackBox.TrackHeaderBox.Height = 288;
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
            avc1.AVCConfigurationBox.AVCLevelIndication = 20;
            avc1.AVCConfigurationBox.AVCProfileIndication = 77;
            avc1.AVCConfigurationBox.PPSs = new List<byte[]>() { ppsNALU.RawData };
            avc1.AVCConfigurationBox.SPSs = new List<byte[]>() { spsNALU.RawData };
            avc1.AVCConfigurationBox.ProfileCompatibility = 0;
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

            //fragment moof n
            List<FragmentBox> moofs = new List<FragmentBox>();
            ulong moofOffset = (ulong)writer.GetCurrentPosition();
            FragmentBox fragmentBox = new FragmentBox();
            fragmentBox.MovieFragmentBox = new MovieFragmentBox();
            fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
            fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = 1;
            fragmentBox.MovieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox(0x39);
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = 1;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.BaseDataOffset = moofOffset;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = 48000;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = (uint)jT1078Package.Bodies.Length;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = 0x1010000;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = 0;
            //trun
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(flags: 0x5);
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = 0;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = new List<TrackRunBox.TrackRunInfo>();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Add(new TrackRunBox.TrackRunInfo());
            fragmentBox.MediaDataBox = new MediaDataBox();
            fragmentBox.MediaDataBox.Data = nalus.Select(s => s.RawData).ToList();
            moofs.Add(fragmentBox);
            foreach (var moof in moofs)
            {
                moof.ToBuffer(ref writer);
            }
            //mfra
            MovieFragmentRandomAccessBox movieFragmentRandomAccessBox = new MovieFragmentRandomAccessBox();
            //mfra->tfra
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox = new TrackFragmentRandomAccessBox(1);
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox.TrackID = 0x01;
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfos = new List<TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfo>();
            TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfo trackFragmentRandomAccessInfo1 = new TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfo();
            trackFragmentRandomAccessInfo1.Time = 0;
            trackFragmentRandomAccessInfo1.MoofOffset = moofOffset;
            trackFragmentRandomAccessInfo1.TrafNumber = 0x01;
            trackFragmentRandomAccessInfo1.TrunNumber = 0x01;
            trackFragmentRandomAccessInfo1.SampleNumber = 0x01;
            movieFragmentRandomAccessBox.TrackFragmentRandomAccessBox.TrackFragmentRandomAccessInfos.Add(trackFragmentRandomAccessInfo1);
            //mfra->mfro
            movieFragmentRandomAccessBox.MovieFragmentRandomAccessOffsetBox = new MovieFragmentRandomAccessOffsetBox(0);
            movieFragmentRandomAccessBox.ToBuffer(ref writer);
            var buffer = writer.FlushAndGetArray();

            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_2.mp4");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.Write(buffer);
            fileStream.Close();
        }

        [Fact]
        public void Test3()
        {
            H264Decoder decoder = new H264Decoder();
            var packages = ParseNALUTests();
            //10M
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(new byte[10 * 1024 * 1024]);
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.mp4");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            var jT1078Package = packages.FirstOrDefault();
            var nalus = decoder.ParseNALU(jT1078Package);
            var spsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.SPS);
            //SPS
            spsNALU.RawData = decoder.DiscardEmulationPreventionBytes(spsNALU.RawData);
            var ppsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == NalUnitType.PPS);
            ppsNALU.RawData = decoder.DiscardEmulationPreventionBytes(ppsNALU.RawData);
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
            //fragment moof n
            foreach (var package in packages)
            {
                ulong moofOffset = (ulong)writer.GetCurrentPosition();
                var package_nalus = decoder.ParseNALU(package);
                FragmentBox fragmentBox = new FragmentBox();
                fragmentBox.MovieFragmentBox = new MovieFragmentBox();
                fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
                fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = package.SN;
                fragmentBox.MovieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox(0x39);
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = 1;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.BaseDataOffset = moofOffset;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleDuration = 48000;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleSize = (uint)package.Bodies.Length;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.DefaultSampleFlags = 0x1010000;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = package.Timestamp * 1000;
                //trun
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox(flags: 0x5);
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.FirstSampleFlags = 0;
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos = new List<TrackRunBox.TrackRunInfo>();
                fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox.TrackRunInfos.Add(new TrackRunBox.TrackRunInfo());
                fragmentBox.MediaDataBox = new MediaDataBox();
                fragmentBox.MediaDataBox.Data = package_nalus.Select(s => s.RawData).ToList();
                fragmentBox.ToBuffer(ref writer);
            }
            var buffer = writer.FlushAndGetArray();
            fileStream.Write(buffer);
            fileStream.Close();
        }

        [Fact]
        public void Test4()
        {
            FMp4Encoder fMp4Encoder = new FMp4Encoder();
            H264Decoder h264Decoder = new H264Decoder();
            var packages = ParseNALUTests1();
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_7_3.mp4");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);

            var ftyp = fMp4Encoder.FtypBox();
            fileStream.Write(ftyp);

            var iPackage = packages.FirstOrDefault(f => f.Label3.DataType == JT1078DataType.视频I帧);
            var iNalus = h264Decoder.ParseNALU(iPackage);
            //判断第一帧是否关键帧
            var moov = fMp4Encoder.MoovBox(
                iNalus.FirstOrDefault(f => f.NALUHeader.NalUnitType == NalUnitType.SPS),
                iNalus.FirstOrDefault(f => f.NALUHeader.NalUnitType == NalUnitType.PPS));
            fileStream.Write(moov);
            List<JT1078Package> tmp = new List<JT1078Package>();
            foreach (var package in packages)
            {
                if (package.Label3.DataType == Protocol.Enums.JT1078DataType.视频I帧)
                {
                    if (tmp.Count>0)
                    {
                        fileStream.Write(fMp4Encoder.StypBox());
                        var otherBuffer = fMp4Encoder.OtherVideoBox(tmp);
                        fileStream.Write(otherBuffer);
                        tmp.Clear();
                    }
                }
                tmp.Add(package);
            }
            fileStream.Close();
        }

        [Fact]
        public void Test4_2()
        {
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_7.h264");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            List<JT1078Package> packages = new List<JT1078Package>();
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_6.txt"));
            foreach (var line in lines)
            {
                var bytes = line.ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                fileStream.Write(package.Bodies);
            }
            fileStream.Close();
        }
              
        [Fact]
        public void Test4_4()
        {
            uint a = uint.MaxValue;
            var b = a + 1;

            FMp4Encoder fMp4Encoder = new FMp4Encoder();
            H264Decoder h264Decoder = new H264Decoder();
            var packages = ParseNALUTests1();
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_7_4_4.mp4");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);

            var ftyp = fMp4Encoder.FtypBox();
            fileStream.Write(ftyp);

            var iPackage = packages.FirstOrDefault(f => f.Label3.DataType == JT1078DataType.视频I帧);
            var iNalus = h264Decoder.ParseNALU(iPackage);
            //判断第一帧是否关键帧
            var moov = fMp4Encoder.MoovBox(
                iNalus.FirstOrDefault(f => f.NALUHeader.NalUnitType == NalUnitType.SPS),
                iNalus.FirstOrDefault(f => f.NALUHeader.NalUnitType == NalUnitType.PPS));
            fileStream.Write(moov);

            Queue<Mp4Frame> mp4Frames = new Queue<Mp4Frame>();
            List<NalUnitType> filter = new List<NalUnitType>();
            filter.Add(NalUnitType.SEI);
            filter.Add(NalUnitType.PPS);
            filter.Add(NalUnitType.SPS);
            filter.Add(NalUnitType.AUD);
            foreach (var package in packages)
            {
                List<H264NALU> h264NALUs = h264Decoder.ParseNALU(package);
                if (h264NALUs != null && h264NALUs.Count > 0)
                {
                    Mp4Frame mp4Frame = new Mp4Frame
                    {
                        Key = package.GetKey(),
                        KeyFrame = package.Label3.DataType == JT1078DataType.视频I帧
                    };
                    mp4Frame.NALUs = h264NALUs;
                    mp4Frames.Enqueue(mp4Frame);
                }
            }
            while (mp4Frames.TryDequeue(out Mp4Frame frame))
            {
                fileStream.Write(fMp4Encoder.OtherVideoBox(frame.NALUs, frame.Key, frame.KeyFrame)); 
            }
            fileStream.Close();
        }

        class Mp4Frame
        {
            public string Key { get; set; }
            public bool KeyFrame { get; set; }
            public List<H264NALU> NALUs { get; set; }
        }

        [Fact]
        public void WebSocketMp4()
        {
            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_10.mp4");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
            System.Net.WebSockets.ClientWebSocket clientWebSocket = new System.Net.WebSockets.ClientWebSocket();
            clientWebSocket.ConnectAsync(new Uri("ws://127.0.0.1:8080/live/JT1078_7.live.mp4"), CancellationToken.None).GetAwaiter().GetResult();
            Task.Run(async() => 
            {
                while (true)
                {
                    var buffer = new byte[1024*1024];
                    var result = await clientWebSocket.ReceiveAsync(buffer, CancellationToken.None);
                    if (result.EndOfMessage)
                    {
                        await fileStream.WriteAsync(buffer.AsSpan(0, result.Count).ToArray());
                    }
                    else
                    {
                        await fileStream.WriteAsync(buffer);
                    }
                }
            });
            Thread.Sleep(100 * 1000);
        }

        [Fact]
        public void tkhd_width_height_test()
        {
            //01 60 00 00
            //01 20 00 00
            var a = BinaryPrimitives.ReadUInt32LittleEndian(new byte[] { 0x01, 0x60, 0, 0 });
            var b = BinaryPrimitives.ReadUInt32LittleEndian(new byte[] { 0x01, 0x20, 0, 0 });

            //00 00 01 60 
            //00 00 01 20
            var c = BinaryPrimitives.ReadUInt32BigEndian(new byte[] { 0, 0, 0x01, 0x20 });
            var d = BinaryPrimitives.ReadUInt32BigEndian(new byte[] { 0, 0, 0x01, 0x60 });

            var e = new byte[4];
            var f = new byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(e, 352);
            BinaryPrimitives.WriteUInt32LittleEndian(f, 288);
            //60 01 00 00
            //20 01 00 00
            var g = e.ToHexString();
            var h = f.ToHexString();
        }

        [Fact]
        public void tfdt_test()
        {
            var TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
            TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = 0;
            FMp4MessagePackWriter writer = new FMp4MessagePackWriter(new byte[65535]);
            TrackFragmentBaseMediaDecodeTimeBox.ToBuffer(ref writer);
            var buffer = writer.FlushAndGetArray().ToHexString();
        }

        [Fact]
        public void match_test()
        {
            var filepath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.mp4");
            var filepath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_1_fragmented.mp4");
            var byte1 = File.ReadAllBytes(filepath1);
            var byte2 = File.ReadAllBytes(filepath2);
            if (byte1.Length == byte2.Length)
            {
                for (var i = 0; i < byte1.Length; i++)
                {
                    if (byte1[i] != byte2[i])
                    {

                    }
                }
            }
        }

        public JT1078Package ParseNALUTest()
        {
            JT1078Package Package = null;
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_1.txt"));
            int mergeBodyLength = 0;
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[6].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                Package = JT1078Serializer.Merge(package);
            }
            return Package;
        }

        public List<JT1078Package> ParseNALUTests()
        {
            List<JT1078Package> packages = new List<JT1078Package>();
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_3.txt"));
            int mergeBodyLength = 0;
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[6].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                var packageMerge = JT1078Serializer.Merge(package);
                if (packageMerge != null)
                {
                    packages.Add(packageMerge);
                }
            }
            return packages;
        }

        public List<JT1078Package> ParseNALUTests1()
        {
            List<JT1078Package> packages = new List<JT1078Package>();
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "jt1078_6.txt"));
            int mergeBodyLength = 0;
            foreach (var line in lines)
            {
                var bytes = line.ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                var packageMerge = JT1078Serializer.Merge(package);
                if (packageMerge != null)
                {
                    packages.Add(packageMerge);
                }
            }
            return packages;
        }
    }
}
