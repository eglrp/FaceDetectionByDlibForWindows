namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    public class FaceDetectionContextDlibHogSvm : FaceDetectionContextBase
    {
        public DlibSharp.FrontalFaceDetector DlibHogSvm { get; private set; }

        public FaceDetectionContextDlibHogSvm()
            : base("DlibHogSvm", new OpenCvSharp.Scalar(0, 255, 0))
        {
            DlibHogSvm = new DlibSharp.FrontalFaceDetector();
        }

        public void DetectFaces(OpenCvSharp.Mat inputColorImage)
        {
            if (IsEnabled == false) { return; }
            Trace.Assert(inputColorImage != null);
            Elapsed.Restart();

            DetectedFaceRects = DlibHogSvm.DetectFaces(inputColorImage);

            Elapsed.Stop();
            var fps = (1000.0 / (double)Elapsed.ElapsedMilliseconds);
            FpsFiltered = 0.7 * FpsFiltered + 0.3 * fps;
        }
    }
}
