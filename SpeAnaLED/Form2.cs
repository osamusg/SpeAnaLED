using System;
using System.Windows.Forms;
using System.Drawing;

namespace SpeAnaLED
{
    public partial class Form2 : Form
    {
        private readonly Form1 form1;

        // common
        public int channel;
        public int numberOfBar;

        // layput
        public int borderSize;
        public int titleHeight;
        public readonly int defaultBorderSize;
        public readonly int defaultTitleHeight;
        public int form3Top, form3Left;                             // for load config
        public int form3Width, form3Height;                         // for load config
        public int form4Top, form4Left;                             // for load config
        public int form4Width, form4Height;                         // for load config
        
        // drawing
        private int currentAlfaChannel;

        // calculations
        public int counterCycle;
        public int peakHoldTimeMsec;
        public float pow;
        public float[][] adjustTable;

        // constants
        public const int maxChannel = 2;
        public const int maxNumberOfBar = 32;
        public const int timerIntervalMilliSeconds = 25;
        public const float cycleMultiplyer = 50f / maxNumberOfBar / maxChannel; // 0.78125f = const50 / maxNumberOfBars / maxChannels
        public const int lmPBWidth = 13 * (24 + 6);                 // level meter PictureBox width = 390 = 13LEDs * (24+6)px
        private const string gitUri = "https://github.com/osamusg/SpeAnaLED";

        // event handler (Fire)
        public event EventHandler ClearSpectrum;
        public event EventHandler AlfaChannelChanged;
        public event EventHandler Form_DoubleClick;
        public event EventHandler CounterCycleChanged;
        public event EventHandler NumberOfChannelChanged;
        public event EventHandler NumberOfBarChanged;
        public event EventHandler DeviceReloadRequested;

        public Form2(Form1 _form1)
        {
            InitializeComponent();

            form1 = _form1;
            RelLabel.Text = form1.relText;
            adjustTable = new float[2][] { new float[6], new float[6] };

            // subscribe
            form1.KeyDown += Form2_KeyDown;
            
            borderSize = defaultBorderSize = (form1.Width - form1.ClientSize.Width) / 2;
            titleHeight = defaultTitleHeight = form1.Height - form1.ClientSize.Height - borderSize * 2;

        }

        public void Init()
        { 
            // after config loaded,

            // Params
            counterCycle = (int)(peakHoldTimeMsec / timerIntervalMilliSeconds * numberOfBar * channel * cycleMultiplyer);

            // Controls
            PeakholdTimeComboBox.SelectedIndex = PeakholdTimeComboBox.Items.IndexOf(peakHoldTimeMsec.ToString());
            SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
            PeakholdDescentSpeedTextBox.Text = PeakholdDescentSpeedTrackBar.Value.ToString();
            LevelSensitivityTextBox.Text = (LevelSensitivityTrackBar.Value / 10f).ToString("0.0");
            HideSpectrumWindowCheckBox.Checked = !form1.form1Visible;
            if (numberOfBar == 32) NumberOfBar32RadioButton.Checked = true;
            else if (numberOfBar == 16) NumberOfBar16RadioButton.Checked = true;
            else if (numberOfBar == 8) NumberOfBar8RadioButton.Checked = true;
            else NumberOfBar4RadioButton.Checked = true;

            // Enabling
            if (channel < 2) ChannelLayoutGroup.Enabled = false;
            LabelPeakhold.Enabled = LabelMsec.Enabled = PeakholdTimeComboBox.Enabled = PeakholdCheckBox.Checked;
            VerticalFlipCheckBox.Enabled = VerticalRadioButton.Checked;
            FlipGroup.Enabled = HorizontalRadioButton.Checked;
            HideSpectrumWindowCheckBox.Enabled = (LevelMeterCheckBox.Checked || LevelStreamCheckBox.Checked);
            LevelStreamPanel.Enabled = LevelStreamCheckBox.Checked;

#if DEBUG
            ShowCounterCheckBox.Visible = true;
            label2.Visible = true;
#endif
        }

        private void NumberOfBarRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            int nob = 32;
            if (NumberOfBar16RadioButton.Checked) nob = 16;
            else if (NumberOfBar8RadioButton.Checked) nob = 8;
            else if (NumberOfBar4RadioButton.Checked) nob = 4;
            numberOfBar = nob;
            counterCycle = (int)(peakHoldTimeMsec / timerIntervalMilliSeconds * numberOfBar * channel * cycleMultiplyer);
            NumberOfBarChanged?.Invoke(this, EventArgs.Empty);  // == Also CounterCycle will be changed
        }

        private void PeakholdTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            peakHoldTimeMsec = Convert.ToInt16(PeakholdTimeComboBox.SelectedItem);
            counterCycle = (int)(peakHoldTimeMsec / timerIntervalMilliSeconds * numberOfBar * channel * cycleMultiplyer);
            CounterCycleChanged?.Invoke(this, EventArgs.Empty);
        }

        private void PeakholdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PeakholdCheckBox.Checked)
                PeakholdTimeComboBox.Enabled =
                LabelPeakhold.Enabled =
                LabelMsec.Enabled =
                PeakholdDescentSpeedLabel.Enabled =
                PeakholdDescentSpeedTrackBar.Enabled =
                PeakholdDescentSpeedTextBox.Enabled = true;
            else
                PeakholdTimeComboBox.Enabled =
                LabelPeakhold.Enabled =
                LabelMsec.Enabled =
                PeakholdDescentSpeedLabel.Enabled =
                PeakholdDescentSpeedTrackBar.Enabled =
                PeakholdDescentSpeedTextBox.Enabled = false;
        }

        private void PeakholdDescentSpeedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PeakholdDescentSpeedTextBox.Text = PeakholdDescentSpeedTrackBar.Value.ToString();
        }

        private void PeakholdDescentSpeedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;
            
            e.SuppressKeyPress = true;
            PeakholdDescentSpeedTextBox_Leave(sender, e);
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

        private void SensitivityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SensitivityTextBox.Text = (SensitivityTrackBar.Value / 10f).ToString("0.0");
        }

        private void SensitivityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;

            e.SuppressKeyPress = true;
            SensitivityTextBox_Leave(sender, e);
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

        private void LevelSensitivityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            LevelSensitivityTextBox.Text = (LevelSensitivityTrackBar.Value / 10f).ToString("0.0");
        }

        private void LevelSensitivityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return) return;

            e.SuppressKeyPress = true;
            LevelSensitivityTextBox_Leave(sender, e);
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

        private void EnumerateButton_Click(object sender, EventArgs e)
        {
            ClearSpectrum?.Invoke(this, EventArgs.Empty);
        }

        private void DeviceReloadButton_Click(object sender, EventArgs e)
        {
            DeviceReloadRequested?.Invoke(this, EventArgs.Empty);
            ClearSpectrum?.Invoke(this, EventArgs.Empty);
        }

        private void MonoRadioButton_CheckedChanged(object sender, EventArgs e)
        //private void StereoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            channel = MonoRadioButton.Checked ? 1 : 2;
            counterCycle = (int)(peakHoldTimeMsec / timerIntervalMilliSeconds * numberOfBar * channel * cycleMultiplyer);
            NumberOfChannelChanged?.Invoke(this, EventArgs.Empty);
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

        private void Form2_DoubleClick(object sender, EventArgs e)
        {
            Form_DoubleClick?.Invoke(sender, EventArgs.Empty);
        }

        protected internal void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;      // suppress bell rings
                this.Visible = false;
                form1.Activate();               // prevent form1 go behind
            }
            else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
                NumberOfBar32RadioButton.Checked = true;
            else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
                NumberOfBar16RadioButton.Checked = true;
            else if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
                NumberOfBar8RadioButton.Checked = true;
            else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
                NumberOfBar4RadioButton.Checked = true;
            else if (e.KeyCode == Keys.L)
                PrisumRadioButton.Checked = true;
            else if (e.KeyCode == Keys.C)
                ClassicRadioButton.Checked = true;
            else if (e.KeyCode == Keys.S)
                SimpleRadioButton.Checked = true;
            else if (e.KeyCode == Keys.R)
                RainbowRadioButton.Checked = true;
            else if (e.KeyCode == Keys.O)
                this.Visible = !this.Visible;
            else if (e.KeyCode == Keys.P)
                SSaverCheckBox.Checked = !SSaverCheckBox.Checked;
            else if (e.KeyCode == Keys.D)
            {
                DeviceReloadRequested?.Invoke(this, EventArgs.Empty);
                ClearSpectrum?.Invoke(this, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.Q)
            {
                var result = MessageBox.Show(
                "Quit?",
                "Quit",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
                );
                if (result == DialogResult.OK)
                    Application.Exit();
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(gitUri);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
