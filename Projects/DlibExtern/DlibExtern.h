#ifndef __DLIBEXTERN_H__
#define __DLIBEXTERN_H__

#include "CommonSymbolsWithDlib.h"
#include <dlib/geometry/rectangle.h>
#include <dlib/dnn/cuda_dlib.h>

// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the DLL_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// DLL_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#if defined(DLIBEXTERN_EXPORTS)
#define EXTERN_API extern "C" __declspec(dllexport)
#elif defined(DLIBEXTERN_IMPORTS)
#define EXTERN_API extern "C" __declspec(dllimport)
#else
#define EXTERN_API extern "C"
#endif

using uchar = unsigned char;

using ErrorCallback = void(*)(const char*);
static ErrorCallback g_ErrorCallback;

EXTERN_API ErrorCallback inline dlib_set_error_redirect(const ErrorCallback callback)
{
    auto current_callback = g_ErrorCallback;
    g_ErrorCallback = callback;
    return current_callback;
}

extern "C"
{
    enum ResizeImageInterporateKind
    {
        None = 0,
        NearestNeighbor = 1,
        Bilinear = 2,
        Quadratic = 3,
    };

    struct Rect
    {
        int32_t x, y, width, height;

        Rect()
            : x(0), y(0), width(0), height(0)
        {}

        Rect(int _x, int _y, int _w, int _h)
            : x(_x), y(_y), width(_w), height(_h)
        {}

        Rect(const dlib::rectangle &r)
            : x(r.left()), y(r.top()), width(r.width()), height(r.height())
        {}
    };

    struct Point
    {
        int32_t x, y;

        Point()
            : x(0), y(0)
        {}

        Point(int _x, int _y)
            : x(_x), y(_y)
        {}

        Point(const dlib::point& p)
            : x(p.x()), y(p.y())
        {}
    };

    struct FaceLandmarkInternal
    {
        Rect rect;
        Point parts[68];
    };
}

#endif
