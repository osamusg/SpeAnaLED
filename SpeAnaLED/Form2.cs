using System;
using System.Windows.Forms;

namespace SpeAnaLED
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1005:デリゲート呼び出しを簡素化できます。", Justification = "<保留中>")]

    public partial class Form2 : Form
    {
        // go public controls
        public ComboBox Devicelist { get { return devicelist; } }
        
        // event handler
        public event EventHandler ClearSpectrum;

        const string gitUri = "https://github.com/osamusg/SpeAnaLED";

        public Form2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
        }

        private void TextBox_Sensitivity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;      // suppress bell rings
                try
                {
                    int SensitivityChangedValue = Convert.ToInt16(float.Parse(SensitivityTextBox.Text) * 10f);
                    if (SensitivityChangedValue >= 10 && SensitivityChangedValue < 100)
                        SensitivityTrackBar.Value = SensitivityChangedValue;
                    else SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
                }
                catch
                {
                    SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
                }
            }
        }

        private void TrackBar2_ValueChanged(object sender, EventArgs e)
        {
            DecaySpeedTextBox.Text = PeakholdDecayTimeTrackBar.Value.ToString();
        }

        private void TextBox_DecaySpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;      // suppress bell rings
                try
                {
                    int DecaySpeedChangeValue = Convert.ToInt16(DecaySpeedTextBox.Text);
                    if (DecaySpeedChangeValue >= 4 && DecaySpeedChangeValue <= 20)
                        PeakholdDecayTimeTrackBar.Value = DecaySpeedChangeValue;
                    else DecaySpeedTextBox.Text = PeakholdDecayTimeTrackBar.Value.ToString();
                }
                catch (Exception)
                {
                    DecaySpeedTextBox.Text = PeakholdDecayTimeTrackBar.Value.ToString();
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClearSpectrum != null) ClearSpectrum(this, EventArgs.Empty);
            if (devicelist.SelectedIndex == -1)
            {
                devicelist.Items.Add("Please Enumrate Devices");
                devicelist.SelectedIndex = 0;
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;      // suppress bell rings
            if (e.KeyCode == Keys.Escape)
                this.Visible = false;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(gitUri);
        }
    }
}
