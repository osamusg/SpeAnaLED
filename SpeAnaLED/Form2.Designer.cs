﻿namespace SpeAnaLED
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.SensitivityTrackBar = new System.Windows.Forms.TrackBar();
            this.SensitivityTextBox = new System.Windows.Forms.TextBox();
            this.ClassicRadio = new System.Windows.Forms.RadioButton();
            this.PrisumRadio = new System.Windows.Forms.RadioButton();
            this.SimpleRadio = new System.Windows.Forms.RadioButton();
            this.RainbowRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EnumerateButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.NumberOfBarComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PeakholdTimeComboBox = new System.Windows.Forms.ComboBox();
            this.LabelPeakhold = new System.Windows.Forms.Label();
            this.LabelMsec = new System.Windows.Forms.Label();
            this.PeakholdDecayTimeTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DecaySpeedTextBox = new System.Windows.Forms.TextBox();
            this.PeakholdCheckBox = new System.Windows.Forms.CheckBox();
            this.SSaverCheckBox = new System.Windows.Forms.CheckBox();
            this.ChannelLayoutGroup = new System.Windows.Forms.GroupBox();
            this.FlipGroup = new System.Windows.Forms.GroupBox();
            this.NoFlipRadio = new System.Windows.Forms.RadioButton();
            this.RightFlipRadio = new System.Windows.Forms.RadioButton();
            this.LeftFlipRadio = new System.Windows.Forms.RadioButton();
            this.HorizontalRadio = new System.Windows.Forms.RadioButton();
            this.VerticalRadio = new System.Windows.Forms.RadioButton();
            this.AlwaysOnTopCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.StereoRadio = new System.Windows.Forms.RadioButton();
            this.MonoRadio = new System.Windows.Forms.RadioButton();
            this.ShowCounterCheckBox = new System.Windows.Forms.CheckBox();
            this.HideFreqCheckBox = new System.Windows.Forms.CheckBox();
            this.HideTitleCheckBox = new System.Windows.Forms.CheckBox();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ExitAppButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeakholdDecayTimeTrackBar)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.ChannelLayoutGroup.SuspendLayout();
            this.FlipGroup.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(567, 451);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // SensitivityTrackBar
            // 
            this.SensitivityTrackBar.LargeChange = 10;
            this.SensitivityTrackBar.Location = new System.Drawing.Point(6, 18);
            this.SensitivityTrackBar.Maximum = 99;
            this.SensitivityTrackBar.Minimum = 10;
            this.SensitivityTrackBar.Name = "SensitivityTrackBar";
            this.SensitivityTrackBar.Size = new System.Drawing.Size(318, 45);
            this.SensitivityTrackBar.TabIndex = 13;
            this.SensitivityTrackBar.Value = 78;
            this.SensitivityTrackBar.ValueChanged += new System.EventHandler(this.TrackBar1_ValueChanged);
            // 
            // SensitivityTextBox
            // 
            this.SensitivityTextBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SensitivityTextBox.Location = new System.Drawing.Point(330, 20);
            this.SensitivityTextBox.Name = "SensitivityTextBox";
            this.SensitivityTextBox.Size = new System.Drawing.Size(43, 23);
            this.SensitivityTextBox.TabIndex = 14;
            this.SensitivityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SensitivityTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Sensitivity_KeyDown);
            // 
            // ClassicRadio
            // 
            this.ClassicRadio.AutoSize = true;
            this.ClassicRadio.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClassicRadio.Location = new System.Drawing.Point(17, 47);
            this.ClassicRadio.Name = "ClassicRadio";
            this.ClassicRadio.Size = new System.Drawing.Size(70, 20);
            this.ClassicRadio.TabIndex = 4;
            this.ClassicRadio.TabStop = true;
            this.ClassicRadio.Text = "Classic";
            this.ClassicRadio.UseVisualStyleBackColor = true;
            // 
            // PrisumRadio
            // 
            this.PrisumRadio.AutoSize = true;
            this.PrisumRadio.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrisumRadio.Location = new System.Drawing.Point(17, 22);
            this.PrisumRadio.Name = "PrisumRadio";
            this.PrisumRadio.Size = new System.Drawing.Size(52, 20);
            this.PrisumRadio.TabIndex = 5;
            this.PrisumRadio.TabStop = true;
            this.PrisumRadio.Text = "LED";
            this.PrisumRadio.UseVisualStyleBackColor = true;
            // 
            // SimpleRadio
            // 
            this.SimpleRadio.AutoSize = true;
            this.SimpleRadio.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimpleRadio.Location = new System.Drawing.Point(17, 73);
            this.SimpleRadio.Name = "SimpleRadio";
            this.SimpleRadio.Size = new System.Drawing.Size(67, 20);
            this.SimpleRadio.TabIndex = 6;
            this.SimpleRadio.TabStop = true;
            this.SimpleRadio.Text = "Simple";
            this.SimpleRadio.UseVisualStyleBackColor = true;
            // 
            // RainbowRadio
            // 
            this.RainbowRadio.AutoSize = true;
            this.RainbowRadio.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RainbowRadio.Location = new System.Drawing.Point(17, 98);
            this.RainbowRadio.Name = "RainbowRadio";
            this.RainbowRadio.Size = new System.Drawing.Size(155, 20);
            this.RainbowRadio.TabIndex = 7;
            this.RainbowRadio.TabStop = true;
            this.RainbowRadio.Text = "Rainbow (Horizontal)";
            this.RainbowRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClassicRadio);
            this.groupBox1.Controls.Add(this.RainbowRadio);
            this.groupBox1.Controls.Add(this.PrisumRadio);
            this.groupBox1.Controls.Add(this.SimpleRadio);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 133);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Style";
            // 
            // EnumerateButton
            // 
            this.EnumerateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EnumerateButton.Location = new System.Drawing.Point(458, 26);
            this.EnumerateButton.Name = "EnumerateButton";
            this.EnumerateButton.Size = new System.Drawing.Size(75, 23);
            this.EnumerateButton.TabIndex = 2;
            this.EnumerateButton.Text = "Enumerate";
            this.EnumerateButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SensitivityTrackBar);
            this.groupBox2.Controls.Add(this.SensitivityTextBox);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 71);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sensitivity (1.0 - 9.9)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
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
            // NumberOfBarComboBox
            // 
            this.NumberOfBarComboBox.DisplayMember = "1,2,4,8,16";
            this.NumberOfBarComboBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfBarComboBox.FormattingEnabled = true;
            this.NumberOfBarComboBox.Items.AddRange(new object[] {
            "4",
            "8",
            "16"});
            this.NumberOfBarComboBox.Location = new System.Drawing.Point(344, 59);
            this.NumberOfBarComboBox.Name = "NumberOfBarComboBox";
            this.NumberOfBarComboBox.Size = new System.Drawing.Size(47, 24);
            this.NumberOfBarComboBox.TabIndex = 7;
            this.NumberOfBarComboBox.ValueMember = "1,2,4,8,16";
            this.NumberOfBarComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(203, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Number of Bandwidth :";
            // 
            // PeakholdTimeComboBox
            // 
            this.PeakholdTimeComboBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeakholdTimeComboBox.FormattingEnabled = true;
            this.PeakholdTimeComboBox.Items.AddRange(new object[] {
            "500",
            "1000",
            "1500",
            "2000",
            "2500",
            "3000",
            "3500",
            "4000"});
            this.PeakholdTimeComboBox.Location = new System.Drawing.Point(127, 46);
            this.PeakholdTimeComboBox.Name = "PeakholdTimeComboBox";
            this.PeakholdTimeComboBox.Size = new System.Drawing.Size(58, 24);
            this.PeakholdTimeComboBox.TabIndex = 8;
            // 
            // LabelPeakhold
            // 
            this.LabelPeakhold.AutoSize = true;
            this.LabelPeakhold.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPeakhold.Location = new System.Drawing.Point(14, 50);
            this.LabelPeakhold.Name = "LabelPeakhold";
            this.LabelPeakhold.Size = new System.Drawing.Size(96, 15);
            this.LabelPeakhold.TabIndex = 19;
            this.LabelPeakhold.Text = "Peakhold Time :";
            // 
            // LabelMsec
            // 
            this.LabelMsec.AutoSize = true;
            this.LabelMsec.Font = new System.Drawing.Font("Arial", 9F);
            this.LabelMsec.Location = new System.Drawing.Point(191, 50);
            this.LabelMsec.Name = "LabelMsec";
            this.LabelMsec.Size = new System.Drawing.Size(41, 15);
            this.LabelMsec.TabIndex = 20;
            this.LabelMsec.Text = "msec.";
            // 
            // PeakholdDecayTimeTrackBar
            // 
            this.PeakholdDecayTimeTrackBar.LargeChange = 4;
            this.PeakholdDecayTimeTrackBar.Location = new System.Drawing.Point(6, 94);
            this.PeakholdDecayTimeTrackBar.Maximum = 20;
            this.PeakholdDecayTimeTrackBar.Minimum = 4;
            this.PeakholdDecayTimeTrackBar.Name = "PeakholdDecayTimeTrackBar";
            this.PeakholdDecayTimeTrackBar.Size = new System.Drawing.Size(318, 45);
            this.PeakholdDecayTimeTrackBar.SmallChange = 2;
            this.PeakholdDecayTimeTrackBar.TabIndex = 16;
            this.PeakholdDecayTimeTrackBar.TickFrequency = 2;
            this.PeakholdDecayTimeTrackBar.Value = 10;
            this.PeakholdDecayTimeTrackBar.ValueChanged += new System.EventHandler(this.TrackBar2_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.DecaySpeedTextBox);
            this.groupBox3.Controls.Add(this.PeakholdDecayTimeTrackBar);
            this.groupBox3.Controls.Add(this.PeakholdCheckBox);
            this.groupBox3.Controls.Add(this.PeakholdTimeComboBox);
            this.groupBox3.Controls.Add(this.LabelPeakhold);
            this.groupBox3.Controls.Add(this.LabelMsec);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 291);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(386, 143);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Peakhold";
            // 
            // DecaySpeedTextBox
            // 
            this.DecaySpeedTextBox.Location = new System.Drawing.Point(330, 94);
            this.DecaySpeedTextBox.Name = "DecaySpeedTextBox";
            this.DecaySpeedTextBox.Size = new System.Drawing.Size(43, 21);
            this.DecaySpeedTextBox.TabIndex = 17;
            this.DecaySpeedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DecaySpeedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_DecaySpeed_KeyDown);
            // 
            // PeakholdCheckBox
            // 
            this.PeakholdCheckBox.AutoSize = true;
            this.PeakholdCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeakholdCheckBox.Location = new System.Drawing.Point(17, 24);
            this.PeakholdCheckBox.Name = "PeakholdCheckBox";
            this.PeakholdCheckBox.Size = new System.Drawing.Size(72, 19);
            this.PeakholdCheckBox.TabIndex = 18;
            this.PeakholdCheckBox.Text = "Enabled";
            this.PeakholdCheckBox.UseVisualStyleBackColor = true;
            // 
            // SSaverCheckBox
            // 
            this.SSaverCheckBox.AutoSize = true;
            this.SSaverCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSaverCheckBox.Location = new System.Drawing.Point(208, 122);
            this.SSaverCheckBox.Name = "SSaverCheckBox";
            this.SSaverCheckBox.Size = new System.Drawing.Size(143, 19);
            this.SSaverCheckBox.TabIndex = 20;
            this.SSaverCheckBox.Text = "Prevent Screen Saver";
            this.SSaverCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChannelLayoutGroup
            // 
            this.ChannelLayoutGroup.Controls.Add(this.FlipGroup);
            this.ChannelLayoutGroup.Controls.Add(this.HorizontalRadio);
            this.ChannelLayoutGroup.Controls.Add(this.VerticalRadio);
            this.ChannelLayoutGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelLayoutGroup.Location = new System.Drawing.Point(6, 91);
            this.ChannelLayoutGroup.Name = "ChannelLayoutGroup";
            this.ChannelLayoutGroup.Size = new System.Drawing.Size(204, 178);
            this.ChannelLayoutGroup.TabIndex = 9;
            this.ChannelLayoutGroup.TabStop = false;
            this.ChannelLayoutGroup.Text = "Channel Layout";
            // 
            // FlipGroup
            // 
            this.FlipGroup.Controls.Add(this.NoFlipRadio);
            this.FlipGroup.Controls.Add(this.RightFlipRadio);
            this.FlipGroup.Controls.Add(this.LeftFlipRadio);
            this.FlipGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlipGroup.Location = new System.Drawing.Point(24, 73);
            this.FlipGroup.Name = "FlipGroup";
            this.FlipGroup.Size = new System.Drawing.Size(170, 97);
            this.FlipGroup.TabIndex = 12;
            this.FlipGroup.TabStop = false;
            this.FlipGroup.Text = "Horizontal Flip";
            // 
            // NoFlipRadio
            // 
            this.NoFlipRadio.AutoSize = true;
            this.NoFlipRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoFlipRadio.Location = new System.Drawing.Point(18, 23);
            this.NoFlipRadio.Name = "NoFlipRadio";
            this.NoFlipRadio.Size = new System.Drawing.Size(64, 19);
            this.NoFlipRadio.TabIndex = 2;
            this.NoFlipRadio.TabStop = true;
            this.NoFlipRadio.Text = "No Flip";
            this.NoFlipRadio.UseVisualStyleBackColor = true;
            // 
            // RightFlipRadio
            // 
            this.RightFlipRadio.AutoSize = true;
            this.RightFlipRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightFlipRadio.Location = new System.Drawing.Point(18, 72);
            this.RightFlipRadio.Name = "RightFlipRadio";
            this.RightFlipRadio.Size = new System.Drawing.Size(113, 19);
            this.RightFlipRadio.TabIndex = 1;
            this.RightFlipRadio.TabStop = true;
            this.RightFlipRadio.Text = "R.ch (Center Hi)";
            this.RightFlipRadio.UseVisualStyleBackColor = true;
            // 
            // LeftFlipRadio
            // 
            this.LeftFlipRadio.AutoSize = true;
            this.LeftFlipRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftFlipRadio.Location = new System.Drawing.Point(18, 47);
            this.LeftFlipRadio.Name = "LeftFlipRadio";
            this.LeftFlipRadio.Size = new System.Drawing.Size(122, 19);
            this.LeftFlipRadio.TabIndex = 0;
            this.LeftFlipRadio.TabStop = true;
            this.LeftFlipRadio.Text = "L.ch (Center Low)";
            this.LeftFlipRadio.UseVisualStyleBackColor = true;
            // 
            // HorizontalRadio
            // 
            this.HorizontalRadio.AutoSize = true;
            this.HorizontalRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HorizontalRadio.Location = new System.Drawing.Point(13, 48);
            this.HorizontalRadio.Name = "HorizontalRadio";
            this.HorizontalRadio.Size = new System.Drawing.Size(80, 19);
            this.HorizontalRadio.TabIndex = 11;
            this.HorizontalRadio.TabStop = true;
            this.HorizontalRadio.Text = "Horizontal";
            this.HorizontalRadio.UseVisualStyleBackColor = true;
            // 
            // VerticalRadio
            // 
            this.VerticalRadio.AutoSize = true;
            this.VerticalRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerticalRadio.Location = new System.Drawing.Point(13, 23);
            this.VerticalRadio.Name = "VerticalRadio";
            this.VerticalRadio.Size = new System.Drawing.Size(64, 19);
            this.VerticalRadio.TabIndex = 10;
            this.VerticalRadio.TabStop = true;
            this.VerticalRadio.Text = "Vertical";
            this.VerticalRadio.UseVisualStyleBackColor = true;
            // 
            // AlwaysOnTopCheckBox
            // 
            this.AlwaysOnTopCheckBox.AutoSize = true;
            this.AlwaysOnTopCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlwaysOnTopCheckBox.Location = new System.Drawing.Point(208, 96);
            this.AlwaysOnTopCheckBox.Name = "AlwaysOnTopCheckBox";
            this.AlwaysOnTopCheckBox.Size = new System.Drawing.Size(104, 19);
            this.AlwaysOnTopCheckBox.TabIndex = 19;
            this.AlwaysOnTopCheckBox.Text = "Always on Top";
            this.AlwaysOnTopCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.StereoRadio);
            this.groupBox5.Controls.Add(this.MonoRadio);
            this.groupBox5.Controls.Add(this.ChannelLayoutGroup);
            this.groupBox5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(417, 152);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(220, 278);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Number of Channels";
            // 
            // StereoRadio
            // 
            this.StereoRadio.AutoSize = true;
            this.StereoRadio.Checked = true;
            this.StereoRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StereoRadio.Location = new System.Drawing.Point(140, 19);
            this.StereoRadio.Name = "StereoRadio";
            this.StereoRadio.Size = new System.Drawing.Size(74, 19);
            this.StereoRadio.TabIndex = 2;
            this.StereoRadio.TabStop = true;
            this.StereoRadio.Text = "2: Stereo";
            this.StereoRadio.UseVisualStyleBackColor = true;
            // 
            // MonoRadio
            // 
            this.MonoRadio.AutoSize = true;
            this.MonoRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonoRadio.Location = new System.Drawing.Point(19, 20);
            this.MonoRadio.Name = "MonoRadio";
            this.MonoRadio.Size = new System.Drawing.Size(115, 19);
            this.MonoRadio.TabIndex = 1;
            this.MonoRadio.TabStop = true;
            this.MonoRadio.Text = "1: Mono(L/R Mix)";
            this.MonoRadio.UseVisualStyleBackColor = true;
            // 
            // ShowCounterCheckBox
            // 
            this.ShowCounterCheckBox.AutoSize = true;
            this.ShowCounterCheckBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowCounterCheckBox.Location = new System.Drawing.Point(29, 456);
            this.ShowCounterCheckBox.Name = "ShowCounterCheckBox";
            this.ShowCounterCheckBox.Size = new System.Drawing.Size(137, 18);
            this.ShowCounterCheckBox.TabIndex = 23;
            this.ShowCounterCheckBox.Text = "Show Counter (debug)";
            this.ShowCounterCheckBox.UseVisualStyleBackColor = true;
            // 
            // HideFreqCheckBox
            // 
            this.HideFreqCheckBox.AutoSize = true;
            this.HideFreqCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideFreqCheckBox.Location = new System.Drawing.Point(208, 149);
            this.HideFreqCheckBox.Name = "HideFreqCheckBox";
            this.HideFreqCheckBox.Size = new System.Drawing.Size(117, 19);
            this.HideFreqCheckBox.TabIndex = 24;
            this.HideFreqCheckBox.Text = "Hide Freq. Label";
            this.HideFreqCheckBox.UseVisualStyleBackColor = true;
            // 
            // HideTitleCheckBox
            // 
            this.HideTitleCheckBox.AutoSize = true;
            this.HideTitleCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideTitleCheckBox.Location = new System.Drawing.Point(208, 174);
            this.HideTitleCheckBox.Name = "HideTitleCheckBox";
            this.HideTitleCheckBox.Size = new System.Drawing.Size(96, 19);
            this.HideTitleCheckBox.TabIndex = 25;
            this.HideTitleCheckBox.Text = "Hide Titlebar";
            this.HideTitleCheckBox.UseVisualStyleBackColor = true;
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabel1.Location = new System.Drawing.Point(189, 456);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(230, 15);
            this.LinkLabel1.TabIndex = 26;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "https://github.com/osamusg/SpeAnaLED";
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // ExitAppButton
            // 
            this.ExitAppButton.Enabled = false;
            this.ExitAppButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitAppButton.Location = new System.Drawing.Point(310, 171);
            this.ExitAppButton.Name = "ExitAppButton";
            this.ExitAppButton.Size = new System.Drawing.Size(63, 23);
            this.ExitAppButton.TabIndex = 27;
            this.ExitAppButton.Text = "Exit APP";
            this.ExitAppButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(222, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 14);
            this.label3.TabIndex = 28;
            this.label3.Text = "Can\'t resize the form if checked.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "Peakhold Descent Speed (4 - 20)";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 486);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExitAppButton);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.HideTitleCheckBox);
            this.Controls.Add(this.HideFreqCheckBox);
            this.Controls.Add(this.ShowCounterCheckBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.AlwaysOnTopCheckBox);
            this.Controls.Add(this.SSaverCheckBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NumberOfBarComboBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.EnumerateButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.devicelist);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Setting - SpeAnaLED";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeakholdDecayTimeTrackBar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ChannelLayoutGroup.ResumeLayout(false);
            this.ChannelLayoutGroup.PerformLayout();
            this.FlipGroup.ResumeLayout(false);
            this.FlipGroup.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button EnumerateButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        protected internal System.Windows.Forms.RadioButton NoFlipRadio;
        protected internal System.Windows.Forms.RadioButton RightFlipRadio;
        protected internal System.Windows.Forms.RadioButton LeftFlipRadio;
        protected internal System.Windows.Forms.ComboBox devicelist;
        protected internal System.Windows.Forms.TrackBar SensitivityTrackBar;
        protected internal System.Windows.Forms.ComboBox NumberOfBarComboBox;
        protected internal System.Windows.Forms.RadioButton ClassicRadio;
        protected internal System.Windows.Forms.RadioButton PrisumRadio;
        protected internal System.Windows.Forms.RadioButton SimpleRadio;
        protected internal System.Windows.Forms.RadioButton RainbowRadio;
        protected internal System.Windows.Forms.ComboBox PeakholdTimeComboBox;
        protected internal System.Windows.Forms.TrackBar PeakholdDecayTimeTrackBar;
        protected internal System.Windows.Forms.CheckBox SSaverCheckBox;
        protected internal System.Windows.Forms.CheckBox PeakholdCheckBox;
        protected internal System.Windows.Forms.CheckBox AlwaysOnTopCheckBox;
        protected internal System.Windows.Forms.TextBox SensitivityTextBox;
        protected internal System.Windows.Forms.TextBox DecaySpeedTextBox;
        protected internal System.Windows.Forms.RadioButton HorizontalRadio;
        protected internal System.Windows.Forms.RadioButton VerticalRadio;
        protected internal System.Windows.Forms.RadioButton StereoRadio;
        protected internal System.Windows.Forms.RadioButton MonoRadio;
        protected internal System.Windows.Forms.CheckBox ShowCounterCheckBox;
        protected internal System.Windows.Forms.GroupBox FlipGroup;
        protected internal System.Windows.Forms.Label LabelPeakhold;
        protected internal System.Windows.Forms.Label LabelMsec;
        protected internal System.Windows.Forms.CheckBox HideFreqCheckBox;
        protected internal System.Windows.Forms.LinkLabel LinkLabel1;
        protected internal System.Windows.Forms.CheckBox HideTitleCheckBox;
        protected internal System.Windows.Forms.Button ExitAppButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        protected internal System.Windows.Forms.GroupBox ChannelLayoutGroup;
    }
}