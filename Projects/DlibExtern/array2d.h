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

EXTERN_API dlib::array2d<uchar> *dlib_array2d_uchar_new_width_height(size_t width, size_t height)
{
    return new dlib::array2d<uchar>(height, width);
}

EXTERN_API dlib::array2d<uchar> *dlib_array2d_uchar_new_copied(dlib::array2d<uchar> *obj)
{
    auto ret = new dlib::array2d<uchar>(obj->nr(), obj->nc());
    // TODO: use better method
    dlib::resize_image(*obj, *ret, dlib::interpolate_nearest_neighbor());
    return ret;
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

EXTERN_API void dlib_resize_image_array2d_uchar_src_dest_interporation_kind(dlib::array2d<uchar> *src, dlib::array2d<uchar> *dest, ResizeImageInterporateKind interporation_kind)
{
    try
    {
        switch (interporation_kind)
        {
        case ResizeImageInterporateKind::NearestNeighbor:
            dlib::resize_image(*src, *dest, dlib::interpolate_nearest_neighbor());
            break;
        case ResizeImageInterporateKind::Bilinear:
            dlib::resize_image(*src, *dest, dlib::interpolate_bilinear());
            break;
        case ResizeImageInterporateKind::Quadratic:
            dlib::resize_image(*src, *dest, dlib::interpolate_quadratic());
            break;
        default:
            break;
        }
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

EXTERN_API void dlib_resize_image_array2d_uchar_width_height(dlib::array2d<uchar> **src, size_t width, size_t height)
{
    try
    {
        auto dest = dlib_array2d_uchar_new_width_height(width, height);
        dlib_resize_image_array2d_uchar_src_dest_interporation_kind(*src, dest, ResizeImageInterporateKind::Bilinear);
        dlib_array2d_uchar_delete(*src);
        *src = dest;
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

#endif
