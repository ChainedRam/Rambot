using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Interface
{
    public interface ICommandService : IRamService
    {
        /// <summary>
        /// Holds default command 
        /// </summary>
        ICommand DefaultCommand { get;  }

        /// <summary>
        /// a string that indecates a command 
        /// </summary>
        string CommandInvoker { get; }

        /// <summary>
        /// Invokes a line
        /// </summary>
        /// <param name="line">a string starting with command name</param>
        /// <param name="by"></param>
        /// <param name="ram"></param>
        /// <returns></returns>
        string InvokeCommand(string line, Guid by, IRambot ram);

        IDescriptiveCommand[] ListCommands();

        void RegisterCommand<C>(C c = null) where C : class, IDescriptiveCommand, new(); 
    }
}
