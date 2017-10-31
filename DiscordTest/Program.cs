using Discord;
using Discord.Audio;
using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
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
                    if(channel.Name == "general")
                    {
                        //await channel.SendMessage("Time to sleep. I'll be back within 20 hours.");
                    }
                }
                Channel GeneralChannel = null; 
                foreach (var channel in chainedRam.VoiceChannels)
                {
                    var serv = botClient.GetService<AudioService>();
                    Debug.WriteLine("channel: " + channel.Name);

                    //Channel to join to
                    if (channel.Name == "Dorper")
                    {
                        GeneralChannel = channel; 
                        Debug.WriteLine("Got service!");
                        try
                        {
                            client = await serv.Join(channel);
                        }
                        catch (Exception e)
                        {

                        }
                        Thread.Sleep(500);

                        Debug.WriteLine("Joined: " + channel.Name + "  " + serv.Config.Channels);
                        break;
                    }
                }

                //Adding event listener 
                botClient.JoinedServer += C_JoinedServer;
                botClient.UserLeft += C_UserLeft;
                botClient.UserUpdated += C_UserUpdated;

                botClient.UserJoined += C_UserJoined;

              
                //botClient.UserIsTyping += C_UserIsTyping;

                botClient.MessageUpdated += BotClient_MessageUpdated;
                botClient.MessageReceived += BotClient_MessageReceived;

                Debug.WriteLine("Lock and loaded!");

                int i = 0; 
                while (true)
                {
                    var serv = botClient.GetService<AudioService>();
                    botClient.SetGame(new Game("Botting " + i++));
                    Debug.WriteLine("Setting Game");
                    client = await serv.Join(GeneralChannel);
                    Thread.Sleep(60 * 1000); //5 minutes 
                }

                Debug.WriteLine("Stopped");
            });

           
        }

        static string voice = "";

        private static void BotClient_MessageReceived(object sender, MessageEventArgs e)
        {
            Debug.WriteLine("Wrote: " + e.Message.RawText);

            if (e.Message.RawText.StartsWith("!say "))
            {
                string speach = e.Message.RawText.Substring("!say ".Length);

                Say(speach); 
            }
            else if (e.Message.RawText.StartsWith("!voice list"))
            {
                string build = ""; 
                using (SpeechSynthesizer s = new SpeechSynthesizer())
                {
                    build += "Available voices: \n";
                    foreach (var v in s.GetInstalledVoices())
                    {
                        build +=  (v.VoiceInfo.Name + "\n");
                    }
                    build += ("End of voices available.");

                }
               e.Channel.SendMessage(build); 
            }
            else if (e.Message.RawText.StartsWith("!voice set "))
            {
                string voiceName = e.Message.RawText.Substring("!voice set ".Length);
                using (SpeechSynthesizer s = new SpeechSynthesizer())
                {
                    foreach (var v in s.GetInstalledVoices())
                    {
                        if(v.VoiceInfo.Name == voiceName)
                        {
                            voice = voiceName;
                            e.Channel.SendMessage("Voice is set");
                            return; 
                        }
                    }
                }
                e.Channel.SendMessage("Voice doesn't exist.");
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
            //Debug.WriteLine("Update : " + e.Before.Name);

            if (e.After.VoiceChannel != null  && e.After.VoiceChannel.Name == "Dorper" && e.Before.VoiceChannel?.Name != "Dorper")
            {
                Say("Welcome " + e.After.Name);
            }
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


        private static void Say(string speech)
        {
            using (SpeechSynthesizer s = new SpeechSynthesizer())
            {
                s.Volume = 100;

                if (!string.IsNullOrEmpty(voice))
                {
                    s.SelectVoice(voice);
                }

                s.SetOutputToWaveFile("temp.wav", new SpeechAudioFormatInfo(48000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo));

                s.Speak(speech);
            }

            using (var MP3Reader = new WaveFileReader("temp.wav"))
            {
                int offset = voice.StartsWith("Cepstral") ? 1173560 : 0;

                Debug.WriteLine("Length " + MP3Reader.Length);

                MP3Reader.Seek(offset, System.IO.SeekOrigin.Begin);

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
    }
}
