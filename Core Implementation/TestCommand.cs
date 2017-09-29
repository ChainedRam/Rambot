using Rambot.Core.Interface;
using System;

namespace Rambot.Core.Impl
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

        public override string Execute(string line, Guid user, IRambot bot)
        {
            return "I work"; 
        }
    }
}
