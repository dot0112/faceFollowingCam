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
        public static Rect[] Prediction(Mat frame)
        {
            using (var gray = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                return new CascadeClassifier("haarcascade_frontalface_default.xml").DetectMultiScale(gray);
            }
        }
    }
}
