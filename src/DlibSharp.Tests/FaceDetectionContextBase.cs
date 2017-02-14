namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using OpenCvSharp;

    public class FaceDetectionContextBase
    {
        public string Name { get; set; }
        public Scalar ResultLineColor { get; set; }
        public bool IsEnabled { get; set; }
        public IEnumerable<OpenCvSharp.Rect> DetectedFaceRects { get; set; }
        public Stopwatch Elapsed { get; set; }
        public double FpsFiltered { get; set; }

        public FaceDetectionContextBase(string name, Scalar resultLineColor)
        {
            Name = name;
            ResultLineColor = resultLineColor;
            IsEnabled = true;
            Elapsed = new Stopwatch();
            FpsFiltered = 0;
        }

        public void DrawResultAsEllipse(Mat resultImage)
        {
            if (IsEnabled == false) { return; }
            if (DetectedFaceRects == null) { return; }
            Trace.Assert(resultImage != null);
            foreach (var face in DetectedFaceRects)
            {
                var center = new Point
                {
                    X = (int)(face.X + face.Width * 0.5),
                    Y = (int)(face.Y + face.Height * 0.5)
                };
                var axes = new Size
                {
                    Width = (int)(face.Width * 0.5),
                    Height = (int)(face.Height * 0.5)
                };
                Cv2.Ellipse(resultImage, center, axes, 0, 0, 360, ResultLineColor, 3);
            }
        }

        public void DrawResultAsRectangle(Mat resultImage)
        {
            if (IsEnabled == false) { return; }
            if (DetectedFaceRects == null) { return; }
            Trace.Assert(resultImage != null);
            foreach (var result in DetectedFaceRects)
            {
                Cv2.Rectangle(resultImage, result, ResultLineColor, 3);
            }
        }

        public void DrawResultText(Mat resultImage, Point point)
        {
            if (IsEnabled == false) { return; }
            Trace.Assert(resultImage != null);
            var fpsStr = FpsFiltered.ToString("G3");
            resultImage.PutText(Name + ": FPS: " + fpsStr, point, HersheyFonts.HersheyComplex, 0.5, ResultLineColor);
        }
    }
}
