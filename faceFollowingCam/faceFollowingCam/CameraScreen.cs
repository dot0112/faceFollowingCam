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
        private Rect prev_face = new Rect();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private bool faceMode = false;
        private int width, height;

        public CameraScreen(int CameraNumber)
        {
            InitializeComponent();
            capture = new VideoCapture(CameraNumber);
            form_Resize();
            frame = new Mat();
            timer.Interval = 33;
            timer.Tick += new EventHandler(timer_Tick_Main);
            timer.Start();
        }

        private void timer_Tick_Main(object sender, EventArgs e)
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

        private void form_Resize()
        {
            width = capture.FrameWidth; height = capture.FrameHeight;
            this.Size = new System.Drawing.Size(width, height);
            pictureBox1.Size = this.Size;
            pictureBox2.Size = new System.Drawing.Size(0, 0);
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
            if (!faceMode)
            {
                this.Size = new System.Drawing.Size(width + height, height);
                pictureBox2.Size = new System.Drawing.Size(height, height);
                timer.Tick += timer_Tick_Face;
            }
            else
            {
                this.Size = new System.Drawing.Size(width, height);
                pictureBox2.Size = new System.Drawing.Size(0, 0);
                timer.Tick += timer_Tick_Face;
            }
            faceMode = !faceMode;
        }

        private const int FACE_MARGIN = 25;
        private const int CROP_EXTRA = 50;

        private void timer_Tick_Face(object sender, EventArgs e)
        {
            if (frame == null) return;

            var faces = FaceDetectModel.Prediction(frame);
            Rect temp = prev_face;

            if (faces.Length > 0)
            {
                temp = (faces.Length > 1) ? FindClosestFace(faces, prev_face) : faces[0];
            }

            Rect cropRect = CalculateCropRectangle(temp, frame.Width, frame.Height);

            try
            {
                using (Mat crop_frame = frame.SubMat(cropRect))
                {
                    Mat resized = crop_frame.Resize(new OpenCvSharp.Size(height, height));
                    pictureBox2.Image = MatToBitmap(resized);
                }
            }
            catch (OpenCvSharpException ex)
            {
                Console.WriteLine($"Error cropping image: {ex.Message}");
            }

            prev_face = temp;
        }

        private Rect FindClosestFace(Rect[] faces, Rect prevFace)
        {
            return faces.OrderBy(face =>
                Math.Sqrt(Math.Pow(prevFace.X - face.X, 2) + Math.Pow(prevFace.Y - face.Y, 2))
            ).First();
        }

        private Rect CalculateCropRectangle(Rect face, int frameWidth, int frameHeight)
        {
            int x = Math.Max(0, face.X - FACE_MARGIN);
            int y = Math.Max(0, face.Y - FACE_MARGIN);
            int width = Math.Min(frameWidth - x, face.Width + CROP_EXTRA);
            int height = Math.Min(frameHeight - y, face.Height + CROP_EXTRA);

            return new Rect(x, y, width, height);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
