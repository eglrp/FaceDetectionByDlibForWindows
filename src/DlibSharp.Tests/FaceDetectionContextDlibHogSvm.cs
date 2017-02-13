namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    public class FaceDetectionContextDlibHogSvm : FaceDetectionContextBase, IDisposable
    {
        public DlibSharp.FrontalFaceDetector DlibHogSvm { get; private set; }

        public FaceDetectionContextDlibHogSvm()
            : base("DlibHogSvm", new OpenCvSharp.Scalar(0, 255, 0))
        {
            DlibHogSvm = new DlibSharp.FrontalFaceDetector();
        }

        public void DetectFaces(OpenCvSharp.Mat inputColorImage, double threshold)
        {
            if (IsEnabled == false) { return; }
            Trace.Assert(inputColorImage != null);
            Elapsed.Restart();

            DetectedFaceRects = DlibHogSvm.DetectFaces(inputColorImage, threshold);

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
                if (DlibHogSvm != null) { DlibHogSvm.Dispose(); DlibHogSvm = null; }
            }
            // release any unmanaged objects and set the object references to null
            disposed = true;
        }
        ~FaceDetectionContextDlibHogSvm() { Dispose(false); }
        #endregion
    }
}
