namespace faceFollwingCam
{
	partial class deviceSelect
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
			label1 = new Label();
			deviceList = new ListBox();
			SuspendLayout();
			// 
			// selectBtn
			// 
			selectBtn.Dock = DockStyle.Bottom;
			selectBtn.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
			selectBtn.Location = new Point(0, 462);
			selectBtn.Name = "selectBtn";
			selectBtn.Size = new Size(358, 27);
			selectBtn.TabIndex = 0;
			selectBtn.Text = "Select";
			selectBtn.UseVisualStyleBackColor = true;
			selectBtn.Click += button1_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 8);
			label1.Name = "label1";
			label1.Size = new Size(65, 15);
			label1.TabIndex = 1;
			label1.Text = "Device List";
			// 
			// deviceList
			// 
			deviceList.FormattingEnabled = true;
			deviceList.ItemHeight = 15;
			deviceList.Location = new Point(12, 26);
			deviceList.Name = "deviceList";
			deviceList.Size = new Size(334, 274);
			deviceList.TabIndex = 2;
			// 
			// deviceSelect
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(358, 489);
			Controls.Add(deviceList);
			Controls.Add(label1);
			Controls.Add(selectBtn);
			Name = "deviceSelect";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button selectBtn;
		private Label label1;
		private ListBox deviceList;
	}
}