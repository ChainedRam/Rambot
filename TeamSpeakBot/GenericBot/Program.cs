using System;
using System.Diagnostics;
using NAudio.Wave;
using KLD.TeamSpeak.Core;

namespace Rambot.Core.Impl
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                DirectSoundOut device = null;
                foreach (var deviceInfo in DirectSoundOut.Devices)
                {
                    if (deviceInfo.Description == ("CABLE Input (VB-Audio Virtual Cable)"))
                    {
                        device = new DirectSoundOut(deviceInfo.Guid);
                    }
                }
                var wave = new WaveChannel32(new WaveFileReader(@"C:\Users\kld00\Desktop\weeb.wav")); 
                device.Init(wave);
                device.Play();


 
                while (wave.Position <= wave.Length)
                {
                    
                }
               

            }

            Console.ReadLine(); 
            
            //playSoundInAudioLineTest(); 
            // saveLoadCmdTest(); 
        }

        /// <summary>
        /// Testing playing sound in a custom audio line 
        /// </summary>
        static void playSoundInAudioLineTest()
        {

            //WavPlayer.Play(new WaveFileReader(@"C:\Ram\SoundCollection\sadsong.wav"), "Line 1 (Virtual Audio Cable)"); 

            
         
            Console.ReadLine();
        }


        /// <summary>
        /// Testing saving a command and loading it then executing 
        /// </summary>
        static void saveLoadCmdTest()
        {
            Loader loader = Loader.Instence;

            Command cmd = new TestCommand();


            Debug.WriteLine("First " + cmd.Execute(null, new Guid(), null));


            loader.WriteToBinaryFile("C:\\Ram\\Visual Studio Projects\\TeamSpeakBot\\GenericBot\\excutables\\test.cmd", cmd);


            Command loadedCmd = loader.ReadFromBinaryFile<Command>("C:\\Ram\\Visual Studio Projects\\TeamSpeakBot\\GenericBot\\excutables\\tedfdgfdst.cmd");


            Debug.WriteLine("VIEWS " + loadedCmd.Execute(null, new Guid(), null));

        }
    }
}
