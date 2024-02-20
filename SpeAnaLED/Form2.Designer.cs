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
            this.RainbowFlipCheckBox = new System.Windows.Forms.CheckBox();
            this.EnumerateButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.NumberOfBarComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PeakholdTimeComboBox = new System.Windows.Forms.ComboBox();
            this.LabelPeakhold = new System.Windows.Forms.Label();
            this.LabelMsec = new System.Windows.Forms.Label();
            this.PeakholdDescentSpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PeakholdDescentSpeedLabel = new System.Windows.Forms.Label();
            this.PeakholdDescentSpeedTextBox = new System.Windows.Forms.TextBox();
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
            this.DeviceResetButton = new System.Windows.Forms.Button();
            this.FrequencyLabel = new System.Windows.Forms.Label();
            this.RelLabel = new System.Windows.Forms.Label();
            this.HideSpectrumWindowCheckBox = new System.Windows.Forms.CheckBox();
            this.LevelMeterCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoReloadCheckBox = new System.Windows.Forms.CheckBox();
            this.RefreshModeGroupBox = new System.Windows.Forms.GroupBox();
            this.RefreshFastRadio = new System.Windows.Forms.RadioButton();
            this.RefreshNormalRadio = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.LevelSensitivityTrackBar = new System.Windows.Forms.TrackBar();
            this.LevelSensitivityTextBox = new System.Windows.Forms.TextBox();
            this.LevelStreamCheckBox = new System.Windows.Forms.CheckBox();
            this.LevelStreamPanel = new System.Windows.Forms.Panel();
            this.CombineStreamRadioButton = new System.Windows.Forms.RadioButton();
            this.SeparateStreamRadioButton = new System.Windows.Forms.RadioButton();
            this.StreamColorButton = new System.Windows.Forms.Button();
            this.AlfaTextBox = new System.Windows.Forms.TextBox();
            this.AlfaLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeakholdDescentSpeedTrackBar)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.ChannelLayoutGroup.SuspendLayout();
            this.FlipGroup.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.RefreshModeGroupBox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LevelSensitivityTrackBar)).BeginInit();
            this.LevelStreamPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // devicelist
            // 
            this.devicelist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.devicelist.FormattingEnabled = true;
            this.devicelist.Location = new System.Drawing.Point(12, 28);
            this.devicelist.Name = "devicelist";
            this.devicelist.Size = new System.Drawing.Size(440, 20);
            this.devicelist.TabIndex = 38;
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(567, 520);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SensitivityTrackBar
            // 
            this.SensitivityTrackBar.AutoSize = false;
            this.SensitivityTrackBar.LargeChange = 10;
            this.SensitivityTrackBar.Location = new System.Drawing.Point(6, 29);
            this.SensitivityTrackBar.Maximum = 99;
            this.SensitivityTrackBar.Minimum = 10;
            this.SensitivityTrackBar.Name = "SensitivityTrackBar";
            this.SensitivityTrackBar.Size = new System.Drawing.Size(318, 21);
            this.SensitivityTrackBar.TabIndex = 15;
            this.SensitivityTrackBar.Value = 78;
            this.SensitivityTrackBar.ValueChanged += new System.EventHandler(this.SensitivityTrackBar_ValueChanged);
            // 
            // SensitivityTextBox
            // 
            this.SensitivityTextBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SensitivityTextBox.Location = new System.Drawing.Point(330, 26);
            this.SensitivityTextBox.Name = "SensitivityTextBox";
            this.SensitivityTextBox.Size = new System.Drawing.Size(43, 21);
            this.SensitivityTextBox.TabIndex = 16;
            this.SensitivityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SensitivityTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SensitivityTextBox_KeyDown);
            this.SensitivityTextBox.Leave += new System.EventHandler(this.SensitivityTextBox_Leave);
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
            this.PrisumRadio.TabIndex = 3;
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
            this.SimpleRadio.TabIndex = 5;
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
            this.RainbowRadio.TabIndex = 6;
            this.RainbowRadio.TabStop = true;
            this.RainbowRadio.Text = "Rainbow (Horizontal)";
            this.RainbowRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RainbowFlipCheckBox);
            this.groupBox1.Controls.Add(this.ClassicRadio);
            this.groupBox1.Controls.Add(this.RainbowRadio);
            this.groupBox1.Controls.Add(this.PrisumRadio);
            this.groupBox1.Controls.Add(this.SimpleRadio);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 147);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spectrum Color Scheme";
            // 
            // RainbowFlipCheckBox
            // 
            this.RainbowFlipCheckBox.AutoSize = true;
            this.RainbowFlipCheckBox.Location = new System.Drawing.Point(36, 117);
            this.RainbowFlipCheckBox.Name = "RainbowFlipCheckBox";
            this.RainbowFlipCheckBox.Size = new System.Drawing.Size(79, 19);
            this.RainbowFlipCheckBox.TabIndex = 7;
            this.RainbowFlipCheckBox.Text = "Flip Color";
            this.RainbowFlipCheckBox.UseVisualStyleBackColor = true;
            // 
            // EnumerateButton
            // 
            this.EnumerateButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnumerateButton.Location = new System.Drawing.Point(513, 92);
            this.EnumerateButton.Name = "EnumerateButton";
            this.EnumerateButton.Size = new System.Drawing.Size(129, 23);
            this.EnumerateButton.TabIndex = 39;
            this.EnumerateButton.Text = "Enumerate Devices";
            this.EnumerateButton.UseVisualStyleBackColor = true;
            this.EnumerateButton.Click += new System.EventHandler(this.EnumerateButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SensitivityTrackBar);
            this.groupBox2.Controls.Add(this.SensitivityTextBox);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 394);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 59);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Spectrum Sensitivity (1.0 - 9.9)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "WASAPI Device";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(469, 122);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 71);
            this.textBox1.TabIndex = 35;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Depending on the number of device (disabled or not) you have, enumerating may tak" +
    "e several minutes.";
            // 
            // NumberOfBarComboBox
            // 
            this.NumberOfBarComboBox.DisplayMember = "1,2,4,8,16";
            this.NumberOfBarComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.NumberOfBarComboBox.SelectedIndexChanged += new System.EventHandler(this.NumberOfBarComboBox_SelectedIndexChanged);
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
            this.PeakholdTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PeakholdTimeComboBox.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeakholdTimeComboBox.FormattingEnabled = true;
            this.PeakholdTimeComboBox.Items.AddRange(new object[] {
            "250",
            "500",
            "1000",
            "1500",
            "2000"});
            this.PeakholdTimeComboBox.Location = new System.Drawing.Point(197, 14);
            this.PeakholdTimeComboBox.Name = "PeakholdTimeComboBox";
            this.PeakholdTimeComboBox.Size = new System.Drawing.Size(58, 24);
            this.PeakholdTimeComboBox.TabIndex = 20;
            // 
            // LabelPeakhold
            // 
            this.LabelPeakhold.AutoSize = true;
            this.LabelPeakhold.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPeakhold.Location = new System.Drawing.Point(95, 18);
            this.LabelPeakhold.Name = "LabelPeakhold";
            this.LabelPeakhold.Size = new System.Drawing.Size(96, 15);
            this.LabelPeakhold.TabIndex = 19;
            this.LabelPeakhold.Text = "Peakhold Time :";
            // 
            // LabelMsec
            // 
            this.LabelMsec.AutoSize = true;
            this.LabelMsec.Font = new System.Drawing.Font("Arial", 9F);
            this.LabelMsec.Location = new System.Drawing.Point(261, 18);
            this.LabelMsec.Name = "LabelMsec";
            this.LabelMsec.Size = new System.Drawing.Size(41, 15);
            this.LabelMsec.TabIndex = 20;
            this.LabelMsec.Text = "msec.";
            // 
            // PeakholdDescentSpeedTrackBar
            // 
            this.PeakholdDescentSpeedTrackBar.AutoSize = false;
            this.PeakholdDescentSpeedTrackBar.LargeChange = 4;
            this.PeakholdDescentSpeedTrackBar.Location = new System.Drawing.Point(6, 69);
            this.PeakholdDescentSpeedTrackBar.Maximum = 20;
            this.PeakholdDescentSpeedTrackBar.Minimum = 4;
            this.PeakholdDescentSpeedTrackBar.Name = "PeakholdDescentSpeedTrackBar";
            this.PeakholdDescentSpeedTrackBar.Size = new System.Drawing.Size(318, 21);
            this.PeakholdDescentSpeedTrackBar.SmallChange = 2;
            this.PeakholdDescentSpeedTrackBar.TabIndex = 22;
            this.PeakholdDescentSpeedTrackBar.TickFrequency = 2;
            this.PeakholdDescentSpeedTrackBar.Value = 10;
            this.PeakholdDescentSpeedTrackBar.ValueChanged += new System.EventHandler(this.PeakholdDescentSpeedTrackBar_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.PeakholdDescentSpeedLabel);
            this.groupBox3.Controls.Add(this.PeakholdDescentSpeedTextBox);
            this.groupBox3.Controls.Add(this.PeakholdDescentSpeedTrackBar);
            this.groupBox3.Controls.Add(this.PeakholdCheckBox);
            this.groupBox3.Controls.Add(this.PeakholdTimeComboBox);
            this.groupBox3.Controls.Add(this.LabelPeakhold);
            this.groupBox3.Controls.Add(this.LabelMsec);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 283);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(390, 103);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Spectrum Peakhold";
            // 
            // PeakholdDescentSpeedLabel
            // 
            this.PeakholdDescentSpeedLabel.AutoSize = true;
            this.PeakholdDescentSpeedLabel.Location = new System.Drawing.Point(6, 40);
            this.PeakholdDescentSpeedLabel.Name = "PeakholdDescentSpeedLabel";
            this.PeakholdDescentSpeedLabel.Size = new System.Drawing.Size(134, 15);
            this.PeakholdDescentSpeedLabel.TabIndex = 21;
            this.PeakholdDescentSpeedLabel.Text = "Descent Speed (4 - 20)";
            // 
            // PeakholdDescentSpeedTextBox
            // 
            this.PeakholdDescentSpeedTextBox.Location = new System.Drawing.Point(333, 66);
            this.PeakholdDescentSpeedTextBox.Name = "PeakholdDescentSpeedTextBox";
            this.PeakholdDescentSpeedTextBox.Size = new System.Drawing.Size(43, 21);
            this.PeakholdDescentSpeedTextBox.TabIndex = 23;
            this.PeakholdDescentSpeedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PeakholdDescentSpeedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PeakholdDescentSpeedTextBox_KeyDown);
            this.PeakholdDescentSpeedTextBox.Leave += new System.EventHandler(this.PeakholdDescentSpeedTextBox_Leave);
            // 
            // PeakholdCheckBox
            // 
            this.PeakholdCheckBox.AutoSize = true;
            this.PeakholdCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PeakholdCheckBox.Location = new System.Drawing.Point(17, 18);
            this.PeakholdCheckBox.Name = "PeakholdCheckBox";
            this.PeakholdCheckBox.Size = new System.Drawing.Size(65, 19);
            this.PeakholdCheckBox.TabIndex = 18;
            this.PeakholdCheckBox.Text = "Enable";
            this.PeakholdCheckBox.UseVisualStyleBackColor = true;
            // 
            // SSaverCheckBox
            // 
            this.SSaverCheckBox.AutoSize = true;
            this.SSaverCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSaverCheckBox.Location = new System.Drawing.Point(206, 106);
            this.SSaverCheckBox.Name = "SSaverCheckBox";
            this.SSaverCheckBox.Size = new System.Drawing.Size(143, 19);
            this.SSaverCheckBox.TabIndex = 9;
            this.SSaverCheckBox.Text = "Prevent Screen Saver";
            this.SSaverCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChannelLayoutGroup
            // 
            this.ChannelLayoutGroup.Controls.Add(this.FlipGroup);
            this.ChannelLayoutGroup.Controls.Add(this.HorizontalRadio);
            this.ChannelLayoutGroup.Controls.Add(this.VerticalRadio);
            this.ChannelLayoutGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelLayoutGroup.Location = new System.Drawing.Point(6, 44);
            this.ChannelLayoutGroup.Name = "ChannelLayoutGroup";
            this.ChannelLayoutGroup.Size = new System.Drawing.Size(207, 178);
            this.ChannelLayoutGroup.TabIndex = 24;
            this.ChannelLayoutGroup.TabStop = false;
            this.ChannelLayoutGroup.Text = "Spectrum Channel Layout";
            // 
            // FlipGroup
            // 
            this.FlipGroup.Controls.Add(this.NoFlipRadio);
            this.FlipGroup.Controls.Add(this.RightFlipRadio);
            this.FlipGroup.Controls.Add(this.LeftFlipRadio);
            this.FlipGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlipGroup.Location = new System.Drawing.Point(28, 75);
            this.FlipGroup.Name = "FlipGroup";
            this.FlipGroup.Size = new System.Drawing.Size(172, 97);
            this.FlipGroup.TabIndex = 27;
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
            this.NoFlipRadio.TabIndex = 28;
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
            this.RightFlipRadio.TabIndex = 30;
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
            this.LeftFlipRadio.TabIndex = 29;
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
            this.HorizontalRadio.TabIndex = 26;
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
            this.VerticalRadio.TabIndex = 25;
            this.VerticalRadio.TabStop = true;
            this.VerticalRadio.Text = "Vertical";
            this.VerticalRadio.UseVisualStyleBackColor = true;
            // 
            // AlwaysOnTopCheckBox
            // 
            this.AlwaysOnTopCheckBox.AutoSize = true;
            this.AlwaysOnTopCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlwaysOnTopCheckBox.Location = new System.Drawing.Point(206, 81);
            this.AlwaysOnTopCheckBox.Name = "AlwaysOnTopCheckBox";
            this.AlwaysOnTopCheckBox.Size = new System.Drawing.Size(104, 19);
            this.AlwaysOnTopCheckBox.TabIndex = 8;
            this.AlwaysOnTopCheckBox.Text = "Always on Top";
            this.AlwaysOnTopCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.StereoRadio);
            this.groupBox5.Controls.Add(this.MonoRadio);
            this.groupBox5.Controls.Add(this.ChannelLayoutGroup);
            this.groupBox5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(422, 204);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(220, 228);
            this.groupBox5.TabIndex = 32;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Number of Channels (Spectrum)";
            // 
            // StereoRadio
            // 
            this.StereoRadio.AutoSize = true;
            this.StereoRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StereoRadio.Location = new System.Drawing.Point(140, 19);
            this.StereoRadio.Name = "StereoRadio";
            this.StereoRadio.Size = new System.Drawing.Size(74, 19);
            this.StereoRadio.TabIndex = 34;
            this.StereoRadio.Text = "2: Stereo";
            this.StereoRadio.UseVisualStyleBackColor = true;
            // 
            // MonoRadio
            // 
            this.MonoRadio.AutoSize = true;
            this.MonoRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonoRadio.Location = new System.Drawing.Point(19, 20);
            this.MonoRadio.Name = "MonoRadio";
            this.MonoRadio.Size = new System.Drawing.Size(119, 19);
            this.MonoRadio.TabIndex = 33;
            this.MonoRadio.Text = "1: Mono(L+R Mix)";
            this.MonoRadio.UseVisualStyleBackColor = true;
            this.MonoRadio.CheckedChanged += new System.EventHandler(this.MonoRadio_CheckedChanged);
            // 
            // ShowCounterCheckBox
            // 
            this.ShowCounterCheckBox.AutoSize = true;
            this.ShowCounterCheckBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowCounterCheckBox.Location = new System.Drawing.Point(21, 528);
            this.ShowCounterCheckBox.Name = "ShowCounterCheckBox";
            this.ShowCounterCheckBox.Size = new System.Drawing.Size(137, 18);
            this.ShowCounterCheckBox.TabIndex = 49;
            this.ShowCounterCheckBox.TabStop = false;
            this.ShowCounterCheckBox.Text = "Show Counter (debug)";
            this.ShowCounterCheckBox.UseVisualStyleBackColor = true;
            this.ShowCounterCheckBox.Visible = false;
            // 
            // HideFreqCheckBox
            // 
            this.HideFreqCheckBox.AutoSize = true;
            this.HideFreqCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideFreqCheckBox.Location = new System.Drawing.Point(206, 131);
            this.HideFreqCheckBox.Name = "HideFreqCheckBox";
            this.HideFreqCheckBox.Size = new System.Drawing.Size(117, 19);
            this.HideFreqCheckBox.TabIndex = 10;
            this.HideFreqCheckBox.Text = "Hide Freq. Label";
            this.HideFreqCheckBox.UseVisualStyleBackColor = true;
            // 
            // HideTitleCheckBox
            // 
            this.HideTitleCheckBox.AutoSize = true;
            this.HideTitleCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideTitleCheckBox.Location = new System.Drawing.Point(206, 156);
            this.HideTitleCheckBox.Name = "HideTitleCheckBox";
            this.HideTitleCheckBox.Size = new System.Drawing.Size(96, 19);
            this.HideTitleCheckBox.TabIndex = 11;
            this.HideTitleCheckBox.Text = "Hide Titlebar";
            this.HideTitleCheckBox.UseVisualStyleBackColor = true;
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabel1.Location = new System.Drawing.Point(172, 528);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(230, 15);
            this.LinkLabel1.TabIndex = 36;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "https://github.com/osamusg/SpeAnaLED";
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // ExitAppButton
            // 
            this.ExitAppButton.Enabled = false;
            this.ExitAppButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitAppButton.Location = new System.Drawing.Point(308, 153);
            this.ExitAppButton.Name = "ExitAppButton";
            this.ExitAppButton.Size = new System.Drawing.Size(88, 23);
            this.ExitAppButton.TabIndex = 37;
            this.ExitAppButton.Text = "Exit This App.";
            this.ExitAppButton.UseVisualStyleBackColor = true;
            // 
            // DeviceResetButton
            // 
            this.DeviceResetButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceResetButton.Location = new System.Drawing.Point(521, 28);
            this.DeviceResetButton.Name = "DeviceResetButton";
            this.DeviceResetButton.Size = new System.Drawing.Size(107, 23);
            this.DeviceResetButton.TabIndex = 52;
            this.DeviceResetButton.Text = "Reload Device";
            this.DeviceResetButton.UseVisualStyleBackColor = true;
            this.DeviceResetButton.EnabledChanged += new System.EventHandler(this.DeviceResetButton_EnabledChanged);
            this.DeviceResetButton.Click += new System.EventHandler(this.DeviceResetButton_Click);
            // 
            // FrequencyLabel
            // 
            this.FrequencyLabel.AutoSize = true;
            this.FrequencyLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrequencyLabel.Location = new System.Drawing.Point(458, 31);
            this.FrequencyLabel.Name = "FrequencyLabel";
            this.FrequencyLabel.Size = new System.Drawing.Size(59, 15);
            this.FrequencyLabel.TabIndex = 53;
            this.FrequencyLabel.Text = "- - - . - khz";
            // 
            // RelLabel
            // 
            this.RelLabel.AutoSize = true;
            this.RelLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelLabel.Location = new System.Drawing.Point(408, 528);
            this.RelLabel.Name = "RelLabel";
            this.RelLabel.Size = new System.Drawing.Size(29, 15);
            this.RelLabel.TabIndex = 54;
            this.RelLabel.Text = "Rel.";
            // 
            // HideSpectrumWindowCheckBox
            // 
            this.HideSpectrumWindowCheckBox.AutoSize = true;
            this.HideSpectrumWindowCheckBox.Enabled = false;
            this.HideSpectrumWindowCheckBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideSpectrumWindowCheckBox.Location = new System.Drawing.Point(221, 200);
            this.HideSpectrumWindowCheckBox.Name = "HideSpectrumWindowCheckBox";
            this.HideSpectrumWindowCheckBox.Size = new System.Drawing.Size(139, 18);
            this.HideSpectrumWindowCheckBox.TabIndex = 55;
            this.HideSpectrumWindowCheckBox.Text = "Hide Spectrum Window";
            this.HideSpectrumWindowCheckBox.UseVisualStyleBackColor = true;
            // 
            // LevelMeterCheckBox
            // 
            this.LevelMeterCheckBox.AutoSize = true;
            this.LevelMeterCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelMeterCheckBox.Location = new System.Drawing.Point(206, 181);
            this.LevelMeterCheckBox.Name = "LevelMeterCheckBox";
            this.LevelMeterCheckBox.Size = new System.Drawing.Size(88, 19);
            this.LevelMeterCheckBox.TabIndex = 56;
            this.LevelMeterCheckBox.Text = "Level Meter";
            this.LevelMeterCheckBox.UseVisualStyleBackColor = true;
            // 
            // AutoReloadCheckBox
            // 
            this.AutoReloadCheckBox.AutoSize = true;
            this.AutoReloadCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AutoReloadCheckBox.Location = new System.Drawing.Point(535, 57);
            this.AutoReloadCheckBox.Name = "AutoReloadCheckBox";
            this.AutoReloadCheckBox.Size = new System.Drawing.Size(93, 19);
            this.AutoReloadCheckBox.TabIndex = 57;
            this.AutoReloadCheckBox.Text = "Auto Reload";
            this.AutoReloadCheckBox.UseVisualStyleBackColor = true;
            this.AutoReloadCheckBox.CheckedChanged += new System.EventHandler(this.AutoReloadCheckBox_CheckedChanged);
            // 
            // RefreshModeGroupBox
            // 
            this.RefreshModeGroupBox.Controls.Add(this.RefreshFastRadio);
            this.RefreshModeGroupBox.Controls.Add(this.RefreshNormalRadio);
            this.RefreshModeGroupBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshModeGroupBox.Location = new System.Drawing.Point(12, 207);
            this.RefreshModeGroupBox.Name = "RefreshModeGroupBox";
            this.RefreshModeGroupBox.Size = new System.Drawing.Size(185, 49);
            this.RefreshModeGroupBox.TabIndex = 58;
            this.RefreshModeGroupBox.TabStop = false;
            this.RefreshModeGroupBox.Text = "Refresh Mode";
            // 
            // RefreshFastRadio
            // 
            this.RefreshFastRadio.AutoSize = true;
            this.RefreshFastRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshFastRadio.Location = new System.Drawing.Point(89, 20);
            this.RefreshFastRadio.Name = "RefreshFastRadio";
            this.RefreshFastRadio.Size = new System.Drawing.Size(82, 19);
            this.RefreshFastRadio.TabIndex = 1;
            this.RefreshFastRadio.TabStop = true;
            this.RefreshFastRadio.Text = "More Busy";
            this.RefreshFastRadio.UseVisualStyleBackColor = true;
            // 
            // RefreshNormalRadio
            // 
            this.RefreshNormalRadio.AutoSize = true;
            this.RefreshNormalRadio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshNormalRadio.Location = new System.Drawing.Point(17, 20);
            this.RefreshNormalRadio.Name = "RefreshNormalRadio";
            this.RefreshNormalRadio.Size = new System.Drawing.Size(66, 19);
            this.RefreshNormalRadio.TabIndex = 0;
            this.RefreshNormalRadio.TabStop = true;
            this.RefreshNormalRadio.Text = "Normal";
            this.RefreshNormalRadio.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.LevelSensitivityTrackBar);
            this.groupBox4.Controls.Add(this.LevelSensitivityTextBox);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(16, 459);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(386, 56);
            this.groupBox4.TabIndex = 59;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Level Meter Sensitivity (1.0 - 2.0)";
            // 
            // LevelSensitivityTrackBar
            // 
            this.LevelSensitivityTrackBar.AutoSize = false;
            this.LevelSensitivityTrackBar.LargeChange = 2;
            this.LevelSensitivityTrackBar.Location = new System.Drawing.Point(6, 29);
            this.LevelSensitivityTrackBar.Maximum = 20;
            this.LevelSensitivityTrackBar.Minimum = 10;
            this.LevelSensitivityTrackBar.Name = "LevelSensitivityTrackBar";
            this.LevelSensitivityTrackBar.Size = new System.Drawing.Size(318, 21);
            this.LevelSensitivityTrackBar.TabIndex = 15;
            this.LevelSensitivityTrackBar.Value = 18;
            this.LevelSensitivityTrackBar.ValueChanged += new System.EventHandler(this.LevelSensitivityTrackBar_ValueChanged);
            // 
            // LevelSensitivityTextBox
            // 
            this.LevelSensitivityTextBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelSensitivityTextBox.Location = new System.Drawing.Point(330, 26);
            this.LevelSensitivityTextBox.Name = "LevelSensitivityTextBox";
            this.LevelSensitivityTextBox.Size = new System.Drawing.Size(43, 21);
            this.LevelSensitivityTextBox.TabIndex = 16;
            this.LevelSensitivityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LevelSensitivityTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LevelSensitivityTextBox_KeyDown);
            this.LevelSensitivityTextBox.Leave += new System.EventHandler(this.LevelSensitivityTextBox_Leave);
            // 
            // LevelStreamCheckBox
            // 
            this.LevelStreamCheckBox.AutoSize = true;
            this.LevelStreamCheckBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelStreamCheckBox.Location = new System.Drawing.Point(206, 216);
            this.LevelStreamCheckBox.Name = "LevelStreamCheckBox";
            this.LevelStreamCheckBox.Size = new System.Drawing.Size(98, 19);
            this.LevelStreamCheckBox.TabIndex = 60;
            this.LevelStreamCheckBox.Text = "Level Stream";
            this.LevelStreamCheckBox.UseVisualStyleBackColor = true;
            // 
            // LevelStreamPanel
            // 
            this.LevelStreamPanel.Controls.Add(this.CombineStreamRadioButton);
            this.LevelStreamPanel.Controls.Add(this.SeparateStreamRadioButton);
            this.LevelStreamPanel.Location = new System.Drawing.Point(217, 232);
            this.LevelStreamPanel.Name = "LevelStreamPanel";
            this.LevelStreamPanel.Size = new System.Drawing.Size(179, 22);
            this.LevelStreamPanel.TabIndex = 61;
            // 
            // CombineStreamRadioButton
            // 
            this.CombineStreamRadioButton.AutoSize = true;
            this.CombineStreamRadioButton.Font = new System.Drawing.Font("Arial", 8F);
            this.CombineStreamRadioButton.Location = new System.Drawing.Point(78, 1);
            this.CombineStreamRadioButton.Name = "CombineStreamRadioButton";
            this.CombineStreamRadioButton.Size = new System.Drawing.Size(66, 18);
            this.CombineStreamRadioButton.TabIndex = 1;
            this.CombineStreamRadioButton.Text = "Combine";
            this.CombineStreamRadioButton.UseVisualStyleBackColor = true;
            // 
            // SeparateStreamRadioButton
            // 
            this.SeparateStreamRadioButton.AutoSize = true;
            this.SeparateStreamRadioButton.Font = new System.Drawing.Font("Arial", 8F);
            this.SeparateStreamRadioButton.Location = new System.Drawing.Point(4, 1);
            this.SeparateStreamRadioButton.Name = "SeparateStreamRadioButton";
            this.SeparateStreamRadioButton.Size = new System.Drawing.Size(69, 18);
            this.SeparateStreamRadioButton.TabIndex = 0;
            this.SeparateStreamRadioButton.Text = "Separate";
            this.SeparateStreamRadioButton.UseVisualStyleBackColor = true;
            // 
            // StreamColorButton
            // 
            this.StreamColorButton.Font = new System.Drawing.Font("Arial", 8F);
            this.StreamColorButton.Location = new System.Drawing.Point(221, 252);
            this.StreamColorButton.Name = "StreamColorButton";
            this.StreamColorButton.Size = new System.Drawing.Size(65, 19);
            this.StreamColorButton.TabIndex = 62;
            this.StreamColorButton.Text = "Fore Color";
            this.StreamColorButton.UseVisualStyleBackColor = true;
            // 
            // AlfaTextBox
            // 
            this.AlfaTextBox.Font = new System.Drawing.Font("Arial", 8F);
            this.AlfaTextBox.Location = new System.Drawing.Point(365, 252);
            this.AlfaTextBox.Name = "AlfaTextBox";
            this.AlfaTextBox.Size = new System.Drawing.Size(37, 20);
            this.AlfaTextBox.TabIndex = 63;
            this.AlfaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AlfaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AlfaTextBox_KeyDown);
            this.AlfaTextBox.Leave += new System.EventHandler(this.AlfaTextBox_Leave);
            // 
            // AlfaLabel
            // 
            this.AlfaLabel.AutoSize = true;
            this.AlfaLabel.Font = new System.Drawing.Font("Arial", 8F);
            this.AlfaLabel.Location = new System.Drawing.Point(292, 254);
            this.AlfaLabel.Name = "AlfaLabel";
            this.AlfaLabel.Size = new System.Drawing.Size(72, 14);
            this.AlfaLabel.TabIndex = 64;
            this.AlfaLabel.Text = "Alfa (0 - 255)";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 590);
            this.ControlBox = false;
            this.Controls.Add(this.AlfaLabel);
            this.Controls.Add(this.AlfaTextBox);
            this.Controls.Add(this.StreamColorButton);
            this.Controls.Add(this.LevelStreamPanel);
            this.Controls.Add(this.LevelStreamCheckBox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.RefreshModeGroupBox);
            this.Controls.Add(this.AutoReloadCheckBox);
            this.Controls.Add(this.LevelMeterCheckBox);
            this.Controls.Add(this.HideSpectrumWindowCheckBox);
            this.Controls.Add(this.RelLabel);
            this.Controls.Add(this.FrequencyLabel);
            this.Controls.Add(this.DeviceResetButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ExitAppButton);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.HideTitleCheckBox);
            this.Controls.Add(this.HideFreqCheckBox);
            this.Controls.Add(this.ShowCounterCheckBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.AlwaysOnTopCheckBox);
            this.Controls.Add(this.SSaverCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NumberOfBarComboBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.EnumerateButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.devicelist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Config - SpeAnaLED";
            this.DoubleClick += new System.EventHandler(this.Form2_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeakholdDescentSpeedTrackBar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ChannelLayoutGroup.ResumeLayout(false);
            this.ChannelLayoutGroup.PerformLayout();
            this.FlipGroup.ResumeLayout(false);
            this.FlipGroup.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.RefreshModeGroupBox.ResumeLayout(false);
            this.RefreshModeGroupBox.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LevelSensitivityTrackBar)).EndInit();
            this.LevelStreamPanel.ResumeLayout(false);
            this.LevelStreamPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        protected internal System.Windows.Forms.TrackBar PeakholdDescentSpeedTrackBar;
        protected internal System.Windows.Forms.CheckBox SSaverCheckBox;
        protected internal System.Windows.Forms.CheckBox PeakholdCheckBox;
        protected internal System.Windows.Forms.CheckBox AlwaysOnTopCheckBox;
        protected internal System.Windows.Forms.TextBox SensitivityTextBox;
        protected internal System.Windows.Forms.TextBox PeakholdDescentSpeedTextBox;
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
        protected internal System.Windows.Forms.GroupBox ChannelLayoutGroup;
        protected internal System.Windows.Forms.Button DeviceResetButton;
        protected internal System.Windows.Forms.Label FrequencyLabel;
        protected internal System.Windows.Forms.Label RelLabel;
        protected internal System.Windows.Forms.CheckBox HideSpectrumWindowCheckBox;
        protected internal System.Windows.Forms.Button CloseButton;
        protected internal System.Windows.Forms.CheckBox LevelMeterCheckBox;
        protected internal System.Windows.Forms.CheckBox AutoReloadCheckBox;
        private System.Windows.Forms.GroupBox RefreshModeGroupBox;
        protected internal System.Windows.Forms.RadioButton RefreshFastRadio;
        protected internal System.Windows.Forms.RadioButton RefreshNormalRadio;
        private System.Windows.Forms.GroupBox groupBox4;
        protected internal System.Windows.Forms.TrackBar LevelSensitivityTrackBar;
        protected internal System.Windows.Forms.TextBox LevelSensitivityTextBox;
        protected internal System.Windows.Forms.CheckBox LevelStreamCheckBox;
        protected internal System.Windows.Forms.RadioButton CombineStreamRadioButton;
        protected internal System.Windows.Forms.RadioButton SeparateStreamRadioButton;
        protected internal System.Windows.Forms.Button StreamColorButton;
        protected internal System.Windows.Forms.CheckBox RainbowFlipCheckBox;
        protected internal System.Windows.Forms.Panel LevelStreamPanel;
        protected internal System.Windows.Forms.TextBox AlfaTextBox;
        protected internal System.Windows.Forms.Label AlfaLabel;
        protected internal System.Windows.Forms.Label PeakholdDescentSpeedLabel;
    }
}