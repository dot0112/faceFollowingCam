using DirectShowLib;

namespace faceFollwingCam
{
    public partial class deviceSelect : Form
    {
        DsDevice[] systemCameras;


        public deviceSelect()
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
                MessageBox.Show(deviceList.SelectedItem.ToString());
        }

        private void deviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (int)deviceList.SelectedIndex;
            DsDevice device = systemCameras[index];

            sName.Text = device.Name;
            sID.Text = device.ClassID.ToString();
            sPath.Text = device.DevicePath.Substring(0, 40) + "...";
        }
    }
}