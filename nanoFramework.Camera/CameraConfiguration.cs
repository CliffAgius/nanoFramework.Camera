using System;

namespace nanoFramework.Camera
{
    /// <summary>
    /// Defines the OV2640 camera bus and JPEG capture settings.
    /// </summary>
    public sealed class CameraConfiguration
    {
        public int PinPowerDown { get; set; }

        public int PinReset { get; set; }

        public int PinXclk { get; set; }

        public int PinSiod { get; set; }

        public int PinSioc { get; set; }

        public int PinD7 { get; set; }

        public int PinD6 { get; set; }

        public int PinD5 { get; set; }

        public int PinD4 { get; set; }

        public int PinD3 { get; set; }

        public int PinD2 { get; set; }

        public int PinD1 { get; set; }

        public int PinD0 { get; set; }

        public int PinVsync { get; set; }

        public int PinHref { get; set; }

        public int PinPclk { get; set; }

        public int XclkFrequencyHz { get; set; }

        public CameraFrameSize FrameSize { get; set; }

        public int JpegQuality { get; set; }

        public int FrameBufferCount { get; set; }

        /// <summary>
        /// Creates settings for an AI Thinker ESP32-CAM with an OV2640 module.
        /// </summary>
        public static CameraConfiguration CreateAiThinkerOv2640()
        {
            return new CameraConfiguration
            {
                PinPowerDown = 32,
                PinReset = -1,
                PinXclk = 0,
                PinSiod = 26,
                PinSioc = 27,
                PinD7 = 35,
                PinD6 = 34,
                PinD5 = 39,
                PinD4 = 36,
                PinD3 = 21,
                PinD2 = 19,
                PinD1 = 18,
                PinD0 = 5,
                PinVsync = 25,
                PinHref = 23,
                PinPclk = 22,
                XclkFrequencyHz = 20000000,
                FrameSize = CameraFrameSize.Vga,
                JpegQuality = 12,
                FrameBufferCount = 1
            };
        }

        internal void Validate()
        {
            if (XclkFrequencyHz <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(XclkFrequencyHz));
            }

            if ((int)FrameSize < (int)CameraFrameSize.Qqvga ||
                (int)FrameSize > (int)CameraFrameSize.Uxga)
            {
                throw new ArgumentOutOfRangeException(nameof(FrameSize));
            }

            if (JpegQuality < 0 || JpegQuality > 63)
            {
                throw new ArgumentOutOfRangeException(nameof(JpegQuality));
            }

            if (FrameBufferCount < 1 || FrameBufferCount > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(FrameBufferCount));
            }
        }
    }
}
