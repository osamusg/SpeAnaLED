using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using System.Windows.Media;

namespace SpeAnaLED
{
    public partial class Form3 : Form
    {
        public const int baseClientWidth = 436;
        public const int baseClientHeight = 65;

        private readonly Form1 myParent;
        private readonly bool hideTitle;
        private Point mousePoint = new Point(0, 0);
        public readonly Bitmap[] canvas = new Bitmap[2];
        private bool inFormSizeChange;
        private readonly string panelFontName = "Levenim MT Bold";

        // event handler (Fire)
        public event EventHandler Form3_Closed;

        public Form3(Form1 parent,bool hidetitle)
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
            System.Drawing.Brush greenBrush = new SolidBrush(System.Drawing.Color.FromArgb(96, 194, 96));
            System.Drawing.Brush redBrush = new SolidBrush(System.Drawing.Color.FromArgb(160, 0, 0));
            g_client.InterpolationMode = InterpolationMode.HighQualityBicubic;
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
            var brush = new SolidBrush(System.Drawing.Color.FromArgb(96, 194, 96));        //System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255,144, 238, 144))
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
            if (Form3_Closed != null) Form3_Closed(sender, e);
        }

        private void LevelPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mousePoint = new Point(e.X, e.Y);
            if (this.Cursor != Cursors.Default)
                inFormSizeChange = true;
            else
                inFormSizeChange = false;
        }

        private void LevelPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            int minWidth = 64;
            int minHeight = 32;
            
            if (e.Button == MouseButtons.Left)
            {
                if (this.Cursor == Cursors.SizeWE)
                {
                    if (e.X < this.Width / 2)
                    {
                        this.Width -= e.X - mousePoint.X;
                        if (this.Width < minWidth) this.Width = minWidth;
                        else this.Left += e.X - mousePoint.X;
                    }
                    else
                    {
                        this.Width += (e.X - mousePoint.X) / 25;
                        if (this.Width < minWidth) this.Width = minWidth;
                    }
                }
                else if (this.Cursor == Cursors.SizeNS)
                {
                    if (e.Y < this.Height / 3)
                    {
                        this.Height -= (e.Y - mousePoint.Y) / 15;
                        if (this.Height < minHeight) this.Height = minHeight;
                        else this.Top += (e.Y - mousePoint.Y) / 15;
                    }
                    else if (e.Y > this.Height * 1 / 2)
                    {
                        this.Height += (e.Y - mousePoint.Y) / 25;
                        if (this.Height < minHeight) this.Height = minHeight;
                    }
                }
                else
                {
                    this.Left += e.X - mousePoint.X;
                    this.Top += e.Y - mousePoint.Y;
                }
            }
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                if (e.X < 8 || e.X > this.Width - 8)
                    this.Cursor = Cursors.SizeWE;
                else if (e.Y < 8 || e.Y > this.Height - 8)
                    this.Cursor = Cursors.SizeNS;
                else if (!inFormSizeChange)
                    this.Cursor = Cursors.Default;
            }
        }

        private void LevelPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (!myParent.Visible) myParent.Visible = true;
        }

        private void LevelPictureBox_MouseHover(object sender, EventArgs e)
        {
            inFormSizeChange = false;
            this.Cursor = Cursors.Default;
        }
    }
}
