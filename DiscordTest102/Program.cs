using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Diagnostics;
using Discord.Rpc;
using Discord.Rest;
using Discord.Audio;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using NAudio.Wave;
using Discord.Commands.Builders;

namespace DiscordTest102
{
    public class Program : ModuleBase
    {
        public Program()
        {

        }


        private static void Main(string[] args) => new Program().SuckitClient().GetAwaiter().GetResult();

        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;

        public async Task SuckitClient()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            // Avoid hard coding your token. Use an external source instead in your code.
            string token = "MzI2NjEyODA5NTA3MDEyNjE4.DCpVeg.rLzQZbdzABjGAfAisDAPF86f29c";

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();


            await InstallCommandsAsync();

            var commamnds = _commands.Commands;


            CommandInfo[] infors = commamnds.ToArray();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            /*SocketGuild guild = _client.Get
            

            var channels = guild.VoiceChannels;*/
            foreach(var c in _client.DMChannels)
            {
                Debug.WriteLine(c.ToString());
            }

           await Task.Delay(-1);
        }

        private async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommandAsync;

            _client.UserUpdated += C_UserUpdated;
            _client.CurrentUserUpdated += _client_CurrentUserUpdated;
            // Discover all of the commands in this assembly and load them.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

            //await  _commands.AddModuleAsync<Program>();
        }

        private async Task _client_CurrentUserUpdated(SocketSelfUser arg1, SocketSelfUser arg2)
        {
            await arg2.SendMessageAsync("wtf");
        }

        private async Task C_UserUpdated(SocketUser before, SocketUser after)
        {
            await after.SendMessageAsync("suck it bitch");
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            msg = messageParam; 
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
            {
                return;
            }
            // Create a Command Context
            var context = new SocketCommandContext(_client, message);

            //await context.Channel.SendMessageAsync("I'm have my own self conscious");


            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed successfully)
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private static SocketMessage msg; 

        [Command("join")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = (msg.Author as IGuildUser)?.VoiceChannel;

            if (channel == null)
            {
                return; 
            }
                
            //{ await msg.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            var audioClient = await channel.ConnectAsync();

            using (SpeechSynthesizer s = new SpeechSynthesizer())
            {
                s.SetOutputToWaveFile("temp.wav", new SpeechAudioFormatInfo(48000, AudioBitsPerSample.Sixteen, AudioChannel.Stereo));

                s.Speak("Fuck");
            }


            await SendAsync(audioClient,"temp.wav"); 
            /*
            using (var MP3Reader = new WaveFileReader("temp.wav"))
            {
                int offset = 0;// voice.StartsWith("Cepstral") ? 1173560 : 0;

                Debug.WriteLine("Length " + MP3Reader.Length);

                MP3Reader.Seek(offset, System.IO.SeekOrigin.Begin);

                var serv = audioClient; 

                WaveFormat OutFormat = new WaveFormat(48000, 16, audioClient.ch);

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
                        audioClient.Send(buffer, 0, blockSize);
                    }
                }
            }
            */
        }

        private Process CreateStream(string path)
        {
            var ffmpeg = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i {path} -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            return Process.Start(ffmpeg);
        }

        private async Task SendAsync(IAudioClient client, string path)
        {
            // Create FFmpeg using the previous example
            var ffmpeg = CreateStream(path);
            var output = ffmpeg.StandardOutput.BaseStream;
            var discord = client.CreatePCMStream(AudioApplication.Mixed);
            await output.CopyToAsync(discord);
            await discord.FlushAsync();
        }
    }
    
}
