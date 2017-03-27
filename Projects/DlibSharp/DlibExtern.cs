namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ErrorCallback([MarshalAs(UnmanagedType.LPStr)] string what);

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
#if WIN64
        const string DlibExternDllPath = @".\dll\x64\DlibExtern.dll";
#else
        const string DlibExternDllPath = @".\dll\x86\DlibExtern.dll";
#endif

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_set_error_redirect([MarshalAs(UnmanagedType.FunctionPtr)] ErrorCallback callback);
    }

    public enum ResizeImageInterporateKind
    {
        None = 0,
        NearestNeighbor = 1,
        Bilinear = 2,
        Quadratic = 3
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public override string ToString()
        {
            return $"(X:{X} Y:{Y} W:{Width} H:{Height})";
        }
    }

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_new1();

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_new2(IntPtr size);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_new3(IntPtr data, IntPtr dataLength);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_getSize(IntPtr vector);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_Rect_getPointer(IntPtr vector);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void vector_Rect_delete(IntPtr vector);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return $"(X:{X} Y:{Y})";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FaceLandmark
    {
        public const int PartsLength = 68;

        public Rect Rect;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = PartsLength)]
        public Point[] Parts;
    }

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmark_new1();

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmark_new2(IntPtr size);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmark_new3(IntPtr data, IntPtr dataLength);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmark_getSize(IntPtr vector);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmark_getPointer(IntPtr vector);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void vector_FaceLandmark_delete(IntPtr vector);
    }
}
