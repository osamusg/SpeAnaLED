using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpeAnaLED
{
    public partial class Form4 : Form
    {
        private readonly Form1 form1;
        private readonly Form2 form2;
        private Point mouseDragStartPoint = new Point(0, 0);
        private struct RectangleBool { public bool left, right, top, bottom; }
        private RectangleBool pinch;
        private bool inFormSizeChange;
        private Bitmap previousBitmap;
        private readonly int[] streamBaseLine = { 0, 0 };
        public float pow;
        public int streamCombineMode;                               // combine:1 separate:2
        public Pen streamPen, startPen;
        public int streamScrollUnit = 1;                            // 1 2 4?, For test
        public int streamChannelSpacing = 0;

        // event handler (Fire)
        public event FormClosedEventHandler Form_Closed;

        public Form4(Form1 _form1, Form2 _form2)
        {
            InitializeComponent();

            form1 = _form1;
            form2 = _form2;

            // subscribe
            form2.CombineStreamRadioButton.CheckedChanged += Form2_CombineStreamRadioButton_CheckedChanged;
            form2.StreamColorButton.Click += Form2_StreamColorButton_Click;
            form2.AlfaChannelChanged += Form2_AlfaChannelChanged;
            //form2.MonoRadioButton.CheckedChanged += Form4_SizeChanged;
            form1.DispatchAnalyzerLevelChanged += Form1_ReceiveSpectrumData;
            //form1.SpectrumCleared += Form4_SizeChanged;

            Form4_SizeChanged(this, EventArgs.Empty);

            streamPen = new Pen(new Color(), 1f);
            startPen = new Pen(new Color(), 2f);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (form2.HideTitleCheckBox.Checked)
                FormBorderStyle = FormBorderStyle.None;
            else
                FormBorderStyle = FormBorderStyle.SizableToolWindow;

            Width = form2.form4Width - (FormBorderStyle == FormBorderStyle.None ? 0 : form2.defaultBorderSize * 2);
            Height = form2.form4Height - (FormBorderStyle == FormBorderStyle.None ? 0 : form2.defaultTitleHeight + form2.defaultBorderSize);
            Top = form2.form4Top;
            Left = form2.form4Left;

            pow = form2.pow;

            form2.AlfaTextBox.Text = streamPen.Color.A.ToString();

            if (!Form1.inInit) Form4_SizeChanged(this, e);      // to decide baseLine

            if (Visible)
            {
                form2.HideSpectrumWindowCheckBox.Enabled =
                form2.HideSpectrumWindowCheckBox.Visible = true;
            }
        }

        private void Form1_ReceiveSpectrumData(object sender, EventArgs e)
        {
            var canvas = new Bitmap(StreamPictureBox.Width, StreamPictureBox.Height);
            var g = Graphics.FromImage(canvas);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            int drawPointX = StreamPictureBox.Width - 1 - (int)(startPen.Width / 2) - 3;      // Why 3?
            Rectangle srcRect = new Rectangle(streamScrollUnit + 0, 0, drawPointX - (streamScrollUnit - 1), canvas.Height);
            Rectangle destRect = new Rectangle(0, 0, drawPointX - (streamScrollUnit - 1), canvas.Height);

            g.DrawImage(previousBitmap, destRect, srcRect, GraphicsUnit.Pixel);
            g.DrawLine(startPen, StreamPictureBox.Right - (int)(startPen.Width) - 1, StreamPictureBox.Top, StreamPictureBox.Right - (int)(startPen.Width) - 1, StreamPictureBox.Bottom);

            int[] streamLevel = new int[Form2.maxChannel];
            float streamLevelL = (float)(Math.Pow(form1.level[0], pow) / Math.Pow(Form2.lmPBWidth, pow - 1));  // power-ing
            float streamLevelR = (float)(Math.Pow(form1.level[1], pow) / Math.Pow(Form2.lmPBWidth, pow - 1));
            streamLevel[0] = (int)(streamLevelL * (streamBaseLine[0] + 1) / Form2.lmPBWidth);                  //analyzer._level = 0 -390
            streamLevel[1] = (int)(streamLevelR * (streamBaseLine[0] + 1) / Form2.lmPBWidth);                  //analyzer._level = 0 -390, value calcurated from same point as left ch.

            if (streamCombineMode == 2)
            {
                g.DrawLine(streamPen, drawPointX, streamBaseLine[0] + streamLevel[0], drawPointX, streamBaseLine[0] - streamLevel[0]);
                g.DrawLine(streamPen, drawPointX, streamBaseLine[1] + streamLevel[1], drawPointX, streamBaseLine[1] - streamLevel[1]);

            }
            else
            {
                g.DrawLine(streamPen, drawPointX, streamBaseLine[0] - streamLevel[0], drawPointX, streamBaseLine[0]);
                g.DrawLine(streamPen, drawPointX, streamBaseLine[0] + streamLevel[1] + streamChannelSpacing, drawPointX, streamBaseLine[0] + streamChannelSpacing);
            }

            StreamPictureBox.Image = previousBitmap = canvas;

            //label1.Text = form1.level[0].ToString("0");
        }

        private void Form2_CombineStreamRadioButton_CheckedChanged(object sender, EventArgs e)
        //private void Form2_SeparateStreamRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            streamCombineMode = form2.CombineStreamRadioButton.Checked ? 1 : 2;
            Form4_SizeChanged(sender, e);
        }

        private void Form2_StreamColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog
            {
                AnyColor = true,
                FullOpen = false,
                ShowHelp = false,
                SolidColorOnly = false,
            };
            if (cd.ShowDialog() == DialogResult.OK)
                streamPen.Color = Color.FromArgb(streamPen.Color.A, cd.Color);
        }

        private void Form2_AlfaChannelChanged(object sender, EventArgs e)
        {
            streamPen.Color = Color.FromArgb(Int16.Parse(form2.AlfaTextBox.Text), streamPen.Color.R, streamPen.Color.G, streamPen.Color.B);
        }

        private void Form4_SizeChanged(object sender, EventArgs e)
        {
            if (Form1.inLayout) return;
            StreamPictureBox.Top = StreamPictureBox.Left = 0;
            StreamPictureBox.Size = new Size(ClientSize.Width, ClientSize.Height);
            //if (form2.MonoRadioButton.Checked)
            //    streamCombineMode = 1;
            //else
            streamCombineMode = form2.CombineStreamRadioButton.Checked ? 1 : 2;
            
            for (int i = 0; i < streamCombineMode; i++)
            {
                streamBaseLine[i] = ((StreamPictureBox.Height - 1) - streamChannelSpacing) / Form2.maxChannel / streamCombineMode;
                streamBaseLine[i] = streamBaseLine[i] * ((i + 1) * 2 - 1) + (streamChannelSpacing + 1) * i;
            }

            previousBitmap = new Bitmap(StreamPictureBox.Width, StreamPictureBox.Height);
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_Closed?.Invoke(sender, e);

            form2.form4Width = Width;
            form2.form4Height = Height;
            form2.form4Top = Top;
            form2.form4Left = Left;
            if (e.CloseReason == CloseReason.UserClosing)
                form2.LevelStreamCheckBox.Checked = false;
        }

        private void StreamPictureBox_MouseDown(object sender, MouseEventArgs e)
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

        private void StreamPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                int minWidth = 128;
                int minHeight = 39;

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

        private void StreamPictureBox_MouseHover(object sender, EventArgs e)
        {
            inFormSizeChange = false;
            Cursor = Cursors.Default;
        }

        private void StreamPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Cursor != Cursors.Default)
                Cursor = Cursors.Default;
            pinch.left = pinch.right = pinch.top = pinch.bottom = false;
        }
        
        private void StreamPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (!form2.Visible) form2.Visible = true;
        }
    }
}
