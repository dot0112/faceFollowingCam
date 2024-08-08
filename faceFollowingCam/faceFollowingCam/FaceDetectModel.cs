using OpenCvSharp.Aruco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorflow;
using Tensorflow.NumPy;
using static Tensorflow.Binding;
using OpenCvSharp;

namespace faceFollwingCam
{
    internal class FaceDetectModel
    {

        static Mat gray_threshold = new Mat();

        public static Mat Gray_threshold
        {
            get { return gray_threshold; }
        }

        public static Rect[] Prediction(Mat frame)
        {
            using (var gray = new Mat())
            using (var gray_blur = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.GaussianBlur(gray, gray_blur, new OpenCvSharp.Size(5, 5), sigmaX: 0, sigmaY: 0);
                Cv2.Threshold(gray_blur, gray_threshold, 127, 255, ThresholdTypes.Binary);
                return new CascadeClassifier("haarcascade_frontalface_default.xml").DetectMultiScale(image: gray, scaleFactor: 1.5);
            }
        }
    }
}
