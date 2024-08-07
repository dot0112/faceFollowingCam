namespace faceFollwingCam
{
    partial class CameraScreen
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
            menuStrip = new MenuStrip();
            recordingToolMenuItem = new ToolStripMenuItem();
            recordingStatusToolMenuItem = new ToolStripMenuItem();
            recordingSettingToolMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            menuStrip.SuspendLayout();
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
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { recordingToolMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
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
            recordingStatusToolMenuItem.Click += recordingStatusToolMenuItem_Click;
            // 
            // recordingSettingToolMenuItem
            // 
            recordingSettingToolMenuItem.Name = "recordingSettingToolMenuItem";
            recordingSettingToolMenuItem.Size = new Size(180, 22);
            recordingSettingToolMenuItem.Text = "Setting";
            recordingSettingToolMenuItem.Click += recordingSettingToolMenuItem_Click;
            // 
            // CameraScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "CameraScreen";
            Text = "CameraScreen";
            FormClosing += CameraScreen_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private MenuStrip menuStrip;
        private ToolStripMenuItem recordingToolMenuItem;
        private ToolStripMenuItem recordingStatusToolMenuItem;
        private ToolStripMenuItem recordingSettingToolMenuItem;
    }
}