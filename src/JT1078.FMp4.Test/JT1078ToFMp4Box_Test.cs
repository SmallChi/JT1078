using JT1078.FMp4.Enums;
using JT1078.FMp4.Samples;
using System;
using System.Collections.Generic;
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
            //ftyp
            FileTypeBox fileTypeBox = new FileTypeBox();
            fileTypeBox.MajorBrand = "isom";
            fileTypeBox.MinorVersion = "\0\0\0\u0001";
            fileTypeBox.CompatibleBrands.Add("isom");
            fileTypeBox.CompatibleBrands.Add("avc1");
            //moov
            MovieBox movieBox = new MovieBox();
            movieBox.MovieHeaderBox = new MovieHeaderBox(1, 0);
            movieBox.MovieHeaderBox.CreationTime = 2;
            movieBox.MovieHeaderBox.ModificationTime = 3;
            //var upperWordDuration = Math.floor(duration / (UINT32_MAX + 1));
            //var lowerWordDuration = Math.floor(duration % (UINT32_MAX + 1));
            //movieBox.MovieHeaderBox.Duration=
            //movieBox.MovieHeaderBox.Timescale=
            movieBox.TrackBox = new TrackBox();
            movieBox.TrackBox.TrackHeaderBox = new TrackHeaderBox(1, 7);
            movieBox.TrackBox.TrackHeaderBox.CreationTime = 2;
            movieBox.TrackBox.TrackHeaderBox.ModificationTime = 3;
            movieBox.TrackBox.TrackHeaderBox.TrackID = movieBox.MovieHeaderBox.NextTrackID;
            //duration = track.duration * track.timescale,
            //upperWordDuration = Math.floor(duration / (UINT32_MAX + 1)),
            //lowerWordDuration = Math.floor(duration % (UINT32_MAX + 1));
            //movieBox.TrackBox.TrackHeaderBox.Duration=
            //movieBox.TrackBox.TrackHeaderBox.Timescale=
            movieBox.TrackBox.TrackHeaderBox.TrackIsAudio = false;
            //movieBox.TrackBox.TrackHeaderBox.Width=
            //movieBox.TrackBox.TrackHeaderBox.Height=
            movieBox.TrackBox.MediaBox = new MediaBox();
            movieBox.TrackBox.MediaBox.MediaHeaderBox = new MediaHeaderBox();
            //duration *= timescale;
            //var upperWordDuration = Math.floor(duration / (UINT32_MAX + 1));
            //var lowerWordDuration = Math.floor(duration % (UINT32_MAX + 1));
            movieBox.TrackBox.MediaBox.MediaHeaderBox.CreationTime = 2;
            movieBox.TrackBox.MediaBox.MediaHeaderBox.ModificationTime = 3;
            //movieBox.TrackBox.MediaBox.MediaHeaderBox.Timescale=
            //movieBox.TrackBox.MediaBox.MediaHeaderBox.Duration=
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
            //avc1.Width=
            //avc1.Height=
            //avc1.AVCConfigurationBox.AVCLevelIndication
            //avc1.AVCConfigurationBox.AVCProfileIndication
            //avc1.AVCConfigurationBox.PPSs
            //avc1.AVCConfigurationBox.SPSs
            //avc1.AVCConfigurationBox.ProfileCompatibility
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
            //fragmentBox.MovieFragmentBox.MovieFragmentHeaderBox.SequenceNumber=SN
            fragmentBox.MovieFragmentBox.TrackFragmentBox = new TrackFragmentBox();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox = new TrackFragmentHeaderBox();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentHeaderBox.TrackID = movieBox.MovieHeaderBox.NextTrackID;
            fragmentBox.MovieFragmentBox.TrackFragmentBox.SampleDependencyTypeBox = new SampleDependencyTypeBox();
            fragmentBox.MovieFragmentBox.TrackFragmentBox.SampleDependencyTypeBox.SampleDependencyTypes = new List<SampleDependencyTypeBox.SampleDependencyType>();
            //todo:fragmentBox.MovieFragmentBox.TrackFragmentBox.SampleDependencyTypeBox.SampleDependencyTypes
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox = new TrackFragmentBaseMediaDecodeTimeBox();
            //upperWordBaseMediaDecodeTime = Math.floor(baseMediaDecodeTime / (UINT32_MAX + 1)),
            //lowerWordBaseMediaDecodeTime = Math.floor(baseMediaDecodeTime % (UINT32_MAX + 1));
            //fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackFragmentBaseMediaDecodeTimeBox.BaseMediaDecodeTime
            //trun
            fragmentBox.MovieFragmentBox.TrackFragmentBox.TrackRunBox = new TrackRunBox();

            moofs.Add(fragmentBox);




            //mfra


        }
    }
}
