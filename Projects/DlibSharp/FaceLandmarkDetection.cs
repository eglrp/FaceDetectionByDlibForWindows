namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Diagnostics;

    public class FaceLandmarkDetection : IDisposable
    {
        IntPtr detector = IntPtr.Zero;
        IntPtr dets = IntPtr.Zero;

        void ReleaseDetector() { if (detector != IntPtr.Zero) { NativeMethods.dlib_face_landmark_detection_delete(detector); detector = IntPtr.Zero; } }
        void ReleaseDets() { if (dets != IntPtr.Zero) { NativeMethods.vector_FaceLandmark_delete(dets); dets = IntPtr.Zero; } }

        public FaceLandmarkDetection()
        {
            detector = NativeMethods.dlib_get_face_landmark_detection();
        }

        public FaceLandmark[] DetectFaceLandmarks(Array2dUchar array2dUchar, double faceDetectionThreshold)
        {
            var ret = new FaceLandmark[0];
            try
            {
                dets = NativeMethods.vector_FaceLandmark_new1();
                NativeMethods.dlib_face_landmark_detection_operator(detector, array2dUchar.DlibArray2dUchar, faceDetectionThreshold, dets);
                Trace.Assert(dets != null && dets != IntPtr.Zero);
                long count = NativeMethods.vector_FaceLandmark_getSize(dets).ToInt64();
                // If it does not return ret here, exception occurs.
                if (count == 0) { return ret; }
                var ptr = NativeMethods.vector_FaceLandmark_getPointer(dets);
                if (true)
                {
                    ret = new FaceLandmark[count];
                    Marshal.PtrToStructure(ptr, ret);
                }
                else
                {
                    ret = (FaceLandmark[])Marshal.PtrToStructure(ptr, typeof(FaceLandmark[]));
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debugger.Break();
#endif
                Console.WriteLine(ex.Message);
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
        ~FaceLandmarkDetection() { Dispose(false); }
        #endregion
    }

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_get_face_landmark_detection();

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_face_landmark_detection_delete(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_face_landmark_detection_operator(
            IntPtr obj, IntPtr image, double adjust_threshold, IntPtr dst);
    }
}
