using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rambot.Core.Interface;

namespace Rambot.Core.Impl
{
    public class ManualCommand : Command
    {
        public override string Manual
        {
            get
            {
                return "man command is used to help explain how command work"; 
            }
        }

        public override string Name
        {
            get
            {
                return "man"; 
            }
        }

        public override string Execute(string line, Guid by, IRambot bot)
        {

            return ""; 
        }
    }
}
