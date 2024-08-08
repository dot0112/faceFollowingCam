using DirectShowLib;
using faceFollowingCam;

namespace faceFollwingCam
{
    public partial class DeviceSelect : Form
    {
        DsDevice[] systemCameras;


        public DeviceSelect()
        {
            InitializeComponent();
            SearchCameraDevices();

            if (systemCameras != null)
            {
                foreach (DsDevice camera in systemCameras)
                {
                    deviceList.Items.Add(camera.Name);
                }
            }
        }

        public void SearchCameraDevices()
        {
            try
            {
                systemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (deviceList.SelectedItem != null)
            {
                CameraScreen cameraScreenForm = new CameraScreen(deviceList.SelectedIndex, deviceList.SelectedItem.ToString());
                cameraScreenForm.Show();
                this.Hide();
                cameraScreenForm.FormClosed += (s, args) => this.Close();
            }

        }

        private void deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (deviceList.SelectedIndex != -1 && deviceList.SelectedIndex < systemCameras.Length)
                {
                    int index = (int)deviceList.SelectedIndex;
                    DsDevice device = systemCameras[index];

                    sName.Text = device.Name;
                    sID.Text = device.ClassID.ToString();
                    sPath.Text = device.DevicePath.Substring(0, 40) + "...";
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void searchVideoLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string validExtenstion = ".avi.mov.qt.mkv.webm.mp4.mpg.mpeg.m2v.m4v.wmv.ogv.ogg.mxf";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.FileName = "";
            openFileDialog.Filter =
                "All File|*.*|" +
                "AVI File (*.avi)|*.avi|" +
                "MOV File (*.mov)|*.mov|" +
                "QT File (*.qt)|*.qt|" +
                "MKV File (*.mkv)|*.mkv|" +
                "WEBM File (*.webm)|*.webm|" +
                "MP4 File (*.mp4)|*.mp4|" +
                "MPG File (*.mpg)|*.mpg|" +
                "MPEG File (*.mpeg)|*.mpeg|" +
                "M2V File (*.m2v)|*.m2v|" +
                "M4V File (*.m4v)|*.m4v|" +
                "WMV File (*.wmv)|*.wmv|" +
                "OGV File (*.ogv)|*.ogv|" +
                "OGG File (*.ogg)|*.ogg|" +
                "MXF File (*.mxf)|*.mxf";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string videoName = openFileDialog.FileName;
                string extension = videoName.Substring(videoName.LastIndexOf("."));
                if (validExtenstion.IndexOf(extension) == -1) { 
                    MessageBox.Show("Invalid File Extension","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                VideoScreen videoScreenForm = new VideoScreen(videoName);
                videoScreenForm.Show();
                this.Hide();
                videoScreenForm.FormClosed += (s, args) => this.Close();
            }
        }
    }
}