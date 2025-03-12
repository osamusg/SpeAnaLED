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
        private int channel;                            // 1: "mixed-data"(mono) 2: L & R (stereo)
        private int devicenumber;                       // device number
        private bool enable;                            // enabled status
        private bool initialized;                       // initialized flag
        public bool inInit = true;                      // in intializing flag
        private readonly WASAPIPROC process;            // callback function to obtain data
        public List<byte> spectrumdata;                 // spectrum data buffer
        public int[] level;                             // level bars data for left and right channel intensity
        private readonly DispatcherTimer timer;         // timer that refreshes the display
        private BASSData DATAFLAG;                      // for "interreave" format
        private int mixfreq;                            // devide frequency
        private float mixfreqMultiplyer;                // frequency multiply value
        private int valueBase;                          // frequency value adjustment base
        private int lastlevel;                          // last output level
        private int hangcontrol;                        // last output level counter
        private readonly float[][] adjustTable;         // adjust table for frequency

        private readonly float freqShift = (float)Math.Round(Math.Log(20000/*hz*/, 2) - 10/*difference to 20hz*/, 2);    // constant 4.29

        private readonly bool UNICODE = true;           // codepage switch

        // for fire
        public event EventHandler SpectrumChanged;
        public event EventHandler NumberOfChannelChanged;

        public Analyzer(Form2 form2)
        {
            _form2 = form2;

            channel = _form2.MonoRadioButton.Checked ? 1 : 2;
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
            adjustTable = _form2.adjustTable;
            UNICODE = Form1.Unicode();

            // Event handler for option form (subscribe)
            timer.Tick += Timer_Tick;
            _form2.NumberOfBarChanged += Form2_MonoRadio_CheckChanged;
            _form2.MonoRadioButton.CheckedChanged += Form2_MonoRadio_CheckChanged;
            _form2.DeviceReloadRequested += ReloadDefaultDevice;

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UNICODE, UNICODE);

            try
            {
                ReloadDefaultDevice(this, EventArgs.Empty);

                if (devicenumber <= 0) 
                    throw new InvalidOperationException("Device not found");
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(devicenumber);
                
                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
                if (!Bass.BASS_Init(0, mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))             // "no sound" device for Bass.dll initialization
                    MessageBox.Show(
                    "Bass.dll can't be initialized.\r\n" +
                    "(May be Device Power-SW off?)\r\n" +
                    "ERROR CODE: " + Bass.BASS_ErrorGetCode().ToString(),
                    "Bass.dll Initiallize Error - " + _form2.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation); ;
            }
            catch
            {
                _form2.DefaultDeviceName.Text = "WASPI device not found.";
            }

            inInit = false;
        }

        private void ReloadDefaultDevice(object sender, EventArgs e)
        {
            _form2.DeviceReloadButton.Enabled =
            _form2.AutoReloadCheckBox.Enabled = false;

            initialized = false;
            Enable = false;
            Free();
            devicenumber = -1;

            BASS_WASAPI_DEVICEINFO device;
            int deviceCount = BassWasapi.BASS_WASAPI_GetDeviceCount();

            for (int i = 0; i < deviceCount; i++)
            {
                device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
                if (device.IsDefault)
                {
                    _form2.DefaultDeviceName.Text = device.name;
                    break;
                }
            }
            for (int i = 0; i < deviceCount; i++)
            {
                device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
                if (device.name == _form2.DefaultDeviceName.Text && device.IsEnabled && device.IsLoopback)
                {
                    devicenumber = i;
                    mixfreq = device.mixfreq;
                    SetParamFromFreq(mixfreq);         // set _DATAFLAG and _mixfreqMulti
                    if (!inInit)
                    { 
                        if (!Bass.BASS_Init(0, mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))                 // need mixfreq here
                            throw new Exception("Bass.dll Initialize Error\r\n" + "ERROR CODE: " + Bass.BASS_ErrorGetCode().ToString());
                        Enable = true;
                    }
                    break;
                }
            }
            if (devicenumber <= 0)
            {
                MessageBox.Show(
                    "No output device found.\r\n" +
                    "(May be Device Power-SW off?)\r\n" +
                    "ERROR CODE: No Device",
                    "No Output Device - " + _form2.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                _form2.DefaultDeviceName.Text = "WASPI device not found.";
                _form2.FrequencyLabel.Text = "- - - . - khz";
            }

            _form2.DeviceReloadButton.Enabled =
            _form2.AutoReloadCheckBox.Enabled = true;
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
                        if (!BassWasapi.BASS_WASAPI_Init(devicenumber, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, process, IntPtr.Zero))
                        {
                            MessageBox.Show(Bass.BASS_ErrorGetCode().ToString(), "WASPI Init. Error");
                        }
                        else
                        {
                            initialized = true;
                            if (mixfreq != 0f) _form2.FrequencyLabel.Text = (mixfreq / 1000f).ToString("0.0") + " khz";
                        }
                    }
                    if (!BassWasapi.BASS_WASAPI_Start())
                    {
                        MessageBox.Show(Bass.BASS_ErrorGetCode().ToString(), "WASPI Start Error");
                    }
                }
                else
                {
                    BassWasapi.BASS_WASAPI_Stop(true);
                }

                System.Threading.Thread.Sleep(500);
                timer.IsEnabled = value;
            }
        }

        // WASAPI callback, required for continuous recording
        private int Process(IntPtr buffer, int length, IntPtr user) { return length; }

        // reseived event functions
        // timer
        private void Timer_Tick(object sender, EventArgs e)
        {
            float[] tmp = new float[fft.Length];
            if (BassWasapi.BASS_WASAPI_GetData(tmp, (int)DATAFLAG) < -1) return;    // get channel fft data (float[])
            fft[0] = 0f;                                    // padding 0
            Array.Copy(tmp, 0, fft, 1, tmp.Length - 1);     // I don't know why need this.
            int bandX, powerY;
            int fftPos = 0;     // buffer data position
            int freqValue = 1;

            // computes the spectrum data, the code is taken from a bass_wasapi sample.
            for (bandX = 0; bandX < _form2.numberOfBar; bandX++)
            {
                float[] peak = new float[] { 0f, 0f };      // 0=Left(mono), 1=Right

                freqValue = (int)(Math.Pow(2, (bandX * 10.0 / _form2.numberOfBar) + freqShift) * mixfreqMultiplyer);

                if (freqValue <= fftPos) freqValue = fftPos + 1;                                            // if out of range, min. freq. selected
                if (freqValue > valueBase - channel) freqValue = valueBase - channel;                       // truncate last data

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

                    ReloadDefaultDevice(this, EventArgs.Empty);
                }
            }
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

            if (!Bass.BASS_Init(0, mixfreq, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                throw new Exception("Bass.dll Initialize Error");
            Enable = true;

            // fire channel change event to form1
            NumberOfChannelChanged?.Invoke(this, EventArgs.Empty);
        }

        // helper function
        private void SetParamFromFreq(int freq)
        {
            // freq is readonly

            /* adjustTable is based on actual measurements, so, little bit funny...
              bands:    Dummy  Dummy  Dummy    8,    16,     32
               mono   { 0.41f, 0.41f, 0.41f, 0.41f, 0.33f, 0.22f }  less -> shift Hi freq, large -> shift Low freq
               stereo {  1.8f,  1.8f,  1.8f,  1.8f, 1.25f,  0.9f }
            */

            if (freq <= 48000)            // 44.1khz, 48khz
            {
                DATAFLAG = channel > 1 ? BASSData.BASS_DATA_FFT4096 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT2048;
                mixfreqMultiplyer = 48000f / freq * 0.25f * adjustTable[channel - 1][(int)Math.Log(_form2.numberOfBar, 2)];
                valueBase = 4096 * channel;
            }
            else if (freq <= 96000)       // 88.2khz, 96khz
            {
                DATAFLAG = channel > 1 ? BASSData.BASS_DATA_FFT8192 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT4096;
                mixfreqMultiplyer = 48000f / freq * 0.5f * adjustTable[channel - 1][(int)Math.Log(_form2.numberOfBar, 2)];
                valueBase = 8192 * channel;
            }
            else if (freq <= 192000)      // 176.4khz, 192khz
            {
                DATAFLAG = channel > 1 ? BASSData.BASS_DATA_FFT16384 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT8192;
                mixfreqMultiplyer = 48000f / freq * adjustTable[channel - 1][(int)Math.Log(_form2.numberOfBar, 2)];
                valueBase = 16384 * channel;
            }
            else                          // 320khz and above?
            {
                DATAFLAG = channel > 1 ? BASSData.BASS_DATA_FFT32768 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT16384;
                mixfreqMultiplyer = 48000f / freq * 2f * adjustTable[channel - 1][(int)Math.Log(_form2.numberOfBar, 2)];
                valueBase = 16384 * channel;
            }

            // debug
            //_form2.label2.Text = adjustTable[channel - 1][(int)Math.Log(_form2.numberOfBar, 2)].ToString();
            //_form2.label2.Text = mixfreqMultiplyer.ToString();
        }
        
        // cleanup
        public void Free()
        {
            if (devicenumber > 0)
            {
                BassWasapi.BASS_WASAPI_Free();
                Bass.BASS_Free();
            }
        }

    }
}