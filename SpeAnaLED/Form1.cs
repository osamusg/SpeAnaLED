using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
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
        
        private readonly Analyzer analizer;
        private readonly Form2 form2 = null;
        private int borderSize;
        private int titleHeight;
        private readonly Bitmap[] canvas = new Bitmap[2];
        private Color[] colors;
        private float[] positions;
        private Pen myPen;
        private Pen bgPen;
        private LinearGradientBrush brush;
        private Label[] freqLabel_Left;
        private Label[] freqLabel_Right;
        private int counterCycle;
        private int cycleMultiplyer;
        private readonly int[] peakValue;
        private int peakCounter = 0;
        //private readonly int[,] peakTiming = new int[2,16];
        private readonly int channel;
        public int numberOfBar;
        private const int maxNumberOfBar = 16;
        private float ratio;
        private int dash;
        private int displayOffCounter;
        private bool inInit = false;
        private const int baseWidth = 650;
        private const int baseHeght = 129;
        private int canvasWidth;


        // parameters (set defaults)
        private int endPointX = 0;              // グラデーションのデフォルト描画方向 上から下
        private int endPointY;                  // PictureBoxの大きさにより可変?
        private float penWidth = 30f;           // 計算で出すもの?
        private int labelPadding = 6;           // 計算で出すもの?
        private int barSpacing = 10;            // 計算で出すもの?
        private float labelFontSize = 9f;       // 計算で出すもの?
        private int leftPadding;
        private int horizontalPadding;
        private int verticalPadding;
        private int horizontalSpacing;
        private float spectrumWidthScale;
        private float spectrumHeightScale;


        private float sensibility;
        private int peakHoldTimeMsec;
        private int peakHoldDecayCycle = 10;    // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)
        private bool classicChecked, prisumChecked, simpleChecked, rainbowChecked;
        //private bool leftSideFlip;
        private int flipSide;                   // 0: none 1:left(Center Low) 2:right(Center Hi)

        // 念のためデフォルト値はセットしておく 配列の長さを確定するために必要
        private Color[] classicColors =
            {  Color.FromArgb(255,0,0),     //  0 100 50  red
               Color.FromArgb(255,255,0),   // 60 100 50  yellow
               Color.FromArgb(0,128,0),};   //120 100 25  green
        private float[] classicPositions = { 0.0f, 0.3f, 1.0f };

        private Color[] prisumColors =
            {   Color.FromArgb(255,0,0),    //   0  red
                Color.FromArgb(255,255,0),  //  60  yellow
                Color.FromArgb(0,255,0),    // 120  lime
                Color.FromArgb(0,255,255),  // 180  cyan
                Color.FromArgb(0,0,255),};  // 240  blue
        private float[][] prisumPositions = new float[17][];        // maxNumberOfBar+1 jagg array for easy to see

        private Color[] simpleColors = { Color.LightSkyBlue, Color.LightSkyBlue};
        private float[] simplePositions = { 0.0f, 1.0f };
        
        public Form1()
        {
            InitializeComponent();

            inInit = true;
            form2 = new Form2(); 
            analizer = new Analyzer(form2.devicelist, form2.EnumerateButton, form2.ComboBox1)
            {
                Enable = true,
                DisplayEnable = true
            };
            
            // Event handler for option form (subscribe)
            form2.TrackBar1.ValueChanged += Form2_TrackBar1_ValueChanged;
            form2.TrackBar2.ValueChanged += Form2_TrackBar2_ValueChanged;
            form2.RadioClassic.CheckedChanged += Form2_RadioClassic_CheckChanged;
            form2.RadioPrisum.CheckedChanged += Form2_RadioPrisum_CheckChanged;
            form2.RadioSimple.CheckedChanged += Form2_RadioSimple_CheckChanged;
            form2.RadioRainbow.CheckedChanged += Form2_RadioRainbow_CheckChanged;
            form2.ComboBox1.SelectedIndexChanged += Form2_ComboBox1_SelectedItemChanged;
            form2.ComboBox2.SelectedIndexChanged += Form2_ComboBox2_SelectedItemChanged;
            form2.SSaverCheckBox.CheckedChanged += Form2_SSaverCheckboxCheckedChanged;
            form2.PeakholdCheckBox.CheckedChanged += Form2_PeakholdCheckboxCheckedChanged;
            form2.AlwaysOnTopCheckBox.CheckedChanged += Form2_AlwaysOnTopCheckboxCheckChanged;
            form2.RadioVertical.CheckedChanged += Form2_RadioV_H_CheckChanged;      // V・H兼用
            form2.RadioHorizontal.CheckedChanged += Form2_RadioV_H_CheckChanged;    // V・H兼用
            //form2.CheckBoxFlip.CheckedChanged += Form2_CheckBoxFlipCheckChanged;
            form2.ShowCyclCheckBox.CheckedChanged += Form2_ShowCycleCheckChanged;
            form2.RadioRightFlip.CheckedChanged += Form2_RadioFlipSideCheckChanged; // L・R兼用
            form2.RadioLeftFlip.CheckedChanged += Form2_RadioFlipSideCheckChanged;  // L・R兼用
            form2.RadioNoFlip.CheckedChanged += Form2_RadioFlipSideCheckChanged;

            // Other Event handler (subscribe)
            analizer.SpectrumChanged += ReceiveSpectrumData;
            Application.ApplicationExit += Application_ApplicationExit;

            // 以下内部の計算で出すもの
                        borderSize = (this.Width - this.ClientSize.Width) / 2;
            titleHeight = this.Height - this.ClientSize.Height - borderSize * 2;
            endPointY = spectrum1.Height;
            numberOfBar = analizer._lines;
            channel = analizer._channel;
            peakValue = new int[maxNumberOfBar * channel];
            
            // I don't know why "* 20 (num..." is necessary. This is due to actual measurements.
            // If you edit this parameter, also edit "Form2_ComboBox1_SelectedItemChanged" and
            // "Form2_ComboBox2_SelectedItemChanged".
            cycleMultiplyer = 50;
            counterCycle = (int)(peakHoldTimeMsec / analizer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar * channel / 16.0));      // default hold time
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                }
                else
                {
                    MessageBox.Show("Opps! Config file seems something wrong...\r\n" +
                        "Use default parameters.",
                        "Config file error - " + ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                SetDefaultParams();
            }

            ////// Test Param
            spectrum1.Width += 240;
            spectrum1.Height += 768;
            spectrum2.Width = spectrum1.Width;
            spectrum2.Height = spectrum1.Height;
            /////
            
            spectrumWidthScale = spectrum1.Width / (float)baseWidth;
            spectrumHeightScale = spectrum1.Height / (float)baseHeght;

            //SetLayout();

            freqLabel_Left = new Label[maxNumberOfBar];
            freqLabel_Right = new Label[maxNumberOfBar];
            for (int i = 0; i < maxNumberOfBar; i++)
            {
                freqLabel_Left[i] = new Label();
                freqLabel_Right[i] = new Label();
            }
            
            for (int i = 0; i < channel; i++) canvas[i] = new Bitmap(spectrum1.Width,spectrum1.Height);
            
            bgPen = new Pen(Color.DimGray, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };

            if (form2.RadioNoFlip.Checked)
                flipSide = 0;   // No flip
            else if (form2.RadioLeftFlip.Checked)
                flipSide = 1;   // Left
            else
                flipSide = 2;   // Right

            SetLayout();
            ClearSpectrum(this, EventArgs.Empty);

            if (channel < 2)        //あとで外関数に出す
            {
                spectrum1.Visible = false;
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
            }

            dash = (int)penWidth / 10;

            // デフォルト(前回)カラー(pen)の事前準備
            colors = prisumColors;
            positions = prisumPositions[numberOfBar];

            brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
            {
                InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
            };
            myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };

            // form2 setting
            form2.ComboBox1.SelectedIndex = form2.ComboBox1.Items.IndexOf(numberOfBar.ToString());
            form2.ComboBox2.SelectedIndex = form2.ComboBox2.Items.IndexOf(peakHoldTimeMsec.ToString());
            form2.TextBox_Sensibility.Text = (form2.TrackBar1.Value / 10f).ToString("0.0");
            form2.TextBox_DecaySpeed.Text = form2.TrackBar2.Value.ToString();
            if (!form2.PeakholdCheckBox.Checked) form2.LabelPeakhold.Enabled = form2.LabelMsec.Enabled = form2.ComboBox2.Enabled = false;
            if (form2.ShowCyclCheckBox.Checked)
                label1.Visible = true;
            else
                label1.Visible = false;

            if ((form2.RadioPrisum.Checked = prisumChecked) == true)
            {
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];
                endPointX = 0;
                endPointY = canvas[0].Height;
            }
            else if ((form2.RadioClassic.Checked = classicChecked) == true)
            {
                colors = classicColors;
                positions = classicPositions;
                endPointX = 0;
                endPointY = canvas[0].Height;
            }
            else if ((form2.RadioSimple.Checked = simpleChecked) == true)
            {
                colors = simpleColors;
                positions = simplePositions;
                endPointX = 0;
                endPointY = canvas[0].Height;
            }
            else
            {
                form2.RadioRainbow.Checked = rainbowChecked;
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];       // need for color position adjust
                endPointX = leftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;        // Horizontal
                endPointY = 0;
            }

            brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
            {
                InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
            }; 
            myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };

            sensibility = form2.TrackBar1.Value / 10f;
            ratio = (float)(0xff / /*canvas[0].Height*/ baseHeght) * (10f - sensibility);
            TopMost = form2.AlwaysOnTopCheckBox.Checked;
            //if(leftSideFlip = form2.CheckBoxFlip.Checked) LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: true);        // flipの場合のみLeftだけ書き直す

            //ClearSpectrum(this, EventArgs.Empty);

            inInit = false;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void SetLayout()
        {
            // SetLayoutしてからClearSpectrum
            canvasWidth = leftPadding + numberOfBar * ((int)penWidth + barSpacing) - (leftPadding - barSpacing);
            spectrum1.Width = (int)(canvasWidth * spectrumWidthScale);
            spectrum2.Width = spectrum1.Width;
            // Heightは?

            if (form2.RadioHorizontal.Checked)
            {
                this.Width = (borderSize + horizontalPadding + spectrum1.Width) * 2;
                this.Height = borderSize * 2 + titleHeight + verticalPadding + spectrum1.Height + horizontalSpacing;
                spectrum2.Top = spectrum1.Top;
                spectrum2.Left = spectrum1.Right;
                
                form2.CheckBoxFlip.Enabled = true;
                form2.GroupFlip.Enabled = true;
            }
            else
            {
                this.Width = (borderSize + horizontalPadding) * 2 + spectrum1.Width;
                this.Height = (borderSize + spectrum1.Height + horizontalSpacing) * 2 + verticalPadding + titleHeight;
                spectrum2.Left = spectrum1.Left;
                spectrum2.Top = spectrum1.Bottom + horizontalSpacing;
             
                form2.CheckBoxFlip.Checked = false;
                form2.CheckBoxFlip.Enabled = false;
                form2.GroupFlip.Enabled = false;
            }
            //if (!inInit)
            //{
            if (form2.RadioNoFlip.Checked)
            {
                flipSide = 0;
                if (!inInit)
                {
                    LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: false);
                    LocateFrequencyLabel(spectrum2, freqLabel_Right, isFlip: false);
                }
            }
            else if (form2.RadioLeftFlip.Checked)
            {
                flipSide = 1;   // Left
                if (!inInit)
                {
                    LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: true);
                    LocateFrequencyLabel(spectrum2, freqLabel_Right, isFlip: false);
                }
            }
            else
            {
                flipSide = 2;   // Right
                if (!inInit)
                {
                    LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: false);
                    LocateFrequencyLabel(spectrum2, freqLabel_Right, isFlip: true);
                }
            }
            //}
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

        private void spectrum1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && form2.Visible == false)
                form2.Show(this);
        }

        private void spectrum2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && form2.Visible == false)
                form2.Show(this);
        }

        private void Form2_TrackBar1_ValueChanged(object sender, EventArgs e)           // 感度調整
        {
            if (!inInit)
            {
                sensibility = form2.TrackBar1.Value / 10f;
                ratio = (float)(0xff / /*canvas[0].Height*/ baseHeght) * (10f - sensibility);
                //label1.Text = sensibility.ToString("0.0");
            }
        }

        private void Form2_TrackBar2_ValueChanged(object sender, EventArgs e)           // 減衰スピード調整
        {
            peakHoldDecayCycle = 20 * numberOfBar / channel / form2.TrackBar2.Value;      // スピードと値の増減方向が合うように逆数にする
            // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)
        }

        private void Form2_ComboBox1_SelectedItemChanged(object sender, EventArgs e)    // バンド数変更
        {
            numberOfBar = Convert.ToInt16(form2.ComboBox1.SelectedItem);
            counterCycle = (int)(peakHoldTimeMsec / analizer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar / 16.0));    // ホールドタイムはバンド数に影響をうける
            Form2_TrackBar2_ValueChanged(sender, e);        // TrackBar2は変わっていないが同じ計算式なのでnumberOfBarの変更を処理

            if (form2.RadioRainbow.Checked)     // need for color position adjust
            {
                endPointX = leftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;        // Horizontal
                endPointY = 0;
                brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
                {
                    InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
                };
                myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            }

            SetLayout();    // SetLayoutしてからClearSpectrum
            if (!inInit) ClearSpectrum(sender, EventArgs.Empty);
        }

        private void Form2_ComboBox2_SelectedItemChanged(object sender, EventArgs e)    // ホールドタイム変更
        {
            peakHoldTimeMsec = Convert.ToInt16(form2.ComboBox2.SelectedItem);
            counterCycle = (int)(peakHoldTimeMsec / analizer._timer1.Interval.Milliseconds * cycleMultiplyer * (numberOfBar / 16.0));
        }
        
        private void Form2_RadioClassic_CheckChanged(object sender, EventArgs e)
        {
            if (form2.RadioClassic.Checked && !inInit)
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

        private void Form2_RadioPrisum_CheckChanged(object sender, EventArgs e)
        {
            if (form2.RadioPrisum.Checked && !inInit)
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

        private void Form2_RadioSimple_CheckChanged(object sender, EventArgs e)
        {
            if (form2.RadioSimple.Checked && !inInit)
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

        private void Form2_RadioRainbow_CheckChanged(object sender, EventArgs e)
        {
            if (form2.RadioRainbow.Checked && !inInit)
            {
                colors = prisumColors;
                positions = prisumPositions[numberOfBar];       // need for color position adjust
                endPointX = leftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;        // Horizontal
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
                form2.ComboBox2.Enabled = form2.LabelPeakhold.Enabled = form2.LabelMsec.Enabled = true;
            else
                form2.ComboBox2.Enabled = form2.LabelPeakhold.Enabled = form2.LabelMsec.Enabled = false;
        }

        private void Form2_AlwaysOnTopCheckboxCheckChanged(object sender, EventArgs e)
        {
            if (inInit == false) TopMost = !TopMost;
        }

        private void Form2_RadioV_H_CheckChanged(object sender, EventArgs e)            // V・H兼用
        {
            if (!inInit)
            {
                //leftSideFlip = !leftSideFlip;
                flipSide = 0;
                SetLayout();
            }
        }

        private void Form2_RadioMonoCheckChanged(object sender, EventArgs e)
        {
            ;
        }

        private void Form2_RadioStereoCheckChanged(object sender, EventArgs e)
        {
            ;
        }
        
        /*private void Form2_CheckBoxFlipCheckChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                leftSideFlip = !leftSideFlip;
                if (leftSideFlip)
                {
                    flipSide = 1;   // Flip Left
                    LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: true);
                }
                else
                {
                    flipSide = 0;
                    LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: false);
                }
                //if (leftSideFlip) canvas[1].RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
        }*/

        private void Form2_RadioFlipSideCheckChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                if (form2.RadioNoFlip.Checked)
                {
                    flipSide = 0;
                    LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: false);
                    LocateFrequencyLabel(spectrum2, freqLabel_Right, isFlip : false);
                }
                else if (form2.RadioLeftFlip.Checked)
                {
                    flipSide = 1;   // Left
                    LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: true);
                    LocateFrequencyLabel(spectrum2, freqLabel_Right, isFlip: false);
                }
                else
                {
                    flipSide = 2;   // Right
                    LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: false);
                    LocateFrequencyLabel(spectrum2, freqLabel_Right, isFlip: true);
                }
                
            }
        }

        private void Form2_ShowCycleCheckChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                if (form2.ShowCyclCheckBox.Checked)
                    label1.Visible = true;
                else
                    label1.Visible = false;
            }
        }
        
        private void ReceiveSpectrumData(object sender, EventArgs e)
        {
            if (inInit) { return; }

            int bounds;
            int isRight = 0; // interreave対応

            var g = new Graphics[2];
            for (int i = 0; i < channel; i++) g[i] = Graphics.FromImage(canvas[i]);

            for (int i = 0; i < numberOfBar * channel; i++)     // analizerから受け取る_spectumdataは Max16*2=32バイト L,R,L,R,...
            {
                if (channel > 1) isRight = i % 2;

                var posX = leftPadding + (i - isRight) / channel * (penWidth + barSpacing);             //横方向の位置
                var powY = (int)(analizer._spectrumdata[i] / ratio);                                    // 描画境界を計算
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
                    else
                    {
                        if (peakCounter > counterCycle * 0.75      // 減衰させるかどうかを決定 0.75はサイクルの3/4から減衰させるため
                        && peakCounter % peakHoldDecayCycle == 0)                                     // 減衰スピード
                        {
                            for (int j = 0; j < peakValue.Length; j++) peakValue[j] -= (dash + dash);   // Peakを1レベル減衰させる
                            if (peakValue[i] < powY)
                            {
                                peakValue[i] = powY;                              //減衰しすぎたら更新
                            }
                        }
                        //else
                        //    peakValue[i] = powY; 
                        bounds = ((peakValue[i] - dash) / (dash + dash) + 1) * (dash + dash); // 描画境界を計算
                                                                                              //((int)((x-3)/6)+1)*6
                        if (bounds > 7)
                            // Peak描画本体
                            g[isRight].DrawLine(myPen, posX, canvas[0].Height - bounds, posX, canvas[0].Height - bounds - dash);   // boundsから上に向かってひと目盛だけ描く
                    }
                }
                peakCounter++;
                
                label1.Text = peakCounter.ToString("0000") + " / " + counterCycle.ToString();
                
                if (peakCounter >= counterCycle)      // peakhold=falseでもスクリーンセーバー制御に使っているのでカウンターだけは回しておく
                {
                    peakCounter = 0;   // 規定サイクル回ったらカウンターをリセット
                    for (int j =0; j < numberOfBar * channel; j++ ) peakValue[j] = (int)(analizer._spectrumdata[j] / ratio);     //Peak値もリセット
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
                //label1.Text = sensibility.ToString();
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

            spectrum2.Image = canvas[0];

            if (canvas[1] != null)
            {
            //    if (leftSideFlip) canvas[1].RotateFlip(RotateFlipType.RotateNoneFlipX);       // canvas[1] is left...
                spectrum1.Image = canvas[1];
            }
        }

        private void ClearSpectrum(object sender, EventArgs e)
        {
            // SetLayoutしてからClearSpectrum SetLayout内でspectrumとcanvasのサイズを調整済み
            
            for (int i = 0; i < canvas.Length; i++) canvas[i] = new Bitmap(canvasWidth, baseHeght);

            var g = Graphics.FromImage(canvas[0]);                                                                  
            for (int j = 0; j < numberOfBar; j++)       // Barが減ると右側が残ってしまう！！
            {
                var posX = leftPadding + j * 40;
                g.DrawLine(bgPen, posX, canvas[0].Height, posX, 0);
            }

            g.Dispose();
            spectrum1.Image = canvas[0];
            spectrum2.Image = canvas[0];
            spectrum1.Update();
            spectrum2.Update();
            //label1.Text = canvas[0].Width.ToString();

            if(form2.RadioRainbow.Checked == true)
                endPointX = leftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - barSpacing;

            if (form2.RadioRightFlip.Checked)
            {
                LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: false);
                LocateFrequencyLabel(spectrum2, freqLabel_Right, isFlip: true);
            }
            else if (form2.RadioLeftFlip.Checked)
            {
                LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: true);
                LocateFrequencyLabel(spectrum2, freqLabel_Right, isFlip: false);
            }
            else
            {
                LocateFrequencyLabel(spectrum1, freqLabel_Left, isFlip: false);
                LocateFrequencyLabel(spectrum2, freqLabel_Right, isFlip: false);        // 位置もそれぞれの画像を基準にここで設定しているのでLR両方必要
            }
        }

        private void LocateFrequencyLabel(PictureBox spectrum, Label[] freqLabel, bool isFlip)
        {
            //PictureBox _spectrum = spectrum;
            SuspendLayout();

            int barSpacingScaled = (int)(barSpacing * spectrumWidthScale);
            for (int i = 0; i < maxNumberOfBar; i++)
            {
                string labelText;
                //int freqvalues = (int)(Math.Pow(2, i * 10.0 / (numberOfBar - 1) + 4) / (2*channel*2)) * 10    // /2*10 is magic number...
                int freqvalues = (int)(Math.Pow(2, i * 10.0 / (numberOfBar - 1) + 4.35));       // チャンネル数が影響する?
                if (freqvalues > 1000)
                {
                    labelText = (freqvalues / 1000).ToString("0") + "KHz";
                }
                else labelText = (freqvalues / 10 * 10).ToString("0") + "Hz";    // round -1

                //int j = leftSideFlip && isLeft ? numberOfBar - 1 - i : i;
                int j = flipSide > 0 && isFlip ? numberOfBar - 1 - i : i;
                if (i < numberOfBar)        // Barのある部分のラベル
                {
                    freqLabel[i].Name = "freqlabel" + (j + 1).ToString();
                    freqLabel[j].Text = labelText;
                    freqLabel[i].Font = new Font("Arial", labelFontSize /*, this.Width > 640 ? FontStyle.Bold : FontStyle.Regular*/);
                    freqLabel[i].Top = spectrum.Bottom + labelPadding;
                    //freqLabel[i].Left = spectrum.Left + barSpacing + i * (int)(penWidth + barSpacing) - 3;
                    freqLabel[i].Left = (int)(spectrum.Left + barSpacingScaled + i * (penWidth + (float)barSpacing) * spectrumWidthScale/* + 3f * spectrumWidthScale*/);
                    freqLabel[i].TextAlign = ContentAlignment.TopCenter;
                    freqLabel[i].AutoSize = true;
                    freqLabel[i].Visible = true;
                }
                else    // Barがないのでlabel不要
                {
                    freqLabel[i].Text = "";
                    freqLabel[i].Visible = false;
                }
            }
            this.Controls.AddRange(freqLabel);
            ResumeLayout(false);
        }

        private void LoadConfigParams()
        {
            //config reader
            var confReader = new ConfigReader(@".\" + ProductName + @".conf");

            numberOfBar = confReader.GetValue("numberOfBar", 8);    // 16);
            form2.RadioMono.Checked = confReader.GetValue("mono", false);
            form2.RadioStereo.Checked = confReader.GetValue("stereo", true);

            spectrum1.Width = spectrum2.Width = confReader.GetValue("spectrum.Width", 650);      // フォームのサイズを決めてから逆算するべき?
            spectrum1.Height = spectrum2.Height = confReader.GetValue("spectrum.Height", 129);
            spectrum1.Left = spectrum2.Left = horizontalPadding = confReader.GetValue("horizontalPadding", 12);
            spectrum1.Top = verticalPadding = confReader.GetValue("verticalPadding", 12);

            this.Top = confReader.GetValue("form1Top", 130);
            this.Left = confReader.GetValue("form1Left", 130);
            this.Width = confReader.GetValue("form1Width", 692);        // あとで計算結果で再チェック
            this.Height = confReader.GetValue("form1Height", 390);
            form2.Top = confReader.GetValue("form2Top", this.Bottom + verticalPadding);
            form2.Left = confReader.GetValue("form2Left", this.Left);

            horizontalSpacing = confReader.GetValue("horizontalSpacing", 46);
            leftPadding = confReader.GetValue("leftPadding", 25);

            peakHoldTimeMsec = confReader.GetValue("peakHoldTimeMsec", 1000);
            form2.TrackBar1.Value = confReader.GetValue("form2TrackBar1Value", 80);
            form2.TrackBar2.Value = confReader.GetValue("form2TrackBar2Value", 10);

            form2.PeakholdCheckBox.Checked = confReader.GetValue("peakhold", true);                           // redundant using variables?
            form2.AlwaysOnTopCheckBox.Checked = confReader.GetValue("alwaysOnTop", false);
            form2.SSaverCheckBox.Checked = confReader.GetValue("preventSSaver", true);
            form2.RadioHorizontal.Checked = confReader.GetValue("horizontalLayout", false);
            form2.RadioVertical.Checked = confReader.GetValue("verticalLayout", true);
            //form2.CheckBoxFlip.Checked = confReader.GetValue("flipLeft", false);
            form2.RadioNoFlip.Checked = confReader.GetValue("flipNone", true);
            form2.RadioRightFlip.Checked = confReader.GetValue("flipRight", false);
            form2.RadioLeftFlip.Checked = confReader.GetValue("flipLeft", false);

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
        }

        private bool SaveConfigParams()
        {
            var confWriter = new ConfigWriter();
            confWriter.AddValue("numberOfBar", numberOfBar);

            confWriter.AddValue("form1Top", this.Top);
            confWriter.AddValue("form1Left", this.Left);
            confWriter.AddValue("form1Width", this.Width);
            confWriter.AddValue("form1Height", this.Height);
            confWriter.AddValue("form2Top", form2.Top);
            confWriter.AddValue("form2Left", form2.Left);

            //confWriter.AddValue("spectrum.Width", spectrum1.Width);      // フォームのサイズを決めてから逆算するべき?
            //confWriter.AddValue("spectrum.Height", spectrum1.Height);
            confWriter.AddValue("horizontalPadding", horizontalPadding);
            confWriter.AddValue("verticalPadding", verticalPadding);
            confWriter.AddValue("horizontalSpacing", horizontalSpacing);
            confWriter.AddValue("leftPadding", leftPadding);

            confWriter.AddValue("peakHoldTimeMsec", peakHoldTimeMsec);
            confWriter.AddValue("form2TrackBar1Value", form2.TrackBar1.Value);      // sensibility
            confWriter.AddValue("form2TrackBar2Value", form2.TrackBar2.Value);      // peakhold decay time
            
            confWriter.AddValue("peakhold", form2.PeakholdCheckBox.Checked);
            confWriter.AddValue("alwaysOnTop", form2.AlwaysOnTopCheckBox.Checked);
            confWriter.AddValue("preventSSaver", form2.SSaverCheckBox.Checked);

            confWriter.AddValue("LED", form2.RadioPrisum.Checked);
            confWriter.AddValue("classic", form2.RadioClassic.Checked);
            confWriter.AddValue("simple", form2.RadioSimple.Checked);
            confWriter.AddValue("rainbow", form2.RadioRainbow.Checked);
            
            confWriter.AddValue("verticalLayout", form2.RadioVertical.Checked);
            confWriter.AddValue("horizontalLayout", form2.RadioHorizontal.Checked);
            //confWriter.AddValue("flipLeft", form2.CheckBoxFlip.Checked);
            confWriter.AddValue("flipNone", form2.RadioNoFlip.Checked);
            confWriter.AddValue("flipRight", form2.RadioRightFlip.Checked);
            confWriter.AddValue("flipLeft", form2.RadioLeftFlip.Checked);


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

            int[] bars = { 1, 2, 4, 8, 16 };
            string[] strPrisumPosition = new string[maxNumberOfBar+1];
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

        private void SetDefaultParams()
        {
            numberOfBar = 16;
            spectrum1.Width = spectrum2.Width = 650;
            spectrum1.Height = spectrum2.Height = 129;
            horizontalPadding = 12;     // クライアント領域の左余白
            verticalPadding = 12;       // クライアント領域の上余白
            horizontalSpacing = 46;     // 左右チャンネルの上下間隔
            leftPadding = 25;           // spectrum内の左余白

            this.Top = 130;
            this.Left = 130;
            this.Width = 692;
            this.Height = 390;
            form2.Top = this.Bottom + verticalPadding;
            form2.Left = this.Left;

            peakHoldTimeMsec = 1000;
            form2.TrackBar1.Value = 80;
            form2.TrackBar2.Value = 10;
            form2.PeakholdCheckBox.Checked = true;
            form2.AlwaysOnTopCheckBox.Checked = false;
            form2.SSaverCheckBox.Checked = true;

            classicChecked = false;
            prisumChecked = true;
            simpleChecked = false;
            rainbowChecked = false;
            form2.RadioVertical.Checked = true;
            form2.RadioHorizontal.Checked = false;
            //leftSideFlip = false;
            form2.RadioNoFlip.Checked = true;
            form2.RadioRightFlip.Checked = false;
            form2 .RadioLeftFlip.Checked = false;

            classicColors = new Color[]
            {   Color.FromArgb(255, 0, 0),                          // 配列の数を決めるために実データで初期化する
                    Color.FromArgb(255, 255, 0),
                    Color.FromArgb(0, 128, 0)
            };
            classicPositions = new float[] { 0.0f, 0.3f, 1.0f };    // Positionも同じく初期化必要 以下同じ

            prisumColors = new Color[]
            {   Color.FromArgb(255,0,0),    //   0  red
                    Color.FromArgb(255,255,0),  //  60  yellow
                    Color.FromArgb(0,255,0),    // 120  lime
                    Color.FromArgb(0,255,255),  // 180  cyan
                    Color.FromArgb(0,0,255),    // 240  blue
            };
            prisumPositions = new float[17][];
            prisumPositions[1] = new float[] { 0.0f, 0.40f, 0.5f, 0.55f, 1.0f };
            prisumPositions[2] = new float[] { 0.0f, 0.35f, 0.5f, 0.55f, 1.0f };
            prisumPositions[4] = new float[] { 0.0f, 0.35f, 0.5f, 0.55f, 1.0f };
            prisumPositions[8] = new float[] { 0.0f, 0.37f, 0.5f, 0.55f, 1.0f };
            prisumPositions[16] = new float[] { 0.0f, 0.36f, 0.5f, 0.55f, 1.0f };

            simpleColors = new Color[] { Color.LightSkyBlue, Color.LightSkyBlue };
            simplePositions = new float[] { 0.0f, 1.0f };
        }

        private void label1_Click(object sender, EventArgs e)
        {
            /* テスト用メソッド
            int channels = 2;

            int[] fft = new int[1024];
            int x, y;
            int b0 = 0;
            for (x = 0; x < 8; x++)
            {
                int peak = 0;                               // peakは各バンド内の最大値
                int b1 = (int)Math.Pow(2, x * 10.0 / (8 - 1));
                if (b1 > 1023) b1 = 1023;

                if (b1 <= b0) b1 = b0 + 1;
                for (; b0 < b1; b0+=channels)
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