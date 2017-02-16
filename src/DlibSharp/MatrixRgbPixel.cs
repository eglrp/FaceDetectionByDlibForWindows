namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Diagnostics;

    public class MatrixRgbPixel : IDisposable
    {
        internal IntPtr DlibMatrixRgbPixel { get; private set; }

        public MatrixRgbPixel()
        {
            DlibMatrixRgbPixel = NativeMethods.dlib_matrix_rgbpixel_new();
        }

        public Int32 Width { get { return NativeMethods.dlib_matrix_rgbpixel_nc(DlibMatrixRgbPixel); } }
        public Int32 Height { get { return NativeMethods.dlib_matrix_rgbpixel_nr(DlibMatrixRgbPixel); } }

        public void SetBitmap(System.Drawing.Bitmap inputImage)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                inputImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] imageBytes = stream.ToArray();
                NativeMethods.dlib_load_bmp_matrix_rgbpixel(DlibMatrixRgbPixel, imageBytes, new IntPtr(imageBytes.Length));
            }
        }

        public void PyramidUp()
        {
            Trace.Assert(DlibMatrixRgbPixel != IntPtr.Zero);
            NativeMethods.dlib_pyramid_up_matrix_rgbpixel(DlibMatrixRgbPixel);
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
            if (DlibMatrixRgbPixel != IntPtr.Zero) { NativeMethods.dlib_matrix_rgbpixel_delete(DlibMatrixRgbPixel); DlibMatrixRgbPixel = IntPtr.Zero; }
            disposed = true;
        }
        ~MatrixRgbPixel() { Dispose(false); }
        #endregion
    }

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_matrix_rgbpixel_new();

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_matrix_rgbpixel_delete(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static Int32 dlib_matrix_rgbpixel_nr(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static Int32 dlib_matrix_rgbpixel_nc(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_load_image_matrix_rgbpixel(IntPtr obj, string file_name);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_load_bmp_matrix_rgbpixel(IntPtr obj, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, IntPtr buffer_length);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_pyramid_up_matrix_rgbpixel(IntPtr obj);
    }
}
