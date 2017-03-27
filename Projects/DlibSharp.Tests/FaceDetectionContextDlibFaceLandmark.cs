namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using OpenCvSharp.Extensions;

    public class FaceDetectionContextDlibFaceLandmark : FaceDetectionContextBase, IDisposable
    {
        public DlibSharp.FaceLandmarkDetection DlibFaceLandmark { get; private set; }
        public DlibSharp.Array2dRgbPixel Image { get; private set; }
        public DlibSharp.FaceLandmark[] FaceLandmarkArray { get; private set; }

        public FaceDetectionContextDlibFaceLandmark()
                : base("DlibFaceLandmark", new OpenCvSharp.Scalar(255, 0, 0))
        {
            DlibFaceLandmark = new DlibSharp.FaceLandmarkDetection("D:/Data/Dlib/shape_predictor_68_face_landmarks.dat");
            Image = new DlibSharp.Array2dRgbPixel();
        }

        public void DetectFaceLandmarks(OpenCvSharp.Mat inputColorImage, double threshold)
        {
            if (IsEnabled == false) { return; }
            Trace.Assert(inputColorImage != null);
            Elapsed.Restart();

            Image.SetBitmap(inputColorImage.ToBitmap());
            FaceLandmarkArray = DlibFaceLandmark.DetectFaceLandmarks(Image, threshold);
            DetectedFaceRects = FaceLandmarkArray.Select(e => new OpenCvSharp.Rect(e.Rect.X, e.Rect.Y, e.Rect.Width, e.Rect.Height));

            Elapsed.Stop();
            var fps = (1000.0 / (double)Elapsed.ElapsedMilliseconds);
            FpsFiltered = 0.7 * FpsFiltered + 0.3 * fps;
        }

        OpenCvSharp.Scalar lineColor { get; set; } = new OpenCvSharp.Scalar(255, 0, 0);
        void DrawLine(OpenCvSharp.Mat resultImage, System.Drawing.Point startPoint, System.Drawing.Point endPoint)
        {
            OpenCvSharp.Cv2.Line(resultImage, new OpenCvSharp.Point(startPoint.X, startPoint.Y), new OpenCvSharp.Point(endPoint.X, endPoint.Y), lineColor);
        }

        public void DrawResultLandmarks(OpenCvSharp.Mat resultImage)
        {
            if (IsEnabled == false) { return; }
            if (DetectedFaceRects == null) { return; }
            Trace.Assert(resultImage != null);
            foreach (var d in FaceLandmarkArray)
            {
                // Around Chin. Ear to Ear
                for (int i = 1; i <= 16; i++) DrawLine(resultImage, d.Parts[i], d.Parts[i - 1]);
                // Line on top of nose
                for (int i = 28; i <= 30; i++) DrawLine(resultImage, d.Parts[i], d.Parts[i - 1]);
                // left eyebrow
                for (int i = 18; i <= 21; i++) DrawLine(resultImage, d.Parts[i], d.Parts[i - 1]);
                // Right eyebrow
                for (int i = 23; i <= 26; i++) DrawLine(resultImage, d.Parts[i], d.Parts[i - 1]);
                // Bottom part of the nose
                for (int i = 31; i <= 35; i++) DrawLine(resultImage, d.Parts[i], d.Parts[i - 1]);
                // Line from the nose to the bottom part above
                DrawLine(resultImage, d.Parts[30], d.Parts[35]);

                // Left eye
                for (int i = 37; i <= 41; i++) DrawLine(resultImage, d.Parts[i], d.Parts[i - 1]);
                DrawLine(resultImage, d.Parts[36], d.Parts[41]);

                // Right eye
                for (int i = 43; i <= 47; i++) DrawLine(resultImage, d.Parts[i], d.Parts[i - 1]);
                DrawLine(resultImage, d.Parts[42], d.Parts[47]);

                // Lips outer part
                for (int i = 49; i <= 59; i++) DrawLine(resultImage, d.Parts[i], d.Parts[i - 1]);
                DrawLine(resultImage, d.Parts[48], d.Parts[59]);

                // Lips inside part
                for (int i = 61; i <= 67; i++) DrawLine(resultImage, d.Parts[i], d.Parts[i - 1]);
                DrawLine(resultImage, d.Parts[60], d.Parts[67]);
            }
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
                if (DlibFaceLandmark != null) { DlibFaceLandmark.Dispose(); DlibFaceLandmark = null; }
                if (Image != null) { Image.Dispose(); Image = null; }
            }
            // release any unmanaged objects and set the object references to null
            disposed = true;
        }
        ~FaceDetectionContextDlibFaceLandmark() { Dispose(false); }
        #endregion
    }
}
