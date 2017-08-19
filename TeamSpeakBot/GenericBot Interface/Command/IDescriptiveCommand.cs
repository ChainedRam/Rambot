using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Interface
{
    public interface IDescriptiveCommand : ICommand
    {
        string Description { get;  }
    }
}
