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
        public static double Prediction(Mat frame)
        {
            var modelPath = "path_to_your_model";
            var graph = new Graph().as_default();
            var model = tf.keras.models.load_model(modelPath);

            var resized = frame.Resize(new OpenCvSharp.Size(224, 224));
            var rgb = resized.CvtColor(ColorConversionCodes.BGR2RGB);
            float[] floatArray = new float[224 * 224 * 3];
            for (int y = 0; y < rgb.Rows; y++)
            {
                for (int x = 0; x < rgb.Cols; x++)
                {
                    Vec3b pixel = rgb.At<Vec3b>(y, x);
                    floatArray[(y * rgb.Cols + x) * 3 + 0] = pixel.Item0 / 255.0f;
                    floatArray[(y * rgb.Cols + x) * 3 + 1] = pixel.Item1 / 255.0f;
                    floatArray[(y * rgb.Cols + x) * 3 + 2] = pixel.Item2 / 255.0f;
                }
            }
            var tensor = np.expand_dims(np.array(floatArray), 0).astype(np.float32) / 255.0;

            var predictions = model.predict(tensor);
            var label = np.argmax(predictions.numpy());

            return label;
        }
    }
}
