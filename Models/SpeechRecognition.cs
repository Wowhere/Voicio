﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Vosk;

namespace voicio.Models
{
    public class SpeechRecognition
    {
        private VoskRecognizer rec;
        public string Recognize(byte[] buffer)
        {
            using (MemoryStream source = new MemoryStream(buffer))
            {
                //byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (rec.AcceptWaveform(buffer, bytesRead))
                    {
                        Console.WriteLine(rec.Result());
                    }
                    else
                    {
                        Console.WriteLine(rec.PartialResult());
                    }
                }
            }
            return rec.FinalResult();
        }
        public SpeechRecognition(string modelpath) {
            Model model = new Model(modelpath);
            rec = new VoskRecognizer(model, 16000.0f);
            rec.SetMaxAlternatives(0);
            rec.SetWords(true);
        }
    }
}
