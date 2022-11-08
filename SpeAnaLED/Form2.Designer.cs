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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.devicelist = new System.Windows.Forms.ComboBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.TextBox_Sensitivity = new System.Windows.Forms.TextBox();
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
            this.LabelPeakhold = new System.Windows.Forms.Label();
            this.LabelMsec = new System.Windows.Forms.Label();
            this.TrackBar2 = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TextBox_DecaySpeed = new System.Windows.Forms.TextBox();
            this.SSaverCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CheckBoxFlip = new System.Windows.Forms.CheckBox();
            this.RadioHorizontal = new System.Windows.Forms.RadioButton();
            this.RadioVertical = new System.Windows.Forms.RadioButton();
            this.PeakholdCheckBox = new System.Windows.Forms.CheckBox();
            this.AlwaysOnTopCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.RadioStereo = new System.Windows.Forms.RadioButton();
            this.RadioMono = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.ShowCyclCheckBox = new System.Windows.Forms.CheckBox();
            this.GroupFlip = new System.Windows.Forms.GroupBox();
            this.RadioLeftFlip = new System.Windows.Forms.RadioButton();
            this.RadioRightFlip = new System.Windows.Forms.RadioButton();
            this.RadioNoFlip = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.GroupFlip.SuspendLayout();
            this.SuspendLayout();
            // 
            // devicelist
            // 
            this.devicelist.FormattingEnabled = true;
            this.devicelist.Location = new System.Drawing.Point(12, 28);
            this.devicelist.Name = "devicelist";
            this.devicelist.Size = new System.Drawing.Size(440, 20);
            this.devicelist.TabIndex = 1;
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(562, 419);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Close";
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
            this.TrackBar1.TabIndex = 13;
            this.TrackBar1.Value = 78;
            this.TrackBar1.ValueChanged += new System.EventHandler(this.TrackBar1_ValueChanged);
            // 
            // TextBox_Sensitivity
            // 
            this.TextBox_Sensitivity.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox_Sensitivity.Location = new System.Drawing.Point(279, 18);
            this.TextBox_Sensitivity.Name = "TextBox_Sensitivity";
            this.TextBox_Sensitivity.Size = new System.Drawing.Size(43, 23);
            this.TextBox_Sensitivity.TabIndex = 14;
            this.TextBox_Sensitivity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBox_Sensitivity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Sensitivity_KeyDown);
            // 
            // RadioClassic
            // 
            this.RadioClassic.AutoSize = true;
            this.RadioClassic.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioClassic.Location = new System.Drawing.Point(17, 47);
            this.RadioClassic.Name = "RadioClassic";
            this.RadioClassic.Size = new System.Drawing.Size(70, 20);
            this.RadioClassic.TabIndex = 4;
            this.RadioClassic.TabStop = true;
            this.RadioClassic.Text = "Classic";
            this.RadioClassic.UseVisualStyleBackColor = true;
            // 
            // RadioPrisum
            // 
            this.RadioPrisum.AutoSize = true;
            this.RadioPrisum.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioPrisum.Location = new System.Drawing.Point(17, 22);
            this.RadioPrisum.Name = "RadioPrisum";
            this.RadioPrisum.Size = new System.Drawing.Size(52, 20);
            this.RadioPrisum.TabIndex = 5;
            this.RadioPrisum.TabStop = true;
            this.RadioPrisum.Text = "LED";
            this.RadioPrisum.UseVisualStyleBackColor = true;
            // 
            // RadioSimple
            // 
            this.RadioSimple.AutoSize = true;
            this.RadioSimple.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioSimple.Location = new System.Drawing.Point(17, 73);
            this.RadioSimple.Name = "RadioSimple";
            this.RadioSimple.Size = new System.Drawing.Size(67, 20);
            this.RadioSimple.TabIndex = 6;
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
            this.RadioRainbow.TabIndex = 7;
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
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Style";
            // 
            // EnumerateButton
            // 
            this.EnumerateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EnumerateButton.Location = new System.Drawing.Point(458, 28);
            this.EnumerateButton.Name = "EnumerateButton";
            this.EnumerateButton.Size = new System.Drawing.Size(75, 23);
            this.EnumerateButton.TabIndex = 2;
            this.EnumerateButton.Text = "Enumerate";
            this.EnumerateButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TrackBar1);
            this.groupBox2.Controls.Add(this.TextBox_Sensitivity);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 226);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 71);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sensitivity (1.0 - 9.9)";
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
            this.textBox1.TabIndex = 21;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Depending on the number of output device (disabled or not), enumerating may take " +
    "several minute.";
            // 
            // ComboBox1
            // 
            this.ComboBox1.DisplayMember = "1,2,4,8,16";
            this.ComboBox1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Items.AddRange(new object[] {
            "4",
            "8",
            "16"});
            this.ComboBox1.Location = new System.Drawing.Point(344, 59);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(47, 24);
            this.ComboBox1.TabIndex = 7;
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
            this.ComboBox2.Location = new System.Drawing.Point(333, 108);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(58, 24);
            this.ComboBox2.TabIndex = 8;
            // 
            // LabelPeakhold
            // 
            this.LabelPeakhold.AutoSize = true;
            this.LabelPeakhold.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPeakhold.Location = new System.Drawing.Point(219, 111);
            this.LabelPeakhold.Name = "LabelPeakhold";
            this.LabelPeakhold.Size = new System.Drawing.Size(108, 16);
            this.LabelPeakhold.TabIndex = 19;
            this.LabelPeakhold.Text = "Peakhold Time :";
            // 
            // LabelMsec
            // 
            this.LabelMsec.AutoSize = true;
            this.LabelMsec.Font = new System.Drawing.Font("Arial", 10F);
            this.LabelMsec.Location = new System.Drawing.Point(393, 111);
            this.LabelMsec.Name = "LabelMsec";
            this.LabelMsec.Size = new System.Drawing.Size(44, 16);
            this.LabelMsec.TabIndex = 20;
            this.LabelMsec.Text = "msec.";
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
            this.TrackBar2.TabIndex = 16;
            this.TrackBar2.TickFrequency = 2;
            this.TrackBar2.Value = 10;
            this.TrackBar2.ValueChanged += new System.EventHandler(this.TrackBar2_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TextBox_DecaySpeed);
            this.groupBox3.Controls.Add(this.TrackBar2);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 303);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(334, 66);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Peakhold Decay Speed (4 - 20)";
            // 
            // TextBox_DecaySpeed
            // 
            this.TextBox_DecaySpeed.Location = new System.Drawing.Point(279, 18);
            this.TextBox_DecaySpeed.Name = "TextBox_DecaySpeed";
            this.TextBox_DecaySpeed.Size = new System.Drawing.Size(43, 23);
            this.TextBox_DecaySpeed.TabIndex = 17;
            this.TextBox_DecaySpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBox_DecaySpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_DecaySpeed_KeyDown);
            // 
            // SSaverCheckBox
            // 
            this.SSaverCheckBox.AutoSize = true;
            this.SSaverCheckBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSaverCheckBox.Location = new System.Drawing.Point(206, 169);
            this.SSaverCheckBox.Name = "SSaverCheckBox";
            this.SSaverCheckBox.Size = new System.Drawing.Size(165, 20);
            this.SSaverCheckBox.TabIndex = 20;
            this.SSaverCheckBox.Text = "Prevent Screen Saver";
            this.SSaverCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.CheckBoxFlip);
            this.groupBox4.Controls.Add(this.GroupFlip);
            this.groupBox4.Controls.Add(this.RadioHorizontal);
            this.groupBox4.Controls.Add(this.RadioVertical);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(7, 82);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(204, 181);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Channel Layout";
            // 
            // CheckBoxFlip
            // 
            this.CheckBoxFlip.AutoSize = true;
            this.CheckBoxFlip.Location = new System.Drawing.Point(121, 50);
            this.CheckBoxFlip.Name = "CheckBoxFlip";
            this.CheckBoxFlip.Size = new System.Drawing.Size(77, 20);
            this.CheckBoxFlip.TabIndex = 12;
            this.CheckBoxFlip.Text = "Flip Left";
            this.CheckBoxFlip.UseVisualStyleBackColor = true;
            // 
            // RadioHorizontal
            // 
            this.RadioHorizontal.AutoSize = true;
            this.RadioHorizontal.Location = new System.Drawing.Point(21, 50);
            this.RadioHorizontal.Name = "RadioHorizontal";
            this.RadioHorizontal.Size = new System.Drawing.Size(87, 20);
            this.RadioHorizontal.TabIndex = 11;
            this.RadioHorizontal.TabStop = true;
            this.RadioHorizontal.Text = "Horizontal";
            this.RadioHorizontal.UseVisualStyleBackColor = true;
            // 
            // RadioVertical
            // 
            this.RadioVertical.AutoSize = true;
            this.RadioVertical.Location = new System.Drawing.Point(21, 22);
            this.RadioVertical.Name = "RadioVertical";
            this.RadioVertical.Size = new System.Drawing.Size(71, 20);
            this.RadioVertical.TabIndex = 10;
            this.RadioVertical.TabStop = true;
            this.RadioVertical.Text = "Vertical";
            this.RadioVertical.UseVisualStyleBackColor = true;
            // 
            // PeakholdCheckBox
            // 
            this.PeakholdCheckBox.AutoSize = true;
            this.PeakholdCheckBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeakholdCheckBox.Location = new System.Drawing.Point(207, 88);
            this.PeakholdCheckBox.Name = "PeakholdCheckBox";
            this.PeakholdCheckBox.Size = new System.Drawing.Size(85, 20);
            this.PeakholdCheckBox.TabIndex = 18;
            this.PeakholdCheckBox.Text = "Peakhold";
            this.PeakholdCheckBox.UseVisualStyleBackColor = true;
            // 
            // AlwaysOnTopCheckBox
            // 
            this.AlwaysOnTopCheckBox.AutoSize = true;
            this.AlwaysOnTopCheckBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlwaysOnTopCheckBox.Location = new System.Drawing.Point(206, 143);
            this.AlwaysOnTopCheckBox.Name = "AlwaysOnTopCheckBox";
            this.AlwaysOnTopCheckBox.Size = new System.Drawing.Size(116, 20);
            this.AlwaysOnTopCheckBox.TabIndex = 19;
            this.AlwaysOnTopCheckBox.Text = "Always on Top";
            this.AlwaysOnTopCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.RadioStereo);
            this.groupBox5.Controls.Add(this.RadioMono);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(417, 143);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(220, 270);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Number of Channel";
            // 
            // RadioStereo
            // 
            this.RadioStereo.AutoSize = true;
            this.RadioStereo.Checked = true;
            this.RadioStereo.Location = new System.Drawing.Point(128, 23);
            this.RadioStereo.Name = "RadioStereo";
            this.RadioStereo.Size = new System.Drawing.Size(83, 20);
            this.RadioStereo.TabIndex = 2;
            this.RadioStereo.TabStop = true;
            this.RadioStereo.Text = "2: Stereo";
            this.RadioStereo.UseVisualStyleBackColor = true;
            // 
            // RadioMono
            // 
            this.RadioMono.AutoSize = true;
            this.RadioMono.Location = new System.Drawing.Point(7, 23);
            this.RadioMono.Name = "RadioMono";
            this.RadioMono.Size = new System.Drawing.Size(106, 20);
            this.RadioMono.TabIndex = 1;
            this.RadioMono.TabStop = true;
            this.RadioMono.Text = "1: Mono(Mix)";
            this.RadioMono.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "This needs App restart";
            // 
            // ShowCyclCheckBox
            // 
            this.ShowCyclCheckBox.AutoSize = true;
            this.ShowCyclCheckBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowCyclCheckBox.Location = new System.Drawing.Point(206, 395);
            this.ShowCyclCheckBox.Name = "ShowCyclCheckBox";
            this.ShowCyclCheckBox.Size = new System.Drawing.Size(126, 18);
            this.ShowCyclCheckBox.TabIndex = 23;
            this.ShowCyclCheckBox.Text = "Show Cycle (debug)";
            this.ShowCyclCheckBox.UseVisualStyleBackColor = true;
            // 
            // GroupFlip
            // 
            this.GroupFlip.Controls.Add(this.RadioNoFlip);
            this.GroupFlip.Controls.Add(this.RadioRightFlip);
            this.GroupFlip.Controls.Add(this.RadioLeftFlip);
            this.GroupFlip.Location = new System.Drawing.Point(31, 76);
            this.GroupFlip.Name = "GroupFlip";
            this.GroupFlip.Size = new System.Drawing.Size(163, 97);
            this.GroupFlip.TabIndex = 12;
            this.GroupFlip.TabStop = false;
            this.GroupFlip.Text = "Flip";
            // 
            // RadioLeftFlip
            // 
            this.RadioLeftFlip.AutoSize = true;
            this.RadioLeftFlip.Location = new System.Drawing.Point(18, 75);
            this.RadioLeftFlip.Name = "RadioLeftFlip";
            this.RadioLeftFlip.Size = new System.Drawing.Size(138, 20);
            this.RadioLeftFlip.TabIndex = 0;
            this.RadioLeftFlip.TabStop = true;
            this.RadioLeftFlip.Text = "L.ch (Center Low)";
            this.RadioLeftFlip.UseVisualStyleBackColor = true;
            // 
            // RadioRightFlip
            // 
            this.RadioRightFlip.AutoSize = true;
            this.RadioRightFlip.Location = new System.Drawing.Point(18, 49);
            this.RadioRightFlip.Name = "RadioRightFlip";
            this.RadioRightFlip.Size = new System.Drawing.Size(127, 20);
            this.RadioRightFlip.TabIndex = 1;
            this.RadioRightFlip.TabStop = true;
            this.RadioRightFlip.Text = "R.ch (Center Hi)";
            this.RadioRightFlip.UseVisualStyleBackColor = true;
            // 
            // RadioNoFlip
            // 
            this.RadioNoFlip.AutoSize = true;
            this.RadioNoFlip.Location = new System.Drawing.Point(18, 23);
            this.RadioNoFlip.Name = "RadioNoFlip";
            this.RadioNoFlip.Size = new System.Drawing.Size(69, 20);
            this.RadioNoFlip.TabIndex = 2;
            this.RadioNoFlip.TabStop = true;
            this.RadioNoFlip.Text = "No Flip";
            this.RadioNoFlip.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 457);
            this.ControlBox = false;
            this.Controls.Add(this.ShowCyclCheckBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.AlwaysOnTopCheckBox);
            this.Controls.Add(this.PeakholdCheckBox);
            this.Controls.Add(this.SSaverCheckBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.LabelMsec);
            this.Controls.Add(this.LabelPeakhold);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Setting - SpeAnaLED";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.GroupFlip.ResumeLayout(false);
            this.GroupFlip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button EnumerateButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        protected internal System.Windows.Forms.RadioButton RadioNoFlip;
        protected internal System.Windows.Forms.RadioButton RadioRightFlip;
        protected internal System.Windows.Forms.RadioButton RadioLeftFlip;
        protected internal System.Windows.Forms.ComboBox devicelist;
        protected internal System.Windows.Forms.TrackBar TrackBar1;
        protected internal System.Windows.Forms.ComboBox ComboBox1;
        protected internal System.Windows.Forms.RadioButton RadioClassic;
        protected internal System.Windows.Forms.RadioButton RadioPrisum;
        protected internal System.Windows.Forms.RadioButton RadioSimple;
        protected internal System.Windows.Forms.RadioButton RadioRainbow;
        protected internal System.Windows.Forms.ComboBox ComboBox2;
        protected internal System.Windows.Forms.TrackBar TrackBar2;
        protected internal System.Windows.Forms.CheckBox SSaverCheckBox;
        protected internal System.Windows.Forms.CheckBox PeakholdCheckBox;
        protected internal System.Windows.Forms.CheckBox AlwaysOnTopCheckBox;
        protected internal System.Windows.Forms.TextBox TextBox_Sensitivity;
        protected internal System.Windows.Forms.TextBox TextBox_DecaySpeed;
        protected internal System.Windows.Forms.RadioButton RadioHorizontal;
        protected internal System.Windows.Forms.RadioButton RadioVertical;
        protected internal System.Windows.Forms.CheckBox CheckBoxFlip;
        protected internal System.Windows.Forms.RadioButton RadioStereo;
        protected internal System.Windows.Forms.RadioButton RadioMono;
        protected internal System.Windows.Forms.CheckBox ShowCyclCheckBox;
        protected internal System.Windows.Forms.GroupBox GroupFlip;
        protected internal System.Windows.Forms.Label LabelPeakhold;
        protected internal System.Windows.Forms.Label LabelMsec;
    }
}