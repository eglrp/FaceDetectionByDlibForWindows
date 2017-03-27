#ifndef __test_dlibextern_face_landmark_detection_h__
#define __test_dlibextern_face_landmark_detection_h__

#include <iostream>

#include "../DlibExtern/CommonSymbolsWithDlib.h"
#include "../DlibExtern/LinkingLibrariesSettings.h"
#pragma comment(lib, "../../DlibBuiltFilesForWindows/" DLIB_LIB_DIR_NAME "/dlib_build/" DEBUG_OR_RELEASE "/dlib.lib")

#include "../DlibExtern/DlibExtern.h"
#include "../DlibExtern/array2d_uchar.h"
#include "../DlibExtern/array2d_rgbpixel.h"
#include "../DlibExtern/std_vector.h"
#include "../DlibExtern/frontal_face_detector.h"
#include "../DlibExtern/face_landmark_detection.h"
#include "../DlibExtern/image_window.h"

void show_array2d_rgbpixel_width_height(dlib::array2d<dlib::rgb_pixel>* obj)
{
    std::cout << "width=" << dlib_array2d_rgbpixel_nc(obj) << ", height=" << dlib_array2d_rgbpixel_nr(obj) << std::endl;
}

bool test_dlibextern_array2d_rgbpixel_resize_image()
{
    if (false)
    {
        auto window = dlib_image_window_new();
        auto image = dlib_array2d_rgbpixel_new();
        dlib_image_window_set_image_array2d_rgbpixel(window, image);
        show_array2d_rgbpixel_width_height(image);
        dlib_array2d_rgbpixel_delete(image);
        system("pause");
        dlib_image_window_delete(window);
    }

    if (false)
    {
        auto window = dlib_image_window_new_title("new_width_height");
        auto image = dlib_array2d_rgbpixel_new_width_height(640, 480);
        dlib_image_window_set_image_array2d_rgbpixel(window, image);
        show_array2d_rgbpixel_width_height(image);
        dlib_array2d_rgbpixel_delete(image);
        system("pause");
        dlib_image_window_delete(window);
    }

    if (false)
    {
        auto window = dlib_image_window_new_title("new_cloned");
        auto src = dlib_array2d_rgbpixel_new();
        dlib_load_image_array2d_rgbpixel(src, "../DlibSharp.Tests/images/lenna.bmp");
        show_array2d_rgbpixel_width_height(src);

        auto image = dlib_array2d_rgbpixel_new_copied(src);

        dlib_image_window_set_image_array2d_rgbpixel(window, image);
        show_array2d_rgbpixel_width_height(image);
        system("pause");
        dlib_array2d_rgbpixel_delete(image);
        dlib_array2d_rgbpixel_delete(src);
        dlib_image_window_delete(window);
    }

    if (true)
    {
        auto window = dlib_image_window_new_title("resize_self");
        auto image = dlib_array2d_rgbpixel_new();
        dlib_load_image_array2d_rgbpixel(image, "../DlibSharp.Tests/images/lenna.bmp");
        show_array2d_rgbpixel_width_height(image);

        auto width = dlib_array2d_rgbpixel_nc(image);
        auto height = dlib_array2d_rgbpixel_nr(image);
        dlib_resize_image_array2d_rgbpixel_width_height(&image, width * 3, height * 3);

        dlib_image_window_set_image_array2d_rgbpixel(window, image);
        show_array2d_rgbpixel_width_height(image);
        system("pause");
        dlib_array2d_rgbpixel_delete(image);
        dlib_image_window_delete(window);
    }

    if (true)
    {
        auto window = dlib_image_window_new_title("input");
        auto window1 = dlib_image_window_new_title("NearestNeighbor");
        auto window2 = dlib_image_window_new_title("Bilinear");
        auto window3 = dlib_image_window_new_title("Quadratic");

        auto image = dlib_array2d_rgbpixel_new();
        dlib_load_image_array2d_rgbpixel(image, "../DlibSharp.Tests/images/lenna.bmp");
        auto image_width = dlib_array2d_rgbpixel_nc(image);
        auto image_height = dlib_array2d_rgbpixel_nr(image);

        auto imageNearestNeighbor = dlib_array2d_rgbpixel_new_width_height(image_width * 3, image_height * 3);
        auto imageBilinear = dlib_array2d_rgbpixel_new_width_height(image_width * 3, image_height * 3);
        auto imageQuadratic = dlib_array2d_rgbpixel_new_width_height(image_width * 3, image_height * 3);

        dlib_resize_image_array2d_rgbpixel_src_dest_interporation_kind(image, imageNearestNeighbor, ResizeImageInterporateKind::NearestNeighbor);
        dlib_resize_image_array2d_rgbpixel_src_dest_interporation_kind(image, imageBilinear, ResizeImageInterporateKind::Bilinear);
        dlib_resize_image_array2d_rgbpixel_src_dest_interporation_kind(image, imageQuadratic, ResizeImageInterporateKind::Quadratic);

        dlib_image_window_set_image_array2d_rgbpixel(window, image);
        dlib_image_window_set_image_array2d_rgbpixel(window1, imageNearestNeighbor);
        dlib_image_window_set_image_array2d_rgbpixel(window2, imageBilinear);
        dlib_image_window_set_image_array2d_rgbpixel(window3, imageQuadratic);

        show_array2d_rgbpixel_width_height(image);
        show_array2d_rgbpixel_width_height(imageNearestNeighbor);
        show_array2d_rgbpixel_width_height(imageBilinear);
        show_array2d_rgbpixel_width_height(imageQuadratic);

        system("pause");

        dlib_resize_image_array2d_rgbpixel_width_height(&imageNearestNeighbor, image_width, image_height);
        dlib_resize_image_array2d_rgbpixel_width_height(&imageBilinear, image_width, image_height);
        dlib_resize_image_array2d_rgbpixel_width_height(&imageQuadratic, image_width, image_height);

        dlib_image_window_set_image_array2d_rgbpixel(window, image);
        dlib_image_window_set_image_array2d_rgbpixel(window1, imageNearestNeighbor);
        dlib_image_window_set_image_array2d_rgbpixel(window2, imageBilinear);
        dlib_image_window_set_image_array2d_rgbpixel(window3, imageQuadratic);
        show_array2d_rgbpixel_width_height(image);
        show_array2d_rgbpixel_width_height(imageNearestNeighbor);
        show_array2d_rgbpixel_width_height(imageBilinear);
        show_array2d_rgbpixel_width_height(imageQuadratic);

        system("pause");

        dlib_array2d_rgbpixel_delete(image);
        dlib_array2d_rgbpixel_delete(imageNearestNeighbor);
        dlib_array2d_rgbpixel_delete(imageBilinear);
        dlib_array2d_rgbpixel_delete(imageQuadratic);
        dlib_image_window_delete(window);
        dlib_image_window_delete(window1);
        dlib_image_window_delete(window2);
        dlib_image_window_delete(window3);
    }

    return true;
}

bool test_dlibextern_face_landmark_detection()
{
    auto window = dlib_image_window_new_title("input");
    auto image = dlib_array2d_rgbpixel_new();
    dlib_load_image_array2d_rgbpixel(image, "../DlibSharp.Tests/images/lenna.bmp");
    dlib_image_window_set_image_array2d_rgbpixel(window, image);

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

    dlib_array2d_rgbpixel_delete(image);
    vector_FaceLandmarkInternal_delete(dets);
    dlib_face_landmark_detection_delete(detector);

    dlib_image_window_delete(window);

    return true;
}

#endif
