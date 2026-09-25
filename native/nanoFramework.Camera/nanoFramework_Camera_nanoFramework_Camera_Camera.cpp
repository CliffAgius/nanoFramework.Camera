//-----------------------------------------------------------------------------
//
//                   ** WARNING! ** 
//    This file was generated automatically by a tool.
//    Re-running the tool will overwrite this file.
//    You should copy this file to a custom location
//    before adding any customization in the copy to
//    prevent loss of your changes when the tool is
//    re-run.
//
//-----------------------------------------------------------------------------

#include "nanoFramework_Camera.h"
#include "nanoFramework_Camera_nanoFramework_Camera_Camera.h"

#include <esp32_camera_driver.h>

using namespace nanoFramework_Camera::nanoFramework_Camera;

namespace
{
    bool s_cameraInitialized = false;

    framesize_t GetFrameSize(signed int frameSize)
    {
        switch (frameSize)
        {
            case CameraFrameSize_Qqvga:
                return FRAMESIZE_QQVGA;
            case CameraFrameSize_Qvga:
                return FRAMESIZE_QVGA;
            case CameraFrameSize_Vga:
                return FRAMESIZE_VGA;
            case CameraFrameSize_Svga:
                return FRAMESIZE_SVGA;
            case CameraFrameSize_Xga:
                return FRAMESIZE_XGA;
            case CameraFrameSize_Sxga:
                return FRAMESIZE_SXGA;
            case CameraFrameSize_Uxga:
                return FRAMESIZE_UXGA;
            default:
                return FRAMESIZE_VGA;
        }
    }
}

signed int Camera::NativeInit( signed int param0, signed int param1, signed int param2, signed int param3, signed int param4, signed int param5, signed int param6, signed int param7, signed int param8, signed int param9, signed int param10, signed int param11, signed int param12, signed int param13, signed int param14, signed int param15, signed int param16, signed int param17, signed int param18, signed int param19, HRESULT &hr )
{
    (void)hr;

    if (s_cameraInitialized)
    {
        return ESP_OK;
    }

    camera_config_t config = {};
    config.pin_pwdn = param0;
    config.pin_reset = param1;
    config.pin_xclk = param2;
    config.pin_sccb_sda = param3;
    config.pin_sccb_scl = param4;
    config.pin_d7 = param5;
    config.pin_d6 = param6;
    config.pin_d5 = param7;
    config.pin_d4 = param8;
    config.pin_d3 = param9;
    config.pin_d2 = param10;
    config.pin_d1 = param11;
    config.pin_d0 = param12;
    config.pin_vsync = param13;
    config.pin_href = param14;
    config.pin_pclk = param15;
    config.xclk_freq_hz = param16;
    config.ledc_timer = LEDC_TIMER_0;
    config.ledc_channel = LEDC_CHANNEL_0;
    config.pixel_format = PIXFORMAT_JPEG;
    config.frame_size = GetFrameSize(param17);
    config.jpeg_quality = param18;
    config.fb_count = param19;
    config.fb_location = CAMERA_FB_IN_PSRAM;
    config.grab_mode = CAMERA_GRAB_WHEN_EMPTY;

    esp_err_t result = esp_camera_init(&config);
    if (result == ESP_OK)
    {
        s_cameraInitialized = true;
    }

    return result;
}

CLR_RT_TypedArray_UINT8 Camera::NativeGetJpeg(  HRESULT &hr )
{

    (void)hr;
    CLR_RT_TypedArray_UINT8 retValue = 0;

   camera_fb_t* fb = esp_camera_fb_get();
    if (!fb)
    {
        return retValue; // returns empty array
    }

    // Allocate managed array
    CLR_RT_HeapBlock_Array* jpegArray;
    CLR_RT_HeapBlock& arrayRef = retValue;

    CLR_RT_HeapBlock_Array::CreateInstance(arrayRef, fb->len, g_CLR_RT_WellKnownTypes.m_UInt8);
    jpegArray = arrayRef.DereferenceArray();

    // Copy JPEG data
    memcpy(jpegArray->GetFirstElement(), fb->buf, fb->len);

    // Return frame buffer to driver
    esp_camera_fb_return(fb);

    return retValue;
}

void Camera::NativeDeinit(  HRESULT &hr )
{
    (void)hr;

    if (s_cameraInitialized)
    {
        esp_camera_deinit();
        s_cameraInitialized = false;
    }
}
