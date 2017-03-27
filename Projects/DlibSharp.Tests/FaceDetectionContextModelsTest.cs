namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using OpenCvSharp;

    class FaceDetectionContextModelsTest : IDisposable
    {
        public VideoCapture Capture { get; set; }
        public Mat SourceBgr { get; private set; }
        public Mat ResultBgr { get; private set; }

        public FaceDetectionContextDlibDnnMmod FaceDetectionContextDlibDnnMmod { get; private set; }
        public FaceDetectionContextDlibHogSvm FaceDetectionContextDlibHogSvm { get; private set; }
        public FaceDetectionContextDlibFaceLandmark FaceDetectionContextDlibFaceLandmark { get; private set; }
        public FaceDetectionContextCascadeClassifier FaceDetectionContextCascadeClassifier { get; private set; }

        Window resultWnd = null;

        public FaceDetectionContextModelsTest()
        {
            ResultBgr = new Mat();

            FaceDetectionContextDlibDnnMmod = new FaceDetectionContextDlibDnnMmod();
            FaceDetectionContextDlibHogSvm = new FaceDetectionContextDlibHogSvm();
            FaceDetectionContextDlibFaceLandmark = new FaceDetectionContextDlibFaceLandmark();
            FaceDetectionContextCascadeClassifier = new FaceDetectionContextCascadeClassifier("HaarCascade", new Scalar(127, 127, 127), "data/haarcascade_frontalface_alt.xml");

            resultWnd = new Window("Result. # of Devices: " + DnnMmodFaceDetection.GetDevicesCount());
        }

        public void StartCameraCapture()
        {
            Capture = new VideoCapture(0);
        }

        public void StartVideoOrImageFileCapture()
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog() != true) { return; }
            Capture = new VideoCapture(ofd.FileName);
        }

        public void RepeatDetection()
        {
            SourceBgr = new Mat();
            try
            {
                while (true)
                {
                    if (Capture.IsOpened() == false) { throw new Exception("capture.IsOpened() == false"); }
                    var retrievedMat = Capture.RetrieveMat();
                    if (retrievedMat.Width > 0 && retrievedMat.Height > 0)
                    {
                        // If the Capture is the image file, 2nd RetrieveMat() returns empty Mat.
                        SourceBgr = retrievedMat;
                    }
                    else
                    {
                        // 何もしなくても良いが、MS API使う場合は何度もAPI呼び出すことになるのでbreakする。
                        break;
                    }
                    ResultBgr = SourceBgr.Clone();

                    FaceDetectionContextDlibDnnMmod.DetectFaces(SourceBgr);
                    FaceDetectionContextDlibHogSvm.DetectFaces(SourceBgr, 0);
                    FaceDetectionContextDlibFaceLandmark.DetectFaceLandmarks(SourceBgr, 0);
                    using (var sourceGray = SourceBgr.CvtColor(ColorConversionCodes.BGR2GRAY))
                    {
                        FaceDetectionContextCascadeClassifier.DetectFaces(sourceGray, 1.08, 5, new Size(25, 25));
                    }

                    FaceDetectionContextCascadeClassifier.DrawResultAsRectangle(ResultBgr);
                    FaceDetectionContextDlibHogSvm.DrawResultAsRectangle(ResultBgr);
                    FaceDetectionContextDlibDnnMmod.DrawResultAsRectangle(ResultBgr);
                    FaceDetectionContextDlibFaceLandmark.DrawResultAsRectangle(ResultBgr);
                    FaceDetectionContextDlibFaceLandmark.DrawResultLandmarks(ResultBgr);

                    FaceDetectionContextDlibDnnMmod.DrawResultText(ResultBgr, new Point(20, 20));
                    FaceDetectionContextDlibHogSvm.DrawResultText(ResultBgr, new Point(20, 40));
                    FaceDetectionContextDlibFaceLandmark.DrawResultText(ResultBgr, new Point(20, 60));
                    FaceDetectionContextCascadeClassifier.DrawResultText(ResultBgr, new Point(20, 80));

                    resultWnd.ShowImage(ResultBgr);

                    var ch = Cv2.WaitKey(1);
                    if (ch == 0x1b) { break; }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }
            if (Capture != null) { Capture.Release(); Capture.Dispose(); Capture = null; }
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
                if (FaceDetectionContextDlibDnnMmod != null) { FaceDetectionContextDlibDnnMmod.Dispose(); FaceDetectionContextDlibDnnMmod = null; }
                if (FaceDetectionContextDlibHogSvm != null) { FaceDetectionContextDlibHogSvm.Dispose(); FaceDetectionContextDlibHogSvm = null; }
                if (FaceDetectionContextDlibFaceLandmark != null) { FaceDetectionContextDlibFaceLandmark.Dispose(); FaceDetectionContextDlibFaceLandmark = null; }
            }
            // release any unmanaged objects and set the object references to null
            disposed = true;
        }
        ~FaceDetectionContextModelsTest() { Dispose(false); }
        #endregion
    }
}
