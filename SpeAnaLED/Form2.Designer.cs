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
            this.CloseButton = new System.Windows.Forms.Button();
            this.SensitivityTrackBar = new System.Windows.Forms.TrackBar();
            this.SensitivityTextBox = new System.Windows.Forms.TextBox();
            this.ClassicRadioButton = new System.Windows.Forms.RadioButton();
            this.PrisumRadioButton = new System.Windows.Forms.RadioButton();
            this.SimpleRadioButton = new System.Windows.Forms.RadioButton();
            this.RainbowRadioButton = new System.Windows.Forms.RadioButton();
            this.SpectrumColorShemeGroupBox = new System.Windows.Forms.GroupBox();
            this.RainbowFlipCheckBox = new System.Windows.Forms.CheckBox();
            this.SpectrumSensitivityGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PeakholdTimeComboBox = new System.Windows.Forms.ComboBox();
            this.LabelPeakhold = new System.Windows.Forms.Label();
            this.LabelMsec = new System.Windows.Forms.Label();
            this.PeakholdDescentSpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.SpectrumPeakholdGroupBox = new System.Windows.Forms.GroupBox();
            this.PeakholdDescentSpeedLabel = new System.Windows.Forms.Label();
            this.PeakholdDescentSpeedTextBox = new System.Windows.Forms.TextBox();
            this.PeakholdCheckBox = new System.Windows.Forms.CheckBox();
            this.SSaverCheckBox = new System.Windows.Forms.CheckBox();
            this.ChannelLayoutGroup = new System.Windows.Forms.GroupBox();
            this.FlipGroup = new System.Windows.Forms.GroupBox();
            this.NoFlipRadioButton = new System.Windows.Forms.RadioButton();
            this.RightFlipRadioButton = new System.Windows.Forms.RadioButton();
            this.LeftFlipRadioButton = new System.Windows.Forms.RadioButton();
            this.HorizontalRadioButton = new System.Windows.Forms.RadioButton();
            this.VerticalRadioButton = new System.Windows.Forms.RadioButton();
            this.AlwaysOnTopCheckBox = new System.Windows.Forms.CheckBox();
            this.NoCGroupBox = new System.Windows.Forms.GroupBox();
            this.StereoRadioButton = new System.Windows.Forms.RadioButton();
            this.MonoRadioButton = new System.Windows.Forms.RadioButton();
            this.ShowCounterCheckBox = new System.Windows.Forms.CheckBox();
            this.HideFreqCheckBox = new System.Windows.Forms.CheckBox();
            this.HideTitleCheckBox = new System.Windows.Forms.CheckBox();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ExitAppButton = new System.Windows.Forms.Button();
            this.DeviceReloadButton = new System.Windows.Forms.Button();
            this.FrequencyLabel = new System.Windows.Forms.Label();
            this.RelLabel = new System.Windows.Forms.Label();
            this.HideSpectrumWindowCheckBox = new System.Windows.Forms.CheckBox();
            this.LevelMeterCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoReloadCheckBox = new System.Windows.Forms.CheckBox();
            this.LevelMeterSensitivityGroupBox = new System.Windows.Forms.GroupBox();
            this.LevelSensitivityTrackBar = new System.Windows.Forms.TrackBar();
            this.LevelSensitivityTextBox = new System.Windows.Forms.TextBox();
            this.LevelStreamCheckBox = new System.Windows.Forms.CheckBox();
            this.LevelStreamPanel = new System.Windows.Forms.Panel();
            this.AlfaTextBox = new System.Windows.Forms.TextBox();
            this.AlfaLabel = new System.Windows.Forms.Label();
            this.StreamColorButton = new System.Windows.Forms.Button();
            this.CombineStreamRadioButton = new System.Windows.Forms.RadioButton();
            this.SeparateStreamRadioButton = new System.Windows.Forms.RadioButton();
            this.NumberOfBarsPanel = new System.Windows.Forms.Panel();
            this.NumberOfBar8RadioButton = new System.Windows.Forms.RadioButton();
            this.NumberOfBar32RadioButton = new System.Windows.Forms.RadioButton();
            this.NumberOfBar4RadioButton = new System.Windows.Forms.RadioButton();
            this.NumberOfBar16RadioButton = new System.Windows.Forms.RadioButton();
            this.NumberOfBarLabel = new System.Windows.Forms.Label();
            this.DefaultDeviceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VerticalFlipCheckBox = new System.Windows.Forms.CheckBox();
            this.RefreshNormalRadio = new System.Windows.Forms.RadioButton();
            this.RefreshFastRadio = new System.Windows.Forms.RadioButton();
            this.RefreshModeGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.SensitivityTrackBar)).BeginInit();
            this.SpectrumColorShemeGroupBox.SuspendLayout();
            this.SpectrumSensitivityGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeakholdDescentSpeedTrackBar)).BeginInit();
            this.SpectrumPeakholdGroupBox.SuspendLayout();
            this.ChannelLayoutGroup.SuspendLayout();
            this.FlipGroup.SuspendLayout();
            this.NoCGroupBox.SuspendLayout();
            this.LevelMeterSensitivityGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LevelSensitivityTrackBar)).BeginInit();
            this.LevelStreamPanel.SuspendLayout();
            this.NumberOfBarsPanel.SuspendLayout();
            this.RefreshModeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(513, 522);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(129, 27);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close This Dialog";
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
            // ClassicRadioButton
            // 
            this.ClassicRadioButton.AutoSize = true;
            this.ClassicRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClassicRadioButton.Location = new System.Drawing.Point(17, 50);
            this.ClassicRadioButton.Name = "ClassicRadioButton";
            this.ClassicRadioButton.Size = new System.Drawing.Size(67, 19);
            this.ClassicRadioButton.TabIndex = 4;
            this.ClassicRadioButton.TabStop = true;
            this.ClassicRadioButton.Text = "Classic";
            this.ClassicRadioButton.UseVisualStyleBackColor = true;
            // 
            // PrisumRadioButton
            // 
            this.PrisumRadioButton.AutoSize = true;
            this.PrisumRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrisumRadioButton.Location = new System.Drawing.Point(17, 24);
            this.PrisumRadioButton.Name = "PrisumRadioButton";
            this.PrisumRadioButton.Size = new System.Drawing.Size(49, 19);
            this.PrisumRadioButton.TabIndex = 3;
            this.PrisumRadioButton.TabStop = true;
            this.PrisumRadioButton.Text = "LED";
            this.PrisumRadioButton.UseVisualStyleBackColor = true;
            // 
            // SimpleRadioButton
            // 
            this.SimpleRadioButton.AutoSize = true;
            this.SimpleRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimpleRadioButton.Location = new System.Drawing.Point(17, 76);
            this.SimpleRadioButton.Name = "SimpleRadioButton";
            this.SimpleRadioButton.Size = new System.Drawing.Size(64, 19);
            this.SimpleRadioButton.TabIndex = 5;
            this.SimpleRadioButton.TabStop = true;
            this.SimpleRadioButton.Text = "Simple";
            this.SimpleRadioButton.UseVisualStyleBackColor = true;
            // 
            // RainbowRadioButton
            // 
            this.RainbowRadioButton.AutoSize = true;
            this.RainbowRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RainbowRadioButton.Location = new System.Drawing.Point(17, 102);
            this.RainbowRadioButton.Name = "RainbowRadioButton";
            this.RainbowRadioButton.Size = new System.Drawing.Size(140, 19);
            this.RainbowRadioButton.TabIndex = 6;
            this.RainbowRadioButton.TabStop = true;
            this.RainbowRadioButton.Text = "Rainbow (Horizontal)";
            this.RainbowRadioButton.UseVisualStyleBackColor = true;
            // 
            // SpectrumColorShemeGroupBox
            // 
            this.SpectrumColorShemeGroupBox.Controls.Add(this.RainbowFlipCheckBox);
            this.SpectrumColorShemeGroupBox.Controls.Add(this.ClassicRadioButton);
            this.SpectrumColorShemeGroupBox.Controls.Add(this.RainbowRadioButton);
            this.SpectrumColorShemeGroupBox.Controls.Add(this.PrisumRadioButton);
            this.SpectrumColorShemeGroupBox.Controls.Add(this.SimpleRadioButton);
            this.SpectrumColorShemeGroupBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpectrumColorShemeGroupBox.Location = new System.Drawing.Point(12, 57);
            this.SpectrumColorShemeGroupBox.Name = "SpectrumColorShemeGroupBox";
            this.SpectrumColorShemeGroupBox.Size = new System.Drawing.Size(185, 148);
            this.SpectrumColorShemeGroupBox.TabIndex = 3;
            this.SpectrumColorShemeGroupBox.TabStop = false;
            this.SpectrumColorShemeGroupBox.Text = "Spectrum Color Scheme";
            // 
            // RainbowFlipCheckBox
            // 
            this.RainbowFlipCheckBox.AutoSize = true;
            this.RainbowFlipCheckBox.Location = new System.Drawing.Point(36, 120);
            this.RainbowFlipCheckBox.Name = "RainbowFlipCheckBox";
            this.RainbowFlipCheckBox.Size = new System.Drawing.Size(79, 19);
            this.RainbowFlipCheckBox.TabIndex = 7;
            this.RainbowFlipCheckBox.Text = "Flip Color";
            this.RainbowFlipCheckBox.UseVisualStyleBackColor = true;
            // 
            // SpectrumSensitivityGroupBox
            // 
            this.SpectrumSensitivityGroupBox.Controls.Add(this.SensitivityTrackBar);
            this.SpectrumSensitivityGroupBox.Controls.Add(this.SensitivityTextBox);
            this.SpectrumSensitivityGroupBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpectrumSensitivityGroupBox.Location = new System.Drawing.Point(15, 394);
            this.SpectrumSensitivityGroupBox.Name = "SpectrumSensitivityGroupBox";
            this.SpectrumSensitivityGroupBox.Size = new System.Drawing.Size(386, 59);
            this.SpectrumSensitivityGroupBox.TabIndex = 14;
            this.SpectrumSensitivityGroupBox.TabStop = false;
            this.SpectrumSensitivityGroupBox.Text = "Spectrum Sensitivity (1.0 - 9.9)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Default Device (WASAPI)";
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
            this.PeakholdTimeComboBox.SelectedIndexChanged += new System.EventHandler(this.PeakholdTimeComboBox_SelectedIndexChanged);
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
            // SpectrumPeakholdGroupBox
            // 
            this.SpectrumPeakholdGroupBox.Controls.Add(this.PeakholdDescentSpeedLabel);
            this.SpectrumPeakholdGroupBox.Controls.Add(this.PeakholdDescentSpeedTextBox);
            this.SpectrumPeakholdGroupBox.Controls.Add(this.PeakholdDescentSpeedTrackBar);
            this.SpectrumPeakholdGroupBox.Controls.Add(this.PeakholdCheckBox);
            this.SpectrumPeakholdGroupBox.Controls.Add(this.PeakholdTimeComboBox);
            this.SpectrumPeakholdGroupBox.Controls.Add(this.LabelPeakhold);
            this.SpectrumPeakholdGroupBox.Controls.Add(this.LabelMsec);
            this.SpectrumPeakholdGroupBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpectrumPeakholdGroupBox.Location = new System.Drawing.Point(12, 283);
            this.SpectrumPeakholdGroupBox.Name = "SpectrumPeakholdGroupBox";
            this.SpectrumPeakholdGroupBox.Size = new System.Drawing.Size(390, 103);
            this.SpectrumPeakholdGroupBox.TabIndex = 17;
            this.SpectrumPeakholdGroupBox.TabStop = false;
            this.SpectrumPeakholdGroupBox.Text = "Spectrum Peakhold";
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
            this.PeakholdCheckBox.CheckedChanged += new System.EventHandler(this.PeakholdCheckBox_CheckedChanged);
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
            this.ChannelLayoutGroup.Controls.Add(this.VerticalFlipCheckBox);
            this.ChannelLayoutGroup.Controls.Add(this.FlipGroup);
            this.ChannelLayoutGroup.Controls.Add(this.HorizontalRadioButton);
            this.ChannelLayoutGroup.Controls.Add(this.VerticalRadioButton);
            this.ChannelLayoutGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelLayoutGroup.Location = new System.Drawing.Point(6, 44);
            this.ChannelLayoutGroup.Name = "ChannelLayoutGroup";
            this.ChannelLayoutGroup.Size = new System.Drawing.Size(207, 188);
            this.ChannelLayoutGroup.TabIndex = 24;
            this.ChannelLayoutGroup.TabStop = false;
            this.ChannelLayoutGroup.Text = "Spectrum Channel Layout";
            // 
            // FlipGroup
            // 
            this.FlipGroup.Controls.Add(this.NoFlipRadioButton);
            this.FlipGroup.Controls.Add(this.RightFlipRadioButton);
            this.FlipGroup.Controls.Add(this.LeftFlipRadioButton);
            this.FlipGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlipGroup.Location = new System.Drawing.Point(28, 85);
            this.FlipGroup.Name = "FlipGroup";
            this.FlipGroup.Size = new System.Drawing.Size(172, 97);
            this.FlipGroup.TabIndex = 27;
            this.FlipGroup.TabStop = false;
            this.FlipGroup.Text = "Horizontal Flip";
            // 
            // NoFlipRadioButton
            // 
            this.NoFlipRadioButton.AutoSize = true;
            this.NoFlipRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoFlipRadioButton.Location = new System.Drawing.Point(18, 23);
            this.NoFlipRadioButton.Name = "NoFlipRadioButton";
            this.NoFlipRadioButton.Size = new System.Drawing.Size(64, 19);
            this.NoFlipRadioButton.TabIndex = 28;
            this.NoFlipRadioButton.TabStop = true;
            this.NoFlipRadioButton.Text = "No Flip";
            this.NoFlipRadioButton.UseVisualStyleBackColor = true;
            // 
            // RightFlipRadioButton
            // 
            this.RightFlipRadioButton.AutoSize = true;
            this.RightFlipRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightFlipRadioButton.Location = new System.Drawing.Point(18, 72);
            this.RightFlipRadioButton.Name = "RightFlipRadioButton";
            this.RightFlipRadioButton.Size = new System.Drawing.Size(113, 19);
            this.RightFlipRadioButton.TabIndex = 30;
            this.RightFlipRadioButton.TabStop = true;
            this.RightFlipRadioButton.Text = "R.ch (Center Hi)";
            this.RightFlipRadioButton.UseVisualStyleBackColor = true;
            // 
            // LeftFlipRadioButton
            // 
            this.LeftFlipRadioButton.AutoSize = true;
            this.LeftFlipRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftFlipRadioButton.Location = new System.Drawing.Point(18, 47);
            this.LeftFlipRadioButton.Name = "LeftFlipRadioButton";
            this.LeftFlipRadioButton.Size = new System.Drawing.Size(122, 19);
            this.LeftFlipRadioButton.TabIndex = 29;
            this.LeftFlipRadioButton.TabStop = true;
            this.LeftFlipRadioButton.Text = "L.ch (Center Low)";
            this.LeftFlipRadioButton.UseVisualStyleBackColor = true;
            // 
            // HorizontalRadioButton
            // 
            this.HorizontalRadioButton.AutoSize = true;
            this.HorizontalRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HorizontalRadioButton.Location = new System.Drawing.Point(13, 65);
            this.HorizontalRadioButton.Name = "HorizontalRadioButton";
            this.HorizontalRadioButton.Size = new System.Drawing.Size(80, 19);
            this.HorizontalRadioButton.TabIndex = 26;
            this.HorizontalRadioButton.TabStop = true;
            this.HorizontalRadioButton.Text = "Horizontal";
            this.HorizontalRadioButton.UseVisualStyleBackColor = true;
            // 
            // VerticalRadioButton
            // 
            this.VerticalRadioButton.AutoSize = true;
            this.VerticalRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerticalRadioButton.Location = new System.Drawing.Point(13, 23);
            this.VerticalRadioButton.Name = "VerticalRadioButton";
            this.VerticalRadioButton.Size = new System.Drawing.Size(64, 19);
            this.VerticalRadioButton.TabIndex = 25;
            this.VerticalRadioButton.TabStop = true;
            this.VerticalRadioButton.Text = "Vertical";
            this.VerticalRadioButton.UseVisualStyleBackColor = true;
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
            // NoCGroupBox
            // 
            this.NoCGroupBox.Controls.Add(this.StereoRadioButton);
            this.NoCGroupBox.Controls.Add(this.MonoRadioButton);
            this.NoCGroupBox.Controls.Add(this.ChannelLayoutGroup);
            this.NoCGroupBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoCGroupBox.Location = new System.Drawing.Point(422, 87);
            this.NoCGroupBox.Name = "NoCGroupBox";
            this.NoCGroupBox.Size = new System.Drawing.Size(220, 238);
            this.NoCGroupBox.TabIndex = 32;
            this.NoCGroupBox.TabStop = false;
            this.NoCGroupBox.Text = "Number of Channels (Spectrum)";
            // 
            // StereoRadioButton
            // 
            this.StereoRadioButton.AutoSize = true;
            this.StereoRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StereoRadioButton.Location = new System.Drawing.Point(140, 19);
            this.StereoRadioButton.Name = "StereoRadioButton";
            this.StereoRadioButton.Size = new System.Drawing.Size(74, 19);
            this.StereoRadioButton.TabIndex = 34;
            this.StereoRadioButton.Text = "2: Stereo";
            this.StereoRadioButton.UseVisualStyleBackColor = true;
            this.StereoRadioButton.CheckedChanged += new System.EventHandler(this.MonoRadioButton_CheckedChanged);
            // 
            // MonoRadioButton
            // 
            this.MonoRadioButton.AutoSize = true;
            this.MonoRadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonoRadioButton.Location = new System.Drawing.Point(19, 20);
            this.MonoRadioButton.Name = "MonoRadioButton";
            this.MonoRadioButton.Size = new System.Drawing.Size(119, 19);
            this.MonoRadioButton.TabIndex = 33;
            this.MonoRadioButton.Text = "1: Mono(L+R Mix)";
            this.MonoRadioButton.UseVisualStyleBackColor = true;
            this.MonoRadioButton.CheckedChanged += new System.EventHandler(this.MonoRadioButton_CheckedChanged);
            // 
            // ShowCounterCheckBox
            // 
            this.ShowCounterCheckBox.AutoSize = true;
            this.ShowCounterCheckBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowCounterCheckBox.Location = new System.Drawing.Point(21, 531);
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
            this.LinkLabel1.Location = new System.Drawing.Point(172, 534);
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
            // DeviceReloadButton
            // 
            this.DeviceReloadButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceReloadButton.Location = new System.Drawing.Point(513, 27);
            this.DeviceReloadButton.Name = "DeviceReloadButton";
            this.DeviceReloadButton.Size = new System.Drawing.Size(129, 23);
            this.DeviceReloadButton.TabIndex = 52;
            this.DeviceReloadButton.Text = "Reload Default Dev.";
            this.DeviceReloadButton.UseVisualStyleBackColor = true;
            this.DeviceReloadButton.Click += new System.EventHandler(this.DeviceReloadButton_Click);
            // 
            // FrequencyLabel
            // 
            this.FrequencyLabel.AutoSize = true;
            this.FrequencyLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrequencyLabel.Location = new System.Drawing.Point(454, 31);
            this.FrequencyLabel.Name = "FrequencyLabel";
            this.FrequencyLabel.Size = new System.Drawing.Size(59, 15);
            this.FrequencyLabel.TabIndex = 53;
            this.FrequencyLabel.Text = "- - - . - khz";
            // 
            // RelLabel
            // 
            this.RelLabel.AutoSize = true;
            this.RelLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelLabel.Location = new System.Drawing.Point(408, 534);
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
            this.AutoReloadCheckBox.Location = new System.Drawing.Point(549, 57);
            this.AutoReloadCheckBox.Name = "AutoReloadCheckBox";
            this.AutoReloadCheckBox.Size = new System.Drawing.Size(93, 19);
            this.AutoReloadCheckBox.TabIndex = 57;
            this.AutoReloadCheckBox.Text = "Auto Reload";
            this.AutoReloadCheckBox.UseVisualStyleBackColor = true;
            // 
            // LevelMeterSensitivityGroupBox
            // 
            this.LevelMeterSensitivityGroupBox.Controls.Add(this.LevelSensitivityTrackBar);
            this.LevelMeterSensitivityGroupBox.Controls.Add(this.LevelSensitivityTextBox);
            this.LevelMeterSensitivityGroupBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelMeterSensitivityGroupBox.Location = new System.Drawing.Point(16, 459);
            this.LevelMeterSensitivityGroupBox.Name = "LevelMeterSensitivityGroupBox";
            this.LevelMeterSensitivityGroupBox.Size = new System.Drawing.Size(386, 56);
            this.LevelMeterSensitivityGroupBox.TabIndex = 59;
            this.LevelMeterSensitivityGroupBox.TabStop = false;
            this.LevelMeterSensitivityGroupBox.Text = "Level Meter Sensitivity (1.0 - 2.0)";
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
            this.LevelStreamCheckBox.Location = new System.Drawing.Point(206, 222);
            this.LevelStreamCheckBox.Name = "LevelStreamCheckBox";
            this.LevelStreamCheckBox.Size = new System.Drawing.Size(98, 19);
            this.LevelStreamCheckBox.TabIndex = 60;
            this.LevelStreamCheckBox.Text = "Level Stream";
            this.LevelStreamCheckBox.UseVisualStyleBackColor = true;
            // 
            // LevelStreamPanel
            // 
            this.LevelStreamPanel.Controls.Add(this.AlfaTextBox);
            this.LevelStreamPanel.Controls.Add(this.AlfaLabel);
            this.LevelStreamPanel.Controls.Add(this.StreamColorButton);
            this.LevelStreamPanel.Controls.Add(this.CombineStreamRadioButton);
            this.LevelStreamPanel.Controls.Add(this.SeparateStreamRadioButton);
            this.LevelStreamPanel.Location = new System.Drawing.Point(217, 238);
            this.LevelStreamPanel.Name = "LevelStreamPanel";
            this.LevelStreamPanel.Size = new System.Drawing.Size(199, 45);
            this.LevelStreamPanel.TabIndex = 61;
            // 
            // AlfaTextBox
            // 
            this.AlfaTextBox.Font = new System.Drawing.Font("Arial", 8F);
            this.AlfaTextBox.Location = new System.Drawing.Point(148, 20);
            this.AlfaTextBox.Name = "AlfaTextBox";
            this.AlfaTextBox.Size = new System.Drawing.Size(37, 20);
            this.AlfaTextBox.TabIndex = 64;
            this.AlfaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AlfaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AlfaTextBox_KeyDown);
            this.AlfaTextBox.Leave += new System.EventHandler(this.AlfaTextBox_Leave);
            // 
            // AlfaLabel
            // 
            this.AlfaLabel.AutoSize = true;
            this.AlfaLabel.Font = new System.Drawing.Font("Arial", 8F);
            this.AlfaLabel.Location = new System.Drawing.Point(75, 22);
            this.AlfaLabel.Name = "AlfaLabel";
            this.AlfaLabel.Size = new System.Drawing.Size(72, 14);
            this.AlfaLabel.TabIndex = 65;
            this.AlfaLabel.Text = "Alfa (0 - 255)";
            // 
            // StreamColorButton
            // 
            this.StreamColorButton.Font = new System.Drawing.Font("Arial", 8F);
            this.StreamColorButton.Location = new System.Drawing.Point(4, 20);
            this.StreamColorButton.Name = "StreamColorButton";
            this.StreamColorButton.Size = new System.Drawing.Size(65, 19);
            this.StreamColorButton.TabIndex = 65;
            this.StreamColorButton.Text = "Fore Color";
            this.StreamColorButton.UseVisualStyleBackColor = true;
            // 
            // CombineStreamRadioButton
            // 
            this.CombineStreamRadioButton.AutoSize = true;
            this.CombineStreamRadioButton.Font = new System.Drawing.Font("Arial", 8F);
            this.CombineStreamRadioButton.Location = new System.Drawing.Point(4, 3);
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
            this.SeparateStreamRadioButton.Location = new System.Drawing.Point(76, 3);
            this.SeparateStreamRadioButton.Name = "SeparateStreamRadioButton";
            this.SeparateStreamRadioButton.Size = new System.Drawing.Size(69, 18);
            this.SeparateStreamRadioButton.TabIndex = 0;
            this.SeparateStreamRadioButton.Text = "Separate";
            this.SeparateStreamRadioButton.UseVisualStyleBackColor = true;
            // 
            // NumberOfBarsPanel
            // 
            this.NumberOfBarsPanel.Controls.Add(this.NumberOfBar8RadioButton);
            this.NumberOfBarsPanel.Controls.Add(this.NumberOfBar32RadioButton);
            this.NumberOfBarsPanel.Controls.Add(this.NumberOfBar4RadioButton);
            this.NumberOfBarsPanel.Controls.Add(this.NumberOfBar16RadioButton);
            this.NumberOfBarsPanel.Controls.Add(this.NumberOfBarLabel);
            this.NumberOfBarsPanel.Location = new System.Drawing.Point(204, 55);
            this.NumberOfBarsPanel.Name = "NumberOfBarsPanel";
            this.NumberOfBarsPanel.Size = new System.Drawing.Size(287, 21);
            this.NumberOfBarsPanel.TabIndex = 62;
            // 
            // NumberOfBar8RadioButton
            // 
            this.NumberOfBar8RadioButton.AutoSize = true;
            this.NumberOfBar8RadioButton.Location = new System.Drawing.Point(216, 2);
            this.NumberOfBar8RadioButton.Name = "NumberOfBar8RadioButton";
            this.NumberOfBar8RadioButton.Size = new System.Drawing.Size(29, 16);
            this.NumberOfBar8RadioButton.TabIndex = 20;
            this.NumberOfBar8RadioButton.TabStop = true;
            this.NumberOfBar8RadioButton.Text = "8";
            this.NumberOfBar8RadioButton.UseVisualStyleBackColor = true;
            this.NumberOfBar8RadioButton.CheckedChanged += new System.EventHandler(this.NumberOfBarRadioButtonCheckedChanged);
            // 
            // NumberOfBar32RadioButton
            // 
            this.NumberOfBar32RadioButton.AutoSize = true;
            this.NumberOfBar32RadioButton.Location = new System.Drawing.Point(138, 2);
            this.NumberOfBar32RadioButton.Name = "NumberOfBar32RadioButton";
            this.NumberOfBar32RadioButton.Size = new System.Drawing.Size(35, 16);
            this.NumberOfBar32RadioButton.TabIndex = 22;
            this.NumberOfBar32RadioButton.TabStop = true;
            this.NumberOfBar32RadioButton.Text = "32";
            this.NumberOfBar32RadioButton.UseVisualStyleBackColor = true;
            // 
            // NumberOfBar4RadioButton
            // 
            this.NumberOfBar4RadioButton.AutoSize = true;
            this.NumberOfBar4RadioButton.Location = new System.Drawing.Point(251, 2);
            this.NumberOfBar4RadioButton.Name = "NumberOfBar4RadioButton";
            this.NumberOfBar4RadioButton.Size = new System.Drawing.Size(29, 16);
            this.NumberOfBar4RadioButton.TabIndex = 21;
            this.NumberOfBar4RadioButton.TabStop = true;
            this.NumberOfBar4RadioButton.Text = "4";
            this.NumberOfBar4RadioButton.UseVisualStyleBackColor = true;
            this.NumberOfBar4RadioButton.Visible = false;
            this.NumberOfBar4RadioButton.CheckedChanged += new System.EventHandler(this.NumberOfBarRadioButtonCheckedChanged);
            // 
            // NumberOfBar16RadioButton
            // 
            this.NumberOfBar16RadioButton.AutoSize = true;
            this.NumberOfBar16RadioButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NumberOfBar16RadioButton.Location = new System.Drawing.Point(175, 2);
            this.NumberOfBar16RadioButton.Name = "NumberOfBar16RadioButton";
            this.NumberOfBar16RadioButton.Size = new System.Drawing.Size(35, 16);
            this.NumberOfBar16RadioButton.TabIndex = 19;
            this.NumberOfBar16RadioButton.TabStop = true;
            this.NumberOfBar16RadioButton.Text = "16";
            this.NumberOfBar16RadioButton.UseVisualStyleBackColor = true;
            this.NumberOfBar16RadioButton.CheckedChanged += new System.EventHandler(this.NumberOfBarRadioButtonCheckedChanged);
            // 
            // NumberOfBarLabel
            // 
            this.NumberOfBarLabel.AutoSize = true;
            this.NumberOfBarLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfBarLabel.Location = new System.Drawing.Point(0, 2);
            this.NumberOfBarLabel.Name = "NumberOfBarLabel";
            this.NumberOfBarLabel.Size = new System.Drawing.Size(132, 15);
            this.NumberOfBarLabel.TabIndex = 18;
            this.NumberOfBarLabel.Text = "Number of Bandwidth :";
            // 
            // DefaultDeviceName
            // 
            this.DefaultDeviceName.BackColor = System.Drawing.SystemColors.Control;
            this.DefaultDeviceName.Location = new System.Drawing.Point(12, 28);
            this.DefaultDeviceName.Name = "DefaultDeviceName";
            this.DefaultDeviceName.ReadOnly = true;
            this.DefaultDeviceName.Size = new System.Drawing.Size(440, 21);
            this.DefaultDeviceName.TabIndex = 63;
            this.DefaultDeviceName.Text = "WASPI device not found.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(453, 394);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 12);
            this.label2.TabIndex = 64;
            this.label2.Text = "label2(debug)";
            this.label2.Visible = false;
            // 
            // VerticalFlipCheckBox
            // 
            this.VerticalFlipCheckBox.AutoSize = true;
            this.VerticalFlipCheckBox.Enabled = false;
            this.VerticalFlipCheckBox.Location = new System.Drawing.Point(46, 43);
            this.VerticalFlipCheckBox.Name = "VerticalFlipCheckBox";
            this.VerticalFlipCheckBox.Size = new System.Drawing.Size(116, 19);
            this.VerticalFlipCheckBox.TabIndex = 28;
            this.VerticalFlipCheckBox.Text = "R.ch Vertical Flip";
            this.VerticalFlipCheckBox.UseVisualStyleBackColor = true;
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
            // RefreshModeGroupBox
            // 
            this.RefreshModeGroupBox.Controls.Add(this.RefreshFastRadio);
            this.RefreshModeGroupBox.Controls.Add(this.RefreshNormalRadio);
            this.RefreshModeGroupBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshModeGroupBox.Location = new System.Drawing.Point(12, 211);
            this.RefreshModeGroupBox.Name = "RefreshModeGroupBox";
            this.RefreshModeGroupBox.Size = new System.Drawing.Size(185, 49);
            this.RefreshModeGroupBox.TabIndex = 58;
            this.RefreshModeGroupBox.TabStop = false;
            this.RefreshModeGroupBox.Text = "Refresh Mode";
            this.RefreshModeGroupBox.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 599);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DefaultDeviceName);
            this.Controls.Add(this.LevelStreamPanel);
            this.Controls.Add(this.LevelStreamCheckBox);
            this.Controls.Add(this.LevelMeterSensitivityGroupBox);
            this.Controls.Add(this.RefreshModeGroupBox);
            this.Controls.Add(this.AutoReloadCheckBox);
            this.Controls.Add(this.LevelMeterCheckBox);
            this.Controls.Add(this.HideSpectrumWindowCheckBox);
            this.Controls.Add(this.RelLabel);
            this.Controls.Add(this.FrequencyLabel);
            this.Controls.Add(this.DeviceReloadButton);
            this.Controls.Add(this.SpectrumPeakholdGroupBox);
            this.Controls.Add(this.ExitAppButton);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.HideTitleCheckBox);
            this.Controls.Add(this.HideFreqCheckBox);
            this.Controls.Add(this.ShowCounterCheckBox);
            this.Controls.Add(this.NoCGroupBox);
            this.Controls.Add(this.AlwaysOnTopCheckBox);
            this.Controls.Add(this.SSaverCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SpectrumSensitivityGroupBox);
            this.Controls.Add(this.SpectrumColorShemeGroupBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.NumberOfBarsPanel);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
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
            this.SpectrumColorShemeGroupBox.ResumeLayout(false);
            this.SpectrumColorShemeGroupBox.PerformLayout();
            this.SpectrumSensitivityGroupBox.ResumeLayout(false);
            this.SpectrumSensitivityGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PeakholdDescentSpeedTrackBar)).EndInit();
            this.SpectrumPeakholdGroupBox.ResumeLayout(false);
            this.SpectrumPeakholdGroupBox.PerformLayout();
            this.ChannelLayoutGroup.ResumeLayout(false);
            this.ChannelLayoutGroup.PerformLayout();
            this.FlipGroup.ResumeLayout(false);
            this.FlipGroup.PerformLayout();
            this.NoCGroupBox.ResumeLayout(false);
            this.NoCGroupBox.PerformLayout();
            this.LevelMeterSensitivityGroupBox.ResumeLayout(false);
            this.LevelMeterSensitivityGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LevelSensitivityTrackBar)).EndInit();
            this.LevelStreamPanel.ResumeLayout(false);
            this.LevelStreamPanel.PerformLayout();
            this.NumberOfBarsPanel.ResumeLayout(false);
            this.NumberOfBarsPanel.PerformLayout();
            this.RefreshModeGroupBox.ResumeLayout(false);
            this.RefreshModeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        protected internal System.Windows.Forms.RadioButton NoFlipRadioButton;
        protected internal System.Windows.Forms.RadioButton RightFlipRadioButton;
        protected internal System.Windows.Forms.RadioButton LeftFlipRadioButton;
        protected internal System.Windows.Forms.TrackBar SensitivityTrackBar;
        protected internal System.Windows.Forms.RadioButton ClassicRadioButton;
        protected internal System.Windows.Forms.RadioButton PrisumRadioButton;
        protected internal System.Windows.Forms.RadioButton SimpleRadioButton;
        protected internal System.Windows.Forms.RadioButton RainbowRadioButton;
        protected internal System.Windows.Forms.ComboBox PeakholdTimeComboBox;
        protected internal System.Windows.Forms.TrackBar PeakholdDescentSpeedTrackBar;
        protected internal System.Windows.Forms.CheckBox SSaverCheckBox;
        protected internal System.Windows.Forms.CheckBox PeakholdCheckBox;
        protected internal System.Windows.Forms.CheckBox AlwaysOnTopCheckBox;
        protected internal System.Windows.Forms.TextBox SensitivityTextBox;
        protected internal System.Windows.Forms.TextBox PeakholdDescentSpeedTextBox;
        protected internal System.Windows.Forms.RadioButton HorizontalRadioButton;
        protected internal System.Windows.Forms.RadioButton VerticalRadioButton;
        protected internal System.Windows.Forms.RadioButton StereoRadioButton;
        protected internal System.Windows.Forms.RadioButton MonoRadioButton;
        protected internal System.Windows.Forms.CheckBox ShowCounterCheckBox;
        protected internal System.Windows.Forms.GroupBox FlipGroup;
        protected internal System.Windows.Forms.Label LabelPeakhold;
        protected internal System.Windows.Forms.Label LabelMsec;
        protected internal System.Windows.Forms.CheckBox HideFreqCheckBox;
        protected internal System.Windows.Forms.LinkLabel LinkLabel1;
        protected internal System.Windows.Forms.CheckBox HideTitleCheckBox;
        protected internal System.Windows.Forms.Button ExitAppButton;
        protected internal System.Windows.Forms.GroupBox ChannelLayoutGroup;
        protected internal System.Windows.Forms.Button DeviceReloadButton;
        protected internal System.Windows.Forms.Label FrequencyLabel;
        protected internal System.Windows.Forms.Label RelLabel;
        protected internal System.Windows.Forms.CheckBox HideSpectrumWindowCheckBox;
        protected internal System.Windows.Forms.Button CloseButton;
        protected internal System.Windows.Forms.CheckBox LevelMeterCheckBox;
        protected internal System.Windows.Forms.CheckBox AutoReloadCheckBox;
        protected internal System.Windows.Forms.TrackBar LevelSensitivityTrackBar;
        protected internal System.Windows.Forms.TextBox LevelSensitivityTextBox;
        protected internal System.Windows.Forms.CheckBox LevelStreamCheckBox;
        protected internal System.Windows.Forms.RadioButton CombineStreamRadioButton;
        protected internal System.Windows.Forms.RadioButton SeparateStreamRadioButton;
        protected internal System.Windows.Forms.CheckBox RainbowFlipCheckBox;
        protected internal System.Windows.Forms.Panel LevelStreamPanel;
        protected internal System.Windows.Forms.Label PeakholdDescentSpeedLabel;
        protected internal System.Windows.Forms.TextBox AlfaTextBox;
        protected internal System.Windows.Forms.Label AlfaLabel;
        protected internal System.Windows.Forms.Button StreamColorButton;
        protected internal System.Windows.Forms.GroupBox SpectrumColorShemeGroupBox;
        protected internal System.Windows.Forms.GroupBox SpectrumPeakholdGroupBox;
        protected internal System.Windows.Forms.GroupBox SpectrumSensitivityGroupBox;
        protected internal System.Windows.Forms.GroupBox NoCGroupBox;
        protected internal System.Windows.Forms.GroupBox LevelMeterSensitivityGroupBox;
        protected internal System.Windows.Forms.Label NumberOfBarLabel;
        protected internal System.Windows.Forms.RadioButton NumberOfBar16RadioButton;
        protected internal System.Windows.Forms.RadioButton NumberOfBar8RadioButton;
        protected internal System.Windows.Forms.Panel NumberOfBarsPanel;
        protected internal System.Windows.Forms.RadioButton NumberOfBar4RadioButton;
        protected internal System.Windows.Forms.RadioButton NumberOfBar32RadioButton;
        protected internal System.Windows.Forms.TextBox DefaultDeviceName;
        protected internal System.Windows.Forms.Label label2;
        protected internal System.Windows.Forms.CheckBox VerticalFlipCheckBox;
        protected internal System.Windows.Forms.RadioButton RefreshNormalRadio;
        protected internal System.Windows.Forms.RadioButton RefreshFastRadio;
        private System.Windows.Forms.GroupBox RefreshModeGroupBox;
    }
}