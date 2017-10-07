using KLDev.Rambot.Interface;
using System;

namespace KLDev.Rambot.Core
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

        public abstract string Execute(string line, Guid invokerId, IBot bot);
    }
}
