using System;
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
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        private enum EXECUTION_STATE : uint
        {
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0X80000000,
        }

        public readonly string relText = "Rel." + "25040221";
        private readonly Analyzer analyzer;
        private readonly Form2 form2 = null;
        private Form3 form3 = null;
        private Form4 form4 = null;
        
        // layout
        private int form1Top, form1Left;                    // for load config
        private int form1Width, form1Height;                // for load config to not call SizeChanged Event
        public bool form1Visible;
        private int borderSize;
        private int titleHeight;
        private int verticalSpacing;

        // basic params
        public int[] level;
        private int channel;                // copy from form2
        private int numberOfBar;            // copy from form2
        private string lastDefaultDevice;   // copy from form2

        // calculation
        private int counterCycle;           // copy from form2
        private int[] peakValue;
        private int peakCounter;
        private int peakHoldDescentCycle;   // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)
        private float sensitivity;
        private float sensitivityRatio;
        private int displayOffCounter;
        
        // form size change
        private Point mouseDragStartPoint = new Point(0, 0);
        private struct RectangleBool { public bool left, right, top, bottom; }
        private RectangleBool pinch;

        // form layout
        private int labelPadding;
        private int leftPadding;
        private int topPadding;
        private int bottomPadding;
        private int barLeftPadding;
        private float spectrumWidthScale;
        public int mdt;                    // mouse detect thickness (inner)
        
        // condition static
        public static bool inInit;
        public static bool inLayout;
        private static bool inFormSizeChange;
        
        // constants form form2
        private readonly int maxChannel;
        private readonly int maxNumberOfBar;
        private readonly int defaultBorderSize;
        private readonly int defaultTitleHeight;
        
        // constants
        private const float penWidth = 30f;
        private const int barSpacing = 10;
        private const int baseSpectrumWidth = 650;
        private const int baseSpectrumHeight = 129;
        private static bool UNICODE;

        // draw
        private readonly Bitmap[] canvas;
        private int canvasWidth;
        private Color[] colors;
        private float[] positions;
        private int endPointX, endPointY;                  // default gradient direction, from upward to downward
        private RotateFlipType flipLeft, flipRight;
        private bool rainbowFlip;
        private Pen myPen;
        public readonly Pen bgPen = new Pen(new Color());
        private LinearGradientBrush brush;
        // freq label
        private readonly Label[] freqLabel_Left, freqLabel_Right;
        private float labelFontSize;
        private readonly float baseLabelFontSize = 6f;     // not constant for possibility of change in the future
        public int baseLabelWidth, baseLabelHeight;

        // color settings
        private bool classicChecked, prisumChecked, simpleChecked, rainbowChecked;
        //
        // based on hvianna's color settings from audioMotion-analyzer (https://github.com/hvianna/audioMotion-analyzer)
        //
        private Color[] classicColors =
            {   Color.FromArgb(255,0,0),     //  0 100 50  red
                Color.FromArgb(255,255,0),   // 60 100 50  yellow
                Color.FromArgb(0,128,0),};   //120 100 25  green
        private readonly float[] classicPositions = { 0.0f, 0.3f, 1.0f };

        private Color[] prisumColors =
            {   Color.FromArgb(255,0,0),    //   0  red
                Color.FromArgb(255,255,0),  //  60  yellow
                Color.FromArgb(0,255,0),    // 120  lime
                Color.FromArgb(0,255,255),  // 180  cyan
                Color.FromArgb(0,0,255),};  // 240  blue
        private readonly float[][] prisumPositions = new float[33][];        // maxNumberOfBar+1 jagg array for easy to see

        private Color[] simpleColors = { Color.LightSkyBlue, Color.LightSkyBlue };
        private readonly float[] simplePositions = { 0.0f, 1.0f };

        // for fire
        public event EventHandler DispatchAnalyzerLevelChanged;
        //public event EventHandler SpectrumCleared;

        // static function
        public static bool Unicode() { return UNICODE; }

        public Form1()
        {
            InitializeComponent();

            inInit = true;

            form2 = new Form2(this);// { Owner = this };
            form3 = new Form3(this, form2) { Owner = this };
            form4 = new Form4(this, form2) { Owner = this };


            string configFileName = @".\" + ProductName + @".conf";
            try
            {
                LoadConfigParams();
            }
            catch
            {
                MessageBox.Show(
                    "Opps! Config file seems something wrong...\r\n" +
                    "Backup config file and use default parameters.\r\n" +
                    "When exiting the application,\r\n" +
                    "new config file will be created.",
                    "Config file error - " + ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                if (File.Exists(configFileName))
                {
                    if (File.Exists(configFileName + @".broken")) File.Delete(configFileName + @".broken");
                    if (File.Exists(configFileName)) File.Move(configFileName, configFileName + @".broken");
                }
                LoadConfigParams();
            }

            // after param load, make a Analyzer instance.
            analyzer = new Analyzer(form2);
            if (analyzer.devicenumber > 0)
            {
                analyzer.Enable = true;
            }

            form2.ClassicRadioButton.CheckedChanged += Form2_ClassicRadioButton_CheckChanged;
            form2.PrisumRadioButton.CheckedChanged += Form2_PrisumRadioButton_CheckChanged;
            form2.SimpleRadioButton.CheckedChanged += Form2_SimpleRadioButton_CheckChanged;
            form2.RainbowRadioButton.CheckedChanged += Form2_RainbowRadioButton_CheckChanged;
            form2.RainbowFlipCheckBox.CheckedChanged += Form2_RainbowFlipCheckBox_CheckChanged;
            form2.VerticalRadioButton.CheckedChanged += Form2_V_H_RadioButton_CheckChanged;      // V/H dual use
            form2.VerticalFlipCheckBox.CheckedChanged += Form2_VerticalFlipRadioButton_CheckChanged;
            form2.HorizontalRadioButton.CheckedChanged += Form2_V_H_RadioButton_CheckChanged;    // V/H dual use
            form2.RightFlipRadioButton.CheckedChanged += Form2_FlipSideRadioButton_CheckChanged; // L/R dual use
            form2.LeftFlipRadioButton.CheckedChanged += Form2_FlipSideRadioButton_CheckChanged;  // L/R dual use
            form2.NoFlipRadioButton.CheckedChanged += Form2_FlipSideRadioButton_CheckChanged;
            form2.SensitivityTrackBar.ValueChanged += Form2_SensitivityTrackBar_ValueChanged;
            form2.PeakholdDescentSpeedTrackBar.ValueChanged += Form2_PeakholdDescentSpeedTrackBar_ValueChanged;
            form2.SSaverCheckBox.CheckedChanged += Form2_SSaverCheckbox_CheckedChanged;
            form2.AlwaysOnTopCheckBox.CheckedChanged += Form2_AlwaysOnTopCheckbox_CheckChanged;
            form2.HideFreqCheckBox.CheckedChanged += Form2_HideFreqCheckBox_CheckChanged;
            form2.HideTitleCheckBox.CheckedChanged += Form2_HideTitleCheckBox_CheckChanged;
            form2.HideSpectrumWindowCheckBox.CheckedChanged += Form2_HideSpectrumWindow_CheckChanged;
            form2.ExitAppButton.Click += Form2_ExitAppButton_Click;
            form2.LevelMeterCheckBox.CheckedChanged += Form2_LevelMeterCheckBox_CheckChanged;
            form2.LevelStreamCheckBox.CheckedChanged += Form2_LevelStreamCheckBox_CheckChanged;
            form2.Form_DoubleClick += Form2_Form_DoubleClick;
            form2.TaskTrayCheckBox.CheckedChanged += Form2_TaskTrayCheckBox_CheckChanged;
            form2.ShowCounterCheckBox.CheckedChanged += Form2_ShowCounterCheckBox_CheckChanged;

            form2.NumberOfChannelChanged += Form2_NumberOfChannelsChanged;
            form2.NumberOfBarChanged += Form2_NumberOfBarRadioButton_CheckChanged;
            form2.CounterCycleChanged += Form2_CounterCycleChanged;
            form2.ClearSpectrum += ClearSpectrum;

            form3.FormClosed += ChildFormClosed;
            form3.KeyDown += form2.Form2_KeyDown;
            form4.KeyDown += form2.Form2_KeyDown;

            // Other Event handler (subscribe)
            analyzer.SpectrumChanged += Analyzer_ReceiveSpectrumData;
            Application.ApplicationExit += Application_ApplicationExit;

            // defaults calculated by internal numbers
            this.Text = NotifyIcon.Text = ProductName;

            maxChannel = Form2.maxChannel;
            maxNumberOfBar = Form2.maxNumberOfBar;
            channel = form2.MonoRadioButton.Checked ? 1 : 2;
            numberOfBar = form2.numberOfBar;

            borderSize = defaultBorderSize = form2.borderSize;
            titleHeight = defaultTitleHeight = form2.titleHeight;

            spectrumWidthScale = Spectrum1.Width / (float)baseSpectrumWidth;        // depens on number of bars
            canvasWidth = barLeftPadding + numberOfBar * ((int)penWidth + barSpacing) - (barLeftPadding - barSpacing);  // Calculate size per number of bars to enlarge
            canvas = new Bitmap[maxChannel];
            freqLabel_Left = new Label[maxNumberOfBar];
            freqLabel_Right = new Label[maxNumberOfBar];
            peakValue = new int[maxNumberOfBar * maxChannel];
            endPointX = 0;
            endPointY = baseSpectrumHeight;
            bgPen = new Pen(Color.FromArgb(29, 29, 29), penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            level = new int[maxChannel];

            for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i] = new Label();
            for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Right[i] = new Label();

            // form2 settings
            form2.Init();
            
            // from form2
            counterCycle = form2.counterCycle;
            sensitivity = form2.SensitivityTrackBar.Value / 10f;
            sensitivityRatio = (float)(0xff / baseSpectrumHeight) * (10f - sensitivity);
            TopMost = form3.TopMost = form4.TopMost = form2.AlwaysOnTopCheckBox.Checked;
            peakHoldDescentCycle = form2.PeakholdDescentSpeedTrackBar.Value;
            NotifyIcon.Visible = !(ShowInTaskbar = !form2.TaskTrayCheckBox.Checked);

            if (form2.HideTitleCheckBox.Checked)
            {
                titleHeight = 0;
                borderSize = 0;
                this.FormBorderStyle = FormBorderStyle.None;
                form2.ExitAppButton.Enabled = true;
            }

            // Color scheme settings
            if ((form2.PrisumRadioButton.Checked = prisumChecked) == true)
            {
                colors = prisumColors;
                positions = prisumPositions[1];     // vertical is always 1.
                endPointX = 0;
                endPointY = baseSpectrumHeight;
                form2.RainbowFlipCheckBox.Enabled = false;
            }
            else if ((form2.ClassicRadioButton.Checked = classicChecked) == true)
            {
                colors = classicColors;
                positions = classicPositions;
                endPointX = 0;
                endPointY = baseSpectrumHeight;
                form2.RainbowFlipCheckBox.Enabled = false;
            }
            else if ((form2.SimpleRadioButton.Checked = simpleChecked) == true)
            {
                colors = simpleColors;
                positions = simplePositions;
                endPointX = 0;
                endPointY = baseSpectrumHeight;
                form2.RainbowFlipCheckBox.Enabled = false;
            }
            else
            {
                form2.RainbowRadioButton.Checked = rainbowChecked;
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];               // need for color position adjust
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;    // Horizontal
                endPointY = 0;
                form2.RainbowFlipCheckBox.Enabled = true;
            }
            // set Gradient color pen
            if (!form2.RainbowRadioButton.Checked)
            {
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
            }
            else if (!rainbowFlip)              // Blue -> Red
            {
                brush = new LinearGradientBrush(new Point(endPointX, endPointY), new Point(0, 0), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
            }
            else                                // Red -> Blue
            {
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
            }

            myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            form2.RainbowFlipCheckBox.Checked = rainbowFlip;

            if (form2.StereoRadioButton.Checked)
            {
                if (form2.HorizontalRadioButton.Checked)
                {
                    if (form2.LeftFlipRadioButton.Checked)
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipX;
                        flipRight = RotateFlipType.RotateNoneFlipNone;
                    }
                    else if (form2.RightFlipRadioButton.Checked)
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipNone;
                        flipRight = RotateFlipType.RotateNoneFlipX;
                    }
                }
                else
                {
                    if (form2.VerticalFlipCheckBox.Checked)
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipNone;
                        flipRight = RotateFlipType.RotateNoneFlipY;
                    }
                    else
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipNone;
                        flipRight = RotateFlipType.RotateNoneFlipNone;
                    }
                }
            }

            form3.Visible = LevelMeterToolStripMenuItem.Checked = form2.LevelMeterCheckBox.Checked;
            form4.Visible = LevelStreamToolStripMenuItem.Checked = form2.LevelStreamCheckBox.Checked;

            // debug
            LabelCycle.Visible = form2.ShowCounterCheckBox.Checked;

            inInit = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inInit = true;
            inLayout = true;

            // Now, Set main form layout
            this.Top = form1Top;
            this.Left = form1Left;
            this.Width = form1Width;        // this calls SizeChanged event
            this.Height = form1Height;      // this calls SizeChanged event


            // Then, Set Spectrum PictureBox size and location from main form size
            Form1_SizeChanged(this, EventArgs.Empty);
            ClearSpectrum(this, EventArgs.Empty);       // draw background image


            if (form2.HideSpectrumWindowCheckBox.Checked = !form1Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                form3.Activate();
                form4.Activate();
            }
            else
                SpectrumToolStripMenuItem.Checked = true;

            inLayout = false;
            inInit = false;
        }

        private void Analyzer_ReceiveSpectrumData(object sender, EventArgs e)
        {
            if (inInit) { return; }

            int bounds;
            int isRight = 0; // for interreave
            int dash = (int)(penWidth * bgPen.DashPattern[0]);      // 30f * 0.1f
            int dashBG = (int)(penWidth * bgPen.DashPattern[1]);    // 30f * 0.1f
            int dashUnit = dash + dashBG;                           // 6
            var g = new Graphics[maxChannel];

            for (int i = 0; i < channel; i++)
            {
                g[i] = Graphics.FromImage(canvas[i]);
                g[i].InterpolationMode = InterpolationMode.HighQualityBicubic;
            }
            for (int i = 0; i < numberOfBar * channel; i++)     // _spectumdata receiving from analizer is max32*2=64 byte L,R,L,R,...
            {
                if (channel > 1) isRight = i % 2;               // even: left=0=No, odd: right=1=Yes, mono is always 0

                float posX = barLeftPadding + (i - isRight) / channel * (penWidth + barSpacing);        // horizontal position
                int powY = (int)(analyzer.spectrumdata[i] / sensitivityRatio);                          // calculate drawing vertical length
                g[isRight].DrawLine(bgPen, posX, canvas[0].Height, posX, 0);                            // first, draw BG from bottom to top
                powY = ((powY - dash) / dashUnit + 1) * dashUnit;                                       // calculate LED bounds
                if (powY > 6)                                                                           // spectrumdata 0x00 is powY = 6
                    g[isRight].DrawLine(myPen, posX, canvas[0].Height, posX, canvas[0].Height - powY);  // from bottom to top direction

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
                        for (int j = 0; j < peakValue.Length; j++) peakValue[j] -= dashUnit;        // let Peak descend 1 level
                        if (peakValue[i] < powY) peakValue[i] = powY;                               // When it descends too much, update
                        else if (powY == 0 && peakValue[i] > 0)
                            // never reach here?
                            for (int j = 0; j < peakValue.Length; j++) peakValue[j] -= dashUnit;
                    }
                    bounds = ((peakValue[i] - dash) / dashUnit + 1) * dashUnit;                     // calculate border line ((int)((x-3)/6)+1)*6 same as powY
                    if (bounds > 6)                                                                 // entity of peak drawing. _spectrum 0x00 is  powY=6
                        g[isRight].DrawLine(myPen, posX, canvas[0].Height - bounds, posX, canvas[0].Height - bounds - dash);   // draw only 1 scale from bounds
                }

                peakCounter++;      // end of peak drawing

                // debug
                //if (form2.ShowCounterCheckBox.Checked) LabelCycle.Text = peakCounter.ToString("0000") + " / " + counterCycle.ToString()+ displayOffCounter.ToString();

            }   // numberOfBar forloop ended

            g[0].Dispose();
            canvas[0].RotateFlip(flipLeft);     // Left or mono
            Spectrum1.Image = canvas[0];        // mono/left=canvas[0]

            if (channel > 1)                    // Right, there is canvas[1] only in "Stereo"
            {
                g[1].Dispose();
                canvas[1].RotateFlip(flipRight);
                Spectrum2.Image = canvas[1];
            }

            level = analyzer.level;
            DispatchAnalyzerLevelChanged?.Invoke(this, EventArgs.Empty);        // Dispatch form3 and form4 for level meter

            // reset peakhold
            if (peakCounter >= counterCycle)    // if peakhold=false, counter is also used screen saver preventing, so add the counter always
            {
                peakCounter = 0;                // reset when the specified number of rounds
                for (int i = 0; i < numberOfBar * channel; i++)
                    peakValue[i] = (int)(analyzer.spectrumdata[i] / sensitivityRatio);      // reset peak also
                displayOffCounter++;
            }

            // Screen saver prevention
            if (displayOffCounter > 90)
            {
                if (form2.SSaverCheckBox.Checked)
                {
                    SetThreadExecutionState(
                        EXECUTION_STATE.ES_DISPLAY_REQUIRED |
                        EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                }
                else
                    SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);

                displayOffCounter = 0;
            }
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
            //SpectrumCleared?.Invoke(this, EventArgs.Empty);

            if (form2.RainbowRadioButton.Checked)
            {
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;
                endPointY = 0;
            }
        }

        private void LocateFrequencyLabel(PictureBox spectrum, Label[] freqLabel, bool isFlip)
        {
            if (MouseButtons == MouseButtons.Left) return;     // prevent from dragging

            spectrumWidthScale = (float)spectrum.Width / baseSpectrumWidth * (maxNumberOfBar / numberOfBar);
            if (baseLabelWidth >= 100)
            {
                Label baseLabel = new Label() { Font = new Font("Arial", 7f, FontStyle.Bold), Text = "20khz", AutoSize = true, Visible = false, };
                this.Controls.Add(baseLabel);
                baseLabelWidth = baseLabel.Width;//28;
                baseLabelHeight = baseLabel.Height;//12;
                baseLabel.Dispose();
            }

            SuspendLayout();

            labelFontSize = (float)Math.Round(baseLabelFontSize * spectrumWidthScale, 1) - 1;
            labelFontSize = labelFontSize < 11f ? labelFontSize : 11f;
            labelFontSize = labelFontSize > 7f ? labelFontSize : 7f;

            for (int i = 0; i < maxNumberOfBar; i++)
            {
                string labelText;
                float freqvalues = (float)Math.Pow(2, i * 10.0 / (numberOfBar - 1) + 4.29);     // 4.29=Round(Log(20000hz, 2) - (in increments of)10, 2);
                if (freqvalues > 10000)
                    labelText = (Math.Round(freqvalues / 1000, MidpointRounding.AwayFromZero)).ToString("0") + "k";    // "khz";
                else if (freqvalues > 1000)
                    labelText = (Math.Round(freqvalues / 100, MidpointRounding.AwayFromZero) * 100 / 1000f).ToString("0.0") + "k";
                else if (freqvalues > 100 || numberOfBar < 32)
                    labelText = (Math.Round(freqvalues / 10, MidpointRounding.AwayFromZero) * 10).ToString("0");       // round -1
                else
                    labelText = freqvalues.ToString(" 0");

                int j = isFlip ? numberOfBar - 1 - i : i;

                if (i < numberOfBar)        // Only Bar exists
                {
                    freqLabel[i].Height = freqLabel[i].Font.Height;
                    freqLabel[i].AutoSize = true;
                    freqLabel[i].Font = new Font("Arial", labelFontSize, FontStyle.Bold);
                    freqLabel[j].Text = labelText;
                    int labelPos = (int)(spectrum.Left + (float)(barLeftPadding + i * (penWidth + barSpacing)) / (float)(barSpacing + numberOfBar * (penWidth + barSpacing)) * spectrum.Width) - (int)(baseLabelWidth / 2);
                    freqLabel[i].Left = labelPos;
                    freqLabel[i].Name = "freqlabel" + (j + 1).ToString();
                    freqLabel[i].Top = spectrum.Bottom + (labelPadding + bottomPadding) / 3 - freqLabel[i].Font.Height / 2;
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
            if (spectrum == Spectrum1 && channel < maxChannel)
            {
                freqLabel_Left[numberOfBar - 1].MouseMove += Form1_MouseMove;
                freqLabel_Left[numberOfBar - 1].MouseDown += Form1_MouseDown;
                freqLabel_Left[numberOfBar - 1].MouseUp += Form1_MouseUp;
            }
            else if (spectrum == Spectrum2)
            {
                freqLabel_Right[numberOfBar - 1].MouseMove += Form1_MouseMove;
                freqLabel_Right[numberOfBar - 1].MouseDown += Form1_MouseDown;
                freqLabel_Right[numberOfBar - 1].MouseUp += Form1_MouseUp;

                freqLabel_Left[numberOfBar - 1].MouseMove -= Form1_MouseMove;
                freqLabel_Left[numberOfBar - 1].MouseDown -= Form1_MouseDown;
                freqLabel_Left[numberOfBar - 1].MouseUp -= Form1_MouseUp;
            }

            this.Controls.AddRange(freqLabel);
            ResumeLayout(false);

            if ((!isFlip && freqLabel[numberOfBar - 2].Right > freqLabel[numberOfBar - 1].Left ||
                isFlip && freqLabel[1].Left < freqLabel[0].Right) && freqLabel[numberOfBar - 1].Left != 0)
            {           // 2 lines
                int j = isFlip ? -1 : 0;
                for (int i = 1; i < maxNumberOfBar; i += 2) freqLabel[i + j].Top += (int)(freqLabel[0].Height * 0.9);        // lower line
                if (isFlip)
                    freqLabel[0].Left += freqLabel[0].Width / 3;
            }
            else        // 1 line
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel[i].Top += freqLabel[0].Height / 3;
            if (freqLabel[0].Left < spectrum.Left) freqLabel[0].Left = spectrum.Left;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        //private void SetSpectrumLayout(int formW, int formH)
        {
            int formW = this.Width;
            int formH = this.Height;

            // void, but the objective is to determine size of Spectrum and label location from size of form.
            // All sizes of Spectrum should be determined only here.
            // Labels are also drawn by calling LocateFrequencyLabel from here.
            // Be sure to calculate the size of the Spectrum from the size of the form.
            // (Do not calculate size of form from size of Spectrum.)
            // Every time set both Width and Height to be sure.
            // 
            // Difference of form's VH 'form' was finished calcurations in V_H_Change function.
            // (The reason is, that function need the 'form' before the change, and it need to know
            // there has been a change.)
            //
            // Factors that influence process of label drawing are,
            // AA. label drawing required or not (In case of V layout, only bottom(R ch.) labels needed.
            // BB. Consider Flip (left, right and none)

            inLayout = true;    // prevent double executions from SizeChanged

            if (!form2.HideFreqCheckBox.Checked)    // cases requiring Labels
            {
                if (form2.HorizontalRadioButton.Checked)         // H-layout: need Labels
                {
                    Spectrum1.Width = Spectrum2.Width = (formW - borderSize * 2 - leftPadding * 2) / channel;   // Spectrum.Width makes no difference with or without Labels
                    Spectrum1.Height = Spectrum2.Height = formH - borderSize * 2 - titleHeight - topPadding - labelPadding - baseLabelHeight - bottomPadding;
                    Spectrum1.Top = Spectrum2.Top = topPadding;
                    Spectrum1.Left = leftPadding;
                    Spectrum2.Left = Spectrum1.Right;

                    // only H-layout requires L Ch. labels
                    if (channel < 2) LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    else
                    {
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: form2.LeftFlipRadioButton.Checked);
                        LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: form2.RightFlipRadioButton.Checked);
                    }
                }
                else                                            // V-layout: need Labels
                {
                    Spectrum1.Width = Spectrum2.Width = formW - borderSize * 2 - leftPadding * 2;               // Spectrum.Width makes no difference with or without Labels
                    Spectrum1.Height = Spectrum2.Height = (formH - borderSize * 2 - titleHeight - topPadding - verticalSpacing * (channel - 1) - labelPadding - baseLabelHeight - bottomPadding) / channel;
                    Spectrum1.Top = topPadding;
                    Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                    Spectrum1.Left = Spectrum2.Left = leftPadding;

                    if (channel < 2) LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    else
                    {
                        for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;             // stereo V layout, not required left label
                        LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);                        // only case in stereo is visible
                    }
                }
            }
            else        // cases not requiring Labels
            {
                if (form2.HorizontalRadioButton.Checked)        // H-layout: not need Labels
                {
                    Spectrum1.Width = Spectrum2.Width = (formW - borderSize * 2 - leftPadding * 2) / channel; // Spectrum.Width makes no difference with or without Labels
                    Spectrum1.Height = Spectrum2.Height = formH - borderSize * 2 - titleHeight - topPadding;
                    Spectrum1.Top = Spectrum2.Top = topPadding;
                    Spectrum1.Left = leftPadding;
                    Spectrum2.Left = Spectrum1.Right;// + 1;
                }
                else                                            // V-layout: not need Labels
                {
                    Spectrum1.Width = Spectrum2.Width = formW - borderSize * 2 - leftPadding * 2;       // Spectrum.Width makes no difference with or without Labels
                    Spectrum1.Height = Spectrum2.Height = (formH - borderSize * 2 - titleHeight - topPadding - verticalSpacing * (channel - 1)) / channel; // mono:0, steereo:1
                    Spectrum1.Top = topPadding;
                    Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                    Spectrum1.Left = Spectrum2.Left = leftPadding;
                }

                // Common process for V and H that does not require Labels.
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

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                AppContextMenuStrip.Show();
            else if (e.Button == MouseButtons.Left)
                Activate();
        }

        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!form2.Visible)
                form2.Visible = true;
        }

        private void SpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.HideSpectrumWindowCheckBox.Checked = !SpectrumToolStripMenuItem.Checked;
        }

        private void LevelMeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.LevelMeterCheckBox.Checked = LevelMeterToolStripMenuItem.Checked;
        }

        private void LevelStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.LevelStreamCheckBox.Checked = LevelStreamToolStripMenuItem.Checked;
        }

        private void DeviceReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSpectrum(this, EventArgs.Empty);
            analyzer.ReloadDefaultDevice(this, EventArgs.Empty);
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        //private void Spectrum1_DoubleClick(object sender, EventArgs e)
        //private void Spectrum2_DoubleClick(object sender, EventArgs e)
        {
            if (!form2.Visible) form2.Visible = true;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        //private void Spectrum1_MouseDown(object sender, MouseEventArgs e)
        //private void Spectrum2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !form2.Visible)
                form2.Visible = true;
            else if (e.Button == MouseButtons.Left)
            {
                mouseDragStartPoint = new Point(e.X, e.Y);
                int _height, _width;
                if (form2.HideTitleCheckBox.Checked)
                {
                    _height = ((Control)sender).Height;
                    _width = ((Control)sender).Width;

                    pinch.left = e.X < mdt;
                    pinch.right = e.X > _width - mdt;
                    pinch.top = e.Y < mdt;
                    pinch.bottom = e.Y > _height - mdt;
                    inFormSizeChange = pinch.left || pinch.right || pinch.top || pinch.bottom;
                    if (!inFormSizeChange) Cursor = Cursors.SizeAll;
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                SetMinimumFormSize();

                if (e.Button == MouseButtons.Left)
                {
                    if (Cursor == Cursors.SizeWE)
                    {
                        if (pinch.left)
                        {
                            Width -= e.X - mouseDragStartPoint.X;
                            Left += e.X - mouseDragStartPoint.X;
                        }
                        else if (pinch.right)
                        {
                            if (sender == this)
                                Width = e.X;
                            else
                                Width = ((Control)sender).Left + e.X;
                        }
                    }
                    else if (Cursor == Cursors.SizeNS)
                    {
                        /*if (pinch.top) {} else*/      // Form1 has no upper edge.
                        if (pinch.bottom)
                        {
                            Height = e.Y;
                        }
                    }
                    else if (Cursor == Cursors.SizeNESW)
                    {
                        if (pinch.left && pinch.bottom)
                        {
                            Width -= e.X - mouseDragStartPoint.X;
                            Left += e.X - mouseDragStartPoint.X;
                            Height = e.Y;
                        }
                        /*else if (pinch.right && pinch.top) {}*/       // Form1 has no right upper corner.
                    }
                    else if (Cursor == Cursors.SizeNWSE)
                    {
                        if (pinch.right && pinch.bottom)
                        {
                            Width = e.X;
                            Height = e.Y;
                        }
                        /*else if (pinch.left && pinch.top) {}*/
                    }
                    else
                    {
                        Left += e.X - mouseDragStartPoint.X;
                        Top += e.Y - mouseDragStartPoint.Y;
                    }
                }

                if (MouseButtons != MouseButtons.Left && sender == this)
                {
                    if (e.X < mdt && e.Y > Height - mdt)               // LeftBottom
                        Cursor = Cursors.SizeNESW;
                    else if (e.X > Width - mdt && e.Y > Height - mdt)  // RightBottom
                        Cursor = Cursors.SizeNWSE;
                    else if (e.X < mdt || ((channel < maxChannel || form2.VerticalRadioButton.Checked) && e.X > Spectrum1.Width - mdt))
                        Cursor = Cursors.SizeWE;
                    else if ((e.Y < mdt || e.Y > Height - mdt) && Cursor != Cursors.SizeWE)
                        Cursor = Cursors.SizeNS;
                    else if (Cursor != Cursors.SizeAll && e.Button == MouseButtons.None)
                        Cursor = Cursors.Default;
                }
                else if (MouseButtons != MouseButtons.Left && sender == freqLabel_Right[numberOfBar - 1])
                {
                    if (e.X > ((Control)sender).Width - mdt)
                        Cursor = Cursors.SizeWE;
                }
            }
        }

        private void Form1_MouseHover(object sender, EventArgs e)
        //private void Spectrum11_MouseHover(object sender, EventArgs e)
        //private void Spectrum12_MouseHover(object sender, EventArgs e)
        {
            inFormSizeChange = false;
            Cursor = Cursors.Default;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        //private void Spectrum1_MouseUp(object sender, MouseEventArgs e)
        //private void Spectrum2_MouseUp(object sender, MouseEventArgs e)
        {
            if (Cursor != Cursors.Default)
            {
                Cursor = Cursors.Default;
                inLayout = pinch.left = pinch.right = pinch.top = pinch.bottom = false;
                Form1_SizeChanged(this, EventArgs.Empty);
            }
        }

        private void Spectrum1_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                SetMinimumFormSize();

                if (e.Button == MouseButtons.Left)
                {
                    if (Cursor == Cursors.SizeWE)
                    {
                        if (pinch.left)
                        {
                            Width -= e.X - mouseDragStartPoint.X;
                            Left += e.X - mouseDragStartPoint.X;
                        }
                        else if (pinch.right && (form2.MonoRadioButton.Checked || form2.VerticalRadioButton.Checked))
                        {
                            Width = e.X;
                        }
                    }
                    else if (Cursor == Cursors.SizeNS)
                    {
                        if (pinch.top)
                        {
                            Height -= e.Y - mouseDragStartPoint.Y;
                            Top += e.Y - mouseDragStartPoint.Y;
                        }
                        else if (pinch.bottom)
                        {
                            Height = e.Y;
                        }
                    }
                    else if (Cursor == Cursors.SizeNESW)
                    {
                        if (pinch.left && pinch.bottom)
                        {
                            Width -= e.X - mouseDragStartPoint.X;
                            Left += e.X - mouseDragStartPoint.X;
                            Height = e.Y;
                        }
                        else if (pinch.right && pinch.top)
                        {
                            Width = e.X;
                            Height -= e.Y - mouseDragStartPoint.Y;
                            Top += e.Y - mouseDragStartPoint.Y;
                        }
                    }
                    else if (Cursor == Cursors.SizeNWSE)
                    {
                        if (pinch.left && pinch.top)
                        {
                            Width -= e.X - mouseDragStartPoint.X;
                            Left += e.X - mouseDragStartPoint.X;
                            Height -= e.Y - mouseDragStartPoint.Y;
                            Top += e.Y - mouseDragStartPoint.Y;
                        }
                    }
                    else
                    {
                        Left += e.X - mouseDragStartPoint.X;
                        Top += e.Y - mouseDragStartPoint.Y;
                    }
                }
                if (e.Button != MouseButtons.Left)
                { 
                    if (e.X < mdt && e.Y < mdt)                         // LeftTop
                        Cursor = Cursors.SizeNWSE;
                    else if ((channel < maxChannel || form2.VerticalRadioButton.Checked) &&
                            e.X > Spectrum1.Width - mdt && e.Y < mdt)   // RightTop
                        Cursor = Cursors.SizeNESW;
                    else if (form2.HideFreqCheckBox.Checked &&          // LeftBottom
                            (channel < maxChannel || form2.HorizontalRadioButton.Checked) &&
                            e.X < mdt && e.Y > Spectrum1.Height - mdt)
                        Cursor = Cursors.SizeNESW;
                    else if (form2.HideFreqCheckBox.Checked &&          // RightBottom 
                            channel < maxChannel &&
                            e.X > Spectrum1.Width - mdt &&
                            e.Y > Spectrum1.Height - mdt)
                        Cursor = Cursors.SizeNWSE;
                    else if (e.X < mdt || (channel < maxChannel || channel > 1 &&
                            form2.VerticalRadioButton.Checked) &&
                            e.X > Spectrum1.Width - mdt)
                        Cursor = Cursors.SizeWE;
                    else if (e.Y < mdt || (form2.HideFreqCheckBox.Checked &&
                            e.Y > Spectrum1.Height - mdt))
                        Cursor = Cursors.SizeNS;
                    else if (Cursor != Cursors.SizeAll &&
                            e.Button == MouseButtons.None)
                        Cursor = Cursors.Default;
                }
            }
        }

        private void Spectrum2_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                SetMinimumFormSize();

                if (e.Button == MouseButtons.Left)
                {
                    if (Cursor == Cursors.SizeWE)
                    {
                        if (pinch.left)
                        {
                            Width -= e.X - mouseDragStartPoint.X;
                            Left += e.X - mouseDragStartPoint.X;
                        }
                        else if (pinch.right)
                        {
                            if (channel > 1 && form2.HorizontalRadioButton.Checked)
                                Width = e.X * 2;      // borderThickness is 0 at this case
                            else
                                Width = e.X;
                        }
                    }
                    else if (Cursor == Cursors.SizeNS)
                    {
                        if (pinch.top)
                        {
                            Height -= e.Y - mouseDragStartPoint.Y;
                            Top += e.Y - mouseDragStartPoint.Y;
                        }
                        else if (pinch.bottom)
                        {
                            if (form2.VerticalRadioButton.Checked)
                            {
                                Height = e.Y * 2 + verticalSpacing;
                            }
                            else
                            {
                                Height = e.Y;
                            }
                        }
                    }
                    else if (Cursor == Cursors.SizeNESW)
                    {
                        /*if (pinch.left && pinch.bottom)*/
                        if (pinch.right && pinch.top)
                        {
                            if (channel > 1 && form2.HorizontalRadioButton.Checked)
                                Width = e.X * 2;
                            else
                                Width = e.X;
                            Height -= e.Y - mouseDragStartPoint.Y;
                            Top += e.Y - mouseDragStartPoint.Y;
                        }
                    }
                    else if (Cursor == Cursors.SizeNWSE)
                    {
                        if (pinch.right && pinch.bottom)
                        {
                            if (channel > 1 && form2.HorizontalRadioButton.Checked)
                            {
                                Width = e.X * 2;
                            }
                            else
                            {
                                Width = e.X;
                            }
                            Height = e.Y;
                        }
                        /*else if (pinch.left && pinch.top)*/
                    }
                    else
                    {
                        Left += e.X - mouseDragStartPoint.X;
                        Top += e.Y - mouseDragStartPoint.Y;
                    }
                }

                if (e.Button != MouseButtons.Left)
                {
                    if (form2.HorizontalRadioButton.Checked &&          // RightTop
                        e.X > Spectrum2.Width - mdt && e.Y < mdt)
                        Cursor = Cursors.SizeNESW;
                    else if (form2.HideFreqCheckBox.Checked &&          // LeftBottom
                            form2.VerticalRadioButton.Checked &&
                            e.X < mdt && e.Y > Spectrum2.Height - mdt)
                        Cursor = Cursors.SizeNESW;
                    else if (form2.HideFreqCheckBox.Checked &&          // RightBottom 
                            e.X > Spectrum2.Width - mdt && e.Y > Spectrum2.Height - mdt)
                        Cursor = Cursors.SizeNWSE;
                    else if (e.X > Spectrum2.Width - mdt || (form2.VerticalRadioButton.Checked && e.X < mdt))
                        Cursor = Cursors.SizeWE;
                    else if ((e.Y < mdt && form2.HorizontalRadioButton.Checked) || (form2.HideFreqCheckBox.Checked && e.Y > Spectrum2.Height - mdt))
                        Cursor = Cursors.SizeNS;
                    else if (Cursor != Cursors.SizeAll && e.Button == MouseButtons.None)
                        Cursor = Cursors.Default;
                }
            }
        }

        private void SetMinimumFormSize()
        {
            int minWidth, minHeight;
            if (channel > 1 && form2.HorizontalRadioButton.Checked)                     // Form's minimum width
                minWidth = !form2.HideFreqCheckBox.Checked ? 12 * numberOfBar * channel : 128 * channel;
            else
                minWidth = !form2.HideFreqCheckBox.Checked ? 12 * numberOfBar : 128;
            if (channel > 1 && form2.VerticalRadioButton.Checked)                       // Form's minimum height
                minHeight = !form2.HideFreqCheckBox.Checked ? 48 * channel + verticalSpacing + labelPadding + bottomPadding : 48 * channel + verticalSpacing;
            else                                                                        // ver2+label4+bottom16    118:98  
                minHeight = !form2.HideFreqCheckBox.Checked ? 48 + labelPadding + bottomPadding : 48;
            MinimumSize = new Size(minWidth, minHeight);                                // 68:48
        }

        private void Form2_CounterCycleChanged(object sender, EventArgs e)
        {
            numberOfBar = form2.numberOfBar;
            counterCycle = form2.counterCycle;
        }

        private void Form2_NumberOfBarRadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (inInit) return;

            numberOfBar = form2.numberOfBar;
            counterCycle = form2.counterCycle;
            peakHoldDescentCycle = 20 * numberOfBar / channel / form2.PeakholdDescentSpeedTrackBar.Value;

            if (form2.RainbowRadioButton.Checked)     // need for color position adjust (Horizontal LED(Prisum) color)
            {
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;    // right-end of image by number of bar
                endPointY = 0;
                brush = new LinearGradientBrush(new Point(endPointX, endPointY), new Point(0, 0), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            }

            canvasWidth = barLeftPadding + numberOfBar * ((int)penWidth + barSpacing) - (barLeftPadding - barSpacing);  // to stretch, calculate canvas size by number of bar
            spectrumWidthScale = (float)Spectrum1.Width / baseSpectrumWidth * (maxNumberOfBar / numberOfBar);
            
            if (!form2.HideFreqCheckBox.Checked)
            {
                if(channel < 2)
                    LocateFrequencyLabel(Spectrum1, freqLabel_Left, false);
                else if (!form2.VerticalRadioButton.Checked)
                    LocateFrequencyLabel(Spectrum1, freqLabel_Left, form2.LeftFlipRadioButton.Checked);
                else
                    for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;

                LocateFrequencyLabel(Spectrum2, freqLabel_Right, form2.RightFlipRadioButton.Checked);
            }

            if (form2.RainbowRadioButton.Checked) Form2_RainbowRadioButton_CheckChanged(sender, EventArgs.Empty);   // To change color positons.

            inLayout = true;
            if (!inInit)
            {
                int _width;
                if (channel > 1 && form2.HorizontalRadioButton.Checked)                     // Form's minimum width
                    _width = !form2.HideFreqCheckBox.Checked ? 12 * numberOfBar * channel : 128 * channel;
                else
                    _width = !form2.HideFreqCheckBox.Checked ? 12 * numberOfBar : 128;
                if (MinimumSize.Width < _width)
                {
                    MinimumSize = new Size(_width, MinimumSize.Height);
                    Form1_SizeChanged(this, EventArgs.Empty);
                }
                ClearSpectrum(sender, EventArgs.Empty);
            }
            inLayout = false;
        }

        private void Form2_NumberOfChannelsChanged(object sender, EventArgs e)
        {
            inLayout = true;
            inInit = true;

            // reset parameters
            channel = form2.MonoRadioButton.Checked ? 1 : 2;
            peakValue = new int[maxNumberOfBar * channel];
            //counterCycle = (int)(peakHoldTimeMsec / analyzer._timer1.Interval.Milliseconds * Form2.cycleMultiplyer * (numberOfBar * channel / (float)maxNumberOfBar));
            peakHoldDescentCycle = 20 * numberOfBar / channel / form2.PeakholdDescentSpeedTrackBar.Value;       // Inverse the value so that the direction of
                                                                                                                // speed and value increase/decrease match.
            for (int i = 0; i < analyzer.spectrumdata.Count; i++) analyzer.spectrumdata[i] = 0x00;

            inInit = false;

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

            if (!form2.HideFreqCheckBox.Checked)    // cases in require labels
            {
                if (form2.HorizontalRadioButton.Checked)    // H-layout: require labels
                {
                    int _width = (borderSize + leftPadding) * 2 + Spectrum1.Width * channel;
                    if (_width < MinimumSize.Width) MinimumSize = new Size(_width, MinimumSize.Height);
                    Width = _width;
                    //Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height + labelPadding + baseLabelHeight + bottomPadding;

                    if (channel > 1)
                    {
                        if (form2.LeftFlipRadioButton.Checked)
                        {
                            flipLeft = RotateFlipType.RotateNoneFlipX;
                            flipRight = RotateFlipType.RotateNoneFlipNone;
                        }
                        else if (form2.RightFlipRadioButton.Checked)
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
                else                                        // V-layout: require labels
                {
                    int _height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height * channel + verticalSpacing * (channel - 1) + labelPadding + baseLabelHeight + bottomPadding;
                    if (_height < MinimumSize.Height) MinimumSize = new Size(MinimumSize.Width, _height);
                    Height = _height;
                    //this.Width = (borderSize + leftPadding) * 2 + Spectrum1.Width;

                    if (channel > 1 || form2.VerticalFlipCheckBox.Checked)
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipNone;
                        flipRight = RotateFlipType.RotateNoneFlipY;
                    }
                }
            }
            else                                    // cases in not require labels
            {
                if (form2.HorizontalRadioButton.Checked)    // H-layout: not require labels
                {
                    int _width = (borderSize + leftPadding) * 2 + Spectrum1.Width * channel;
                    if (_width < MinimumSize.Width) MinimumSize = new Size(_width, MinimumSize.Height);
                    Width = _width;
                    //this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height;

                    if (form2.LeftFlipRadioButton.Checked)
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipX;
                        flipRight = RotateFlipType.RotateNoneFlipNone;
                    }
                    else if (form2.RightFlipRadioButton.Checked)
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
                else                                        // V-layout: not require labels
                {
                    int _height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height * channel + verticalSpacing * (channel - 1);
                    if (_height < MinimumSize.Height) MinimumSize = new Size(MinimumSize.Width, _height);
                    Height = _height;
                    //this.Width = (borderSize + leftPadding) * 2 + Spectrum1.Width;
                    
                    if (form2.VerticalFlipCheckBox.Checked && channel > 1)
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipNone;
                        flipRight = RotateFlipType.RotateNoneFlipY;
                    }
                    else
                    {
                        flipLeft = RotateFlipType.RotateNoneFlipNone;
                        flipRight = RotateFlipType.RotateNoneFlipNone;
                    }
                }
            }

            inLayout = false;

            Form1_SizeChanged(this, EventArgs.Empty);
            ClearSpectrum(this, EventArgs.Empty);
            Form2_CounterCycleChanged(this, EventArgs.Empty);

            inInit = false;
        }
        
        private void Form2_ClassicRadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (form2.ClassicRadioButton.Checked && !inInit)
            {
                colors = classicColors;
                positions = classicPositions;
                endPointX = 0;
                endPointY = canvas[0].Height;
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
                form2.RainbowFlipCheckBox.Enabled = false;
            }
        }

        private void Form2_PrisumRadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (form2.PrisumRadioButton.Checked && !inInit)
            {
                colors = prisumColors;
                positions = prisumPositions[1];     // In vertical, position is always 1.
                endPointX = 0;
                endPointY = canvas[0].Height;
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
                form2.RainbowFlipCheckBox.Enabled = false;
            }
        }

        private void Form2_SimpleRadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (form2.SimpleRadioButton.Checked && !inInit)
            {
                colors = simpleColors;
                positions = simplePositions;
                endPointX = 0;
                endPointY = canvas[0].Height;
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
                form2.RainbowFlipCheckBox.Enabled = false;
            }
        }

        private void Form2_RainbowRadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (form2.RainbowRadioButton.Checked && !inInit)
            {
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];       // need for color position adjustment
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;        // Horizontal
                endPointY = 0;
                if (!rainbowFlip)
                {
                    brush = new LinearGradientBrush(new Point(endPointX, endPointY), new Point(0, 0), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                    { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                }
                else
                {
                    brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                    { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                }
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
                form2.RainbowFlipCheckBox.Enabled = true;
            }
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

        private void Form2_SSaverCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // Do nothing here, flag is checked by timer.
        }

        private void Form2_AlwaysOnTopCheckbox_CheckChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                TopMost = form3.TopMost = form4.TopMost = !TopMost;
            }
        }

        private void Form2_TaskTrayCheckBox_CheckChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                if (!form2.HideSpectrumWindowCheckBox.Checked)
                    this.Visible = false;       // for preventing flicker by task tray icon
                if (form2.LevelMeterCheckBox.Checked)
                    form3.Visible = false;
                if (form2.LevelStreamCheckBox.Checked)
                    form4.Visible = false;
                if (form2.TaskTrayCheckBox.Checked)
                {
                    form2.ShowInTaskbar = this.ShowInTaskbar = false;
                    NotifyIcon.Visible = true;
                    //NotifyIcon.ShowBalloonTip(1000, ProductName, ProductName + " is running in the background.", ToolTipIcon.Info);
                    if (this.Visible) Activate();
                }
                else
                {
                    form2.ShowInTaskbar = this.ShowInTaskbar = true;
                    NotifyIcon.Visible = false;
                    if (this.Visible) Activate();
                }
                Screen screen = Screen.FromPoint(new Point(this.Left, this.Top));
                if (screen != Screen.PrimaryScreen)
                {       // These are needed in case of different resolution in multi display
                    this.Left += 1;
                    this.Left -= 1;
                    form3.Left += 1;
                    form3.Left -= 1;
                    form4.Left += 1;
                    form4.Left -= 1;
                }
                if (!form2.HideSpectrumWindowCheckBox.Checked)
                    this.Visible = true;
                if (form2.LevelMeterCheckBox.Checked)
                    form3.Visible = true;
                if (form2.LevelStreamCheckBox.Checked)
                    form4.Visible = true;

                form2.Activate();

            }
        }

        private void Form2_V_H_RadioButton_CheckChanged(object sender, EventArgs e)                    // V/H dual use
        {
            // Only resizing of the H and V forms and drawing of the freq. labels are handled here.
            // This will not call Form1_SizeChanged(). Because it's hard to align that sizes of Spectrum will not change.
            // Changing layout cause form's Width and Height changes (but try not to call SizeChanged function)
            // Do not change size of Spectrums and form location here.
            // So don't need to call SetLayout here.
            // No difference in actual form size by Labels exist or not (Height of Spectrum must be already changed)
            // Do not call Form1_SizeChanged(), so draw label here

            inLayout = true;

            if (form2.HorizontalRadioButton.Checked)  // change to H Layout
            {
                int _height, _width = (borderSize + leftPadding + Spectrum1.Width) * 2;          // -1 is for overlap on left and right 
                if (form2.HideFreqCheckBox.Checked)     // Height "calculation" is different by labels exist or not, no difference in actual form size
                    _height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height;// + bottomPadding;
                else
                    _height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height + labelPadding + baseLabelHeight + bottomPadding;
                if (MinimumSize.Width > _width) MinimumSize = new Size(_width, MinimumSize.Height);
                if (MinimumSize.Height > _height) MinimumSize = new Size(MinimumSize.Width, _height);
                this.Size = new Size(_width, _height);

                // only Spectrum2(R Ch.) moves, do not change their size
                Spectrum2.Top = Spectrum1.Top;
                Spectrum2.Left = Spectrum1.Right;

                // only H layout has L Ch.'s label. To set flip state.
                if (form2.LeftFlipRadioButton.Checked)
                {
                    LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: true);
                    flipLeft = RotateFlipType.RotateNoneFlipX;
                    flipRight = RotateFlipType.RotateNoneFlipNone;
                }
                else if (form2.RightFlipRadioButton.Checked)
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
                LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: form2.RightFlipRadioButton.Checked);

                form2.FlipGroup.Enabled = true;
                form2.VerticalFlipCheckBox.Enabled = false;

                // shift Setting dialog location
                /*if (form2.Top < this.Bottom &&
                    form2.Right > this.Left &&
                    form2.Left < this.Right &&
                    this.Bottom < Screen.FromControl(this).Bounds.Height - 50) form2.Top = this.Bottom + 30;
                */
            }
            else        // change to V Layout
            {
                int _height, _width = (borderSize + leftPadding) * 2 + Spectrum1.Width;
                if (form2.HideFreqCheckBox.Checked)     // Spectrum.Height calculation is different by labels exist or not, no difference in actual form size
                    _height = borderSize * 2 + Spectrum1.Height * channel + titleHeight + topPadding + verticalSpacing * (channel - 1);// + bottomPadding;
                else                                    // V-Layout required Label
                    _height = (borderSize + Spectrum1.Height) * 2 + titleHeight + topPadding + verticalSpacing * (channel - 1) + labelPadding + /*freqLabel_Left[0].Height */ baseLabelHeight + bottomPadding;
                if (MinimumSize.Width > _width) MinimumSize = new Size(_width, MinimumSize.Height);
                if (MinimumSize.Height > _height) MinimumSize = new Size(MinimumSize.Width, _height);
                this.Size = new Size(_width, _height);

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

                Form2_VerticalFlipRadioButton_CheckChanged(sender, EventArgs.Empty);

                form2.FlipGroup.Enabled = false;
                form2.VerticalFlipCheckBox.Enabled = true;

                // shift Setting dialog location
                /*if (form2.Top < this.Bottom &&
                    form2.Right > this.Left &&
                    this.Bottom < Screen.FromControl(this).Bounds.Height - 50) form2.Top = this.Bottom + 30;
                */
            }

            inLayout = false;
        }

        private void Form2_FlipSideRadioButton_CheckChanged(object sender, EventArgs e)                // L/R dual use
        {
            if (!inInit)
            {
                if (form2.NoFlipRadioButton.Checked)
                {
                    flipLeft = RotateFlipType.RotateNoneFlipNone;
                    flipRight = RotateFlipType.RotateNoneFlipNone;

                    if (!form2.VerticalRadioButton.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);
                }
                else if (form2.LeftFlipRadioButton.Checked)
                {
                    flipLeft = RotateFlipType.RotateNoneFlipX;
                    flipRight = RotateFlipType.RotateNoneFlipNone;
                    if (!form2.VerticalRadioButton.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: true);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);
                }
                else
                {
                    flipLeft = RotateFlipType.RotateNoneFlipNone;
                    flipRight = RotateFlipType.RotateNoneFlipX;
                    if (!form2.VerticalRadioButton.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: true);
                }

            }
        }

        private void Form2_VerticalFlipRadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (!inInit && channel > 1)
            {
                if (form2.VerticalFlipCheckBox.Checked)
                {
                    flipLeft = RotateFlipType.RotateNoneFlipNone;
                    flipRight = RotateFlipType.RotateNoneFlipY;
                }
                else
                {
                    flipLeft = RotateFlipType.RotateNoneFlipNone;
                    flipRight = RotateFlipType.RotateNoneFlipNone;
                }
            }
        }

        private void Form2_HideFreqCheckBox_CheckChanged(object sender, EventArgs e)
        {
            inLayout = true;
            if (form2.HideFreqCheckBox.Checked)
            {
                int _height = this.Height - (labelPadding + baseLabelHeight + bottomPadding);
                if (MinimumSize.Height > _height) MinimumSize = new Size(MinimumSize.Width, _height);
                this.Height = _height;

                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Right[i].Visible = false;
            }
            else
            {
                this.Height += labelPadding + baseLabelHeight + bottomPadding;
                if (channel > 1 && form2.HorizontalRadioButton.Checked)                     // Form's minimum width
                    this.Width = !form2.HideFreqCheckBox.Checked ? 12 * numberOfBar * channel : 128 * channel;
                else
                    this.Width = !form2.HideFreqCheckBox.Checked ? 12 * numberOfBar : 128;
                Form1_SizeChanged(this, EventArgs.Empty);
            }
            inLayout = false;
        }

        private void Form2_HideTitleCheckBox_CheckChanged(object sender, EventArgs e)
        {
            inLayout = true;
            inInit = true;
            if (form2.HideTitleCheckBox.Checked)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                form3.FormBorderStyle = FormBorderStyle.None;
                form4.FormBorderStyle = FormBorderStyle.None;
                titleHeight = defaultTitleHeight;
                borderSize = defaultBorderSize;

                this.Top += titleHeight + borderSize;
                this.Left += borderSize;
                form3.Top += titleHeight + borderSize;
                form3.Left += borderSize;
                form4.Top += titleHeight + borderSize;
                form4.Left += borderSize;

                form2.ExitAppButton.Enabled = true;

                titleHeight = 0;
                borderSize = 0;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                form3.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form4.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                titleHeight = defaultTitleHeight;
                borderSize = defaultBorderSize;

                this.Top -= titleHeight + borderSize;
                this.Left -= borderSize;
                form3.Top -= titleHeight + borderSize;
                form3.Left -= borderSize;
                form4.Top -= titleHeight + borderSize;
                form4.Left -= borderSize;

                form2.ExitAppButton.Enabled = !Visible || WindowState == FormWindowState.Minimized;
            }
            inInit = false;
            inLayout = false;
        }

        private void Form2_ShowCounterCheckBox_CheckChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                if (form2.ShowCounterCheckBox.Checked)
                    LabelCycle.Visible = true;
                else
                    LabelCycle.Visible = false;
            }
        }

        private void Form2_LevelMeterCheckBox_CheckChanged(object sender, EventArgs e)
        {
            if (form2.LevelMeterCheckBox.Checked)
            {
                form2.CloseButton.Enabled = form2.HideSpectrumWindowCheckBox.Enabled = true;
                form2.ExitAppButton.Enabled = form2.HideTitleCheckBox.Checked || !Visible;
                form2.LevelMeterSensitivityGroupBox.Enabled =
                LevelMeterToolStripMenuItem.Checked = true;

                try
                {
                    form3.Visible = true;
                }
                catch (ObjectDisposedException)
                {
                    form3 = new Form3(this, form2) { Owner = this };
                    form3.Show();
                }
            }
            else
            {
                if ((!Visible || WindowState == FormWindowState.Minimized) && !form4.Visible)
                {
                    form2.CloseButton.Enabled = false;
                    form2.HideSpectrumWindowCheckBox.Enabled = form2.HideSpectrumWindowCheckBox.Checked;
                    form2.ExitAppButton.Enabled = false;
                }
                else
                {
                    form2.CloseButton.Enabled = true;
                    form2.HideSpectrumWindowCheckBox.Enabled = true;
                    form2.ExitAppButton.Enabled = form2.HideTitleCheckBox.Checked || !Visible;
                }
                form2.LevelMeterSensitivityGroupBox.Enabled =
                LevelMeterToolStripMenuItem.Checked =
                form3.Visible = false;
            }
        }

        private void Form2_LevelStreamCheckBox_CheckChanged(object sender, EventArgs e)
        {
            if (form2.LevelStreamCheckBox.Checked)
            {
                form2.CloseButton.Enabled = form2.HideSpectrumWindowCheckBox.Enabled = true;
                form2.ExitAppButton.Enabled = form2.HideTitleCheckBox.Checked || !Visible;
                LevelStreamToolStripMenuItem.Checked = true;

                try
                {
                    form4.Visible = true;
                }
                catch (ObjectDisposedException)
                {
                    form4 = new Form4(this, form2) { Owner = this };
                    form4.Show();
                }
                form2.LevelStreamPanel.Enabled = true;
            }
            else
            {
                if ((!Visible || WindowState == FormWindowState.Minimized) && !form3.Visible)
                {
                    form2.CloseButton.Enabled = false;
                    form2.HideSpectrumWindowCheckBox.Enabled = form2.HideSpectrumWindowCheckBox.Checked;
                    form2.ExitAppButton.Enabled = false;
                }
                else
                {
                    form2.CloseButton.Enabled = true;
                    form2.HideSpectrumWindowCheckBox.Enabled = true;
                    form2.ExitAppButton.Enabled = form2.HideTitleCheckBox.Checked || !Visible;
                }
                form2.LevelStreamPanel.Enabled =
                LevelStreamToolStripMenuItem.Checked = 
                form4.Visible = false;
            }
        }

        private void Form2_HideSpectrumWindow_CheckChanged(object sender, EventArgs e)
        {
            if (inInit == false) this.WindowState = FormWindowState.Normal;

            if (form2.HideSpectrumWindowCheckBox.Checked == false)      // form1 visible
            {
                form2.CloseButton.Enabled =
                form2.HideSpectrumWindowCheckBox.Enabled =
                form2.SpectrumColorShemeGroupBox.Enabled =
                form2.SpectrumPeakholdGroupBox.Enabled =
                form2.SpectrumSensitivityGroupBox.Enabled =
                form2.NumberOfBarsPanel.Enabled =
                form2.NoCGroupBox.Enabled = 
                form2.HideFreqCheckBox.Enabled = true;

                form2.ExitAppButton.Enabled = form2.HideTitleCheckBox.Checked;
                this.WindowState = FormWindowState.Normal;
                this.Visible = SpectrumToolStripMenuItem.Checked = true;
                form2.Activate();
            }
            else                                                        // form1 not visible
            {
                if (!form3.Visible && !form4.Visible && !inInit)
                {
                    form2.CloseButton.Enabled =
                    form2.ExitAppButton.Enabled = false;
                }
                else
                {
                    form2.CloseButton.Enabled =
                    form2.ExitAppButton.Enabled = true;
                }
                form2.SpectrumColorShemeGroupBox.Enabled =
                form2.SpectrumPeakholdGroupBox.Enabled =
                form2.SpectrumSensitivityGroupBox.Enabled =
                form2.NumberOfBarsPanel.Enabled =
                form2.NoCGroupBox.Enabled =
                form2.HideFreqCheckBox.Enabled = false;

                this.Visible = SpectrumToolStripMenuItem.Checked = false;
            }
        }

        private void Form2_RainbowFlipCheckBox_CheckChanged(object sender, EventArgs e)
        {
            if (!form2.RainbowFlipCheckBox.Checked)     // Blue -> Red
            {
                brush = new LinearGradientBrush(new Point(endPointX, endPointY), new Point(0, 0), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                rainbowFlip = false;
            }
            else                                        // Red -> Blue
            {
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                { InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions } };
                rainbowFlip = true;
            }
                
            myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
        }

        private void Form2_Form_DoubleClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Save config file?",
                "Save config file",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.OK)
                SaveConfigParams();
        }

        private void Form2_ExitAppButton_Click(object sender, EventArgs e)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            AppContextMenuStrip.Dispose();
            lastDefaultDevice = form2.DefaultDeviceName.Text;
            Application.Exit();
        }

        private void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Visible && !form3.Visible && !form4.Visible)
            {
                form2.Activate();
                form2.Visible = true;
            }
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
                MessageBox.Show(
                    "No Config file was found.\r\n" +
                    "Default Parameters will be used.\r\n" +
                    "When exit the application,\r\n" +
                    "Config file will be created automatically.",
                    "Config file not found - " + ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            form2.numberOfBar = confReader.GetValue("numberOfBar", 16);
            form2.MonoRadioButton.Checked = confReader.GetValue("mono", false);
            form2.StereoRadioButton.Checked = confReader.GetValue("stereo", true);

            leftPadding = confReader.GetValue("horizontalPadding", 0);
            topPadding = confReader.GetValue("verticalPadding", 0);
            verticalSpacing = confReader.GetValue("verticalSpacing", 4);
            barLeftPadding = confReader.GetValue("barLeftPadding", 25);
            bottomPadding = confReader.GetValue("bottompadding", 16);
            labelPadding = confReader.GetValue("labelPadding", 4);
            baseLabelWidth = confReader.GetValue("baseLabelWidth", 17);
            baseLabelHeight = confReader.GetValue("baseLabelHeight", 17);
            mdt = confReader.GetValue("mouseDetectThickness", 10);

            form2.peakHoldTimeMsec = confReader.GetValue("peakHoldTimeMsec", 500);
            form2.SensitivityTrackBar.Value = confReader.GetValue("sensitivity", 80);
            form2.PeakholdDescentSpeedTrackBar.Value = confReader.GetValue("peakholdDescentSpeed", 12);
            form2.LevelSensitivityTrackBar.Value = confReader.GetValue("levelmeterSensitivity", 10);

            form2.PeakholdCheckBox.Checked = confReader.GetValue("peakhold", true);
            form2.AlwaysOnTopCheckBox.Checked = confReader.GetValue("alwaysOnTop", false);
            form2.SSaverCheckBox.Checked = confReader.GetValue("preventSSaver", false);
            form2.HorizontalRadioButton.Checked = confReader.GetValue("horizontalLayout", true);
            form2.VerticalRadioButton.Checked = confReader.GetValue("verticalLayout", false);
            form2.VerticalFlipCheckBox.Checked = confReader.GetValue("verticalFlip", false);
            form2.NoFlipRadioButton.Checked = confReader.GetValue("flipNone", true);
            form2.RightFlipRadioButton.Checked = confReader.GetValue("flipRight", false);
            form2.LeftFlipRadioButton.Checked = confReader.GetValue("flipLeft", false);
            form2.HideFreqCheckBox.Checked = confReader.GetValue("hideFreq", false);
            form2.HideTitleCheckBox.Checked = confReader.GetValue("hideTitle", false);
            form2.TaskTrayCheckBox.Checked = confReader.GetValue("taskTray", false);
            form2.AutoReloadCheckBox.Checked = confReader.GetValue("autoReload", false);
            form2.SeparateStreamRadioButton.Checked = confReader.GetValue("separateStream", true);
            form2.CombineStreamRadioButton.Checked = confReader.GetValue("combineStream", false);


            if ((prisumChecked = confReader.GetValue("LED", true)) == false)
                if ((classicChecked = confReader.GetValue("classic", false)) == false)
                    if ((simpleChecked = confReader.GetValue("simple", false)) == false)
                        rainbowChecked = confReader.GetValue("rainbow", false);

            Color[] Argb(string s)
            {
                int p = 0;
                string[] s_arr = s.Split(',');
                int c_num = s_arr.Length / 4;
                Color[] c_arr = new Color[c_num];
                for (int i = 0; i < c_num; i++)
                    c_arr[i] = Color.FromArgb(int.Parse(s_arr[p++]), int.Parse(s_arr[p++]), int.Parse(s_arr[p++]), int.Parse(s_arr[p++]));
                return c_arr;
            }
            prisumColors = Argb(confReader.GetValue("prisumColors", "255,255,0,0, 255,255,255,0, 255,0,255,0, 255,0,255,255, 255,0,0,255"));
            classicColors = Argb(confReader.GetValue("classicColors", "255,255,0,0, 255,255,255,0, 255,0,128,0"));
            simpleColors = Argb(confReader.GetValue("simpleColors", "255,135,206,250, 255,135,206,250"));

            bgPen.Color = Argb(confReader.GetValue("bgPenColor", "255,29,29,29"))[0];
            form3.greenPen.Color = Argb(confReader.GetValue("greenPenColor", "144, 144, 238, 144"))[0];
            form3.redPen.Color = Argb(confReader.GetValue("redPenColor", "144, 255, 0, 0"))[0];
            form4.streamPen.Color = Argb(confReader.GetValue("streamPenColor", "192, 0, 255, 255"))[0];
            form4.startPen.Color = Argb(confReader.GetValue("startPenColor", "255, 255, 0, 0"))[0];

            int[] bars = { 1, 2, 4, 8, 16, 32 };
            string[][] param = new string[Form2.maxNumberOfBar + 1][];
            param[1] = confReader.GetValue("prisumPositions_1", "0.0, 0.20, 0.25, 0.27, 1.0").Split(',');      // red,yellow,lime,cyan,blue
            param[2] = confReader.GetValue("prisumPositions_2", "0.0, 0.35, 0.5, 0.55, 1.0").Split(',');
            param[4] = confReader.GetValue("prisumPositions_4", "0.0, 0.50, 0.52, 0.62, 1.0").Split(',');
            param[8] = confReader.GetValue("prisumPositions_8", "0.0, 0.50, 0.70, 0.75, 1.0").Split(',');
            param[16] = confReader.GetValue("prisumPositions_16", "0.0, 0.30, 0.50, 0.60, 1.0").Split(',');
            param[32] = confReader.GetValue("prisumPositions_32", "0.0, 0.30, 0.50, 0.60, 1.0").Split(',');
            foreach (int bar in bars)                                   // 1, 2, 4,8,16,32
            {
                prisumPositions[bar] = new float[param[bar].Length];
                for (int j = 0; j < prisumPositions[bar].Length; j++)   // 0,1,2,3,4    red,yellow,lime,cyan,blue
                {
                    prisumPositions[bar][j] = float.Parse(param[bar][j]);
                }
            }

            int pos = 0;
            foreach (string s in confReader.GetValue("classicPosition", "0.00, 0.30, 1.00").Split(','))
            {
                classicPositions[pos] = float.Parse(s);
                pos++;
            }
            pos = 0;
            foreach (string s in confReader.GetValue("simplePosition", "0.00, 1.00").Split(','))
            {
                simplePositions[pos] = float.Parse(s);
                pos++;
            }

            pos = 0;                                       //bands: Dummy Dummy Dummy  8,    16,   32
            foreach (string s in confReader.GetValue("adjustMono", "0.41, 0.41, 0.41, 0.41, 0.33, 0.22").Split(','))    //    less->Hi, large->Low
            {
                form2.adjustTable[0][pos] = float.Parse(s);
                pos++;
            }
            pos = 0;                                       //bands:  Dummy Dummy  Dummy   8,   16,   32
            foreach (string s in confReader.GetValue("adjustStereo", "1.80, 1.80, 1.80, 1.80, 1.25, 0.90").Split(','))  //    less->Hi, large->Low
            {
                form2.adjustTable[1][pos] = float.Parse(s);
                pos++;
            }

            rainbowFlip = confReader.GetValue("rainbowFlip", false);
            form4.streamScrollUnit = confReader.GetValue("streamScrollUnit", 1);

            form1Top = confReader.GetValue("form1Top", 130);
            form1Left = confReader.GetValue("form1Left", 130);
            form1Width = confReader.GetValue("form1Width", 800);            // When Vertical. Otherwise, resize automatically later
            form1Height = confReader.GetValue("form1Height", 192);
            form1Visible = confReader.GetValue("form1Visible", true);
            
            form2.Top = confReader.GetValue("form2Top", form1Top + form1Height + topPadding);
            form2.Left = confReader.GetValue("form2Left", form1Left);

            form2.form3Width = confReader.GetValue("form3Width", 320);
            form2.form3Height = confReader.GetValue("form3Height", 96);
            form2.form3Top = confReader.GetValue("form3Top", form1Top);                         // adjusted
            form2.form3Left = confReader.GetValue("form3Left", form1Left + form1Width + 48);    // adjusted
            form2.LevelMeterCheckBox.Checked = confReader.GetValue("form3Visible", false);

            form2.form4Width = confReader.GetValue("form4Width", 640);
            form2.form4Height = confReader.GetValue("form4Height", 192);
            form2.form4Top = confReader.GetValue("form4Top", form2.form3Top + form2.form3Height + bottomPadding);
            form2.form4Left = confReader.GetValue("form4Left", form2.form3Left);
            form2.LevelStreamCheckBox.Checked = confReader.GetValue("form4Visible", false);
            
            form4.streamScrollUnit = confReader.GetValue("streamScrollUnit", 1);
            form4.streamChannelSpacing = confReader.GetValue("streamChannelSpacing", 0);
            form2.pow = confReader.GetValue("streamPower", 0.5f);


            UNICODE = confReader.GetValue("unicode", true);                 // In conf file, add "unicode=false" cause change codepage to ANSI
        }

        private bool SaveConfigParams()
        {
            var confWriter = new ConfigWriter();
            confWriter.AddValue("ver.", relText);
            confWriter.AddValue("lastDefaultDevice", lastDefaultDevice);    // Save only, just to be sure
            confWriter.AddValue("numberOfBar", form2.numberOfBar);

            confWriter.AddValue("form1Top", this.Top);
            confWriter.AddValue("form1Left", this.Left);
            confWriter.AddValue("form1Width", this.Width);
            confWriter.AddValue("form1Height", this.Height);
            confWriter.AddValue("form1Visible", !form2.HideSpectrumWindowCheckBox.Checked);
            confWriter.AddValue("form2Top", form2.Top);
            confWriter.AddValue("form2Left", form2.Left);
            confWriter.AddValue("maximized", this.WindowState == FormWindowState.Maximized);

            confWriter.AddValue("horizontalPadding", leftPadding);
            confWriter.AddValue("verticalPadding", topPadding);
            confWriter.AddValue("verticalSpacing", verticalSpacing);
            confWriter.AddValue("barLeftPadding", barLeftPadding);
            confWriter.AddValue("bottomPadding", bottomPadding);
            confWriter.AddValue("labelPadding", labelPadding);
            confWriter.AddValue("baseLabelWidth", freqLabel_Left[0].Width);
            confWriter.AddValue("baseLabelHeight", freqLabel_Left[0].Height);
            confWriter.AddValue("mouseDetectThickness", mdt);

            confWriter.AddValue("peakHoldTimeMsec", form2.peakHoldTimeMsec);
            confWriter.AddValue("sensitivity", form2.SensitivityTrackBar.Value);
            confWriter.AddValue("peakholdDescentSpeed", form2.PeakholdDescentSpeedTrackBar.Value);
            confWriter.AddValue("levelmeterSensitivity", form2.LevelSensitivityTrackBar.Value);

            confWriter.AddValue("peakhold", form2.PeakholdCheckBox.Checked);
            confWriter.AddValue("alwaysOnTop", form2.AlwaysOnTopCheckBox.Checked);
            confWriter.AddValue("preventSSaver", form2.SSaverCheckBox.Checked);
            confWriter.AddValue("hideFreq", form2.HideFreqCheckBox.Checked);
            confWriter.AddValue("hideTitle", form2.HideTitleCheckBox.Checked);
            confWriter.AddValue("autoReload", form2.AutoReloadCheckBox.Checked);
            confWriter.AddValue("taskTray", form2.TaskTrayCheckBox.Checked);
            confWriter.AddValue("LED", form2.PrisumRadioButton.Checked);
            confWriter.AddValue("classic", form2.ClassicRadioButton.Checked);
            confWriter.AddValue("simple", form2.SimpleRadioButton.Checked);
            confWriter.AddValue("rainbow", form2.RainbowRadioButton.Checked);
            confWriter.AddValue("verticalLayout", form2.VerticalRadioButton.Checked);
            confWriter.AddValue("horizontalLayout", form2.HorizontalRadioButton.Checked);
            confWriter.AddValue("verticalFlip", form2.VerticalFlipCheckBox.Checked);
            confWriter.AddValue("flipNone", form2.NoFlipRadioButton.Checked);
            confWriter.AddValue("flipRight", form2.RightFlipRadioButton.Checked);
            confWriter.AddValue("flipLeft", form2.LeftFlipRadioButton.Checked);
            confWriter.AddValue("mono", form2.MonoRadioButton.Checked);
            confWriter.AddValue("stereo", form2.StereoRadioButton.Checked);
            confWriter.AddValue("separateStream", form2.SeparateStreamRadioButton.Checked);
            confWriter.AddValue("combineStream", form2.CombineStreamRadioButton.Checked);
            
            string Argb(Color[] c)
            {
                string ret = string.Empty;
                for (int i = 0; i < c.Length; i++)
                {
                    ret +=
                        Convert.ToInt16(c[i].A).ToString() + "," +
                        Convert.ToInt16(c[i].R).ToString() + "," +
                        Convert.ToInt16(c[i].G).ToString() + "," +
                        Convert.ToInt16(c[i].B).ToString() + ",";
                }
                return ret.TrimEnd(',');
            }
            confWriter.AddValue("prisumColors", Argb(prisumColors));
            confWriter.AddValue("classicColors", Argb(classicColors));
            confWriter.AddValue("simpleColors", Argb(simpleColors));

            confWriter.AddValue("bgPenColor", Argb(new Color[] { bgPen.Color }));
            confWriter.AddValue("greenPenColor", Argb(new Color[] { form3.greenPen.Color }));
            confWriter.AddValue("redPenColor", Argb(new Color[] { form3.redPen.Color }));
            confWriter.AddValue("streamPenColor", Argb(new Color[] { form4.streamPen.Color }));
            confWriter.AddValue("startPenColor", Argb(new Color[] { form4.startPen.Color }));

            int[] bars = { 1, 2, 4, 8, 16, 32 };
            string[] strPrisumPosition = new string[maxNumberOfBar + 1];
            foreach (int bar in bars)
            {
                foreach (var position in prisumPositions[bar])
                    strPrisumPosition[bar] += position.ToString("0.00") + ",";

                strPrisumPosition[bar] = strPrisumPosition[bar].TrimEnd(',');
            }
            confWriter.AddValue("prisumPositions_1", strPrisumPosition[1]);
            confWriter.AddValue("prisumPositions_2", strPrisumPosition[2]);
            confWriter.AddValue("prisumPositions_4", strPrisumPosition[4]);
            confWriter.AddValue("prisumPositions_8", strPrisumPosition[8]);
            confWriter.AddValue("prisumPositions_16", strPrisumPosition[16]);
            confWriter.AddValue("prisumPositions_32", strPrisumPosition[32]);

            string strClassicPosition = string.Empty;
            foreach (var item in classicPositions) strClassicPosition += item.ToString("0.00") + ",";
            strClassicPosition = strClassicPosition.Trim(',');
            confWriter.AddValue("classicPosition", strClassicPosition);

            string strSimplePosition = string.Empty;
            foreach (var item in simplePositions) strSimplePosition += item.ToString("0.00") + ",";
            strSimplePosition = strSimplePosition.Trim(',');
            confWriter.AddValue("simplePosition", strSimplePosition);

            string strAdjustMono = string.Empty;
            foreach (var item in form2.adjustTable[0]) strAdjustMono += item.ToString("0.00") + ",";
            strAdjustMono = strAdjustMono.Trim(',');
            confWriter.AddValue("adjustMono", strAdjustMono);

            string strAdjustStereo = string.Empty;
            foreach (var item in form2.adjustTable[1]) strAdjustStereo += item.ToString("0.00") + ",";
            strAdjustStereo = strAdjustStereo.Trim(',');
            confWriter.AddValue("adjustStereo", strAdjustStereo);

            confWriter.AddValue("rainbowFlip", rainbowFlip);
            confWriter.AddValue("streamScroll", form4.streamScrollUnit);

            confWriter.AddValue("spectrum2Top", Spectrum2.Top);                 // for debug save only
            confWriter.AddValue("spectrum2Left", Spectrum2.Left);
            confWriter.AddValue("spectrum2Width", Spectrum2.Width);
            confWriter.AddValue("spectrum2Height", Spectrum2.Height);

            confWriter.AddValue("form3Top", form3.Top);
            confWriter.AddValue("form3Left", form3.Left);
            confWriter.AddValue("form3Width", form3.Width + (!form2.HideTitleCheckBox.Checked ? form2.defaultBorderSize * 2 : 0));
            confWriter.AddValue("form3Height", form3.Height + (!form2.HideTitleCheckBox.Checked ? form2.defaultTitleHeight + form2.defaultBorderSize : 0));
            confWriter.AddValue("form3Visible", form2.LevelMeterCheckBox.Checked);    // When exiting, form3 has already closed

            confWriter.AddValue("form4Top", form4.Top);
            confWriter.AddValue("form4Left", form4.Left);
            confWriter.AddValue("form4Width", form4.Width + (!form2.HideTitleCheckBox.Checked ? form2.defaultBorderSize * 2 : 0));
            confWriter.AddValue("form4Height", form4.Height + (!form2.HideTitleCheckBox.Checked ? form2.defaultTitleHeight + form2.defaultBorderSize : 0));
            confWriter.AddValue("form4Visible", form2.LevelStreamCheckBox.Checked);    // When exiting, form4 has already closed
            confWriter.AddValue("streamScrollUnit", form4.streamScrollUnit);
            confWriter.AddValue("streamChannelSpacing", form4.streamChannelSpacing);
            confWriter.AddValue("streamPower", form2.pow);

            if (UNICODE == false) confWriter.AddValue("unicode", false);

            string configFileName = @".\" + ProductName + @".conf";
            try
            {
                confWriter.Save(configFileName);
                return true;
            }
            catch (Exception)
            {
                FileStream fs;
                if (!File.Exists(configFileName))
                {
                    fs = new FileStream(configFileName, FileMode.CreateNew);
                    fs.Dispose();
                }
                throw;
            }
        }
        
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = false;
                WindowState = FormWindowState.Normal;
                Top = form1Top;
                Left = form1Left;
                Width = form1Width;
                Height = form1Height;
            }
            SaveConfigParams();
        }
    }
}