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

namespace faceFollowingCam
{
    public partial class RecordingSetting : Form
    {
        public RecordingSetting()
        {
            InitializeComponent();
            RecordingEnv.PathChanged += UpdatePathTextBox;
            RecordingEnv.NameChanged += UpdateNameTextBox;
            SavePathTextBox.Text = RecordingEnv.Path;
            FileNameTextBox.Text = RecordingEnv.Name;
        }

        private void PathSearchButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.InitialDirectory = RecordingEnv.Path;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                RecordingEnv.Path = folderBrowserDialog.SelectedPath;
            }
        }

        private void NameSubmitButton_Click(object sender, EventArgs e)
        {
            string fileName = FileNameTextBox.Text;
            char[] invalidChars = Path.GetInvalidFileNameChars();
            string pattern = "[" + Regex.Escape(new string(invalidChars)) + "]";
            if (Regex.IsMatch(fileName, pattern))
            {
                MessageBox.Show("Can't Change File Name", "Invalid File Name");
                return;
            }
            RecordingEnv.Name = fileName;
        }

        private void UpdatePathTextBox(object sender, EventArgs e)
        {
            SavePathTextBox.Text = RecordingEnv.Path;
        }

        private void UpdateNameTextBox(object sender, EventArgs e)
        {
            FileNameTextBox.Text = RecordingEnv.Name;
        }
    }
}
