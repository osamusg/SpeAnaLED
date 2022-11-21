namespace SpeAnaLED
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.LeftPictureBox = new System.Windows.Forms.PictureBox();
            this.RightPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LeftValueLabel = new System.Windows.Forms.Label();
            this.RightValueLabel = new System.Windows.Forms.Label();
            this.FormPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LeftPictureBox
            // 
            this.LeftPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LeftPictureBox.Location = new System.Drawing.Point(31, 16);
            this.LeftPictureBox.Name = "LeftPictureBox";
            this.LeftPictureBox.Size = new System.Drawing.Size(384, 8);
            this.LeftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LeftPictureBox.TabIndex = 0;
            this.LeftPictureBox.TabStop = false;
            this.LeftPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftPictureBox_MouseDown);
            this.LeftPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LeftPictureBox_MouseMove);
            // 
            // RightPictureBox
            // 
            this.RightPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RightPictureBox.Location = new System.Drawing.Point(31, 42);
            this.RightPictureBox.Name = "RightPictureBox";
            this.RightPictureBox.Size = new System.Drawing.Size(384, 8);
            this.RightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RightPictureBox.TabIndex = 1;
            this.RightPictureBox.TabStop = false;
            this.RightPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightPictureBox_MouseDown);
            this.RightPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RightPictureBox_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.YellowGreen;
            this.label1.Location = new System.Drawing.Point(30, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "PEAK LEVEL METER";
            this.label1.Visible = false;
            // 
            // LeftValueLabel
            // 
            this.LeftValueLabel.AutoSize = true;
            this.LeftValueLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LeftValueLabel.Location = new System.Drawing.Point(312, 3);
            this.LeftValueLabel.Name = "LeftValueLabel";
            this.LeftValueLabel.Size = new System.Drawing.Size(41, 15);
            this.LeftValueLabel.TabIndex = 8;
            this.LeftValueLabel.Text = "label2";
            this.LeftValueLabel.Visible = false;
            // 
            // RightValueLabel
            // 
            this.RightValueLabel.AutoSize = true;
            this.RightValueLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.RightValueLabel.Location = new System.Drawing.Point(312, 51);
            this.RightValueLabel.Name = "RightValueLabel";
            this.RightValueLabel.Size = new System.Drawing.Size(41, 15);
            this.RightValueLabel.TabIndex = 9;
            this.RightValueLabel.Text = "label2";
            this.RightValueLabel.Visible = false;
            // 
            // FormPictureBox
            // 
            this.FormPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.FormPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FormPictureBox.Location = new System.Drawing.Point(12, 8);
            this.FormPictureBox.Name = "FormPictureBox";
            this.FormPictureBox.Size = new System.Drawing.Size(180, 15);
            this.FormPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FormPictureBox.TabIndex = 10;
            this.FormPictureBox.TabStop = false;
            this.FormPictureBox.DoubleClick += new System.EventHandler(this.FormPictureBox_DoubleClick);
            this.FormPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormPictureBox_MouseDown);
            this.FormPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPictureBox_MouseMove);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(430, 63);
            this.Controls.Add(this.LeftPictureBox);
            this.Controls.Add(this.RightValueLabel);
            this.Controls.Add(this.LeftValueLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RightPictureBox);
            this.Controls.Add(this.FormPictureBox);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.ShowInTaskbar = false;
            this.Text = "Peak Meter - SpeAnaLED";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.SizeChanged += new System.EventHandler(this.Form3_SizeChanged);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form3_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form3_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form3_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.LeftPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        protected internal System.Windows.Forms.PictureBox LeftPictureBox;
        protected internal System.Windows.Forms.PictureBox RightPictureBox;
        protected internal System.Windows.Forms.Label LeftValueLabel;
        protected internal System.Windows.Forms.Label RightValueLabel;
        private System.Windows.Forms.PictureBox FormPictureBox;
    }
}