using KLDev.Rambot.Interface;

namespace KLDev.Rambot.Core.Events
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
