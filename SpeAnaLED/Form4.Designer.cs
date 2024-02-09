namespace SpeAnaLED
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.StreamPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StreamPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // StreamPictureBox
            // 
            this.StreamPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StreamPictureBox.Location = new System.Drawing.Point(44, 43);
            this.StreamPictureBox.Name = "StreamPictureBox";
            this.StreamPictureBox.Size = new System.Drawing.Size(100, 50);
            this.StreamPictureBox.TabIndex = 0;
            this.StreamPictureBox.TabStop = false;
            this.StreamPictureBox.DoubleClick += new System.EventHandler(this.StreamPictureBox_DoubleClick);
            this.StreamPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StreamPictureBox_MouseDown);
            this.StreamPictureBox.MouseHover += new System.EventHandler(this.StreamPictureBox_MouseHover);
            this.StreamPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StreamPictureBox_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(624, 141);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StreamPictureBox);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(144, 64);
            this.Name = "Form4";
            this.ShowInTaskbar = false;
            this.Text = "Level Stream - SpeAnaLED";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form4_FormClosed);
            this.Load += new System.EventHandler(this.Form4_Load);
            this.SizeChanged += new System.EventHandler(this.Form4_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.StreamPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected internal System.Windows.Forms.PictureBox StreamPictureBox;
        protected internal System.Windows.Forms.Label label1;
    }
}