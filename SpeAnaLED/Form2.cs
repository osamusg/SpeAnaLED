using System;
using System.Windows.Forms;

namespace SpeAnaLED
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1005:デリゲート呼び出しを簡素化できます。", Justification = "<保留中>")]

    public partial class Form2 : Form
    {
        private readonly string defaultSensibilityText = "7.8";     // Configから設定するようにする デザイナーとの連動用でスライダーも変更要
        private readonly string defaultDecaySpeedText = "10";       // Configから設定するようにする デザイナーとの連動用でスライダーも変更要
        private readonly bool RadioClassicChecked = true;
        private readonly bool SSPreventChecked = true;
        private readonly bool PeakholdChecked = true;
        private readonly bool AlwaysOnTopChecked = false;

        // go public controls
        //public ComboBox Form2_ComboBox1 { get { return this.ComboBox1; } }
        public TrackBar Form2_TrackBar1 { get { return this.TrackBar1; } }
        public TrackBar Form2_TrackBar2 { get { return this.TrackBar2; } }
        public RadioButton Form2_RadioClassic { get { return this.RadioClassic; } }
        public RadioButton Form2_RadioPrisum { get { return this.RadioPrisum; } }
        public RadioButton Form2_RadioSimple { get { return this.RadioSimple; } }
        public RadioButton Form2_RadioRainbow { get { return this.RadioRainbow; } }
        public ComboBox Form2_ComboBox1 { get { return this.ComboBox2; } }
        public CheckBox Form2_SSaverCheckBox { get { return this.SSaverCheckBox; } }
        public CheckBox Form2_PeakholdCheckbox { get { return this.PeakholdCheckBox; } }
        public CheckBox Form2_AlwaysOnTopCheckbox { get { return this.AlwaysOnTopCheckBox; } }

        public event EventHandler ClearSpectrum;


        public Form2()
        {
            InitializeComponent();

            //TextBox_Sensibility.Text = (TrackBar1.Value / 10f).ToString("0,0");   // Configから設定するようにする
            TextBox_Sensibility.Text = defaultSensibilityText;
            TextBox_DecaySpeed.Text = defaultDecaySpeedText;
            ComboBox1.SelectedIndex = 4;    // 16 Bars
            ComboBox2.SelectedIndex = 3;    // 2000ms
            
            Form2_RadioClassic.Checked = RadioClassicChecked;                       // Configから設定するようにする
            Form2_SSaverCheckBox.Checked = SSPreventChecked;
            Form2_PeakholdCheckbox.Checked = PeakholdChecked;
            Form2_AlwaysOnTopCheckbox.Checked = AlwaysOnTopChecked;

            // Event handler (subscribe)
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
