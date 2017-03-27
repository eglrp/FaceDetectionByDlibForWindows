namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    static class Program
    {
        static void Main()
        {
            Test02();
        }

        static void Test02()
        {
            using (var obj = new FaceDetectionContextModelsTest())
            {
                obj.StartCameraCapture();
                obj.RepeatDetection();
            }
        }

        [Test]
        static void Test01()
        {
            var test = new DlibSharpTests();

            test.TestArray2dUchar();
            test.RawApiFrontalFaceDetectorUsingMemoryInput();
            test.TestFrontalFaceDetector();

            test.TestMatrixRgbPixel();
            test.RawApiDnnMmodDetectionUsingMemoryInput();
            test.TestDnnMmodFaceDetection();

            test.TestArray2dRgbPixel();
            test.RawApiFaceLandmarkDetectionUsingMemoryInput();
            test.TestFaceLandmarkDetection();
        }
    }
}
