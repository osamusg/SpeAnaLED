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
            this.LevelPictureBox = new System.Windows.Forms.PictureBox();
            this.LeftValueLabel = new System.Windows.Forms.Label();
            this.RightValueLabel = new System.Windows.Forms.Label();
            this.FormPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LevelPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LevelPictureBox
            // 
            this.LevelPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.LevelPictureBox.Location = new System.Drawing.Point(0, 16);
            this.LevelPictureBox.Name = "LevelPictureBox";
            this.LevelPictureBox.Size = new System.Drawing.Size(436, 8);
            this.LevelPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LevelPictureBox.TabIndex = 0;
            this.LevelPictureBox.TabStop = false;
            this.LevelPictureBox.DoubleClick += new System.EventHandler(this.LevelPictureBox_DoubleClick);
            this.LevelPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LevelPictureBox_MouseDown);
            this.LevelPictureBox.MouseHover += new System.EventHandler(this.LevelPictureBox_MouseHover);
            this.LevelPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LevelPictureBox_MouseMove);
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
            this.RightValueLabel.Location = new System.Drawing.Point(312, 49);
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
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(436, 63);
            this.Controls.Add(this.LevelPictureBox);
            this.Controls.Add(this.RightValueLabel);
            this.Controls.Add(this.LeftValueLabel);
            this.Controls.Add(this.FormPictureBox);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.ShowInTaskbar = false;
            this.Text = "Level Meter - SpeAnaLED";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.SizeChanged += new System.EventHandler(this.Form3_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.LevelPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected internal System.Windows.Forms.PictureBox LevelPictureBox;
        protected internal System.Windows.Forms.Label LeftValueLabel;
        protected internal System.Windows.Forms.Label RightValueLabel;
        private System.Windows.Forms.PictureBox FormPictureBox;
    }
}