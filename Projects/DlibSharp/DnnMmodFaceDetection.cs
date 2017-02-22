namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Diagnostics;

    public class DnnMmodFaceDetection : IDisposable
    {
        IntPtr detector = IntPtr.Zero;
        IntPtr dets = IntPtr.Zero;

        public static int GetDevicesCount() { return NativeMethods.dlib_cuda_get_num_devices(); }

        void ReleaseDetector() { if (detector != IntPtr.Zero) { NativeMethods.dlib_dnn_mmod_face_detection_delete(detector); detector = IntPtr.Zero; } }
        void ReleaseDets() { if (dets != IntPtr.Zero) { NativeMethods.vector_Rect_delete(dets); dets = IntPtr.Zero; } }

        public DnnMmodFaceDetection(string mmodHumanFaceDetectorDataFilePath)
        {
            if (IntPtr.Size == 4)
            {
                throw new System.PlatformNotSupportedException("x64 CUDA and cuDNN 5.1 are necessary");
            }
            if (System.IO.File.Exists(mmodHumanFaceDetectorDataFilePath) == false)
            {
                throw new System.IO.FileNotFoundException("The training data used to create the model is also available at " + Environment.NewLine + "http://dlib.net/files/data/dlib_face_detection_dataset-2016-09-30.tar.gz", mmodHumanFaceDetectorDataFilePath);
            }
            detector = NativeMethods.dlib_dnn_mmod_face_detection_construct(mmodHumanFaceDetectorDataFilePath);
        }

        public System.Drawing.Rectangle[] DetectFaces(MatrixRgbPixel inputImage)
        {
            var ret = new System.Drawing.Rectangle[0];
            try
            {
                dets = NativeMethods.vector_Rect_new1();
                NativeMethods.dlib_dnn_mmod_face_detection_operator(detector, inputImage.DlibMatrixRgbPixel, dets);
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
        ~DnnMmodFaceDetection() { Dispose(false); }
        #endregion
    }

    [SuppressUnmanagedCodeSecurity]
    internal static partial class NativeMethods
    {
        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static IntPtr dlib_dnn_mmod_face_detection_construct(string mmodHumanFaceDetectorDataFilePath);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_dnn_mmod_face_detection_delete(IntPtr obj);

        [DllImport(DlibExternDllPath, CallingConvention = CallingConvention.Cdecl)]
        extern internal static void dlib_dnn_mmod_face_detection_operator(IntPtr obj, IntPtr image, IntPtr dst);
    }
}
