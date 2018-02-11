#ifndef __DLIBEXTERN_DNN_SEMANTIC_SEGMENTATION_H__
#define __DLIBEXTERN_DNN_SEMANTIC_SEGMENTATION_H__

#include "CommonSymbolsWithDlib.h"
#include "DlibExtern.h"

#include <dlib/dnn.h>
#include <dlib/data_io.h>
#include <dlib/gui_widgets.h>
#include <dlib/image_transforms.h>

using namespace dlib;

inline bool operator == (const dlib::rgb_pixel& a, const dlib::rgb_pixel& b)
{
    return a.red == b.red && a.green == b.green && a.blue == b.blue;
}

// ----------------------------------------------------------------------------------------

// The PASCAL VOC2012 dataset contains 20 ground-truth classes + background.  Each class
// is represented using an RGB color value.  We associate each class also to an index in the
// range [0, 20], used internally by the network.

struct Voc2012class
{
    Voc2012class(uint16_t index, const dlib::rgb_pixel& rgb_label, const std::string& classlabel)
        : index(index), rgb_label(rgb_label), classlabel(classlabel)
    {
    }

    // The index of the class. In the PASCAL VOC 2012 dataset, indexes from 0 to 20 are valid.
    const uint16_t index = 0;

    // The corresponding RGB representation of the class.
    const dlib::rgb_pixel rgb_label;

    // The label of the class in plain text.
    const std::string classlabel;
};

namespace
{
    constexpr int class_count = 21; // background + 20 classes

    const std::vector<Voc2012class> classes = {
        // background
        Voc2012class(0, dlib::rgb_pixel(0, 0, 0), ""),

        // The cream-colored `void' label is used in border regions and to mask difficult objects
        // (see http://host.robots.ox.ac.uk/pascal/VOC/voc2012/htmldoc/devkit_doc.html)
        Voc2012class(dlib::loss_multiclass_log_per_pixel_::label_to_ignore,
        dlib::rgb_pixel(224, 224, 192), "border"),

        Voc2012class(1,  dlib::rgb_pixel(128,   0,   0), "aeroplane"),
        Voc2012class(2,  dlib::rgb_pixel(0, 128,   0), "bicycle"),
        Voc2012class(3,  dlib::rgb_pixel(128, 128,   0), "bird"),
        Voc2012class(4,  dlib::rgb_pixel(0,   0, 128), "boat"),
        Voc2012class(5,  dlib::rgb_pixel(128,   0, 128), "bottle"),
        Voc2012class(6,  dlib::rgb_pixel(0, 128, 128), "bus"),
        Voc2012class(7,  dlib::rgb_pixel(128, 128, 128), "car"),
        Voc2012class(8,  dlib::rgb_pixel(64,   0,   0), "cat"),
        Voc2012class(9,  dlib::rgb_pixel(192,   0,   0), "chair"),
        Voc2012class(10, dlib::rgb_pixel(64, 128,   0), "cow"),
        Voc2012class(11, dlib::rgb_pixel(192, 128,   0), "diningtable"),
        Voc2012class(12, dlib::rgb_pixel(64,   0, 128), "dog"),
        Voc2012class(13, dlib::rgb_pixel(192,   0, 128), "horse"),
        Voc2012class(14, dlib::rgb_pixel(64, 128, 128), "motorbike"),
        Voc2012class(15, dlib::rgb_pixel(192, 128, 128), "person"),
        Voc2012class(16, dlib::rgb_pixel(0,  64,   0), "pottedplant"),
        Voc2012class(17, dlib::rgb_pixel(128,  64,   0), "sheep"),
        Voc2012class(18, dlib::rgb_pixel(0, 192,   0), "sofa"),
        Voc2012class(19, dlib::rgb_pixel(128, 192,   0), "train"),
        Voc2012class(20, dlib::rgb_pixel(0,  64, 128), "tvmonitor"),
    };
}

template <typename Predicate>
const Voc2012class& find_voc2012_class(Predicate predicate)
{
    const auto i = std::find_if(classes.begin(), classes.end(), predicate);

    if (i != classes.end())
    {
        return *i;
    }
    else
    {
        throw std::runtime_error("Unable to find a matching VOC2012 class");
    }
}

// ----------------------------------------------------------------------------------------

// Introduce the building blocks used to define the segmentation network.
// The network first does residual downsampling (similar to the dnn_imagenet_(train_)ex 
// example program), and then residual upsampling. The network could be improved e.g.
// by introducing skip connections from the input image, and/or the first layers, to the
// last layer(s).  (See Long et al., Fully Convolutional Networks for Semantic Segmentation,
// https://people.eecs.berkeley.edu/~jonlong/long_shelhamer_fcn.pdf)

template <int N, template <typename> class BN, int stride, typename SUBNET>
using block = BN<dlib::con<N, 3, 3, 1, 1, dlib::relu<BN<dlib::con<N, 3, 3, stride, stride, SUBNET>>>>>;

template <int N, template <typename> class BN, int stride, typename SUBNET>
using blockt = BN<dlib::cont<N, 3, 3, 1, 1, dlib::relu<BN<dlib::cont<N, 3, 3, stride, stride, SUBNET>>>>>;

template <template <int, template<typename>class, int, typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual = dlib::add_prev1<block<N, BN, 1, dlib::tag1<SUBNET>>>;

template <template <int, template<typename>class, int, typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual_down = dlib::add_prev2<dlib::avg_pool<2, 2, 2, 2, dlib::skip1<dlib::tag2<block<N, BN, 2, dlib::tag1<SUBNET>>>>>>;

template <template <int, template<typename>class, int, typename> class block, int N, template<typename>class BN, typename SUBNET>
using residual_up = dlib::add_prev2<dlib::cont<N, 2, 2, 2, 2, dlib::skip1<dlib::tag2<blockt<N, BN, 2, dlib::tag1<SUBNET>>>>>>;

template <int N, typename SUBNET> using res = dlib::relu<residual<block, N, dlib::bn_con, SUBNET>>;
template <int N, typename SUBNET> using ares = dlib::relu<residual<block, N, dlib::affine, SUBNET>>;
template <int N, typename SUBNET> using res_down = dlib::relu<residual_down<block, N, dlib::bn_con, SUBNET>>;
template <int N, typename SUBNET> using ares_down = dlib::relu<residual_down<block, N, dlib::affine, SUBNET>>;
template <int N, typename SUBNET> using res_up = dlib::relu<residual_up<block, N, dlib::bn_con, SUBNET>>;
template <int N, typename SUBNET> using ares_up = dlib::relu<residual_up<block, N, dlib::affine, SUBNET>>;

// ----------------------------------------------------------------------------------------

template <typename SUBNET> using res512 = res<512, SUBNET>;
template <typename SUBNET> using res256 = res<256, SUBNET>;
template <typename SUBNET> using res128 = res<128, SUBNET>;
template <typename SUBNET> using res64 = res<64, SUBNET>;
template <typename SUBNET> using ares512 = ares<512, SUBNET>;
template <typename SUBNET> using ares256 = ares<256, SUBNET>;
template <typename SUBNET> using ares128 = ares<128, SUBNET>;
template <typename SUBNET> using ares64 = ares<64, SUBNET>;


template <typename SUBNET> using level1 = dlib::repeat<2, res512, res_down<512, SUBNET>>;
template <typename SUBNET> using level2 = dlib::repeat<2, res256, res_down<256, SUBNET>>;
template <typename SUBNET> using level3 = dlib::repeat<2, res128, res_down<128, SUBNET>>;
template <typename SUBNET> using level4 = dlib::repeat<2, res64, res<64, SUBNET>>;

template <typename SUBNET> using alevel1 = dlib::repeat<2, ares512, ares_down<512, SUBNET>>;
template <typename SUBNET> using alevel2 = dlib::repeat<2, ares256, ares_down<256, SUBNET>>;
template <typename SUBNET> using alevel3 = dlib::repeat<2, ares128, ares_down<128, SUBNET>>;
template <typename SUBNET> using alevel4 = dlib::repeat<2, ares64, ares<64, SUBNET>>;

template <typename SUBNET> using level1t = dlib::repeat<2, res512, res_up<512, SUBNET>>;
template <typename SUBNET> using level2t = dlib::repeat<2, res256, res_up<256, SUBNET>>;
template <typename SUBNET> using level3t = dlib::repeat<2, res128, res_up<128, SUBNET>>;
template <typename SUBNET> using level4t = dlib::repeat<2, res64, res_up<64, SUBNET>>;

template <typename SUBNET> using alevel1t = dlib::repeat<2, ares512, ares_up<512, SUBNET>>;
template <typename SUBNET> using alevel2t = dlib::repeat<2, ares256, ares_up<256, SUBNET>>;
template <typename SUBNET> using alevel3t = dlib::repeat<2, ares128, ares_up<128, SUBNET>>;
template <typename SUBNET> using alevel4t = dlib::repeat<2, ares64, ares_up<64, SUBNET>>;

// ----------------------------------------------------------------------------------------

// training network type
using net_type = dlib::loss_multiclass_log_per_pixel<
    dlib::cont<class_count, 7, 7, 2, 2,
    level4t<level3t<level2t<level1t<
    level1<level2<level3<level4<
    dlib::max_pool<3, 3, 2, 2, dlib::relu<dlib::bn_con<dlib::con<64, 7, 7, 2, 2,
    dlib::input<dlib::matrix<dlib::rgb_pixel>>
    >>>>>>>>>>>>>>;

// testing network type (replaced batch normalization with fixed affine transforms)
using anet_type = dlib::loss_multiclass_log_per_pixel<
    dlib::cont<class_count, 7, 7, 2, 2,
    alevel4t<alevel3t<alevel2t<alevel1t<
    alevel1<alevel2<alevel3<alevel4<
    dlib::max_pool<3, 3, 2, 2, dlib::relu<dlib::affine<dlib::con<64, 7, 7, 2, 2,
    dlib::input<dlib::matrix<dlib::rgb_pixel>>
    >>>>>>>>>>>>>>;


const Voc2012class& find_voc2012_class(const uint16_t& index_label)
{
    return find_voc2012_class(
        [&index_label](const Voc2012class& voc2012class)
    {
        return index_label == voc2012class.index;
    }
    );
}

// Convert an index in the range [0, 20] to a corresponding RGB class label.
inline rgb_pixel index_label_to_rgb_label(uint16_t index_label)
{
    return find_voc2012_class(index_label).rgb_label;
}

// Convert an image containing indexes in the range [0, 20] to a corresponding
// image containing RGB class labels.
void index_label_image_to_rgb_label_image(
    const matrix<uint16_t>& index_label_image,
    matrix<rgb_pixel>& rgb_label_image
)
{
    const long nr = index_label_image.nr();
    const long nc = index_label_image.nc();

    rgb_label_image.set_size(nr, nc);

    for (long r = 0; r < nr; ++r)
    {
        for (long c = 0; c < nc; ++c)
        {
            rgb_label_image(r, c) = index_label_to_rgb_label(index_label_image(r, c));
        }
    }
}

// Find the most prominent class label from amongst the per-pixel predictions.
std::string get_most_prominent_non_background_classlabel(const matrix<uint16_t>& index_label_image)
{
    const long nr = index_label_image.nr();
    const long nc = index_label_image.nc();

    std::vector<unsigned int> counters(class_count);

    for (long r = 0; r < nr; ++r)
    {
        for (long c = 0; c < nc; ++c)
        {
            const uint16_t label = index_label_image(r, c);
            ++counters[label];
        }
    }

    const auto max_element = std::max_element(counters.begin() + 1, counters.end());
    const uint16_t most_prominent_index_label = max_element - counters.begin();

    return find_voc2012_class(most_prominent_index_label).classlabel;
}



class dlib_extern_dnn_semantic_segmentation
{
public:
    anet_type detector;

    image_window win;
    matrix<uint16_t> index_label_image;
    matrix<rgb_pixel> rgb_label_image;

public:
    dlib_extern_dnn_semantic_segmentation(const char* file_name)
    {
        deserialize(file_name) >> detector;
    }

    ~dlib_extern_dnn_semantic_segmentation()
    {
        detector.clean();
        std::cout << "dlib_extern_dnn_semantic_segmentation destructor called" << std::endl;
    }

public:
    void Detect(matrix<rgb_pixel> *image, std::vector<Rect> *dst)
    {
        matrix<rgb_pixel>& input_image = *image;
        try
        {
            auto dets = detector(*image);

            const matrix<uint16_t> temp = detector(input_image);

            // Crop the returned image to be exactly the same size as the input.
            const chip_details chip_details(
                centered_rect(temp.nc() / 2, temp.nr() / 2, input_image.nc(), input_image.nr()),
                chip_dims(input_image.nr(), input_image.nc())
            );
            extract_image_chip(temp, chip_details, index_label_image, interpolate_nearest_neighbor());

            // Convert the indexes to RGB values.
            index_label_image_to_rgb_label_image(index_label_image, rgb_label_image);

            // Show the input image on the left, and the predicted RGB labels on the right.
            win.set_image(join_rows(input_image, rgb_label_image));

            // Find the most prominent class label from amongst the per-pixel predictions.
            const std::string classlabel = get_most_prominent_non_background_classlabel(index_label_image);

            std::cout << classlabel << " - hit enter to process the next image";
        }
        catch (error &e)
        {
            if (g_ErrorCallback) g_ErrorCallback(e.what());
        }
    }
};

EXTERN_API dlib_extern_dnn_semantic_segmentation* dlib_dnn_semantic_segmentation_construct(const char *file_name)
{
    return new dlib_extern_dnn_semantic_segmentation(file_name);
}

EXTERN_API void dlib_dnn_semantic_segmentation_delete(dlib_extern_dnn_semantic_segmentation *obj)
{
    delete obj;
}

EXTERN_API void dlib_dnn_semantic_segmentation_operator(dlib_extern_dnn_semantic_segmentation *obj, matrix<rgb_pixel> *image, std::vector<Rect> *dst)
{
    obj->Detect(image, dst);
}

#endif
