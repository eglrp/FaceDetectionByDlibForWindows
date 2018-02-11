namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using OpenCvSharp.Extensions;

    public class FaceDetectionContextDlibDnnSemanticSegmentation : FaceDetectionContextBase, IDisposable
    {
        public DlibSharp.DnnSemanticSegmentation DlibDnnSemanticSegmentation { get; private set; }
        public DlibSharp.MatrixRgbPixel Image { get; private set; }

        public FaceDetectionContextDlibDnnSemanticSegmentation()
            : base("DlibDnnSemanticSegmentation", new OpenCvSharp.Scalar(0, 0, 255))
        {
            if (IntPtr.Size == 4)
            {
                System.Windows.MessageBox.Show("To use dlib DNN, x64 CUDA and cuDNN 5.1 are necessary");
                IsEnabled = false;
                return;
            }
            DlibDnnSemanticSegmentation = new DlibSharp.DnnSemanticSegmentation("D:/Data/Dlib/semantic_segmentation_voc2012net.dnn");
            Image = new DlibSharp.MatrixRgbPixel();
        }

        public void DetectFaces(OpenCvSharp.Mat inputColorImage)
        {
            if (IsEnabled == false || DlibDnnSemanticSegmentation == null) { return; }
            Trace.Assert(inputColorImage != null);
            Elapsed.Restart();

            Image.SetBitmap(inputColorImage.ToBitmap());
            DetectedFaceRects = DlibDnnSemanticSegmentation.DetectFaces(Image)
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
                if (DlibDnnSemanticSegmentation != null) { DlibDnnSemanticSegmentation.Dispose(); DlibDnnSemanticSegmentation = null; }
                if (Image != null) { Image.Dispose(); Image = null; }
            }
            // release any unmanaged objects and set the object references to null
            disposed = true;
        }
        ~FaceDetectionContextDlibDnnSemanticSegmentation() { Dispose(false); }
        #endregion
    }
}
