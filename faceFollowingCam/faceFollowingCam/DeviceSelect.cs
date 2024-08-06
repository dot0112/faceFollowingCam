using DirectShowLib;

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
                CameraScreen cameraScreenForm = new CameraScreen(deviceList.SelectedIndex);
                cameraScreenForm.Show();
                this.Hide();
                cameraScreenForm.FormClosed += (s, args) => this.Close();
            }

        }

        private void deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(deviceList.SelectedIndex != -1 && deviceList.SelectedIndex < systemCameras.Length)
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
    }
}