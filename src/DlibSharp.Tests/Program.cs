namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    static class Program
    {
        static void Main()
        {
            Test03();
        }

        static void Test03()
        {
            var test = new FaceDetector();
            test.TestFrontalFaceDetector();
            test.TestDnnMmodFaceDetection();
        }

        static void Test02()
        {
            using (var obj = new FaceDetectionContextModelsTest())
            {
                obj.StartCameraCapture();
                obj.RepeatDetection();
            }
        }

        static void Test01()
        {
            var test = new FaceDetector();
            test.RawApiFrontalFaceDetectorUsingMemoryInput();
            for (int i = 0; i < 10; i++) { test.RawApiDnnMmodDetectionUsingMemoryInput(); }
        }
    }
}
