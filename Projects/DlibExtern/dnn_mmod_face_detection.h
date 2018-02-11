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

class dlib_extern_dnn_mmod_face_detection
{
public:
    net_type detector;

public:
    dlib_extern_dnn_mmod_face_detection(const char* file_name)
    {
        deserialize(file_name) >> detector;
    }

    ~dlib_extern_dnn_mmod_face_detection()
    {
        detector.clean();
        std::cout << "dlib_extern_dnn_mmod_face_detection destructor called" << std::endl;
    }

    void Detect(matrix<rgb_pixel> *image, std::vector<Rect> *dst)
    {
        try
        {
            auto dets = detector(*image);

            dst->resize(dets.size());
            for (size_t i = 0; i < dets.size(); ++i)
            {
                (*dst)[i] = Rect(dets[i]);
            }
        }
        catch (dlib::error &e)
        {
            if (g_ErrorCallback) g_ErrorCallback(e.what());
        }
    }
};

EXTERN_API int dlib_cuda_get_num_devices()
{
    return dlib::cuda::get_num_devices();
}

EXTERN_API dlib_extern_dnn_mmod_face_detection* dlib_dnn_mmod_face_detection_construct(const char *file_name)
{
    return new dlib_extern_dnn_mmod_face_detection(file_name);
}

EXTERN_API void dlib_dnn_mmod_face_detection_delete(dlib_extern_dnn_mmod_face_detection *obj)
{
    delete obj;
}

EXTERN_API void dlib_dnn_mmod_face_detection_operator(dlib_extern_dnn_mmod_face_detection *obj, matrix<rgb_pixel> *image, std::vector<Rect> *dst)
{
    obj->Detect(image, dst);
}

#endif
