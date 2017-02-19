#ifndef __DLIBEXTERN_H__
#define __DLIBEXTERN_H__

#include "CommonSymbolsWithDlib.h"
#include <dlib/geometry/rectangle.h>
#include <dlib/dnn/cuda_dlib.h>

// �ȉ��� ifdef �u���b�N�� DLL ����̃G�N�X�|�[�g��e�Ղɂ���}�N�����쐬���邽�߂� 
// ��ʓI�ȕ��@�ł��B���� DLL ���̂��ׂẴt�@�C���́A�R�}���h ���C���Œ�`���ꂽ DLIBEXTERN_EXPORTS
// �V���{�����g�p���ăR���p�C������܂��B���̃V���{���́A���� DLL ���g�p����v���W�F�N�g�ł͒�`�ł��܂���B
// �\�[�X�t�@�C�������̃t�@�C�����܂�ł��鑼�̃v���W�F�N�g�́A 
// DLIBEXTERN_API �֐��� DLL ����C���|�[�g���ꂽ�ƌ��Ȃ��̂ɑ΂��A���� DLL �́A���̃}�N���Œ�`���ꂽ
// �V���{�����G�N�X�|�[�g���ꂽ�ƌ��Ȃ��܂��B
#if defined(DLIBEXTERN_EXPORTS)
#define EXTERN_API extern "C" __declspec(dllexport)
#elif defined(DLIBEXTERN_IMPORTS)
#define EXTERN_API extern "C" __declspec(dllimport)
#else
#define EXTERN_API extern "C"
#endif

using uchar = unsigned char;

using ErrorCallback = void(*)(const char*);
static ErrorCallback g_ErrorCallback;

EXTERN_API ErrorCallback inline dlib_set_error_redirect(const ErrorCallback callback)
{
    auto current_callback = g_ErrorCallback;
    g_ErrorCallback = callback;
    return current_callback;
}

extern "C"
{
    enum ResizeImageInterporateKind
    {
        NearestNeighbor,
        Bilinear,
        Quadratic
    };

    struct Rect
    {
        int32_t x, y, width, height;

        Rect()
            : x(0), y(0), width(0), height(0)
        {}

        Rect(int _x, int _y, int _w, int _h) 
            : x(_x), y(_y), width(_w), height(_h)
        {}

        Rect(const dlib::rectangle &r)
            : x(r.left()), y(r.top()), width(r.width()), height(r.height())
        {}
	};
}

#endif