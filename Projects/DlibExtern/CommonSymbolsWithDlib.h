#ifndef __COMMONSYMBOLSWITHDLIB_H__
#define __COMMONSYMBOLSWITHDLIB_H__


#define DLIBEXTERN_USE_MKL_BLAS	1

#define DLIB_JPEG_SUPPORT
#define DLIB_JPEG_STATIC
#define DLIB_PNG_SUPPORT
//#define DLIB_DISABLE_ASSERT


#ifdef _DEBUG
#define DEBUG_OR_RELEASE "Debug"
#define ENABLE_ASSERTS
#else
#define DEBUG_OR_RELEASE "Release"
#endif

#if defined(PLATFORM_IS_Win32)
#define X86_OR_X64 "x86"
#define WIN32_OR_X64 "Win32"

#ifdef _DEBUG
#define DLIB_LIB_FILE_NAME "dlib_debug_32bit_msvc1900.lib"
#else
#define DLIB_LIB_FILE_NAME "dlib_release_32bit_msvc1900.lib"
#endif

#if DLIBEXTERN_USE_MKL_BLAS
// Not AVX+MKL but SSE4+MKL, because I hope it to work on Atom, too.
#define DLIB_LIB_DIR_NAME "dlib_examples_build_x86_mkl"
#define DLIB_USE_BLAS
#define DLIB_USE_LAPACK
#else
// There may be cases that users cannot use (community edition) MKL.
#error "There is no library now.  Atom processors cannot use AVX, but can use MKL.  Try MKL version on 64-bit OS."
//#define DLIB_LIB_DIR_NAME "dlib_examples_build_x86_sse4"
#endif

#elif defined(PLATFORM_IS_x64)
#define X86_OR_X64 "x64"
#define WIN32_OR_X64 "x64"

#ifdef _DEBUG
#define DLIB_LIB_FILE_NAME "dlib_debug_64bit_msvc1900.lib"
#else
#define DLIB_LIB_FILE_NAME "dlib_release_64bit_msvc1900.lib"
#endif

#define DLIB_USE_CUDA

#if DLIBEXTERN_USE_MKL_BLAS
// Not SSE4+MKL but AVX+MKL, CUDA users may use AVX(2).
#define DLIB_LIB_DIR_NAME "dlib_examples_build_x64_mkl_cuda"
#define DLIB_USE_BLAS
#define DLIB_USE_LAPACK
#else
// There may be cases that users cannot use (community edition) MKL.
#define DLIB_LIB_DIR_NAME "dlib_examples_build_x64_avx_cuda"
#endif

#else
#error "It is necessary to define 'PLATFORM_IS_$(Platform);' in 'Preprocessor Definition' on your Visual Studio project property."

#endif

#endif
