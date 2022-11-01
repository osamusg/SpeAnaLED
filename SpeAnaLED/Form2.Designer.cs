namespace SpeAnaLED
{
    partial class Form2
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
            this.devicelist = new System.Windows.Forms.ComboBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.TextBox_Sensibility = new System.Windows.Forms.TextBox();
            this.RadioClassic = new System.Windows.Forms.RadioButton();
            this.RadioPrisum = new System.Windows.Forms.RadioButton();
            this.RadioSimple = new System.Windows.Forms.RadioButton();
            this.RadioRainbow = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EnumerateButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TrackBar2 = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TextBox_DecaySpeed = new System.Windows.Forms.TextBox();
            this.SSaverCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // devicelist
            // 
            this.devicelist.FormattingEnabled = true;
            this.devicelist.Location = new System.Drawing.Point(12, 28);
            this.devicelist.Name = "devicelist";
            this.devicelist.Size = new System.Drawing.Size(440, 20);
            this.devicelist.TabIndex = 0;
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Button1.Location = new System.Drawing.Point(562, 310);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 2;
            this.Button1.Text = "OK";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TrackBar1
            // 
            this.TrackBar1.LargeChange = 10;
            this.TrackBar1.Location = new System.Drawing.Point(6, 18);
            this.TrackBar1.Maximum = 99;
            this.TrackBar1.Minimum = 10;
            this.TrackBar1.Name = "TrackBar1";
            this.TrackBar1.Size = new System.Drawing.Size(273, 45);
            this.TrackBar1.TabIndex = 3;
            this.TrackBar1.Value = 78;
            this.TrackBar1.ValueChanged += new System.EventHandler(this.TrackBar1_ValueChanged);
            // 
            // TextBox_Sensibility
            // 
            this.TextBox_Sensibility.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox_Sensibility.Location = new System.Drawing.Point(279, 18);
            this.TextBox_Sensibility.Name = "TextBox_Sensibility";
            this.TextBox_Sensibility.Size = new System.Drawing.Size(43, 23);
            this.TextBox_Sensibility.TabIndex = 5;
            this.TextBox_Sensibility.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBox_Sensibility.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Sensibility_KeyDown);
            // 
            // RadioClassic
            // 
            this.RadioClassic.AutoSize = true;
            this.RadioClassic.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioClassic.Location = new System.Drawing.Point(17, 21);
            this.RadioClassic.Name = "RadioClassic";
            this.RadioClassic.Size = new System.Drawing.Size(70, 20);
            this.RadioClassic.TabIndex = 6;
            this.RadioClassic.TabStop = true;
            this.RadioClassic.Text = "Classic";
            this.RadioClassic.UseVisualStyleBackColor = true;
            // 
            // RadioPrisum
            // 
            this.RadioPrisum.AutoSize = true;
            this.RadioPrisum.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioPrisum.Location = new System.Drawing.Point(17, 47);
            this.RadioPrisum.Name = "RadioPrisum";
            this.RadioPrisum.Size = new System.Drawing.Size(68, 20);
            this.RadioPrisum.TabIndex = 7;
            this.RadioPrisum.TabStop = true;
            this.RadioPrisum.Text = "Prisum";
            this.RadioPrisum.UseVisualStyleBackColor = true;
            // 
            // RadioSimple
            // 
            this.RadioSimple.AutoSize = true;
            this.RadioSimple.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioSimple.Location = new System.Drawing.Point(17, 73);
            this.RadioSimple.Name = "RadioSimple";
            this.RadioSimple.Size = new System.Drawing.Size(67, 20);
            this.RadioSimple.TabIndex = 8;
            this.RadioSimple.TabStop = true;
            this.RadioSimple.Text = "Simple";
            this.RadioSimple.UseVisualStyleBackColor = true;
            // 
            // RadioRainbow
            // 
            this.RadioRainbow.AutoSize = true;
            this.RadioRainbow.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioRainbow.Location = new System.Drawing.Point(17, 98);
            this.RadioRainbow.Name = "RadioRainbow";
            this.RadioRainbow.Size = new System.Drawing.Size(155, 20);
            this.RadioRainbow.TabIndex = 9;
            this.RadioRainbow.TabStop = true;
            this.RadioRainbow.Text = "Rainbow (Horizontal)";
            this.RadioRainbow.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RadioClassic);
            this.groupBox1.Controls.Add(this.RadioRainbow);
            this.groupBox1.Controls.Add(this.RadioPrisum);
            this.groupBox1.Controls.Add(this.RadioSimple);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 133);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Style";
            // 
            // EnumerateButton
            // 
            this.EnumerateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EnumerateButton.Location = new System.Drawing.Point(458, 28);
            this.EnumerateButton.Name = "EnumerateButton";
            this.EnumerateButton.Size = new System.Drawing.Size(75, 23);
            this.EnumerateButton.TabIndex = 11;
            this.EnumerateButton.Text = "Enumerate";
            this.EnumerateButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TrackBar1);
            this.groupBox2.Controls.Add(this.TextBox_Sensibility);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 71);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sensibility (1.0 - 9.9)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Device";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(458, 57);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(179, 71);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "Depending on the number of output device (disabled or not), enumerating may take " +
    "several minute.";
            // 
            // ComboBox1
            // 
            this.ComboBox1.DisplayMember = "1,2,4,8,16";
            this.ComboBox1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16"});
            this.ComboBox1.Location = new System.Drawing.Point(344, 59);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(47, 24);
            this.ComboBox1.TabIndex = 16;
            this.ComboBox1.ValueMember = "1,2,4,8,16";
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(204, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Number of Band :";
            // 
            // ComboBox2
            // 
            this.ComboBox2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox2.FormattingEnabled = true;
            this.ComboBox2.Items.AddRange(new object[] {
            "500",
            "1000",
            "1500",
            "2000",
            "2500",
            "3000",
            "3500",
            "4000"});
            this.ComboBox2.Location = new System.Drawing.Point(333, 87);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(58, 24);
            this.ComboBox2.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(204, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Peakhold Time :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F);
            this.label4.Location = new System.Drawing.Point(393, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "msec.";
            // 
            // TrackBar2
            // 
            this.TrackBar2.LargeChange = 4;
            this.TrackBar2.Location = new System.Drawing.Point(6, 18);
            this.TrackBar2.Maximum = 20;
            this.TrackBar2.Minimum = 4;
            this.TrackBar2.Name = "TrackBar2";
            this.TrackBar2.Size = new System.Drawing.Size(267, 45);
            this.TrackBar2.SmallChange = 2;
            this.TrackBar2.TabIndex = 21;
            this.TrackBar2.TickFrequency = 2;
            this.TrackBar2.Value = 16;
            this.TrackBar2.ValueChanged += new System.EventHandler(this.TrackBar2_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TextBox_DecaySpeed);
            this.groupBox3.Controls.Add(this.TrackBar2);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 270);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(334, 66);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Peakhold Decay Speed (4 - 20)";
            // 
            // TextBox_DecaySpeed
            // 
            this.TextBox_DecaySpeed.Location = new System.Drawing.Point(279, 18);
            this.TextBox_DecaySpeed.Name = "TextBox_DecaySpeed";
            this.TextBox_DecaySpeed.Size = new System.Drawing.Size(43, 23);
            this.TextBox_DecaySpeed.TabIndex = 22;
            this.TextBox_DecaySpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBox_DecaySpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_DecaySpeed_KeyDown);
            // 
            // SSaverCheckBox
            // 
            this.SSaverCheckBox.AutoSize = true;
            this.SSaverCheckBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSaverCheckBox.Location = new System.Drawing.Point(458, 152);
            this.SSaverCheckBox.Name = "SSaverCheckBox";
            this.SSaverCheckBox.Size = new System.Drawing.Size(165, 20);
            this.SSaverCheckBox.TabIndex = 23;
            this.SSaverCheckBox.Text = "Prevant Screen Saver";
            this.SSaverCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 348);
            this.ControlBox = false;
            this.Controls.Add(this.SSaverCheckBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ComboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.EnumerateButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.devicelist);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Options - SpeAnaLED";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox devicelist;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.TextBox TextBox_Sensibility;
        public System.Windows.Forms.TrackBar TrackBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button EnumerateButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.ComboBox ComboBox1;
        public System.Windows.Forms.RadioButton RadioClassic;
        public System.Windows.Forms.RadioButton RadioPrisum;
        public System.Windows.Forms.RadioButton RadioSimple;
        public System.Windows.Forms.RadioButton RadioRainbow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox ComboBox2;
        public System.Windows.Forms.TrackBar TrackBar2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TextBox_DecaySpeed;
        private System.Windows.Forms.CheckBox SSaverCheckBox;
    }
}