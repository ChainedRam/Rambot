using System;
using System.IO;
using Rambot.Core.Util.ClipExtention; 

using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using System.Text.RegularExpressions;
using Rambot.Core.Impl;
using Rambot.Core.Interface;

namespace KLD.TeamSpeak.Core
{
    public class PlayCommand : NestedParameterCommand, IDescriptiveCommand
    {
        public override string Name
        {
            get
            {
                return "play"; 
            }
        }

        protected override string ParameterLabel
        {
            get
            {
                return "Options";
            }
        }

        public override string CommandManual
        {
            get
            {
                return "Plays clips. Simple use: '!play [clipName]'."; 
            }
        }

        public string Description
        {
            get
            {
                return CommandManual; 
            }
        }

        public PlayCommand() 
        {
            RegisterParameter<PlayListParamerter>();
            RegisterParameter<PlaySetParameter>();
            RegisterParameter<PlayJoinParameter>();
        }

        protected override string CommandExecute(string line, Guid by, IRambot bot)
        {
            string[] args = line.Split(' ');

            /*if(args.Length == 1) //validation
            {
                return "Missing arguments: refer to manual: '!man play'"; 
            }*/

            string clipName = line;

            if(bot.ClipPlayer.Play(bot.ClipCollection.GetClipPath(clipName)))
            {
                bot.DataService?.LogClipPlayed(clipName, by); 
            }
            return string.Empty; 
        }

    }
}

internal class PlayListParamerter : NestedRegexParameterCommand , IAliasedCommand
{
    public override string CommandManual
    {
        get
        {
            return "Lists available clips that can be filtered.";    
        } 
    }

    protected override string ParameterLabel
    {
        get
        {
            return "Uses";
        }
    }


    public override string Name
    {
        get
        {
            return "list";
        }
    }

    public string Alias
    {
        get
        { 
           return "l";
        }
    }

    public PlayListParamerter() : base()
    {
        RegisterParameter<PlayListPageNumberCommand>();
        RegisterParameter<PlayListWordCommand>();
        RegisterParameter<PlayListStartWordCommand>();
        RegisterParameter<PlayListEndWordCommand>();
        RegisterParameter<PlayListUnusedCommand>();
    }

    protected override string CommandExecute(string line, Guid by, IRambot bot)
    {
        if(string.IsNullOrEmpty(line))
        {
            return GetParam("0").Execute("1", by, bot);
        }
        else
        {
            return "Unsupported parameter(s). Please refer to manual."; 
        }
    }

    public override string Expression
    {
        get
        {
            return "list"; 
        }
    }

    #region List Formats
    private class PlayListPageNumberCommand : RegexCommand
    {
        public override string Expression
        {
            get
            {
                return "\\d+";
            }
        }

        public override string Manual
        {
            get
            {
                return "'play -list [pageNumber=0]' display list at a given page number (default 1).";
            }
        }

        public override string Execute(string line, Guid by, IRambot bot)
        {
            int pageNumber = int.Parse(line) - 1;

            return bot.ClipCollection.GetAvailableClips().FormatClipsVersion().FormatPage("Available Clips", pageNumber);
        }
    }

    private class PlayListUnusedCommand : RegexCommand
    {
        public override string Expression
        {
            get
            {
                return "-unused(\\s+\\d+)?";
            }
        }

        public override string Manual
        {
            get
            {
                return "'play -list -unused [pageNumber=0]' display list of unused clips."; 
            }
        }

        public override string Execute(string line, Guid by, IRambot bot)
        {
            string[] args = line.Split(' '); 
                
            int pageNumber = 0;

            if (args.Length > 1)
            {
                pageNumber = int.Parse(args[1]) - 1;
            }
           
            IEnumerable<string> allClips = bot.ClipCollection.GetAvailableClips();
            IEnumerable<string> usedClips = bot.DataService.GetUserUsedSounds(by);

            IEnumerable<string> unusedClips = allClips.Except(usedClips);

            return unusedClips.FormatClipsVersion().FormatPage("Unused Clips", pageNumber); 
        }
    }

    //for inherentece 
    private abstract class PlayListSearchCommand : RegexCommand
    {
        protected abstract string[] Filter(string[] clips, string criteria);

        public override string Execute(string line, Guid invokerId, IRambot bot)
        {
            string[] args = line.Split(' ');
            int pageNumber = 0;

            if(args.Length > 2)
            {
                pageNumber = int.Parse(args[1]) - 1; 
            }

            string[] clips = bot.ClipCollection.GetAvailableClips();

            clips = Filter(clips, args[0]);

            return clips.FormatClipsVersion().FormatPage($"Search result for '{args[0]}'", pageNumber);
        }
    }

    private class PlayListWordCommand : PlayListSearchCommand
    {
        public override string Expression
        {
            get
            {
                return "(c/)?[a-zA-Z]\\w*(\\s+\\d+)?";
            }
        }

        public override string Manual
        {
            get
            {
                return "'play -list [word] [pageNumber=0]' display list containing given word."; //TODO s
            }
        }

        protected override string[] Filter(string[] clips, string criteria)
        {
            if(criteria.StartsWith("c/") == false)
            {
                criteria = "c/" + criteria; 
            }

            string term = criteria.Split('/')[1];

            return clips.Where(c => c.Contains(term)).ToArray(); 
        }
    }

    private class PlayListStartWordCommand : PlayListSearchCommand
    {
        public override string Expression
        {
            get
            {
                return "s/\\w+(\\s+\\d+)?";
            }
        }

        public override string Manual
        {
            get
            {
                return "'play -list s/[word] [pageNumber=0]' display list starting with given word.";//TODO 
            }
        }

        protected override string[] Filter(string[] clips, string criteria)
        {
            string term = criteria.Split('/')[1];

            return clips.Where(c => c.StartsWith(term)).ToArray();
        }
    }

    private class PlayListEndWordCommand : PlayListSearchCommand
    {
        public override string Expression
        {
            get
            {
                return "e/\\w+(\\s+\\d+)?";
            }
        }

        public override string Manual
        {
            get
            {
                return "'play -list e/[word] [pageNumber=0]' display list ending with given word (ignores clip version)."; //TODO;//TODO 
            }
        }

        protected override string[] Filter(string[] clips, string criteria)
        {
            string term = criteria.Split('/')[1];

            return clips.Where(c => Regex.Replace(c, "\\d+$", "").EndsWith(term)).ToArray();
        }
    }
    #endregion
}

internal class PlaySetParameter : AliasedCommand
{
    public override string Alias
    {
        get
        {
            return "s";
        }
    }

    public override string Manual
    {
        get
        {
            return "Sets login sound. Use: '!play -set [clipName]'.";
        }
    }

    public override string Name
    {
        get
        {
            return "set";
        }
    }

    public override string Execute(string line, Guid by, IRambot bot)
    {
        //validate line 

        string clip = line.Split(' ')[1]; 

        if (bot.ClipCollection.HasClip(line) == false)
        {
            return "Clip '" + clip + "' doesn't exist. Please use '!play -list' to find available clips";
        }

        bot.DataService.SetUserLoginSound(by, clip); 

        return "Your join clip was set to '" + clip + "'"; 
    }
}

internal class PlayJoinParameter : AliasedCommand
{
    public override string Alias
    {
        get
        {
            return "j";
        }
    }

    public override string Manual
    {
        get
        {
            return "Plays login sound. Use: '!play -join'.";
        }
    }

    public override string Name
    {
        get
        {
            return "join";
        }
    }

    public override string Execute(string line, Guid by, IRambot bot)
    {
        string clip = bot.DataService.GetUserLoginSound(by);

        if (bot.ClipCollection.HasClip(line) == false)
        {
            return "Clip '" + clip + "' was removed or renamed. Please set a new one using '!play -set [clipName]'"; 
        }

        bot.ClipPlayer.Play(bot.ClipCollection.GetClipPath(clip)); 

        return ""; 
    }
}
