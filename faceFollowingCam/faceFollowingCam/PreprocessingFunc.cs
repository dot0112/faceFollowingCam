using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace faceFollowingCam
{
    class PreprocessingFunc
    {
        public static Bitmap MatToBitmap(Mat mat)
        {
            if (mat.Empty())
                return null;

            int width = mat.Cols;
            int height = mat.Rows;
            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Rectangle rect = new Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            IntPtr ptr = bmpData.Scan0;

            if (mat.Channels() == 3)
            {
                int totalBytes = width * height * 3;
                byte[] rgbValues = new byte[totalBytes];
                Marshal.Copy(mat.Data, rgbValues, 0, totalBytes);
                Marshal.Copy(rgbValues, 0, ptr, totalBytes);
            }
            else if (mat.Channels() == 1)
            {
                Mat bgrMat = new Mat();
                Cv2.CvtColor(mat, bgrMat, ColorConversionCodes.GRAY2BGR);
                int totalBytes = width * height * 3;
                byte[] rgbValues = new byte[totalBytes];
                Marshal.Copy(bgrMat.Data, rgbValues, 0, totalBytes);
                Marshal.Copy(rgbValues, 0, ptr, totalBytes);
            }
            else
            {
                throw new ArgumentException("Unsupported number of channels in Mat");
            }

            bitmap.UnlockBits(bmpData);
            return bitmap;
        }
    }
}
