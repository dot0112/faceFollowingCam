using DirectShowLib.DES;
using faceFollwingCam;
using OpenCvSharp;
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

namespace faceFollowingCam
{
    public partial class FaceCamera : Form
    {
        private Mat frame = CameraScreen.frame;
        private Rect[] faces = new Rect[0];
        private Rect prev_face;
        System.Windows.Forms.Timer timer;
        int width, height, cam_width, cam_height;
        private const int FACE_MARGIN = 25;
        private const int CROP_EXTRA = 50;
        bool showPostProcessing = false;

        public FaceCamera(System.Windows.Forms.Timer timer)
        {
            InitializeComponent();
            this.timer = timer;
            form_Resize();
            timer.Tick += new EventHandler(timer_Tick);
        }


        private void form_Resize()
        {
            if (frame != null)
            {
                width = frame.Width; height = frame.Height;
                cam_width = width / 2; cam_height = height / 2;
                this.Size = new System.Drawing.Size(width, height);
                pictureBox.Size = this.Size;
                prev_face = new Rect(0, 0, width, height);
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

        private void timer_Tick(object sender, EventArgs e)
        {
            if (frame == null) return;

            Mat frame_blur = new Mat(), frame_threshold = new Mat();
            Cv2.GaussianBlur(frame, frame_blur, new OpenCvSharp.Size(5, 5), sigmaX: 0, sigmaY: 0);
            Cv2.Threshold(frame_blur, frame_threshold, 127, 255, ThresholdTypes.Binary);
            if (showPostProcessing)
            {
                pictureBox.Image = MatToBitmap(frame_threshold);
                return;
            }

            var faces = FaceDetectModel.Prediction(frame_blur);
            Rect temp = prev_face;

            if (faces.Length > 0)
            {
                temp = (faces.Length > 1) ? FindClosestFace(faces, prev_face) : faces[0];
            }

            Rect cropRect = CalculateCropRectangle(temp);

            if (cropRect.Width <= 0 || cropRect.Height <= 0 ||
        cropRect.X < 0 || cropRect.Y < 0 ||
        cropRect.X + cropRect.Width > frame.Width ||
        cropRect.Y + cropRect.Height > frame.Height)
            {
                Console.WriteLine("Invalid crop rectangle");
                return;
            }


            try
            {
                using (Mat crop_frame = frame.SubMat(cropRect))
                {
                    Mat resized = crop_frame.Resize(new OpenCvSharp.Size(width, height));
                    pictureBox.Image = MatToBitmap(resized);
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

        private Rect CalculateCropRectangle(Rect face)
        {
            int pad_width = (cam_width - face.Width) / 2, pad_height = (cam_height - face.Height) / 2;
            int x = face.X, y = face.Y;
            int dW = 0, dH = 0;

            // calculate X
            int new_x = Math.Max(x - pad_width, 0);
            dW = new_x == 0 ? pad_width - x : 0;
            new_x -= Math.Max(x + face.Width + pad_width - width, 0);

            // calculate Y
            int new_y = Math.Max(y - pad_height, 0);
            dH = new_y == 0 ? pad_height - y : 0;
            new_y -= Math.Max(y + face.Height + pad_height - height, 0);

            return new Rect(new_x, new_y, cam_width + dW, cam_height + dH);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            // 이벤트 구독 해제
            timer.Tick -= timer_Tick;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            showPostProcessing = !showPostProcessing;
        }
    }
}
