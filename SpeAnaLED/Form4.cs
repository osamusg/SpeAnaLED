using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpeAnaLED
{
    public partial class Form4 : Form
    {
        private readonly Form1 myParent;
        private readonly bool hideTitle;
        private Point mouseDragStartPoint = new Point(0, 0);
        private bool pinchLeft, pinchRight, pinchTop, pinchBottom;
        private bool inFormSizeChange;

        // event handler (Fire)
        public event EventHandler Form_SizeChanged;
        public event MouseEventHandler PictureBox_MouseDown;
        public event FormClosedEventHandler Form_Closed;

        public Form4(Form1 parent, bool hidetitle)
        {
            InitializeComponent();

            myParent = parent;
            this.hideTitle = hidetitle;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Form4_SizeChanged(this, EventArgs.Empty);
            if (hideTitle)
                this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Form4_SizeChanged(object sender, EventArgs e)
        {
            Form_SizeChanged?.Invoke(sender, EventArgs.Empty);      // baseLine calcurate in parent form1
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!myParent.Visible) myParent.Visible = true;
            Form_Closed?.Invoke(sender, e);
        }

        private void StreamPictureBox_MouseDown(object sender, MouseEventArgs e)
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

        private void StreamPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (!myParent.Visible) myParent.Visible = true;
        }

        private void StreamPictureBox_MouseHover(object sender, EventArgs e)
        {
            inFormSizeChange = false;
            this.Cursor = Cursors.Default;
        }

        private void StreamPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Cursor != Cursors.Default)
                Cursor = Cursors.Default;
            pinchLeft = pinchRight = pinchTop = pinchBottom = false;
        }
    }
}
