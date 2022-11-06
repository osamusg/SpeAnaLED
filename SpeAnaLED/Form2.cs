using System;
using System.Windows.Forms;
using ConfigFile;

namespace SpeAnaLED
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1005:デリゲート呼び出しを簡素化できます。", Justification = "<保留中>")]

    public partial class Form2 : Form
    {
        // go public controls
        //public TrackBar Form2_TrackBar1 { get { return this.TrackBar1; } }
        //public TrackBar Form2_TrackBar2 { get { return TrackBar2; } }
        
        public event EventHandler ClearSpectrum;

        public Form2()
        {
            InitializeComponent();

            // 未実装対応
            RadioMono.Enabled = false;
            CheckBoxFlip.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            TextBox_Sensibility.Text = (TrackBar1.Value / 10f).ToString("0.0");
        }

        private void TextBox_Sensibility_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;      // suppress bell rings
                try
                {
                    int SensibilityChangedValue = Convert.ToInt16(float.Parse(TextBox_Sensibility.Text) * 10f);
                    if (SensibilityChangedValue >= 10 && SensibilityChangedValue < 100)
                        TrackBar1.Value = SensibilityChangedValue;
                    else TextBox_Sensibility.Text = (TrackBar1.Value / 10f).ToString("0.0");
                }
                catch
                {
                    TextBox_Sensibility.Text = (TrackBar1.Value / 10f).ToString("0.0");
                }
            }
        }

        private void TrackBar2_ValueChanged(object sender, EventArgs e)
        {
            TextBox_DecaySpeed.Text = TrackBar2.Value.ToString();
        }

        private void TextBox_DecaySpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;      // suppress bell rings
                try
                {
                    int DecaySpeedChangeValue = Convert.ToInt16(TextBox_DecaySpeed.Text);
                    if (DecaySpeedChangeValue >= 4 && DecaySpeedChangeValue <= 20)
                        TrackBar2.Value = DecaySpeedChangeValue;
                    else TextBox_DecaySpeed.Text = TrackBar2.Value.ToString();
                }
                catch (Exception)
                {
                    TextBox_DecaySpeed.Text = TrackBar2.Value.ToString();
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClearSpectrum != null) ClearSpectrum(this, EventArgs.Empty);
        }
    }
}
