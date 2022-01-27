using System.Collections.Generic;

namespace NVGE.Settings
{
    internal class Image
    {
        public int Reduction { get; set; }
        public int Scale { get; set; }
        public int GPU { get; set; }
        public int Blocksize { get; set; }
        public bool Advanced { get; set; }
        public int Thread { get; set; }
        public int Format { get; set; }
        public int Model { get; set; }
        public bool Verbose { get; set; }
        public bool TTA { get; set; }
        public string Param { get; set; }
    }

    internal class Video
    {
        public string FPS { get; set; }
        public string FFmpegLocation { get; set; }
        public int Advanced { get; set; }
        public int InternalAAC { get; set; }
        public int Subtitle { get; set; }
        public int CopyingChapters { get; set; }
        public int HideInformations { get; set; }
        public int Overwrite { get; set; }
        public int OutputAudioOnly { get; set; }
        public int Metadata { get; set; }
        public int DataStream { get; set; }
        public int Sequential { get; set; }
        public int NVENC { get; set; }
        public string CRFLevel { get; set; }
        public int VideoCodec { get; set; }
        public string VideoCodecIndex { get; set; }
        public int VideoGeneration { get; set; }
        public int GenerationIndex { get; set; }
        public int AudioCodec { get; set; }
        public string AudioCodecIndex { get; set; }
        public int AudioOutputCodec { get; set; }
        public int OutputCodecIndex { get; set; }
        public int AudioBitrate { get; set; }
        public string AudioBitrateIndex { get; set; }
        public int H264 { get; set; }
        public int H264Index { get; set; }
        public int OutputLocation { get; set; }
        public string VideoLocation { get; set; }
        public string AudioLocation { get; set; }
        public string VideoParam { get; set; }
        public string AudioParam { get; set; }
        public string MergeParam { get; set; }
    }

    internal class Others
    {
        public string FFmpegVersion { get; set; }
    }
}
