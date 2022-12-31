using System;
using Xunit;
using JT808.Protocol.Extensions;
using JT1078.Protocol.Enums;
using System.Linq;
using System.IO;

namespace JT1078.Protocol.Test
{
    public class JT1078SerializerTest
    {
        [Fact]
        public void SerializeTest1()
        {
            JT1078Package jT1078Package = new JT1078Package();
            jT1078Package.Label1 = new JT1078Label1(0x81);
            jT1078Package.Label2 = new JT1078Label2(0xE2);
            jT1078Package.SN = 0x1088;
            jT1078Package.SIM = "11234567810";
            jT1078Package.LogicChannelNumber = 0x01;
            jT1078Package.Label3 = new JT1078Label3(0x10);
            jT1078Package.Timestamp = 1562085870204;
            jT1078Package.LastIFrameInterval = 0x0280;
            jT1078Package.LastFrameInterval = 0x0028;
            jT1078Package.Bodies = "00 00 00 01 61 E1 A2 BF 00 98 CF C0 EE 1E 17 28 34 07 78 8E 39 A4 03 FD DB D1 D5 46 BF B0 63 01 3F 59 AC 34 C9 7A 02 1A B9 6A 28 A4 2C 08".ToHexBytes();
            var hex=JT1078Serializer.Serialize(jT1078Package).ToHexString();
            Assert.Equal("30 31 63 64 81 E2 10 88 01 12 34 56 78 10 01 10 00 00 01 6B B3 92 CA 7C 02 80 00 28 00 2E 00 00 00 01 61 E1 A2 BF 00 98 CF C0 EE 1E 17 28 34 07 78 8E 39 A4 03 FD DB D1 D5 46 BF B0 63 01 3F 59 AC 34 C9 7A 02 1A B9 6A 28 A4 2C 08".Replace(" ",""), hex);
        }

        [Fact]
        public void SerializeTest2()
        {
            JT1078Package jT1078Package = new JT1078Package();
            jT1078Package.Label1 = new JT1078Label1(0x81);
            jT1078Package.Label2 = new JT1078Label2(0x88);
            jT1078Package.SN = 0x10BA;
            jT1078Package.SIM = "11234567810";
            jT1078Package.LogicChannelNumber = 0x01;
            jT1078Package.Label3 = new JT1078Label3(0x30);
            jT1078Package.Timestamp = 1562085871501;
            jT1078Package.Bodies = "FE 6D 3B BE EF 3E 4E 7D FF B7 6D 5F F5 6F C7 BE 6F DF 77 DC DF 8E ED 3B 6F E3 3F B5 73 DF 6F EC F8 FD FF FE BE EF DB F7 6F DB BF FD D7 BF 6F FB 6F 6E F7 FF 5F DF BF D3 F7 8F FD FA B2 EF 3E F7 5F FF F1 5D 3F BF FB 26 BE ED C7 B7 7D 3F AE E3 FB EF 1D 3B AE 93 FE EF 7F DF 77 93 FE B6 65 3B BD FA E6 8E ED F8 F7 EF DB B1 FF C6 6F 7C FF EF FD DB 71 7F FF 6E EE 3E".ToHexBytes();
            var hex = JT1078Serializer.Serialize(jT1078Package).ToHexString();
            Assert.Equal("30 31 63 64 81 88 10 BA 01 12 34 56 78 10 01 30 00 00 01 6B B3 92 CF 8D 00 78 FE 6D 3B BE EF 3E 4E 7D FF B7 6D 5F F5 6F C7 BE 6F DF 77 DC DF 8E ED 3B 6F E3 3F B5 73 DF 6F EC F8 FD FF FE BE EF DB F7 6F DB BF FD D7 BF 6F FB 6F 6E F7 FF 5F DF BF D3 F7 8F FD FA B2 EF 3E F7 5F FF F1 5D 3F BF FB 26 BE ED C7 B7 7D 3F AE E3 FB EF 1D 3B AE 93 FE EF 7F DF 77 93 FE B6 65 3B BD FA E6 8E ED F8 F7 EF DB B1 FF C6 6F 7C FF EF FD DB 71 7F FF 6E EE 3E".Replace(" ", ""), hex);
        }

        [Fact]
        public void SerializeTest3()
        {
            JT1078Package jT1078Package = new JT1078Package();
            jT1078Package.Label1 = new JT1078Label1(0x81);
            jT1078Package.Label2 = new JT1078Label2(0xE2);
            jT1078Package.SN = 0x10BB;
            jT1078Package.SIM = "11234567810";
            jT1078Package.LogicChannelNumber = 0x01;
            jT1078Package.Label3 = new JT1078Label3(0x10);
            jT1078Package.LastIFrameInterval = 0x0730;
            jT1078Package.LastFrameInterval = 0x0028;
            jT1078Package.Timestamp = 1562085871404;
            jT1078Package.Bodies = "00 00 00 01 61 E4 62 BF 00 32 BE 88 82 3B 94 6F 41 EE 7C 28 7D 82 A5 9C 29 49 A8 4C BF".ToHexBytes();
            var hex = JT1078Serializer.Serialize(jT1078Package).ToHexString();
            Assert.Equal("30 31 63 64 81 E2 10 BB 01 12 34 56 78 10 01 10 00 00 01 6B B3 92 CF 2C 07 30 00 28 00 1D 00 00 00 01 61 E4 62 BF 00 32 BE 88 82 3B 94 6F 41 EE 7C 28 7D 82 A5 9C 29 49 A8 4C BF".Replace(" ", ""), hex);
        }

        [Fact]
        public void SerializeTest4()
        {
            JT1078Package jT1078Package = new JT1078Package();
            jT1078Package.Label1 = new JT1078Label1(0x81);
            jT1078Package.Label2 = new JT1078Label2(0x88);
            jT1078Package.SN = 0x1135;
            jT1078Package.SIM = "11234567810";
            jT1078Package.LogicChannelNumber = 0x01;
            jT1078Package.Label3 = new JT1078Label3(0x30);
            jT1078Package.Timestamp = 1562085874181;
            jT1078Package.Bodies = "B7 6D FF EF 7D FB A9 9D FE A9 1F 37 77 F3 37 BE EF FB F7 FB FB BE 7D DF B7 FD FB 76 AF DE 77 65 C7 EF E3 FB BE FF DB 4E FF DB B7 63 FF EE EF D8 BE 1D 37 B7 7D E7 7D F3 C6 F7 FD F4 BE 1F F7 B7 55 FF 76 FC FE CE 7B FF B7 7D 3F F5 FF FE 76 6C DF FE 53 DB CF 6D FB BF FD DE B1 EF 3E 77 D3 3F 6E 9A BF BF FF DB F7 FD DB 7F 63 FF 6E EC F8 EE 1F FB FD 7F FB 7D 7C DB".ToHexBytes();
            var hex = JT1078Serializer.Serialize(jT1078Package).ToHexString();
            Assert.Equal("30 31 63 64 81 88 11 35 01 12 34 56 78 10 01 30 00 00 01 6B B3 92 DA 05 00 78 B7 6D FF EF 7D FB A9 9D FE A9 1F 37 77 F3 37 BE EF FB F7 FB FB BE 7D DF B7 FD FB 76 AF DE 77 65 C7 EF E3 FB BE FF DB 4E FF DB B7 63 FF EE EF D8 BE 1D 37 B7 7D E7 7D F3 C6 F7 FD F4 BE 1F F7 B7 55 FF 76 FC FE CE 7B FF B7 7D 3F F5 FF FE 76 6C DF FE 53 DB CF 6D FB BF FD DE B1 EF 3E 77 D3 3F 6E 9A BF BF FF DB F7 FD DB 7F 63 FF 6E EC F8 EE 1F FB FD 7F FB 7D 7C DB".Replace(" ", ""), hex);
        }

        [Fact]
        public void SerializeTest5()
        {
            JT1078Package jT1078Package = new JT1078Package();
            jT1078Package.Label1 = new JT1078Label1(0x81);
            jT1078Package.Label2 = new JT1078Label2(0x88);
            jT1078Package.SN = 0x1135;
            jT1078Package.SIM = "11234567810";
            jT1078Package.LogicChannelNumber = 0x01;
            jT1078Package.Label3 = new JT1078Label3(0x30);
            jT1078Package.Timestamp = 1562085874181;
            jT1078Package.Bodies = Enumerable.Range(0, 900).Select(s => (byte)(s + 1)).ToArray();
            var hex = JT1078Serializer.Serialize(jT1078Package).ToHexString();
        }

        [Fact]
        public void DeserializeTest1()
        {
            //30 31 63 64
            //81
            //E2
            //10 88
            //01 12 34 56 78 10
            //01
            //10
            //00 00 01 6B B3 92 CA 7C
            //02 80
            //00 28
            //00 2E
            //00 00 00 01 61 E1 A2 BF
            //00 98 CF C0 EE 1E 17 28
            //34 07 78 8E 39 A4 03 FD
            //DB D1 D5 46 BF B0 63 01
            //3F 59 AC 34 C9 7A 02 1A
            //B9 6A 28 A4 2C 08
            var bytes = "30 31 63 64 81 E2 10 88 01 12 34 56 78 10 01 10 00 00 01 6B B3 92 CA 7C 02 80 00 28 00 2E 00 00 00 01 61 E1 A2 BF 00 98 CF C0 EE 1E 17 28 34 07 78 8E 39 A4 03 FD DB D1 D5 46 BF B0 63 01 3F 59 AC 34 C9 7A 02 1A B9 6A 28 A4 2C 08".ToHexBytes();
            JT1078Package package = JT1078Serializer.Deserialize(bytes);
            Assert.Equal(0x81, package.Label1.ToByte());
            Assert.Equal(0xE2, package.Label2.ToByte());
            Assert.Equal(0x1088, package.SN);
            Assert.Equal("011234567810", package.SIM);
            Assert.Equal(0x01, package.LogicChannelNumber);
            Assert.Equal(0x10, package.Label3.ToByte());
            Assert.Equal((ulong)1562085870204, package.Timestamp);
            Assert.Equal(0x0280, package.LastIFrameInterval);
            Assert.Equal(0x0028, package.LastFrameInterval);
            Assert.Equal(0x002E, package.DataBodyLength);
            Assert.Equal("00 00 00 01 61 E1 A2 BF 00 98 CF C0 EE 1E 17 28 34 07 78 8E 39 A4 03 FD DB D1 D5 46 BF B0 63 01 3F 59 AC 34 C9 7A 02 1A B9 6A 28 A4 2C 08".ToHexBytes(), package.Bodies);
        }

        [Fact]
        public void DeserializeTest2()
        {
            //30 31 63 64
            //81 
            //88 
            //10 BA 
            //01 12 34 56 78 10
            //01 
            //30 
            //00 00 01 6B B3 92 CF 8D 
            //00 78 
            //FE 6D 3B BE EF 3E 4E 7D 
            //FF B7 6D 5F F5 6F C7 BE 
            //6F DF 77 DC DF 8E ED 3B 
            //6F E3 3F B5 73 DF 6F EC 
            //F8 FD FF FE BE EF DB F7  
            //6F DB BF FD D7 BF 6F FB 
            //6F 6E F7 FF 5F DF BF D3 
            //F7 8F FD FA B2 EF 3E F7 
            //5F FF F1 5D 3F BF FB 26  
            //BE ED C7 B7 7D 3F AE E3 
            //FB EF 1D 3B AE 93 FE EF 
            //7F DF 77 93 FE B6 65 3B 
            //BD FA E6 8E ED F8 F7 EF 
            //DB B1 FF C6 6F 7C FF EF  
            //FD DB 71 7F FF 6E EE 3E
            var bytes = "30 31 63 64 81 88 10 BA 01 12 34 56 78 10 01 30 00 00 01 6B B3 92 CF 8D 00 78 FE 6D 3B BE EF 3E 4E 7D FF B7 6D 5F F5 6F C7 BE 6F DF 77 DC DF 8E ED 3B 6F E3 3F B5 73 DF 6F EC F8 FD FF FE BE EF DB F7 6F DB BF FD D7 BF 6F FB 6F 6E F7 FF 5F DF BF D3 F7 8F FD FA B2 EF 3E F7 5F FF F1 5D 3F BF FB 26 BE ED C7 B7 7D 3F AE E3 FB EF 1D 3B AE 93 FE EF 7F DF 77 93 FE B6 65 3B BD FA E6 8E ED F8 F7 EF DB B1 FF C6 6F 7C FF EF FD DB 71 7F FF 6E EE 3E".ToHexBytes();
            JT1078Package package = JT1078Serializer.Deserialize(bytes);
            Assert.Equal(0x81, package.Label1.ToByte());
            Assert.Equal(0x88, package.Label2.ToByte());
            Assert.Equal(0x10BA, package.SN);
            Assert.Equal("011234567810", package.SIM);
            Assert.Equal(0x01, package.LogicChannelNumber);
            Assert.Equal(0x30, package.Label3.ToByte());
            Assert.Equal((ulong)1562085871501, package.Timestamp);
            Assert.Equal(0x0078, package.DataBodyLength);
            Assert.Equal("FE 6D 3B BE EF 3E 4E 7D FF B7 6D 5F F5 6F C7 BE 6F DF 77 DC DF 8E ED 3B 6F E3 3F B5 73 DF 6F EC F8 FD FF FE BE EF DB F7 6F DB BF FD D7 BF 6F FB 6F 6E F7 FF 5F DF BF D3 F7 8F FD FA B2 EF 3E F7 5F FF F1 5D 3F BF FB 26 BE ED C7 B7 7D 3F AE E3 FB EF 1D 3B AE 93 FE EF 7F DF 77 93 FE B6 65 3B BD FA E6 8E ED F8 F7 EF DB B1 FF C6 6F 7C FF EF FD DB 71 7F FF 6E EE 3E".ToHexBytes(), package.Bodies.ToArray());
        }

        [Fact]
        public void DeserializeTest3()
        {
            //30 31 63 64 
            //81 
            //E2 
            //10 BB 
            //01 12 34 56 78 10 
            //01 
            //10 
            //00 00 01 6B B3 92 CF 2C
            //07 30
            //00 28 
            //00 1D 
            //00 00 00 01 61 E4 62 BF 
            //00 32 BE 88 82 3B 94 6F 
            //41 EE 7C 28 7D 82 A5 9C 
            //29 49 A8 4C BF
            var bytes = "30 31 63 64 81 E2 10 BB 01 12 34 56 78 10 01 10 00 00 01 6B B3 92 CF 2C 07 30 00 28 00 1D 00 00 00 01 61 E4 62 BF 00 32 BE 88 82 3B 94 6F 41 EE 7C 28 7D 82 A5 9C 29 49 A8 4C BF".ToHexBytes();
            JT1078Package package = JT1078Serializer.Deserialize(bytes);
            Assert.Equal(0x81, package.Label1.ToByte());
            Assert.Equal(0xE2, package.Label2.ToByte());
            Assert.Equal(0x10BB, package.SN);
            Assert.Equal("011234567810", package.SIM);
            Assert.Equal(0x01, package.LogicChannelNumber);
            Assert.Equal(0x10, package.Label3.ToByte());
            Assert.Equal((ulong)1562085871404, package.Timestamp);
            Assert.Equal(0x0730, package.LastIFrameInterval);
            Assert.Equal(0x0028, package.LastFrameInterval);
            Assert.Equal(0x001D, package.DataBodyLength);
            Assert.Equal("00 00 00 01 61 E4 62 BF 00 32 BE 88 82 3B 94 6F 41 EE 7C 28 7D 82 A5 9C 29 49 A8 4C BF".ToHexBytes(), package.Bodies.ToArray());
        }

        [Fact]
        public void DeserializeTest4()
        {
            //30 31 63 64 
            //81 
            //88 
            //11 35 
            //01 12 34 56 78 10 
            //01 
            //30 
            //00 00 01 6B B3 92 DA 05
            //00 78
            //B7 6D FF EF 7D FB A9 9D 
            //FE A9 1F 37 77 F3 37 BE 
            //EF FB F7 FB FB BE 7D DF 
            //B7 FD FB 76 AF DE 77 65 
            //C7 EF E3 FB BE FF DB 4E 
            //FF DB B7 63 FF EE EF D8 
            //BE 1D 37 B7 7D E7 7D F3 
            //C6 F7 FD F4 BE 1F F7 B7 
            //55 FF 76 FC FE CE 7B FF 
            //B7 7D 3F F5 FF FE 76 6C 
            //DF FE 53 DB CF 6D FB BF 
            //FD DE B1 EF 3E 77 D3 3F 
            //6E 9A BF BF FF DB F7 FD 
            //DB 7F 63 FF 6E EC F8 EE 
            //1F FB FD 7F FB 7D 7C DB
            var bytes = "30 31 63 64 81 88 11 35 01 12 34 56 78 10 01 30 00 00 01 6B B3 92 DA 05 00 78 B7 6D FF EF 7D FB A9 9D FE A9 1F 37 77 F3 37 BE EF FB F7 FB FB BE 7D DF B7 FD FB 76 AF DE 77 65 C7 EF E3 FB BE FF DB 4E FF DB B7 63 FF EE EF D8 BE 1D 37 B7 7D E7 7D F3 C6 F7 FD F4 BE 1F F7 B7 55 FF 76 FC FE CE 7B FF B7 7D 3F F5 FF FE 76 6C DF FE 53 DB CF 6D FB BF FD DE B1 EF 3E 77 D3 3F 6E 9A BF BF FF DB F7 FD DB 7F 63 FF 6E EC F8 EE 1F FB FD 7F FB 7D 7C DB".ToHexBytes();
            JT1078Package package = JT1078Serializer.Deserialize(bytes);
            Assert.Equal(0x81, package.Label1.ToByte());
            Assert.Equal(0x88, package.Label2.ToByte());
            Assert.Equal(0x1135, package.SN);
            Assert.Equal("011234567810", package.SIM);
            Assert.Equal(0x01, package.LogicChannelNumber);
            Assert.Equal(0x30, package.Label3.ToByte());
            Assert.Equal((ulong)1562085874181, package.Timestamp);
            Assert.Equal(0x0078, package.DataBodyLength);
            Assert.Equal("B7 6D FF EF 7D FB A9 9D FE A9 1F 37 77 F3 37 BE EF FB F7 FB FB BE 7D DF B7 FD FB 76 AF DE 77 65 C7 EF E3 FB BE FF DB 4E FF DB B7 63 FF EE EF D8 BE 1D 37 B7 7D E7 7D F3 C6 F7 FD F4 BE 1F F7 B7 55 FF 76 FC FE CE 7B FF B7 7D 3F F5 FF FE 76 6C DF FE 53 DB CF 6D FB BF FD DE B1 EF 3E 77 D3 3F 6E 9A BF BF FF DB F7 FD DB 7F 63 FF 6E EC F8 EE 1F FB FD 7F FB 7D 7C DB".ToHexBytes(), package.Bodies.ToArray());
        }      

        [Fact]
        public void DeserializeTest6()
        {
            var bytes = "3031636481E20492001901305037031000000174922CB80F00A00050033D0000000161E042BF50E2C3B950968B6CB9B80E2CB3F68DBFA51A9C9DC3C0B85C5215C0A4DE5C1D48B72D760400BF88D5EA46D276C8682573C459CD33E8D98C4C77A96F366D636454DC62AFCD4491A670744247B727859C14ECE91C03013D37CFDDBE93CA8C5651863E47DE5231BE914029A04A9E1318D983DB115800C01FC961DBA05DA3D2CEF66761CA5BF7CFA1815429DDE0AF2CF527C251C2E190407D313A651895FAFA7A1AC2E4ABEFDD7E26F1B37D9C643A1AF302ACD77A8A74A34747BC333C89A2E02AD902137708251313D77A649A751EA81E48A341C44D895694F37D2BEC8B9B50A46B9C2CB9AE62DCE10BF7692D1151A3B55CA3310AC494CF095D6258A71F875ED8D9C54694B29C217F48E0C511ECB52951F6DF1B9CF9FB6CFCAEFF515645D66278A964D4CCF64D9999F82D639A77F3B79E91C1B9A1F0A156B712D52E9F810FA6B6885A788EB7C1EA6272660F108C5A3D0C5893BA0021B1A080EC158D77BE40DC92A2EBEF96469FC8B258F3EDF843859DD83861D1B27E72019DC5FBF97D2489554092C2DEEAECD15FE6C2455D8A8CDEB590707914C2D01DA003C84C9F04D45569655A05648C465EBE1E04B6B330F6DFE7DE4F0975ED884275D8D8931405E88DC9D54084F25DED012E175F61E5EDB56F308D36F046C127CC96B0CCACA3B96018A6354D15A6520901A4EBC699AA9DC5E45FF17C35FED9A14B4BA84457534FB636EE998990C7B9E6A44638FB51615A09CCB922B6DA47C5E3A45EA794E76514DFA76CBA2DEB5F947CB71557AFA34F6D9DD37F747FB4D37AD14EFBF5070831DDC59C78713DF1CA8A394636158E761A12D6B763645D220D356B8B48462AD7A0112C4A59BEF52AD70BB69907CDDB350ED2D389DB0B003ECEF40FACBB4A3F29409C51AAEAB4A21AA3C70DFEDD116B7534E5A938974B431BC46956D376D73C6A7A0B0E9FF6A4A006097D680C93E5BFA75B3D4D533EA83F93A2503A2F813D3DA037498B4F31270845B3807A61AF0E722EA9ABBF2C0A5DCEA564DE3F58CC1273A8E52CB675B26700AD24F2115823F2BF918279321628FDBAD3B8C0FA0C50536DF88022F6C3956EE0ACCBF31B602AEB0D1933A677EB40125C89FE2A61A05BA7DE296DAE20EEF74790B1405B9766FBF1A0FFFD03BA07D943B504FA0C568DA0".ToHexBytes();
            JT1078Package package = JT1078Serializer.Deserialize(bytes);
            Assert.Equal(0b10000001, package.Label1.ToByte());
            Assert.Equal(0b11100010, package.Label2.ToByte());
            Assert.Equal(1170u, package.SN);
            Assert.Equal("001901305037", package.SIM);
            Assert.Equal(0x03, package.LogicChannelNumber);
            Assert.Equal(0b00010000, package.Label3.ToByte());
            Assert.Equal((ulong)1600180238351, package.Timestamp);
            Assert.Equal(829u, package.DataBodyLength);
        }

        [Fact]
        public void Json1()
        {
            var bytes = "30 31 63 64 81 88 11 35 01 12 34 56 78 10 01 30 00 00 01 6B B3 92 DA 05 00 78 B7 6D FF EF 7D FB A9 9D FE A9 1F 37 77 F3 37 BE EF FB F7 FB FB BE 7D DF B7 FD FB 76 AF DE 77 65 C7 EF E3 FB BE FF DB 4E FF DB B7 63 FF EE EF D8 BE 1D 37 B7 7D E7 7D F3 C6 F7 FD F4 BE 1F F7 B7 55 FF 76 FC FE CE 7B FF B7 7D 3F F5 FF FE 76 6C DF FE 53 DB CF 6D FB BF FD DE B1 EF 3E 77 D3 3F 6E 9A BF BF FF DB F7 FD DB 7F 63 FF 6E EC F8 EE 1F FB FD 7F FB 7D 7C DB".ToHexBytes();
            string json = JT1078Serializer.Analyze(bytes);
        }

        [Fact]
        public void Json2()
        {
            var bytes = "30 31 63 64 81 E2 10 88 01 12 34 56 78 10 01 10 00 00 01 6B B3 92 CA 7C 02 80 00 28 00 2E 00 00 00 01 61 E1 A2 BF 00 98 CF C0 EE 1E 17 28 34 07 78 8E 39 A4 03 FD DB D1 D5 46 BF B0 63 01 3F 59 AC 34 C9 7A 02 1A B9 6A 28 A4 2C 08".ToHexBytes();
            string json = JT1078Serializer.Analyze(bytes);
        }

        [Fact]
        public void Label1Test1()
        {
            Assert.Equal(0x81, JT1078Package.DefaultLabel1.ToByte());
        }

        [Fact]
        public void Label1Test2()
        {
            Assert.Equal(223, new JT1078Label1(3, 0, 1, 15).ToByte());
            JT1078Label1 label1 = new JT1078Label1(223);
            Assert.Equal(3, label1.V);
            Assert.Equal(0, label1.P);
            Assert.Equal(1, label1.X);
            Assert.Equal(15, label1.CC);
        }

        [Fact]
        public void Label1Test3()
        {
            Assert.Equal(127, new JT1078Label1(127).ToByte());
            Assert.Equal(89, new JT1078Label1(89).ToByte());
            Assert.Equal(254, new JT1078Label1(254).ToByte());
            Assert.Equal(2, new JT1078Label1(2).ToByte());
            Assert.Equal(10, new JT1078Label1(10).ToByte());
            Assert.Equal(9, new JT1078Label1(9).ToByte());
        }

        [Fact]
        public void Label2Test1()
        {
            JT1078Label2 label2 = new JT1078Label2(254);
            Assert.Equal(254, label2.ToByte());
            Assert.Equal(1, label2.M);
            Assert.Equal(126, (byte)label2.PT);
        }

        [Fact]
        public void Label2Test2()
        {
            JT1078Label2 label2 = new JT1078Label2(0, 28);
            Assert.Equal(28, label2.ToByte());
            Assert.Equal(0, label2.M);
            Assert.Equal(JT1078AVType.AMR, label2.PT);
        }

        [Fact]
        public void Label3Test1()
        {
            JT1078Label3 label3 = new JT1078Label3(34);
            Assert.Equal(34, label3.ToByte());
            Assert.Equal(JT1078DataType.视频B帧, label3.DataType);
            Assert.Equal(JT1078SubPackageType.分包处理时的最后一个包, label3.SubpackageType);
        }

        [Fact]
        public void Label3Test2()
        {
            JT1078Label3 label3 = new JT1078Label3(JT1078DataType.视频B帧, JT1078SubPackageType.分包处理时的最后一个包);
            Assert.Equal(34, label3.ToByte());
            Assert.Equal(JT1078DataType.视频B帧, label3.DataType);
            Assert.Equal(JT1078SubPackageType.分包处理时的最后一个包, label3.SubpackageType);
        }

        [Fact]
        public void MergeTest()
        {
            var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "h264","JT1078_1.txt"));
            JT1078Package merge=null;
            int mergeBodyLength=0;
            foreach (var line in lines)
            {
                var data = line.Split(',');
                var bytes = data[6].ToHexBytes();
                JT1078Package package = JT1078Serializer.Deserialize(bytes);
                mergeBodyLength += package.DataBodyLength;
                merge = JT1078Serializer.Merge(package,JT808ChannelType.Live);
            }
            Assert.NotNull(merge);
            Assert.Equal(mergeBodyLength, merge.Bodies.Length);
            Assert.Equal(JT1078SubPackageType.分包处理时的第一个包, merge.Label3.SubpackageType);
        }

    }
}
