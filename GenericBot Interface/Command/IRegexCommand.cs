using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Interface
{
    public interface IRegexCommand : ICommand
    {
        string Expression { get; }
    }
}
