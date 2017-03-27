#ifndef __DLIBEXTERN_STD_VECTOR__
#define __DLIBEXTERN_STD_VECTOR__

#include "CommonSymbolsWithDlib.h"
#include "DlibExtern.h"
#include <vector>

#pragma region Rect

EXTERN_API std::vector<Rect> *vector_Rect_new1()
{
    return new std::vector<Rect>;
}
EXTERN_API std::vector<Rect> *vector_Rect_new2(size_t size)
{
    return new std::vector<Rect>(size);
}
EXTERN_API std::vector<Rect> *vector_Rect_new3(Rect *data, size_t dataLength)
{
    return new std::vector<Rect>(data, data + dataLength);
}
EXTERN_API size_t vector_Rect_getSize(std::vector<Rect> *vector)
{
    return vector->size();
}
EXTERN_API Rect *vector_Rect_getPointer(std::vector<Rect> *vector)
{
    return &(vector->at(0));
}
EXTERN_API void vector_Rect_delete(std::vector<Rect> *vector)
{
    delete vector;
}

#pragma endregion

#pragma region FaceLandmarkInternal

EXTERN_API std::vector<FaceLandmarkInternal> *vector_FaceLandmarkInternal_new1()
{
    return new std::vector<FaceLandmarkInternal>;
}
EXTERN_API std::vector<FaceLandmarkInternal> *vector_FaceLandmarkInternal_new2(size_t size)
{
    return new std::vector<FaceLandmarkInternal>(size);
}
EXTERN_API std::vector<FaceLandmarkInternal> *vector_FaceLandmarkInternal_new3(FaceLandmarkInternal *data, size_t dataLength)
{
    return new std::vector<FaceLandmarkInternal>(data, data + dataLength);
}
EXTERN_API size_t vector_FaceLandmarkInternal_getSize(std::vector<FaceLandmarkInternal> *vector)
{
    return vector->size();
}
EXTERN_API FaceLandmarkInternal *vector_FaceLandmarkInternal_getPointer(std::vector<FaceLandmarkInternal> *vector)
{
    return &(vector->at(0));
}
EXTERN_API void vector_FaceLandmarkInternal_delete(std::vector<FaceLandmarkInternal> *vector)
{
    delete vector;
}

#pragma endregion

#endif
