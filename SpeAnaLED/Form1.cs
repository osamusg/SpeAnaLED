using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
//using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace SpeAnaLED
{
    public partial class Form1 : Form
    {
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
        private int numberOfBar;
        private readonly int maxNumberOfBar = 16;
        private float ratio;
        private int dash;
        private int displayOffCounter;
        
        // parameters (set defaults)
        private int leftPadding = 25;
        private int endPointX = 0;              // グラデーションのデフォルト描画方向 上から下
        private int endPointY;                  // PictureBoxの大きさにより可変?
        private float penWidth = 30f;
        private int horizontalPadding = 12;
        private int verticalPadding = 12;
        private int labelPadding = 6;
        private int barSpacing = 10;
        private float labelFontSize = 9f;
        
        private float sensibility = 7.8f;       // 変えた場合、form2.csのデフォルト値も直すこと
        private int peakHoldTimeMsec = 2000;
        private int peakHoldDecayCycle = 10;    // fast(heavy) <- 8cycle=160/20unit 10(default)=160/16 16=160/10 20=160/8 -> slow(light)
        private bool preventSSaver = true;
        private bool peakhold = true;

        private readonly Color[] classicColors =
            {  Color.FromArgb(255,0,0),     //  0 100 50  red
               Color.FromArgb(255,255,0),   // 60 100 50  yellow
               Color.FromArgb(0,128,0),     //120 100 25  green
            };
        private readonly float[] classicPositions = { 0.0f, 0.3f, 1.0f };

        private readonly Color[] prisumColors =
            {   Color.FromArgb(255,0,0),    //   0  red
                Color.FromArgb(255,255,0),  //  60  yellow
                Color.FromArgb(0,255,0),    // 120  lime
                Color.FromArgb(0,255,255),  // 180  cyan
                Color.FromArgb(0,0,255),    // 240  blue
            };
        private readonly  float[][] prisumPositions = new float[17][];   // maxNumberOfBar+1 jagg array for easy to see
        
        private readonly Color[] simpleColors = { Color.LightSkyBlue, Color.LightSkyBlue};
        private readonly float[] simplePositions = { 0.0f, 1.0f };

        public Form1()
        {
            InitializeComponent();


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


            spectrum1.Width = spectrum2.Width = 650;      // あとで計算するようにする
            spectrum1.Height = spectrum2.Height = 128;
            spectrum1.Left = spectrum2.Left = horizontalPadding;
            spectrum1.Top = verticalPadding;
            spectrum2.Top = spectrum1.Bottom + 46;
            endPointY = spectrum1.Height;
            numberOfBar = analizer._lines;
            channel = analizer._channel;
            peakValue = new int[maxNumberOfBar * channel];

            // adjust Rainbow color position
            prisumPositions[1] = new float[5] { 0.0f, 0.4f, 0.5f, 0.55f, 1.0f };    // red,yellow,lime,cyan,blue
            prisumPositions[2] = new float[5] { 0.0f, 0.35f, 0.5f, 0.55f, 1.0f };
            prisumPositions[4] = new float[5] { 0.0f, 0.35f, 0.5f, 0.55f, 1.0f };
            prisumPositions[8] = new float[5] { 0.0f, 0.37f, 0.5f, 0.55f, 1.0f };
            prisumPositions[16] = new float[5] { 0.0f, 0.36f, 0.5f, 0.55f, 1.0f };

            // I don't know why "* 20 (num..." is necessary. This is due to actual measurements.
            // If you edit this parameter, also edit "Form2_ComboBox1_SelectedItemChanged" and
            // "Form2_ComboBox2_SelectedItemChanged".
            counterCycle = (int)(peakHoldTimeMsec / analizer._timer1.Interval.Milliseconds * 40 * (numberOfBar * channel / 16.0));      // default hold time
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            colors = classicColors;         // あとでconfigに前回内容を保存する？
            positions = classicPositions;

            brush = new LinearGradientBrush(new Point(0, 0), new Point(endPointX, endPointY), Color.FromArgb(255, 0, 0), Color.FromArgb(0, 0, 255))
            {
                InterpolationColors = new ColorBlend() { Colors = colors, Positions = positions }
            };
            myPen = new Pen(brush, penWidth) { DashPattern = new float[] { 0.1f, 0.1f } };
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
            if (form2.Form2_RadioClassic.Checked)
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
            if (form2.Form2_RadioPrisum.Checked)
            {
                colors = prisumColors;
                positions = prisumPositions[16];
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
            if (form2.Form2_RadioSimple.Checked)
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
            if (form2.Form2_RadioRainbow.Checked)
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
            preventSSaver = !preventSSaver;
        }

        private void Form2_PeakholdCheckboxCheckedChanged(object sender, EventArgs e)
        {
            peakhold = !peakhold;
        }

        private void Form2_AlwaysOnTopCheckboxCheckChanged(object sender, EventArgs e)
        {
            TopMost = !TopMost;
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
                    label1.Text = counterCycle.ToString();
                }
                if (displayOffCounter > 5)  //100
                {
                    displayOffCounter = 0;
                    if (preventSSaver)
                    {
                        int xPosShift = Cursor.Position.X == 0 ? 1 : -1;
                        Cursor.Position = new System.Drawing.Point(Cursor.Position.X + xPosShift, Cursor.Position.Y);
                        Cursor.Position = new System.Drawing.Point(Cursor.Position.X - xPosShift, Cursor.Position.Y);
                    }
                }
                label1.Text = counterCycle.ToString();
            }
            
            for (int i = 0; i < channel; i++) g[i].Dispose();

            //canvas[0].RotateFlip(RotateFlipType.Rotate180FlipX);
            //canvas[1].RotateFlip(RotateFlipType.Rotate180FlipX);
            spectrum2.Image = canvas[0];
            if (canvas[1] != null) spectrum1.Image = canvas[1];
            //spectrum2.Image.RotateFlip(RotateFlipType.RotateNoneFlipXY);
        }

        private void ClearSpectrum(object sender, EventArgs e)
        {
            numberOfBar = analizer._lines;
            spectrum1.Width = leftPadding + numberOfBar * ((int)bgPen.Width + barSpacing) - (leftPadding - barSpacing);
            spectrum2.Width = spectrum1.Width;

            //canvas[0] =  new Bitmap(spectrum1.Width, spectrum1.Height);
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
            ;
        }
    }
}