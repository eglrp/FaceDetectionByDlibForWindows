#include <iostream>

#include "../DlibExtern/DlibExtern.h"
#include "../DlibExtern/array2d.h"
#include "../DlibExtern/matrix_rgbpixel.h"
#include "../DlibExtern/std_vector.h"
#include "../DlibExtern/frontal_face_detector.h"
#include "../DlibExtern/dnn_mmod_face_detection.h"

void test_dlibextern_dnn_mmod();

#ifdef _DEBUG
#define DEBUG_OR_RELEASE "Debug"
#else
#define DEBUG_OR_RELEASE "Release"
#endif

#pragma comment(lib, "../../ThirdParty/bin/dlib_build_Win32_AVX/" DEBUG_OR_RELEASE "/dlib_build_Win32_AVX.lib")

int main(void)
{
	test_dlibextern_dnn_mmod();

	return 0;
}

void test_dlibextern_dnn_mmod()
{
	auto image = dlib_matrix_rgbpixel_new();
	dlib_load_image_matrix_rgbpixel(image, "../DlibSharp.Tests/images/lenna.bmp");
	//dlib_pyramid_up_matrix_rgbpixel(image);

	auto dets = vector_Rect_new1();
	auto detector = dlib_dnn_mmod_face_detection_construct();
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
}
