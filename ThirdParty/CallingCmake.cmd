rmdir /S /Q dlib_examples_build_x64_cuda
rmdir /S /Q dlib_examples_build_x86_mkl
rmdir /S /Q dlib_examples_build_x86_avx

mkdir dlib_examples_build_x64_cuda
copy .\CMakeCache_x64_cuda.txt dlib_examples_build_x64_cuda\CMakeCache.txt
cd dlib_examples_build_x64_cuda
cmake.exe ../dlib/examples -G "Visual Studio 14 2015 Win64"
cd ..

mkdir dlib_examples_build_x86_mkl
copy .\CMakeCache_x86_mkl.txt dlib_examples_build_x86_mkl\CMakeCache.txt
cd dlib_examples_build_x86_mkl
cmake.exe ../dlib/examples -G "Visual Studio 14 2015"
cd ..

mkdir dlib_examples_build_x86_avx
copy .\CMakeCache_x86_avx.txt dlib_examples_build_x86_avx\CMakeCache.txt
cd dlib_examples_build_x86_avx
cmake.exe ../dlib/examples -G "Visual Studio 14 2015"
cd ..

pause
