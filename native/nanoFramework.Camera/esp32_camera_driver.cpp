#include "esp_camera.h"
#include "esp32_camera_driver.h"

bool esp32_camera_init(CameraPins pins,
                       int xclkFreq,
                       int frameSize,
                       int pixelFormat,
                       int jpegQuality)
{
    camera_config_t config = {};

    config.pin_d0 = pins.PinD0;
    config.pin_d1 = pins.PinD1;
    config.pin_d2 = pins.PinD2;
    config.pin_d3 = pins.PinD3;
    config.pin_d4 = pins.PinD4;
    config.pin_d5 = pins.PinD5;
    config.pin_d6 = pins.PinD6;
    config.pin_d7 = pins.PinD7;

    config.pin_xclk = pins.PinXclk;
    config.pin_pclk = pins.PinPclk;
    config.pin_vsync = pins.PinVsync;
    config.pin_href = pins.PinHref;

    config.pin_sccb_sda = pins.PinSccbSda;
    config.pin_sccb_scl = pins.PinSccbScl;

    config.pin_pwdn = pins.PinPwdn;
    config.pin_reset = pins.PinReset;

    config.xclk_freq_hz = xclkFreq;
    config.pixel_format = (pixformat_t)pixelFormat;
    config.frame_size = (framesize_t)frameSize;
    config.jpeg_quality = jpegQuality;
    config.fb_count = 1;

    return esp_camera_init(&config) == ESP_OK;
}

bool esp32_camera_capture(uint8_t** outBuffer, size_t* outLength)
{
    camera_fb_t* fb = esp_camera_fb_get();
    if (!fb)
    {
        return false;
    }

    *outBuffer = (uint8_t*)malloc(fb->len);
    if (!*outBuffer)
    {
        esp_camera_fb_return(fb);
        return false;
    }

    memcpy(*outBuffer, fb->buf, fb->len);
    *outLength = fb->len;

    esp_camera_fb_return(fb);
    return true;
}

void esp32_camera_free(uint8_t* buffer)
{
    if (buffer)
    {
        free(buffer);
    }
}
