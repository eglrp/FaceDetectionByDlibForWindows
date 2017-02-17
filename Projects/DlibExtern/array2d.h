#ifndef __DLIBEXTERN_ARRAY2D_H__
#define __DLIBEXTERN_ARRAY2D_H__

#include "CommonSymbolsWithDlib.h"
#include "DlibExtern.h"
#include <dlib/image_io.h>
#include <dlib/image_transforms/interpolation.h>
#include <stdint.h>

EXTERN_API dlib::array2d<uchar> *dlib_array2d_uchar_new()
{
    return new dlib::array2d<uchar>;
}

EXTERN_API void dlib_array2d_uchar_delete(dlib::array2d<uchar> *obj)
{
    delete obj;
}

EXTERN_API int32_t dlib_array2d_uchar_nr(dlib::array2d<uchar> *obj)
{
    return (int32_t)(obj->nr());
}

EXTERN_API int32_t dlib_array2d_uchar_nc(dlib::array2d<uchar> *obj)
{
    return (int32_t)(obj->nc());
}

EXTERN_API void dlib_load_image_array2d_uchar(dlib::array2d<uchar> *obj, const char *file_name)
{
    try 
    {
        dlib::load_image(*obj, file_name);        
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

EXTERN_API void dlib_load_bmp_array2d_uchar(dlib::array2d<uchar> *obj, const char *buffer, size_t buffer_length)
{
    try
    { 
        std::istringstream stream(std::string(buffer, buffer + buffer_length));
        dlib::load_bmp(*obj, stream);
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

EXTERN_API void dlib_pyramid_up_array2d_uchar(dlib::array2d<uchar> *obj)
{
    try
    {
        dlib::pyramid_up(*obj);
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

#endif
