#include <iostream>

#include "../DlibExtern/CommonSymbolsWithDlib.h"
#include "../DlibExtern/LinkingLibrariesSettings.h"
#pragma comment(lib, "../../DlibBuiltFilesForWindows/" DLIB_LIB_DIR_NAME "/dlib_build/" DEBUG_OR_RELEASE "/dlib.lib")

#include "../DlibExtern/DlibExtern.h"
#include "../DlibExtern/array2d.h"
#include "../DlibExtern/matrix_rgbpixel.h"
#include "../DlibExtern/std_vector.h"
#include "../DlibExtern/frontal_face_detector.h"
#include "../DlibExtern/dnn_mmod_face_detection.h"
#include "../DlibExtern/image_window.h"
#include "test_dlibextern_frontal_face_detector.h"
#include "test_dlibextern_dnn_mmod_face_detection.h"

int main(void)
{
    test_dlibextern_array2d_uchar_resize_image();
    test_dlibextern_matrix_rgbpixel_resize_image();
    //test_dlibextern_frontal_face_detector();
    //test_dlibextern_dnn_mmod_face_detection();

    return 0;
}
