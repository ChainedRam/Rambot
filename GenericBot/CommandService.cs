using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rambot.Core.Util.ClipExtention; 
using Rambot.Core.Interface.Enum;

namespace Rambot.Core.Impl
{
    public class CommandService : ICommandService
    {
        private Dictionary<string, IDescriptiveCommand> Commands; 

        public string CommandInvoker { get; private set; }
        

        public ICommand DefaultCommand { get; private set; }

        public RamServiceType Services
        {
            get
            {
                return RamServiceType.ClipCollection; //TODO COMMAND pls
            }
        }
        public CommandService()
        {
            Commands = new Dictionary<string, IDescriptiveCommand>(); 
        }

        public string InvokeCommand(string line, Guid by, IRambot ram)
        {
            //remove '!' etc: "!play b0ss" -> "play b0ss"
            string cmdName = line.Trim().Split(' ')[0];

            //gets cmd name  etc: "play b0ss" -> substring(0, 4) -> "play" 
            if (cmdName.Contains(" "))
            {
                cmdName = cmdName.Substring(0, cmdName.IndexOf(" "));
            }

            if (cmdName == "!")
            {
                return ListCommands().Select(c => $"{c.Name}: {c.Description}").ToArray().FormatPage("Available Commands", 0, width: 0); 
            }

            ICommand cmd; 

            if (Commands.ContainsKey(cmdName))
            {
                cmd = Commands[cmdName];
                line = line.Substring(cmdName.Length); 
            }
            else
            {
                cmd = DefaultCommand; 
            }
           
            try
            {
                return cmd.Execute(line, by, ram);
            }
            catch (Exception e)
            {
                ram.DataService.LogError(e, line, by, ram); 
                return "Error: " + e.Message;
            }
            
        }

        public IDescriptiveCommand[] ListCommands()
        {
            return Commands.Values.ToArray(); 
        }

        public void RegisterCommand<C>(C c = null) where C : class, IDescriptiveCommand, new()
        {
             c = c ?? new C(); 

            if(Commands.ContainsKey(c.Name))
            {
                throw new RambotDuplicateCommandException("Command with name '" + c.Name  + "' is already registered."); 
            }
            else
            {
                Commands.Add(c.Name, c); 
            }
        }
    }
}
