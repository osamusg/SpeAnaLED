using System;
using System.Drawing.Text;
using System.Windows.Forms;

namespace SpeAnaLED
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1005:デリゲート呼び出しを簡素化できます。", Justification = "<保留中>")]

    public partial class Form2 : Form
    {
        // go public controls for Analyzer class
        public ComboBox Devicelist { get { return devicelist; } }
       
        // event handler
        public event EventHandler ClearSpectrum;

        private const string gitUri = "https://github.com/osamusg/SpeAnaLED";

        public Form2()
        {
            InitializeComponent();
        }

        public static bool AutoReloadCheckBoxChecked { get { return AutoReloadCheckBoxChecked; } }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;      // suppress bell rings
                this.Visible = false;
                this.Owner.Activate();          // prevent form1 go behind
            }
        }

        private void Form2_DoubleClick(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void NumberOfBarComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClearSpectrum != null) ClearSpectrum(this, EventArgs.Empty);
            if (devicelist.SelectedIndex == -1)
            {
                devicelist.Items.Clear();
                devicelist.Items.Add("Please Enumerate Devices");
                devicelist.SelectedIndex = 0;
            }
        }

        private void SensitivityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
        }

        private void SensitivityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;      // suppress bell rings
                try
                {
                    int SensitivityChangedValue = Convert.ToInt16(float.Parse(SensitivityTextBox.Text) * 10f);
                    if (SensitivityChangedValue >= 10 && SensitivityChangedValue < 100)
                        SensitivityTrackBar.Value = SensitivityChangedValue;
                    SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
                }
                catch
                {
                    SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
                }
            }
        }

        private void PeakholdDescentSpeedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PeakholdDescentSpeedTextBox.Text = PeakholdDescentSpeedTrackBar.Value.ToString();
        }

        private void PeakholdDescentSpeedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;      // suppress bell rings
                try
                {
                    int DescentSpeedChangeValue = Convert.ToInt16(PeakholdDescentSpeedTextBox.Text);
                    if (DescentSpeedChangeValue >= 4 && DescentSpeedChangeValue <= 20)
                        PeakholdDescentSpeedTrackBar.Value = DescentSpeedChangeValue;
                    else PeakholdDescentSpeedTextBox.Text = PeakholdDescentSpeedTrackBar.Value.ToString();
                }
                catch
                {
                    PeakholdDescentSpeedTextBox.Text = PeakholdDescentSpeedTrackBar.Value.ToString();
                }
            }

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(gitUri);
        }

        private void EnumerateButton_Click(object sender, EventArgs e)
        {
            if (ClearSpectrum != null) ClearSpectrum(this, EventArgs.Empty);
        }

        private void DeviceResetButton_Click(object sender, EventArgs e)
        {
            if (ClearSpectrum != null) ClearSpectrum(this, EventArgs.Empty);
        }

        private void MonoRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (ClearSpectrum != null) ClearSpectrum(this, EventArgs.Empty);
        }
    }
}
