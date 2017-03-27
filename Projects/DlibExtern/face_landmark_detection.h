#ifndef __DLIBEXTERN_FACE_LANDMARK_DETECTION_H__
#define __DLIBEXTERN_FACE_LANDMARK_DETECTION_H__

#include "CommonSymbolsWithDlib.h"
#include "DlibExtern.h"
#include <dlib/image_processing/frontal_face_detector.h>
#include <dlib/image_processing/render_face_detections.h>
#include <dlib/image_processing.h>
#include <dlib/gui_widgets.h>
#include <dlib/image_io.h>

class dlib_extern_face_landmark_detection
{
public:
    dlib::frontal_face_detector detector;
    dlib::shape_predictor sp;
    const int PartsLength = 68;

public:
    dlib_extern_face_landmark_detection(const char* shape_predictor_file_name)
    {
        detector = dlib::get_frontal_face_detector();
        dlib::deserialize(shape_predictor_file_name) >> sp;
    }

    ~dlib_extern_face_landmark_detection()
    {
    }

    void Detect(dlib::array2d<dlib::rgb_pixel>* image, double adjust_threshold, std::vector<FaceLandmarkInternal>* dst)
    {
        try
        {
            std::vector<dlib::rectangle> dets = detector(*image, adjust_threshold);
#if _DEBUG
            std::cout << "Number of faces detected: " << dets.size() << std::endl;
#endif

            dst->resize(dets.size());
            for (unsigned long dets_i = 0; dets_i < dets.size(); ++dets_i)
            {
                dlib::full_object_detection shape = sp(*image, dets[dets_i]);
#if _DEBUG
                std::cout << "number of parts: " << shape.num_parts() << std::endl;
                std::cout << "pixel position of first part:  " << shape.part(0) << std::endl;
                std::cout << "pixel position of second part: " << shape.part(1) << std::endl;
#endif
                // You get the idea, you can get all the face part locations if
                // you want them.  Here we just store them in shapes so we can
                // put them on the screen.
                (*dst)[dets_i].rect = shape.get_rect();
                assert(shape.num_parts() == PartsLength);
                for (size_t parts_i = 0; parts_i < PartsLength; parts_i++)
                {
                    (*dst)[dets_i].parts[parts_i] = shape.part(parts_i);
                }
            }
        }
        catch (dlib::error &e)
        {
            if (g_ErrorCallback) g_ErrorCallback(e.what());
        }
    }
};

EXTERN_API dlib_extern_face_landmark_detection* dlib_get_face_landmark_detection(const char* shape_predictor_file_name)
{
    return new dlib_extern_face_landmark_detection(shape_predictor_file_name);
}

EXTERN_API void dlib_face_landmark_detection_delete(dlib_extern_face_landmark_detection* obj)
{
    delete obj;
}

EXTERN_API void dlib_face_landmark_detection_detect(
    dlib_extern_face_landmark_detection* obj,
    dlib::array2d<dlib::rgb_pixel>* image,
    double adjust_threshold,
    std::vector<FaceLandmarkInternal>* dst)
{
    obj->Detect(image, adjust_threshold, dst);
}

#endif
