using System;
using System.Diagnostics;
using System.Threading;
using nanoFramework.Camera;
using CameraDevice = nanoFramework.Camera.Camera;

namespace nanoFramework.Camera.TestApplication
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                CameraConfiguration configuration = CameraConfiguration.CreateAiThinkerOv2640();
                configuration.FrameSize = CameraFrameSize.Qqvga;
                configuration.JpegQuality = 20;
                configuration.FrameBufferCount = 1;

                using (var camera = new CameraDevice(configuration))
                {
                    Debug.WriteLine("Initializing camera...");
                    camera.Init();

                    Debug.WriteLine("Capturing JPEG...");
                    byte[] jpeg = camera.GetJpeg();

                    bool hasJpegMarkers =
                        jpeg.Length >= 4 &&
                        jpeg[0] == 0xFF &&
                        jpeg[1] == 0xD8 &&
                        jpeg[jpeg.Length - 2] == 0xFF &&
                        jpeg[jpeg.Length - 1] == 0xD9;

                    Debug.WriteLine("JPEG bytes: " + jpeg.Length);
                    Debug.WriteLine("Valid JPEG markers: " + hasJpegMarkers);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Camera test failed:");
                Debug.WriteLine(exception.ToString());
            }

            Thread.Sleep(-1);
        }
    }
}
