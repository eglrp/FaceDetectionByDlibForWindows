namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Diagnostics;

    public class FrontalFaceDetector : IDisposable
    {
        IntPtr detector = IntPtr.Zero;
        IntPtr dets = IntPtr.Zero;

        void ReleaseDetector() { if (detector != IntPtr.Zero) { NativeMethods.dlib_frontal_face_detector_delete(detector); detector = IntPtr.Zero; } }
        void ReleaseDets() { if (dets != IntPtr.Zero) { NativeMethods.vector_Rect_delete(dets); dets = IntPtr.Zero; } }

        public FrontalFaceDetector()
        {
            detector = NativeMethods.dlib_get_frontal_face_detector();
        }

        public System.Drawing.Rectangle[] DetectFaces(Array2dUchar array2dUchar, double threshold)
        {
            var ret = new System.Drawing.Rectangle[0];
            try
            {
                dets = NativeMethods.vector_Rect_new1();
                NativeMethods.dlib_frontal_face_detector_operator(detector, array2dUchar.DlibArray2dUchar, threshold, dets);
                unsafe
                {
                    Trace.Assert(dets != null && dets != IntPtr.Zero);
                    long count = NativeMethods.vector_Rect_getSize(dets).ToInt64();
                    // If it does not return ret here, exception occurs.
                    if (count == 0) { return ret; }
                    Rect* rectangles = (Rect*)NativeMethods.vector_Rect_getPointer(dets).ToPointer();
                    ret = new System.Drawing.Rectangle[count];
                    for (int i = 0; i < count; i++)
                    {
                        var src = rectangles[i];
                        ret[i] = new System.Drawing.Rectangle(src.X, src.Y, src.Width, src.Height);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                ReleaseDets();
            }
            return ret;
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
            ReleaseDets();
            ReleaseDetector();
            disposed = true;
        }
        ~FrontalFaceDetector() { Dispose(false); }
        #endregion
    }

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_get_frontal_face_detector();

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_frontal_face_detector_delete(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_frontal_face_detector_operator(
            IntPtr obj, IntPtr image, double adjust_threshold, IntPtr dst);
    }
}
