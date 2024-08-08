using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace faceFollowingCam
{
    public interface IScreen
    {
        public Mat GetFrame();
        public System.Windows.Forms.Timer GetTimer();
        public VideoCapture GetCapture();
        public string GetSourceName();
    }
}
