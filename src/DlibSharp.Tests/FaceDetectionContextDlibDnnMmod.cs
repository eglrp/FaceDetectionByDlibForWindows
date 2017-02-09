namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using NUnit.Framework;

    public class FaceDetectionContextDlibDnnMmod : FaceDetectionContextBase
    {
        public DlibSharp.DnnMmodFaceDetection DlibDnnMmod { get; private set; }

        public FaceDetectionContextDlibDnnMmod()
            : base("DlibDnnMmod", new OpenCvSharp.Scalar(0, 255, 0))
        {
            DlibDnnMmod = new DlibSharp.DnnMmodFaceDetection();
        }

        public void DetectFaces(OpenCvSharp.Mat inputColorImage)
        {
            if (IsEnabled == false) { return; }
            Trace.Assert(inputColorImage != null);
            Elapsed.Restart();

            DetectedFaceRects = DlibDnnMmod.DetectFaces(inputColorImage);

            Elapsed.Stop();
            var fps = (1000.0 / (double)Elapsed.ElapsedMilliseconds);
            FpsFiltered = 0.7 * FpsFiltered + 0.3 * fps;
        }
    }
}
