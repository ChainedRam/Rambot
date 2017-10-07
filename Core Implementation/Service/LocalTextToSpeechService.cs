using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Speech.Synthesis;
using NAudio.Wave;
using System.Text.RegularExpressions;
using KLDev.Rambot.Interface;
using KLDev.Rambot.Core.Enum;
using KLDev.Rambot.Exceptions;

namespace KLDev.Rambot.Core
{
    public class LocalTextToSpeechService : ITextToSpeechService
    {
        public RamServiceType Services
        {
            get
            {
                return RamServiceType.TextToSpeech; 
            }
        }

        private Guid DeviceGuid;
        private object SpeakLock;
        private bool IsSpeaking;
        private Dictionary<string, int> OffsetVoices; 

        public LocalTextToSpeechService(string device)
        {
            DeviceGuid = DirectSoundOut.Devices.Where(d => d.Description == device).SingleOrDefault().Guid;
            SpeakLock = new object();
            IsSpeaking = false;

            OffsetVoices = new Dictionary<string, int>();

            //Supported voice 
            OffsetVoices.Add("^Cepstral\\s+\\w+", 335062); 
        }

       
        public void Say(string msg, string voice = "1")
        {
            lock (SpeakLock)
            {
                if(IsSpeaking)
                {
                    throw new RambotSpeechInteruptedException(); 
                }

                IsSpeaking = true; 
                if (msg.Contains('/'))
                {
                    voice = msg.Split('/')[0];

                    msg = msg.Split('/')[1];
                }

                MemoryStream stream = new MemoryStream();
                DirectSoundOut Device = new DirectSoundOut(DeviceGuid);

                using (SpeechSynthesizer speech = new SpeechSynthesizer())
                {
                    int offset = 0;

                    if (!string.IsNullOrEmpty(voice))
                    {
                        string voiceName;
                        if(Regex.IsMatch(voice, "^\\d+$"))
                        {
                            int index = int.Parse(voice) - 1; 
                            if (index < 0)
                            {
                                throw new RambotIndexOutOfBoundException("Voice index needs to be positive.");
                            }

                            if(index >= GetVoices().Length)
                            {
                                throw new RambotIndexOutOfBoundException("Voice index exceeds excpectation (Max:" + GetVoices().Length + ").");
                            }

                            voiceName = GetVoices()[index]; 
                        }
                        else
                        {
                            string[] selectedVoice = GetVoices().Where(v => v.Contains(voice)).ToArray(); 

                            if(selectedVoice.Length == 0)
                            {
                                throw new RambotUndefinedVoiceException("'" + voice + "' voice doesn't exist."); 
                            }
                            else if(selectedVoice.Length != 1)
                            {
                                throw new RambotDuplicateVoiceMatchException("Confused between voices: {" + string.Join(",", selectedVoice) + "}.");
                            }
                            voiceName = selectedVoice[0];
                           
                        }

                        speech.SelectVoice(voiceName);
                        offset = OffsetVoices.Where(c => Regex.IsMatch(voiceName, c.Key)).Select(d => d.Value).SingleOrDefault();
                    }

                    speech.SetOutputToWaveStream(stream);
                    speech.Speak(msg);

                    stream.Seek(offset, SeekOrigin.Begin);

                    Device.Init(new RawSourceWaveStream(stream, new WaveFormat(22050, 16, 1)));
                    Device.Play();
                    Device.PlaybackStopped += (a, b) => { IsSpeaking = false; }; 
                }
            }
        }

        public string[] GetVoices()
        {
            using (SpeechSynthesizer speech = new SpeechSynthesizer())
            {
                return speech.GetInstalledVoices().Select(v => v.VoiceInfo.Name).ToArray();
            }
        }
    }
}
