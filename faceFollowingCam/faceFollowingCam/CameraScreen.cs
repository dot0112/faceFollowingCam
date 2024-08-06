using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace faceFollwingCam
{
    public partial class CameraScreen : Form
    {
        private VideoCapture capture;
        private Mat frame;
        private Rect[] faces = new Rect[0];

        public CameraScreen(int CameraNumber)
        {
            InitializeComponent();
            capture = new VideoCapture(CameraNumber);
            form_Resize(capture);
            frame = new Mat();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 33;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (capture.IsOpened())
            {
                capture.Read(frame);
                Bitmap bitmap = MatToBitmap(frame);
                pictureBox1.Image = bitmap;
            }
        }

        private Bitmap MatToBitmap(Mat mat)
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

        private void form_Resize(VideoCapture capture)
        {
            this.Size = new System.Drawing.Size(capture.FrameWidth * 2, capture.FrameHeight);
            pictureBox1.Size = this.Size / 2;
            pictureBox2.Size = pictureBox1.Size;
            pictureBox2.Left = pictureBox1.Right;
            pictureBox2.Top = pictureBox1.Top;
        }

        private void CameraScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
            {
                capture.Release();
                frame.Dispose();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var faces = FaceDetectModel.Prediction(frame);
            foreach (var face in faces)
            {
                // 검출된 얼굴 주위에 사각형 그리기
                Cv2.Rectangle(frame, face, Scalar.Red, 2);
            }
            Bitmap bitmap = MatToBitmap(frame);
            pictureBox2.Image = bitmap;
        }
    }
}
