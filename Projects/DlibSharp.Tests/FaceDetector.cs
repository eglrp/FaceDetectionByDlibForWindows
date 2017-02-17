﻿namespace DlibSharp.Tests
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
    public class FaceDetector
    {
        private ErrorCallback callback;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory);

            callback = delegate (string what) { throw new Exception(what); };
            NativeMethods.dlib_set_error_redirect(callback);
        }

        [Test]
        public void RawApiNewAndDispose()
        {
            IntPtr detector = NativeMethods.dlib_get_frontal_face_detector();
            NativeMethods.dlib_frontal_face_detector_delete(detector);
        }

        /// <summary>
        /// https://github.com/davisking/dlib/blob/master/examples/face_detection_ex.cpp
        /// </summary>
        [Test]
        public void RawApiFrontalFaceDetector()
        {
            const string imagePath = "images\\lenna.bmp";

            IntPtr detector = IntPtr.Zero;
            IntPtr image = IntPtr.Zero;
            IntPtr dets = IntPtr.Zero;
            try
            {
                detector = NativeMethods.dlib_get_frontal_face_detector();
                image = NativeMethods.dlib_array2d_uchar_new();

                NativeMethods.dlib_load_image_array2d_uchar(image, imagePath);
                NativeMethods.dlib_pyramid_up_array2d_uchar(image);

                dets = NativeMethods.vector_Rect_new1();
                NativeMethods.dlib_frontal_face_detector_operator(detector, image, 0, dets);
                unsafe
                {
                    Rect* rectangles = (Rect*)NativeMethods.vector_Rect_getPointer(dets).ToPointer();
                    long count = NativeMethods.vector_Rect_getSize(dets).ToInt64();
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine(rectangles[i]);
                    }
                }
            }
            finally
            {
                if (image != IntPtr.Zero)
                    NativeMethods.dlib_array2d_uchar_delete(image);
                if (detector != IntPtr.Zero)
                    NativeMethods.dlib_frontal_face_detector_delete(detector);
                if (dets != IntPtr.Zero)
                    NativeMethods.vector_Rect_delete(dets);
            }
        }

        [Test]
        public void RawApiFrontalFaceDetectorUsingMemoryInput()
        {
            const string imagePath = "images\\lenna.bmp";
            var imageBytes = File.ReadAllBytes(imagePath);

            IntPtr detector = IntPtr.Zero;
            IntPtr dets = IntPtr.Zero;
            try
            {
                using (var window = new ImageWindow())
                using (var image = new Array2dUchar())
                {
                    Debug.WriteLine("Size: ({0},{1})", image.Width, image.Height);
                    Trace.Assert(image.Width == 0 && image.Height == 0);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(1);

                    NativeMethods.dlib_load_bmp_array2d_uchar(image.DlibArray2dUchar, imageBytes, new IntPtr(imageBytes.Length));
                    Debug.WriteLine("Size: ({0},{1})", image.Width, image.Height);
                    Trace.Assert(image.Width == 512 && image.Height == 480);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(1);

                    image.SetBitmap(new System.Drawing.Bitmap(imagePath));
                    Debug.WriteLine("Size: ({0},{1})", image.Width, image.Height);
                    Trace.Assert(image.Width == 512 && image.Height == 480);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(1);

                    image.PyramidUp();
                    Debug.WriteLine("Size: ({0},{1})", image.Width, image.Height);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(1);

                    detector = NativeMethods.dlib_get_frontal_face_detector();
                    dets = NativeMethods.vector_Rect_new1();
                    NativeMethods.dlib_frontal_face_detector_operator(detector, image.DlibArray2dUchar, 0, dets);
                    unsafe
                    {
                        Rect* rectangles = (Rect*)NativeMethods.vector_Rect_getPointer(dets).ToPointer();
                        long count = NativeMethods.vector_Rect_getSize(dets).ToInt64();
                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine(rectangles[i]);
                        }
                    }
                }
            }
            finally
            {
                if (detector != IntPtr.Zero)
                    NativeMethods.dlib_frontal_face_detector_delete(detector);
                if (dets != IntPtr.Zero)
                    NativeMethods.vector_Rect_delete(dets);
            }
        }

        [Test]
        public void RawApiDnnMmodDetectionUsingMemoryInput()
        {
            const string imagePath = "images\\lenna.bmp";

            if (IntPtr.Size == 4) { return; }

            var imageBytes = File.ReadAllBytes(imagePath);

            IntPtr detector = IntPtr.Zero;
            IntPtr dets = IntPtr.Zero;
            try
            {
                using (var window = new ImageWindow())
                using (var image = new MatrixRgbPixel())
                {
                    Debug.WriteLine("Size: ({0},{1})", image.Width, image.Height);
                    Trace.Assert(image.Width == 0 && image.Height == 0);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(1);

                    NativeMethods.dlib_load_bmp_matrix_rgbpixel(image.DlibMatrixRgbPixel, imageBytes, new IntPtr(imageBytes.Length));
                    Debug.WriteLine("Size: ({0},{1})", image.Width, image.Height);
                    Trace.Assert(image.Width == 512 && image.Height == 480);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(1);

                    image.SetBitmap(new System.Drawing.Bitmap(imagePath));
                    Debug.WriteLine("Size: ({0},{1})", image.Width, image.Height);
                    Trace.Assert(image.Width == 512 && image.Height == 480);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(1);

                    image.PyramidUp();
                    Debug.WriteLine("Size: ({0},{1})", image.Width, image.Height);
                    window.SetImage(image);
                    OpenCvSharp.Cv2.WaitKey(1);

                    dets = NativeMethods.vector_Rect_new1();
                    detector = NativeMethods.dlib_dnn_mmod_face_detection_construct("./data/mmod_human_face_detector.dat");
                    NativeMethods.dlib_dnn_mmod_face_detection_operator(detector, image.DlibMatrixRgbPixel, dets);
                    long count = NativeMethods.vector_Rect_getSize(dets).ToInt64();
                    if (count > 0)
                    {
                        unsafe
                        {
                            Rect* rectangles = (Rect*)NativeMethods.vector_Rect_getPointer(dets).ToPointer();
                            for (int i = 0; i < count; i++)
                            {
                                Console.WriteLine(rectangles[i]);
                            }
                        }
                    }
                }
            }
            finally
            {
                if (detector != IntPtr.Zero)
                    NativeMethods.dlib_dnn_mmod_face_detection_delete(detector);
                if (dets != IntPtr.Zero)
                    NativeMethods.vector_Rect_delete(dets);
            }
        }

        [Test]
        public void TestFrontalFaceDetector()
        {
            const string imagePath = "images\\lenna.bmp";

            if (IntPtr.Size == 4) { return; }
            using (var window = new ImageWindow())
            using (var image = new Array2dUchar())
            using (var detector = new FrontalFaceDetector())
            {
                var bmp = new System.Drawing.Bitmap(imagePath);
                image.SetBitmap(bmp);
                image.PyramidUp();
                window.SetImage(image);
                OpenCvSharp.Cv2.WaitKey(1);
                var rects = detector.DetectFaces(image, -0.5);
                foreach (var rect in rects) { Console.WriteLine(rect); }
            }
        }

        [Test]
        public void TestDnnMmodFaceDetection()
        {
            const string imagePath = "images\\lenna.bmp";

            if (IntPtr.Size == 4) { return; }
            using (var window = new ImageWindow())
            using (var image = new MatrixRgbPixel())
            using (var detector = new DnnMmodFaceDetection("./data/mmod_human_face_detector.dat"))
            {
                var bmp = new System.Drawing.Bitmap(imagePath);
                image.SetBitmap(bmp);
                image.PyramidUp();
                window.SetImage(image);
                OpenCvSharp.Cv2.WaitKey(1);
                var rects = detector.DetectFaces(image);
                foreach (var rect in rects) { Console.WriteLine(rect); }
            }
        }
    }
}
