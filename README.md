# JT1078

道路运输车辆卫星定位系统-视频通讯协议主要分为三大部分。

1. [设备终端到平台的通信也就是JT808]
2. [企业平台到政府监管的通信也就是JT809](#809Ext)
3. [设备终端上传的实时音视频流数据也就是视频服务器](#1078)  
3.1  [将1078的数据(h264)编码成FLV](#1078flv)  
3.2  [将1078的数据(h264)编码成HLS](#1078hls)  
3.3  [将1078的数据(h264)编码成FMp4](#1078fmp4)  
4. ***====音频部分暂未实现====***

[![MIT Licence](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/SmallChi/JT1078/blob/master/LICENSE)[![Github Build status](https://github.com/SmallChi/JT1078/workflows/.NET%20Core/badge.svg)]()

## NuGet安装

| Package Name| Version|Pre Version| Downloads|
| --- |---| --- | --- |
| Install-Package JT1078 | ![JT1078](https://img.shields.io/nuget/v/JT1078.svg) | ![JT1078](https://img.shields.io/nuget/vpre/JT1078.svg) |![JT1078](https://img.shields.io/nuget/dt/JT1078.svg) |
| Install-Package JT1078.Flv | ![JT1078.Flv](https://img.shields.io/nuget/v/JT1078.Flv.svg) |  ![JT1078.Flv](https://img.shields.io/nuget/vpre/JT1078.Flv.svg) |![JT1078.Flv](https://img.shields.io/nuget/dt/JT1078.Flv.svg) |
| Install-Package JT1078.Hls | ![JT1078.Hls](https://img.shields.io/nuget/v/JT1078.Hls.svg) | ![JT1078.Hls](https://img.shields.io/nuget/vpre/JT1078.Hls.svg) |![JT1078.Hls](https://img.shields.io/nuget/dt/JT1078.Hls.svg) |
| Install-Package JT1078.FMp4 | ![JT1078.FMp4](https://img.shields.io/nuget/v/JT1078.FMp4.svg) | ![JT1078.FMp4](https://img.shields.io/nuget/vpre/JT1078.FMp4.svg) |![JT1078.FMp4](https://img.shields.io/nuget/dt/JT1078.FMp4.svg) |

## <span id="1078">基于JT1078音视频流数据的RTP协议</span>

### 前提条件

1. 掌握进制转换：二进制转十六进制；
2. 掌握BCD编码、Hex编码；
3. 掌握各种位移、异或；
4. 掌握快速ctrl+c、ctrl+v；
5. 掌握Span\<T\>的基本用法
6. 掌握以上装逼技能，就可以开始搬砖了。

### 数据结构解析

| 帧头标识 | 标注1| 标注1| 包序号|SIM 卡号 |逻辑通道号| 标注3 | 时间戳 |Last I Frame Interval|Last Frame Interval|数据体长度|数据体
| :----: | :----:  | :----:  | :----:  | :----: | :----:  |:----:|:----:|:----: |:----: |:----: |:----: |
| FH_Flag  | Label1 | Label2 | SN | SIM |LogicChannelNumber|Label3|Timestamp|LastIFrameInterval|LastFrameInterval|DataBodyLength|Bodies

#### 标注1（Label1）

|RTP协议的版本号|填充标志|扩展标志|CSRC计数器|
| :----: | :----:  | :----:  | :----:  |
| V  | P | X | CC |

#### 标注2（Label2）

|标志位，确定是否完整数据帧的边界|负载类型|
| :----: | :----:  |
|  M  | PT |

#### 标注3（Label3）

|数据类型|分包处理标记|
| :----: | :----:  |
|  DataType  | SubpackageType |

> 1.参考JTT1078文档
> 2.参考RTP协议

### 举个栗子1

#### 1.组包

``` package

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
var hex = JT1078Serializer.Serialize(jT1078Package).ToHexString();
// 输出结果Hex：
//30 31 63 64 81 E2 10 88 01 12 34 56 78 10 01 10 00 00 01 6B B3 92 CA 7C 02 80 00 28 00 2E 00 00 00 01 61 E1 A2 BF 00 98 CF C0 EE 1E 17 28 34 07 78 8E 39 A4 03 FD DB D1 D5 46 BF B0 63 01 3F 59 AC 34 C9 7A 02 1A B9 6A 28 A4 2C 08
```

#### 2.手动解包

``` unpackage
1.原包：
30 31 63 64 81 E2 10 88 01 12 34 56 78 10 01 10 00 00 01 6B B3 92 CA 7C 02 80 00 28 00 2E 00 00 00 01 61 E1 A2 BF 00 98 CF C0 EE 1E 17 28 34 07 78 8E 39 A4 03 FD DB D1 D5 46 BF B0 63 01 3F 59 AC 34 C9 7A 02 1A B9 6A 28 A4 2C 08

2.拆解：
30 31 63 64              --帧头表示
81                       --Label1 =>10000001 V P X CC
E2                       --Label2 =>11100010 M PT
10 88                    --SN 包序号
01 12 34 56 78 10        --SIM卡号
01                       --逻辑通道号
10                       --Label3 =>数据类型 分包处理标记
00 00 01 6B B3 92 CA 7C  --时间戳
02 80                    --Last I Frame Interval
00 28                    --Last Frame Interval
00 2E                    --数据体长度
00 00 00 01 61 E1 A2 BF  --数据体
00 98 CF C0 EE 1E 17 28
34 07 78 8E 39 A4 03 FD
DB D1 D5 46 BF B0 63 01
3F 59 AC 34 C9 7A 02 1A
B9 6A 28 A4 2C 08
```

#### 3.程序解包

``` unpackage2
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
```

### 使用BenchmarkDotNet性能测试报告（只是玩玩，不能当真）

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT
  Job-SIQGUC : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT

Platform=AnyCpu  Server=False  Toolchain=.NET Core 3.1  

```
|            Method |      N |          Mean |        Error |       StdDev |      Gen 0 |  Gen 1 | Gen 2 |    Allocated |
|------------------ |------- |--------------:|-------------:|-------------:|-----------:|-------:|------:|-------------:|
|  **JT1078Serializer** |    **100** |     **183.51 us** |     **2.207 us** |     **2.064 us** |    **37.8418** |      **-** |     **-** |    **232.81 KB** |
| JT1078Deserialize |    100 |      35.88 us |     0.503 us |     0.420 us |    23.8037 |      - |     - |    146.09 KB |
|  **JT1078Serializer** |  **10000** |  **23,107.15 us** |   **196.882 us** |   **184.164 us** |  **3781.2500** |      **-** |     **-** |  **23281.25 KB** |
| JT1078Deserialize |  10000 |   3,620.54 us |    45.558 us |    40.386 us |  2382.8125 | 3.9063 |     - |  14609.38 KB |
|  **JT1078Serializer** | **100000** | **236,213.13 us** | **5,465.042 us** | **6,074.380 us** | **38000.0000** |      **-** |     **-** |  **232812.5 KB** |
| JT1078Deserialize | 100000 |  37,065.84 us |   665.875 us |   590.281 us | 23785.7143 |      - |     - | 146093.75 KB |

## <span id="1078flv">基于JT1078的Flv视频编码器</span>

### 前提条件

1. 掌握JT078解码；
2. 了解H264解码；
3. 掌握FLV编码；

由于网上资料比较多，自己也不擅长写文章，这边只是着重写一些在实际开发中需要注意的问题。

> 注意：目前仅支持H264编码的视频播放，主次码流的切换。由于多数设备厂商只支持一路通道只能存在主码流或者子码流，所以不考虑同时上传主次码流。

### 关注点

1. 在组包FLV的时候需要注意将解析的NALU值放入VideoTagsData中，格式:[NALU.Length 长度]+[NALU 值]...[NALU.Length 长度]+[NALU 值]可以存放多个NALU。

2. JT1078的属性大有用处。

| JT1078属性  | FLV属性 |
| :--- | :----|
|Timestamp|JT1078的Timestamp为FLv的Timestamp|
|DataType|JT1078的DataType为FLv的FrameType的值（判断是否为关键帧）|
|LastFrameInterval|JT1078的LastFrameInterval为FLv（B/P帧）的CompositionTime值|

## <span id="1078hls">基于JT1078的Hls视频编码器</span>

### 前提条件

1. 掌握JT078解码；
2. 了解H264解码；
3. 掌握TS编码；
4. 掌握Hls编码；

## <span id="1078fmp4">基于JT1078的FMp4视频编码器</span>

### 前提条件

1. 掌握JT078解码；
2. 了解H264解码；
3. 掌握FMp4编码；

### 使用BenchmarkDotNet性能测试报告（只是玩玩，不能当真）

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-8700K CPU 3.70GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT
  Job-GMXLRW : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT

Platform=AnyCpu  Server=False  Toolchain=.NET Core 3.1  

```
|          Method |      N |            Mean |        Error |       StdDev |       Gen 0 |     Gen 1 | Gen 2 |     Allocated |
|---------------- |------- |----------------:|-------------:|-------------:|------------:|----------:|------:|--------------:|
| **EXPGolombReader** |    **100** |        **11.23 us** |     **0.032 us** |     **0.025 us** |      **1.5259** |         **-** |     **-** |       **9.38 KB** |
|     H264Decoder |    100 |     1,218.74 us |    23.313 us |    27.752 us |    126.9531 |    1.9531 |     - |     786.72 KB |
|      FlvEncoder |    100 |       215.40 us |     3.245 us |     2.533 us |    249.0234 |    3.4180 |     - |    1528.91 KB |
| **EXPGolombReader** |  **10000** |     **1,170.19 us** |    **22.642 us** |    **25.167 us** |    **152.3438** |         **-** |     **-** |      **937.5 KB** |
|     H264Decoder |  10000 |   119,152.25 us |   955.118 us |   893.418 us |  12800.0000 |  200.0000 |     - |   78672.14 KB |
|      FlvEncoder |  10000 |    21,582.41 us |   587.627 us |   549.667 us |  24937.5000 |         - |     - |  152890.63 KB |
| **EXPGolombReader** | **100000** |    **11,687.72 us** |   **162.828 us** |   **152.309 us** |   **1515.6250** |         **-** |     **-** |       **9375 KB** |
|     H264Decoder | 100000 | 1,192,549.87 us | 7,656.632 us | 7,162.018 us | 128000.0000 | 3000.0000 |     - |  786718.75 KB |
|      FlvEncoder | 100000 |   216,951.31 us | 3,513.653 us | 2,934.059 us | 249333.3333 |         - |     - | 1528906.66 KB |
