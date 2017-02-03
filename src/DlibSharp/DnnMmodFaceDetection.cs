namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DnnMmodFaceDetection : IDisposable
    {
        IntPtr detector = IntPtr.Zero;
        IntPtr image = IntPtr.Zero;
        IntPtr dets = IntPtr.Zero;

        void ReleaseDetector() { if (image != IntPtr.Zero) { NativeMethods.dlib_array2d_uchar_delete(image); image = IntPtr.Zero; } }
        void ReleaseImage() { if (detector != IntPtr.Zero) { NativeMethods.dlib_dnn_mmod_face_detection_delete(detector); detector = IntPtr.Zero; } }
        void ReleaseDets() { if (dets != IntPtr.Zero) { NativeMethods.vector_Rect_delete(dets); dets = IntPtr.Zero; } }

        public DnnMmodFaceDetection()
        {
            detector = NativeMethods.dlib_dnn_mmod_face_detection_construct();
            image = NativeMethods.dlib_array2d_uchar_new();
        }

        public OpenCvSharp.Rect[] DetectFaces(OpenCvSharp.Mat inputImage)
        {
            OpenCvSharp.Rect[] ret = new OpenCvSharp.Rect[0];
            byte[] imageBytes;
            try
            {
                if (false)
                {
                    const string imagePath = "../../../../../ThirdParty/dlibsharp/src/DlibSharp.Tests/images/lenna.bmp";
                    imageBytes = System.IO.File.ReadAllBytes(imagePath);
                }
                else
                {
                    imageBytes = inputImage.ToBytes(".bmp");
                }
                NativeMethods.dlib_load_bmp_array2d_uchar(image, imageBytes, new IntPtr(imageBytes.Length));

                //NativeMethods.dlib_pyramid_up_array2d_uchar(image);

                dets = NativeMethods.vector_Rect_new1();
                NativeMethods.dlib_dnn_mmod_face_detection_operator(detector, image, dets);
                unsafe
                {
                    long count = NativeMethods.vector_Rect_getSize(dets).ToInt64();
                    if (count == 0)
                    {
                        // If it does not return ret here, exception occurs.
                        return ret;
                    }
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
                //OnDisposing(EventArgs.Empty);
            }
            // release any unmanaged objects and set the object references to null
            ReleaseDetector();
            ReleaseImage();
            ReleaseDets();
            if (dets != IntPtr.Zero) { NativeMethods.vector_Rect_delete(dets); dets = IntPtr.Zero; }
            disposed = true;
            //OnDisposed(EventArgs.Empty);
        }
        ~DnnMmodFaceDetection() { Dispose(false); }
        #endregion
    }
}
