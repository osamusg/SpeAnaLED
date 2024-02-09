using System;
using System.Windows.Forms;

namespace SpeAnaLED
{
    public partial class Form2 : Form
    {
        // go public controls for Analyzer class
        public ComboBox Devicelist { get { return devicelist; } }
       
        // event handler (Fire)
        public event EventHandler ClearSpectrum;
        public event EventHandler AlfaChannelChanged;
        public event EventHandler Form_DoubleClick;

        private const string gitUri = "https://github.com/osamusg/SpeAnaLED";
        private static bool autoReloadChecked;
        public int currentAlfaChannel;

        public Form2()
        {
            InitializeComponent();
        }

        public static bool AutoReloadChecked { get { return autoReloadChecked; } }

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
            Form_DoubleClick?.Invoke(sender, EventArgs.Empty);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void NumberOfBarComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearSpectrum?.Invoke(this, EventArgs.Empty);
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
                    if (SensitivityChangedValue != SensitivityTrackBar.Value)
                    {
                        if (SensitivityChangedValue >= 10 && SensitivityChangedValue < 100)
                            SensitivityTrackBar.Value = SensitivityChangedValue;
                        else
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                            SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
                        }
                    }
                }
                catch
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
                }
            }
        }

        private void SensitivityTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int SensitivityChangeValue = Convert.ToInt16(float.Parse(SensitivityTextBox.Text) * 10f);
                if (SensitivityChangeValue != SensitivityTrackBar.Value)
                {
                    if (SensitivityChangeValue >= 10 && SensitivityChangeValue < 100)
                        SensitivityTrackBar.Value = SensitivityChangeValue;
                    else
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                        SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
                    }
                }
            }
            catch
            {
                System.Media.SystemSounds.Exclamation.Play();
                SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
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
                    if (DescentSpeedChangeValue != PeakholdDescentSpeedTrackBar.Value)
                    {
                        if (DescentSpeedChangeValue >= 4 && DescentSpeedChangeValue <= 20)
                            PeakholdDescentSpeedTrackBar.Value = DescentSpeedChangeValue;
                        else
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                            PeakholdDescentSpeedTextBox.Text = PeakholdDescentSpeedTrackBar.Value.ToString();
                        }
                    }
                }
                catch
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    PeakholdDescentSpeedTextBox.Text = PeakholdDescentSpeedTrackBar.Value.ToString();
                }
            }

        }

        private void PeakholdDescentSpeedTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int DescentSpeedChangeValue = Convert.ToInt16(PeakholdDescentSpeedTextBox.Text);
                if (DescentSpeedChangeValue != PeakholdDescentSpeedTrackBar.Value)
                {
                    if (DescentSpeedChangeValue >= 4 && DescentSpeedChangeValue <= 20)
                        PeakholdDescentSpeedTrackBar.Value = DescentSpeedChangeValue;
                    else
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                        PeakholdDescentSpeedTextBox.Text = PeakholdDescentSpeedTrackBar.Value.ToString();
                    }
                }
            }
            catch
            {
                System.Media.SystemSounds.Exclamation.Play();
                PeakholdDescentSpeedTextBox.Text = PeakholdDescentSpeedTrackBar.Value.ToString();
            }
        }

        private void LevelSensitivityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            LevelSensitivityTextBox.Text = (LevelSensitivityTrackBar.Value / 10f).ToString("0.0");
        }

        private void LevelSensitivityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                try
                {
                    int LevelSensitivityChangeValue = Convert.ToInt16(float.Parse(LevelSensitivityTextBox.Text) * 10f);
                    if (LevelSensitivityChangeValue != LevelSensitivityTrackBar.Value)
                    {
                        if (LevelSensitivityChangeValue >= 10 && LevelSensitivityChangeValue <= 20)
                            LevelSensitivityTrackBar.Value = LevelSensitivityChangeValue;
                        else
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                            LevelSensitivityTextBox.Text = (LevelSensitivityTrackBar.Value / 10f).ToString("0.0");
                        }
                    }
                }
                catch
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    LevelSensitivityTextBox.Text = (LevelSensitivityTrackBar.Value / 10f).ToString("0.0");
                }
            }
        }

        private void LevelSensitivityTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int LevelSensitivityChangeValue = Convert.ToInt16(float.Parse(LevelSensitivityTextBox.Text) * 10f);
                if (LevelSensitivityChangeValue != LevelSensitivityTrackBar.Value)
                {
                    if (LevelSensitivityChangeValue >= 10 && LevelSensitivityChangeValue <= 20)
                        LevelSensitivityTrackBar.Value = LevelSensitivityChangeValue;
                    else
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                        LevelSensitivityTextBox.Text = (LevelSensitivityTrackBar.Value / 10f).ToString("0.0");
                    }
                }
            }
            catch
            {
                System.Media.SystemSounds.Exclamation.Play();
                LevelSensitivityTextBox.Text = (LevelSensitivityTrackBar.Value / 10f).ToString("0.0");
            }

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(gitUri);
        }

        private void EnumerateButton_Click(object sender, EventArgs e)
        {
            ClearSpectrum?.Invoke(this, EventArgs.Empty);
        }

        private void DeviceResetButton_Click(object sender, EventArgs e)
        {
            ClearSpectrum?.Invoke(this, EventArgs.Empty);
        }

        private void MonoRadio_CheckedChanged(object sender, EventArgs e)
        {
            ClearSpectrum?.Invoke(this, EventArgs.Empty);
        }

        private void AutoReloadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            autoReloadChecked = AutoReloadCheckBox.Checked;
        }

        private void DeviceResetButton_EnabledChanged(object sender, EventArgs e)
        {
            AutoReloadCheckBox.Enabled = DeviceResetButton.Enabled;
        }

        private void AlfaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;      // suppress bell rings
                try
                {
                    int alfaChannelValue = Convert.ToInt16(AlfaTextBox.Text);
                    if (alfaChannelValue != currentAlfaChannel)
                    {
                        if (alfaChannelValue >= 0 && alfaChannelValue <= 255)
                        {
                            currentAlfaChannel = alfaChannelValue;
                            AlfaChannelChanged?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                            AlfaTextBox.Text = currentAlfaChannel.ToString();
                        }
                    }
                }
                catch
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    AlfaTextBox.Text = currentAlfaChannel.ToString();
                }
            }
        }

        private void AlfaTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int alfaChannelValue = Convert.ToInt16(AlfaTextBox.Text);
                if (alfaChannelValue != currentAlfaChannel)
                {
                    if (alfaChannelValue >= 0 && alfaChannelValue <= 255)
                    {
                        currentAlfaChannel = alfaChannelValue;
                        AlfaChannelChanged?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                        AlfaTextBox.Text = currentAlfaChannel.ToString();
                    }
                }
            }
            catch
            {
                System.Media.SystemSounds.Exclamation.Play();
                AlfaTextBox.Text = currentAlfaChannel.ToString();
            }
        }
    }
}
