using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using Un4seen.Bass;
using Un4seen.BassWasapi;

namespace SpeAnaLED
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1005:デリゲート呼び出しを簡素化できます。", Justification = "<保留中>")]

    internal class Analyzer
    {
        public float[] _fft;                            // buffer for fft data
        public readonly ComboBox _devicelist;           // device list
        private bool _enable;                           // enabled status
        private bool _initialized;                      // initialized flag
        private readonly WASAPIPROC _process;           // callback function to obtain data
        //private int deviceindex;                        // used device index
        public List<byte> _spectrumdata;                // spectrum data buffer
        public readonly DispatcherTimer _timer1;        // timer that refreshes the display
        private BASSData _DATAFLAG;                     // for "interreave" format
        private BASSData _DATAFLAG_8192;                // for Hi-Reso
        private BASSData _DATAFLAG_16384;               // for Hi-Reso
        public int _devicenumber;
        private readonly bool _UNICODE;                 // codepage switch
        private readonly Button _form2EnumButton;       // for subscribe
        private readonly Button _form2DeviceResetButton;// for subscribe
        private readonly ComboBox _form2NumberOfBars;   // for subscribe
        private readonly RadioButton _form2MonoRadio;   // for subscribe
        public event EventHandler SpectrumChanged;      // for fire
        public event EventHandler NumberOfChannelChanged;   // for fire
        private int _mixfreq;
        private float _mixfreqMulti;
        private readonly float _freqShift = (float)Math.Round(Math.Log(20000/*hz*/, 2) - /*in increments of*/10, 2);
        private bool inInit = true;

        public int _channel;                            // 1: "mix-data"(mono) 2: L+R
        private int _lines;                             // default number of spectrum lines

        public Analyzer(ComboBox devicelist, Button enumButton, Button deviceResetButton, ComboBox numberofbars, RadioButton monoRadio)    // Control data for event subscribe
        {
            _form2MonoRadio = monoRadio;
            _channel = monoRadio.Checked ? 1 : 2;
            //_fft = new float[8192 * _channel];
            _fft = new float[16384 * _channel];
            _devicelist = devicelist;
            _initialized = false;
            _process = new WASAPIPROC(Process);
            _timer1 = new DispatcherTimer();
            _timer1.Tick += Timer1_Tick;
            _timer1.Interval = TimeSpan.FromMilliseconds(25);   // 40hz refresh rate
            _timer1.IsEnabled = false;
            _spectrumdata = new List<byte>();
            _devicenumber = Form1.DeviceNumber();
            _form2EnumButton = enumButton;
            _form2DeviceResetButton= deviceResetButton;
            _form2NumberOfBars = numberofbars;
            _DATAFLAG_8192 = _channel > 1 ? BASSData.BASS_DATA_FFT16384 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT8192;
            _DATAFLAG_16384 = _channel > 1 ? BASSData.BASS_DATA_FFT32768 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT16384;
            _lines = Convert.ToInt16(_form2NumberOfBars.SelectedItem);
            _UNICODE = Form1.Unicode();

            // Event handler for option form (reseive)
            _form2EnumButton.Click += new EventHandler(Form2_EnumerateButton_Clicked);
            _form2DeviceResetButton.Click += new EventHandler(Form2_devicelist_SelectedIndexChanged);
            _form2NumberOfBars.SelectedIndexChanged += new EventHandler(Form2_NumberOfBarIndexChanged);
            _form2MonoRadio.CheckedChanged += new EventHandler(Form2_MonoRadio_CheckChanged);
            _devicelist.SelectedIndexChanged += new EventHandler(Form2_devicelist_SelectedIndexChanged);

            Init();
        }

        // flag for display enable
        public bool DisplayEnable { get; set; }

        // initialization
        private void Init()     // Analyzer class initialization. DO NOT call twice or more.
        {
            try
            {
                //for (int i = 0; i < BassWasapi.BASS_WASAPI_GetDeviceCount(); i++)
                //{
                if (_devicenumber == -1) throw new InvalidOperationException("no device");
                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UNICODE, _UNICODE);
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(_devicenumber);
                /*if (device.IsEnabled && device.IsLoopback)
                {
                    _devicelist.Items.Add(string.Format("{0} - {1}", _devicenumber, device.name));
                }*/
                //}
                //_devicelist.SelectedIndex = 0;
                string itemName = string.Format("{0} - {1}", _devicenumber, device.name);
                _mixfreq = device.mixfreq;
                _DATAFLAG = _mixfreq > 96000 ? _DATAFLAG_16384 : _DATAFLAG_8192;
                _mixfreqMulti = 44100f / _mixfreq * (_mixfreq > 96000 ? 2f : 1f); 
                _devicelist.SelectedIndex = _devicelist.Items.IndexOf(itemName);
            }
            catch
            {
                MessageBox.Show("No (saveed) output device found.\r\n" +
                    "(May be Device Power-SW off?)\r\n" +
                    "Please enumrate devices from \"Setting\" dialog.",
                    "No Output Device - SpeAnaLED",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                _devicelist.SelectedIndex = -1;
                
                Free();
            }
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
            bool result = Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);   // "no sound" device for Bass.dll
            if (!result) MessageBox.Show("Valid device not found.\r\n" +
                        "(May be Device Power-SW off? or\r\n" +
                        "device not selected in Windows?)\r\n" +
                        "Please re-select valid device from \"setting\" dialog.",
                        "Valid Device Not Found - SpeAnaLED",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation); ;

            inInit = false;
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
                        /*var deviceArray = (_devicelist.Items[_devicelist.SelectedIndex] as string).Split(' ');
                        deviceindex = Convert.ToInt32(deviceArray[0]);*/
                        //bool result = BassWasapi.BASS_WASAPI_Init(deviceindex, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, _process, IntPtr.Zero);
                        bool result = BassWasapi.BASS_WASAPI_Init(_devicenumber, _mixfreq, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, _process, IntPtr.Zero);
                        if (!result)
                        {
                            var error = Bass.BASS_ErrorGetCode();
                            MessageBox.Show(error.ToString());
                        }
                        else
                        {
                            _initialized = true;
                            //_devicelist.Enabled = false;
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
            

            // computes the spectrum data, the code is taken from a bass_wasapi sample.
            for (bandX = 0; bandX < _lines; bandX++)
            {
                float[] peak = new float[] { 0f, 0f };      // 0=Left(mono), 1=Right

                if (_channel > 1)
                    freqValue = (int)(Math.Pow(2, (bandX * 10.0 / (_lines - 1)) + _freqShift) * _mixfreqMulti);
                else
                    freqValue = (int)(Math.Pow(2, bandX * 10.0 / (_lines - 1) + _freqShift) / 4 * _mixfreqMulti);   // I don't know why 4...

                if (freqValue <= fftPos) freqValue = fftPos + 1;                                            // if out of range, min. freq. selected
                if (_mixfreq > 96000)
                {
                    if (freqValue > 16384 * _channel - _channel) freqValue = 16384 * _channel - _channel;
                }
                else
                {
                    if (freqValue > 8192 * _channel - _channel) freqValue = 8192 * _channel - _channel;         // truncate last data
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
        }
             
        private void Form2_NumberOfBarIndexChanged(object sender, EventArgs e)
        {
            _lines = Convert.ToInt16(_form2NumberOfBars.SelectedItem);
        }
        
        private void Form2_EnumerateButton_Clicked(object sender, EventArgs e)
        {
            _form2EnumButton.Enabled = false;
            _devicelist.Enabled = false;
            _initialized = false;
            Enable = false;
            Free();

            _devicelist.SelectedIndex = -1;
            _devicelist.Items.Clear();
            for (int i = 0; i < BassWasapi.BASS_WASAPI_GetDeviceCount(); i++)
            {
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
                if (device.IsEnabled && device.IsLoopback)
                {
                    _devicelist.Items.Add(string.Format("{0} - {1}", i, device.name));
                }
            }

            try
            {
                _devicelist.SelectedIndex = 0;
                bool result = Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                if (!result) throw new Exception("Source Device Initialize Error");
                _devicenumber = Convert.ToInt16(_devicelist.SelectedItem.ToString().Split(' ')[0]);

                Enable = true;
            }
            catch
            {
                _devicelist.Items.Clear();
                _devicelist.Items.Add("Please Enumrate Devices");
                _devicelist.SelectedIndex = 0;
                
                MessageBox.Show("No Output Device found." +
                    "(May be Device Power-SW off?)",
                    "No Output Device - SpeAnaLED",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                
                DisplayEnable = false;
                Enable = false;
                Free();
            }

            _devicelist.Enabled = true;
            _form2EnumButton.Enabled = true;
        }

        private void Form2_devicelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!inInit/* && _initialized == true*/)
            {
                _initialized = false;
                Enable = false;
                Free();

                _devicenumber = Convert.ToInt16(_devicelist.SelectedItem.ToString().Split(' ')[0]);
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(_devicenumber);
                _mixfreq = device.mixfreq;
                _mixfreqMulti = 44100f / _mixfreq * (_mixfreq > 96000 ? 2f : 1f);

                bool result = Bass.BASS_Init(0, _mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                if (!result) throw new Exception("Source Device Initialize Error");
                Enable = true;
            }
        }

        private void Form2_MonoRadio_CheckChanged(object sender, EventArgs e)
        {
            _initialized = false;
            Enable = false;
            Free();

            _channel = _form2MonoRadio.Checked ? 1 : 2;
            _fft.Initialize();
            //_fft = new float[8192 * _channel];
            _fft = new float[16384 * _channel];
            _DATAFLAG = _channel > 1 ? _DATAFLAG_16384 : _DATAFLAG_8192;
            
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
