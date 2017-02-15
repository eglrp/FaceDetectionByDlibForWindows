#include "common.h"

#include "../DlibExtern/CommonSymbolsWithDlib.h"
#include "../DlibExtern/LinkingLibrariesSettings.h"

#pragma comment(lib, DlibBuiltFilesForWindows_ROOT_PATH DLIB_LIB_DIR_NAME "/dlib_build/" DEBUG_OR_RELEASE "/dlib.lib")

#ifdef _DEBUG
#define OPENCV_LIB_EXT "d.lib"
#else
#define OPENCV_LIB_EXT ".lib"
#endif

#pragma comment(lib, DlibBuiltFilesForWindows_ROOT_PATH "opencv_" X86_OR_X64 "/install/" X86_OR_X64 "/vc14/lib/opencv_core320" OPENCV_LIB_EXT)
#pragma comment(lib, DlibBuiltFilesForWindows_ROOT_PATH "opencv_" X86_OR_X64 "/install/" X86_OR_X64 "/vc14/lib/opencv_highgui320" OPENCV_LIB_EXT)
#pragma comment(lib, DlibBuiltFilesForWindows_ROOT_PATH "opencv_" X86_OR_X64 "/install/" X86_OR_X64 "/vc14/lib/opencv_imgcodecs320" OPENCV_LIB_EXT)
#pragma comment(lib, DlibBuiltFilesForWindows_ROOT_PATH "opencv_" X86_OR_X64 "/install/" X86_OR_X64 "/vc14/lib/opencv_imgproc320" OPENCV_LIB_EXT)
#pragma comment(lib, DlibBuiltFilesForWindows_ROOT_PATH "opencv_" X86_OR_X64 "/install/" X86_OR_X64 "/vc14/lib/opencv_superres320" OPENCV_LIB_EXT)
#pragma comment(lib, DlibBuiltFilesForWindows_ROOT_PATH "opencv_" X86_OR_X64 "/install/" X86_OR_X64 "/vc14/lib/opencv_video320" OPENCV_LIB_EXT)
#pragma comment(lib, DlibBuiltFilesForWindows_ROOT_PATH "opencv_" X86_OR_X64 "/install/" X86_OR_X64 "/vc14/lib/opencv_videoio320" OPENCV_LIB_EXT)
#pragma comment(lib, "vfw32.lib")

//#include <iostream>

void face_detection_ex_test();
void face_landmark_detection_ex_test();
void video_tracking_ex_test();
void three_dimension_point_cloud_ex_test();
void dnn_imagenet_ex_test();
void dnn_mmod_face_detection_ex_test();
//void dnn_mmod_face_detection_from_webcam_images();
void dnn_mmod_face_detection_webcam();
int webcam_face_pose_ex_test();

int main(void)
{
	//face_detection_ex_test();
	//face_landmark_detection_ex_test();
	//video_tracking_ex_test();
	//three_dimension_point_cloud_ex_test();
	//dnn_imagenet_ex_test();
	//dnn_mmod_face_detection_ex_test();
	dnn_mmod_face_detection_webcam();
	//webcam_face_pose_ex_test();

	return 0;
}
