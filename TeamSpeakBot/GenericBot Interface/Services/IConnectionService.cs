using Rambot.Core.Impl.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Interface
{
    public interface IConnectionService : IRamService
    {
        #region Properties 
        string ServerName { get; }
        #endregion
        #region Methods
        bool Login(string[] args);

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
