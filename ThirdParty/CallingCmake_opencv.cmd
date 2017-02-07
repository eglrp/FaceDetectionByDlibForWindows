rmdir /S /Q opencv_x86
rmdir /S /Q opencv_x64

mkdir opencv_x86
cd opencv_x86
cmake.exe ../opencv -G "Visual Studio 14 2015"       -DCMAKE_SUPPRESS_REGENERATION:BOOL=TRUE -DBUILD_TESTS=OFF -DBUILD_opencv_apps=OFF -DBUILD_opencv_ts=OFF -DBUILD_PERF_TESTS=OFF -DBUILD_SHARED_LIBS=OFF -DENABLE_AVX=ON -DWITH_CUDA=OFF -DWITH_CUFFT=OFF -DWITH_LAPACK=OFF -DWITH_MATLAB=OFF -DWITH_OPENCL=OFF
cmake.exe --build "." --target "INSTALL" --config "Debug"
cmake.exe --build "." --target "INSTALL" --config "Release"
cd ..

mkdir opencv_x64
cd opencv_x64
cmake.exe ../opencv -G "Visual Studio 14 2015 Win64" -DCMAKE_SUPPRESS_REGENERATION:BOOL=TRUE -DBUILD_TESTS=OFF -DBUILD_opencv_apps=OFF -DBUILD_opencv_ts=OFF -DBUILD_PERF_TESTS=OFF -DBUILD_SHARED_LIBS=OFF -DENABLE_AVX=ON -DWITH_CUDA=OFF -DWITH_CUFFT=OFF -DWITH_LAPACK=OFF -DWITH_MATLAB=OFF -DWITH_OPENCL=OFF
cmake.exe --build "." --target "INSTALL" --config "Debug"
cmake.exe --build "." --target "INSTALL" --config "Release"
cd ..

pause
