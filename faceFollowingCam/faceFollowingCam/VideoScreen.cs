using DirectShowLib;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow;

namespace faceFollowingCam
{
    public partial class VideoScreen : Form
    {
        private VideoCapture capture;
        private static Mat frame = new Mat();
        private static System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        int width, height;
        bool play = false, prev_status = false;;
        public event EventHandler<Mat> FrameUpdated;

        public static Mat Frame
        {
            get => frame;
        }

        public static System.Windows.Forms.Timer timer
        {
            get => _timer;
        }

        public VideoScreen(string videoName)
        {
            InitializeComponent();
            capture = new VideoCapture(videoName);
            if (capture.IsOpened())
            {
                capture.Read(frame);
                pictureBox.Image = PreprocessingFunc.MatToBitmap(frame);
                FrameUpdated?.Invoke(this, frame);
            }
            form_Resize();
            timer.Interval = (int)Math.Round(1000 / capture.Fps);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (capture.IsOpened() && play)
            {
                if (!capture.Read(frame))
                {
                    UpdatePlayStatus(false);
                    return;
                }
                trackBar.Value = (int)(capture.PosFrames / capture.Fps);
                pictureBox.Image = PreprocessingFunc.MatToBitmap(frame);
                FrameUpdated?.Invoke(this, frame);
            }
        }

        private void form_Resize()
        {
            width = capture.FrameWidth; height = capture.FrameHeight;
            this.Size = new System.Drawing.Size(width, height + 75);
            pictureBox.Size = new System.Drawing.Size(width, height);
            trackBar.Size = new System.Drawing.Size(width - 150, trackBar.Height);
            playButton.Top = pictureBox.Bottom;
            trackBar.Top = pictureBox.Bottom + 5;
            trackBar.Left = playButton.Right + 25;
            trackBar.Maximum = (int)Math.Round(capture.FrameCount / capture.Fps);
        }

        void UpdatePlayStatus(bool status)
        {
            play = status;
            playButton.Text = play ? "Stop" : "Play";
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (capture.PosFrames == capture.FrameCount)
            {
                capture.PosFrames = 0;
            }
            UpdatePlayStatus(!play);
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (capture.IsOpened()&&!play)
            {
                capture.PosFrames = (int) (trackBar.Value * capture.Fps);
                capture.Read(frame);
                pictureBox.Image = PreprocessingFunc.MatToBitmap(frame);
                FrameUpdated?.Invoke(this, frame);
            }
        }

        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            prev_status = play;
            UpdatePlayStatus(false);
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            UpdatePlayStatus(prev_status);
        }
    }
}
