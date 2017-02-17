#ifndef __DLIBEXTERN_IMAGE_WINDOW_H__
#define __DLIBEXTERN_IMAGE_WINDOW_H__

#include "CommonSymbolsWithDlib.h"
#include "DlibExtern.h"
#include <dlib/image_io.h>
#include <dlib/image_transforms/interpolation.h>
#include <dlib/gui_widgets.h>

EXTERN_API dlib::image_window *dlib_image_window_new()
{
    return new dlib::image_window();
}

EXTERN_API void dlib_image_window_delete(dlib::image_window *obj)
{
    delete obj;
}

EXTERN_API void dlib_image_window_set_title(dlib::image_window *obj, const char *title)
{
    try
    {
        std::string titleAsString(title);
        obj->set_title(titleAsString);
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

EXTERN_API void dlib_image_window_set_image_array2d_uchar(dlib::image_window *obj, dlib::array2d<uchar> *image)
{
    try
    {
        obj->set_image<dlib::array2d<uchar>>(*image);
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

EXTERN_API void dlib_image_window_set_image_matrix_rgbpixel(dlib::image_window *obj, dlib::matrix<dlib::rgb_pixel> *image)
{
    try
    {
        obj->set_image<dlib::matrix<dlib::rgb_pixel>>(*image);
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

EXTERN_API void dlib_image_window_clear_overlay(dlib::image_window *obj)
{
    try
    {
        obj->clear_overlay();
    }
    catch (dlib::error &e)
    {
        if (g_ErrorCallback) g_ErrorCallback(e.what());
    }
}

#endif
