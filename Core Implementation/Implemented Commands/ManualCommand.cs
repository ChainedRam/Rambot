using KLDev.Rambot.Interface;
using System;

namespace KLDev.Rambot.Core
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

        public override string Execute(string line, Guid by, IBot bot)
        {

            return ""; 
        }
    }
}
