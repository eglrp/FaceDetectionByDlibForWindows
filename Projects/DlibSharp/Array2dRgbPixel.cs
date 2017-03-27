namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Diagnostics;

    public class Array2dRgbPixel : IDisposable
    {
        internal IntPtr DlibArray2dRgbPixel;

        public Array2dRgbPixel()
        {
            DlibArray2dRgbPixel = NativeMethods.dlib_array2d_rgbpixel_new();
        }

        public Array2dRgbPixel(int width, int height)
        {
            Trace.Assert(width > 0 && height > 0);
            DlibArray2dRgbPixel = NativeMethods.dlib_array2d_rgbpixel_new_width_height(width, height);
        }

        public Array2dRgbPixel(Array2dRgbPixel copyingSource)
        {
            Trace.Assert(copyingSource != null);
            Trace.Assert(copyingSource.DlibArray2dRgbPixel != IntPtr.Zero);
            DlibArray2dRgbPixel = NativeMethods.dlib_array2d_rgbpixel_new_copied(copyingSource.DlibArray2dRgbPixel);
        }

        public Int32 Width { get { return NativeMethods.dlib_array2d_rgbpixel_nc(DlibArray2dRgbPixel); } }
        public Int32 Height { get { return NativeMethods.dlib_array2d_rgbpixel_nr(DlibArray2dRgbPixel); } }

        public void SetBitmap(System.Drawing.Bitmap inputImage)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                inputImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] imageBytes = stream.ToArray();
                NativeMethods.dlib_load_bmp_array2d_rgbpixel(DlibArray2dRgbPixel, imageBytes, new IntPtr(imageBytes.Length));
            }
        }

        public void PyramidUp()
        {
            Trace.Assert(DlibArray2dRgbPixel != IntPtr.Zero);
            NativeMethods.dlib_pyramid_up_array2d_rgbpixel(DlibArray2dRgbPixel);
        }

        public static void ResizeImageWithResizeImageInterporateKind(Array2dRgbPixel src, Array2dRgbPixel dest, ResizeImageInterporateKind kind)
        {
            Trace.Assert(src != null && dest != null);
            Trace.Assert(src.DlibArray2dRgbPixel != IntPtr.Zero);
            Trace.Assert(dest.DlibArray2dRgbPixel != IntPtr.Zero);
            NativeMethods.dlib_resize_image_array2d_rgbpixel_src_dest_interporation_kind(src.DlibArray2dRgbPixel, dest.DlibArray2dRgbPixel, (int)kind);
        }

        public void ResizeImage(int width, int height)
        {
            Trace.Assert(DlibArray2dRgbPixel != IntPtr.Zero);
            NativeMethods.dlib_resize_image_array2d_rgbpixel_width_height(ref DlibArray2dRgbPixel, width, height);
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
            if (DlibArray2dRgbPixel != IntPtr.Zero) { NativeMethods.dlib_array2d_rgbpixel_delete(DlibArray2dRgbPixel); DlibArray2dRgbPixel = IntPtr.Zero; }
            disposed = true;
        }
        ~Array2dRgbPixel() { Dispose(false); }
        #endregion
    }



    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_array2d_rgbpixel_new();

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_array2d_rgbpixel_new_width_height(int width, int height);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_array2d_rgbpixel_new_copied(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_array2d_rgbpixel_delete(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static Int32 dlib_array2d_rgbpixel_nr(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static Int32 dlib_array2d_rgbpixel_nc(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_load_image_array2d_rgbpixel(IntPtr obj, string file_name);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_load_bmp_array2d_rgbpixel(IntPtr obj, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, IntPtr buffer_length);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_pyramid_up_array2d_rgbpixel(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_resize_image_array2d_rgbpixel_src_dest_interporation_kind(IntPtr src, IntPtr dest, int interporation_kind);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_resize_image_array2d_rgbpixel_width_height(ref IntPtr src, int width, int height);
    }
}
