#ifndef __test_dlibextern_face_landmark_detection_h__
#define __test_dlibextern_face_landmark_detection_h__

#include <iostream>

#include "../DlibExtern/CommonSymbolsWithDlib.h"
#include "../DlibExtern/LinkingLibrariesSettings.h"
#pragma comment(lib, "../../DlibBuiltFilesForWindows/" DLIB_LIB_DIR_NAME "/dlib_build/" DEBUG_OR_RELEASE "/dlib.lib")

#include "../DlibExtern/DlibExtern.h"
#include "../DlibExtern/array2d_rgbpixel.h"
#include "../DlibExtern/std_vector.h"
#include "../DlibExtern/face_landmark_detection.h"
#include "../DlibExtern/image_window.h"

bool test_dlibextern_face_landmark_detection()
{
    auto window = dlib_image_window_new_title("input");
    auto image = dlib_array2d_uchar_new();
    dlib_load_image_array2d_uchar(image, "../DlibSharp.Tests/images/lenna.bmp");
    dlib_image_window_set_image_array2d_uchar(window, image);

    auto dets = vector_FaceLandmarkInternal_new1();
    auto detector = dlib_get_face_landmark_detection("D:/Data/Dlib/shape_predictor_68_face_landmarks.dat");
    dlib_face_landmark_detection_detect(detector, image, -0.5, dets);
    long count = vector_FaceLandmarkInternal_getSize(dets);
    if (count > 0)
    {
        auto faceLandmarkInternals = vector_FaceLandmarkInternal_getPointer(dets);
        for (int i = 0; i < count; i++)
        {
            std::cout
                << faceLandmarkInternals[i].rect.x
                << ", " << faceLandmarkInternals[i].rect.y
                << ", " << faceLandmarkInternals[i].rect.width
                << ", " << faceLandmarkInternals[i].rect.height
                << std::endl;
            for (int j = 0; j < 68; j++)
            {
                std::cout
                    << "{"
                    << faceLandmarkInternals[i].parts[j].x
                    << ", " << faceLandmarkInternals[i].parts[j].y
                    << "}, ";
            }
            std::cout << std::endl;
        }
    }

    dlib_array2d_uchar_delete(image);
    vector_FaceLandmarkInternal_delete(dets);
    dlib_face_landmark_detection_detect(detector);

    dlib_image_window_delete(window);

    return true;
}

#endif
