using JT1078.FMp4.Enums;
using JT1078.FMp4.MessagePack;
using JT1078.FMp4.Samples;
using JT1078.Protocol;
using JT1078.Protocol.Extensions;
using JT1078.Protocol.H264;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            var spsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == 7);
            //SPS
            spsNALU.RawData = decoder.DiscardEmulationPreventionBytes(spsNALU.RawData);
            var ppsNALU = nalus.FirstOrDefault(n => n.NALUHeader.NalUnitType == 8);
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
            movieBox.MovieHeaderBox = new MovieHeaderBox(1, 0);
            movieBox.MovieHeaderBox.CreationTime = 2;
            movieBox.MovieHeaderBox.ModificationTime = 3;
            //var upperWordDuration = Math.floor(duration / (UINT32_MAX + 1));
            //var lowerWordDuration = Math.floor(duration % (UINT32_MAX + 1));
            movieBox.MovieHeaderBox.Duration = 1;
            movieBox.MovieHeaderBox.Timescale = 1000;
            movieBox.TrackBox = new TrackBox();
            movieBox.TrackBox.TrackHeaderBox = new TrackHeaderBox(1, 7);
            movieBox.TrackBox.TrackHeaderBox.CreationTime = 2;
            movieBox.TrackBox.TrackHeaderBox.ModificationTime = 3;
            movieBox.TrackBox.TrackHeaderBox.TrackID = movieBox.MovieHeaderBox.NextTrackID;
            //duration = track.duration * track.timescale,
            //upperWordDuration = Math.floor(duration / (UINT32_MAX + 1)),
            //lowerWordDuration = Math.floor(duration % (UINT32_MAX + 1));
            movieBox.TrackBox.TrackHeaderBox.Duration = 0;
            movieBox.TrackBox.TrackHeaderBox.TrackIsAudio = false;
            movieBox.TrackBox.TrackHeaderBox.Width = 352;
            movieBox.TrackBox.TrackHeaderBox.Height = 288;
            movieBox.TrackBox.MediaBox = new MediaBox();
            movieBox.TrackBox.MediaBox.MediaHeaderBox = new MediaHeaderBox();
            //duration *= timescale;
            //var upperWordDuration = Math.floor(duration / (UINT32_MAX + 1));
            //var lowerWordDuration = Math.floor(duration % (UINT32_MAX + 1));
            movieBox.TrackBox.MediaBox.MediaHeaderBox.CreationTime = 2;
            movieBox.TrackBox.MediaBox.MediaHeaderBox.ModificationTime = 3;
            movieBox.TrackBox.MediaBox.MediaHeaderBox.Timescale = 1000;
            movieBox.TrackBox.MediaBox.MediaHeaderBox.Duration = 1;
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
            movieBox.TrackBox.MediaBox.MediaInformationBox.SampleTableBox.SampleDescriptionBox = new SampleDescriptionBox(movieBox.TrackBox.MediaBox.HandlerBox.HandlerType);
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
            trex.TrackID = movieBox.MovieHeaderBox.NextTrackID;
            trex.DefaultSampleDescriptionIndex = 1;
            trex.DefaultSampleDuration = 0;
            trex.DefaultSampleSize = 0;
            trex.DefaultSampleFlags = 1;
            movieBox.MovieExtendsBox.TrackExtendsBoxs.Add(trex);
            //fragment moof n
            List<FragmentBox> moofs = new List<FragmentBox>();
            FragmentBox fragmentBox = new FragmentBox();
            fragmentBox.MovieFragmentBox = new MovieFragmentBox();
            fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox = new MovieFragmentHeaderBox();
            fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox.SequenceNumber = 0;
            fragmentBox.MovieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = movieBox.MovieHeaderBox.NextTrackID;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.SampleDependencyTypeBox = new SampleDependencyTypeBox();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.SampleDependencyTypeBox.SampleDependencyTypes = new List<SampleDependencyTypeBox.SampleDependencyType>();
            //todo:fragmentBox.MovieFragmentBox.TrackFragmentBox.SampleDependencyTypeBox.SampleDependencyTypes
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
            //upperWordBaseMediaDecodeTime = Math.floor(baseMediaDecodeTime / (UINT32_MAX + 1)),
            //lowerWordBaseMediaDecodeTime = Math.floor(baseMediaDecodeTime % (UINT32_MAX + 1));
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime = 0;
            //trun
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox();
            fragmentBox.MediaDataBox = new MediaDataBox();
            var nalUnitTypes = new List<int>() { 7, 8 };
            fragmentBox.MediaDataBox.Data = nalus
                        .Where(w => !nalUnitTypes.Contains(w.NALUHeader.NalUnitType))
                        .Select(s => s.RawData)
                        .SelectMany(a => a)
                        .ToArray();
            moofs.Add(fragmentBox);


            //mfra

            FMp4Box fMp4Box = new FMp4Box();
            fMp4Box.FileTypeBox = fileTypeBox;
            fMp4Box.MovieBox = movieBox;
            fMp4Box.FragmentBoxs = moofs;
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
    }
}
