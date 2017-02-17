namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using OpenCvSharp;

    public class FaceDetectionContextCascadeClassifier : FaceDetectionContextBase
    {
        public CascadeClassifier HaarCascade { get; private set; }

        public FaceDetectionContextCascadeClassifier(string name, Scalar resultLineColor, string xmlFileName)
            : base(name, resultLineColor)
        {
            HaarCascade = new CascadeClassifier(xmlFileName);
        }

        public void DetectFaces(Mat inputGrayImage, double scaleFactor, int numberOfNeighbors, Size minSize)
        {
            if (IsEnabled == false) { return; }
            Trace.Assert(inputGrayImage != null);
            Elapsed.Restart();

            DetectedFaceRects = HaarCascade.DetectMultiScale(inputGrayImage, scaleFactor, numberOfNeighbors, HaarDetectionType.ScaleImage, minSize);

            Elapsed.Stop();
            var fps = (1000.0 / (double)Elapsed.ElapsedMilliseconds);
            FpsFiltered = 0.7 * FpsFiltered + 0.3 * fps;
        }
    }
}
