namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;
    using System.Threading.Tasks;

    // ReSharper disable InconsistentNaming

    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        private const string Dll = "DlibExtern";


        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_set_error_redirect([MarshalAs(UnmanagedType.FunctionPtr)] ErrorCallback callback);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static int dlib_cuda_get_num_devices();


        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_get_frontal_face_detector();

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_frontal_face_detector_delete(IntPtr obj);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_frontal_face_detector_operator(
            IntPtr obj, IntPtr image, double adjust_threshold, IntPtr dst);


        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_array2d_uchar_new();

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_array2d_uchar_delete(IntPtr obj);


        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_load_image_array2d_uchar(IntPtr obj, string file_name);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_load_bmp_array2d_uchar(IntPtr obj, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, IntPtr buffer_length);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_pyramid_up_array2d_uchar(IntPtr obj);


        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_matrix_rgbpixel_new();

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_matrix_rgbpixel_delete(IntPtr obj);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_load_image_matrix_rgbpixel(IntPtr obj, string file_name);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_load_bmp_matrix_rgbpixel(IntPtr obj, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, IntPtr buffer_length);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_pyramid_up_matrix_rgbpixel(IntPtr obj);


        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_dnn_mmod_face_detection_construct(string mmodHumanFaceDetectorDataFilePath);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_dnn_mmod_face_detection_delete(IntPtr obj);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_dnn_mmod_face_detection_operator(IntPtr obj, IntPtr image, IntPtr dst);


        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_new1();

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_new2(IntPtr size);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_new3(IntPtr data, IntPtr dataLength);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_getSize(IntPtr vector);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_getPointer(IntPtr vector);

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void vector_Rect_delete(IntPtr vector);
    }
}
