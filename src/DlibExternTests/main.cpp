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

void test_dlibextern_dnn_mmod();

int main(void)
{
    test_dlibextern_dnn_mmod();

    return 0;
}

void test_dlibextern_dnn_mmod()
{
    auto window = dlib_image_window_new();
    dlib_image_window_set_title(window, "test window");

    auto image = dlib_matrix_rgbpixel_new();
    auto width = dlib_matrix_rgbpixel_nc(image);
    auto height = dlib_matrix_rgbpixel_nr(image);
    std::cout << "width=" << width << ", height=" << height << std::endl;
    dlib_image_window_set_image_matrix_rgbpixel(window, image);

    dlib_load_image_matrix_rgbpixel(image, "../DlibSharp.Tests/images/lenna.bmp");
    width = dlib_matrix_rgbpixel_nc(image);
    height = dlib_matrix_rgbpixel_nr(image);
    std::cout << "width=" << width << ", height=" << height << std::endl;
    dlib_image_window_set_image_matrix_rgbpixel(window, image);

    dlib_pyramid_up_matrix_rgbpixel(image);
    width = dlib_matrix_rgbpixel_nc(image);
    height = dlib_matrix_rgbpixel_nr(image);
    std::cout << "width=" << width << ", height=" << height << std::endl;
    dlib_image_window_set_image_matrix_rgbpixel(window, image);

    auto dets = vector_Rect_new1();
    auto detector = dlib_dnn_mmod_face_detection_construct("../DlibSharp.Tests/data/mmod_human_face_detector.dat");
    dlib_dnn_mmod_face_detection_operator(detector, image, dets);
    long count = vector_Rect_getSize(dets);
    if (count > 0)
    {
        auto rectangles = vector_Rect_getPointer(dets);
        for (int i = 0; i < count; i++)
        {
            std::cout
                << rectangles[i].x
                << ", " << rectangles[i].y
                << ", " << rectangles[i].width
                << ", " << rectangles[i].height
                << std::endl;
        }
    }

    dlib_matrix_rgbpixel_delete(image);
    vector_Rect_delete(dets);
    dlib_dnn_mmod_face_detection_delete(detector);

    dlib_image_window_delete(window);
}
