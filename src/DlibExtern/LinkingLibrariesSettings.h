#include "CommonSymbolsWithDlib.h"

#if defined(PLATFORM_IS_Win32)

#if DLIBEXTERN_USE_MKL_BLAS
#define INTEL_MKL_LIBS_PATH "C:\\Program Files (x86)\\IntelSWTools\\compilers_and_libraries_2017.1.143\\windows\\mkl\\lib\\"
#define INTEL_TBB_LIBS_PATH "C:\\Program Files (x86)\\IntelSWTools\\compilers_and_libraries_2017.1.143\\windows\\tbb\\lib\\"
#pragma comment(lib, INTEL_MKL_LIBS_PATH "ia32_win\\mkl_intel_c.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "ia32_win\\mkl_tbb_thread.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "ia32_win\\mkl_core.lib")
#pragma comment(lib, INTEL_TBB_LIBS_PATH "ia32_win\\vc14\\tbb.lib")
#endif

#elif defined(PLATFORM_IS_x64)

#if DLIBEXTERN_USE_CUDA
#define CUDA_X64_LIB_PATH "C:/Program Files/NVIDIA GPU Computing Toolkit/CUDA/v8.0/lib/x64/"
#pragma comment(lib, CUDA_X64_LIB_PATH "cuda.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cublas.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cudart_static.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cudnn.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "curand.lib")
#endif

#endif

#pragma comment(lib, DLIB_SRC_ROOT_PATH DLIB_LIB_DIR_NAME "/dlib_build/" DEBUG_OR_RELEASE "/dlib.lib")
