# waifu2x ncnn Vulkan - GUI Edition Reloaded (waifu2x-nVGE)
![Downloads](https://img.shields.io/github/downloads/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/total.svg)
[![GitHub (pre-)release](https://img.shields.io/github/release/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/all.svg)](https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases)

[English README](https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/blob/master/README-en.md)

nihui氏開発の [waifu2x ncnn Vulkan](https://github.com/nihui/waifu2x-ncnn-vulkan) をGUI(グラフィカルユーザーインターフェース)化したもの。  

直感的に画像を変換できるようにすることを目指して作られたアプリケーションです。  
尚、画像を綺麗に拡大変換することが主な要点ですが、本ツールは画像だけでなく動画もアップスケーリングすることが可能です。  

**ダウンロード:**

[リリースビルド](https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases)

本アプリケーションは発行されているため、ランタイムのインストール不要で実行可能です。

※何らかの理由でポータブル版を使用する場合は、ランタイムのインストールが必要です。

[.NET Desktop Runtime 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)

まだインストールを行っていない場合、上記から再頒布可能パッケージをダウンロードし、PCにインストールしてください。  


## これは何？

AIを用いて画像の鮮明さを保ちつつ、綺麗に拡大できます。  

例：  
**※クリックすることで画像を拡大できます。**  

<kbd><img src="https://user-images.githubusercontent.com/59692068/124342813-b6824200-dc01-11eb-9603-7e5127ef55af.jpg" width="350"><br>**オリジナル (640x800)**</kbd>
<kbd><img src="https://user-images.githubusercontent.com/59692068/124342863-2395d780-dc02-11eb-94e7-05e4ba0f8b6a.png" width="350"><br>**waifu2x (640x800, 除去レベル2, CUnet)**</kbd>

<kbd><img src="https://user-images.githubusercontent.com/59692068/124342967-fa297b80-dc02-11eb-845f-90fb6236dd82.png" width="350"><br>**通常拡大 (1280x1600, バイリニア補間)**</kbd>
<kbd><img src="https://user-images.githubusercontent.com/59692068/124342972-0ca3b500-dc03-11eb-8ac5-ba8cf4b21ca6.png" width="350"><br>**通常拡大 (1280x1600, Lanczos3補間)**</kbd>

<kbd><img src="https://user-images.githubusercontent.com/59692068/124342976-14635980-dc03-11eb-8207-feb18e0e36de.png" width="350"><br>**waifu2xで拡大 (1280x1600, 除去レベル2, CUnet)**</kbd>
<kbd><img src="https://user-images.githubusercontent.com/59692068/124343761-07496900-dc09-11eb-8465-ca91b6839623.png" width="350"><br>**waifu2xで拡大 (1280x1600, 除去レベル2, RGB)**</kbd>

<kbd><img src="https://user-images.githubusercontent.com/59692068/124343766-0d3f4a00-dc09-11eb-8ed6-53f80b983930.png" width="350"><br>**waifu2xで拡大 (1280x1600, 除去レベル2, Photo)**</kbd>
<kbd><img src="https://user-images.githubusercontent.com/59692068/124343864-c4d45c00-dc09-11eb-97fd-337a68323bd4.png" width="350"><br>**waifu2xで拡大 (1280x1600, 除去レベル3, CUnet, TTA有効)**</kbd>

**画像アップスケーリングの注意点**

ノイズ除去レベルを高くしすぎると、画像の細部が潰れる恐れがあります。  

TTAを使用する場合、画像指数が向上しますがその代償としてアップスケーリング時間が長くなります。  
しかもオン/オフ時の違いがわからないので、TTAは有効にしないのがオススメです。

**動画アップスケーリングの注意点**

画像のアップスケーリングが主な目的ですが、動画のアップスケーリングも可能です。  
以下の比較動画を参照してください。  

[アップスケーリング比較動画](https://www.youtube.com/embed/hU3T_Gu3Ehk)  

動画のアップスケーリングを行う際、最初にFFmpegを使用してフレーム分割を行います。  
アップスケーリングを行う際は、PCに非常に大きな負荷が掛かります。  
スペックが低いPCでアップスケーリングを行う場合、膨大な時間が掛かる恐れがあります。  

動画アップスケーリングに掛かる時間は、アップスケーリングを行う動画の時間と解像度に左右されます。  
つまり、アップスケーリングを行う動画の時間が長ければ長いほど、また、高解像度であればあるほど、時間が掛かります。  

ただし、PCに強力なGPU(グラフィックボード)が搭載されている場合は、アップスケーリング時間を短縮することが可能です。  
アップスケーリングの速度は、搭載されているGPUの性能に大きく依存します。  
その為、GPUの性能が高ければ高いほどアップスケーリング速度は高速になります。  
CPU内臓のグラフィック(iGPU)の使用はお勧めしませんが、変換できないわけではありません。  
ただし、内臓グラフィックで処理を行った場合、膨大な時間が掛かる恐れがあります。  
その為、NVIDIA もしくは AMD (RADEON) 製GPUの使用が推薦されます。  

**システム条件**

OS: Windows 7 以上の 64bit PC  

x86(32bit)用アプリケーションはありません。 
x86用が必要な場合、C++ソースからご自身でビルドを行ってください。  

**推薦**

OS: Windows10 64bit  
CPU: Intel Core i3 またはそれ以上  AMD Ryzen 3 またはそれ以上  
RAM: 8GB またはそれ以上  
GPU: NVIDIA GeForce GTX 1060 またはそれ以上  

## その他

**開発**

Microsoft Visual Studio 2022

**Supported Language**

- English
- 日本語
- Chinese

**使用ツール、ユーティリティ等**  

[FFmpeg](https://ffmpeg.org) - convert and stream audio and video.  
[OpenCVSharp](https://github.com/shimat/opencvsharp) - OpenCV wrapper for .NET  
[Magick.NET](https://github.com/dlemstra/Magick.NET) - The .NET library for ImageMagick  
[waifu2x-ncnn-vulkan](https://github.com/nihui/waifu2x-ncnn-vulkan) - waifu2x converter ncnn version  

## ライセンス

MIT