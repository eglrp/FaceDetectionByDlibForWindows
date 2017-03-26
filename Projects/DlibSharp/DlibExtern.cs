namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

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

    public enum ResizeImageInterporateKind
    {
        None = 0,
        NearestNeighbor = 1,
        Bilinear = 2,
        Quadratic = 3
    }

    public enum ResizeImageInterporateKind
    {
        None = 0,
        NearestNeighbor = 1,
        Bilinear = 2,
        Quadratic = 3
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ErrorCallback([MarshalAs(UnmanagedType.LPStr)] string what);

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
}
