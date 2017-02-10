namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    public class DnnMmodFaceDetection : IDisposable
    {
        IntPtr detector = IntPtr.Zero;
        IntPtr image = IntPtr.Zero;
        IntPtr dets = IntPtr.Zero;

        public static int GetDevicesCount() { return NativeMethods.dlib_cuda_get_num_devices(); }

        void ReleaseDetector() { if (image != IntPtr.Zero) { NativeMethods.dlib_matrix_rgbpixel_delete(image); image = IntPtr.Zero; } }
        void ReleaseImage() { if (detector != IntPtr.Zero) { NativeMethods.dlib_dnn_mmod_face_detection_delete(detector); detector = IntPtr.Zero; } }
        void ReleaseDets() { if (dets != IntPtr.Zero) { NativeMethods.vector_Rect_delete(dets); dets = IntPtr.Zero; } }

        public DnnMmodFaceDetection(string mmodHumanFaceDetectorDataFilePath)
        {
            if (System.IO.File.Exists(mmodHumanFaceDetectorDataFilePath) == false)
            {
                throw new System.IO.FileNotFoundException("The training data used to create the model is also available at " + Environment.NewLine + "http://dlib.net/files/data/dlib_face_detection_dataset-2016-09-30.tar.gz", mmodHumanFaceDetectorDataFilePath);
            }
            detector = NativeMethods.dlib_dnn_mmod_face_detection_construct(mmodHumanFaceDetectorDataFilePath);
            image = NativeMethods.dlib_matrix_rgbpixel_new();
        }

        public OpenCvSharp.Rect[] DetectFaces(OpenCvSharp.Mat inputImage)
        {
            OpenCvSharp.Rect[] ret = new OpenCvSharp.Rect[0];
            try
            {
                byte[] imageBytes = inputImage.ToBytes(".bmp");
                NativeMethods.dlib_load_bmp_matrix_rgbpixel(image, imageBytes, new IntPtr(imageBytes.Length));

                dets = NativeMethods.vector_Rect_new1();
                NativeMethods.dlib_dnn_mmod_face_detection_operator(detector, image, dets);
                unsafe
                {
                    Trace.Assert(dets != null && dets != IntPtr.Zero);
                    long count = NativeMethods.vector_Rect_getSize(dets).ToInt64();
                    // If it does not return ret here, exception occurs.
                    if (count == 0) { return ret; }
                    Rect* rectangles = (Rect*)NativeMethods.vector_Rect_getPointer(dets).ToPointer();
                    ret = new OpenCvSharp.Rect[count];
                    for (int i = 0; i < count; i++)
                    {
                        var src = rectangles[i];
                        ret[i] = new OpenCvSharp.Rect(src.X, src.Y, src.Width, src.Height);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
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
            ReleaseImage();
            ReleaseDetector();
            disposed = true;
        }
        ~DnnMmodFaceDetection() { Dispose(false); }
        #endregion
    }
}
