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
            this.SSaverCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GroupFlip = new System.Windows.Forms.GroupBox();
            this.NoFlipRadio = new System.Windows.Forms.RadioButton();
            this.RightFlipRadio = new System.Windows.Forms.RadioButton();
            this.LeftFlipRadio = new System.Windows.Forms.RadioButton();
            this.HorizontalRadio = new System.Windows.Forms.RadioButton();
            this.VerticalRadio = new System.Windows.Forms.RadioButton();
            this.PeakholdCheckBox = new System.Windows.Forms.CheckBox();
            this.AlwaysOnTopCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.RadioStereo = new System.Windows.Forms.RadioButton();
            this.RadioMono = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.ShowCounterCheckBox = new System.Windows.Forms.CheckBox();
            this.HideFreqCheckBox = new System.Windows.Forms.CheckBox();
            this.HideTitleCheckBox = new System.Windows.Forms.CheckBox();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ExitAppButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeakholdDecayTimeTrackBar)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.GroupFlip.SuspendLayout();
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
            this.CloseButton.Location = new System.Drawing.Point(562, 419);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 272);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 71);
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
            this.PeakholdTimeComboBox.Location = new System.Drawing.Point(333, 108);
            this.PeakholdTimeComboBox.Name = "PeakholdTimeComboBox";
            this.PeakholdTimeComboBox.Size = new System.Drawing.Size(58, 24);
            this.PeakholdTimeComboBox.TabIndex = 8;
            // 
            // LabelPeakhold
            // 
            this.LabelPeakhold.AutoSize = true;
            this.LabelPeakhold.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPeakhold.Location = new System.Drawing.Point(219, 111);
            this.LabelPeakhold.Name = "LabelPeakhold";
            this.LabelPeakhold.Size = new System.Drawing.Size(96, 15);
            this.LabelPeakhold.TabIndex = 19;
            this.LabelPeakhold.Text = "Peakhold Time :";
            // 
            // LabelMsec
            // 
            this.LabelMsec.AutoSize = true;
            this.LabelMsec.Font = new System.Drawing.Font("Arial", 9F);
            this.LabelMsec.Location = new System.Drawing.Point(393, 111);
            this.LabelMsec.Name = "LabelMsec";
            this.LabelMsec.Size = new System.Drawing.Size(41, 15);
            this.LabelMsec.TabIndex = 20;
            this.LabelMsec.Text = "msec.";
            // 
            // PeakholdDecayTimeTrackBar
            // 
            this.PeakholdDecayTimeTrackBar.LargeChange = 4;
            this.PeakholdDecayTimeTrackBar.Location = new System.Drawing.Point(6, 18);
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
            this.groupBox3.Controls.Add(this.DecaySpeedTextBox);
            this.groupBox3.Controls.Add(this.PeakholdDecayTimeTrackBar);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 349);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(379, 66);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Peakhold Decay Speed (4 - 20)";
            // 
            // DecaySpeedTextBox
            // 
            this.DecaySpeedTextBox.Location = new System.Drawing.Point(330, 18);
            this.DecaySpeedTextBox.Name = "DecaySpeedTextBox";
            this.DecaySpeedTextBox.Size = new System.Drawing.Size(43, 21);
            this.DecaySpeedTextBox.TabIndex = 17;
            this.DecaySpeedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DecaySpeedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_DecaySpeed_KeyDown);
            // 
            // SSaverCheckBox
            // 
            this.SSaverCheckBox.AutoSize = true;
            this.SSaverCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSaverCheckBox.Location = new System.Drawing.Point(206, 169);
            this.SSaverCheckBox.Name = "SSaverCheckBox";
            this.SSaverCheckBox.Size = new System.Drawing.Size(143, 19);
            this.SSaverCheckBox.TabIndex = 20;
            this.SSaverCheckBox.Text = "Prevent Screen Saver";
            this.SSaverCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.GroupFlip);
            this.groupBox4.Controls.Add(this.HorizontalRadio);
            this.groupBox4.Controls.Add(this.VerticalRadio);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(7, 82);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(204, 181);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Channel Layout";
            // 
            // GroupFlip
            // 
            this.GroupFlip.Controls.Add(this.NoFlipRadio);
            this.GroupFlip.Controls.Add(this.RightFlipRadio);
            this.GroupFlip.Controls.Add(this.LeftFlipRadio);
            this.GroupFlip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupFlip.Location = new System.Drawing.Point(31, 76);
            this.GroupFlip.Name = "GroupFlip";
            this.GroupFlip.Size = new System.Drawing.Size(163, 97);
            this.GroupFlip.TabIndex = 12;
            this.GroupFlip.TabStop = false;
            this.GroupFlip.Text = "Horizontal Flip";
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
            this.LeftFlipRadio.Location = new System.Drawing.Point(18, 49);
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
            this.HorizontalRadio.Location = new System.Drawing.Point(16, 48);
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
            this.VerticalRadio.Location = new System.Drawing.Point(16, 23);
            this.VerticalRadio.Name = "VerticalRadio";
            this.VerticalRadio.Size = new System.Drawing.Size(64, 19);
            this.VerticalRadio.TabIndex = 10;
            this.VerticalRadio.TabStop = true;
            this.VerticalRadio.Text = "Vertical";
            this.VerticalRadio.UseVisualStyleBackColor = true;
            // 
            // PeakholdCheckBox
            // 
            this.PeakholdCheckBox.AutoSize = true;
            this.PeakholdCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeakholdCheckBox.Location = new System.Drawing.Point(207, 88);
            this.PeakholdCheckBox.Name = "PeakholdCheckBox";
            this.PeakholdCheckBox.Size = new System.Drawing.Size(78, 19);
            this.PeakholdCheckBox.TabIndex = 18;
            this.PeakholdCheckBox.Text = "Peakhold";
            this.PeakholdCheckBox.UseVisualStyleBackColor = true;
            // 
            // AlwaysOnTopCheckBox
            // 
            this.AlwaysOnTopCheckBox.AutoSize = true;
            this.AlwaysOnTopCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlwaysOnTopCheckBox.Location = new System.Drawing.Point(206, 143);
            this.AlwaysOnTopCheckBox.Name = "AlwaysOnTopCheckBox";
            this.AlwaysOnTopCheckBox.Size = new System.Drawing.Size(104, 19);
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
            this.groupBox5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(417, 143);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(220, 270);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Number of Channels";
            // 
            // RadioStereo
            // 
            this.RadioStereo.AutoSize = true;
            this.RadioStereo.Checked = true;
            this.RadioStereo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioStereo.Location = new System.Drawing.Point(122, 20);
            this.RadioStereo.Name = "RadioStereo";
            this.RadioStereo.Size = new System.Drawing.Size(74, 19);
            this.RadioStereo.TabIndex = 2;
            this.RadioStereo.TabStop = true;
            this.RadioStereo.Text = "2: Stereo";
            this.RadioStereo.UseVisualStyleBackColor = true;
            // 
            // RadioMono
            // 
            this.RadioMono.AutoSize = true;
            this.RadioMono.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioMono.Location = new System.Drawing.Point(23, 20);
            this.RadioMono.Name = "RadioMono";
            this.RadioMono.Size = new System.Drawing.Size(93, 19);
            this.RadioMono.TabIndex = 1;
            this.RadioMono.TabStop = true;
            this.RadioMono.Text = "1: Mono(Mix)";
            this.RadioMono.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "This needs App restart";
            // 
            // ShowCounterCheckBox
            // 
            this.ShowCounterCheckBox.AutoSize = true;
            this.ShowCounterCheckBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowCounterCheckBox.Location = new System.Drawing.Point(18, 427);
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
            this.HideFreqCheckBox.Location = new System.Drawing.Point(206, 196);
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
            this.HideTitleCheckBox.Location = new System.Drawing.Point(206, 221);
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
            this.LinkLabel1.Location = new System.Drawing.Point(178, 427);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(230, 15);
            this.LinkLabel1.TabIndex = 26;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "https://github.com/osamusg/SpeAnaLED";
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ExitAppButton
            // 
            this.ExitAppButton.Enabled = false;
            this.ExitAppButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitAppButton.Location = new System.Drawing.Point(308, 218);
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
            this.label3.Location = new System.Drawing.Point(220, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 14);
            this.label3.TabIndex = 28;
            this.label3.Text = "Can\'t resize the form if checked.";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 457);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExitAppButton);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.HideTitleCheckBox);
            this.Controls.Add(this.HideFreqCheckBox);
            this.Controls.Add(this.ShowCounterCheckBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.AlwaysOnTopCheckBox);
            this.Controls.Add(this.PeakholdCheckBox);
            this.Controls.Add(this.SSaverCheckBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.LabelMsec);
            this.Controls.Add(this.LabelPeakhold);
            this.Controls.Add(this.PeakholdTimeComboBox);
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.GroupFlip.ResumeLayout(false);
            this.GroupFlip.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
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
        protected internal System.Windows.Forms.RadioButton RadioStereo;
        protected internal System.Windows.Forms.RadioButton RadioMono;
        protected internal System.Windows.Forms.CheckBox ShowCounterCheckBox;
        protected internal System.Windows.Forms.GroupBox GroupFlip;
        protected internal System.Windows.Forms.Label LabelPeakhold;
        protected internal System.Windows.Forms.Label LabelMsec;
        protected internal System.Windows.Forms.CheckBox HideFreqCheckBox;
        protected internal System.Windows.Forms.LinkLabel LinkLabel1;
        protected internal System.Windows.Forms.CheckBox HideTitleCheckBox;
        protected internal System.Windows.Forms.Button ExitAppButton;
        private System.Windows.Forms.Label label3;
    }
}