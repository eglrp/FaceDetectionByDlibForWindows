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
            Test04();
        }

        [Test]
        static void Test04()
        {
            var test = new DlibSharpTests();
            test.TestArray2dUchar();
            test.TestMatrixRgbPixel();
        }

        [Test]
        static void Test03()
        {
            var test = new DlibSharpTests();
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

        [Test]
        static void Test01()
        {
            var test = new DlibSharpTests();
            test.RawApiFrontalFaceDetectorUsingMemoryInput();
            for (int i = 0; i < 10; i++) { test.RawApiDnnMmodDetectionUsingMemoryInput(); }
        }
    }
}
