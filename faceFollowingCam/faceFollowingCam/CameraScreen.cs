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
using faceFollowingCam;
using OpenCvSharp;

namespace faceFollwingCam
{
    public partial class CameraScreen : Form
    {
        private VideoCapture capture;
        public static Mat frame;
        private Rect[] faces = new Rect[0];
        private Rect prev_face = new Rect();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private bool faceMode = false;
        private int width, height;
        public event EventHandler<Mat> FrameUpdated;

        public CameraScreen(int CameraNumber, string CameraName = "Camera")
        {
            InitializeComponent();
            this.Text = "Screen - " + CameraName;
            capture = new VideoCapture(CameraNumber);
            form_Resize();
            frame = new Mat();
            timer.Interval = 33;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (capture.IsOpened())
            {
                capture.Read(frame);
                pictureBox.Image = PreprocessingFunc.MatToBitmap(frame);
                FrameUpdated?.Invoke(this, frame);
            }
        }

        private void form_Resize()
        {
            width = capture.FrameWidth; height = capture.FrameHeight;
            this.Size = new System.Drawing.Size(width, height);
            pictureBox.Size = this.Size;
        }

        private void CameraScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
            {
                capture.Release();
                frame.Dispose();
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (!faceMode)
            {
                faceMode = true;
                FaceCamera faceCameraForm = new FaceCamera(timer);
                faceCameraForm.Show();
                faceCameraForm.FormClosed += (s, args) => faceMode = false ;
            }
        }
    }
}
