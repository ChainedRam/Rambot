using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Interface 
{
    public interface IAliasedCommand : ICommand
    {
        string Alias { get; }
    }
}
