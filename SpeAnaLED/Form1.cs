﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using ConfigFile;


namespace SpeAnaLED
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0X80000000,
        }

        //const int WS_BORDER = 0x00800000;

        private readonly Analyzer analyzer;
        private readonly Form2 form2 = null;
        private int form1Top;                           // for load config
        private int form1Left;                          // for load config
        private int form1Width;                         // for load config to not call SizeChanged Event
        private int form1Height;                        // for load config to not call SizeChanged Event
        private bool isMaximized = false;               // for load config
        private string devices;                         // for load config. conf can't store arrays length unknown
        public int numberOfBar;
        private int channel;
        private readonly Bitmap[] canvas = new Bitmap[2];
        private int canvasWidth;
        private Color[] colors;
        private float[] positions;
        private Pen myPen;
        private readonly Pen bgPen = new Pen(Color.White);  // not constant for possibility of change in the future
        private LinearGradientBrush brush;
        private int endPointX = 0;                      // default gradient direction, from upward to downward
        private int endPointY;                          // variable depending on size of PictureBox
        private readonly Label[] freqLabel_Left = new Label[maxNumberOfBar];
        private readonly Label[] freqLabel_Right = new Label[maxNumberOfBar];
        private float baseLabelFontSize = 8f;           // not constant for possibility of change in the future
        private int barLeftPadding;
        private int counterCycle;
        private int[] peakValue;
        private int peakCounter = 0;
        private int peakHoldTimeMsec;
        private int peakHoldDescentCycle = 10;  // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)
        private float sensitivity;
        private float sensitivityRatio;
        private int displayOffCounter;
        private RotateFlipType flipLeft = RotateFlipType.RotateNoneFlipNone;
        private RotateFlipType flipRight = RotateFlipType.RotateNoneFlipNone;
        private bool inInit = false;
        private bool inLayout = false;
        private Point mousePoint = new Point(0, 0);
        public static int deviceNumber;
        public static bool UNICODE;

        private int titleHeight;
        private int borderSize;
        private int labelPadding;
        private int leftPadding;
        private int topPadding;
        private int bottomPadding;
        private int verticalSpacing;
        private float spectrumWidthScale;
        //private float spectrumHeightScale;

        // constants
        private const int maxNumberOfBar = 16;
        private const float penWidth = (float)30;
        private const int baseLabelFontWidth = 37;
        private const int baseLabelFontHeight = 13;
        private const int barSpacing = 10;
        private const int baseSpectrumWidth = 650;
        private const int baseSpectrumHeight = 129;
        private const int cycleMultiplyer = 50;

        // color settings
        //
        // based on hvianna's color settings from audioMotion-analyzer (https://github.com/hvianna/audioMotion-analyzer)
        //
        private bool classicChecked, prisumChecked, simpleChecked, rainbowChecked;
        private readonly Color[] classicColors =
            {  Color.FromArgb(255,0,0),     //  0 100 50  red
               Color.FromArgb(255,255,0),   // 60 100 50  yellow
               Color.FromArgb(0,128,0),};   //120 100 25  green
        private readonly float[] classicPositions = { 0.0f, 0.3f, 1.0f };

        private readonly Color[] prisumColors =
            {   Color.FromArgb(255,0,0),    //   0  red
                Color.FromArgb(255,255,0),  //  60  yellow
                Color.FromArgb(0,255,0),    // 120  lime
                Color.FromArgb(0,255,255),  // 180  cyan
                Color.FromArgb(0,0,255),};  // 240  blue
        private readonly float[][] prisumPositions = new float[17][];        // maxNumberOfBar+1 jagg array for easy to see

        private readonly Color[] simpleColors = { Color.LightSkyBlue, Color.LightSkyBlue };
        private readonly float[] simplePositions = { 0.0f, 1.0f };

        public Form1()
        {
            InitializeComponent();

            inInit = true;
            form2 = new Form2 { Owner = this };

            string configFileName = @".\" + ProductName + @".conf";
            try
            {
                LoadConfigParams();
            }
            catch
            {
                MessageBox.Show("Opps! Config file seems something wrong...\r\n" +
                    "Delete file and use default parameters.",
                    "Config file error - " + ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                System.IO.File.Delete(configFileName);
                LoadConfigParams();
            }

            // before analizer birth...
            string[] deviceItems = devices.Split(',');
            for (int i = 0; i < deviceItems.Length; i++) form2.devicelist.Items.Add(deviceItems[i].TrimStart());
            form2.devicelist.SelectedIndex = 0;

            // after param load, make a Analyzer instance.
            analyzer = new Analyzer(form2.devicelist, form2.EnumerateButton, form2.DeviceResetButton, form2.NumberOfBarComboBox, form2.MonoRadio, form2.FrequencyLabel);

            if (devices == null || devices == string.Empty)
            {
                analyzer.inInit = true;
                form2.devicelist.Items.Clear();
                form2.devicelist.Items.Add("Please Enumerate Devices");
                form2.devicelist.SelectedIndex = 0;
                form2.devicelist.Enabled = false;
                form2.DeviceResetButton.Enabled = false;
                analyzer.inInit = false;
            }
            else
            {
                analyzer.Enable = true;
                // analyzer.DisplayEnable = true;       // do nothing in DisplayEnable
            }

            // Event handler for modeless option form (subscribe)
            form2.SensitivityTrackBar.ValueChanged += Form2_SensitivityTrackBar_ValueChanged;
            form2.PeakholdDescentSpeedTrackBar.ValueChanged += Form2_PeakholdDescentSpeedTrackBar_ValueChanged;
            form2.ClassicRadio.CheckedChanged += Form2_ClassicRadio_CheckChanged;
            form2.PrisumRadio.CheckedChanged += Form2_PrisumRadio_CheckChanged;
            form2.SimpleRadio.CheckedChanged += Form2_SimpleRadio_CheckChanged;
            form2.RainbowRadio.CheckedChanged += Form2_RainbowRadio_CheckChanged;
            form2.NumberOfBarComboBox.SelectedIndexChanged += Form2_NumberOfBarComboBox_SelectedItemChanged;
            form2.PeakholdTimeComboBox.SelectedIndexChanged += Form2_PeakholdTimeComboBox_SelectedItemChanged;
            form2.SSaverCheckBox.CheckedChanged += Form2_SSaverCheckboxCheckedChanged;
            form2.PeakholdCheckBox.CheckedChanged += Form2_PeakholdCheckboxCheckedChanged;
            form2.AlwaysOnTopCheckBox.CheckedChanged += Form2_AlwaysOnTopCheckboxCheckChanged;
            form2.VerticalRadio.CheckedChanged += Form2_V_H_RadioCheckChanged;      // V/H dual use
            form2.HorizontalRadio.CheckedChanged += Form2_V_H_RadioCheckChanged;    // V/H dual use
            form2.ShowCounterCheckBox.CheckedChanged += Form2_ShowCounterCheckChanged;
            form2.RightFlipRadio.CheckedChanged += Form2_FlipSideRadioCheckChanged; // L/R dual use
            form2.LeftFlipRadio.CheckedChanged += Form2_FlipSideRadioCheckChanged;  // L/R dual use
            form2.NoFlipRadio.CheckedChanged += Form2_FlipSideRadioCheckChanged;
            form2.HideFreqCheckBox.CheckedChanged += Form2_HideFreqCheckBoxCheckChanged;
            form2.HideTitleCheckBox.CheckedChanged += Form2_HideTitleCheckBoxCheckChanged;
            form2.ExitAppButton.Click += Form2_ExitAppButtonClicked;
            form2.ClearSpectrum += ClearSpectrum;

            // Other Event handler (subscribe)
            analyzer.SpectrumChanged += Analyzer_ReceiveSpectrumData;
            analyzer.NumberOfChannelChanged += Analyzer_NumberOfChannelsChanged;
            Application.ApplicationExit += Application_ApplicationExit;

            // defaults calculated by internal numbers
            this.Text = ProductName;
            channel = analyzer._channel;
            spectrumWidthScale = Spectrum1.Width / (float)baseSpectrumWidth;        // depens on number of bars
            //spectrumHeightScale = Spectrum1.Height / (float)baseSpectrumHeght;    // haven't used this one
            canvasWidth = barLeftPadding + numberOfBar * ((int)penWidth + barSpacing) - (barLeftPadding - barSpacing);  // Calculate size per number of bars to enlarge
            borderSize = (this.Width - this.ClientSize.Width) / 2;
            titleHeight = this.Height - this.ClientSize.Height - borderSize * 2;
            peakValue = new int[maxNumberOfBar * channel];
            endPointY = baseSpectrumHeight;
            bgPen = new Pen(Color.FromArgb(29, 29, 29), penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };

            // default hold time. I don't know why "* cycleMultiplyer * (number..." is necessary. This is due to actual measurements.
            counterCycle = (int)(peakHoldTimeMsec / analyzer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar * channel / 16.0));

            //for (int i = 0; i < channel; i++) canvas[i] = new Bitmap(baseSpectrumWidth, baseSpectrumHeight);
            for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i] = new Label();
            for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Right[i] = new Label();
            
            // form2 setting
            form2.NumberOfBarComboBox.SelectedIndex = form2.NumberOfBarComboBox.Items.IndexOf(numberOfBar.ToString());
            form2.PeakholdTimeComboBox.SelectedIndex = form2.PeakholdTimeComboBox.Items.IndexOf(peakHoldTimeMsec.ToString());
            form2.SensitivityTextBox.Text = (form2.SensitivityTrackBar.Value / 10f).ToString("0.0");
            form2.PeakholdDescentSpeedTextBox.Text = form2.PeakholdDescentSpeedTrackBar.Value.ToString();
            if (!form2.PeakholdCheckBox.Checked) form2.LabelPeakhold.Enabled = form2.LabelMsec.Enabled = form2.PeakholdTimeComboBox.Enabled = false;
            if (form2.HideTitleCheckBox.Checked)
            {
                titleHeight = 0;
                borderSize = 0;
                this.FormBorderStyle = FormBorderStyle.None;
                form2.ExitAppButton.Enabled = true;
            }
            if (form2.VerticalRadio.Checked) form2.FlipGroup.Enabled = false;
            if (channel < 2) form2.ChannelLayoutGroup.Enabled = false;

            // from form2
            sensitivity = form2.SensitivityTrackBar.Value / 10f;
            sensitivityRatio = (float)(0xff / baseSpectrumHeight) * (10f - sensitivity);
            TopMost = form2.AlwaysOnTopCheckBox.Checked;
            if (form2.ShowCounterCheckBox.Checked)
                LabelCycle.Visible = true;
            else
                LabelCycle.Visible = false;
            
            if ((form2.PrisumRadio.Checked = prisumChecked) == true)
            {
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];
                endPointX = 0;
                endPointY = canvas[0].Height;
            }
            else if ((form2.ClassicRadio.Checked = classicChecked) == true)
            {
                colors = classicColors;
                positions = classicPositions;
                endPointX = 0;
                endPointY = canvas[0].Height;
            }
            else if ((form2.SimpleRadio.Checked = simpleChecked) == true)
            {
                colors = simpleColors;
                positions = simplePositions;
                endPointX = 0;
                endPointY = canvas[0].Height;
            }
            else
            {
                form2.RainbowRadio.Checked = rainbowChecked;
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];               // need for color position adjust
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;    // Horizontal
                endPointY = 0;
            }
            // set Gradient color pen
            if (!form2.RainbowRadio.Checked)
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                    { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
            else
                brush = new LinearGradientBrush(new Point(endPointX, endPointY), new Point(0, 0), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                    { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
            myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };

            if (form2.LeftFlipRadio.Checked)
            {
                flipLeft = RotateFlipType.RotateNoneFlipX;
                flipRight = RotateFlipType.RotateNoneFlipNone;
            }
            else if (form2.RightFlipRadio.Checked)
            {
                flipLeft = RotateFlipType.RotateNoneFlipNone;
                flipRight = RotateFlipType.RotateNoneFlipX;
            }
            else
            {
                flipLeft = RotateFlipType.RotateNoneFlipNone;
                flipRight = RotateFlipType.RotateNoneFlipNone;
            }

            inInit = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inInit = true;
            inLayout= true;

            //if (form2.HideFreqCheckBox.Checked) Form2_HideFreqCheckBoxCheckChanged(sender, EventArgs.Empty);

            // Now, Set main form layout
            this.Top = form1Top;
            this.Left = form1Left;
            this.Width = form1Width;        // this calls sizeChanged event (and SetSpectrumLayout)
            this.Height = form1Height;      // this calls sizeChanged event (and SetSpectrumLayout)
            this.WindowState = isMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
            //form2.Top = form2Top;
            //form2.Left = form2Left;

            // Then, Set Spectrum PictureBox size and location from main form size
            SetSpectrumLayout(this.Width, this.Height);
            ClearSpectrum(this, EventArgs.Empty);       // draw background image

            inLayout = false;
            inInit = false;
        }

        public static int DeviceNumber() { return deviceNumber; }

        private void SetSpectrumLayout(int formW, int formH)
        {
            // void, but the objective is to determine size of Spectrum and label location from size of form.
            // All sizes of Spectrum should be determined only here.
            // Labels are also drawn by calling LocateFrequencyLabel from here.
            // Be sure to calculate the size of the Spectrum from the size of the form.
            // (Do not calculate size of form from size of Spectrum.)
            // Size of form is not directly changed. Otherwise, SizeChange would be called again.
            // Every time set both Width and Height to be sure.
            // 
            // This function is also called from SizeChanged and From V_H_Changed.
            // Difference of form's VH 'form' was finished calcurations in V_H_Change function.
            // (The reason is, that function need the 'form' before the change, and it need to know
            // there has been a change.)
            //
            // Factors that influence process of label drawing are,
            // AA. label drawing required or not (In case of V layout, only bottom(R ch.) labels needed.
            // BB. Consider Flip (left, right and none)
            //
            // The arguments formH amd formV are disposable, so don't need to copy local variables.

            inLayout = true;    // prevent double executions from SizeChanged

            if (!form2.HideFreqCheckBox.Checked)    // cases requiring Labels
            {
                if (form2.HorizontalRadio.Checked)              // H-layout: need Labels
                {
                    Spectrum1.Width = Spectrum2.Width = (formW - borderSize * 2 - leftPadding * 2) / channel; // Spectrum.Width makes no difference with or without Labels
                    Spectrum1.Height = Spectrum2.Height = formH - borderSize * 2 - titleHeight - topPadding - labelPadding - baseLabelFontHeight - bottomPadding;
                    Spectrum1.Top = Spectrum2.Top = topPadding;          
                    Spectrum1.Left = leftPadding;                         
                    Spectrum2.Left = Spectrum1.Right;

                    // only H-layout requires L Ch. labels
                    if (channel < 2) LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    else LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: form2.LeftFlipRadio.Checked);
                }
                else                                            // V-layout: need Labels
                {
                    Spectrum1.Width = Spectrum2.Width = formW - borderSize * 2 - leftPadding * 2;       // Spectrum.Width makes no difference with or without Labels
                    Spectrum1.Height = Spectrum2.Height = (formH - borderSize * 2 - titleHeight - topPadding - verticalSpacing * (channel - 1) - labelPadding - baseLabelFontHeight - bottomPadding) / channel;
                    Spectrum1.Top = topPadding;
                    Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                    Spectrum1.Left = Spectrum2.Left = leftPadding;

                    if (channel < 2) LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;        // stereo V layout, not required left label
                }

                // in case of V-layout, no requires L Ch. labels.
                // Common process for V and H that requires labels for R Ch.  Consider Flip.
                LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: form2.RightFlipRadio.Checked);     // but only case in stereo is visible
            }
            else        // cases not requiring Labels
            {
                if (form2.HorizontalRadio.Checked)      // H-layout: not need Labels
                {
                    Spectrum1.Width = Spectrum2.Width = (formW - borderSize * 2 - leftPadding * 2) / channel; // Spectrum.Width makes no difference with or without Labels
                    Spectrum1.Height = Spectrum2.Height = formH - borderSize * 2 - titleHeight - topPadding;
                    Spectrum1.Top = Spectrum2.Top = topPadding;
                    Spectrum1.Left = leftPadding;
                    Spectrum2.Left = Spectrum1.Right + 1;
                }
                else                                    // V-layout: not need Labels
                {
                    Spectrum1.Width = Spectrum2.Width = formW - borderSize * 2 - leftPadding * 2;       // Spectrum.Width makes no difference with or without Labels
                    Spectrum1.Height = Spectrum2.Height = (formH - borderSize * 2 - titleHeight - topPadding - verticalSpacing * (channel - 1)) / channel; // mono:0, steereo:1
                    Spectrum1.Top = topPadding;
                    Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                    Spectrum1.Left = Spectrum2.Left = leftPadding;
                }

                // Common process for V and H that does not requires Labels.
                // Label.Visible=false set here
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Right[i].Visible = false;
            }

            if (channel < 2)        // case in Mono
            {
                Spectrum2.Visible = false;
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Right[i].Visible = false;
            }
            else                    // case in stereo
            {
                Spectrum2.Visible = true;
            }

            inLayout = false;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (!inLayout) SetSpectrumLayout(this.Width, this.Height);
            // Try to keep just this amap. Event receiving is its only role.
            // This will be called from V/H layout change also because size of form will change by V/H change.
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            if (form2.Visible == false)
                form2.Show(this);
        }

        private void Spectrum1_DoubleClick(object sender, EventArgs e)
        {
            if (form2.Visible == false)
                form2.Show(this);
        }

        private void Spectrum2_DoubleClick(object sender, EventArgs e)
        {
            if (form2.Visible == false)
                form2.Show(this);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && form2.Visible == false)
                form2.Show(this);
        }

        private void Spectrum1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && form2.HideTitleCheckBox.Checked)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void Spectrum1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && form2.Visible == false)
                form2.Show(this);
            if (e.Button == MouseButtons.Left)
                mousePoint = new Point(e.X, e.Y);
        }

        private void Spectrum2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && form2.HideTitleCheckBox.Checked)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void Spectrum2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && form2.Visible == false)
                form2.Show(this);
            if (e.Button == MouseButtons.Left)
                mousePoint = new Point(e.X, e.Y);
        }
        
        private void Form2_SensitivityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                sensitivity = form2.SensitivityTrackBar.Value / 10f;
                sensitivityRatio = (float)(0xff / baseSpectrumHeight) * (10f - sensitivity);
            }
        }

        private void Form2_PeakholdDescentSpeedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            peakHoldDescentCycle = 20 * numberOfBar / channel / form2.PeakholdDescentSpeedTrackBar.Value;       // Inverse the value so that the direction of
            // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)            //  speed and value increase/decrease match.
        }

        private void Form2_NumberOfBarComboBox_SelectedItemChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                numberOfBar = Convert.ToInt16(form2.NumberOfBarComboBox.SelectedItem);
                counterCycle = (int)(peakHoldTimeMsec / analyzer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar / 16.0));    // Hold time is affected by the number of bands
                peakHoldDescentCycle = 20 * numberOfBar / channel / form2.PeakholdDescentSpeedTrackBar.Value;   // Inverse the value so that the direction of
                // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)        //  speed and value increase/decrease match.

                if (form2.RainbowRadio.Checked)     // need for color position adjust (Horizontal LED(Prisum) color)
                {
                    endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;    // right-end of image by number of bar
                    endPointY = 0;
                    brush = new LinearGradientBrush(new Point(endPointX, endPointY), new Point(0, 0), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                        { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                    myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
                }

                canvasWidth = barLeftPadding + numberOfBar * ((int)penWidth + barSpacing) - (barLeftPadding - barSpacing);  // to stretch, calculate canvas size by number of bar
                if (!inInit) ClearSpectrum(sender, EventArgs.Empty);
                spectrumWidthScale = (float)Spectrum1.Width / baseSpectrumWidth * (maxNumberOfBar / numberOfBar);
                if (!form2.HideFreqCheckBox.Checked)
                {
                    if (!form2.VerticalRadio.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, form2.LeftFlipRadio.Checked);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, form2.RightFlipRadio.Checked);
                }
            }
        }

        private void Form2_PeakholdTimeComboBox_SelectedItemChanged(object sender, EventArgs e)
        {
            peakHoldTimeMsec = Convert.ToInt16(form2.PeakholdTimeComboBox.SelectedItem);
            counterCycle = (int)(peakHoldTimeMsec / analyzer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar / 16.0));
        }

        private void Form2_ClassicRadio_CheckChanged(object sender, EventArgs e)
        {
            if (form2.ClassicRadio.Checked && !inInit)
            {
                colors = classicColors;
                positions = classicPositions;
                endPointX = 0;
                endPointY = canvas[0].Height;
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                    { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            }
        }

        private void Form2_PrisumRadio_CheckChanged(object sender, EventArgs e)
        {
            if (form2.PrisumRadio.Checked && !inInit)
            {
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];
                endPointX = 0;
                endPointY = canvas[0].Height;
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                    { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            }
        }

        private void Form2_SimpleRadio_CheckChanged(object sender, EventArgs e)
        {
            if (form2.SimpleRadio.Checked && !inInit)
            {
                colors = simpleColors;
                positions = simplePositions;
                endPointX = 0;
                endPointY = canvas[0].Height;
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                    { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            }
        }

        private void Form2_RainbowRadio_CheckChanged(object sender, EventArgs e)
        {
            if (form2.RainbowRadio.Checked && !inInit)
            {
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];       // need for color position adjustment
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;        // Horizontal
                endPointY = 0;
                brush = new LinearGradientBrush(new Point(endPointX, endPointY), new Point(0, 0), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                    { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            }
        }

        private void Form2_SSaverCheckboxCheckedChanged(object sender, EventArgs e)
        {
            // Do nothing here, flag is checked by timer.
        }

        private void Form2_PeakholdCheckboxCheckedChanged(object sender, EventArgs e)
        {
            if (form2.PeakholdCheckBox.Checked)
                form2.PeakholdTimeComboBox.Enabled = form2.LabelPeakhold.Enabled = form2.LabelMsec.Enabled = true;
            else
                form2.PeakholdTimeComboBox.Enabled = form2.LabelPeakhold.Enabled = form2.LabelMsec.Enabled = false;
        }

        private void Form2_AlwaysOnTopCheckboxCheckChanged(object sender, EventArgs e)
        {
            if (inInit == false) TopMost = !TopMost;
        }

        private void Form2_V_H_RadioCheckChanged(object sender, EventArgs e)            // V/H dual use
        {
            // Only resizing of the H and V forms and drawing of the freq. labels are handled here.
            // This will not call SetSpectrumLayout. Because it's hard to align that sizes of Spectrum will not change.
            // Changing layout cause form's Width and Height changes (but try not to call SizeChanged function)
            // Do not change size of Spectrums and form location here.
            // So don't need to call SetLayout here.
            // No difference in actual form size by Labels exist or not (Height of Spectrum must be already changed)
            // Do not call SetSpectrumLayout, so draw label here

            inLayout = true;

            if (form2.HorizontalRadio.Checked)  // change to H Layout
            {
                this.Width = (borderSize + leftPadding + Spectrum1.Width) * 2;          // -1 is for overlap on left and right 
                if (form2.HideFreqCheckBox.Checked)     // Height "calculation" is different by labels exist or not, no difference in actual form size
                {
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height;// + bottomPadding;
                }
                else
                {
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height + labelPadding + baseLabelFontHeight + bottomPadding;
                }
                // only Spectrum2(R Ch.) moves, do not change their size
                Spectrum2.Top = Spectrum1.Top;
                Spectrum2.Left = Spectrum1.Right;

                // only H layout has L Ch.'s label. To set flip state.
                if (form2.LeftFlipRadio.Checked)
                {
                    LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: true);
                    flipLeft = RotateFlipType.RotateNoneFlipX;
                    flipRight = RotateFlipType.RotateNoneFlipNone;
                }
                else if (form2.RightFlipRadio.Checked)
                {
                    LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    flipLeft = RotateFlipType.RotateNoneFlipNone;
                    flipRight = RotateFlipType.RotateNoneFlipX;
                }
                else
                {
                    LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    flipLeft = RotateFlipType.RotateNoneFlipNone;
                    flipRight = RotateFlipType.RotateNoneFlipNone;
                }

                // In Stereo, only R Ch. labels are moved.
                LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: form2.RightFlipRadio.Checked);

                form2.FlipGroup.Enabled = true;

                // shift Setting dialog location
                if (form2.Top < this.Bottom &&
                    form2.Right > this.Left &&
                    form2.Left < this.Right &&
                    this.Bottom < Screen.FromControl(this).Bounds.Height - 50) form2.Top = this.Bottom + 30;
            }
            else        // change to V Layout
            {
                this.Width = (borderSize + leftPadding) * 2 + Spectrum1.Width;
                if (form2.HideFreqCheckBox.Checked)     // Spectrum.Height calculation is different by labels exist or not, no difference in actual form size
                    this.Height = (borderSize) * 2 + Spectrum1.Height * channel + titleHeight + topPadding + verticalSpacing * (channel - 1);// + bottomPadding;
                else                                    // V-Layout required Label
                    this.Height = (borderSize + Spectrum1.Height) * 2 + titleHeight + topPadding + verticalSpacing * (channel - 1) + labelPadding + /*freqLabel_Left[0].Height */ baseLabelFontHeight + bottomPadding;

                // Only Spectrum2(R Ch.) moves, do not change their size
                Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                Spectrum2.Left = Spectrum1.Left;

                // V-Layout has no flip
                //form2.NoFlipRadio.Checked = true;       // L & R Labels will be also set.

                // no L Ch. label in V layout
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);

                // re-draw R Ch. flip to return to flip state.
                flipLeft = RotateFlipType.RotateNoneFlipNone;
                flipRight = RotateFlipType.RotateNoneFlipNone;

                form2.FlipGroup.Enabled = false;

                // shift Setting dialog location
                if (form2.Top < this.Bottom &&
                    form2.Right > this.Left &&
                    //form2.Left < this.Right &&
                    this.Bottom < Screen.FromControl(this).Bounds.Height - 50) form2.Top = this.Bottom + 30;
            }

            inLayout = false;
        }

        private void Form2_FlipSideRadioCheckChanged(object sender, EventArgs e)                // L/R dual use
        {
            if (!inInit)
            {
                if (form2.NoFlipRadio.Checked)
                {
                    flipLeft = RotateFlipType.RotateNoneFlipNone;
                    flipRight = RotateFlipType.RotateNoneFlipNone;

                    if (!form2.VerticalRadio.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);
                }
                else if (form2.LeftFlipRadio.Checked)
                {
                    flipLeft = RotateFlipType.RotateNoneFlipX;
                    flipRight = RotateFlipType.RotateNoneFlipNone;
                    if (!form2.VerticalRadio.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: true);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);
                }
                else
                {
                    flipLeft = RotateFlipType.RotateNoneFlipNone;
                    flipRight = RotateFlipType.RotateNoneFlipX;
                    if (!form2.VerticalRadio.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: true);
                }

            }
        }

        private void Form2_HideTitleCheckBoxCheckChanged(object sender, EventArgs e)
        {
            inLayout = true;
            if (form2.HideTitleCheckBox.Checked == true)
            {
                borderSize = 0;
                titleHeight = 0;
                this.FormBorderStyle = FormBorderStyle.None;
                SetSpectrumLayout(this.Width, this.Height);
                //this.Left +=  2;
                form2.ExitAppButton.Enabled = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                borderSize = (this.Width - this.ClientSize.Width) / 2;
                titleHeight = this.Height - this.ClientSize.Height - borderSize * 2;
                SetSpectrumLayout(this.Width, this.Height);
                form2.ExitAppButton.Enabled = false;
            }
            inLayout = false;
        }

        private void Form2_ShowCounterCheckChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                if (form2.ShowCounterCheckBox.Checked)
                    LabelCycle.Visible = true;
                else
                    LabelCycle.Visible = false;
            }
        }

        private void Form2_ExitAppButtonClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_HideFreqCheckBoxCheckChanged(object sender, EventArgs e)
        {
            // Try to keep just this amap. Receiving event is its only role
            SetSpectrumLayout(this.Width, this.Height);
        }

        private void Analyzer_ReceiveSpectrumData(object sender, EventArgs e)
        {
            if (inInit) { return; }

            int bounds;
            int isLeft = 0; // for interreave
            int dash, dashBG;
            dash = (int)(penWidth / 10);
            dashBG = (int)(penWidth / 10);

            var g = new Graphics[2];
            for (int i = 0; i < channel; i++) g[i] = Graphics.FromImage(canvas[i]);

            for (int i = 0; i < numberOfBar * channel; i++)     // _spectumdata receiving from analizer is max16*2=32 byte L,R,L,R,...
            {
                if (channel > 1) isLeft = i % 2;                // even: right=0, odd: left=1 (I don't know why), mono is always 0

                var posX = barLeftPadding + (i - isLeft) / channel * (penWidth + barSpacing);       // horizontal position
                var powY = (int)(analyzer._spectrumdata[i] / sensitivityRatio);                     // calculate drawing vertical length
                g[isLeft].DrawLine(bgPen, posX, 0, posX, canvas[0].Height);                         // first, draw BG from top to bottom
                powY = ((powY - dash) / (dash + dashBG) + 1) * (dash + dashBG);                     // align LEDs
                if (powY > 6)                                                                       // _spectrumdata 0x00 is powY = 6
                    g[isLeft].DrawLine(myPen, posX, canvas[0].Height, posX, canvas[0].Height - powY);   // from bottom to top direction

                // Peak Hold
                if (form2.PeakholdCheckBox.Checked)
                {
                    if (peakValue[i] <= powY && powY != 0)
                    {
                        peakValue[i] = powY;        // update peak but not required drawing
                    }
                    // down here, when powY is below peak, draw peak
                    else if (peakCounter > counterCycle * 0.75      // decide whether to descend peak. 0.75 is to descend from cycle of 3/4
                        && peakCounter % peakHoldDescentCycle == 0)                                 // speed of descend
                    {
                        for (int j = 0; j < peakValue.Length; j++) peakValue[j] -= (dash + dashBG); // let Peak descend 1 level
                        if (peakValue[i] < powY) peakValue[i] = powY;                               // When it descends too much, update
                        /*if (peakValue[i] < powY)
                        {
                            peakValue[i] = powY;                                                    // When it descends too much, update
                        }*/
                        else if (powY == 0 && peakValue[i] > 0)
                        { // not reached?
                            for (int j = 0; j < peakValue.Length; j++) peakValue[j] -= (dash + dashBG);
                        }
                    }

                    bounds = ((peakValue[i] - dash) / (dash + dashBG) + 1) * (dash + dashBG); // calculate border line ((int)((x-3)/6)+1)*6 same as powY
                    if (bounds > 6) //  entity of peak drawing. _spectrum 0x00 is  powY=6
                        g[isLeft].DrawLine(myPen, posX, canvas[0].Height - bounds, posX, canvas[0].Height - bounds - dash);   // draw only 1 scale from bounds
                }
                peakCounter++;

                LabelCycle.Text = peakCounter.ToString("0000") + " / " + counterCycle.ToString();

                if (peakCounter >= counterCycle)    // if peakhold=false, counter is used screen saver preventing, so add the counter
                {
                    peakCounter = 0;                // reset when the specified number of rounds
                    for (int j = 0; j < numberOfBar * channel; j++) peakValue[j] = (int)(analyzer._spectrumdata[j] / sensitivityRatio);     // reset peak also
                    displayOffCounter++;
                }
                if (displayOffCounter > 5)          // counterCycle(2000) * 25milSec. * 5 = 250Sec. = 4.17 min.
                {
                    if (form2.SSaverCheckBox.Checked) { SetThreadExecutionState(
                            EXECUTION_STATE.ES_CONTINUOUS |
                            EXECUTION_STATE.ES_DISPLAY_REQUIRED |
                            EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                    }
                    displayOffCounter = 0;
                }
            }

            for (int i = 0; i < channel; i++) g[i].Dispose();

            canvas[0].RotateFlip(flipRight);    // Right (I don't know why.)
            canvas[1].RotateFlip(flipLeft);     // Left, May be there is no canvas[1] in "MONO"

            if (channel > 1)                    // not mono
            {
                Spectrum1.Image = canvas[1];    // even: right=canvas0, odd: left=canvas1 Why?
                Spectrum2.Image = canvas[0];
            }
            else                                // mono
                Spectrum1.Image = canvas[0];    // mono is always to canvas0
        }

        private void Analyzer_NumberOfChannelsChanged(object sender, EventArgs e)
        {
            inInit = true;
            inLayout= true;
            
            channel = analyzer._channel;
            peakValue = new int[maxNumberOfBar * channel];
            counterCycle = (int)(peakHoldTimeMsec / analyzer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar * channel / 16.0));
            peakHoldDescentCycle = 20 * numberOfBar / channel / form2.PeakholdDescentSpeedTrackBar.Value;       // Inverse the value so that the direction of
            for (int i = 0; i < channel; i++) canvas[i] = new Bitmap(Spectrum1.Width, Spectrum1.Height);        //  speed and value increase/decrease match.
            for (int i = 0; i < analyzer._spectrumdata.Count; i++) analyzer._spectrumdata[i] = 0x00;

            if (channel < 2)                        // Case in change to Mono
            {
                Spectrum2.Visible = false;
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Right[i].Visible = false;

                flipLeft = RotateFlipType.RotateNoneFlipNone;       // no flip in mono
                flipRight = RotateFlipType.RotateNoneFlipNone;
                LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                form2.ChannelLayoutGroup.Enabled = false;
            }
            else                                    // cases in change to Stereo 
            {
                form2.ChannelLayoutGroup.Enabled = true;
            }

            if (!form2.HideFreqCheckBox.Checked)    // cases in requiring labels
            {
                if (form2.HorizontalRadio.Checked)  // H-layout: requiring labels
                {
                    this.Width = (borderSize + leftPadding) * 2 + Spectrum1.Width * channel;
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height + labelPadding + baseLabelFontHeight + bottomPadding;

                    if (channel > 1)
                    {
                        if (form2.LeftFlipRadio.Checked)
                        {
                            flipLeft = RotateFlipType.RotateNoneFlipX;
                            flipRight = RotateFlipType.RotateNoneFlipNone;
                        }
                        else if (form2.RightFlipRadio.Checked)
                        {
                            flipLeft = RotateFlipType.RotateNoneFlipNone;
                            flipRight = RotateFlipType.RotateNoneFlipX;
                        }
                        else
                        {
                            flipLeft = RotateFlipType.RotateNoneFlipNone;
                            flipRight = RotateFlipType.RotateNoneFlipNone;
                        }
                }
                }
                else                                // V-layout: requiring labels
                {
                    this.Width = (borderSize + leftPadding) * 2 + Spectrum1.Width;
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height * channel + verticalSpacing * (channel - 1) + labelPadding + baseLabelFontHeight + bottomPadding;
                }
            }
            else                                    // cases in not requiring labels
            {
                if (form2.HorizontalRadio.Checked)  // H-layout: not requiring labels
                {
                    this.Width = (borderSize + leftPadding) * 2 + Spectrum1.Width * channel;
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height;
                    
                    if (form2.LeftFlipRadio.Checked)
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipX;
                        flipRight = RotateFlipType.RotateNoneFlipNone;
                    }
                    else if (form2.RightFlipRadio.Checked)
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipNone;
                        flipRight = RotateFlipType.RotateNoneFlipX;
                    }
                    else
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipNone;
                        flipRight = RotateFlipType.RotateNoneFlipNone;
                    }

                }
                else                                // V-layout: not requiring Labels
                {
                    this.Width = (borderSize + leftPadding) * 2 + Spectrum1.Width;
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height * channel + verticalSpacing * (channel - 1);
                    flipLeft = RotateFlipType.RotateNoneFlipNone;
                    flipRight = RotateFlipType.RotateNoneFlipNone;
                }
            }

            inLayout = false;

            SetSpectrumLayout(this.Width, this.Height);
            ClearSpectrum(this, EventArgs.Empty);

            inInit = false;
        }

        private void ClearSpectrum(object sender, EventArgs e)
        {
            for (int i = 0; i < canvas.Length; i++) canvas[i] = new Bitmap(canvasWidth, baseSpectrumHeight);    // when numberOfBar changes required by new number of bar

            var g = Graphics.FromImage(canvas[0]);
            for (int j = 0; j < numberOfBar; j++)
            {
                var posX = barLeftPadding + j * (barSpacing + penWidth);
                g.DrawLine(bgPen, posX, canvas[0].Height, posX, 0);
            }

            g.Dispose();
            Spectrum1.Image = canvas[0];
            Spectrum2.Image = canvas[0];
            Spectrum1.Update();
            Spectrum2.Update();

            if (form2.RainbowRadio.Checked)
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;
        }

        private void LocateFrequencyLabel(PictureBox spectrum, Label[] freqLabel, bool isFlip)
        {
            SuspendLayout();

            spectrumWidthScale = (float)Spectrum1.Width / baseSpectrumWidth * (maxNumberOfBar / numberOfBar);
            //spectrumHeightScale = (float)Spectrum1.Height / baseSpectrumHeight;
            float labelFontSize = (float)Math.Round(baseLabelFontSize * spectrumWidthScale, 1);
            labelFontSize = labelFontSize < 11f ? labelFontSize : 11f;
            labelFontSize = labelFontSize > 6f ? labelFontSize : 6f;

            for (int i = 0; i < maxNumberOfBar; i++)
            {
                string labelText;
                
                int freqvalues = (int)(Math.Round(Math.Pow(2, i * 10.0 / (numberOfBar - 1) + 4.29), 0));    // 4.29=Round(Log(20000hz, 2) - (in increments of)10, 2);
                if (freqvalues > 10000)
                    labelText = (freqvalues / 1000).ToString("0") + "khz";
                else if (freqvalues > 1000)
                    labelText = (freqvalues / 100 * 100 / 1000f).ToString("0.0") + "khz";
                else
                    labelText = (freqvalues / 10 * 10).ToString("0") + "hz";    // round -1

                int j = isFlip ? numberOfBar - 1 - i : i;
                
                if (i < numberOfBar)        // Only Bar exists
                {
                    freqLabel[i].Width = baseLabelFontWidth;        // I don't know why this need. (in first time only)
                    freqLabel[i].Height = baseLabelFontHeight;
                    freqLabel[i].AutoSize = true;
                    freqLabel[i].Font = new Font("Arial", labelFontSize, FontStyle.Bold);
                    freqLabel[j].Text = labelText;
                    int labelPos = (int)(spectrum.Left + (float)(25 + i * 40) / (float)(10 + numberOfBar * 40) * spectrum.Width) - freqLabel[i].Width / 2;
                    freqLabel[i].Left = labelPos;
                    freqLabel[i].Name = "freqlabel" + (j + 1).ToString();
                    freqLabel[i].Top = spectrum.Bottom + labelPadding * (numberOfBar / maxNumberOfBar);
                    freqLabel[i].TextAlign = ContentAlignment.TopCenter;
                    freqLabel[i].Visible = true;
                    freqLabel[i].ForeColor = Color.LightGray;
                    freqLabel[i].BackColor = Color.Transparent;
                }
                else    // Bar not exists
                {
                    freqLabel[i].Text = String.Empty;
                    freqLabel[i].Visible = false;
                }
            }
            if ((!isFlip && freqLabel[numberOfBar - 2].Right > freqLabel[numberOfBar - 1].Left ||
                isFlip && freqLabel[1].Left < freqLabel[0].Right) && freqLabel[numberOfBar - 1].Left != 0)
            {
                int j = isFlip ? -1 : 0;
                for (int i = 1; i < maxNumberOfBar; i += 2) freqLabel[i + j].Top += freqLabel[0].Height;
                if (!isFlip) freqLabel[numberOfBar - 1].Left -= freqLabel[numberOfBar - 1].Width / 10;
                else freqLabel[0].Left += freqLabel[0].Width / 8;
            }
            else
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel[i].Top += freqLabel[0].Height / 2;
            
            this.Controls.AddRange(freqLabel);
            ResumeLayout(false);
        }

        private void LoadConfigParams()
        {
            //config reader
            var configFileName = @".\" + ProductName + @".conf";

            var confReader = new ConfigReader();
            try
            {
                confReader = new ConfigReader(configFileName);
            }
            catch
            {
                MessageBox.Show("No Config file was found.\r\n" +
                    "Use default Parameters.\r\n" +
                    "Please enumerate devices from \"Setting\" dialog.",
                    "Config file not found - " + ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            numberOfBar = confReader.GetValue("numberOfBar", 16);
            deviceNumber = confReader.GetValue("deviceNumber", -1);
            devices = confReader.GetValue("devices", string.Empty);
            form2.MonoRadio.Checked = confReader.GetValue("mono", false);
            form2.StereoRadio.Checked = confReader.GetValue("stereo", true);

            leftPadding = confReader.GetValue("horizontalPadding", 0);
            topPadding = confReader.GetValue("verticalPadding", 0);
            verticalSpacing = confReader.GetValue("horizontalSpacing", 8);
            barLeftPadding = confReader.GetValue("barLeftPadding", 25);
            bottomPadding = confReader.GetValue("bottompadding", 16);
            labelPadding = confReader.GetValue("labelPadding", 4);

            peakHoldTimeMsec = confReader.GetValue("peakHoldTimeMsec", 500);
            form2.SensitivityTrackBar.Value = confReader.GetValue("sensitivity", 80);
            form2.PeakholdDescentSpeedTrackBar.Value = confReader.GetValue("peakholdDescentSpeed", 10);
            form2.PeakholdCheckBox.Checked = confReader.GetValue("peakhold", true);
            form2.AlwaysOnTopCheckBox.Checked = confReader.GetValue("alwaysOnTop", false);
            form2.SSaverCheckBox.Checked = confReader.GetValue("preventSSaver", true);
            form2.HorizontalRadio.Checked = confReader.GetValue("horizontalLayout", true);
            form2.VerticalRadio.Checked = confReader.GetValue("verticalLayout", false);
            form2.NoFlipRadio.Checked = confReader.GetValue("flipNone", true);
            form2.RightFlipRadio.Checked = confReader.GetValue("flipRight", false);
            form2.LeftFlipRadio.Checked = confReader.GetValue("flipLeft", false);
            form2.HideFreqCheckBox.Checked = confReader.GetValue("hideFreq", false);
            form2.HideTitleCheckBox.Checked = confReader.GetValue("hideTitle", false);

            //penWidth = confReader.GetValue("penWidth",30f);
            //barSpacing = confReader.GetValue("barSpacing", 10);
            baseLabelFontSize = confReader.GetValue("baseLabelFontSize", 8f);
            labelPadding = confReader.GetValue("labelPadding", 6);

            if ((prisumChecked = confReader.GetValue("LED", true)) == false)
                if ((classicChecked = confReader.GetValue("classic", false)) == false)
                    if ((simpleChecked = confReader.GetValue("simple", false)) == false)
                        rainbowChecked = confReader.GetValue("rainbow", false);

            string[] ARGB = confReader.GetValue("classicColors", "255,255,0,0, 255,255,255,0, 255,0,128,0").Split(',');
            int pos = 0;
            for (int i = 0; i < ARGB.Length / 4; i++)
            {
                classicColors[i] = Color.FromArgb(int.Parse(ARGB[pos]), int.Parse(ARGB[pos + 1]), int.Parse(ARGB[pos + 2]), int.Parse(ARGB[pos + 3]));
                pos += 4;
            }
            ARGB = confReader.GetValue("prisumColors", "255,255,0,0, 255,255,255,0, 255,0,255,0, 255,0,255,255, 255,0,0,255").Split(',');
            pos = 0;
            for (int i = 0; i < ARGB.Length / 4; i++)
            {
                prisumColors[i] = Color.FromArgb(int.Parse(ARGB[pos]), int.Parse(ARGB[pos + 1]), int.Parse(ARGB[pos + 2]), int.Parse(ARGB[pos + 3]));
                pos += 4;
            }
            ARGB = confReader.GetValue("simpleColors", "255,135,206,250, 255,135,206,250").Split(',');
            pos = 0;
            for (int i = 0; i < ARGB.Length / 4; i++)
            {
                simpleColors[i] = Color.FromArgb(int.Parse(ARGB[pos]), int.Parse(ARGB[pos + 1]), int.Parse(ARGB[pos + 2]), int.Parse(ARGB[pos + 3]));
                pos += 4;
            }
            ARGB = confReader.GetValue("bgPenColor", "255,29,29,29").Split(',');
            bgPen.Color = Color.FromArgb(int.Parse(ARGB[0]), int.Parse(ARGB[1]), int.Parse(ARGB[2]), int.Parse(ARGB[3]));

            int[] bars = { 1, 2, 4, 8, 16 };
            string[][] param = new string[maxNumberOfBar + 1][];
            param[1] = confReader.GetValue("prisumPositions_1", "0.0, 0.40, 0.5, 0.55, 1.0").Split(',');      // red,yellow,lime,cyan,blue
            param[2] = confReader.GetValue("prisumPositions_2", "0.0, 0.35, 0.5, 0.55, 1.0").Split(',');
            param[4] = confReader.GetValue("prisumPositions_4", "0.0, 0.35, 0.5, 0.55, 1.0").Split(',');
            param[8] = confReader.GetValue("prisumPositions_8", "0.0, 0.37, 0.5, 0.55, 1.0").Split(',');
            param[16] = confReader.GetValue("prisumPositions_16", "0.0, 0.36, 0.5, 0.55, 1.0").Split(',');
            foreach (int bar in bars)                             // 1,2,4,8,16
            {
                prisumPositions[bar] = new float[param[bar].Length];
                for (int j = 0; j < prisumPositions[bar].Length; j++)           // 0,1,2,3,4
                {
                    prisumPositions[bar][j] = new float();
                    prisumPositions[bar][j] = float.Parse(param[bar][j]);
                }
            }

            pos = 0;
            foreach (string s in confReader.GetValue("classicPosition", "0.00, 0.30, 1.00").Split(','))
            {
                classicPositions[pos] = new float();
                classicPositions[pos] = float.Parse(s);
                pos++;
            }
            pos = 0;
            foreach (string s in confReader.GetValue("simplePosition", "0.00, 1.00").Split(','))
            {
                simplePositions[pos] = new float();
                simplePositions[pos] = float.Parse(s);
                pos++;
            }

            form1Width = confReader.GetValue("form1Width", 640);            // When Vertical. Otherwise, resize automatically later
            form1Height = confReader.GetValue("form1Height", 192);
            form1Top = confReader.GetValue("form1Top", 130);
            form1Left = confReader.GetValue("form1Left", 130);
            form2.Top = confReader.GetValue("form2Top", form1Top + form1Height + topPadding);
            form2.Left = confReader.GetValue("form2Left", form1Left);
            isMaximized = confReader.GetValue("maximized", false);

            UNICODE = confReader.GetValue("unicode", true);                 // add "unicode=false" in conf file cause codepage change to ANSI
        }

        private bool SaveConfigParams()
        {
            var confWriter = new ConfigWriter();
            confWriter.AddValue("numberOfBar", numberOfBar);
            confWriter.AddValue("deviceNumber", analyzer._devicenumber);

            confWriter.AddValue("form1Top", this.Top);
            confWriter.AddValue("form1Left", this.Left);
            confWriter.AddValue("form1Width", this.Width);
            confWriter.AddValue("form1Height", this.Height);
            confWriter.AddValue("form2Top", form2.Top);
            confWriter.AddValue("form2Left", form2.Left);
            confWriter.AddValue("maximized", this.WindowState == FormWindowState.Maximized);

            confWriter.AddValue("horizontalPadding", leftPadding);
            confWriter.AddValue("verticalPadding", topPadding);
            confWriter.AddValue("horizontalSpacing", verticalSpacing);
            confWriter.AddValue("barLeftPadding", barLeftPadding);
            confWriter.AddValue("bottomPadding", bottomPadding);
            confWriter.AddValue("labelPadding", labelPadding);

            confWriter.AddValue("peakHoldTimeMsec", peakHoldTimeMsec);
            confWriter.AddValue("sensitivity", form2.SensitivityTrackBar.Value);
            confWriter.AddValue("peakDescentSpeed", form2.PeakholdDescentSpeedTrackBar.Value);

            confWriter.AddValue("peakhold", form2.PeakholdCheckBox.Checked);
            confWriter.AddValue("alwaysOnTop", form2.AlwaysOnTopCheckBox.Checked);
            confWriter.AddValue("preventSSaver", form2.SSaverCheckBox.Checked);
            confWriter.AddValue("hideFreq", form2.HideFreqCheckBox.Checked);

            confWriter.AddValue("LED", form2.PrisumRadio.Checked);
            confWriter.AddValue("classic", form2.ClassicRadio.Checked);
            confWriter.AddValue("simple", form2.SimpleRadio.Checked);
            confWriter.AddValue("rainbow", form2.RainbowRadio.Checked);
            confWriter.AddValue("verticalLayout", form2.VerticalRadio.Checked);
            confWriter.AddValue("horizontalLayout", form2.HorizontalRadio.Checked);
            confWriter.AddValue("flipNone", form2.NoFlipRadio.Checked);
            confWriter.AddValue("flipRight", form2.RightFlipRadio.Checked);
            confWriter.AddValue("flipLeft", form2.LeftFlipRadio.Checked);
            confWriter.AddValue("hideTitle", form2.HideTitleCheckBox.Checked);
            confWriter.AddValue("mono", form2.MonoRadio.Checked);
            confWriter.AddValue("stereo", form2.StereoRadio.Checked);

            confWriter.AddValue("baseLabelFontSize", baseLabelFontSize);
            confWriter.AddValue("labelPadding", labelPadding);

            string strColors = "";
            for (int i = 0; i < classicColors.Length; i++)
                strColors += Convert.ToInt16(classicColors[i].A).ToString() + "," +
                    Convert.ToInt16(classicColors[i].R).ToString() + "," +
                    Convert.ToInt16(classicColors[i].G).ToString() + "," +
                    Convert.ToInt16(classicColors[i].B).ToString() + ",";
            confWriter.AddValue("classicColors", strColors.TrimEnd(','));

            strColors = "";
            for (int i = 0; i < prisumColors.Length; i++)
                strColors += Convert.ToInt16(prisumColors[i].A).ToString() + "," +
                    Convert.ToInt16(prisumColors[i].R).ToString() + "," +
                    Convert.ToInt16(prisumColors[i].G).ToString() + "," +
                    Convert.ToInt16(prisumColors[i].B).ToString() + ",";
            confWriter.AddValue("prisumColors", strColors.TrimEnd(','));

            strColors = "";
            for (int i = 0; i < simpleColors.Length; i++)
                strColors += Convert.ToInt16(simpleColors[i].A).ToString() + "," +
                    Convert.ToInt16(simpleColors[i].R).ToString() + "," +
                    Convert.ToInt16(simpleColors[i].G).ToString() + "," +
                    Convert.ToInt16(simpleColors[i].B).ToString() + ",";
            confWriter.AddValue("simpleColors", strColors.TrimEnd(','));

            strColors = Convert.ToInt16(bgPen.Color.A).ToString() + "," +
                    Convert.ToInt16(bgPen.Color.R).ToString() + "," +
                    Convert.ToInt16(bgPen.Color.G).ToString() + "," +
                    Convert.ToInt16(bgPen.Color.B).ToString();
            confWriter.AddValue("bgPenColor", strColors);


            int[] bars = { 1, 2, 4, 8, 16 };
            string[] strPrisumPosition = new string[maxNumberOfBar + 1];
            foreach (int bar in bars)
            {
                foreach (var position in prisumPositions[bar])
                {
                    strPrisumPosition[bar] += position.ToString("0.00") + ",";
                }
                strPrisumPosition[bar] = strPrisumPosition[bar].TrimEnd(',');
            }
            confWriter.AddValue("prisumPositions_1", strPrisumPosition[1]);
            confWriter.AddValue("prisumPositions_2", strPrisumPosition[2]);
            confWriter.AddValue("prisumPositions_4", strPrisumPosition[4]);
            confWriter.AddValue("prisumPositions_8", strPrisumPosition[8]);
            confWriter.AddValue("prisumPositions_16", strPrisumPosition[16]);

            string strClassicPosition = "";
            foreach (var item in classicPositions) strClassicPosition += item.ToString("0.00") + ",";
            strClassicPosition = strClassicPosition.Trim(',');
            confWriter.AddValue("classicPosition", strClassicPosition);

            string strSimplePosition = "";
            foreach (var item in simplePositions) strSimplePosition += item.ToString("0.00") + ",";
            strSimplePosition = strSimplePosition.Trim(',');
            confWriter.AddValue("simplePosition", strSimplePosition);

            string devices = string.Empty;
            for (int i = 0; i < form2.devicelist.Items.Count; i++)
            {
                devices += form2.devicelist.Items[i].ToString().TrimEnd() + ",";
            }
                confWriter.AddValue("devices", devices.TrimEnd(','));

            if (UNICODE == false) confWriter.AddValue("unicode", false);
            
            confWriter.AddValue("spectrum2Width", Spectrum2.Width);         // save only for debug
            confWriter.AddValue("spectrum2Height", Spectrum2.Height);
            confWriter.AddValue("spectrum2Top", Spectrum2.Top);
            confWriter.AddValue("spectrum2Left", Spectrum2.Height);

            // add "unicode=false" in config file to change cause codepage to ANSI (not saved)

            string configFileName = @".\" + ProductName + @".conf";
            try
            {
                confWriter.Save(configFileName);
                return true;
            }
            catch (Exception)
            {
                FileStream fs;
                if (!System.IO.File.Exists(configFileName))
                {
                    fs = new FileStream(configFileName, FileMode.CreateNew);
                    fs.Dispose();
                }
                throw;
            }
        }

        public static bool Unicode() { return UNICODE; }
        
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            SaveConfigParams();
        }
    }
}