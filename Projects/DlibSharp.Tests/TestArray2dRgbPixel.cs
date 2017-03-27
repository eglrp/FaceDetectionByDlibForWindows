namespace DlibSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using NUnit.Framework;

    [TestFixture]
    public partial class DlibSharpTests
    {
        [Test]
        public void TestArray2dRgbPixel()
        {
            using (var window = new ImageWindow())
            using (var src = new Array2dRgbPixel())
            {
                const string imagePath = "images\\lenna.bmp";
                var bmp = new System.Drawing.Bitmap(imagePath);
                src.SetBitmap(bmp);
                window.SetImage(src);
                OpenCvSharp.Cv2.WaitKey(Cv2WaitKeyDelay);

                using (var image = new Array2dRgbPixel(src))
                {
                    image.PyramidUp();
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(Cv2WaitKeyDelay);

                    image.ResizeImage(image.Width / 2, image.Height / 2);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(Cv2WaitKeyDelay);
                }
                using (var image = new Array2dRgbPixel(640, 480))
                {
                    // Quadratic does not work.
                    Array2dRgbPixel.ResizeImageWithResizeImageInterporateKind(src, image, ResizeImageInterporateKind.Bilinear);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(Cv2WaitKeyDelay);
                }
            }
        }
    }
}
