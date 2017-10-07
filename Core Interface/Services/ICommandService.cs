using System;

namespace KLDev.Rambot.Interface
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
        string InvokeIndecation { get; }

        /// <summary>
        /// Invokes a line
        /// </summary>
        /// <param name="line">a string starting with command name</param>
        /// <param name="by"></param>
        /// <param name="ram"></param>
        /// <returns></returns>
        string InvokeCommand(string line, Guid by, IBot ram);

        IDescriptiveCommand[] ListCommands();

        void RegisterCommand<C>(C c = null) where C : class, IDescriptiveCommand, new(); 
    }
}
