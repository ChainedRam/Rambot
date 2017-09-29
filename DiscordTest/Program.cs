using Discord;
using Discord.Audio;
using Discord.Net.Rest;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestFramework
{

    //From https://github.com/RogueException/Discord.Net/blob/dev/docs/guides/getting_started/samples/intro/
    class Program
    {
        static void Main(string[] args)
        {
            Discord0_9_6();
            //new Program().Discord1_0_2().GetAwaiter().GetResult();
            Debug.WriteLine("Ended");
        }

        /* 
        private async Task Discord1_0_2()
        {
            //create bot 
            DiscordSocketClient botClient = new DiscordSocketClient();

            botClient.MessageReceived += MessageRecieved; 

            await botClient.LoginAsync(TokenType.Bot, "MzI2NjEyODA5NTA3MDEyNjE4.DCpVeg.rLzQZbdzABjGAfAisDAPF86f29c");

            await botClient.StartAsync();



            Debug.WriteLine("Connected bot");

            //lock for ever
            await Task.Delay(-1);
        }

        private Task MessageRecieved(SocketMessage m)
        {
            Debug.WriteLine("Recieved message " + m.ToString() + " from " + m.Author + " in channel " + m.Channel.Name + ":" + m.Channel.Id);
            return Task.CompletedTask; 
        }*/


        static DiscordClient botClient;
        static IAudioClient client;
        static Channel cch;

        static void Discord0_9_6()
        {
            botClient = new DiscordClient();

            botClient.UsingAudio(x => { x.Mode = AudioMode.Outgoing; x.EnableEncryption = false; });

            //AudioService audio = botClient.AddService<AudioService>();

            Debug.WriteLine("Connecting bot");

            botClient.ExecuteAndWait(async () =>
            {
                //use you access token
                await botClient.Connect("MzI2NjEyODA5NTA3MDEyNjE4.DCpVeg.rLzQZbdzABjGAfAisDAPF86f29c", TokenType.Bot);

                Thread.Sleep(500);

                Debug.WriteLine("Connected bot");

                Server chainedRam = null;

                foreach (var item in botClient.Servers)
                {
                    Debug.WriteLine("server: " + item.Name);

                    if (item.Name == "chainedram")
                    {
                        chainedRam = item;
                    }
                }

                foreach (var channel in chainedRam.TextChannels)
                {
                    cch = channel;
                }
                foreach (var channel in chainedRam.VoiceChannels)
                {
                    var serv = botClient.GetService<AudioService>();
                    Debug.WriteLine("channel: " + channel.Name);

                    //Channel to join to
                    if (channel.Name == "General")
                    {
                        Debug.WriteLine("Got service!");

                        client = await serv.Join(channel);
                        Thread.Sleep(500);

                        Debug.WriteLine("Joined: " + channel.Name + "  " + serv.Config.Channels);
                        break;
                    }
                }

                //Adding event listener 
                //c.JoinedServer += C_JoinedServer;
                //c.UserLeft += C_UserLeft;
                //c.UserUpdated += C_UserUpdated;
                //c.UserJoined += C_UserJoined;

                botClient.UserIsTyping += C_UserIsTyping;

                botClient.MessageUpdated += BotClient_MessageUpdated;
                botClient.MessageReceived += BotClient_MessageReceived;

                Debug.WriteLine("Lock and loaded!");
                while (true) ;
                Debug.WriteLine("Stopped");

            });

        }

        static string voice = "";

        private static void BotClient_MessageReceived(object sender, MessageEventArgs e)
        {
            Debug.WriteLine("Wrote: " + e.Message.RawText);

            if (e.Message.RawText.StartsWith("!say "))
            {
                using (SpeechSynthesizer s = new SpeechSynthesizer())
                {
                    s.Volume = 100;

                    if (!string.IsNullOrEmpty(voice))
                    {
                        s.SelectVoice(voice);
                    }

                    s.SetOutputToWaveFile("temp.wav", new SpeechAudioFormatInfo(48000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo));



                    string speach = e.Message.RawText.Substring("!say ".Length);
                    s.Speak(speach);
                }

                using (var MP3Reader = new WaveFileReader("temp.wav"))
                {
                    MP3Reader.Seek(0, System.IO.SeekOrigin.Begin);

                    var serv = botClient.GetService<AudioService>();

                    WaveFormat OutFormat = new WaveFormat(48000, 16, serv.Config.Channels);
                    int blockSize = OutFormat.AverageBytesPerSecond;
                    byte[] buffer = new byte[blockSize];
                    using (var resampler = new MediaFoundationResampler(MP3Reader, OutFormat))

                    {
                        resampler.ResamplerQuality = 60;

                        int byteCount;
                        while ((byteCount = resampler.Read(buffer, 0, blockSize)) > 0)
                        {
                            if (byteCount < blockSize)
                            {
                                for (int i = byteCount; i < blockSize; i++)
                                {
                                    buffer[i] = 0;
                                }
                            }
                            client.Send(buffer, 0, blockSize);
                        }
                    }
                }

            }
            else if (e.Message.RawText.StartsWith("!voice list"))
            {
                using (SpeechSynthesizer s = new SpeechSynthesizer())
                {
                    cch.SendMessage("Available voices: ");
                    foreach (var v in s.GetInstalledVoices())
                    {
                        cch.SendMessage(v.VoiceInfo.Name);
                    }
                    cch.SendMessage("End of voices available.");
                }
            }
            else if (e.Message.RawText.StartsWith("!voice set "))
            {
                string speach = e.Message.RawText.Substring("!voice set ".Length);

                voice = speach;
            }

        }

        private static void BotClient_MessageUpdated(object sender, MessageUpdatedEventArgs e)
        {

        }

        private static void C_JoinedServer(object sender, ServerEventArgs e)
        {
            Debug.WriteLine("JoinedServer : " + e.Server);
        }

        private static void C_UserUpdated(object sender, UserUpdatedEventArgs e)
        {
            Debug.WriteLine("Update : " + e.Before.Name);
        }

        private static void C_UserLeft(object sender, UserEventArgs e)
        {
            Debug.WriteLine("Left : " + e.User.Name);
        }

        private static void C_UserIsTyping(object sender, ChannelUserEventArgs e)
        {
            Debug.WriteLine("Typing : " + e.User.Name);

        }

        private static void C_UserJoined(object sender, UserEventArgs e)
        {
            Debug.WriteLine("Joined : " + e.User.Name);
        }
    }
}
