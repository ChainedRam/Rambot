using System;

namespace Rambot.Core.Interface
{
    /// <summary>
    /// An excutable 
    /// </summary>
    public interface ICommand 
    {
        #region Property 
        /// <summary>
        /// Command name 
        /// </summary>
        string Name{ get; }
        /// <summary>
        /// Command's description 
        /// </summary>
        string Manual{ get;  }
        #endregion
        #region Methods
        /// <summary>
        /// Executes a command and can return a response. 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="invokerId"></param>
        /// <param name="bot"></param>
        /// <returns>response</returns>
        string Execute(string line, Guid invokerId, IRambot bot);
        #endregion
    }
}
