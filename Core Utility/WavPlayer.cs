using NAudio.Wave;
using System;

namespace Rambot.Core.Util
{
    public class WavPlayer
    {
        public static void Play(string file, string audioLine)
        {
            DirectSoundOut device = null;
            foreach (var deviceInfo in DirectSoundOut.Devices)
            { 
                if (deviceInfo.Description == (audioLine))
                {
                    device = new DirectSoundOut(deviceInfo.Guid);
                    break;
                }
            }
           
            if (device == null)
            {
                throw new NullReferenceException("Device with name "+ audioLine +" not found."); 
            }

            device.Init(new WaveChannel32(new WaveFileReader(file)));
            device.Play();
        }

        private DirectSoundOut Device = null;
        public WavPlayer(string audioLine)
        {
            foreach (var deviceInfo in DirectSoundOut.Devices)
            {
                if (deviceInfo.Description == (audioLine))
                {
                    Device = new DirectSoundOut(deviceInfo.Guid);
                    break; 
                }
            }
        }

        public void PlayInLine(string file)
        {
            Device.Init(new WaveChannel32(new WaveFileReader(file)));
            Device.Play();
        }
    }
}
