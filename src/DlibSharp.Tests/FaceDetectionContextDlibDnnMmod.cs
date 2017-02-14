namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using OpenCvSharp.Extensions;

    public class FaceDetectionContextDlibDnnMmod : FaceDetectionContextBase, IDisposable
    {
        public DlibSharp.DnnMmodFaceDetection DlibDnnMmod { get; private set; }

        public FaceDetectionContextDlibDnnMmod()
            : base("DlibDnnMmod", new OpenCvSharp.Scalar(0, 0, 255))
        {
            DlibDnnMmod = new DlibSharp.DnnMmodFaceDetection("./data/mmod_human_face_detector.dat");
        }

        public void DetectFaces(OpenCvSharp.Mat inputColorImage)
        {
            if (IsEnabled == false) { return; }
            Trace.Assert(inputColorImage != null);
            Elapsed.Restart();

            DetectedFaceRects = DlibDnnMmod.DetectFaces(inputColorImage.ToBitmap())
                .Select(e => new OpenCvSharp.Rect(e.X, e.Y, e.Width, e.Height));

            Elapsed.Stop();
            var fps = (1000.0 / (double)Elapsed.ElapsedMilliseconds);
            FpsFiltered = 0.7 * FpsFiltered + 0.3 * fps;
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
                if (DlibDnnMmod != null) { DlibDnnMmod.Dispose(); DlibDnnMmod = null; }
            }
            // release any unmanaged objects and set the object references to null
            disposed = true;
        }
        ~FaceDetectionContextDlibDnnMmod() { Dispose(false); }
        #endregion
    }
}
