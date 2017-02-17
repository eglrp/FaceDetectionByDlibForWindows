namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Diagnostics;

    public class ImageWindow : IDisposable
    {
        internal IntPtr DlibImageWindow { get; private set; }

        public ImageWindow()
        {
            DlibImageWindow = NativeMethods.dlib_image_window_new();
        }

        public void SetTitle(string title)
        {
            NativeMethods.dlib_image_window_set_title(DlibImageWindow, title);
        }

        public void SetImage(Array2dUchar image)
        {
            NativeMethods.dlib_image_window_set_image_array2d_uchar(DlibImageWindow, image.DlibArray2dUchar);
        }

        public void SetImage(MatrixRgbPixel image)
        {
            NativeMethods.dlib_image_window_set_image_matrix_rgbpixel(DlibImageWindow, image.DlibMatrixRgbPixel);
        }

        #region IDisposable
        private bool disposed = false;
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) { return; }
            if (disposing)
            {
                // dispose managed objects, and dispose objects that implement IDisposable
            }
            // release any unmanaged objects and set the object references to null
            if (DlibImageWindow != IntPtr.Zero) { NativeMethods.dlib_image_window_delete(DlibImageWindow); DlibImageWindow = IntPtr.Zero; }
            disposed = true;
        }
        ~ImageWindow() { Dispose(false); }
        #endregion
    }

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_image_window_new();

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_image_window_delete(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_image_window_set_title(IntPtr obj, string title);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_image_window_set_image_array2d_uchar(IntPtr obj, IntPtr image);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_image_window_set_image_matrix_rgbpixel(IntPtr obj, IntPtr image);

#if false
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_image_window_clear_overlay(IntPtr obj);
#endif
    }
}
