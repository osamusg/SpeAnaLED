namespace SpeAnaLED
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.spectrum1 = new System.Windows.Forms.PictureBox();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.spectrum2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.spectrum1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spectrum2)).BeginInit();
            this.SuspendLayout();
            // 
            // spectrum1
            // 
            this.spectrum1.BackColor = System.Drawing.Color.Black;
            this.spectrum1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spectrum1.Location = new System.Drawing.Point(12, 12);
            this.spectrum1.Name = "spectrum1";
            this.spectrum1.Size = new System.Drawing.Size(100, 50);
            this.spectrum1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.spectrum1.TabIndex = 0;
            this.spectrum1.TabStop = false;
            this.spectrum1.DoubleClick += new System.EventHandler(this.Spectrum1_DoubleClick);
            this.spectrum1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.spectrum1_MouseDown);
            // 
            // Timer1
            // 
            this.Timer1.Enabled = true;
            this.Timer1.Interval = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // spectrum2
            // 
            this.spectrum2.BackColor = System.Drawing.Color.Black;
            this.spectrum2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spectrum2.Location = new System.Drawing.Point(12, 197);
            this.spectrum2.Name = "spectrum2";
            this.spectrum2.Size = new System.Drawing.Size(100, 50);
            this.spectrum2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.spectrum2.TabIndex = 2;
            this.spectrum2.TabStop = false;
            this.spectrum2.DoubleClick += new System.EventHandler(this.Spectrum2_DoubleClick);
            this.spectrum2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.spectrum2_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 351);
            this.Controls.Add(this.spectrum2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spectrum1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SpeAnaLED";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.spectrum1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spectrum2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox spectrum1;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox spectrum2;
    }
}

