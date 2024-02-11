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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Spectrum1 = new System.Windows.Forms.PictureBox();
            this.LabelCycle = new System.Windows.Forms.Label();
            this.Spectrum2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum2)).BeginInit();
            this.SuspendLayout();
            // 
            // Spectrum1
            // 
            this.Spectrum1.BackColor = System.Drawing.Color.Black;
            this.Spectrum1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Spectrum1.Location = new System.Drawing.Point(14, 15);
            this.Spectrum1.Margin = new System.Windows.Forms.Padding(4);
            this.Spectrum1.Name = "Spectrum1";
            this.Spectrum1.Size = new System.Drawing.Size(116, 62);
            this.Spectrum1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Spectrum1.TabIndex = 0;
            this.Spectrum1.TabStop = false;
            this.Spectrum1.DoubleClick += new System.EventHandler(this.Spectrum1_DoubleClick);
            this.Spectrum1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Spectrum1_MouseDown);
            this.Spectrum1.MouseHover += new System.EventHandler(this.Spectrum1_MouseHover);
            this.Spectrum1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Spectrum1_MouseMove);
            // 
            // LabelCycle
            // 
            this.LabelCycle.AutoSize = true;
            this.LabelCycle.BackColor = System.Drawing.Color.Transparent;
            this.LabelCycle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCycle.Location = new System.Drawing.Point(21, 22);
            this.LabelCycle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelCycle.Name = "LabelCycle";
            this.LabelCycle.Size = new System.Drawing.Size(41, 15);
            this.LabelCycle.TabIndex = 1;
            this.LabelCycle.Text = "label1";
            this.LabelCycle.Visible = false;
            // 
            // Spectrum2
            // 
            this.Spectrum2.BackColor = System.Drawing.Color.Black;
            this.Spectrum2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Spectrum2.Location = new System.Drawing.Point(14, 246);
            this.Spectrum2.Margin = new System.Windows.Forms.Padding(4);
            this.Spectrum2.Name = "Spectrum2";
            this.Spectrum2.Size = new System.Drawing.Size(116, 62);
            this.Spectrum2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Spectrum2.TabIndex = 2;
            this.Spectrum2.TabStop = false;
            this.Spectrum2.DoubleClick += new System.EventHandler(this.Spectrum2_DoubleClick);
            this.Spectrum2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Spectrum2_MouseDown);
            this.Spectrum2.MouseHover += new System.EventHandler(this.Spectrum2_MouseHover);
            this.Spectrum2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Spectrum2_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(789, 439);
            this.Controls.Add(this.LabelCycle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Spectrum2);
            this.Controls.Add(this.Spectrum1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(144, 64);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.Form1_VisibleChanged);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseHover += new System.EventHandler(this.Form1_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Spectrum1;
        private System.Windows.Forms.Label LabelCycle;
        private System.Windows.Forms.PictureBox Spectrum2;
        private System.Windows.Forms.Label label1;
    }
}
