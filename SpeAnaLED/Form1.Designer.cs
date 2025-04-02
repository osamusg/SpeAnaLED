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
            this.LabelCycle = new System.Windows.Forms.Label();
            this.Spectrum2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.AppContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SpectrumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelMeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LevelStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeviceReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum2)).BeginInit();
            this.AppContextMenuStrip.SuspendLayout();
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
            this.Spectrum1.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.Spectrum1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Spectrum1.MouseHover += new System.EventHandler(this.Form1_MouseHover);
            this.Spectrum1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Spectrum1_MouseMove);
            this.Spectrum1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // LabelCycle
            // 
            this.LabelCycle.AutoSize = true;
            this.LabelCycle.BackColor = System.Drawing.Color.Transparent;
            this.LabelCycle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCycle.Location = new System.Drawing.Point(148, 40);
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
            this.Spectrum2.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.Spectrum2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Spectrum2.MouseHover += new System.EventHandler(this.Form1_MouseHover);
            this.Spectrum2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Spectrum2_MouseMove);
            this.Spectrum2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
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
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.AppContextMenuStrip;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "notifyIcon";
            this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // AppContextMenuStrip
            // 
            this.AppContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigToolStripMenuItem,
            this.toolStripSeparator1,
            this.SpectrumToolStripMenuItem,
            this.LevelMeterToolStripMenuItem,
            this.LevelStreamToolStripMenuItem,
            this.toolStripSeparator2,
            this.DeviceReloadToolStripMenuItem,
            this.toolStripSeparator3,
            this.ExitToolStripMenuItem});
            this.AppContextMenuStrip.Name = "ContextMenuStrip";
            this.AppContextMenuStrip.Size = new System.Drawing.Size(149, 154);
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ConfigToolStripMenuItem.Text = "Config";
            this.ConfigToolStripMenuItem.Click += new System.EventHandler(this.ConfigToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // SpectrumToolStripMenuItem
            // 
            this.SpectrumToolStripMenuItem.CheckOnClick = true;
            this.SpectrumToolStripMenuItem.Name = "SpectrumToolStripMenuItem";
            this.SpectrumToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.SpectrumToolStripMenuItem.Text = "Spectrum";
            this.SpectrumToolStripMenuItem.Click += new System.EventHandler(this.SpectrumToolStripMenuItem_Click);
            // 
            // LevelMeterToolStripMenuItem
            // 
            this.LevelMeterToolStripMenuItem.CheckOnClick = true;
            this.LevelMeterToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelMeterToolStripMenuItem.Name = "LevelMeterToolStripMenuItem";
            this.LevelMeterToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.LevelMeterToolStripMenuItem.Text = "Level Meter";
            this.LevelMeterToolStripMenuItem.Click += new System.EventHandler(this.LevelMeterToolStripMenuItem_Click);
            // 
            // LevelStreamToolStripMenuItem
            // 
            this.LevelStreamToolStripMenuItem.CheckOnClick = true;
            this.LevelStreamToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelStreamToolStripMenuItem.Name = "LevelStreamToolStripMenuItem";
            this.LevelStreamToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.LevelStreamToolStripMenuItem.Text = "Level Stream";
            this.LevelStreamToolStripMenuItem.Click += new System.EventHandler(this.LevelStreamToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.Form2_ExitAppButton_Click);
            // 
            // DeviceReloadToolStripMenuItem
            // 
            this.DeviceReloadToolStripMenuItem.Name = "DeviceReloadToolStripMenuItem";
            this.DeviceReloadToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.DeviceReloadToolStripMenuItem.Text = "Reload Device";
            this.DeviceReloadToolStripMenuItem.Click += new System.EventHandler(this.DeviceReloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
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
            this.MinimumSize = new System.Drawing.Size(96, 48);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseHover += new System.EventHandler(this.Form1_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spectrum2)).EndInit();
            this.AppContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Spectrum1;
        private System.Windows.Forms.PictureBox Spectrum2;
        protected internal System.Windows.Forms.Label label1;
        protected internal System.Windows.Forms.Label LabelCycle;
        protected internal System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip AppContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        protected internal System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        protected internal System.Windows.Forms.ToolStripMenuItem LevelMeterToolStripMenuItem;
        protected internal System.Windows.Forms.ToolStripMenuItem LevelStreamToolStripMenuItem;
        protected internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SpectrumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeviceReloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
