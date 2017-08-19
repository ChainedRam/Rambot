using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Rambot.Core.Impl
{
    /// <summary>
    /// Template class for defining a command that contains parameters 
    /// </summary>
    public abstract class Command : ICommand
    {
        #region Property
        /// <summary>
        /// Commands name 
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Holds command manual
        /// </summary>
        public abstract string Manual { get; }
        #endregion
        #region Constroctures
        /// <summary>
        /// create a commadn with empty paramter map
        /// </summary>
        public Command()
        {
           
        }
        #endregion
        #region Abstarct Method
        /// <summary>
        /// Attempts to find a matching parameter
        /// </summary>
        /// <param name="line"></param>
        /// <param name="by"></param>
        /// <param name="bot"></param>
        /// <returns></returns>
        /// <exception cref="RambotUndefinedVoiceException"></exception>
        public abstract string Execute(string line, Guid by, IRambot bot);
        #endregion
    }
}
