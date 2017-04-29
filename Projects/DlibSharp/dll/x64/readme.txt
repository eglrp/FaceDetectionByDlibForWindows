Currently, in "dll/x64" folder,
* cublas64_80.dll (39.8[MB])
* cudnn64_5.dll   (81.2[MB])
* curand64_80.dll (46.9[MB])
* cusolver64_80.dll (46.9[MB])
* libiomp5md.dll (1.3[MB])
are necessary for easy re-distribution for internal tests, experiments and so on.
But we do not re-distribute these files.
So please copy these files there, or install CUDA and cuDNN.
If you have PATH to the runtime files in test environment,
you can delete these links to these files in DlibSharp.csproj.


To copy 
* cublas64_80.dll
* curand64_80.dll
* cusolver64_80.dll
please install CUDA Toolkit 8.0.


To copy
* cudnn64_5.dll
please download
cudnn-8.0-windows7-x64-v5.1.zip
from NVIDIA web site.
https://developer.nvidia.com/cudnn
There is not 32-bit or static link version of cuDNN.
This windows 7 version can work on Windows 10, I think.


To copy
* libiomp5md.dll
please download Intel MKL (Community License) from
https://registrationcenter.intel.com/en/forms/?productid=2558&licensetype=2>
and so on.


Static link libraries (.lib) are linked into DlibExtern.dll.  So it is unnecessary to copy mkl_core.dll, mkl_intel_thread.dll and so on.
FYI, install directory is, for example "C:\Program Files (x86)\IntelSWTools\compilers_and_libraries\windows\redist\ia32\mkl\".

https://software.intel.com/en-us/articles/intel-math-kernel-library-intel-mkl-linkage-and-distribution-quick-reference-guide
"Static library, contains all processor-specific optimization in one package.
mkl_intel_c.lib
mkl_intel_thread.lib
mkl_core.lib
..."

But libiomp5md.dll is NOT static linked, so you need to register paths for libiomp5md.dll or copy it into the same folder as EXE files.
* C:\Program Files (x86)\IntelSWTools\compilers_and_libraries_2017.2.187\windows\redist\ia32\compiler
* C:\Program Files (x86)\IntelSWTools\compilers_and_libraries_2017.2.187\windows\redist\intel64\compiler
