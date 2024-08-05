using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;

namespace faceFollwingCam
{
	public partial class deviceSelect : Form
	{
		public List<string> dName, dID, dDes, dMan;

		public deviceSelect()
		{
			InitializeComponent();

			dName = new List<string>();
			dID = new List<string>();
			dDes = new List<string>();
			dMan = new List<string>();

			SearchCameraDevices();

			if (dName != null)
			{
				foreach (var name in dName)
				{
					deviceList.Items.Add(name);
				}
			}
		}

		public void SearchCameraDevices()
		{
			try
			{
				string query = "SELECT * FROM Win32_PnPEntity WHERE Description LIKE '%Camera%' OR Description LIKE '%Imaging Device%'";
				ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

				foreach (ManagementObject device in searcher.Get())
				{
					dName.Add(device["Name"]?.ToString());
					dID.Add(device["DeviceID"]?.ToString());
					dDes.Add(device["Description"]?.ToString());
					dMan.Add(device["Manufacturer"]?.ToString());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
				MessageBox.Show(deviceList.SelectedItems.ToString());
		}
	}
}