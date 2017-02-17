Currently, in "dll/x64" folder,
* cublas64_80.dll (39.8[MB])
* cudnn64_5.dll   (81.2[MB])
* curand64_80.dll (46.9[MB])
are necessary for easy re-distribution for internal tests, experiments and so on.
But we do not re-distribute these files.
So please copy these files there, or install CUDA and cuDNN.
If you have PATH to the runtime files in test environment,
you can delete these links to these files in DlibSharp.csproj.


To copy 
* cublas64_80.dll
* curand64_80.dll
please install CUDA Toolkit 8.0.


To copy
* cudnn64_5.dll
please download
cudnn-8.0-windows7-x64-v5.1.zip
from NVIDIA web site.
https://developer.nvidia.com/cudnn
There is not 32-bit or static link version of cuDNN.
This windows 7 version can work on Windows 10, I think.
