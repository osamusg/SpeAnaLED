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
        private bool pinchLeft, pinchRight, pinchTop, pinchBottom;
        private bool inFormSizeChange;
        private Bitmap previousBitmap;
        private readonly int[] streamBaseLine = { 0, 0 };
        public float pow;
        public int streamCombineMode;                               // combine:1 separate:2
        public Pen streamPen, startPen;
        public int streamScrollUnit = 1;                           // 1 2 4?, For test
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
            form2.SeparateStreamRadioButton.CheckedChanged += Form2_CombineStreamRadioButton_CheckedChanged;
            form2.StreamColorButton.Click += Form2_StreamColorButton_Click;
            form2.AlfaChannelChanged += Form2_AlfaChannelChanged;
            form1.DispatchAnalyzerLevelChanged += Form1_ReceiveSpectrumData;

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

            Width = form2.form4Width - (FormBorderStyle == FormBorderStyle.None ? form2.defaultBorderSize * 2 : 0);
            Height = form2.form4Height - (FormBorderStyle == FormBorderStyle.None ? form2.defaultTitleHeight + form2.defaultBorderSize : 0);
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
            int mdt = 8;    // mouse detect thickness (inner)

            if (e.Button == MouseButtons.Right)
            {
                form2.Visible = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (FormBorderStyle == FormBorderStyle.None)
                {
                    mouseDragStartPoint = new Point(e.X, e.Y);

                    pinchLeft = e.X < mdt;
                    pinchRight = e.X > Width - mdt;
                    pinchTop = e.Y < mdt;
                    pinchBottom = e.Y > Height - mdt;
                    inFormSizeChange = pinchLeft || pinchRight || pinchTop || pinchBottom;
                    if (!inFormSizeChange) Cursor = Cursors.SizeAll;
                }
            }
        }

        private void StreamPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                int minWidth = 64;
                int minHeight = 32;
                int mdt = 8;        // mouse detect thickness

                if (e.Button == MouseButtons.Left)
                {
                    if (Cursor == Cursors.SizeWE)
                    {
                        if (pinchLeft)
                        {
                            Width -= e.X - mouseDragStartPoint.X;
                            if (Width < minWidth) Width = minWidth;
                            else Left += e.X - mouseDragStartPoint.X;
                        }
                        else if (pinchRight)
                        {
                            Width = e.X;
                            if (Width < minWidth) Width = minWidth;
                        }
                    }
                    else if (Cursor == Cursors.SizeNS)
                    {
                        if (pinchTop)
                        {
                            Height -= e.Y - mouseDragStartPoint.Y;
                            if (Height < minHeight) Height = minHeight;
                            else Top += e.Y - mouseDragStartPoint.Y;
                        }
                        else if (pinchBottom)
                        {
                            Height = e.Y;
                            if (Height < minHeight) Height = minHeight;
                        }
                    }
                    else
                    {
                        Left += e.X - mouseDragStartPoint.X;
                        Top += e.Y - mouseDragStartPoint.Y;
                    }
                }

                if (e.X < mdt || e.X > Width - mdt)
                    Cursor = Cursors.SizeWE;
                else if (e.Y < mdt || e.Y > Height - mdt)
                    Cursor = Cursors.SizeNS;
                else if (Cursor != Cursors.SizeAll && e.Button == MouseButtons.None)
                    Cursor = Cursors.Default;
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
            pinchLeft = pinchRight = pinchTop = pinchBottom = false;
        }
        
        private void StreamPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (!form2.Visible) form2.Visible = true;
        }
    }
}
