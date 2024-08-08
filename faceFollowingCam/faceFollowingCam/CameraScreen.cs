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
    public partial class CameraScreen : Form, IScreen
    {
        private bool faceMode = false, recording = false;
        private int width, height;
        public event EventHandler<Mat> FrameUpdated;

        private string sourceName;
        private VideoCapture capture;
        private Mat frame = new Mat();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public Mat GetFrame()
        {
            return frame;
        }

        public System.Windows.Forms.Timer GetTimer()
        {
            return timer;
        }

        public VideoCapture GetCapture()
        {
            return capture;
        }

        public string GetSourceName()
        {
            return sourceName;
        }

        public CameraScreen(int CameraNumber, string CameraName = "Camera")
        {
            InitializeComponent();
            sourceName = CameraName;
            this.Text = "Screen - " + sourceName;
            capture = new VideoCapture(CameraNumber);
            form_Resize();
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
            this.Size = new System.Drawing.Size(width, height + 24);
            menuStrip.Size = new System.Drawing.Size(width, menuStrip.Height);
            pictureBox.Size = this.Size;
            pictureBox.Top = menuStrip.Bottom;
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
                FaceCamera faceCameraForm = new FaceCamera((IScreen)this);
                faceCameraForm.Show();
                faceCameraForm.FormClosed += (s, args) => faceMode = false;
            }
        }

        private void recordingSettingToolMenuItem_Click(object sender, EventArgs e)
        {
            RecordingSetting recordingSettingForm = new RecordingSetting();
            recordingSettingForm.Show();
        }

        private async void recordingStatusToolMenuItem_Click(object sender, EventArgs e)
        {
            recording = !recording;
            recordingStatusToolMenuItem.Text = !recording ? "Start Recording" : "End Recording";
            if (recording) await RecordingFunc.StartRecordingAsync(() =>
            {
                return frame.Clone();
            });
            else RecordingFunc.StopRecording();
        }
    }
}
