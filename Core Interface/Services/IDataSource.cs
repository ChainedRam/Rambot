using KLDev.Rambot.Core.Events;
using System;
using System.Collections.Generic;

namespace KLDev.Rambot.Interface
{
    public interface IDataService : IRamService
    {
        #region Registration 
        #endregion

        #region Logs
        void LogUserConnectedToServer(UserJoinedServerEventArgs e);
        void LogUserDisconnectedToServer(UserLeftServerEventArgs e);
        
        void LogUserJoinedChannel(UserJoinedChannelEventArgs e);
        void LogUserLeftChannel(UserLeftChannelEventArgs e);

        void LogUserSentText(UserSentTextToChannelEventArgs e);

        void LogUserInvokedCommand(Guid rid, string command, string line);
        #endregion 

        #region Sound 
        string GetUserLoginSound(Guid rid);
        void SetUserLoginSound(Guid rid, string clip);

        IEnumerable<string> GetUserUsedSounds(Guid rid);
        void LogClipPlayed(string clipName, Guid by);
        void LogError(Exception e, string line, Guid by, IBot ram);
        #endregion
    }
}
