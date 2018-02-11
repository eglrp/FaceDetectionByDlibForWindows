#ifndef __LINKINGLIBRARIESSETTINGS_H__
#define __LINKINGLIBRARIESSETTINGS_H__

#define INTEL_MKL_LIBS_PATH "C:\\Program Files (x86)\\IntelSWTools\\compilers_and_libraries\\windows\\"

// NOTE: In each [Project Property Pages],
// Set [Intel Performance Libraries] - [Use Intel MKL] to "No"
// Set [Linker] - [Input] - [Ignore Specific Default Libraries] to "libcpmt.lib;%(IgnoreSpecificDefaultLibraries)"

// To copy
// * libiomp5md.dll
// please download Intel MKL (Community License) from
// https://registrationcenter.intel.com/en/forms/?productid=2558&licensetype=2>
// and so on.
// 
// 
// Static link libraries (.lib) are linked into DlibExtern.dll.  So it is unnecessary to copy mkl_core.dll, mkl_intel_thread.dll and so on.
// FYI, install directory is, for example "C:\Program Files (x86)\IntelSWTools\compilers_and_libraries\windows\redist\ia32\mkl\".
// 
// https://software.intel.com/en-us/articles/intel-math-kernel-library-intel-mkl-linkage-and-distribution-quick-reference-guide
// "Static library, contains all processor-specific optimization in one package.
// mkl_intel_c.lib
// mkl_intel_thread.lib
// mkl_core.lib
// ..."
// 
// But libiomp5md.dll is NOT static linked, so you need to register paths for libiomp5md.dll or copy it into the same folder as EXE files.
// * C:\Program Files (x86)\IntelSWTools\compilers_and_libraries_2017.2.187\windows\redist\ia32\compiler
// * C:\Program Files (x86)\IntelSWTools\compilers_and_libraries_2017.2.187\windows\redist\intel64\compiler

#if defined(PLATFORM_IS_Win32)

#if DLIBEXTERN_USE_MKL_BLAS
#pragma comment(lib, INTEL_MKL_LIBS_PATH "mkl\\lib\\ia32\\mkl_intel_c.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "mkl\\lib\\ia32\\mkl_core.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "mkl\\lib\\ia32\\mkl_intel_thread.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "compiler\\lib\\ia32\\libiomp5md.lib")
#endif

#elif defined(PLATFORM_IS_x64)

#if DLIBEXTERN_USE_MKL_BLAS
#pragma comment(lib, INTEL_MKL_LIBS_PATH "mkl\\lib\\intel64\\mkl_intel_lp64.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "mkl\\lib\\intel64\\mkl_core.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "mkl\\lib\\intel64\\mkl_intel_thread.lib")
#pragma comment(lib, INTEL_MKL_LIBS_PATH "compiler\\lib\\intel64\\libiomp5md.lib")
#endif

#define CUDA_X64_LIB_PATH "C:/Program Files/NVIDIA GPU Computing Toolkit/CUDA/v9.1/lib/x64/"
#pragma comment(lib, CUDA_X64_LIB_PATH "cuda.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cublas.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cudart_static.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cudnn.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "curand.lib")
#pragma comment(lib, CUDA_X64_LIB_PATH "cusolver.lib")

#endif

#endif
