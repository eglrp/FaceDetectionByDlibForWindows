#ifndef __DLIBEXTERN_MATRIX_RGBPIXEL_H__
#define __DLIBEXTERN_MATRIX_RGBPIXEL_H__

#include "CommonSymbolsWithDlib.h"
#include "DlibExtern.h"
#include <dlib/image_io.h>
#include <dlib/image_transforms/interpolation.h>
#include <stdint.h>

EXTERN_API dlib::matrix<dlib::rgb_pixel> *dlib_matrix_rgbpixel_new()
{
    return new dlib::matrix<dlib::rgb_pixel>;
}

EXTERN_API dlib::matrix<dlib::rgb_pixel> *dlib_matrix_rgbpixel_new_width_height(size_t width, size_t height)
{
    return new dlib::matrix<dlib::rgb_pixel>(height, width);
}

EXTERN_API dlib::matrix<dlib::rgb_pixel> *dlib_matrix_rgbpixel_new_copied(dlib::matrix<dlib::rgb_pixel> *obj)
{
    auto ret = new dlib::matrix<dlib::rgb_pixel>(obj->nr(), obj->nc());
    // TODO: use better method
    dlib::resize_image(*obj, *ret, dlib::interpolate_nearest_neighbor());
    return ret;
}

EXTERN_API void dlib_matrix_rgbpixel_delete(dlib::matrix<dlib::rgb_pixel> *obj)
{
    delete obj;
}

EXTERN_API int32_t dlib_matrix_rgbpixel_nr(dlib::matrix<dlib::rgb_pixel> *obj)
{
    return (int32_t)(obj->nr());
}

EXTERN_API int32_t dlib_matrix_rgbpixel_nc(dlib::matrix<dlib::rgb_pixel> *obj)
{
    return (int32_t)(obj->nc());
}

EXTERN_API void dlib_load_image_matrix_rgbpixel(dlib::matrix<dlib::rgb_pixel> *obj, const char *file_name)
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

EXTERN_API void dlib_load_bmp_matrix_rgbpixel(dlib::matrix<dlib::rgb_pixel> *obj, const char *buffer, size_t buffer_length)
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

EXTERN_API void dlib_pyramid_up_matrix_rgbpixel(dlib::matrix<dlib::rgb_pixel> *obj)
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

EXTERN_API void dlib_resize_image_matrix_rgbpixel_src_dest_interporation_kind(dlib::matrix<dlib::rgb_pixel> *src, dlib::matrix<dlib::rgb_pixel> *dest, int32_t interporation_kind)
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

EXTERN_API void dlib_resize_image_matrix_rgbpixel_width_height(dlib::matrix<dlib::rgb_pixel> **src, size_t width, size_t height)
{
    try
    {
        auto dest = dlib_matrix_rgbpixel_new_width_height(width, height);
        dlib_resize_image_matrix_rgbpixel_src_dest_interporation_kind(*src, dest, ResizeImageInterporateKind::Bilinear);
        dlib_matrix_rgbpixel_delete(*src);
        *src = dest;
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

#endif
