using NAudio;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.IO;

namespace voicio.Models
{
    public class NAudioRecorder
    {
        private WaveInEvent Microphone;
        private bool IsRecording = false;
        private BufferedWaveProvider WaveProvider;
        private Timer RecordTimer;
        private void DataAvailableEvent(object sender, WaveInEventArgs e)
        {
            WaveProvider.AddSamples(e.Buffer, 0, e.Buffer.Length);
        }
        //private void OnRecordingStopped(object sender, StoppedEventArgs e)
        //{
        //    Microphone.Dispose();
        //    Microphone = null;
        //    RecordTimer.Dispose();
        //    RecordTimer = null;
        //}
        public async Task StartRecord()
        {
            IsRecording = true;
            Microphone.StartRecording();
            RecordTimer = new Timer(5000);
            RecordTimer.Elapsed += (sender,e) => StopRecord();
            RecordTimer.AutoReset = false;
            RecordTimer.Start();
        }
        public void StopRecord()
        {
            IsRecording = false;
            Microphone.StopRecording();
        }
        public byte[] GetByteArray()
        {
            var buffer = new byte[] { };
            //var stream = new MemoryStream(WaveProvider.Read);
            var i = WaveProvider.Read(buffer, 0, WaveProvider.BufferedBytes);
            return buffer;
        }
        public NAudioRecorder() {
            Microphone = new WaveInEvent()
            {
                WaveFormat = new WaveFormat(44100, 1)
            };
            Microphone.DataAvailable += DataAvailableEvent;
            //Microphone.RecordingStopped += OnRecordingStopped;
            WaveProvider = new BufferedWaveProvider(Microphone.WaveFormat);
        }
    }
}
