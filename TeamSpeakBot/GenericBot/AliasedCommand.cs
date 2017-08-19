using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Impl
{
    internal abstract class AliasedCommand : Command, IAliasedCommand
    {
        public  abstract string Alias { get; }
    }
}
