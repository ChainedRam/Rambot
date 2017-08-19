using Rambot.Core.Impl.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Interface
{
    /// <summary>
    /// Represent bot connectivity requirment 
    /// </summary>
    public interface IRambot
    {
        #region Attributes
        /// <summary>
        /// An assigned identity
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Bot display name 
        /// </summary>
        string Name { get; }

        ICommandService CommandService { get; }

        IDataService DataService { get; }

        IConnectionService ConnectionService { get; }

        IClipCollectionService ClipCollection { get; }

        IClipPlayer ClipPlayer { get; }

        ITextToSpeechService TextToSpeachService { get; }

        //List<ICommand> Commands { get; set; }

        //ICommand defaultCommand { get; set; }

        //IForbSource DataSource { get; set; }

        //string SoundPath { get; set; }

        #endregion
        #region Events 
        //event Action<UserJoinedChannelEventArgs> UserJoinedChannel;
        //event Action<UserLeftChannelEventArgs> UserLeftChannel;

        //event Action<UserJoinedServerEventArgs> UserJoinedServer;
        //event Action<UserLeftServerEventArgs> UserLeftServer;

        //event Action<UserSentTextToChannelEventArgs> UserSentText;
        //event Action<UserSentTextToUserEventArgs> UserSentPrivateMesage;

        #endregion
        #region Methods
        /*
        /// <summary>
        /// Logs a bot into a service 
        /// </summary>
        /// <param name="args"></param>
        void Login(params string[] args);

        /// <summary>
        /// Logs bot out of a service 
        /// </summary>
        void Logout();
        */

        /*
        /// <summary>
        /// Post a message into a service 
        /// </summary>
        /// <param name="msg"></param>
        void SendTextToChannel(string msg);
        
        /// <summary>
        /// Post a message into a service 
        /// </summary>
        /// <param name="msg"></param>
        void SendPrivateMessage(Guid user, string msg);
        */

        /*
       /// <summary>
       /// Plays music file 
       /// </summary>
       /// <param name="file"></param>
       void Play(string file);
       */

        void AddService<T>(T service, bool forceOverride = false) where T : IRamService;

        void InstantiateService<T>(bool forceOverride = false) where T : class, IRamService, new();

        T GetService<T>() where T: IRamService; 

        /*
        void UserJoinedChannel(UserJoinedChannelEventArgs e);
        void UserLeftChannel(UserLeftChannelEventArgs e);
        void UserJoinedServer(UserJoinedServerEventArgs e);
        void UserLeftServer(UserLeftServerEventArgs e);
        void UserSentText(UserSentTextToChannelEventArgs e);
        */
        #endregion
    }
}
