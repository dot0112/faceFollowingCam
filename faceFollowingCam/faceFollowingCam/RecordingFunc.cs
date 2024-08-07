using faceFollwingCam;
using OpenCvSharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace faceFollowingCam
{
    internal static class RecordingFunc
    {
        private static CancellationTokenSource? _cts;
        private static Func<Mat> _frameProvider;

        public static async Task StartRecordingAsync(Func<Mat> frameProvider)
        {
            _frameProvider = frameProvider;
            _cts = new CancellationTokenSource();

            try
            {
                await Task.Run(() => Recording(_cts.Token));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        public static void StopRecording()
        {
            _cts?.Cancel();
        }

        private static void Recording(CancellationToken cancellationToken)
        {
            string filePath = System.IO.Path.Combine(RecordingEnv.Path, RecordingEnv.Name + ".avi");
            OpenCvSharp.Size recordingSize;

            using (var initialFrame = _frameProvider())
            {
                recordingSize = initialFrame.Size();
            }

            using (var writer = new VideoWriter())
            {
                if (!writer.Open(filePath, FourCC.MJPG, 30, recordingSize, true))
                {
                    throw new Exception("Failed to open VideoWriter");
                }

                while (!cancellationToken.IsCancellationRequested)
                {
                    using (var frame = _frameProvider())
                    {
                        if (frame != null)
                        {
                            writer.Write(frame);
                        }
                    }
                    Thread.Sleep(33); // 약 30 FPS에 해당하는 대기 시간
                }
            }
        }
    }
}