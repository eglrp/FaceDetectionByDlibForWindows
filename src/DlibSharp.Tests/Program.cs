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
            var test = new FaceDetector();
            test.RawApiFrontalFaceDetectorUsingMemoryInput();
            for (int i = 0; i < 10; i++) { test.RawApiDnnMmodDetectionUsingMemoryInput(); }
        }
    }
}
