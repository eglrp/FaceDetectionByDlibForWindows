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

void test_dlibextern_dnn_mmod();

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
}
