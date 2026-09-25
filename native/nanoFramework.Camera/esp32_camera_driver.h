#pragma once

#include <cstdint>
#include <cstddef>
#include "nanoFramework_Camera.h"

bool esp32_camera_init(CameraPins pins,
                       int xclkFreq,
                       int frameSize,
                       int pixelFormat,
                       int jpegQuality);

bool esp32_camera_capture(uint8_t** outBuffer, size_t* outLength);

void esp32_camera_free(uint8_t* buffer);
