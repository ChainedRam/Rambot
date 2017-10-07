using KLDev.Rambot.Interface;
using System;

namespace KLDev.Rambot.Core
{
    [Serializable]
    class TestCommand : Command
    {

        public TestCommand()
        {
          
        }

        public override string Manual
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Execute(string line, Guid user, IBot bot)
        {
            return "I work"; 
        }
    }
}
