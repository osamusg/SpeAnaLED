using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpeAnaLED
{
    public partial class Form4 : Form
    {
        private readonly Form1 myParent;
        private readonly bool hideTitle;
        private Point mousePoint = new Point(0, 0);
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
            if (e.Button == MouseButtons.Left)
                mousePoint = new Point(e.X, e.Y);
            else if (e.Button == MouseButtons.Right)
                PictureBox_MouseDown?.Invoke(sender, e);
            if (this.Cursor != Cursors.Default)
                inFormSizeChange = true;
            else
                inFormSizeChange = false;
        }

        private void StreamPictureBox_MouseMove(object sender, MouseEventArgs e)
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
            else if (this.FormBorderStyle == FormBorderStyle.None)
            {
                if (e.X < 8 || e.X > this.Width - 8)
                    this.Cursor = Cursors.SizeWE;
                else if (e.Y < 8 || e.Y > this.Height - 8)
                    this.Cursor = Cursors.SizeNS;
                else if (!inFormSizeChange)
                    this.Cursor = Cursors.Default;
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
    }
}
