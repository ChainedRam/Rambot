using KLDev.Rambot.Core;
using KLDev.Rambot.Interface;
using Rambot.Core.Util;
using System;

namespace TestUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("END");
            Console.ReadKey(); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b">base string</param>
        /// <param name="s">string to compare to</param>
        /// <returns></returns>
        static float Compare(string b, string s) // 1.0 = %100, .5 => %50 
        {
            int hit = 0;
            int miss = 0;

            for (int i = 0,  j=0; i < b.Length && j < s.Length; i++, j++)
            { 
                if(b[i] == s[j])
                {
                    hit++; 
                }
                else
                {
                    miss++; 
                }
            }

            return 0; 
        }

        static bool IsSkippable(int iLength, int jLength, ref int i, ref int j)
        {
            if(iLength > jLength)
            {
                i++;
                return true; 
            }
            else if(jLength > iLength)
            {
                j++;
                return true; 
            }

            return false; 
        }

        static void BotRunTest()
        {
            string speakers = "Speakers (Realtek High Definition Audio)";
            string wireless = "Wireless Speakers (Logitech G930 Headset)";

            string line1 = "Line 1 (Virtual Audio Cable)";

            IBot ram = RamBuilder.Create()
                .WithClipCollectionService(new LocalClipCollection("C:\\Ram\\SoundCollection"))
                .WithClipPlayerService(new LocalDeviceClipPlayer(speakers))
                .WithTextToSpeechService(new LocalTextToSpeechService(speakers));

            PlayCommand play = new PlayCommand();

            SayCommand say = new SayCommand();

            //Debug.WriteLine(play.Execute("play -list e", default(Guid), ram));
            //using (SpeechSynthesizer speech = new SpeechSynthesizer())
            //{
            //speech.Speak("hello");
            //}

            LocalTextToSpeechService sayService = new LocalTextToSpeechService(speakers);

            try
            {
                //Debug.WriteLine(say.Execute("say -voice", default(Guid), ram)); 

                //sayService.Say("5/hello");
                //say.Say("David/hello I like trains");
                //Debug.WriteLine("hello".Substring("hello".Length) + "he"); 

                //Thread.Sleep(2000); 
                //say.Say("Mic/twotwo i like to speech yeah yeah baby", "0");
            }
            catch (Exception e)
            {

            }
            //say.Say("three");

            //RAMBOTEntities ram = new RAMBOTEntities();

            //delete
            //var todelete = ram.RamUsers.Where(r => r.ServerId == "Test");
            //if (todelete != null)
            //{
            //   ram.RamUsers.RemoveRange(todelete);
            //   ram.SaveChanges();
            // }

            //ram.RamUsers.Add(new RamUser() {ServerId="Test", Name="Test" });
            //ram.SaveChanges();

        }

        static void ConsoleApplication()
        {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
        }
    }
}
