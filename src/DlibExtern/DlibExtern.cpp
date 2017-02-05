// DlibExtern.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "DlibExtern.h"

#ifdef _DEBUG
#define DEBUG_OR_RELEASE "Debug"
#else
#define DEBUG_OR_RELEASE "Release"
#endif

#if defined(PLATFORM_IS_Win32)
#define X86_OR_X64 "x86"
#define WIN32_OR_X64 "Win32"
#elif defined(PLATFORM_IS_x64)
#define X86_OR_X64 "x64"
#define WIN32_OR_X64 "x64"
#elif
#error "It is necessary to define 'PLATFORM_IS_$(Platform);' in 'Preprocessor Definition' on your Visual Studio project property."
#endif

#define DLIB_SRC_ROOT_PATH  "../../ThirdParty/"

#if defined(PLATFORM_IS_Win32)

#pragma comment(lib, DLIB_SRC_ROOT_PATH "bin/dlib_build_Win32_AVX/" DEBUG_OR_RELEASE "/dlib_build_Win32_AVX.lib")

#elif defined(PLATFORM_IS_x64)

#pragma comment(lib, DLIB_SRC_ROOT_PATH "bin/dlib_build_x64_CUDA/" DEBUG_OR_RELEASE "/dlib_build_x64_CUDA.lib")

#define CUDA_X64_LIB_PATH "C:/Program Files/NVIDIA GPU Computing Toolkit/CUDA/v8.0/lib/x64/"
#pragma comment(lib, CUDA_X64_LIB_PATH "cuda.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cublas.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cudart_static.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cudnn.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "curand.lib")

#if 0
// MKL
#define INTEL_MKL_LIBS_PATH "C:\\Program Files (x86)\\IntelSWTools\\compilers_and_libraries_2017.1.143\\windows\\mkl\\lib\\"
#define INTEL_TBB_LIBS_PATH "C:\\Program Files (x86)\\IntelSWTools\\compilers_and_libraries_2017.1.143\\windows\\tbb\\lib\\"
#pragma comment(lib, INTEL_MKL_LIBS_PATH "ia32_win\\mkl_intel_c.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "ia32_win\\mkl_tbb_thread.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "ia32_win\\mkl_core.lib")
#pragma comment(lib, INTEL_TBB_LIBS_PATH "ia32_win\\vc14\\tbb.lib")
#endif

#endif
