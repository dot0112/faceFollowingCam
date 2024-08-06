using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;

namespace faceFollwingCam
{
    public partial class Main : Form
    {

        string dName = string.Empty;
        private IFilterGraph2 filterGraph = null;
        private IBaseFilter captureFilter = null;
        private PictureBox captureScreen;
        private IBaseFilter rendererFilter = null;

        public Main(string dName)
        {
            InitializeComponent();

            this.dName = dName;

            InitializeCaptureGraph();
        }

        private void InitializeCaptureGraph()
        {
            try
            {
                // 필터 그래프 빌더 생성
                filterGraph = (IFilterGraph2)new FilterGraph();

                // 카메라 장치 선택
                DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
                if (devices.Length == 0)
                {
                    MessageBox.Show("No video capture devices found.");
                    return;
                }

                // 첫 번째 카메라 장치 사용
                Guid captureFilterGuid = typeof(IBaseFilter).GUID;
                var selectedDevice = devices.FirstOrDefault(d => d.Name == dName);
                filterGraph.AddSourceFilterForMoniker(selectedDevice.Mon, null, "Video Capture", out captureFilter);

                // 렌더러 필터 추가
                rendererFilter = (IBaseFilter)new VideoRendererDefault();
                filterGraph.AddFilter(rendererFilter, "Renderer");

                IAMStreamConfig videoStreamConfig = GetStreamConfig(captureFilter);
                VideoInfoHeader videoInfoHeader = GetVideoInfoHeader(videoStreamConfig);
                int videoWidth = videoInfoHeader.BmiHeader.Width;
                int videoHeight = videoInfoHeader.BmiHeader.Height;

                changeScreenSize(videoWidth, videoHeight);

                // 필터 연결
                IPin outPin = DsFindPin.ByDirection(captureFilter, PinDirection.Output, 0);
                IPin inPin = DsFindPin.ByDirection(rendererFilter, PinDirection.Input, 0);
                filterGraph.Connect(outPin, inPin);

                // 미디어 컨트롤 인터페이스 가져오기
                IMediaControl mediaControl = filterGraph as IMediaControl;

                // 미디어 윈도우 인터페이스 가져오기
                IVideoWindow videoWindow = filterGraph as IVideoWindow;
                videoWindow.put_Owner(captureScreen.Handle);
                videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);
                videoWindow.SetWindowPosition(0, 0, captureScreen.Width, captureScreen.Height);

                // 그래프 실행
                mediaControl.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing capture graph: " + ex.Message);
            }
        }

        private void changeScreenSize(int width, int height)
        {
            this.Size = new Size(width + 200, height);
            captureScreen.Size = new Size(width, height);
        }

        private IAMStreamConfig GetStreamConfig(IBaseFilter filter)
        {
            // 출력 핀 찾기
            IPin pin = DsFindPin.ByDirection(filter, PinDirection.Output, 0);

            // IAMStreamConfig 인터페이스로 캐스팅
            return pin as IAMStreamConfig;
        }

        private VideoInfoHeader GetVideoInfoHeader(IAMStreamConfig streamConfig)
        {
            // 현재 구성에서 비디오 정보를 얻기 위한 변수 선언
            AMMediaType mediaType = null;
            VideoInfoHeader videoInfoHeader = new VideoInfoHeader();

            try
            {
                // 포맷 정보를 얻기
                streamConfig.GetFormat(out mediaType);
                if (mediaType != null)
                {
                    // VideoInfoHeader 구조체로 포맷 정보를 변환
                    videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(mediaType.formatPtr, typeof(VideoInfoHeader));
                }
            }
            finally
            {
                if (mediaType != null)
                {
                    DsUtils.FreeAMMediaType(mediaType);
                }
            }

            return videoInfoHeader;
        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // 그래프 중지 및 해제
            if (filterGraph != null)
            {
                IMediaControl mediaControl = filterGraph as IMediaControl;
                mediaControl.Stop();

                Marshal.ReleaseComObject(filterGraph);
                filterGraph = null;
            }
        }

        private void InitializeComponent()
        {
            captureScreen = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)captureScreen).BeginInit();
            SuspendLayout();
            // 
            // captureScreen
            // 
            captureScreen.Dock = DockStyle.Left;
            captureScreen.Location = new Point(0, 0);
            captureScreen.Name = "captureScreen";
            captureScreen.Size = new Size(640, 480);
            captureScreen.TabIndex = 0;
            captureScreen.TabStop = false;
            // 
            // Main
            // 
            ClientSize = new Size(640, 480);
            Controls.Add(captureScreen);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Main";
            ((System.ComponentModel.ISupportInitialize)captureScreen).EndInit();
            ResumeLayout(false);
        }
    }
}
