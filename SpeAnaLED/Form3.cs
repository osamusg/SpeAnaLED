using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpeAnaLED
{
    public partial class Form3 : Form
    {
        private readonly Form1 form1;
        private readonly Form2 form2;
        
        private const int baseClientWidth = 436;
        private const int baseClientHeight = 65;

        private Point mouseDragStartPoint = new Point(0, 0);
        private struct RectangleBool { public bool left, right, top, bottom; }
        private RectangleBool pinch;
        private bool inFormSizeChange;
        private readonly int[] meterPeakValue;
        private float levelMeterSensitivity;
        private int peakCounter;

        // drawing
        private readonly string panelFontName = "Microsoft Sans Serif";
        private readonly Bitmap canvas;
        public Pen greenPen, redPen, bgPen;

        // event handler (Fire)
        public event FormClosedEventHandler Form_Closed;
        
        public Form3(Form1 _form1, Form2 _form2)
        {
            InitializeComponent();

            form1 = _form1;
            form2 = _form2;

            // subscribe
            form2.LevelSensitivityTrackBar.ValueChanged += Form2_LevelSensitivityTrackBar_ValueChanged;
            form1.DispatchAnalyzerLevelChanged += Form1_ReceiveSpectrumData;
            
            meterPeakValue = new int[Form2.maxChannel];
            canvas = new Bitmap(baseClientWidth, baseClientHeight);
            levelMeterSensitivity = (float)form2.LevelSensitivityTrackBar.Value / 10f;

            greenPen = new Pen(new Color(), 8f) { DashPattern = new float[] { 3f, 0.75f } };    // 8*3=24, 8*0.75=6
            redPen = new Pen(new Color(), 8f) { DashPattern = new float[] { 3f, 0.75f } };
            bgPen = new Pen(Color.FromArgb(29, 29, 29), 8f) { DashPattern = new float[] { 3f, 0.75f } };

            FormPictureBox.Controls.Add(LevelPictureBox);
            Draw_Panel();
            
            //if (!Form1.inInit)
            //{
                var g = Graphics.FromImage(canvas);
                g.DrawLine(bgPen, 30, 20 * 26, canvas.Width - 16, 20 * 26);
                LevelPictureBox.Image = canvas;
            //}
        }

        private void Form3_Load(object sender, System.EventArgs e)
        {
            bgPen.Color = form1.bgPen.Color;

            if (form2.HideTitleCheckBox.Checked)
                FormBorderStyle = FormBorderStyle.None;
            else
                FormBorderStyle = FormBorderStyle.SizableToolWindow;

            Width = form2.form3Width - (FormBorderStyle != FormBorderStyle.None ? form2.defaultBorderSize * 2 : 0);
            Height = form2.form3Height - (FormBorderStyle != FormBorderStyle.None ? form2.defaultTitleHeight + form2.defaultBorderSize : 0);
            Top = form2.form3Top;
            Left = form2.form3Left;

            LevelPictureBox.Width = ClientSize.Width;
            LevelPictureBox.Height = ClientSize.Height;
            LevelPictureBox.Left = 0;
            LevelPictureBox.Top = 0;

            if (Visible)
            {
                form2.HideSpectrumWindowCheckBox.Enabled =
                form2.HideSpectrumWindowCheckBox.Visible = true;
            }
        }

        private void Draw_Panel()
        {
            Font panelFont = new Font(panelFontName, 9f, FontStyle.Bold);

            FormPictureBox.Top = FormPictureBox.Left = 0;
            FormPictureBox.Size = new Size(ClientSize.Width, ClientSize.Height);
            Bitmap canvas = new Bitmap(baseClientWidth, baseClientHeight);
            Graphics g = Graphics.FromImage(canvas);
            Brush greenBrush = new SolidBrush(Color.FromArgb(96, 194, 96));
            Brush redBrush = new SolidBrush(Color.FromArgb(160, 0, 0));
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawString("-        -20   -15   -10     -7      -5     -3     -1", panelFont, greenBrush, 30, 26);
            g.DrawString("0     +1     +3     +5    +8", panelFont, redBrush, 276, 26);
            g.DrawString("L", panelFont, greenBrush, 10, 11);
            //g_client.DrawString("db", panelFont, greenBrush, 10, 24);
            g.DrawString("R", panelFont, greenBrush, 10, 38);
            //g_client.DrawString("PEAK LEVEL METER", new Font(fontName, 7f, FontStyle.Bold), greenBrush, 10, 1);

            Bitmap infinityCanvas = new Bitmap(16, 16);
            infinityCanvas.MakeTransparent();
            Graphics g_Infinity = Graphics.FromImage(infinityCanvas);
            float infinityFontWidth = g_Infinity.MeasureString("8", new Font(panelFontName, 11f, FontStyle.Bold)).Width;
            g_Infinity.TranslateTransform(0, infinityFontWidth);
            g_Infinity.RotateTransform(-90f);
            var brush = new SolidBrush(Color.FromArgb(96, 194, 96));
            g_Infinity.DrawString("8", new Font(panelFontName, 12f, FontStyle.Bold), brush, 0, 0);
            g_Infinity.Dispose();

            g.DrawImage(infinityCanvas, 36, 29, (int)(infinityFontWidth * 1.2), 16);
            g.Dispose();

            FormPictureBox.Image = canvas;
            FormPictureBox.Update();
            FormPictureBox.SendToBack();
        }

        private void Form1_ReceiveSpectrumData(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(canvas);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            int[] level = new int[Form2.maxChannel];
            const int leftPadding = 30;
            const int rightPadding = 16;
            const int dottedline = 30;
            const int solidline = dottedline - 7/*blankBG*/;
            const int leftBarTop = 20;
            const int rightChPadding = 26;
            const int redzone = 240;

            level[0] = (int)((form1.level[0] * levelMeterSensitivity + 1) / dottedline) * dottedline;       // analyzer input point and calculate bounds
            level[1] = (int)((form1.level[1] * levelMeterSensitivity + 1) / dottedline) * dottedline;

            for (int i = 0; i < Form2.maxChannel; i++)
            {
                if (level[i] > Form2.lmPBWidth) level[i] = Form2.lmPBWidth;

                g.DrawLine(bgPen, leftPadding, leftBarTop + i * rightChPadding, canvas.Width - rightPadding, leftBarTop + i * rightChPadding);  // 38=42-8/2
                if (level[i] >= redzone)
                {
                    g.DrawLine(greenPen, leftPadding, leftBarTop + i * rightChPadding, redzone - 1 + leftPadding, leftBarTop + i * rightChPadding);
                    g.DrawLine(redPen, redzone + leftPadding, leftBarTop + i * rightChPadding, level[i] + leftPadding, leftBarTop + i * rightChPadding);
                }
                else
                    g.DrawLine(greenPen, leftPadding, leftBarTop + i * rightChPadding, level[i] + leftPadding, leftBarTop + i * rightChPadding);

                /////// Level Meter Peakhold (always shown)
                if (level[i] == 0)
                {
                    meterPeakValue[i] = -solidline;
                }
                else
                {
                    if (peakCounter * Form2.maxNumberOfBar > form2.counterCycle)
                    {
                        if (meterPeakValue[i] < level[i]) { meterPeakValue[i] = level[i]; }
                        if (meterPeakValue[i] >= redzone + solidline)
                            g.DrawLine(redPen, meterPeakValue[i], leftBarTop + i * rightChPadding, meterPeakValue[i] + solidline, leftBarTop + i * rightChPadding);   // 0-23, 30-53, 60-83... left=right-23
                        else
                            g.DrawLine(greenPen, meterPeakValue[i], leftBarTop + i * rightChPadding, meterPeakValue[i] + solidline, leftBarTop + i * rightChPadding);
                    }
                }
            }
            g.Dispose();
            peakCounter += (form2.numberOfBar * form2.channel);      // end of peak drawing

            RightValueLabel.Text = level[1].ToString();

            LevelPictureBox.Image = canvas;
            LevelPictureBox.BringToFront();

            if (peakCounter >= form2.counterCycle)  // if peakhold=false, counter is used screen saver preventing, so add the counter
            {
                peakCounter = 0;                    // reset when the specified number of rounds
                for (int i = 0; i < Form2.maxChannel; i++) meterPeakValue[i] = level[i];    // right level peak is always shown
            }
        }

        private void Form2_LevelSensitivityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            levelMeterSensitivity = form2.LevelSensitivityTrackBar.Value / 10f;
        }
        
        private void Form3_SizeChanged(object sender, System.EventArgs e)
        {
            Draw_Panel();
            LevelPictureBox.Width = ClientSize.Width;
            LevelPictureBox.Height = ClientSize.Height;
            if (!Form1.inInit)
            {
                var g = Graphics.FromImage(canvas);
                    g.DrawLine(bgPen, 30, 20 * 26, canvas.Width - 16, 20 * 26);
                LevelPictureBox.Image = canvas;
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_Closed?.Invoke(sender, e);
            
            form2.form3Width = Width;
            form2.form3Height = Height;
            form2.form3Top = Top;
            form2.form3Left = Left;
            if (e.CloseReason == CloseReason.UserClosing)
                form2.LevelMeterCheckBox.Checked = false;
        }

        private void LevelPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                form2.Visible = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (FormBorderStyle == FormBorderStyle.None)
                {
                    mouseDragStartPoint = new Point(e.X, e.Y);

                    pinch.left = e.X < form1.mdt;
                    pinch.right = e.X > Width - form1.mdt;
                    pinch.top = e.Y < form1.mdt;
                    pinch.bottom = e.Y > Height - form1.mdt;
                    inFormSizeChange = pinch.left || pinch.right || pinch.top || pinch.bottom;
                    if (!inFormSizeChange) Cursor = Cursors.SizeAll;
                }
            }
        }

        private void LevelPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                const int minWidth = 128;
                const int minHeight = 39;

                if (MinimumSize.Width > minWidth) MinimumSize = new Size(minWidth, MinimumSize.Height);
                if (MinimumSize.Height > minHeight) MinimumSize = new Size(MinimumSize.Width, minHeight);
                MinimumSize = new Size(minWidth, minHeight);

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
                        if (pinch.right && pinch.bottom)
                        {
                            Width = e.X;
                            Height = e.Y;
                        }
                        else if (pinch.left && pinch.top)
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

                if (MouseButtons != MouseButtons.Left)
                {
                    if (e.X < form1.mdt && e.Y < form1.mdt)                         // LeftTop
                        Cursor = Cursors.SizeNWSE;
                    else if (e.X > Width - form1.mdt && e.Y < form1.mdt)            // RightTop
                        Cursor = Cursors.SizeNESW;
                    else if (e.X < form1.mdt && e.Y > Height - form1.mdt)           // LeftBottom
                        Cursor = Cursors.SizeNESW;
                    else if (e.X > Width - form1.mdt && e.Y > Height - form1.mdt)   // RightBottom
                        Cursor = Cursors.SizeNWSE;
                    else if (e.X < form1.mdt || e.X > Width - form1.mdt)
                        Cursor = Cursors.SizeWE;
                    else if (e.Y < form1.mdt || e.Y > Height - form1.mdt)
                        Cursor = Cursors.SizeNS;
                    else if (Cursor != Cursors.SizeAll && e.Button == MouseButtons.None)
                        Cursor = Cursors.Default;
                }
            }
        }

        private void LevelPictureBox_MouseHover(object sender, EventArgs e)
        {
            inFormSizeChange = false;
            Cursor = Cursors.Default;
        }

        private void LevelPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Cursor != Cursors.Default)
                Cursor = Cursors.Default;
            pinch.left = pinch.right = pinch.top = pinch.bottom = false;
        }

        private void LevelPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (!form2.Visible) form2.Visible = true;
        }
    }
}
