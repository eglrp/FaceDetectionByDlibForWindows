namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using OpenCvSharp;

    class FaceDetectionContextModelsTest
    {
        public VideoCapture Capture { get; set; }
        public Mat SourceBgr { get; private set; }
        public Mat ResultBgr { get; private set; }
        public FaceDetectionContextDlibDnnMmod FaceDetectionContextDlibDnnMmod { get; private set; }
        Window resultWnd = null;

        public FaceDetectionContextModelsTest()
        {
            FaceDetectionContextDlibDnnMmod = new FaceDetectionContextDlibDnnMmod();

            ResultBgr = new Mat();
            resultWnd = new Window("Result");
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
                for (int i = 0; i < 100; i++)
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
                    FaceDetectionContextDlibDnnMmod.DrawResultAsRectangle(ResultBgr);
                    resultWnd.ShowImage(ResultBgr);
                    Cv2.WaitKey(1);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }
            if (Capture != null) { Capture.Release(); Capture.Dispose(); Capture = null; }
        }
    }
}
