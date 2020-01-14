# JT1078

道路运输车辆卫星定位系统-视频通讯协议主要分为三大部分。

1. [设备终端到平台的通信也就是JT808](#808Ext)
2. [企业平台到政府监管的通信也就是JT809](#809Ext)
3. [设备终端上传的实时音视频流数据也就是视频服务器](#1078)  
3.1  [将1078的数据(h264)编码成FLV](#1078flv)    
3.2  [将1078的数据(h264)编码成HLS(尚未实现)](#1078HLS)  

[![MIT Licence](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/SmallChi/JT1078/blob/master/LICENSE)[![Build Status](https://travis-ci.org/SmallChi/JT1078.svg?branch=master)](https://travis-ci.org/SmallChi/JT1078)

## NuGet安装

| Package Name          | Version                                            | Downloads                                           |
| --------------------- | -------------------------------------------------- | --------------------------------------------------- |
| Install-Package JT1078 | ![JT1078](https://img.shields.io/nuget/v/JT1078.svg) | ![JT1078](https://img.shields.io/nuget/dt/JT1078.svg) |
| Install-Package JT1078.Flv | ![JT1078.Flv](https://img.shields.io/nuget/v/JT1078.Flv.svg) | ![JT1078.Flv](https://img.shields.io/nuget/dt/JT1078.Flv.svg) |
| Install-Package JT808.Protocol.Extensions.JT1078 | ![JT808.Protocol.Extensions.JT1078](https://img.shields.io/nuget/v/JT808.Protocol.Extensions.JT1078.svg) | ![JT808](https://img.shields.io/nuget/dt/JT808.Protocol.Extensions.JT1078.svg) |
| Install-Package JT809.Protocol.Extensions.JT1078 | ![JT809.Protocol.Extensions.JT1078](https://img.shields.io/nuget/v/JT809.Protocol.Extensions.JT1078.svg) | ![JT809](https://img.shields.io/nuget/dt/JT809.Protocol.Extensions.JT1078.svg) |

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
81                       --‭Label1 =>10000001‬ V P X CC
E2                       --Label2 =>‭11100010‬ M PT
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

## <span id="808ext">基于JT808扩展的JT1078消息协议</span>

### JT808扩展协议消息对照表

| 序号  | 消息ID        | 完成情况 | 测试情况 | 消息体名称                                     |
| :---: | :-----------: | :------: | :------: | :----------------------------:              |
| 1     | 0x0200_0x14        | √        | √        | 视频相关报警                            |
| 2     | 0x0200_0x15        | √        | √        | 视频信号丢失报警状态                     |
| 3     | 0x0200_0x16        | √        | √        | 视频信号遮挡报警状态                     |
| 4     | 0x0200_0x17        | √        | √        | 存储器故障报警状态                       |
| 5     | 0x0200_0x18        | √        | √        | 异常驾驶行为报警详细描述                  |
| 6     | 0x8103_0x0075        | √        | √        | 音视频参数设置                   |
| 7     | 0x8103_0x0076        | √        | √        | 音视频通道列表设置                       |
| 8     | 0x8103_0x0077        | √        | √        | 单独视频通道参数设置                       |
| 9     | 0x8103_0x0079        | √        | √        | 特殊报警录像参数设置                   |
| 10     | 0x8103_0x007A        | √        | √        | 视频相关报警屏蔽字                       |
| 11     | 0x8103_0x007B        | √        | √        | 图像分析报警参数设置                       |
| 12     | 0x8103_0x007C        | √        | √        | 终端休眠模式唤醒设置                   |
| 13     | 0x1003        | √        | √        | 终端上传音视频属性                            |
| 14     | 0x1005        | √        | √        | 终端上传乘客流量                     |
| 15     | 0x1205        | √        | √        | 终端上传音视频资源列表                     |
| 16     | 0x1206        | √        | √        | 文件上传完成通知                       |
| 17     | 0x9003        | √        | √        | 查询终端音视频属性                  |
| 18     | 0x9101        | √        | √        | 实时音视频传输请求                   |
| 19     | 0x9102        | √        | √        | 音视频实时传输控制                       |
| 20     | 0x9105        | √        | √        | 实时音视频传输状态通知                       |
| 21     | 0x9201        | √        | √        | 平台下发远程录像回放请求                   |
| 22     | 0x9202        | √        | √        | 平台下发远程录像回放控制                       |
| 23     | 0x9205        | √        | √        | 查询资源列表                       |
| 24     | 0x9206        | √        | √        | 文件上传指令                   |
| 25     | 0x9207        | √        | √        | 文件上传控制                            |
| 26     | 0x9301        | √        | √        | 云台旋转                     |
| 27     | 0x9302        | √        | √        | 云台调整焦距控制                     |
| 28     | 0x9303        | √        | √        | 云台调整光圈控制                       |
| 29     | 0x9304        | √        | √        | 云台雨刷控制                  |
| 30     | 0x9305        | √        | √        | 红外补光控制                   |
| 31     | 0x9306        | √        | √        | 云台变倍控制                       |

### 使用方法

```dotnet
IServiceCollection serviceDescriptors1 = new ServiceCollection();
serviceDescriptors1.AddJT808Configure()
                   .AddJT1078Configure();
```

## <span id="809ext">基于JT809扩展的JT1078消息协议</span>

### JT809扩展协议消息对照表

#### 主链路动态信息交换消息

| 序号  | 消息ID        | 完成情况 | 测试情况 | 消息体名称                                     |
| :---: | :-----------: | :------: | :------: | :----------------------------:              |
| 1     | 0x1700        | √        | √        | 主链路时效口令交互消息                            |
| 2     | 0x1700_0x1701        | √        | √        | 时效口令上报消息(有疑问:数据体有问题)                     |
| 3     | 0x1700_0x1702        | √        | √        | 时效口令请求消息                     |
| 4     | 0x1800        | √        | √        | 主链路实时音视频交互消息                       |
| 5     | 0x1800_0x1801        | √        | √        | 实时音视频请求应答消息                  |
| 6     | 0x1800_0x1802        | √        | √        | 主动请求停止实时音视频传输应答消息                   |
| 7     | 0x1900        | √        | √        | 主链路远程录像检索                       |
| 8     | 0x1900_0x1901        | √        | √        | 主动上传音视频资源目录信息消息                       |
| 9     | 0x1900_0x1902        | √        | √        | 查询音视频资源目录应答消息                   |
| 10     | 0x1A00        | √        | √        | 主链路远程录像回放交互消息                       |
| 11     | 0x1A00_0x1A01        | √        | √        | 远程录像回放请求应答消息                       |
| 12     | 0x1A00_0x1A02        | √        | √        | 远程录像回放控制应答消息                   |
| 13     | 0x1B00        | √        | √        | 主链路远程录像下载交互消息                            |
| 14     | 0x1B00_0x1B01        | √        | √        | 远程录像下载请求应答消息                     |
| 15     | 0x1B00_0x1B02        | √        | √        | 远程录像下载通知消息                     |
| 16     | 0x1B00_0x1B03        | √        | √        | 远程录像下载控制应答消息                       |

#### 从链路动态信息交换消息

| 序号  | 消息ID        | 完成情况 | 测试情况 | 消息体名称                                     |
| :---: | :-----------: | :------: | :------: | :----------------------------: |
| 17     | 0x9700        | √        | √        | 从链路时效口令交互消息                  |
| 18     | 0x9700_0x9702        | √        | √        | 时效口令请求应答消息(有疑问:应该有应答结果)                |
| 19     | 0x9800        | √        | √        | 从链路实时音视频交互信息                       |
| 20     | 0x9800_0x9801        | √        | √        | 实时音视频请求消息                       |
| 21     | 0x9800_0x9802        | √        | √        | 主动请求停止实时音视频传输消息                   |
| 22     | 0x9900        | √        | √        | 从链路远程录像检索交互消息                       |
| 23     | 0x9900_0x9901        | √        | √        | 主动上传音视频资源目录信息应答消息                       |
| 24     | 0x9900_0x9902        | √        | √        | 查询音视频资源目录请求消息                   |
| 25     | 0x9A00        | √        | √        | 从链路远程录像回放交互消息                            |
| 26     | 0x9A00_0x9A01        | √        | √        | 远程录像回放请求消息                     |
| 27     | 0x9A00_0x9A02        | √        | √        | 远程录像回放控制消息                     |
| 28     | 0x9B00        | √        | √        | 从链路远程录像下载交互消息                       |
| 29     | 0x9B00_0x9B01        | √        | √        | 远程录像下载请求消息                  |
| 30     | 0x9B00_0x9B02        | √        | √        | 远程录像下载完成通知应答消息                   |
| 31     | 0x9B00_0x9B03        | √        | √        | 远程录像下载控制消息                       |

### 使用方法

```dotnet
IServiceCollection serviceDescriptors1 = new ServiceCollection();
serviceDescriptors1.AddJT809Configure()
                   .AddJT1078Configure();
```
