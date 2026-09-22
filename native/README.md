# Native firmware assembly

`nanoFramework.Camera/nanoFramework.Camera` contains the native assembly generated from the managed `nanoFramework.Camera` library and completed with an ESP-IDF OV2640 implementation.

The generated method table must stay synchronized with the managed assembly. After changing an `InternalCall` declaration or `AssemblyNativeVersion`, regenerate the stubs with the nanoFramework metadata processor, restore the ESP32 implementation if the generated implementation files are overwritten, and rebuild both sides.

The native implementation requires:

- an ESP32 target with PSRAM;
- the Espressif `esp32-camera` component;
- `esp_camera.h` on the include path;
- `nanoFramework.Camera` in the firmware `NF_INTEROP_ASSEMBLIES` list.

The current managed/native checksum is `0x85DA61A2`.
