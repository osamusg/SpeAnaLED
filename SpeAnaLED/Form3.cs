using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using System.Windows.Media;
//using System.Windows.Media;

namespace SpeAnaLED
{
    public partial class Form3 : Form
    {
        private int baseClientWidth, baseClientHeight;

        private Point mousePoint = new Point(0, 0);
        public readonly Bitmap[] canvas = new Bitmap[2];
        //private readonly Label[] dbLabel = new Label[13];

        public Form3()
        {
            InitializeComponent();

            FormPictureBox.SendToBack();
        }

        private void Form3_Load(object sender, System.EventArgs e)
        {
            baseClientWidth = ClientSize.Width;
            baseClientHeight = ClientSize.Height;

            Panel_Draw();
            this.FormBorderStyle= FormBorderStyle.None;
        }

        private void Panel_Draw()
        {
            Font panelFont = new Font("Robot", 9f, FontStyle.Bold);
            
            FormPictureBox.Top = FormPictureBox.Left = 0;
            FormPictureBox.Size = new Size(ClientSize.Width, ClientSize.Height);
            Bitmap form3ClientCanvas = new Bitmap(baseClientWidth, baseClientHeight);
            Graphics g_client = Graphics.FromImage(form3ClientCanvas);
            System.Drawing.Brush greenBrush = new SolidBrush(System.Drawing.Color.FromArgb(96, 194, 96));
            System.Drawing.Brush redBrush = new SolidBrush(System.Drawing.Color.FromArgb(160, 0, 0));
            g_client.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g_client.DrawString("-       -20   -15    -10     -7     -5     -3     -1", panelFont, greenBrush, 32, 24);
            g_client.DrawString("0     +1     +3    +5     +8", panelFont, redBrush, 273, 24);
            g_client.DrawString("L", panelFont,greenBrush, 10,12);
            g_client.DrawString("db", panelFont, greenBrush, 10, 24);
            g_client.DrawString("R", panelFont, greenBrush, 10, 36);
            g_client.DrawString("PEAK LEVEL METER", new Font("Robot", 7f, FontStyle.Bold), greenBrush, 10, 1);


            Bitmap infinityCanvas = new Bitmap(16, 16);
            infinityCanvas.MakeTransparent();
            Graphics g_Infinity = Graphics.FromImage(infinityCanvas);
            float infinityFontWidth = g_Infinity.MeasureString("8", new Font("Robot", 11f, FontStyle.Bold)).Width;
            g_Infinity.TranslateTransform(0, infinityFontWidth);
            g_Infinity.RotateTransform(-90f);
            var brush = new SolidBrush(System.Drawing.Color.FromArgb(96, 194, 96));        //System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255,144, 238, 144))
            g_Infinity.DrawString("8", new Font("Robot", 12f, FontStyle.Bold), brush, 0, 0);
            g_Infinity.Dispose();

            g_client.DrawImage(infinityCanvas, 38, 27, (int)infinityFontWidth, 16);
            g_client.Dispose();

            FormPictureBox.Image = form3ClientCanvas;
            FormPictureBox.Update();
            FormPictureBox.SendToBack();
        }

        private void FormPictureBox_DoubleClick(object sender, System.EventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.SizableToolWindow)
                FormBorderStyle = FormBorderStyle.None;
            else FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        private void FormPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mousePoint = new Point(e.X, e.Y);

        }

        private void FormPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void Form3_SizeChanged(object sender, System.EventArgs e)
        {
            Panel_Draw();
        }

        private void Form3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.SizableToolWindow)
                FormBorderStyle = FormBorderStyle.None;
            else FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mousePoint = new Point(e.X, e.Y);
            else if (e.Button == MouseButtons.Right)
                Owner.Visible = true;
        }

        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void LeftPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mousePoint = new Point(e.X, e.Y);
        }

        private void LeftPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void RightPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mousePoint = new Point(e.X, e.Y);
        }

        private void RightPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }
    }
}
