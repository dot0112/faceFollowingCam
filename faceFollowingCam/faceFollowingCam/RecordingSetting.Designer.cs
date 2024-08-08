namespace faceFollowingCam
{
    partial class RecordingSetting
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
            SavePathTextBox = new TextBox();
            PathSearchButton = new Button();
            FileNameTextBox = new TextBox();
            NameSubmitButton = new Button();
            label1 = new Label();
            label2 = new Label();
            folderBrowserDialog = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // SavePathTextBox
            // 
            SavePathTextBox.BackColor = SystemColors.Window;
            SavePathTextBox.Location = new Point(81, 12);
            SavePathTextBox.Name = "SavePathTextBox";
            SavePathTextBox.ReadOnly = true;
            SavePathTextBox.Size = new Size(232, 23);
            SavePathTextBox.TabIndex = 0;
            // 
            // PathSearchButton
            // 
            PathSearchButton.Location = new Point(319, 12);
            PathSearchButton.Name = "PathSearchButton";
            PathSearchButton.Size = new Size(75, 23);
            PathSearchButton.TabIndex = 1;
            PathSearchButton.Text = "search...";
            PathSearchButton.UseVisualStyleBackColor = true;
            PathSearchButton.Click += PathSearchButton_Click;
            // 
            // FileNameTextBox
            // 
            FileNameTextBox.Location = new Point(82, 51);
            FileNameTextBox.Name = "FileNameTextBox";
            FileNameTextBox.Size = new Size(231, 23);
            FileNameTextBox.TabIndex = 2;
            // 
            // NameSubmitButton
            // 
            NameSubmitButton.Location = new Point(319, 51);
            NameSubmitButton.Name = "NameSubmitButton";
            NameSubmitButton.Size = new Size(75, 23);
            NameSubmitButton.TabIndex = 3;
            NameSubmitButton.Text = "submit...";
            NameSubmitButton.UseVisualStyleBackColor = true;
            NameSubmitButton.Click += NameSubmitButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 4;
            label1.Text = "Save Path:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 5;
            label2.Text = "File Name:";
            // 
            // RecordingSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 88);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(NameSubmitButton);
            Controls.Add(FileNameTextBox);
            Controls.Add(PathSearchButton);
            Controls.Add(SavePathTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RecordingSetting";
            Text = "Setting";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox SavePathTextBox;
        private Button PathSearchButton;
        private TextBox FileNameTextBox;
        private Button NameSubmitButton;
        private Label label1;
        private Label label2;
        private FolderBrowserDialog folderBrowserDialog;
    }
}