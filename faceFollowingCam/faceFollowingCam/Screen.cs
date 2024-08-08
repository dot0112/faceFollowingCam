using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace faceFollowingCam
{
    internal interface screen
    {
        protected static VideoCapture capture;
        protected static Mat frame = new Mat();
        protected static System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public static Mat Frame
        {
            get => frame;
        }

        public static System.Windows.Forms.Timer timer
        {
            get => _timer;
        }
    }
}
