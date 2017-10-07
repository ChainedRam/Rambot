using System;
using System.Collections.Generic;
using KLDev.Rambot.Interface;
using KLDev.Rambot.Core.Enum;
using KLDev.Rambot.Core.Events;

namespace KLD.TeamSpeak.Repository
{
    public class UnitOfWork : IDataService
    {
        public TeamSpeakRepository TeamSpeakRepository { private set; get; }

        public RamServiceType Services
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public UnitOfWork()
        {
            TeamSpeakContext TScontext = new TeamSpeakContext(); 

            TeamSpeakRepository = new TeamSpeakRepository(TScontext);
        }

        public void SaveAll()
        {
            TeamSpeakRepository.Save(); 
        }

        public void LogUserConnectedToServer(UserJoinedServerEventArgs e)
        {
            /*var user = TeamSpeakRepository.Context.TeamSpeakUsers.Where(t => t.UniqueId == e.UserUid).SingleOrDefault();

            if (user == null)
            {
                user = new TeamSpeakUser() { Nickname = e.UserName, UniqueId = e.UserUid};

                TeamSpeakRepository.Context.TeamSpeakUsers.Add(user);
            }

            user.ClientId = int.Parse(e.Rid);

            TeamSpeakRepository.SaveAsync();*/
        }

        public void LogUserDisconnectedToServer(UserLeftServerEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void LogUserJoinedChannel(UserJoinedChannelEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void LogUserLeftChannel(UserLeftChannelEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void LogUserSentText(UserSentTextToChannelEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void LogUserInvokedCommand(string userid, string command, string line)
        {
            throw new NotImplementedException();
        }

        public string GetUserLoginSound(string userid)
        {
            return TeamSpeakRepository.GetUserIntroName(userid); 
        }

        public void SetUserLoginSound(string userid, string clip)
        {
            TeamSpeakRepository.SetUserIntro(userid, clip);
            SaveAll();
        }

        public IEnumerable<string> GetUserUsedSounds(string userid)
        {
            return TeamSpeakRepository.GetUsedClips(userid); 
        }

        public void LogClipPlayed(string userid, string clip)
        {
            TeamSpeakRepository.RecordPlay(userid, clip);
            SaveAll(); 
        }

        public void LogUserInvokedCommand(Guid rid, string command, string line)
        {
            throw new NotImplementedException();
        }

        public string GetUserLoginSound(Guid rid)
        {
            throw new NotImplementedException();
        }

        public void SetUserLoginSound(Guid rid, string clip)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetUserUsedSounds(Guid rid)
        {
            throw new NotImplementedException();
        }

        public void LogClipPlayed(Guid rid, string clip)
        {
            throw new NotImplementedException();
        }

        public Guid RegisterTeamSpeakUser(string uid, int id)
        {
            throw new NotImplementedException();
        }

        public Guid? MapTeamSpeakUserByUid(string uid)
        {
            throw new NotImplementedException();
        }

        public Guid? MapTeamSpeakUserBylid(int id)
        {
            throw new NotImplementedException();
        }

        public Guid RegisterDiscordUser(string did)
        {
            throw new NotImplementedException();
        }

        public Guid? MapDiscordUser(string did)
        {
            throw new NotImplementedException();
        }

        public void LogClipPlayed(string clipName, Guid by)
        {
            throw new NotImplementedException();
        }

        public void LogError(Exception e, string line, Guid by, IBot ram)
        {
            throw new NotImplementedException();
        }
    }
}
