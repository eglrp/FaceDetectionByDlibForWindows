namespace DlibSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Diagnostics;

    public class FrontalFaceDetector : IDisposable
    {
        IntPtr detector = IntPtr.Zero;
        IntPtr image = IntPtr.Zero;
        IntPtr dets = IntPtr.Zero;

        void ReleaseDetector() { if (image != IntPtr.Zero) { NativeMethods.dlib_array2d_uchar_delete(image); image = IntPtr.Zero; } }
        void ReleaseImage() { if (detector != IntPtr.Zero) { NativeMethods.dlib_frontal_face_detector_delete(detector); detector = IntPtr.Zero; } }
        void ReleaseDets() { if (dets != IntPtr.Zero) { NativeMethods.vector_Rect_delete(dets); dets = IntPtr.Zero; } }

        public FrontalFaceDetector()
        {
            detector = NativeMethods.dlib_get_frontal_face_detector();
            image = NativeMethods.dlib_array2d_uchar_new();
        }

        public System.Drawing.Rectangle[] DetectFaces(System.Drawing.Bitmap inputImage, double threshold)
        {
            var ret = new System.Drawing.Rectangle[0];
            try
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    inputImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] imageBytes = stream.ToArray();
                    NativeMethods.dlib_load_bmp_array2d_uchar(image, imageBytes, new IntPtr(imageBytes.Length));
                }

                dets = NativeMethods.vector_Rect_new1();
                NativeMethods.dlib_frontal_face_detector_operator(detector, image, threshold, dets);
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
            ReleaseImage();
            ReleaseDetector();
            disposed = true;
        }
        ~FrontalFaceDetector() { Dispose(false); }
        #endregion
    }
}
