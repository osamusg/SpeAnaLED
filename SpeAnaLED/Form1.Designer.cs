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
            this.Spectrum1 = new System.Windows.Forms.PictureBox();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.LabelCycle = new System.Windows.Forms.Label();
            this.Spectrum2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum2)).BeginInit();
            this.SuspendLayout();
            // 
            // Spectrum1
            // 
            this.Spectrum1.BackColor = System.Drawing.Color.Black;
            this.Spectrum1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Spectrum1.Location = new System.Drawing.Point(12, 12);
            this.Spectrum1.Name = "Spectrum1";
            this.Spectrum1.Size = new System.Drawing.Size(100, 50);
            this.Spectrum1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Spectrum1.TabIndex = 0;
            this.Spectrum1.TabStop = false;
            this.Spectrum1.DoubleClick += new System.EventHandler(this.Spectrum1_DoubleClick);
            this.Spectrum1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Spectrum1_MouseDown);
            // 
            // Timer1
            // 
            this.Timer1.Enabled = true;
            this.Timer1.Interval = 20;
            // 
            // LabelCycle
            // 
            this.LabelCycle.AutoSize = true;
            this.LabelCycle.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelCycle.Location = new System.Drawing.Point(22, 24);
            this.LabelCycle.Name = "LabelCycle";
            this.LabelCycle.Size = new System.Drawing.Size(41, 14);
            this.LabelCycle.TabIndex = 1;
            this.LabelCycle.Text = "label1";
            this.LabelCycle.Click += new System.EventHandler(this.LabelCycle_Click);
            // 
            // Spectrum2
            // 
            this.Spectrum2.BackColor = System.Drawing.Color.Black;
            this.Spectrum2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Spectrum2.Location = new System.Drawing.Point(12, 197);
            this.Spectrum2.Name = "Spectrum2";
            this.Spectrum2.Size = new System.Drawing.Size(100, 50);
            this.Spectrum2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Spectrum2.TabIndex = 2;
            this.Spectrum2.TabStop = false;
            this.Spectrum2.DoubleClick += new System.EventHandler(this.Spectrum2_DoubleClick);
            this.Spectrum2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Spectrum2_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 351);
            this.Controls.Add(this.Spectrum2);
            this.Controls.Add(this.LabelCycle);
            this.Controls.Add(this.Spectrum1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SpeAnaLED";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Spectrum1;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Label LabelCycle;
        private System.Windows.Forms.PictureBox Spectrum2;
    }
}
