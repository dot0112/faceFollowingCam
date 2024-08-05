namespace faceFollwingCam
{
	partial class DeviceSelect
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            selectBtn = new Button();
            mainLabel = new Label();
            deviceList = new ListBox();
            dName = new Label();
            dPath = new Label();
            dID = new Label();
            sName = new Label();
            sID = new Label();
            sPath = new Label();
            SuspendLayout();
            // 
            // selectBtn
            // 
            selectBtn.Dock = DockStyle.Bottom;
            selectBtn.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            selectBtn.Location = new Point(0, 426);
            selectBtn.Name = "selectBtn";
            selectBtn.Size = new Size(371, 27);
            selectBtn.TabIndex = 0;
            selectBtn.Text = "Select";
            selectBtn.UseVisualStyleBackColor = true;
            selectBtn.Click += button1_Click;
            // 
            // mainLabel
            // 
            mainLabel.AutoSize = true;
            mainLabel.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point);
            mainLabel.Location = new Point(12, 8);
            mainLabel.Name = "mainLabel";
            mainLabel.Size = new Size(92, 21);
            mainLabel.TabIndex = 1;
            mainLabel.Text = "Device List";
            // 
            // deviceList
            // 
            deviceList.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            deviceList.FormattingEnabled = true;
            deviceList.ItemHeight = 20;
            deviceList.Location = new Point(12, 32);
            deviceList.Name = "deviceList";
            deviceList.Size = new Size(347, 284);
            deviceList.TabIndex = 2;
            deviceList.SelectedIndexChanged += deviceList_SelectedIndexChanged;
            // 
            // dName
            // 
            dName.AutoSize = true;
            dName.Location = new Point(12, 334);
            dName.Name = "dName";
            dName.Size = new Size(42, 15);
            dName.TabIndex = 3;
            dName.Text = "Name:";
            // 
            // dPath
            // 
            dPath.AutoSize = true;
            dPath.Location = new Point(12, 394);
            dPath.Name = "dPath";
            dPath.Size = new Size(70, 15);
            dPath.TabIndex = 4;
            dPath.Text = "DevicePath:";
            // 
            // dID
            // 
            dID.AutoSize = true;
            dID.Location = new Point(12, 364);
            dID.Name = "dID";
            dID.Size = new Size(49, 15);
            dID.TabIndex = 5;
            dID.Text = "ClassID:";
            // 
            // sName
            // 
            sName.AutoSize = true;
            sName.Location = new Point(92, 334);
            sName.Name = "sName";
            sName.Size = new Size(0, 15);
            sName.TabIndex = 6;
            // 
            // sID
            // 
            sID.AutoSize = true;
            sID.Location = new Point(92, 364);
            sID.Name = "sID";
            sID.Size = new Size(0, 15);
            sID.TabIndex = 7;
            // 
            // sPath
            // 
            sPath.AutoSize = true;
            sPath.Location = new Point(92, 394);
            sPath.Name = "sPath";
            sPath.Size = new Size(0, 15);
            sPath.TabIndex = 8;
            // 
            // deviceSelect
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            ClientSize = new Size(371, 453);
            Controls.Add(sPath);
            Controls.Add(sID);
            Controls.Add(sName);
            Controls.Add(dID);
            Controls.Add(dPath);
            Controls.Add(dName);
            Controls.Add(deviceList);
            Controls.Add(mainLabel);
            Controls.Add(selectBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "deviceSelect";
            Text = "Select Device";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button selectBtn;
		private Label mainLabel;
		private ListBox deviceList;
        private Label dName;
        private Label dPath;
        private Label dID;
        private Label sName;
        private Label sID;
        private Label sPath;
    }
}