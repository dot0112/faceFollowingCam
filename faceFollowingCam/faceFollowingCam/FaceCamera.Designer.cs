namespace faceFollowingCam
{
    partial class FaceCamera
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            menuStrip1 = new MenuStrip();
            recordingToolMenuItem = new ToolStripMenuItem();
            recordingStatusToolMenuItem = new ToolStripMenuItem();
            recordingSettingToolMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Left;
            pictureBox.Location = new Point(0, 24);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(100, 426);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Click += pictureBox_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { recordingToolMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // recordingToolMenuItem
            // 
            recordingToolMenuItem.DropDownItems.AddRange(new ToolStripItem[] { recordingStatusToolMenuItem, recordingSettingToolMenuItem });
            recordingToolMenuItem.Name = "recordingToolMenuItem";
            recordingToolMenuItem.Size = new Size(73, 20);
            recordingToolMenuItem.Text = "Recording";
            // 
            // recordingStatusToolMenuItem
            // 
            recordingStatusToolMenuItem.Name = "recordingStatusToolMenuItem";
            recordingStatusToolMenuItem.Size = new Size(180, 22);
            recordingStatusToolMenuItem.Text = "Start Recording";
            recordingStatusToolMenuItem.Click += startRecordingToolStripMenuItem_Click;
            // 
            // recordingSettingToolMenuItem
            // 
            recordingSettingToolMenuItem.Name = "recordingSettingToolMenuItem";
            recordingSettingToolMenuItem.Size = new Size(180, 22);
            recordingSettingToolMenuItem.Text = "Setting";
            recordingSettingToolMenuItem.Click += settingToolStripMenuItem_Click;
            // 
            // FaceCamera
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FaceCamera";
            Text = "FaceCamera";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem recordingToolMenuItem;
        private ToolStripMenuItem recordingStatusToolMenuItem;
        private ToolStripMenuItem recordingSettingToolMenuItem;
    }
}