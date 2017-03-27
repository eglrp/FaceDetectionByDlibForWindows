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
    internal struct FaceLandmarkInternal
    {
        internal Rect Rect;
        internal unsafe fixed int Parts[FaceLandmark.PartsLength * 2];
    }

    public class FaceLandmark
    {
        public const int PartsLength = 68;
        public System.Drawing.Rectangle Rect { get; set; }
        public System.Drawing.Point[] Parts { get; set; }
        public FaceLandmark()
        {
            Parts = new System.Drawing.Point[PartsLength];
        }
        internal FaceLandmark(FaceLandmarkInternal src)
        {
            Rect = new System.Drawing.Rectangle(src.Rect.X, src.Rect.Y, src.Rect.Width, src.Rect.Height);
            Parts = new System.Drawing.Point[PartsLength];
            unsafe
            {
                for (int i = 0; i < PartsLength; i++)
                {
                    Parts[i].X = src.Parts[i * 2];
                    Parts[i].Y = src.Parts[i * 2 + 1];
                }
            }
        }
    }

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmarkInternal_new1();

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmarkInternal_new2(IntPtr size);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmarkInternal_new3(IntPtr data, IntPtr dataLength);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmarkInternal_getSize(IntPtr vector);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr vector_FaceLandmarkInternal_getPointer(IntPtr vector);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void vector_FaceLandmarkInternal_delete(IntPtr vector);
    }
}
