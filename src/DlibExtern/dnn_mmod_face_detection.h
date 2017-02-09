#ifndef __DLIBEXTERN_DNN_MMOD_FACE_DETECTION_H__
#define __DLIBEXTERN_DNN_MMOD_FACE_DETECTION_H__

#include "CommonSymbolsWithDlib.h"
#include "DlibExtern.h"

#include <dlib/dnn.h>
#include <dlib/data_io.h>
#include <dlib/image_processing.h>
#include <dlib/gui_widgets.h>

using namespace dlib;

template <long num_filters, typename SUBNET> using con5d = con<num_filters, 5, 5, 2, 2, SUBNET>;
template <long num_filters, typename SUBNET> using con5 = con<num_filters, 5, 5, 1, 1, SUBNET>;

template <typename SUBNET> using downsampler = relu<affine<con5d<32, relu<affine<con5d<32, relu<affine<con5d<16, SUBNET>>>>>>>>>;
template <typename SUBNET> using rcon5 = relu<affine<con5<45, SUBNET>>>;

using net_type = loss_mmod<con<1, 9, 9, 1, 1, rcon5<rcon5<rcon5<downsampler<input_rgb_image_pyramid<pyramid_down<6>>>>>>>>;

EXTERN_API net_type* dlib_dnn_mmod_face_detection_construct()
{
	net_type* ret = new net_type();
	deserialize("C:/Data/Dlib/mmod_human_face_detector.dat/mmod_human_face_detector.dat") >> (*ret);
	return ret;
}

EXTERN_API void dlib_dnn_mmod_face_detection_delete(net_type *obj)
{
	if (obj != nullptr) { delete obj; obj = nullptr; }
}

EXTERN_API void dlib_dnn_mmod_face_detection_operator(net_type *obj, matrix<rgb_pixel> *image, std::vector<Rect> *dst)
{
	try
	{
		auto dets = (*obj)(*image);
		dst->resize(dets.size());
		int i = 0;
		for (auto&& d : dets) { (*dst)[i++] = Rect(d); }
	}
	catch (dlib::error &e)
	{
		if (g_ErrorCallback) g_ErrorCallback(e.what());
	}
}

#endif
