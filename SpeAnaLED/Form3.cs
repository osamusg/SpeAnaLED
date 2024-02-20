using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpeAnaLED
{
    public partial class Form3 : Form
    {
        public const int baseClientWidth = 436;
        public const int baseClientHeight = 65;

        private readonly Form1 myParent;
        private readonly bool hideTitle;
        private Point mouseDragStartPoint = new Point(0, 0);
        private bool pinchLeft, pinchRight, pinchTop, pinchBottom;
        public readonly Bitmap[] canvas = new Bitmap[2];
        private bool inFormSizeChange;
        private readonly string panelFontName = "Levenim MT Bold";

        // event handler (Fire)
        public event FormClosedEventHandler Form_Closed;
        public event MouseEventHandler PictureBox_MouseDown;

        public Form3(Form1 parent, bool hidetitle)
        {
            InitializeComponent();

            myParent = parent;
            this.hideTitle = hidetitle;
        }

        private void Form3_Load(object sender, System.EventArgs e)
        {
            FormPictureBox.SendToBack();
            FormPictureBox.Controls.Add(LevelPictureBox);
            
            Draw_Panel();
            if (hideTitle)
                this.FormBorderStyle = FormBorderStyle.None;
        }

         private void Draw_Panel()
         {
            Font panelFont = new Font(panelFontName, 9f, FontStyle.Bold);
            
            FormPictureBox.Top = FormPictureBox.Left = 0;
            FormPictureBox.Size = new Size(ClientSize.Width, ClientSize.Height);
            Bitmap form3ClientCanvas = new Bitmap(baseClientWidth, baseClientHeight);
            Graphics g_client = Graphics.FromImage(form3ClientCanvas);
            Brush greenBrush = new SolidBrush(Color.FromArgb(96, 194, 96));
            Brush redBrush = new SolidBrush(Color.FromArgb(160, 0, 0));
            g_client.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g_client.DrawString("-        -20   -15    -10    -7      -5     -3     -1", panelFont, greenBrush, 30, 24);
            g_client.DrawString("0     +1     +3     +5    +8", panelFont, redBrush, 276, 24);
            g_client.DrawString("L", panelFont, greenBrush, 10, 11);
            //g_client.DrawString("db", panelFont, greenBrush, 10, 24);
            g_client.DrawString("R", panelFont, greenBrush, 10, 38);
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

            g_client.DrawImage(infinityCanvas, 36, 27, (int)(infinityFontWidth * 1.2), 16);
            g_client.Dispose();

            FormPictureBox.Image = form3ClientCanvas;
            FormPictureBox.Update();
            FormPictureBox.SendToBack();
        }

        private void Form3_SizeChanged(object sender, System.EventArgs e)
        {
            Draw_Panel();
            LevelPictureBox.Width = ClientSize.Width;
            LevelPictureBox.Height = ClientSize.Height;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!myParent.Visible) myParent.Visible = true;
            Form_Closed?.Invoke(sender, e);
        }

        private void LevelPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            int mdt = 8;    // mouse detect thickness (inner)
            
            if (e.Button == MouseButtons.Right)
            {
                PictureBox_MouseDown?.Invoke(sender, e);
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

        private void LevelPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.None)
            {
                int minWidth = 64;
                int minHeight = 32;
                int mdt = 8;    // mouse detect thickness

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

        private void LevelPictureBox_MouseHover(object sender, EventArgs e)
        {
            inFormSizeChange = false;
            this.Cursor = Cursors.Default;
        }

        private void LevelPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Cursor != Cursors.Default)
                Cursor = Cursors.Default;
            pinchLeft = pinchRight = pinchTop = pinchBottom = false;
        }

        private void LevelPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (!myParent.Visible) myParent.Visible = true;
        }
    }
}
