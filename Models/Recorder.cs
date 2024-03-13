using NAudio;
using NAudio.Wave;
using Vosk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.IO;
using Avalonia;

namespace voicio.Models
{
    public class NAudioRecorder
{
    private WaveInEvent Microphone;
    private bool IsRecording = false;
    //private BufferedWaveProvider bufferedWaveProvider;
    private WaveFileWriter CustomWaveProvider;
    private System.Timers.Timer RecordTimer;
    private MemoryStream CustomStream;
    private void DataAvailableEvent(object sender, WaveInEventArgs e)
    {
        CustomWaveProvider.Write(e.Buffer, 0, e.BytesRecorded);
        
    }
    //private void OnRecordingStopped(object sender, StoppedEventArgs e)
    //{
    //    Microphone.Dispose();
    //    Microphone = null;
    //    RecordTimer.Dispose();
    //    RecordTimer = null;
    //}
    //public async Task StartRecord()
    public void StartRecord()
    {
        IsRecording = true;
        Microphone.StartRecording();
        RecordTimer = new System.Timers.Timer(5000);
        RecordTimer.Elapsed += new ElapsedEventHandler(StopRecord);
        RecordTimer.Start();
    }
    public void StopRecord(object sender, EventArgs e)
    {
        IsRecording = false;
        Microphone.StopRecording();
        RecordTimer.Dispose();
        //Console.WriteLine(e);
    }
    public byte[] GetByteArray()
    {
        return CustomStream.ToArray();
    }
    public NAudioRecorder()
    {
        Microphone = new WaveInEvent()
        {
            WaveFormat = new WaveFormat(rate: 44100, bits: 16, channels: 1),
            DeviceNumber = 0,
            BufferMilliseconds = 100,
        };
        Microphone.DataAvailable += DataAvailableEvent;
        CustomStream = new MemoryStream();
        CustomWaveProvider = new WaveFileWriter(CustomStream, Microphone.WaveFormat) { };
    }
}
}
