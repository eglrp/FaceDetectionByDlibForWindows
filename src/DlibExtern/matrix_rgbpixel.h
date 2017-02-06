#ifndef __DLIBEXTERN_MATRIX_RGBPIXEL_H__
#define __DLIBEXTERN_MATRIX_RGBPIXEL_H__

#include "CommonSymbolsWithDlib.h"
#include "DlibExtern.h"
#include <dlib/image_io.h>
#include <dlib/image_transforms/interpolation.h>

EXTERN_API dlib::matrix<dlib::rgb_pixel> *dlib_matrix_rgbpixel_new()
{
	return new dlib::matrix<dlib::rgb_pixel>;
}

EXTERN_API void dlib_matrix_rgbpixel_delete(dlib::matrix<dlib::rgb_pixel> *obj)
{
	delete obj;
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

#endif
