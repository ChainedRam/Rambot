using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Impl
{
    public abstract class RegexCommand : IRegexCommand
    {
        public abstract string Expression { get; }
        public abstract string Manual { get; }

        public virtual string Name
        {
            get
            {
                return ""; 
            }
        }

        public abstract string Execute(string line, Guid invokerId, IRambot bot);
    }
}
