# waifu2x ncnn Vulkan - GUI Edition Reloaded (waifu2x-nVGE)
![Downloads](https://img.shields.io/github/downloads/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/total.svg)
[![GitHub (pre-)release](https://img.shields.io/github/release/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/all.svg)](https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases)

[Japanese README](https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/blob/master/README.md)

A GUI (graphical user interface) version of [waifu2x ncnn Vulkan](https://github.com/nihui/waifu2x-ncnn-vulkan) developed by nihui.  

It is an application designed to allow intuitive image conversion.  
The main point of this tool is to upscale images, but it can also upscale videos.  

**Download:**

[Release build](https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases)

This application has been published and can be run without the need to install a runtime.  

This application is published and can be run without runtime installation.  

[.NET Desktop Runtime 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)

If you have not already done so, please download the redistributable package from above and install it on your PC.  


## What is this?

It uses AI to keep the clarity of the image while allowing you to enlarge it nicely.  

例：  
**You can enlarge the image by clicking on it.**  

<kbd><img src="https://user-images.githubusercontent.com/59692068/124342813-b6824200-dc01-11eb-9603-7e5127ef55af.jpg" width="350"><br>**Original (640x800)**</kbd>
<kbd><img src="https://user-images.githubusercontent.com/59692068/124342863-2395d780-dc02-11eb-94e7-05e4ba0f8b6a.png" width="350"><br>**waifu2x (640x800, 2, CUnet)**</kbd>

<kbd><img src="https://user-images.githubusercontent.com/59692068/124342967-fa297b80-dc02-11eb-845f-90fb6236dd82.png" width="350"><br>**Nomal (1280x1600, Bilinear)**</kbd>
<kbd><img src="https://user-images.githubusercontent.com/59692068/124342972-0ca3b500-dc03-11eb-8ac5-ba8cf4b21ca6.png" width="350"><br>**Nomal (1280x1600, Lanczos3)**</kbd>

<kbd><img src="https://user-images.githubusercontent.com/59692068/124342976-14635980-dc03-11eb-8207-feb18e0e36de.png" width="350"><br>**waifu2x (1280x1600, 2, CUnet)**</kbd>
<kbd><img src="https://user-images.githubusercontent.com/59692068/124343761-07496900-dc09-11eb-8465-ca91b6839623.png" width="350"><br>**waifu2x (1280x1600, 2, RGB)**</kbd>

<kbd><img src="https://user-images.githubusercontent.com/59692068/124343766-0d3f4a00-dc09-11eb-8ed6-53f80b983930.png" width="350"><br>**waifu2x (1280x1600, 2, Photo)**</kbd>
<kbd><img src="https://user-images.githubusercontent.com/59692068/124343864-c4d45c00-dc09-11eb-97fd-337a68323bd4.png" width="350"><br>**waifu2x (1280x1600, 3, CUnet, TTA)**</kbd>

**Precautions for image upscaling**

If the denoising level is set too high, the details of the image may be crushed.  

When using TTA, the image index is improved, but at the cost of a longer upscaling time.  
Moreover, it is not possible to tell the difference when it is on or off, so it is recommended not to enable TTA.

**A note on video upscaling**

Although image upscaling is the main purpose, video upscaling is also possible.  
Please refer to the following comparison video.  

[Upscaling Comparison Video](https://www.youtube.com/embed/hU3T_Gu3Ehk)  

When upscaling video, frame splitting is first done using FFmpeg.  
When performing upscaling, a very large load is placed on the PC.  
If the upscaling is done on a PC with low specifications, it may take a huge amount of time.  

The time required for video upscaling depends on the time and resolution of the video to be upscaled.  
In other words, the longer the video to be upscaled, and the higher the resolution, the longer it will take.  

However, if the PC is equipped with a powerful GPU (graphics board), the upscaling time can be reduced.  
The upscaling speed depends largely on the performance of the GPU installed.  
Therefore, the higher the performance of the GPU, the faster the upscaling speed will be.  
We do not recommend using the CPU's built-in graphics (iGPU), but that does not mean it cannot be converted.  
However, processing with built-in graphics may take a lot of time.  
For this reason, the use of NVIDIA or AMD (RADEON) GPUs is recommended.  

**System Requirements**

OS: Windows 7 or higher 64bit PC  

There is no application for x86 (32bit).  
If you need one for x86, please build it yourself from C++ source.  

**Recommended**

OS: Windows 10 64bit  
CPU: Intel Core i3 or higher AMD Ryzen 3 or higher  
RAM: 8GB or higher  
GPU: NVIDIA GeForce GTX 1060 or higher  

## Other

**Development**

Microsoft Visual Studio 2022

**Supported Language**

- English
- 日本語
- Chinese

**Tools, utilities, etc. used**  

[FFmpeg](https://ffmpeg.org) - convert and stream audio and video.  
[OpenCVSharp](https://github.com/shimat/opencvsharp) - OpenCV wrapper for .NET  
[Magick.NET](https://github.com/dlemstra/Magick.NET) - The .NET library for ImageMagick  
[waifu2x-ncnn-vulkan](https://github.com/nihui/waifu2x-ncnn-vulkan) - waifu2x converter ncnn version  

## License

MIT