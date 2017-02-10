mkdir dlib_examples_build_x64_cuda\Release
mkdir dlib_examples_build_x86_avx\Release
mkdir dlib_examples_build_x86_mkl\Release

mkdir dlib_examples_build_x64_cuda\Debug
mkdir dlib_examples_build_x86_avx\Debug
mkdir dlib_examples_build_x86_mkl\Debug

copy opencv_x64\bin\Release\opencv_core320.dll      dlib_examples_build_x64_cuda\Release\
copy opencv_x64\bin\Release\opencv_videoio320.dll   dlib_examples_build_x64_cuda\Release\
copy opencv_x64\bin\Release\opencv_imgcodecs320.dll dlib_examples_build_x64_cuda\Release\
copy opencv_x64\bin\Release\opencv_imgproc320.dll   dlib_examples_build_x64_cuda\Release\
copy opencv_x86\bin\Release\opencv_core320.dll      dlib_examples_build_x86_avx\Release\
copy opencv_x86\bin\Release\opencv_videoio320.dll   dlib_examples_build_x86_avx\Release\
copy opencv_x86\bin\Release\opencv_imgcodecs320.dll dlib_examples_build_x86_avx\Release\
copy opencv_x86\bin\Release\opencv_imgproc320.dll   dlib_examples_build_x86_avx\Release\
copy opencv_x86\bin\Release\opencv_core320.dll      dlib_examples_build_x86_mkl\Release\
copy opencv_x86\bin\Release\opencv_videoio320.dll   dlib_examples_build_x86_mkl\Release\
copy opencv_x86\bin\Release\opencv_imgcodecs320.dll dlib_examples_build_x86_mkl\Release\
copy opencv_x86\bin\Release\opencv_imgproc320.dll   dlib_examples_build_x86_mkl\Release\
												   
copy opencv_x64\bin\Debug\opencv_core320d.dll       dlib_examples_build_x64_cuda\Debug\
copy opencv_x64\bin\Debug\opencv_videoio320d.dll    dlib_examples_build_x64_cuda\Debug\
copy opencv_x64\bin\Debug\opencv_imgcodecs320d.dll  dlib_examples_build_x64_cuda\Debug\
copy opencv_x64\bin\Debug\opencv_imgproc320d.dll    dlib_examples_build_x64_cuda\Debug\
copy opencv_x86\bin\Debug\opencv_core320d.dll       dlib_examples_build_x86_avx\Debug\
copy opencv_x86\bin\Debug\opencv_videoio320d.dll    dlib_examples_build_x86_avx\Debug\
copy opencv_x86\bin\Debug\opencv_imgcodecs320d.dll  dlib_examples_build_x86_avx\Debug\
copy opencv_x86\bin\Debug\opencv_imgproc320d.dll    dlib_examples_build_x86_avx\Debug\
copy opencv_x86\bin\Debug\opencv_core320d.dll       dlib_examples_build_x86_mkl\Debug\
copy opencv_x86\bin\Debug\opencv_videoio320d.dll    dlib_examples_build_x86_mkl\Debug\
copy opencv_x86\bin\Debug\opencv_imgcodecs320d.dll  dlib_examples_build_x86_mkl\Debug\
copy opencv_x86\bin\Debug\opencv_imgproc320d.dll    dlib_examples_build_x86_mkl\Debug\

pause
