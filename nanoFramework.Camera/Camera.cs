using System;
using System.Runtime.CompilerServices;

namespace nanoFramework.Camera
{
    /// <summary>
    /// Controls an OV2640 camera through the native nanoFramework camera assembly.
    /// </summary>
    public sealed class Camera : IDisposable
    {
        private readonly CameraConfiguration _configuration;
        private bool _isInitialized;

        public Camera()
            : this(CameraConfiguration.CreateAiThinkerOv2640())
        {
        }

        public Camera(CameraConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _configuration = configuration;
        }

        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// Initializes the camera hardware using the configured pins and capture settings.
        /// </summary>
        public void Init()
        {
            if (_isInitialized)
            {
                return;
            }

            _configuration.Validate();

            int error = NativeInit(
                _configuration.PinPowerDown,
                _configuration.PinReset,
                _configuration.PinXclk,
                _configuration.PinSiod,
                _configuration.PinSioc,
                _configuration.PinD7,
                _configuration.PinD6,
                _configuration.PinD5,
                _configuration.PinD4,
                _configuration.PinD3,
                _configuration.PinD2,
                _configuration.PinD1,
                _configuration.PinD0,
                _configuration.PinVsync,
                _configuration.PinHref,
                _configuration.PinPclk,
                _configuration.XclkFrequencyHz,
                (int)_configuration.FrameSize,
                _configuration.JpegQuality,
                _configuration.FrameBufferCount);

            if (error != 0)
            {
                throw new InvalidOperationException("Camera initialization failed with native error " + error + ".");
            }

            _isInitialized = true;
        }

        /// <summary>
        /// Captures one JPEG image and copies it into the managed heap.
        /// </summary>
        public byte[] GetJpeg()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("The camera has not been initialized.");
            }

            byte[] jpeg = NativeGetJpeg();
            if (jpeg == null || jpeg.Length == 0)
            {
                throw new InvalidOperationException("The camera did not return a JPEG frame.");
            }

            return jpeg;
        }

        public void Dispose()
        {
            if (!_isInitialized)
            {
                return;
            }

            NativeDeinit();
            _isInitialized = false;
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern int NativeInit(
            int pinPowerDown,
            int pinReset,
            int pinXclk,
            int pinSiod,
            int pinSioc,
            int pinD7,
            int pinD6,
            int pinD5,
            int pinD4,
            int pinD3,
            int pinD2,
            int pinD1,
            int pinD0,
            int pinVsync,
            int pinHref,
            int pinPclk,
            int xclkFrequencyHz,
            int frameSize,
            int jpegQuality,
            int frameBufferCount);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern byte[] NativeGetJpeg();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeDeinit();
    }
}
