using KLDev.Rambot.Core.Events;
using System;

namespace KLDev.Rambot.Interface
{
    public interface IConnectionService : IRamService
    {
        #region Properties 
        string ServerName { get; }
        #endregion
        #region Methods

        bool Login(string[] args);
        bool Logout(); 

        void SendMessage(string msg);
        void SendPrivateMessage(string msg, Guid userId);

        #endregion
        #region Events 
        event Action<string> OnDisconnected;
        event Action OnConnected;

        event Action<UserJoinedChannelEventArgs> UserJoinedChannel;
        event Action<UserLeftChannelEventArgs> UserLeftChannel;

        event Action<UserJoinedServerEventArgs> UserJoinedServer;
        event Action<UserLeftServerEventArgs> UserLeftServer;

        event Action<UserSentTextToChannelEventArgs> UserSentText;
        event Action<UserSentTextToUserEventArgs> UserSentPrivateMesage;
        #endregion
    }
}
