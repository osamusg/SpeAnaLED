using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Threading;
using Un4seen.Bass;
using Un4seen.BassWasapi;

namespace SpeAnaLED
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1005:デリゲート呼び出しを簡素化できます。", Justification = "<保留中>")]

    //
    // based on h0uri's Analyzer.cs (https://www.instructables.com/Audio-Spectrum-Software-C/) 
    //
    internal class Analyzer
    {
        public float[] _fft;                            // buffer for fft data
        private bool _enable;                           // enabled status
        private bool _initialized;                      // initialized flag
        private readonly WASAPIPROC _process;           // callback function to obtain data
        public List<byte> _spectrumdata;                // spectrum data buffer
        public readonly DispatcherTimer _timer1;        // timer that refreshes the display
        private BASSData _DATAFLAG;                     // for "interreave" format
        public int _devicenumber;                       // device number
        public int[] _level;                            // progressbars for left and right channel intensity
        private int _lastlevel;                         // last output level
        private int _hangcontrol;                       // last output level counter
        private readonly bool _UNICODE;                 // codepage switch
        public readonly ComboBox _devicelist;           // for subscribe
        private readonly Button _form2EnumButton;       // for subscribe
        private readonly Button _form2DeviceResetButton;// for subscribe
        private readonly ComboBox _form2NumberOfBarsComboBox;   // for subscribe
        private readonly RadioButton _form2MonoRadio;   // for subscribe
        private readonly Label _form2FreqLabel;
        public event EventHandler SpectrumChanged;      // for fire
        public event EventHandler NumberOfChannelChanged;   // for fire
        private int _mixfreq;
        private float _mixfreqMultiplyer;
        private readonly float _freqShift = (float)Math.Round(Math.Log(20000/*hz*/, 2) - /*in increments of*/10, 2);
        public bool inInit = true;

        public int _channel;                            // 1: "mix-data"(mono) 2: L+R
        private int _lines;                             // number of spectrum lines
        private readonly int _form3PictureBoxWidth = 390;

        public Analyzer(ComboBox devicelist, Button enumButton, Button deviceResetButton, ComboBox numberOfbarsComboBox, RadioButton monoRadio, Label freqLabel)    // Control data for event subscribe
        {
            _devicelist = devicelist;
            _form2EnumButton = enumButton;
            _form2DeviceResetButton = deviceResetButton;
            _form2NumberOfBarsComboBox = numberOfbarsComboBox;
            _form2MonoRadio = monoRadio;
            _channel = _form2MonoRadio.Checked ? 1 : 2;
            _form2FreqLabel = freqLabel;
            _fft = new float[16384 * _channel];
            _initialized = false;
            _process = new WASAPIPROC(Process);
            _timer1 = new DispatcherTimer();
            _timer1.Tick += Timer1_Tick;
            _timer1.Interval = TimeSpan.FromMilliseconds(25);   // 40hz refresh rate
            _timer1.IsEnabled = false;
            _spectrumdata = new List<byte>();
            _devicenumber = Form1.DeviceNumber();
            _lines = Convert.ToInt16(_form2NumberOfBarsComboBox.SelectedItem);
            _level = new int[2];
            _UNICODE = Form1.Unicode();

            // Event handler for option form (reseive)
            _form2EnumButton.Click += new EventHandler(Form2_EnumerateButton_Clicked);
            _form2DeviceResetButton.Click += new EventHandler(Form2_devicelist_SelectedIndexChanged);
            _devicelist.SelectedIndexChanged += new EventHandler(Form2_devicelist_SelectedIndexChanged);
            _form2NumberOfBarsComboBox.SelectedIndexChanged += new EventHandler(Form2_NumberOfBarIndexChanged);
            _form2MonoRadio.CheckedChanged += new EventHandler(Form2_MonoRadio_CheckChanged);

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UNICODE, _UNICODE);
            Init();
        }

        // flag for enabling and disabling program functionality
        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                if (value)
                {
                    if (!_initialized)
                    {
                        bool result = BassWasapi.BASS_WASAPI_Init(_devicenumber, _mixfreq, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, _process, IntPtr.Zero);
                        if (!result)
                        {
                            var error = Bass.BASS_ErrorGetCode();
                            MessageBox.Show(error.ToString());
                        }
                        else
                        {
                            _initialized = true;
                            if (_mixfreq != 0f) _form2FreqLabel.Text = (_mixfreq / 1000f).ToString("0.0") + " khz";
                        }
                    }
                    bool startResult = BassWasapi.BASS_WASAPI_Start();
                    if (!startResult)
                    {
                        var error = Bass.BASS_ErrorGetCode();
                        MessageBox.Show(error.ToString());
                    }
                }
                else BassWasapi.BASS_WASAPI_Stop(true);

                System.Threading.Thread.Sleep(500);
                _timer1.IsEnabled = value;
            }
        }

        // initialization
        private void Init()     // Analyzer class initialization. DO NOT call twice or more.
        {
            try
            {
                if (_devicenumber == -1) throw new InvalidOperationException("no device");
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(_devicenumber);
                
                string itemName = string.Format("{0} - {1}", _devicenumber, device.name);
                _mixfreq = device.mixfreq;
                SetParamFromFreq(_mixfreq);         // set _DATAFLAG and _mixfreqMulti
                _devicelist.SelectedIndex = _devicelist.Items.IndexOf(itemName);

                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
                bool result = Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);   // "no sound" device for Bass.dll
                if (!result) MessageBox.Show("Valid device not found.\r\n" +
                    "(May be Device Power-SW off? or\r\n" +
                    "device not selected in Windows?)\r\n" +
                    "Please re-select valid device from \"setting\" dialog.\r\n" +
                    "ERROR CODE: " + Bass.BASS_ErrorGetCode().ToString(),
                    "Valid Device Not Found - SpeAnaLED",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); ;
            }
            catch
            {
                _devicelist.SelectedIndex = -1;
            }
            
            inInit = false;
        }

        // WASAPI callback, required for continuous recording
        private int Process(IntPtr buffer, int length, IntPtr user)
        {
            return length;
        }

        // reseived event functions
        //timer
        private void Timer1_Tick(object sender, EventArgs e)
        {
            int ret = BassWasapi.BASS_WASAPI_GetData(_fft, (int)_DATAFLAG);                 //get channel fft data
            if (ret < -1) return;
            int bandX, powerY;
            int fftPos = 0;     // buffer data position
            int freqValue = 1;

            //_level = new int[2];

            // computes the spectrum data, the code is taken from a bass_wasapi sample.
            for (bandX = 0; bandX < _lines; bandX++)
            {
                float[] peak = new float[] { 0f, 0f };      // 0=Left(mono), 1=Right

                if (_channel > 1)
                    freqValue = (int)(Math.Pow(2, (bandX * 10.0 / (_lines - 1)) + _freqShift)  * _mixfreqMultiplyer);
                else
                    freqValue = (int)(Math.Pow(2, (bandX * 10.0 / (_lines - 1)) + _freqShift) / 5 * _mixfreqMultiplyer);    // I don't know why 5...
                                                                                                                            // denominator lager -> shift right
                if (freqValue <= fftPos) freqValue = fftPos + 1;                                            // if out of range, min. freq. selected
                if (_mixfreq <= 48000)          // 44.1khz, 48khz
                {
                    if (freqValue > 4096 * _channel - _channel) freqValue = 4096 * _channel - _channel;     // truncate last data
                }
                else if (_mixfreq <= 88200)     // 88.2khz, 96khz
                {
                    if (freqValue > 8192 * _channel - _channel) freqValue = 8192 * _channel - _channel;
                }
                else                            // 176.4khz, 192khz, 384khz and above?
                {
                    if (freqValue > 16384 * _channel - _channel) freqValue = 16384 * _channel - _channel;
                }

                for (; fftPos < freqValue; fftPos += _channel)                                              // freq band in interreave step
                {
                    for (int i = 0; i < _channel; i++)                                                      // interreave L,R,L,R,... or L+R,L+R,L+R...
                    {
                        if (peak[0] < _fft[1 + fftPos]) peak[0] = _fft[1 + fftPos];                         // set max _fft[x] to peak     L Ch.
                        if (peak[1] < _fft[1 + fftPos + (_channel - 1)]) peak[1] = _fft[1 + fftPos + (_channel - 1)];   // R Ch.
                    }
                }

                for (int i = 0; i < _channel; i++)
                {
                    powerY = (int)(Math.Sqrt(peak[i]) * 3 * 255 - 4);                                       // sqrt to make low values more visible
                    if (powerY > 255) powerY = 255;
                    if (powerY < 0) powerY = 0;
                    _spectrumdata.Add((byte)powerY);
                }
            }

            //  fire draw event to form1
            if (SpectrumChanged != null) SpectrumChanged(this, EventArgs.Empty);

            _spectrumdata.Clear();

            // meter bar
            int l_temp, r_temp;
            float multiplyer = 1.2f;

            int level = BassWasapi.BASS_WASAPI_GetLevel();
            l_temp = (int)(Math.Sqrt(Utils.LowWord32(level) / (Int16.MaxValue / 2f) * _form3PictureBoxWidth) * 13 * multiplyer);        // 0 - 390 (13LEDs * (24+6)px)
            r_temp = (int)(Math.Sqrt(Utils.HighWord32(level) / (Int16.MaxValue / 2f) * _form3PictureBoxWidth) * 13 * multiplyer);       // 0 - 390

            if (l_temp > 390 * multiplyer || r_temp > 390 * multiplyer) _level[0] = _level[1] = 0;
            else
            {
                if (l_temp > 390) _level[0] = 390;
                else _level[0] = l_temp;
                if (r_temp > 390) _level[1] = 390;
                else _level[1] = r_temp;
            }
            
            if (level == _lastlevel && level != 0) _hangcontrol++;
            _lastlevel = level;

            //Required, because some programs hang the output. If the output hangs for a 75ms
            //this piece of code re initializes the output so it doesn't make a gliched sound for long.
            if (_hangcontrol > 3)
            {
                _hangcontrol = 0;
                /*_level[0] = 0;
                _level[1] = 0;
                Form2_devicelist_SelectedIndexChanged(this, EventArgs.Empty);
                Free();
                Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                _initialized = false;
                Enable = true;*/
            }
        }

        private void SetParamFromFreq(int freq)
        {
            // freq is readonly

            if (freq <= 48000 )         // 44.1khz, 48khz
            {
                _DATAFLAG = _channel > 1 ? BASSData.BASS_DATA_FFT8192 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT4096;
                _mixfreqMultiplyer = 44100f / freq * 0.5f;
            }
            else if (freq <= 88200)     // 88.2khz, 96khz
            {
                _DATAFLAG = _channel > 1 ? BASSData.BASS_DATA_FFT16384 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT8192;
                _mixfreqMultiplyer = 44100f / freq;
            }
            else                        // 176.4khz, 192khz, 384khz and above?
            {
                _DATAFLAG = _channel > 1 ? BASSData.BASS_DATA_FFT32768 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT16384;
                _mixfreqMultiplyer = 44100f / freq * 2f;
            }
        }

        private void Form2_NumberOfBarIndexChanged(object sender, EventArgs e)
        {
            _lines = Convert.ToInt16(_form2NumberOfBarsComboBox.SelectedItem);
        }

        private void Form2_EnumerateButton_Clicked(object sender, EventArgs e)
        {
            //_devicelist.Enabled = false;      // loop below is too heavey...
            _form2DeviceResetButton.Enabled = false;
            _form2EnumButton.Enabled = false;

            _initialized = false;
            Enable = false;
            Free();

            _devicelist.Items.Clear();
            BASS_WASAPI_DEVICEINFO device;
            for (int i = 0; i < BassWasapi.BASS_WASAPI_GetDeviceCount(); i++)
            {
                device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
                if (device.IsEnabled && device.IsLoopback)
                {
                    _devicelist.Items.Add(string.Format("{0} - {1}", i, device.name));
                }
            }
            if (_devicelist.Items == null) throw new Exception("Device Serch Error");
            _devicenumber = Convert.ToInt16(_devicelist.Items[0].ToString().Split(' ')[0]);
            _devicelist.SelectedIndex = 0;

            _form2DeviceResetButton.Enabled = true;
            _form2EnumButton.Enabled = true;
            _devicelist.Enabled = true;
        }

        private void Form2_devicelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!inInit)
            {
                if (_devicenumber == -1) return;

                _form2DeviceResetButton.Enabled = false;
                _form2EnumButton.Enabled = false;
                //_devicelist.Enabled = false;

                _initialized = false;
                Enable = false;
                Free();

                _devicenumber = Convert.ToInt16(_devicelist.SelectedItem.ToString().Split(' ')[0]);
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(_devicenumber);
                if (!device.IsEnabled)
                {
                    MessageBox.Show("No (saveed) output device found.\r\n" +
                    "(May be Device Power-SW off?)\r\n" +
                    "ERROR CODE: " + Bass.BASS_ErrorGetCode().ToString(),
                    "No Output Device - SpeAnaLED",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                    Enable = false;
                    Free();
                }
                else
                {
                    _mixfreq = device.mixfreq;
                    SetParamFromFreq(_mixfreq);         // set _DATAFLAG and _mixfreqMulti

                    bool result = Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);       // need _mixfreq here
                    if (!result) throw new Exception("Source Device Initialize Error\r\n" + "ERROR CODE: " + Bass.BASS_ErrorGetCode().ToString());
                    Enable = true;
                }

                _form2EnumButton.Enabled = true;
                _form2DeviceResetButton.Enabled = true;
                _devicelist.Enabled = true;
            }
        }

        private void Form2_MonoRadio_CheckChanged(object sender, EventArgs e)
        {
            _initialized = false;
            Enable = false;
            Free();

            _channel = _form2MonoRadio.Checked ? 1 : 2;
            _fft.Initialize();
            _fft = new float[16384 * _channel];

            SetParamFromFreq(_mixfreq);         // set _DATAFLAG and _mixfreqMulti

            bool result = Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            if (!result) throw new Exception("Source Device Initialize Error");
            Enable = true;

            // fire channel change event to form1
            if (NumberOfChannelChanged != null) NumberOfChannelChanged(this, EventArgs.Empty);
        }

        // cleanup
        public void Free()
        {
            if (_devicelist.SelectedIndex != -1)
            {
                BassWasapi.BASS_WASAPI_Free();
                Bass.BASS_Free();
            }
        }
    }
}