using NAudio;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voicio.Models
{
    public class NAudioRecorder
    {
        private WaveInEvent microphone;
        private bool IsRecording = false;
        private BufferedWaveProvider waveProvider;
        private void DataAvailableEvent(object sender, WaveInEventArgs e)
        {
            waveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }
        public void Record()
        {
            IsRecording = true;
            microphone.StartRecording();
        }
        public void Stop()
        {
            IsRecording = false;
            microphone.StopRecording();
        }
        public int GetByteArray(byte[] buffer, int offset, int count)
        {
            return waveProvider.Read(buffer, offset, count);
        }
        public NAudioRecorder() {
            microphone = new WaveInEvent()
            {
                WaveFormat = new WaveFormat(44100, 1)
            };
            microphone.DataAvailable += DataAvailableEvent;
            waveProvider = new BufferedWaveProvider(microphone.WaveFormat);
        }
    }
}
