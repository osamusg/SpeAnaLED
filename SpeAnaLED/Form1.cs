using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using System.Windows.Shapes;
using ConfigFile;
using Un4seen.Bass.Misc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


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

        const int WS_BORDER = 0x00800000;

        private readonly Analyzer analyzer;
        private readonly Form2 form2 = null;
        private int form1Top;
        private int form1Left;
        private int form1Width;
        private int form1Height;
        private int form2Top;
        private int form2Left;
        private const int maxNumberOfBar = 16;
        private bool maximized = false;
        private int borderSize;
        private int titleHeight;
        private readonly Bitmap[] canvas = new Bitmap[2];
        private Color[] colors;
        private float[] positions;
        private Pen myPen;
        private readonly Pen bgPen = new Pen(Color.White);
        private LinearGradientBrush brush;
        private Label[] freqLabel_Left = new Label[maxNumberOfBar];
        private Label[] freqLabel_Right = new Label[maxNumberOfBar];
        private int counterCycle;
        private readonly int cycleMultiplyer;
        private int[] peakValue;
        private int peakCounter = 0;
        private int channel;
        public int numberOfBar;
        public static bool UNICODE;
        private float ratio;
        private int dash;
        private int displayOffCounter;
        private bool inInit = false;
        private const int baseSpectrumWidth = 650;
        private const int baseSpectrumHeight = 129;
        private int canvasWidth;
        //private bool isVHChange = false;
        private bool inLayout = false;
        private Point mousePoint = new Point(0, 0);


        // parameters (set defaults)
        private int endPointX = 0;              // グラデーションのデフォルト描画方向 上から下
        private int endPointY;                  // PictureBoxの大きさにより可変?
        private const float penWidth = 30f;
        private int labelPadding;
        private const int barSpacing = 10;
        private readonly float baseLabelFontSize = 6f;
        private int barLeftPadding;
        private int leftPadding;
        private int topPadding;
        private int bottomPadding;
        private int verticalSpacing;
        private float spectrumWidthScale;
        //private float spectrumHeightScale;

        public static int deviceNumber;
        private float sensitivity;
        private int peakHoldTimeMsec;
        private int peakHoldDecayCycle = 10;    // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)
        private bool classicChecked, prisumChecked, simpleChecked, rainbowChecked;
        private int flipSide;                   // 0: none 1:left(Center Low) 2:right(Center Hi)

        // Just to be safe, set default to arrays, they are nessesaly to fix each length.
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
            form2 = new Form2();

            string configFileName = @".\" + ProductName + @".conf";
            try
            {
                LoadConfigParams();
            }
            catch
            {
                if (!System.IO.File.Exists(configFileName))
                {
                    MessageBox.Show("No Config file was found.\r\nUse default Parameters.",
                        "Config file not found - " + ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    LoadConfigParams();
                }
                else
                {
                    MessageBox.Show("Opps! Config file seems something wrong...\r\n" +
                        "Delete file and use default parameters.",
                        "Config file error - " + ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    System.IO.File.Delete(configFileName);
                    LoadConfigParams();
                }
                //SetDefaultParams();
            }

            // after param load, make Analyzer instance.
            analyzer = new Analyzer(form2.devicelist, form2.EnumerateButton, form2.NumberOfBarComboBox, form2.RadioMono);

            if (analyzer._devicelist.SelectedIndex == -1)
            {
                analyzer.Enable = false;
                analyzer.DisplayEnable = false;
                form2.devicelist.Items.Add("Please Enumrate Devices");
                form2.devicelist.SelectedIndex = 0;
            }
            else
            {
                analyzer.Enable = true;
                analyzer.DisplayEnable = true;
            }

            // Event handler for option form (subscribe)
            //form2.RadioMono.CheckedChanged += Form2_RadioMonoCheckChanged;
            form2.SensitivityTrackBar.ValueChanged += Form2_SensitivityTrackBar_ValueChanged;
            form2.PeakholdDecayTimeTrackBar.ValueChanged += Form2_PeakholdDecayTimeTrackBar_ValueChanged;
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

            // Other Event handler (subscribe)
            analyzer.SpectrumChanged += ReceiveSpectrumData;
            analyzer.ChannelChanged += NumberOfChannelsChanged;
            Application.ApplicationExit += Application_ApplicationExit;

            // 以下内部の計算で出すもの
            this.Text = ProductName;
            borderSize = (this.Width - this.ClientSize.Width) / 2;
            titleHeight = this.Height - this.ClientSize.Height - borderSize * 2;
            endPointY = Spectrum1.Height;
            //numberOfBar = analyzer._lines;
            channel = analyzer._channel;
            peakValue = new int[maxNumberOfBar * channel];
            bgPen = new Pen(Color.FromArgb(29, 29, 29), penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            canvasWidth = baseSpectrumWidth;      // 例外防止にデフォ16Barで計算しておく
            dash = (int)penWidth / 10;
            for (int i = 0; i < channel; i++) canvas[i] = new Bitmap(baseSpectrumWidth, baseSpectrumHeight);
            for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i] = new Label();
            for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Right[i] = new Label();
            
            // デフォルト(前回)カラー(pen)の事前準備
            colors = prisumColors;
            positions = prisumPositions[numberOfBar];

            brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
            {
                InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
            };
            myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            
            // I don't know why "* 20 (num..." is necessary. This is due to actual measurements.
            // If you edit this parameter, also edit "Form2_ComboBox1_SelectedItemChanged" and
            // "Form2_ComboBox2_SelectedItemChanged".
            cycleMultiplyer = 50;
            // default hold time
            counterCycle = (int)(peakHoldTimeMsec / analyzer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar * channel / 16.0));
        
            spectrumWidthScale = Spectrum1.Width / (float)baseSpectrumWidth;        // Depens on number Of Bar
            //spectrumHeightScale = Spectrum1.Height / (float)baseSpectrumHeght;

            
            //ココカラ
            if (form2.NoFlipRadio.Checked) flipSide = 0;            // No flip
            else if (form2.LeftFlipRadio.Checked) flipSide = 1;     // Left
            else flipSide = 2;                                      // Right

            if (channel < 2)        //あとで検討要
            {
                //Spectrum1.Visible = false;
                //for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
            }
            
            // form2 setting
            form2.NumberOfBarComboBox.SelectedIndex = form2.NumberOfBarComboBox.Items.IndexOf(numberOfBar.ToString());
            form2.PeakholdTimeComboBox.SelectedIndex = form2.PeakholdTimeComboBox.Items.IndexOf(peakHoldTimeMsec.ToString());
            form2.SensitivityTextBox.Text = (form2.SensitivityTrackBar.Value / 10f).ToString("0.0");
            form2.DecaySpeedTextBox.Text = form2.PeakholdDecayTimeTrackBar.Value.ToString();
            if (!form2.PeakholdCheckBox.Checked) form2.LabelPeakhold.Enabled = form2.LabelMsec.Enabled = form2.PeakholdTimeComboBox.Enabled = false;
            if (form2.HideTitleCheckBox.Checked)
            {
                titleHeight = 0;
                borderSize = 0;
                //this.ControlBox = false;
                //this.Text = String.Empty;
                this.FormBorderStyle = FormBorderStyle.None;
                form2.ExitAppButton.Enabled = true;
            }

            // from form2
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
                positions = prisumPositions[numberOfBar];       // need for color position adjust
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;        // Horizontal
                endPointY = 0;
            }
            brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
            {
                InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
            };
            myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };

            sensitivity = form2.SensitivityTrackBar.Value / 10f;
            ratio = (float)(0xff / baseSpectrumHeight) * (10f - sensitivity);
            TopMost = form2.AlwaysOnTopCheckBox.Checked;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (form2.HideFreqCheckBox.Checked) Form2_HideFreqCheckBoxCheckChanged(sender, EventArgs.Empty);

            // Now, Set main form layout
            this.Top = form1Top;
            this.Left = form1Left;
            this.Width = form1Width;        // calls sizeChanged event
            this.Height = form1Height;      // calls sizeChanged event
            this.WindowState = maximized ? FormWindowState.Maximized : FormWindowState.Normal;
            form2.Top = form2Top;
            form2.Left = form2Left;

            // Then, Set Spectrum PictureBox size and location from main form size
            SetSpectrumLayout(this.Width, this.Height);
            ClearSpectrum(this, EventArgs.Empty);

            inInit = false;
        }

        /*protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (this.FormBorderStyle != FormBorderStyle.None)
                {
                    cp.Style &= ~WS_BORDER;
                }
                return cp;
            }
        }*/

        public static int DeviceNumber() { return deviceNumber; }

        /*private void SetLayout_Old()
        {       // 大きさ・配置は全てここで処理すること。Spectrumの大きさはいじらないでフォームから自動計算させること。
            // SetLayout first, after that, ClearSpectrum
            Spectrum1.Top = topPadding;
            Spectrum1.Left = leftPadding;
            canvasWidth = barLeftPadding + numberOfBar * ((int)penWidth + barSpacing) - (barLeftPadding - barSpacing);

            if (form2.RadioHorizontal.Checked)      // V -> H
            {
                if (!isVHChange || inInit)　   // from other event
                {
                    this.Width = (borderSize + leftPadding + Spectrum1.Width) * 2;// - 1;        // -1 : Left and Right, overlapping 1px
                }
                else    // Horizontal button pushed (V->H)
                {
                    Spectrum1.Width = Spectrum2.Width = (this.Size.Width - borderSize * 2 - leftPadding) / 2;
                    
                }
                
                Spectrum2.Top = Spectrum1.Top;
                Spectrum2.Left = Spectrum1.Right;
                spectrumWidthScale = (float)Spectrum1.Width / baseSpectrumWidth * (maxNumberOfBar / numberOfBar);
                
                if (form2.CheckBoxHideFreq.Checked)
                {
                    this.Height = borderSize * 2 + titleHeight + Spectrum1.Height + topPadding +20;
                }
                else
                    this.Height = borderSize * 2 + titleHeight + Spectrum1.Height + topPadding + bottomPadding;

                form2.GroupFlip.Enabled = true;
            }
            else        // H -> V
            {
                if (!isVHChange || inInit)　   // from other event
                {
                    this.Width = (borderSize + leftPadding) * 2 + Spectrum1.Width;
                    this.Height = (borderSize + Spectrum1.Height + verticalSpacing) * 2 + topPadding + titleHeight;
                }
                else    // Vertical button pushed (H->V)
                {
                    Spectrum1.Width = Spectrum2.Width = this.Size.Width - borderSize * 2 - leftPadding;
                    Spectrum2.Width = Spectrum1.Width;
                    
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height * 2 + verticalSpacing + bottomPadding;
                }

                Spectrum2.Left = Spectrum1.Left;
                Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                spectrumWidthScale = (float)Spectrum1.Width / baseSpectrumWidth * (maxNumberOfBar / numberOfBar);
                if (form2.CheckBoxHideFreq.Checked)
                {
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height * 2 + verticalSpacing;
                }

                form2.GroupFlip.Enabled = false;
            }
            
            if (form2.RadioNoFlip.Checked)
            {
                flipSide = 0;
                if (!inInit && !form2.CheckBoxHideFreq.Checked)
                {
                    if (!form2.RadioVertical.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);
                }
            }
            else if (form2.RadioLeftFlip.Checked)
            {
                flipSide = 1;   // Left
                if (!inInit && !form2.CheckBoxHideFreq.Checked)
                {
                    if (!form2.RadioVertical.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);
                }
            }
            else
            {
                flipSide = 2;   // Right
                if (!inInit && !form2.CheckBoxHideFreq.Checked)
                {
                    if (!form2.RadioVertical.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: true);
                }
            }
            if (form2.CheckBoxHideFreq.Checked)
            {
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Right[i].Visible = false;
            }
        }*/

        private void SetSpectrumLayout(int formW, int formH)
        {
            // 値は返さないが、SpectrumやLabelの配置や大きさを決めるのが目的。
            // Spectrumのサイズ・配置は全てココで決めること。
            // ラベルもここからLocateFrequencyLabelへ飛ばすこと。
            // フォームのサイズからSpectrumのサイズを求めること。
            // formの大きさは直接触らない。(SizeChangeが呼ばれてしまう)
            // 念のため、WidthもHeightも設定する
            // 
            // SizeChangedからも呼ばれる
            // V_H_Changedからも呼ばれる formのVHの形の違いはV_H_hangeで終わらせておく?
            // (変更前の形が必要で変更が有ったことを知ってる必要があるから)
            //
            // ラベルを描くときに影響する要因は
            // ラベルの要・不要
            // ラベルはVの場合、下だけ描く
            // Flipを考慮する
            //
            // formH,formVは使い捨てなのでローカルにコピーしなくて良い

            inLayout = true;    // 途中で解除しない事。

            if (!form2.HideFreqCheckBox.Checked)    // ラベルが必要なレイアウト
            {
                if (form2.HorizontalRadio.Checked)              // Hレイアウト:ラベル必要
                {
                    Spectrum1.Width = Spectrum2.Width = (formW - borderSize * 2 - leftPadding * 2) / 2; // Widthはラベル有り無しで違いは無い
                    Spectrum1.Height = Spectrum2.Height = formH - borderSize * 2 - titleHeight - topPadding - labelPadding - freqLabel_Left[0].Height - bottomPadding;
                    Spectrum1.Top = Spectrum2.Top = topPadding;          
                    Spectrum1.Left = leftPadding;                         
                    Spectrum2.Left = Spectrum1.Right;                         

                    // HレイアウトのみL Ch.のラベルが必要
                    LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: form2.LeftFlipRadio.Checked);
                }
                else                                            // Vレイアウト:ラベル必要
                {
                    Spectrum1.Width = Spectrum2.Width = formW - borderSize * 2 - leftPadding * 2;       // Widthはラベル有り無しで違いは無い
                    Spectrum1.Height = Spectrum2.Height = (formH - borderSize * 2 - titleHeight - topPadding - verticalSpacing - labelPadding - freqLabel_Left[0].Height - bottomPadding) / 2;
                    Spectrum1.Top = topPadding;
                    Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                    Spectrum1.Left = Spectrum2.Left = leftPadding;

                }

                // ラベル有りVHで共通の処理
                // Freq label LocateFrequencyLabelへ飛ばす所。
                // Vの場合、Leftのラベルは要らない。
                // Flipも考慮すること

                // RightのラベルはVHレイアウト共通
                LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: form2.RightFlipRadio.Checked);


            }
            else        // ラベル不要のレイアウト
            {
                if (form2.HorizontalRadio.Checked)              // Hレイアウト:ラベル不要
                {
                    Spectrum1.Width = Spectrum2.Width = (formW - borderSize * 2 - leftPadding * 2) / 2; // Widthはラベル有り無しで違いは無い
                    Spectrum1.Height = Spectrum2.Height = formH - borderSize * 2 - titleHeight - topPadding;
                    Spectrum1.Top = Spectrum2.Top = topPadding;
                    Spectrum1.Left = leftPadding;
                    Spectrum2.Left = Spectrum1.Right + 1;

                }
                else                                            // Vレイアウト:ラベル不要
                {
                    Spectrum1.Width = Spectrum2.Width = formW - borderSize * 2 - leftPadding * 2;       // Widthはラベル有り無しで違いは無い
                    Spectrum1.Height = Spectrum2.Height = (formH - borderSize * 2 - titleHeight - topPadding - verticalSpacing) / 2;
                    Spectrum1.Top = topPadding;
                    Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                    Spectrum1.Left = Spectrum2.Left = leftPadding;

                }

                // ラベル無しVHで共通の処理
                // ラベルのVisible=falseはココで。
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Right[i].Visible = false;
            }

            inLayout = false;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (!inLayout) SetSpectrumLayout(this.Width, this.Height);
            // try to keep just this amap. receiving event is its only role
            // also this will be called from V/H layout change because size of form will change
        }

        private void Form2_V_H_RadioCheckChanged(object sender, EventArgs e)            // V/H dual use
        {
            // 縦横のformのサイズ変更とfreq. labelの描画だけココでする
            // ＝formの形をキメてSetLayoutへ飛ばす? -> 飛ばさない。Spectrumのサイズが変わらないように整合するのが大変。
            // レイアウトを変えるとformのWidthもHeightも変わる(sizeChangedには飛ばさない)
            // do not change size of Spectrums
            // so don't need to call SetLayou, re-draw labels here.
            // do not change form location here
            // no difference in actual form size by labels exist or not (Height of Spectrum must be already changed)
            inLayout = true;
            if (form2.HorizontalRadio.Checked)  // change to H Layout
            {
                this.Width = (borderSize + leftPadding + Spectrum1.Width) * 2;          // -1 is for overlap on left and right 
                if (form2.HideFreqCheckBox.Checked)     // Height "calculation" is different by labels exist or not, no difference in actual form size
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height;
                else
                    this.Height = borderSize * 2 + titleHeight + topPadding + Spectrum1.Height + labelPadding + freqLabel_Left[0].Height + bottomPadding;

                // only Spectrum2(R Ch.) moves, do not change their size
                Spectrum2.Top = Spectrum1.Top;
                Spectrum2.Left = Spectrum1.Right;

                // only H layout has L Ch.'s label
                if (!form2.HideFreqCheckBox.Checked) LocateFrequencyLabel(Spectrum1, freqLabel_Left, form2.LeftFlipRadio.Checked);

                form2.GroupFlip.Enabled = true;
            }
            else                                // change to V Layout
            {
                this.Width = (borderSize + leftPadding) * 2 + Spectrum1.Width;
                if (form2.HideFreqCheckBox.Checked)     // Height "calculation" is different by labels exist or not, no difference in actual form size
                    this.Height = (borderSize + Spectrum1.Height) * 2 + titleHeight + topPadding + verticalSpacing;
                else
                    this.Height = (borderSize + Spectrum1.Height) * 2 + titleHeight + topPadding + verticalSpacing + labelPadding + freqLabel_Left[0].Height + bottomPadding;

                // only Spectrum2(R Ch.) moves, do not change their size
                Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                Spectrum2.Left = Spectrum1.Left;

                // no L Ch. label in V layout
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;

                form2.GroupFlip.Enabled = false;
            }

            // do not call SetSpectrumLayout, so draw label here
            // only R Ch. labels move
            // right form the start, only H layout has L Ch.'s label
            LocateFrequencyLabel(Spectrum2, freqLabel_Right, form2.RightFlipRadio.Checked);

            if (form2.VerticalRadio.Checked &&      // shift Setting dialog
                form2.Top < this.Bottom &&
                form2.Right > this.Left &&
                form2.Left < this.Right &&
                this.Bottom < Screen.FromControl(this).Bounds.Height - 50) form2.Top = this.Bottom + 30;
            else if (form2.HorizontalRadio.Checked &&
                form2.Top < this.Bottom &&
                form2.Left < this.Right &&
                form2.Right > this.Left &&
                this.Bottom < Screen.FromControl(this).Bounds.Height - 50) form2.Top = this.Bottom + 30;

            inLayout = false;
        }

        private void Form2_HideFreqCheckBoxCheckChanged(object sender, EventArgs e)
        {
            // try to keep just this amap. receiving event is its only role
            SetSpectrumLayout(this.Width, this.Height);
        }

        /*private void Form1_SizeChanged_Old(object sender, EventArgs e)
        {       // setLayoutを呼ぶだけにすること
            bool isLeftFlip = false;
            bool isRightFlip = false;
            if (!isVHChange)
            {
                if (flipSide == 1) isLeftFlip = true;
                else if (flipSide == 2) isRightFlip = true;

                if (form2.RadioHorizontal.Checked)      // Horizontal Layout
                {
                    Spectrum1.Width = Spectrum2.Width = (this.Width - borderSize * 2 - leftPadding) / 2;
                    Spectrum1.Height = Spectrum2.Height = this.Height - borderSize * 2 - topPadding - bottomPadding - titleHeight;
                    Spectrum2.Left = Spectrum1.Right;
                }
                else                                    // vertical Layout
                {
                    Spectrum1.Width = Spectrum2.Width = this.Width - borderSize * 2 - leftPadding;
                    Spectrum1.Height = Spectrum2.Height = (this.Height - (borderSize + topPadding) * 2 - verticalSpacing - bottomPadding - titleHeight) / 2;
                    Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                }
                spectrumWidthScale = (float)Spectrum1.Width / baseSpectrumWidth * (maxNumberOfBar / numberOfBar);

                Spectrum1.Update();
                Spectrum2.Update();
            }
            if (!inInit)
            {
                if (!form2.CheckBoxHideFreq.Checked)
                {
                    if (!form2.RadioVertical.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, isLeftFlip);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isRightFlip);
                }

                //if (isVHChange) isVHChange = false;
            }
        }*/

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
                ratio = (float)(0xff / /*canvas[0].Height*/ baseSpectrumHeight) * (10f - sensitivity);
                //label1.Text = sensitivity.ToString("0.0");
            }
        }

        private void Form2_PeakholdDecayTimeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            peakHoldDecayCycle = 20 * numberOfBar / channel / form2.PeakholdDecayTimeTrackBar.Value;      // スピードと値の増減方向が合うように逆数にする
            // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)
        }

        private void Form2_NumberOfBarComboBox_SelectedItemChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                //isVHChange = true;
                numberOfBar = Convert.ToInt16(form2.NumberOfBarComboBox.SelectedItem);
                counterCycle = (int)(peakHoldTimeMsec / analyzer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar / 16.0));    // ホールドタイムはバンド数に影響をうける
                peakHoldDecayCycle = 20 * numberOfBar / channel / form2.PeakholdDecayTimeTrackBar.Value;      // スピードと値の増減方向が合うように逆数にする
                                                                                              // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)

                if (form2.RainbowRadio.Checked)     // need for color position adjust
                {
                    endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;        // Horizontal
                    endPointY = 0;
                    brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                    {
                        InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
                    };
                    myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
                }

                //SetSpectrumLayout(this.Width, this.Height);
                canvasWidth = barLeftPadding + numberOfBar * ((int)penWidth + barSpacing) - (barLeftPadding - barSpacing);  // 拡大用にBar数ごとのSpectrumサイズを計算
                if (!inInit) ClearSpectrum(sender, EventArgs.Empty);
                /*{   // これでは実行されてないのでは 実は必要ない？
                    spectrumWidthScale = (float)Spectrum1.Width / baseSpectrumWidth * (maxNumberOfBar / numberOfBar);
                    if (!form2.CheckBoxHideFreq.Checked)
                    {
                        if (!form2.RadioVertical.Checked)
                            LocateFrequencyLabel(Spectrum1, freqLabel_Left, form2.RadioLeftFlip.Checked);
                        else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                        LocateFrequencyLabel(Spectrum2, freqLabel_Right, form2.RadioRightFlip.Checked);
                    }
                }*/
                //isVHChange = false;

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
                {
                    InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
                };
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
                {
                    InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
                };
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
                {
                    InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
                };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            }
        }

        private void Form2_RainbowRadio_CheckChanged(object sender, EventArgs e)
        {
            if (form2.RainbowRadio.Checked && !inInit)
            {
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];       // need for color position adjust
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;        // Horizontal
                endPointY = 0;
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                {
                    InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
                };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            }
        }

        private void Form2_SSaverCheckboxCheckedChanged(object sender, EventArgs e)
        {
            //if (inInit == false) preventSSaver = !preventSSaver;
        }

        private void Form2_PeakholdCheckboxCheckedChanged(object sender, EventArgs e)
        {
            //if (inInit == false) peakhold = !peakhold;
            if (form2.PeakholdCheckBox.Checked)
                form2.PeakholdTimeComboBox.Enabled = form2.LabelPeakhold.Enabled = form2.LabelMsec.Enabled = true;
            else
                form2.PeakholdTimeComboBox.Enabled = form2.LabelPeakhold.Enabled = form2.LabelMsec.Enabled = false;
        }

        private void Form2_AlwaysOnTopCheckboxCheckChanged(object sender, EventArgs e)
        {
            if (inInit == false) TopMost = !TopMost;
        }

        /*private void Form2_RadioV_H_CheckChanged_Old(object sender, EventArgs e)            // V/H dual use
        {
            if (!inInit)
            {
                isVHChange = true;
                flipSide = 0;
                SetLayout_Old();        // Actuaal proccess is in SetLayout
            }
            if (form2.RadioVertical.Checked &&      // shift Setting dialog
                form2.Top < this.Bottom &&
                form2.Right >  this.Left &&
                this.Bottom < Screen.FromControl(this).Bounds.Height - 50) form2.Top = this.Bottom + 30;

            isVHChange = false;
        }*/

        private void Form2_StereoRadioCheckChanged(object sender, EventArgs e)
        {
            ;
        }

        private void Form2_FlipSideRadioCheckChanged(object sender, EventArgs e)                // L/R dual use
        {
            if (!inInit)
            {
                if (form2.NoFlipRadio.Checked)
                {
                    flipSide = 0;
                    if (!form2.VerticalRadio.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);
                }
                else if (form2.LeftFlipRadio.Checked)
                {
                    flipSide = 1;   // Left
                    if (!form2.VerticalRadio.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, true);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: false);
                }
                else
                {
                    flipSide = 2;   // Right
                    if (!form2.VerticalRadio.Checked)
                        LocateFrequencyLabel(Spectrum1, freqLabel_Left, false);
                    else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                    LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: true);
                }

            }
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

        private void Form2_HideTitleCheckBoxCheckChanged(object sender, EventArgs e)
        {
            if (form2.HideTitleCheckBox.Checked == true)        // labelある時おかしい
            {
                borderSize = 0;
                titleHeight = 0;
                /*this.ControlBox = false;
                this.Text = String.Empty;               // this.Height変わる
                */
                this.FormBorderStyle = FormBorderStyle.None;
                SetSpectrumLayout(this.Width, this.Height);
                form2.ExitAppButton.Enabled = true;
            }
            else
            {
                /*this.ControlBox = true;
                this.Text = ProductName;
                */
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.ShowIcon = true;
                borderSize = (this.Width - this.ClientSize.Width) / 2;
                titleHeight = this.Height - this.ClientSize.Height - borderSize * 2; 
                SetSpectrumLayout(this.Width, this.Height);
                form2.ExitAppButton.Enabled=false;
            }
        }

        private void Form2_ExitAppButtonClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*private void Form2_CheckBoxHideFreqCheckChanged_Old(object sender, EventArgs e)
        {
            if (form2.CheckBoxHideFreq.Checked)
            {
                for (int i = 0; i < maxNumberOfBar; i++)
                {
                    freqLabel_Left[i].Visible = false;
                    freqLabel_Right[i].Visible = false;
                }

                if (form2.RadioHorizontal.Checked)      // Horizontal &  show --> Hyde
                {
                    Spectrum1.Height += freqLabel_Left[0].Height + bottomPadding - labelPadding - borderSize;
                    Spectrum2.Height = Spectrum1.Height;
                }
                else                                    // Vertical &  show --> Hyde
                {
                    Spectrum1.Height += (freqLabel_Left[0].Height + bottomPadding + labelPadding) / 2;
                    Spectrum2.Height = Spectrum1.Height;
                    Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                }
            }
            else
            {
                if (form2.RadioHorizontal.Checked)      // Horizontal &  Hyde --> show
                {
                    Spectrum1.Height -= freqLabel_Left[0].Height + bottomPadding - labelPadding - borderSize;
                    Spectrum2.Height = Spectrum1.Height;
                }
                else                                    // Vertical &   Hyde --> show
                {
                    Spectrum1.Height -= (freqLabel_Left[0].Height + bottomPadding + labelPadding) / 2;
                    Spectrum2.Height = Spectrum1.Height;
                    Spectrum2.Top = Spectrum1.Bottom + verticalSpacing;
                }

                spectrumWidthScale = (float)Spectrum1.Width / baseSpectrumWidth * (maxNumberOfBar / numberOfBar);
                if (!form2.RadioVertical.Checked)
                    LocateFrequencyLabel(Spectrum1, freqLabel_Left, isFlip: form2.RadioLeftFlip.Checked);
                else for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
                LocateFrequencyLabel(Spectrum2, freqLabel_Right, isFlip: form2.RadioRightFlip.Checked);
            }
        }*/

        private void ReceiveSpectrumData(object sender, EventArgs e)
        {
            if (inInit) { return; }

            int bounds;
            int isRight = 0; // for interreave

            var g = new Graphics[2];
            for (int i = 0; i < channel; i++) g[i] = Graphics.FromImage(canvas[i]);

            for (int i = 0; i < numberOfBar * channel; i++)     // analizerから受け取る_spectumdataは Max16*2=32バイト L,R,L,R,...
            {
                if (channel > 1) isRight = i % 2;

                var posX = barLeftPadding + (i - isRight) / channel * (penWidth + barSpacing);             // 横方向の位置
                var powY = (int)(analyzer._spectrumdata[i] / ratio);                                    // 描画位置を計算
                g[isRight].DrawLine(bgPen, posX, canvas[0].Height, posX, 0);                            // 背景を下から上に向かって描く
                powY = ((powY - dash) / (dash + dash) + 1) * (dash + dash);
                if (powY > 7)
                    g[isRight].DrawLine(myPen, posX, canvas[0].Height, posX, canvas[0].Height - powY);

                // Peak Hold
                if (form2.PeakholdCheckBox.Checked)
                {
                    if (peakValue[i] <= powY && powY != 0)
                    {
                        peakValue[i] = powY;        // ピーク更新するが描画は不要
                    }
                    // 以下powYがpeak以下の時はpeakを描く
                    else if (peakCounter > counterCycle * 0.75      // 減衰させるかどうかを決定 0.75はサイクルの3/4から減衰させるため
                        && peakCounter % peakHoldDecayCycle == 0)                                     // 減衰スピード
                    {
                        for (int j = 0; j < peakValue.Length; j++) peakValue[j] -= (dash + dash);   // Peakを1レベル減衰させる
                        if (peakValue[i] < powY) peakValue[i] = powY;                               //減衰しすぎたら更新
                        /*if (peakValue[i] < powY)
                        {
                            peakValue[i] = powY;                              //減衰しすぎたら更新
                        }*/
                        else if (powY == 0 && peakValue[i] > 0 /*&& peakCounter % peakHoldDecayCycle == 0*/)
                        {//到達しない理由は？
                            for (int j = 0; j < peakValue.Length; j++) peakValue[j] -= (dash + dash);
                        }
                    }

                    bounds = ((peakValue[i] - dash) / (dash + dash) + 1) * (dash + dash); // 描画境界を計算
                                                                                          //((int)((x-3)/6)+1)*6
                    if (bounds > 7) // Peak描画本体
                        g[isRight].DrawLine(myPen, posX, canvas[0].Height - bounds, posX, canvas[0].Height - bounds - dash);   // boundsから上に向かってひと目盛だけ描く
                }
                peakCounter++;

                LabelCycle.Text = peakCounter.ToString("0000") + " / " + counterCycle.ToString();

                if (peakCounter >= counterCycle)      // peakhold=falseでもスクリーンセーバー制御に使っているのでカウンターだけは回しておく
                {
                    peakCounter = 0;   // 規定サイクル回ったらカウンターをリセット
                    for (int j = 0; j < numberOfBar * channel; j++) peakValue[j] = (int)(analyzer._spectrumdata[j] / ratio);     //Peak値もリセット
                    displayOffCounter++;
                }
                if (displayOffCounter > 5)  // counterCycle(2000) * 25milSec. * 5 = 250Sec. = 4.17 min.
                {
                    if (form2.SSaverCheckBox.Checked)
                    {
                        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                    }
                    displayOffCounter = 0;
                }
            }

            for (int i = 0; i < channel; i++) g[i].Dispose();

            switch (flipSide)
            {
                default:
                    canvas[1].RotateFlip(RotateFlipType.RotateNoneFlipNone);        // Left, May be there is no canvas[1] in "MONO"
                    canvas[0].RotateFlip(RotateFlipType.RotateNoneFlipNone);        // Right
                    break;
                case 0:     // None
                    canvas[1].RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    canvas[0].RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;
                case 1:     // Right
                    canvas[1].RotateFlip(RotateFlipType.RotateNoneFlipX);
                    canvas[0].RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;
                case 2:     // Left
                    canvas[1].RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    canvas[0].RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
            }

            Spectrum2.Image = canvas[0];        // Why Spectrum2 ?

            if (canvas[1] != null)
                Spectrum1.Image = canvas[1];
            else
                Spectrum1.Image = Spectrum2.Image;
        }

        private void NumberOfChannelsChanged(object sender, EventArgs e)
        {
            inInit = true;
            channel = analyzer._channel;
            peakValue = new int[maxNumberOfBar * channel];
            counterCycle = (int)(peakHoldTimeMsec / analyzer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar * channel / 16.0));
            for (int i = 0; i < channel; i++) canvas[i] = new Bitmap(Spectrum1.Width, Spectrum1.Height);
            if (channel < 2)        //あとで外関数に出す
            {
                Spectrum1.Visible = false;
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
            }
            peakHoldDecayCycle = 20 * numberOfBar / channel / form2.PeakholdDecayTimeTrackBar.Value;      // スピードと値の増減方向が合うように逆数にする

            inInit = false;
        }

        private void ClearSpectrum(object sender, EventArgs e)
        {
            // SetLayoutしてからClearSpectrum
            // SetLayout内でspectrumとcanvasのサイズを調整済み

            for (int i = 0; i < canvas.Length; i++) canvas[i] = new Bitmap(canvasWidth, baseSpectrumHeight);    // numberOfBarが変わったら新しいサイズが必要。

            var g = Graphics.FromImage(canvas[0]);
            for (int j = 0; j < numberOfBar; j++)
            {
                var posX = barLeftPadding + j * 40;        // pen.Width=30 + BarSpacing=10
                g.DrawLine(bgPen, posX, canvas[0].Height, posX, 0);
            }

            g.Dispose();
            Spectrum1.Image = canvas[0];
            Spectrum2.Image = canvas[0];
            Spectrum1.Update();
            Spectrum2.Update();
            //label1.Text = canvas[0].Width.ToString();

            if (form2.RainbowRadio.Checked == true)
                endPointX = barLeftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;
        }

        private void LocateFrequencyLabel(PictureBox spectrum, Label[] freqLabel, bool isFlip)
        {
            SuspendLayout();

            float labelFontSize = baseLabelFontSize * spectrumWidthScale;
            labelFontSize = labelFontSize <= 10f ? labelFontSize : 10f;
            labelFontSize = labelFontSize >= 8f ? labelFontSize : 8f;

            for (int i = 0; i < maxNumberOfBar; i++)
            {
                string labelText;
                
                int freqvalues = (int)(Math.Pow(2, i * 10.0 / (numberOfBar - 1) + 4.35));                       //  4.35 is magic number...
                if (freqvalues > 1000)
                    labelText = (freqvalues / 1000).ToString("0") + "KHz";
                else labelText = (freqvalues / 10 * 10).ToString("0") + "Hz";    // round -1

                int j = flipSide > 0 && isFlip ? numberOfBar - 1 - i : i;
                if (i < numberOfBar)        // Only Bar exists
                {
                    freqLabel[i].Font = new Font("Arial", labelFontSize);
                    freqLabel[j].Text = labelText;
                    freqLabel[i].AutoSize = true;
                    int labelPos = (int)(spectrum.Left + (float)(25 + i * 40) / (float)(10 + numberOfBar * 40) * spectrum.Width) - freqLabel[i].Width / 2;
                    freqLabel[i].Left = labelPos;
                    freqLabel[i].Name = "freqlabel" + (j + 1).ToString();
                    freqLabel[i].Top = spectrum.Bottom + labelPadding;
                    freqLabel[i].TextAlign = ContentAlignment.TopCenter;
                    freqLabel[i].Visible = true;
                    freqLabel[i].BackColor = Color.Transparent;
                }
                else    // Bar not exists
                {
                    freqLabel[i].Text = "";
                    freqLabel[i].Visible = false;
                }
            }
            if (((!isFlip && freqLabel[14].Right > freqLabel[15].Left) ||
                (isFlip && freqLabel[1].Left < freqLabel[0].Right)) && freqLabel[15].Left != 0)
            {
                for (int i = 1; i < maxNumberOfBar; i += 2)
                {
                    freqLabel[i].Visible = false;
                }
                freqLabel[numberOfBar - 2].Visible = false;
                freqLabel[numberOfBar - 1].Visible = true;
                freqLabel[numberOfBar - 1].Left -= freqLabel[numberOfBar - 1].Width / 5;
            }
            this.Controls.AddRange(freqLabel);
            ResumeLayout(false);
        }

        private void LoadConfigParams()
        {
            //config reader
            var configFileName = @".\" + ProductName + @".conf";
            FileStream fs;
            if (!System.IO.File.Exists(configFileName))
            {
                fs = new FileStream(configFileName, FileMode.CreateNew); //, FileAccess., FileShare.ReadWrite);
                fs.Dispose();
            }

            var confReader = new ConfigReader(configFileName);

            numberOfBar = confReader.GetValue("numberOfBar", 16);
            deviceNumber = confReader.GetValue("deviceNumber", -1);
            form2.RadioMono.Checked = confReader.GetValue("mono", false);
            form2.RadioStereo.Checked = confReader.GetValue("stereo", true);

            leftPadding = confReader.GetValue("horizontalPadding", 0);
            topPadding = confReader.GetValue("verticalPadding", 0);
            verticalSpacing = confReader.GetValue("horizontalSpacing", 8);
            barLeftPadding = confReader.GetValue("barLeftPadding", 25);
            bottomPadding = confReader.GetValue("bottompadding", 12);
            labelPadding = confReader.GetValue("labelPadding", 2);

            peakHoldTimeMsec = confReader.GetValue("peakHoldTimeMsec", 500);
            form2.SensitivityTrackBar.Value = confReader.GetValue("form2TrackBar1Value", 80);
            form2.PeakholdDecayTimeTrackBar.Value = confReader.GetValue("form2TrackBar2Value", 10);
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

            /*penWidth = confReader.GetValue("penWidth",30f);
            labelPadding = confReader.GetValue("labelPadding", 6);
            barSpacing = confReader.GetValue("barSpacing", 10);
            baseLabelFontSize = confReader.GetValue("baseLabelFontSize", 6f);*/

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

            form1Top = confReader.GetValue("form1Top", 130);
            form1Left = confReader.GetValue("form1Left", 130);
            form1Width = confReader.GetValue("form1Width", 1318);            // When Vertical. Otherwise, resize automatically later
            form1Height = confReader.GetValue("form1Height", 256);
            form2Top = confReader.GetValue("form2Top", form1Top + form1Height + topPadding);
            form2Left = confReader.GetValue("form2Left", form1Left);
            maximized = confReader.GetValue("maximized", false);

            UNICODE = confReader.GetValue("unicode", true);
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
            confWriter.AddValue<bool>("maximized", this.WindowState == FormWindowState.Maximized);

            confWriter.AddValue("horizontalPadding", leftPadding);
            confWriter.AddValue("verticalPadding", topPadding);
            confWriter.AddValue("horizontalSpacing", verticalSpacing);
            confWriter.AddValue("barLeftPadding", barLeftPadding);
            confWriter.AddValue("bottomPadding", bottomPadding);
            confWriter.AddValue("labelPadding", labelPadding);

            confWriter.AddValue("peakHoldTimeMsec", peakHoldTimeMsec);
            confWriter.AddValue("form2TrackBar1Value", form2.SensitivityTrackBar.Value);      // sensitivity
            confWriter.AddValue("form2TrackBar2Value", form2.PeakholdDecayTimeTrackBar.Value);      // peakhold decay time

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


            confWriter.AddValue("mono", form2.RadioMono.Checked);
            confWriter.AddValue("stereo", form2.RadioStereo.Checked);


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

            if (UNICODE == false) confWriter.AddValue("unicode", false);

            confWriter.AddValue("spectrum2Width", Spectrum2.Width);         // save only for debug
            confWriter.AddValue("spectrum2Height", Spectrum2.Height);
            confWriter.AddValue("spectrum2Top", Spectrum2.Top);
            confWriter.AddValue("spectrum2Left", Spectrum2.Height);

            try
            {
                confWriter.Save(@".\" + ProductName + @".conf");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Unicode() { return UNICODE; }
        
        private void LabelCycle_Click(object sender, EventArgs e)
        {
            /* テスト用メソッド
            int channel = 2;

            int[] fft = new int[1024];
            int x, y;
            int b0 = 0;
            for (x = 0; x < 8; x++)
            {
                int peak = 0;                               // peakは各バンド内の最大値
                int b1 = (int)Math.Pow(2, x * 10.0 / (8 - 1));
                if (b1 > 1023) b1 = 1023;

                if (b1 <= b0) b1 = b0 + 1;
                for (; b0 < b1; b0+=channel)
                {
                    if(peak < fft[1+b0]) peak = fft[1+b0];  // b1まで(各バンド内)の間で最大値を探す
                }
                y = (int)(Math.Sqrt(peak) * 3 * 255 - 4);
                if (y > 255) y = 255;
                if (y < 0) y = 0;

                // now y is ansor.

            }*/

            //label1.Text = counterCycle.ToString();
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            SaveConfigParams();
        }
    }
}