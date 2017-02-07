rmdir /S /Q dlib_examples_build_x86_avx
rmdir /S /Q dlib_examples_build_x86_mkl
rmdir /S /Q dlib_examples_build_x64_cuda

mkdir dlib_examples_build_x86_avx
copy .\CMakeCache_dlib_examples_build_x86_avx.txt dlib_examples_build_x86_avx\CMakeCache.txt
cd dlib_examples_build_x86_avx
cmake.exe ../dlib/examples -G "Visual Studio 14 2015"
cmake.exe --build "dlib_build" --target "ALL_BUILD" --config "Debug"
cmake.exe --build "dlib_build" --target "ALL_BUILD" --config "Release"
cd ..

mkdir dlib_examples_build_x64_cuda
copy .\CMakeCache_dlib_examples_build_x64_cuda.txt dlib_examples_build_x64_cuda\CMakeCache.txt
cd dlib_examples_build_x64_cuda
cmake.exe ../dlib/examples -G "Visual Studio 14 2015 Win64"
cmake.exe --build "dlib_build" --target "ALL_BUILD" --config "Debug"
cmake.exe --build "dlib_build" --target "ALL_BUILD" --config "Release"
cd ..

mkdir dlib_examples_build_x86_mkl
copy .\CMakeCache_dlib_examples_build_x86_mkl.txt dlib_examples_build_x86_mkl\CMakeCache.txt
cd dlib_examples_build_x86_mkl
cmake.exe ../dlib/examples -G "Visual Studio 14 2015"
cmake.exe --build "dlib_build" --target "ALL_BUILD" --config "Debug"
cmake.exe --build "dlib_build" --target "ALL_BUILD" --config "Release"
cd ..

pause
