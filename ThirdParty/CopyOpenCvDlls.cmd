copy opencv_x64\bin\Release\*.dll     dlib_examples_build_x64_cuda\Release\
copy opencv_x64\bin\Debug\*.dll       dlib_examples_build_x64_cuda\Debug\

copy opencv_x86\bin\Release\*.dll     dlib_examples_build_x86_avx\Release\
copy opencv_x86\bin\Debug\*.dll       dlib_examples_build_x86_avx\Debug\

copy opencv_x86\bin\Release\*.dll     dlib_examples_build_x86_mkl\Release\
copy opencv_x86\bin\Debug\*.dll       dlib_examples_build_x86_mkl\Debug\

copy opencv_x64\bin\Release\*.dll     ..\src\DlibModifiedExamples\bin\x64\Release\
copy opencv_x64\bin\Debug\*.dll       ..\src\DlibModifiedExamples\bin\x64\Debug\
copy opencv_x86\bin\Release\*.dll     ..\src\DlibModifiedExamples\bin\x86\Release\
copy opencv_x86\bin\Debug\*.dll       ..\src\DlibModifiedExamples\bin\x86\Debug\

copy opencv_x64\bin\Release\*.dll     ..\src\DlibExternTests\bin\x64\Release\
copy opencv_x64\bin\Debug\*.dll       ..\src\DlibExternTests\bin\x64\Debug\
copy opencv_x86\bin\Release\*.dll     ..\src\DlibExternTests\bin\x86\Release\
copy opencv_x86\bin\Debug\*.dll       ..\src\DlibExternTests\bin\x86\Debug\

pause
