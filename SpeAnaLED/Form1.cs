using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Media.Animation;
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
        private readonly Bitmap[] canvas = new Bitmap[2];
        private Color[] colors;
        private float[] positions;
        private Pen myPen;
        private Pen bgPen;
        private LinearGradientBrush brush;
        private Label[] freqLabel_Left;
        private Label[] freqLabel_Right;
        private int counterCycle;
        private readonly int[] peakValue;
        private int peakCounter = 0;
        private readonly int[,] peakTiming = new int[2,16];
        private readonly int channel;
        public int numberOfBar;
        private readonly int maxNumberOfBar = 16;
        private float ratio;
        private int dash;
        private int displayOffCounter;
        private string configFileName;
        private bool inInit = false;

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
        private float sensibility;
        private int form2TrackBar1Value;
        private int peakHoldTimeMsec;
        private int peakHoldDecayCycle = 10;    // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)
        private int form2TrackBar2Value;
        private bool peakhold;
        private bool alwaysOnTop;
        private bool preventSSaver;
        private bool classicChecked, prisumChecked, simpleChecked, rainbowChecked;
        private bool rightSideFlip = false;

        // 念のためデフォルト値はセットしておく
        private Color[] classicColors =
            {  Color.FromArgb(255,0,0),     //  0 100 50  red
               Color.FromArgb(255,255,0),   // 60 100 50  yellow
               Color.FromArgb(0,128,0),     //120 100 25  green
            };
        private float[] classicPositions = { 0.0f, 0.3f, 1.0f };

        private Color[] prisumColors =
            {   Color.FromArgb(255,0,0),    //   0  red
                Color.FromArgb(255,255,0),  //  60  yellow
                Color.FromArgb(0,255,0),    // 120  lime
                Color.FromArgb(0,255,255),  // 180  cyan
                Color.FromArgb(0,0,255),    // 240  blue
            };
        private float[][] prisumPositions = new float[17][];   // maxNumberOfBar+1 jagg array for easy to see
        
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

            // Other Event handler (subscribe)
            analizer.SpectrumChanged += ReceiveSpectrumData;
            Application.ApplicationExit += Application_ApplicationExit;

            // 以下内部の計算で出すもの
            
            endPointY = spectrum1.Height;
            numberOfBar = analizer._lines;
            channel = analizer._channel;
            peakValue = new int[maxNumberOfBar * channel];

            // I don't know why "* 20 (num..." is necessary. This is due to actual measurements.
            // If you edit this parameter, also edit "Form2_ComboBox1_SelectedItemChanged" and
            // "Form2_ComboBox2_SelectedItemChanged".
            counterCycle = (int)(peakHoldTimeMsec / analizer._timer1.Interval.Milliseconds * 40 * (numberOfBar * channel / 16.0));      // default hold time
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadConfigParams();
            }
            catch
            {
                MessageBox.Show("Opps! Config file seems something wrong...\r\nUse default parameters.",
                    "Config file error - SpeAnaLED",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                numberOfBar = 16;
                spectrum1.Width = 650;
                spectrum1.Height = 128;
                horizontalPadding = 12;
                verticalPadding = 12;
                horizontalSpacing = 46;
                leftPadding = 25;
                peakHoldTimeMsec = 1000;
                form2TrackBar1Value = 78;
                form2TrackBar2Value = 10;
                peakhold = true;
                alwaysOnTop = false;
                preventSSaver = true;
                classicChecked = false;
                prisumChecked = true;
                simpleChecked = false;
                rainbowChecked = false;
            }

            spectrum2.Top = spectrum1.Bottom + horizontalSpacing;

            freqLabel_Left = new Label[maxNumberOfBar];
            freqLabel_Right = new Label[maxNumberOfBar];
            for (int i = 0; i < maxNumberOfBar; i++)
            {
                freqLabel_Left[i] = new Label();
                freqLabel_Right[i] = new Label();
            }
            for (int i = 0; i < channel; i++) canvas[i] = new Bitmap(spectrum1.Width,spectrum1.Height);
            bgPen = new Pen(Color.DimGray, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
            ClearSpectrum(this, EventArgs.Empty);

            if (channel < 2)
            {
                spectrum1.Visible = false;
                for (int i = 0; i < maxNumberOfBar; i++) freqLabel_Left[i].Visible = false;
            }

            ratio = 0xff / canvas[0].Height * (10f - sensibility);
            dash = (int)penWidth / 10;

            // デフォルト(前回)カラー(pen)の事前準備
            colors = prisumColors;         // あとでconfigに前回内容を保存する？
            positions = prisumPositions[numberOfBar];

            brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
            {
                InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
            };
            myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };

            // form2 setting
            form2.ComboBox1.SelectedIndex = form2.ComboBox1.Items.IndexOf(numberOfBar.ToString());
            form2.ComboBox2.SelectedIndex = form2.ComboBox2.Items.IndexOf(peakHoldTimeMsec.ToString());
            form2.TrackBar1.Value = form2TrackBar1Value;    // (int)(sensibility * 10f);
            form2.TrackBar2.Value = form2TrackBar2Value;
            form2.TextBox_Sensibility.Text = (form2TrackBar1Value / 10f).ToString("0.0");
            form2.TextBox_DecaySpeed.Text = form2TrackBar2Value.ToString();
            form2.PeakholdCheckBox.Checked = peakhold;
            form2.AlwaysOnTopCheckBox.Checked = alwaysOnTop;
            form2.SSaverCheckBox.Checked = preventSSaver;
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
            ratio = 0xff / canvas[0].Height * (10f - sensibility);

            
            inInit = false;
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
            sensibility = form2.TrackBar1.Value / 10f;
            ratio = 0xff / canvas[0].Height * (10f - sensibility);
            //label1.Text = sensibility.ToString("0.0");
        }

        private void Form2_TrackBar2_ValueChanged(object sender, EventArgs e)           // 減衰スピード調整
        {
            peakHoldDecayCycle = 20 * numberOfBar / channel / form2.TrackBar2.Value;      // スピードと値の増減方向が合うように逆数にする
            // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)
        }

        private void Form2_ComboBox1_SelectedItemChanged(object sender, EventArgs e)    // バンド数変更
        {
            numberOfBar = Convert.ToInt16(form2.ComboBox1.SelectedItem);
            counterCycle = (int)(peakHoldTimeMsec / analizer._timer1.Interval.Milliseconds * 40 * (numberOfBar / 16.0));    // ホールドタイムはバンド数に影響をうける
            Form2_TrackBar2_ValueChanged(sender, e);

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
            ClearSpectrum(sender, EventArgs.Empty);
        }

        private void Form2_ComboBox2_SelectedItemChanged(object sender, EventArgs e)    // ホールドタイム変更
        {
            peakHoldTimeMsec = Convert.ToInt16(form2.ComboBox2.SelectedItem);
            counterCycle = (int)(peakHoldTimeMsec / analizer._timer1.Interval.Milliseconds * 40 * (numberOfBar / 16.0));
            //label1.Text = counterCycle.ToString();
        }
        
        private void Form2_RadioClassic_CheckChanged(object sender, EventArgs e)
        {
            if (form2.Form2_RadioClassic.Checked && !inInit)
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
            if (form2.Form2_RadioPrisum.Checked && !inInit)
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
            if (form2.Form2_RadioSimple.Checked && !inInit)
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
            if (form2.Form2_RadioRainbow.Checked && !inInit)
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
            if (inInit == false) preventSSaver = !preventSSaver;
        }

        private void Form2_PeakholdCheckboxCheckedChanged(object sender, EventArgs e)
        {
            if (inInit == false) peakhold = !peakhold;
        }

        private void Form2_AlwaysOnTopCheckboxCheckChanged(object sender, EventArgs e)
        {
            if (inInit == false) TopMost = !TopMost;
        }

        private void ReceiveSpectrumData(object sender, EventArgs e)
        {
            int bounds;
            int isRight = 0; // interreave対応

            var g = new Graphics[2];
            for (int i = 0; i < channel; i++) g[i] = Graphics.FromImage(canvas[i]);
            
            for (int i = 0; i < numberOfBar * channel; i++)     // analizerから受け取る_spectumdataは Max16*2=32バイト L,R,L,R,...
            {
                if (channel > 1) isRight = i % 2;
                
                var posX = leftPadding + (i - isRight) / channel * (penWidth + barSpacing);          //横方向の位置
                var powY = (int)(analizer._spectrumdata[i] / ratio);
                g[isRight].DrawLine(bgPen, posX, canvas[0].Height, posX, 0);                         // 背景を下から上に向かって描く
                g[isRight].DrawLine(myPen, posX, canvas[0].Height, posX, canvas[0].Height - powY);

                // Peak Hold
                if (peakhold)
                {
                    if (peakValue[i] <= powY && powY != 0)
                    {
                        peakValue[i] = powY;        // ピーク更新するが描画は不要
                        peakTiming[isRight, i / 2] = peakCounter;       // int i/2 = 0,0,1,1,2,2,...
                    }
                    // 以下powYがpeak以下の時はpeakを描く
                    else
                    {
                        if (peakCounter > counterCycle / 2      // 減衰させるかどうかを決定 /2はサイクル後半から減衰させるため
                        && peakCounter % peakHoldDecayCycle == 0                                        // 減衰スピード
                        && peakTiming[isRight, i / channel] + counterCycle > peakCounter)               // ホールドタイムを過ぎた?
                        {
                            for (int j = 0; j < peakValue.Length; j++) peakValue[j] -= (dash + dash);   // Peakを1レベル減衰させる
                            if (peakValue[i] < powY ) peakValue[i] = powY;                              //減衰しすぎたら更新
                            peakTiming[isRight, i / 2] = peakCounter;
                        }
                        
                        bounds = ((int)((peakValue[i] - dash) / (dash + dash)) + 1) * (dash + dash) + dash - 2; // 描画境界を計算
                                //((int)((x-3)/6)+1)*6+3  -(128-129)
                        if (bounds <= 7) bounds = 0;    // 0x00はpowYが7になるから7以下は描画しない
                        // Peak描画本体
                        g[isRight].DrawLine(myPen, posX, canvas[0].Height - bounds, posX, canvas[0].Height - bounds - 2);   // boundsから上に向かってひと目盛だけ描く
                    }
                }
                peakCounter++;
                if (peakCounter >= counterCycle)      // peakhold=falseでもスクリーンセーバー制御に使っているのでカウンターだけは回しておく
                {
                    peakCounter = 0;   // 規定サイクル回ったらカウンターをリセット
                    peakValue[i] = powY;
                    displayOffCounter++;
                    //label1.Text = counterCycle.ToString();
                }
                if (displayOffCounter > 5)  //100
                {
                    if (preventSSaver)
                    {
                        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                    }
                    displayOffCounter = 0;
                }
                //label1.Text = sensibility.ToString();
            }
            
            for (int i = 0; i < channel; i++) g[i].Dispose();

            if (rightSideFlip) canvas[0].RotateFlip(RotateFlipType.RotateNoneFlipX);       // canvas[0] is right...
            spectrum2.Image = canvas[0];
            if (canvas[1] != null) spectrum1.Image = canvas[1];
        }

        private void ClearSpectrum(object sender, EventArgs e)
        {
            //numberOfBar = analizer._lines;
            spectrum1.Width = leftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - (leftPadding - barSpacing);
            spectrum2.Width = spectrum1.Width;

            for (int i = 0; i< canvas.Length; i++) canvas[i] =  new Bitmap(spectrum1.Width, spectrum1.Height);
            var g = Graphics.FromImage(canvas[0]);
            for (int j = 0; j < numberOfBar; j++)
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

            LocateFrequencyLabel(spectrum1, freqLabel_Left);
            LocateFrequencyLabel(spectrum2, freqLabel_Right);       // 位置もそれぞれの画像を基準にこれで設定しているのでLR両方必要
        }

        private void LocateFrequencyLabel(PictureBox spectrum, Label[] freqLabel)
        {
            PictureBox _spectrum = spectrum;
            SuspendLayout();

            for (int i = 0; i < maxNumberOfBar; i++)
            {
                string labelText;
                //int freqvalues = (int)(Math.Pow(2, i * 10.0 / (numberOfBar - 1) + 4) / (2*channel*2)) * 10    // /2*10 is magic number...
                int freqvalues = (int)(Math.Pow(2, i * 10.0 / (numberOfBar - 1) + 4.35));      
                if (freqvalues > 1000)
                {
                    labelText = (freqvalues / 1000).ToString("0") + "KHz";
                }
                else labelText = (freqvalues / 10 * 10).ToString("0") + "Hz";    // round -1

                if (i < numberOfBar)        // Barのある部分のラベル
                {
                    freqLabel[i].Name = "freqlabel" + (i + 1).ToString();
                    freqLabel[i].Text = labelText;
                    freqLabel[i].Font = new Font("Arial", labelFontSize);
                    freqLabel[i].Top = _spectrum.Bottom + labelPadding;
                    freqLabel[i].Left = _spectrum.Left + barSpacing + i * (int)(penWidth + barSpacing);
                    freqLabel[i].TextAlign = ContentAlignment.TopRight;
                    freqLabel[i].AutoSize = true;
                }
                else    // Barがないのでlabel不要
                    freqLabel[i].Text = "";
            }
            this.Controls.AddRange(freqLabel);
            ResumeLayout(false);
        }

        private void LoadConfigParams()
        {
            //config reader
            configFileName = @".\" + this.ProductName + @".conf";
            if (!System.IO.File.Exists(configFileName)) System.IO.File.Create(configFileName);
            var confReader = new ConfigReader(configFileName);

            numberOfBar = confReader.GetValue("numberOFBar", 16);
            
            spectrum1.Width = spectrum2.Width = confReader.GetValue("spectrum.Width", 650);      // フォームのサイズを決めてから逆算するべき?
            spectrum1.Height = spectrum2.Height = confReader.GetValue("spectrum.Height", 128);
            spectrum1.Left = spectrum2.Left = horizontalPadding = confReader.GetValue("horizontalPadding", 12);
            spectrum1.Top = verticalPadding = confReader.GetValue("verticalPadding", 12);

            horizontalSpacing = confReader.GetValue("horizontalSpacing", 46);
            leftPadding = confReader.GetValue("leftPadding", 25);

            peakHoldTimeMsec = confReader.GetValue("peakHoldTimeMsec", 1000);
            form2TrackBar1Value = confReader.GetValue("form2TrackBar1Value", 78);
            form2TrackBar2Value = confReader.GetValue("form2TrackBar2Value", 10);
            
            peakhold = confReader.GetValue("peakhold", true);
            alwaysOnTop = confReader.GetValue("alwaysOnTop", false);
            preventSSaver = confReader.GetValue("preventSSaver", true);

            //classicChecked = prisumChecked = simpleChecked = rainbowChecked = false;
            if ((prisumChecked = confReader.GetValue("prisumChecked", true)) == false)
                if ((classicChecked = confReader.GetValue("classicChecked", false)) == false)
                    if ((simpleChecked = confReader.GetValue("simpleChecked", false)) == false)
                        rainbowChecked = confReader.GetValue("rainbowChecked", false);

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

            // adjust Rainbow color position
            int[] bars = { 1, 2, 4, 8, 16 };
            string[][] param = new string[maxNumberOfBar + 1][];
            param[1] = confReader.GetValue("prisumPositions_1", "0.0, 0.40, 0.5, 0.55, 1.0").Split(',');      // red,yellow,lime,cyan,blue
            param[2] = confReader.GetValue("prisumPositions_2", "0.0, 0.35, 0.5, 0.55, 1.0").Split(',');
            param[4] = confReader.GetValue("prisumPositions_4", "0.0, 0.35, 0.5, 0.55, 1.0").Split(',');
            param[8] = confReader.GetValue("prisumPositions_8", "0.0, 0.37, 0.5, 0.55, 1.0").Split(',');
            param[16] = confReader.GetValue("prisumPositions_16", "0.0, 0.36, 0.5, 0.55, 1.0").Split(',');
            foreach (int i in bars)                                 // 1,2,4,8,16
            {
                foreach (int j in bars)                             // 1,2,4,8,16
                {
                    prisumPositions[j] = new float[/*param[j].Length*/5];
                    for (int k = 0; k < bars.Length; k++)           // 0,1,2,3,4
                        prisumPositions[j][k] = float.Parse(param[j][k]);
                }
            }
        }

        private bool SaveConfigParams()
        {
            var confWriter = new ConfigWriter();
            confWriter.AddValue("numberOfBar", numberOfBar);    //次はloadを書くところから
            
            confWriter.AddValue("spectrum.Width", spectrum1.Width);      // フォームのサイズを決めてから逆算するべき?
            confWriter.AddValue("spectrum.Height", spectrum1.Height);
            confWriter.AddValue("horizontalPadding", horizontalPadding);
            confWriter.AddValue("verticalPadding", verticalPadding);
            confWriter.AddValue("horizontalSpacing", horizontalSpacing);
            confWriter.AddValue("leftPadding", leftPadding);
            confWriter.AddValue("peakHoldTimeMsec", peakHoldTimeMsec);
            confWriter.AddValue("form2TrackBar1Value", form2.TrackBar1.Value);
            confWriter.AddValue("form2TrackBar2Value", form2.TrackBar2.Value);
            confWriter.AddValue("peakhold", peakhold);
            confWriter.AddValue("alwaysOnTop", alwaysOnTop);
            confWriter.AddValue("preventSSaver", preventSSaver);
            confWriter.AddValue("classicChecked", form2.Form2_RadioClassic.Checked);
            confWriter.AddValue("prisumChecked", form2.Form2_RadioPrisum.Checked);
            confWriter.AddValue("simpleChecked", form2.Form2_RadioSimple.Checked);
            confWriter.AddValue("rainbowChecked", form2.Form2_RadioRainbow.Checked);

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
                confWriter.Save(configFileName);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
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