using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Threading;
using Un4seen.Bass;
using Un4seen.BassWasapi;

namespace SpeAnaLED
{
    //
    // based on h0uri's Analyzer.cs (https://www.instructables.com/Audio-Spectrum-Software-C/) 
    //
    internal class Analyzer
    {
        private readonly Form2 _form2;
        private float[] fft;                            // buffer for fft data
        private int channel;                            // 1: "mixed-data"(mono) 2: L+R
        private int devicenumber;                       // device number
        private bool enable;                            // enabled status
        private bool initialized;                       // initialized flag
        public bool inInit = true;                      // in intializing flag
        private readonly WASAPIPROC process;            // callback function to obtain data
        public List<byte> spectrumdata;                 // spectrum data buffer
        public int[] level;                             // progressbars for left and right channel intensity
        private readonly DispatcherTimer timer;         // timer that refreshes the display
        private BASSData DATAFLAG;                      // for "interreave" format
        private int mixfreq;                            // devide frequency
        private float mixfreqMultiplyer;                // frequency multiply value
        private int lastlevel;                          // last output level
        private int hangcontrol;                        // last output level counter
        private int refreshMode;                        // refresh 1: normal, 2: fast

        private readonly float freqShift = (float)Math.Round(Math.Log(20000/*hz*/, 2) - 10/*difference to 20hz*/, 2);    // constant 4.29

        private readonly bool UNICODE = true;           // codepage switch

        // for fire
        public event EventHandler SpectrumChanged;
        public event EventHandler NumberOfChannelChanged;
        public event Form1.CheckedEventHandler IsBusy;

        public Analyzer(Form2 form2)
        {
            _form2 = form2;

            channel = _form2.MonoRadioButton.Checked ? 1 : 2;
            refreshMode = _form2.RefreshFastRadio.Checked ? 1 : 2;
            fft = new float[16384 * channel];
            initialized = false;
            process = new WASAPIPROC(Process);
            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(Form2.timerIntervalMilliSeconds),   // 25msec 40hz refresh rate
                IsEnabled = false,
            };
            spectrumdata = new List<byte>();
            level = new int[Form2.maxChannel];
            devicenumber = Form2.DeviceNumber;
            UNICODE = Form1.Unicode();

            // Event handler for option form (subscribe)
            timer.Tick += Timer_Tick;
            _form2.DeviceListComboBox.SelectedIndexChanged += Form2_DeviceListComboBox_SelectedIndexChanged;
            _form2.EnumerateButton.Click += Form2_EnumerateButton_Clicked;
            _form2.DeviceReloadButton.Click += Form2_DeviceListComboBox_SelectedIndexChanged;
            _form2.DeviceReloadRequested += Form2_DeviceListComboBox_SelectedIndexChanged;
            _form2.MonoRadioButton.CheckedChanged += Form2_MonoRadio_CheckChanged;
            _form2.RefreshFastRadio.CheckedChanged += Form2_RefreshFastRadio_CheckCHanged;

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UNICODE, UNICODE);
            Init();
        }

        // flag for enabling and disabling program functionality
        public bool Enable
        {
            get { return enable; }
            set
            {
                enable = value;
                if (value)
                {
                    if (!initialized)
                    {
                        bool result = BassWasapi.BASS_WASAPI_Init(devicenumber, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, process, IntPtr.Zero);
                        if (!result)
                        {
                            var error = Bass.BASS_ErrorGetCode();
                            MessageBox.Show(error.ToString());
                        }
                        else
                        {
                            initialized = true;
                            if (mixfreq != 0f) _form2.FrequencyLabel.Text = (mixfreq / 1000f).ToString("0.0") + " khz";
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
                timer.IsEnabled = value;
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
                mixfreq = device.mixfreq;
                SetParamFromFreq(mixfreq);         // set _DATAFLAG and _mixfreqMulti
                _form2.DeviceListComboBox.SelectedIndex = _form2.DeviceListComboBox.Items.IndexOf(itemName);

                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
                bool result = Bass.BASS_Init(0, mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);    // "no sound" device for Bass.dll initialization
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
                _form2.DeviceListComboBox.SelectedIndex = -1;
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
            int ret = BassWasapi.BASS_WASAPI_GetData(fft, (int)DATAFLAG);       //get channel fft data
            if (ret < -1) return;
            int bandX, powerY;
            int fftPos = 0;     // buffer data position
            int freqValue = 1;

            // computes the spectrum data, the code is taken from a bass_wasapi sample.
            for (bandX = 0; bandX < _form2.numberOfBar; bandX++)
            {
                float[] peak = new float[] { 0f, 0f };      // 0=Left(mono), 1=Right

                if (channel > 1)
                    freqValue = (int)(Math.Pow(2, (bandX * 10.0 / _form2.numberOfBar) + freqShift) * mixfreqMultiplyer);
                else
                    freqValue = (int)(Math.Pow(2, (bandX * 10.0 / _form2.numberOfBar) + freqShift) / 5 * mixfreqMultiplyer);        // I don't know why 5...
                                                                                                                                    // denominator lager -> shift right
                if (freqValue <= fftPos) freqValue = fftPos + 1;                                            // if out of range, min. freq. selected
                
                if (mixfreq <= 48000)           // 44.1khz, 48khz
                    if (freqValue > 4096 * channel - channel) freqValue = 4096 * channel - channel;         // truncate last data
                else if (mixfreq <= 88200)      // 88.2khz
                    if (freqValue > 8192 * channel - channel) freqValue = 8192 * channel - channel;
                else                            // 96khz, 176.4khz, 192khz, 384khz and above?
                    if (freqValue > 16384 * channel - channel) freqValue = 16384 * channel - channel;

                for (; fftPos < freqValue; fftPos += channel)                                               // freq band in interreave step
                {
                    for (int i = 0; i < channel; i++)                                                       // interreave L,R,L,R,... or L+R,L+R,L+R...
                    {
                        if (peak[0] < fft[1 + fftPos]) peak[0] = fft[1 + fftPos];                           // set max _fft[x] to peak     L Ch.
                        if (peak[1] < fft[1 + fftPos + (channel - 1)]) peak[1] = fft[1 + fftPos + (channel - 1)];                       // R Ch.
                    }
                }

                for (int i = 0; i < channel; i++)
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

            if (level == lastlevel && level != 0) hangcontrol++;
            lastlevel = level;

            //Required, because some programs hang the output. If the output hangs for a 500ms
            //this piece of code re initializes the output so it doesn't make a gliched sound for long.
            if (hangcontrol > 500 / timer.Interval.Milliseconds)
            {
                hangcontrol = 0;
                if (_form2.AutoReloadCheckBox.Checked)
                {
                    this.level[0] = 0;
                    this.level[1] = 0;
                    Form2_DeviceListComboBox_SelectedIndexChanged(this, EventArgs.Empty);   // Program Reset
                    Free();
                    Bass.BASS_Init(0, mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                    initialized = false;
                    Enable = true;
                }
            }
        }

        private void Form2_EnumerateButton_Clicked(object sender, EventArgs e)
        {
            IsBusy?.Invoke(this, new CheckedEventArgs() { Checked = true });

            initialized = false;
            Enable = false;
            Free();

            _form2.DeviceListComboBox.Items.Clear();
            BASS_WASAPI_DEVICEINFO device;
            int deviceCount = BassWasapi.BASS_WASAPI_GetDeviceCount();
            for (int i = 0; i < deviceCount; i++)
            {
                device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
                if (device.IsEnabled && device.IsLoopback)
                {
                    _form2.DeviceListComboBox.Items.Add(string.Format("{0} - {1}", i, device.name));
                }
            }
            
            if (_form2.DeviceListComboBox.Items.Count == 0)
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
                _form2.DeviceListComboBox.Items.Add("Please Enumerate Devices");
                _form2.DeviceListComboBox.SelectedIndex = 0;

                Enable = false;
                Free();
                inInit = false;
            }
            else
            {
                _form2.DeviceListComboBox.SelectedIndex = 0;
                devicenumber = Form2.DeviceNumber;

                _form2.DeviceListComboBox.Enabled = true;
            }

            IsBusy?.Invoke(this, new CheckedEventArgs() { Checked = false });
        }

        private void Form2_DeviceListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inInit || Form2.DeviceNumber == -1) return;

            IsBusy?.Invoke(this, new CheckedEventArgs() { Checked = true });

            initialized = false;
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
                    mixfreq = device.mixfreq;
                    SetParamFromFreq(mixfreq);         // set _DATAFLAG and _mixfreqMulti

                    bool result = Bass.BASS_Init(0, device.mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);       // need _mixfreq here
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
            initialized = false;
            Enable = false;
            Free();

            channel = _form2.MonoRadioButton.Checked ? 1 : 2;
            fft.Initialize();
            fft = new float[16384 * channel];

            SetParamFromFreq(mixfreq);         // set _DATAFLAG and _mixfreqMulti

            bool result = Bass.BASS_Init(0, mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            if (!result) throw new Exception("Source Device Initialize Error");
            Enable = true;

            // fire channel change event to form1
            NumberOfChannelChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Form2_RefreshFastRadio_CheckCHanged(object sender, EventArgs e)
        {
            refreshMode = _form2.RefreshFastRadio.Checked ? 1 : 2;
            Form2_DeviceListComboBox_SelectedIndexChanged(this, EventArgs.Empty);   // To reset device
        }

        // helper function
        private void SetParamFromFreq(int freq)
        {
            // freq is readonly

            if (freq <= 48000 / refreshMode)            // 44.1khz, 48khz
            {
                DATAFLAG = channel > 1 ? BASSData.BASS_DATA_FFT4096 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT2048;
                mixfreqMultiplyer = 44100f / freq * 0.25f;
            }
            else if (freq <= 96000 / refreshMode)       // 88.2khz, 96khz
            {
                DATAFLAG = channel > 1 ? BASSData.BASS_DATA_FFT8192 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT4096;
                mixfreqMultiplyer = 44100f / freq * 0.5f;
            }
            else if (freq <= 192000 / refreshMode)      // 176.4khz, 192khz
            {
                DATAFLAG = channel > 1 ? BASSData.BASS_DATA_FFT16384 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT8192;
                mixfreqMultiplyer = 44100f / freq;
            }
            else                                        // 384khz and above?
            {
                DATAFLAG = channel > 1 ? BASSData.BASS_DATA_FFT32768 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT16384;
                mixfreqMultiplyer = 44100f / freq * 2f;
            }
        }

        // cleanup
        public void Free()
        {
            if (_form2.DeviceListComboBox.SelectedIndex != -1)
            {
                BassWasapi.BASS_WASAPI_Free();
                Bass.BASS_Free();
            }
        }

    }
}