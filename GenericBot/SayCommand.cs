using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rambot.Core.Interface;
using Rambot.Core.Util.ClipExtention; 

namespace Rambot.Core.Impl
{
    public class SayCommand : NestedParameterCommand, IDescriptiveCommand
    {
        public override string CommandManual
        {
            get
            {
                return "[voice/?text] : Converts text to speech with optional (?) voice.";
            }
        }

        public string Description
        {
            get
            {
                return "Converts text to speech. Use: '!say I eat grass'."; 
            }
        }

        public override string Name
        {
            get
            {
                return "say"; 
            }
        }

        public SayCommand() : base()
        {
            RegisterParameter<SayVoicesCommand>();
        }

        protected override string CommandExecute(string line, Guid by, IRambot bot)
        { 
            try
            {
                bot.TextToSpeachService.Say(line);
            }
            catch(RambotDuplicateVoiceMatchException e)
            {
                return e.Message + " Please refer to '!say -voices'."; 
            }
            catch(RambotUndefinedVoiceException e)
            {
                return e.Message + " Please refer to '!say -voices'.";
            }
            catch(RambotSpeechInteruptedException)
            {
                return "Be a good listener and don't interrupt " + bot.Name + "."; 
            }
            catch(RambotIndexOutOfBoundException e)
            {
                return e.Message + " Please refer to '!say -voices'.";
            }

            return ""; 
        }
    }


    internal class SayVoicesCommand : AliasedCommand
    {
        public override string Alias
        {
            get
            {
                return "v";
            }
        }

        public override string Manual
        {
            get
            {
                return "Lists available voices. Use: '!say -voices'.";
            }
        }

        public override string Name
        {
            get
            {
                return "voices";
            }
        }

        public override string Execute(string line, Guid by, IRambot bot)
        {
            return bot.TextToSpeachService.GetVoices().Select((v, i) => (1+i) + "- " + v).ToArray().FormatPage("Available Voices",0, width:1); 
        }
    }

}


