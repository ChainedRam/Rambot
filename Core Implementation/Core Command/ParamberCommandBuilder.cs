using KLDev.Rambot.Interface;
using System;

namespace KLDev.Rambot.Core
{
    internal class ParamberCommandBuilder : NestedParameterCommand
    {

        private string NameValue; 
        public override string Name
        {
            get
            {
                return NameValue; 
            }
        }

        private string CommandManualValue;
        public override string CommandManual
        {
            get
            {
                return CommandManualValue; 
            }
        }

        public ParamberCommandBuilder()
        {
            //AliasValue = "";
            NameValue = "";
            CommandManualValue = "";
            ExecuteValue = (a, c, v) => { throw new NotImplementedException(); };
        }

        private Func<string, Guid, IBot, string> ExecuteValue; 
        public override string Execute(string line, Guid by, IBot bot)
        {
            return ExecuteValue(line, by, bot); 
        }

        public static ParamberCommandBuilder Create()
        {
            return new ParamberCommandBuilder(); 
        }

        public ParamberCommandBuilder WithAlias(string alias)
        {
            //AliasValue = alias;
            return this; 
        }

        public ParamberCommandBuilder WithName(string name)
        {
            NameValue = name;
            return this;
        }

        public ParamberCommandBuilder WithManual(string man)
        {
            CommandManualValue = man;
            return this;    
        }

        public ParamberCommandBuilder WithExeute(Func<string, Guid, IBot, string> exe)
        {
            ExecuteValue = exe; 
            return this;
        }

        public ParamberCommandBuilder WithVoidExeute(Action<string, Guid, IBot> exe)
        {
            ExecuteValue = (a, b, c) => { exe(a, b, c); return string.Empty; };
            return this;
        }

    }
}
