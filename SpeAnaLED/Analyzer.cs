using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Threading;
//using System.Reflection;
using Un4seen.Bass;
using Un4seen.BassWasapi;

namespace SpeAnaLED
{
    //
    // based on h0uri's Analyzer.cs (https://www.instructables.com/Audio-Spectrum-Software-C/) 
    //
    internal class Analyzer
    {
        private readonly Form2 form2;
        private float[] _fft;                           // buffer for fft data
        private int _channel;                           // 1: "mixed-data"(mono) 2: L+R
        private int _numberOfBars;                      // number of spectrum lines
        private int devicenumber;                       // device number
        private bool _enable;                           // enabled status
        private bool _initialized;                      // initialized flag
        public bool inInit = true;
        private readonly WASAPIPROC _process;           // callback function to obtain data
        public List<byte> spectrumdata;                 // spectrum data buffer
        public int[] level;                             // progressbars for left and right channel intensity
        private readonly DispatcherTimer _timer;        // timer that refreshes the display
        private BASSData _DATAFLAG;                     // for "interreave" format
        private int _mixfreq;
        private float _mixfreqMultiplyer;
        private int _lastlevel;                         // last output level
        private int _hangcontrol;                       // last output level counter
        private readonly ComboBox _DeviceListComboBox;  // for subscribe
        private int _refreshMode;

        private readonly float _freqShift = (float)Math.Round(Math.Log(20000/*hz*/, 2) - 10/*difference to 20hz*/, 2);    // constant 4.29

        private readonly bool _UNICODE = true;         // codepage switch

        // for fire
        public event EventHandler SpectrumChanged;
        public event EventHandler NumberOfChannelChanged;
        public event Form1.CheckedEventHandler IsBusy;

        public Analyzer(Form2 _form2)
        {
            form2 = _form2;

            _DeviceListComboBox = form2.DeviceListComboBox;
            _channel = form2.MonoRadioButton.Checked ? 1 : 2;
            _refreshMode = form2.RefreshFastRadio.Checked ? 1 : 2;
            _fft = new float[16384 * _channel];
            _initialized = false;
            _process = new WASAPIPROC(Process);
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(Form2.timerIntervalMilliSeconds),   // 25msec 40hz refresh rate
                IsEnabled = false,
            };
            spectrumdata = new List<byte>();
            _numberOfBars = form2.numberOfBar;
            level = new int[Form2.maxChannel];
            devicenumber = Form2.DeviceNumber;
            _UNICODE = Form1.Unicode();

            // Event handler for option form (subscribe)
            _timer.Tick += Timer_Tick;
            _DeviceListComboBox.SelectedIndexChanged += Form2_DeviceListComboBox_SelectedIndexChanged;
            form2.EnumerateButton.Click += Form2_EnumerateButton_Clicked;
            form2.DeviceResetButton.Click += Form2_DeviceListComboBox_SelectedIndexChanged;
            //form2.NumberOfBarComboBox.SelectedIndexChanged += Form2_NumberOfBarChanged;
            form2.NumberOfBarChanged += Form2_NumberOfBarChanged;
            form2.MonoRadioButton.CheckedChanged += Form2_MonoRadio_CheckChanged;
            form2.RefreshFastRadio.CheckedChanged += Form2_RefreshFastRadio_CheckCHanged;

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
                        bool result = BassWasapi.BASS_WASAPI_Init(devicenumber, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, _process, IntPtr.Zero);
                        if (!result)
                        {
                            var error = Bass.BASS_ErrorGetCode();
                            MessageBox.Show(error.ToString());
                        }
                        else
                        {
                            _initialized = true;
                            if (_mixfreq != 0f) form2.FrequencyLabel.Text = (_mixfreq / 1000f).ToString("0.0") + " khz";
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
                _timer.IsEnabled = value;
            }
        }

        // initialization
        private void Init()     // Analyzer class initialization. DO NOT call twice or more.
        {
            try
            {
                if (devicenumber == -1) throw new InvalidOperationException("no device");
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(devicenumber);

                string itemName = string.Format("{0} - {1}", devicenumber, device.name);
                _mixfreq = device.mixfreq;
                SetParamFromFreq(_mixfreq);         // set _DATAFLAG and _mixfreqMulti
                _DeviceListComboBox.SelectedIndex = _DeviceListComboBox.Items.IndexOf(itemName);

                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
                bool result = Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);   // "no sound" device for Bass.dll initialization
                if (!result) MessageBox.Show(
                    "Valid device not found.\r\n" +
                    "(May be Device Power-SW off? or\r\n" +
                    "device not selected in Windows?)\r\n" +
                    "ERROR CODE: " + Bass.BASS_ErrorGetCode().ToString(),
                    "Valid Device Not Found - SpeAnaLED",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); ;
            }
            catch
            {
                _DeviceListComboBox.SelectedIndex = -1;
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            int ret = BassWasapi.BASS_WASAPI_GetData(_fft, (int)_DATAFLAG);                 //get channel fft data
            if (ret < -1) return;
            int bandX, powerY;
            int fftPos = 0;     // buffer data position
            int freqValue = 1;

            // computes the spectrum data, the code is taken from a bass_wasapi sample.
            for (bandX = 0; bandX < _numberOfBars; bandX++)
            {
                float[] peak = new float[] { 0f, 0f };      // 0=Left(mono), 1=Right

                if (_channel > 1)
                    freqValue = (int)(Math.Pow(2, (bandX * 10.0 / (_numberOfBars - 1)) + _freqShift) * _mixfreqMultiplyer);
                else
                    freqValue = (int)(Math.Pow(2, (bandX * 10.0 / (_numberOfBars - 1)) + _freqShift) / 5 * _mixfreqMultiplyer);    // I don't know why 5...
                                                                                                                                   // denominator lager -> shift right
                if (freqValue <= fftPos) freqValue = fftPos + 1;                                            // if out of range, min. freq. selected
                
                if (_mixfreq <= 48000)          // 44.1khz, 48khz
                    if (freqValue > 4096 * _channel - _channel) freqValue = 4096 * _channel - _channel;     // truncate last data
                else if (_mixfreq <= 88200)     // 88.2khz
                    if (freqValue > 8192 * _channel - _channel) freqValue = 8192 * _channel - _channel;
                else                            // 96khz, 176.4khz, 192khz, 384khz and above?
                    if (freqValue > 16384 * _channel - _channel) freqValue = 16384 * _channel - _channel;

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
                    spectrumdata.Add((byte)powerY);
                }
            }

            // meter bar
            int level = BassWasapi.BASS_WASAPI_GetLevel();
            this.level[0] = (int)(Math.Sqrt(Utils.LowWord32(level) / (short.MaxValue / 2f) * Form2.lmPBWidth) * 13);        // 0 - 390 (13LEDs * (24+6)px)
            this.level[1] = (int)(Math.Sqrt(Utils.HighWord32(level) / (short.MaxValue / 2f) * Form2.lmPBWidth) * 13);       // 0 - 390

            //  fire draw event to form1
            SpectrumChanged?.Invoke(this, EventArgs.Empty);
            spectrumdata.Clear();

            if (level == _lastlevel && level != 0) _hangcontrol++;
            _lastlevel = level;

            //Required, because some programs hang the output. If the output hangs for a 500ms
            //this piece of code re initializes the output so it doesn't make a gliched sound for long.
            if (_hangcontrol > 500 / _timer.Interval.Milliseconds)
            {
                _hangcontrol = 0;
                if (form2.AutoReloadCheckBox.Checked)
                {
                    this.level[0] = 0;
                    this.level[1] = 0;
                    Form2_DeviceListComboBox_SelectedIndexChanged(this, EventArgs.Empty);
                    Free();
                    Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                    _initialized = false;
                    Enable = true;
                }
            }
        }

        private void Form2_NumberOfBarChanged(object sender, EventArgs e)
        {
            _numberOfBars = form2.numberOfBar;
        }

        private void Form2_EnumerateButton_Clicked(object sender, EventArgs e)
        {
            IsBusy?.Invoke(this, new CheckedEventArgs() { Checked = true });

            _initialized = false;
            Enable = false;
            Free();

            _DeviceListComboBox.Items.Clear();
            BASS_WASAPI_DEVICEINFO device;
            int deviceCount = BassWasapi.BASS_WASAPI_GetDeviceCount();
            for (int i = 0; i < deviceCount; i++)
            {
                device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
                if (device.IsEnabled && device.IsLoopback)
                {
                    _DeviceListComboBox.Items.Add(string.Format("{0} - {1}", i, device.name));
                }
            }
            
            if (_DeviceListComboBox.Items.Count == 0)
            {
                MessageBox.Show(
                    "No output device found.\r\n" +
                    "(May be Device Power-SW off? or)\r\n" +
                    "device not selected in Windows?)\r\n" +
                    "ERROR CODE: No Device(Enumerate)",
                    "No Output Device - SpeAnaLED",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                inInit = true;
                _DeviceListComboBox.Items.Add("Please Enumerate Devices");
                _DeviceListComboBox.SelectedIndex = 0;

                Enable = false;
                Free();
                inInit = false;
            }
            else
            {
                //devicenumber = Convert.ToInt16(_DeviceListComboBox.Items[0].ToString().Split(' ')[0]);
                _DeviceListComboBox.SelectedIndex = 0;
                devicenumber = Form2.DeviceNumber;

                _DeviceListComboBox.Enabled = true;
            }

            IsBusy?.Invoke(this, new CheckedEventArgs() { Checked = false });
        }

        private void Form2_DeviceListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inInit || Form2.DeviceNumber == -1) return;

            IsBusy?.Invoke(this, new CheckedEventArgs() { Checked = true });

            _initialized = false;
            Enable = false;
            Free();

            try
            {
                devicenumber = Form2.DeviceNumber;
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(devicenumber);
                if (!device.IsEnabled)
                {
                    MessageBox.Show("No (saveed) output device found.\r\n" +
                    "(May be Device Power-SW off? or)\r\n" +
                    "device not selected in Windows?)\r\n" +
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
            }
            catch
            {
                MessageBox.Show(
                    "No (saveed) output device found.\r\n" +
                    "(May be Device Power-SW off? or)\r\n" +
                    "device not selected in Windows?)\r\n" +
                    "ERROR CODE: No Device(Select Device)",
                    "No Output Device - SpeAnaLED",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                Enable = false;
                Free();
            }

            IsBusy?.Invoke(this, new CheckedEventArgs() { Checked = false });
        }

        private void Form2_MonoRadio_CheckChanged(object sender, EventArgs e)
        {
            _initialized = false;
            Enable = false;
            Free();

            _channel = form2.MonoRadioButton.Checked ? 1 : 2;
            _fft.Initialize();
            _fft = new float[16384 * _channel];

            SetParamFromFreq(_mixfreq);         // set _DATAFLAG and _mixfreqMulti

            bool result = Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            if (!result) throw new Exception("Source Device Initialize Error");
            Enable = true;

            // fire channel change event to form1
            NumberOfChannelChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Form2_RefreshFastRadio_CheckCHanged(object sender, EventArgs e)
        {
            _refreshMode = form2.RefreshFastRadio.Checked ? 1 : 2;
            Form2_DeviceListComboBox_SelectedIndexChanged(this, EventArgs.Empty);   // To reset device
        }

        // helper function
        private void SetParamFromFreq(int freq)
        {
            // freq is readonly

            if (freq <= 48000 / _refreshMode)          // 44.1khz, 48khz
            {
                _DATAFLAG = _channel > 1 ? BASSData.BASS_DATA_FFT4096 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT2048;
                _mixfreqMultiplyer = 44100f / freq * 0.25f;
            }
            else if (freq <= 96000 / _refreshMode)          // 88.2khz, 96khz
            {
                _DATAFLAG = _channel > 1 ? BASSData.BASS_DATA_FFT8192 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT4096;
                _mixfreqMultiplyer = 44100f / freq * 0.5f;
            }
            else if (freq <= 192000 / _refreshMode)     // 176.4khz, 192khz
            {
                _DATAFLAG = _channel > 1 ? BASSData.BASS_DATA_FFT16384 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT8192;
                _mixfreqMultiplyer = 44100f / freq;
            }
            else                        // 384khz and above?
            {
                _DATAFLAG = _channel > 1 ? BASSData.BASS_DATA_FFT32768 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT16384;
                _mixfreqMultiplyer = 44100f / freq * 2f;
            }
        }

        // cleanup
        public void Free()
        {
            if (_DeviceListComboBox.SelectedIndex != -1)
            {
                BassWasapi.BASS_WASAPI_Free();
                Bass.BASS_Free();
            }
        }

    }
}