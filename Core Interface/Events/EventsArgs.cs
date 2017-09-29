using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Impl.Events
{
    public class UserJoinedChannelEventArgs
    {
        public string ChannelName;
        public string ChannelId;

        public IRamUser RamUser;
    }

    public class UserLeftChannelEventArgs
    {
        public IRamUser RamUser;
    }

    public class UserJoinedServerEventArgs
    {
        public IRamUser RamUser; 
    }

    public class UserLeftServerEventArgs
    {
        public IRamUser RamUser;
    }

    public class UserSentTextToChannelEventArgs
    {
        public IRamUser RamUser;

        public string Channel;
        public string ChannelId;

        public string Text; 
    }

    public class UserSentTextToUserEventArgs
    {
        public IRamUser Sender;

        public IRamUser Reciever;

        public string Text;
    }
}
