using System;
using System.Collections.Generic;
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
        readonly private ComboBox _devicelist;          // device list
        private bool _enable;                           // enabled status
        private bool _initialized;                      // initialized flag
        private readonly WASAPIPROC _process;           // callback function to obtain data
        private int deviceindex;                        // used device index
        public List<byte> _spectrumdata;                // spectrum data buffer
        public readonly DispatcherTimer _timer1;        // timer that refreshes the display
        private readonly BASSData _DATAFLAG;            // for "mix-data" format
        private readonly Button _form2EnumButton;       // for subscribe
        private readonly ComboBox _form2NumberOfBar;    // for subscribe
        public event EventHandler SpectrumChanged;      // for fire

        public readonly int _channel = 2;               // 1: "mix-data"(mono) 2: L+R
        public int _lines = 8;                          // default number of spectrum lines


        public Analyzer(ComboBox devicelist, Button enumButton, ComboBox numberofbar)    // イベントを受信するコントロールを登録
        {
            _fft = new float[8192 * _channel];
            _devicelist = devicelist;
            _initialized = false;
            _process = new WASAPIPROC(Process);
            _timer1 = new DispatcherTimer();
            _timer1.Tick += Timer1_Tick;
            _timer1.Interval = TimeSpan.FromMilliseconds(25);   // 40hz refresh rate // 16.667? 60hz
            _timer1.IsEnabled = false;
            _spectrumdata = new List<byte>();
            _form2EnumButton = enumButton;
            _form2NumberOfBar = numberofbar;
            _DATAFLAG = _channel > 1 ? BASSData.BASS_DATA_FFT16384 | BASSData.BASS_DATA_FFT_INDIVIDUAL : BASSData.BASS_DATA_FFT8192;

            // Event handler for option form (reseive)
            _form2EnumButton.Click += new EventHandler(this.Form2_EnumerateButton_Click);
            _form2NumberOfBar.SelectedIndexChanged += new EventHandler(this.Form2_NumberOfBarIndexChanged);

            Init();
        }

        // flag for display enable
        public bool DisplayEnable { get; set; }

        // initialization
        private void Init()
        {
            bool result = false;
            int i = 9;          // Configfileで処理するようにする
            var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
            if (device.IsEnabled && device.IsLoopback)
            {
                // Modified cheaply because of Bass.dll's not-good handling of system default encoding.
                if (device.name[0] > 0x007f)
                {
                    byte[] byteStream = Encoding.GetEncoding("UTF-8").GetBytes(device.name);
                    byteStream = GetTrueSjisByte(byteStream);
                    string trueSjisName = Encoding.GetEncoding(Encoding.Default.CodePage).GetString(byteStream);
                    _devicelist.Items.Add(string.Format("{0} - {1}", i, trueSjisName));
                }
                else _devicelist.Items.Add(string.Format("{0} - {1}", i, device.name));
            }

            try
            {
                _devicelist.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("No Output Device found.\r\nExit the program.",
                    "No Output Device found.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                Free();
                Environment.Exit(0);    // Exit immediately
            }
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
            result = Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);   // "no sound" device for Bass.dll
            if (!result) throw new Exception("Source Device Initialize Error");
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
                        var deviceArray = (_devicelist.Items[_devicelist.SelectedIndex] as string).Split(' ');
                        deviceindex = Convert.ToInt32(deviceArray[0]);
                        bool result = BassWasapi.BASS_WASAPI_Init(deviceindex, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, _process, IntPtr.Zero);
                        if (!result)
                        {
                            var error = Bass.BASS_ErrorGetCode();
                            MessageBox.Show(error.ToString());
                        }
                        else
                        {
                            _initialized = true;
                            _devicelist.Enabled = false;
                        }
                    }
                    BassWasapi.BASS_WASAPI_Start();
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

        //イベントの受信処理用
        //timer
        private void Timer1_Tick(object sender, EventArgs e)
        {
            int ret = BassWasapi.BASS_WASAPI_GetData(_fft, (int)_DATAFLAG);                 //get channel fft data
            if (ret < -1) return;
            int bandX, powerY;
            int fftPos = 0;     // bufferデータ内の位置
            int freqValue = 1;

            // computes the spectrum data, the code is taken from a bass_wasapi sample.
            for (bandX = 0; bandX < _lines; bandX++)
            {
                float peak_left = 0;
                float peak_right = 0;
                freqValue = (int)Math.Pow(2, (bandX * 10.0/(_lines - 1))+4.35);             // 4.35 from actual measurement, not logic...
                if (freqValue <= fftPos) freqValue = fftPos + 1;                            // なぜか範囲外だったらバンドの最低周波数にする
                if (freqValue > 16384 - _channel) freqValue = 16384 - _channel;             // 最後のデータははみ出る
                //for (; freqPos < freqValue; freqPos++)                                    // 周波数バンド内を順に調べる
                for (; fftPos < freqValue; fftPos+=_channel)                                // 周波数バンド内を順にinterreaveで調べる
                {
                    for (int i = 0; i < _channel; i++)                                      // interreave対応
                    { 
                        if (peak_left < _fft[1 + fftPos]) peak_left = _fft[1 + fftPos];     // _fft[x]の最大値を探してpeakに代入
                                                                                            // なぜかLとRが逆かも
                        if (peak_right < _fft[1 + fftPos + (_channel - 1)]) peak_right = _fft[1 + fftPos + (_channel - 1)];
                    }
                }
                powerY = (int)(Math.Sqrt(peak_left) * 3 * 255 - 4);                         // sqrt to make low values more visible
                if (powerY > 255) powerY = 255;
                if (powerY < 0) powerY = 0;
                _spectrumdata.Add((byte)powerY);
                if (_channel > 1)                                                           // interreave対応 L,R,L,R,...
                {
                    powerY = (int)(Math.Sqrt(peak_right) * 3 * 255 - 4);
                    if (powerY > 255) powerY = 255;
                    if (powerY < 0) powerY = 0;
                    _spectrumdata.Add((byte)powerY);
                }
            }

            // 描画データ処理を発火
            if (SpectrumChanged != null) SpectrumChanged(this, EventArgs.Empty);
            
            _spectrumdata.Clear();
        }
             
        private void Form2_NumberOfBarIndexChanged(object sender, EventArgs e)
        {
            _lines = Convert.ToInt16(_form2NumberOfBar.SelectedItem);
        }
        
        private void Form2_EnumerateButton_Click(object sender, EventArgs e)
        {
            _form2EnumButton.Enabled = false;
            _initialized = false;
            Enable = false;
            Free();

            bool result = false;
            
            for (int i = 0; i < BassWasapi.BASS_WASAPI_GetDeviceCount(); i++)
            {
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
                if (device.IsEnabled && device.IsLoopback)
                {
                    // Modified cheaply because of Bass.dll's not-good handling of system default encoding.
                    if (device.name[0] > 0x007f)
                    {
                        byte[] byteStream = Encoding.GetEncoding("UTF-8").GetBytes(device.name);
                        byteStream = GetTrueSjisByte(byteStream);
                        string trueSjisName = Encoding.GetEncoding(Encoding.Default.CodePage).GetString(byteStream);
                        _devicelist.Items.Add(string.Format("{0} - {1}", i, trueSjisName));
                    }
                    else _devicelist.Items.Add(string.Format("{0} - {1}", i, device.name));
                }
            }

            _devicelist.SelectedIndex = 0;
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
            result = Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            if (!result) throw new Exception("Source Device Initialize Error"); 
            
            Enable = true;
            _form2EnumButton.Enabled = true;
        }

        // cleanup
        public void Free()
        {
            BassWasapi.BASS_WASAPI_Free();
            Bass.BASS_Free();
        }

        private byte[] GetTrueSjisByte(byte[] byteStream)
        {
            byte[] bs = byteStream;
            int bsPos = 0;
            int outPos = 0;
            byte[] key = { 0xef, 0xbf, 0xbd };
            byte[] output = new byte[256];
            while (bsPos < bs.Length)
            {
                if (bs[bsPos] == key[0] && bs[bsPos + 1] == key[1] && bs[bsPos + 2] == key[2])
                {
                    if (bs[bsPos + 3] == 0x5b)
                    {
                        output[outPos] = 0x81;
                        outPos++;
                        bsPos += 3;
                    }
                    else if (bs[bsPos + 3] == key[0] && bs[bsPos + 4] == key[1] && bs[bsPos + 5] == key[2]
                        && bs[bsPos + 6] == key[0] && bs[bsPos + 7] == key[1] && bs[bsPos + 8] == key[2])
                    {
                        output[outPos] = 0x83;
                        output[outPos + 1] = 0x93;
                        outPos += 2;
                        bsPos += 6;
                    }
                    else if (bs[bsPos + 3] == key[0] && bs[bsPos + 4] == key[1] && bs[bsPos + 5] == key[2])
                    {
                        output[outPos] = 0x83;
                        output[outPos + 1] = 0x8b;
                        outPos += 2;
                        bsPos += 3;
                    }
                    else if (bs[bsPos + 3] == 0x20)
                    {
                        bsPos += 3;
                    }
                    else
                    {
                        output[outPos] = 0x83;
                        outPos++;
                        bsPos += 3;
                    }

                }
                else
                {
                    output[outPos] = bs[bsPos];
                    bsPos++;
                    outPos++;
                }
            }
            Array.Resize(ref output, outPos);

            return output;
        }
    }
}
