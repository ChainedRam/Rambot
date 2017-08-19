using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rambot.Core.Interface;

namespace Rambot.Core.Impl
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

        private Func<string, Guid, IRambot, string> ExecuteValue; 
        public override string Execute(string line, Guid by, IRambot bot)
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

        public ParamberCommandBuilder WithExeute(Func<string, Guid, IRambot, string> exe)
        {
            ExecuteValue = exe; 
            return this;
        }

        public ParamberCommandBuilder WithVoidExeute(Action<string, Guid, IRambot> exe)
        {
            ExecuteValue = (a, b, c) => { exe(a, b, c); return string.Empty; };
            return this;
        }

    }
}
