using Rambot.Core.Impl;
using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpeakBot;

namespace KLD.TeamSpeak.Core
{
    public class BotConsoleController
    {
        public Action<string> Print;

        RamRanch manager;

        PlayCommand d; 

        public BotConsoleController()
        {
            manager = new RamRanch();

            //execute  commands 
            d = new PlayCommand(); 
        }

        public void Read(string str)
        {
            string[] args = str.Trim().Split(' ');

            switch (args[0])
            {
                case "create":
                    if (Create(args))
                        Print("Bot created");
                    else
                        Print("Unable to create bot");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private bool Create(string[] args)
        {
            if((args.Length+1)/2 != 2)
            {
                Print("Missing arguments: nickname, channel,  channel line?");
                return false; 
            }

            for(int i=0; i<args.Length; i++)
            {
                args[i] = args[i].Replace("_"," ");
            }

            try
            {
                string username = "serveradmin";
                string password = File.ReadAllText("C:\\serveradmin.pwd");
                string hostname = "localhost";
                string name = args[1];
                string channel = args[2];

                int audioLine = manager.Count();

                Rambot.Core.Impl.Rambot bot = new TeamSpeakBot(hostname, audioLine);
                manager.AddBot(bot);

                bot.CommandService.RegisterCommand(d); 
                //bot.SetDefaultCommand(d);
                //bot.SoundPath = "C:\\Ram\\SoundCollection";  TODO service 

                if (args.Length == 4)
                {
                  //  bot.Id = int.Parse(args[3]);  //TODO service 
                }

                    //bot.Login(username, password, name, channel);
             }
            catch(ArgumentException e)
            {
               Print(e.StackTrace); 
               return false; 
            }

            return true; 
        }
    }
}
