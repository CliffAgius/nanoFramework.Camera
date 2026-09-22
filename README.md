# nanoFramework.Camera

A nanoFramework camera library with a managed C# API and a matching native firmware assembly. The first supported hardware is the AI Thinker ESP32-CAM with an OV2640 sensor and PSRAM.

## Managed usage

Install `nanoFramework.Camera` in the nanoFramework application, then use the AI Thinker defaults:

```csharp
using nanoFramework.Camera;

using (var camera = new Camera())
{
	camera.Init();
	byte[] jpeg = camera.GetJpeg();

	// Save or transmit jpeg before capturing another large frame.
}
```

Settings can be changed before constructing the camera:

```csharp
CameraConfiguration settings = CameraConfiguration.CreateAiThinkerOv2640();
settings.FrameSize = CameraFrameSize.Qvga;
settings.JpegQuality = 12;
settings.FrameBufferCount = 1;

using (var camera = new Camera(settings))
{
	camera.Init();
	byte[] jpeg = camera.GetJpeg();
}
```

A lower JPEG quality number produces a higher-quality, usually larger image. Valid values are 0 through 63.

## Firmware requirement

The managed NuGet package does not install native code into an already-flashed device. Firmware must contain the native assembly whose name, version, methods, and checksum match the managed assembly.

The native sources are in `native/nanoFramework.Camera/nanoFramework.Camera`. They were generated from the managed assembly and currently use native checksum `0x85DA61A2` and version `1.0.0.0`.

To add the assembly to an ESP32 nanoFramework firmware build:

1. Copy the contents of `native/nanoFramework.Camera/nanoFramework.Camera` into `InteropAssemblies/nanoFramework.Camera` in the nanoFramework firmware source tree.
2. Copy `FindINTEROP-nanoFramework.Camera.cmake` into the firmware tree's CMake modules directory.
3. Add `nanoFramework.Camera` to the target's `NF_INTEROP_ASSEMBLIES` list.
4. Make the Espressif `esp32-camera` component available to the ESP-IDF build. The implementation includes `esp_camera.h` and links through that component.
5. Build the firmware for an ESP32 target with PSRAM enabled and flash it to the AI Thinker ESP32-CAM.
6. Deploy the managed application referencing the matching `nanoFramework.Camera` package.

The exact target CMake file and ESP-IDF component location depend on the nanoFramework firmware branch. Keep the native assembly in `InteropAssemblies` and the camera driver in an ESP-IDF component search path.

## AI Thinker pin map

| Signal | GPIO |
| --- | ---: |
| PWDN | 32 |
| RESET | -1 |
| XCLK | 0 |
| SIOD | 26 |
| SIOC | 27 |
| D7-D0 | 35, 34, 39, 36, 21, 19, 18, 5 |
| VSYNC | 25 |
| HREF | 23 |
| PCLK | 22 |

## Memory considerations

`GetJpeg()` copies the ESP-IDF frame buffer into a managed `byte[]` before returning the native frame to the camera driver. The nanoCLR managed heap therefore needs enough contiguous space for the complete JPEG. Start with `Qqvga` or `Qvga` while validating a firmware configuration, and avoid retaining multiple images.

## Current scope

- AI Thinker ESP32-CAM
- OV2640
- JPEG capture
- Synchronous single-frame API
- ESP-IDF `esp32-camera` driver

Raspberry Pi Pico and non-OV2640 transports require separate native drivers and are not implemented yet.
