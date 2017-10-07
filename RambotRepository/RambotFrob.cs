using KLDev.Rambot.Core.Events;
using KLDev.Rambot.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KLDev.Rambot.Core.Enum;

namespace RambotRepository
{
    public class RambotFrob : IDataService
    {
        private RAMBOTEntities Rambot; 

        public RambotFrob()
        {
            Rambot = new RAMBOTEntities();

            userConnectedToServerLock = new object();
        }

        public string GetUserLoginSound(Guid rid)
        {
            var clip = Rambot.LoginClips.Where(c => c.Rid == rid).SingleOrDefault(); 
            return  clip?.ClipName; 
        }

        public IEnumerable<string> GetUserUsedSounds(Guid rid)
        {
            return Rambot.ClipUses.Where(c => c.Rid == rid).Select(c => c.ClipName).ToArray(); 
        }

        object clipPlayedLock = new object(); 
        public void LogClipPlayed(Guid rid, string clip)
        {
            lock (clipPlayedLock)
            {
                ClipUse use; 
                if ((use = Rambot.ClipUses.Where(c=> c.Rid == rid && c.ClipName == clip).SingleOrDefault()) == null)
                {
                    use = new ClipUse() {Rid = rid, ClipName = clip, Count = 0 };
                    Rambot.ClipUses.Add(use); 
                }

                use.Count++;

                Task.Run(Rambot.SaveChangesAsync).ConfigureAwait(false); 
            }
        }

        object userConnectedToServerLock = new object();

        public void LogUserConnectedToServer(UserJoinedServerEventArgs e)
        {
            lock (userConnectedToServerLock)
            {
                Rambot.RamActivityLogs.Add(new RamActivityLog()
                {
                    Rid = e.RamUser.Rid,
                    Activity = "Connected",                   
                });

                Task.Run(Rambot.SaveChangesAsync).ConfigureAwait(false); 
            }
        }

        public void LogUserDisconnectedToServer(UserLeftServerEventArgs e)
        {
            throw new NotImplementedException();
        }

        object userInvokedCommandLock = new object(); 
        public void LogUserInvokedCommand(Guid userid, string command, string line)
        {
            lock (userInvokedCommandLock)
            {
                Rambot.CommandUses.Add(new CommandUse()
                {
                    Rid = userid,
                    Command = command,
                    arg = line,
                });

                Task.Run(Rambot.SaveChangesAsync).ConfigureAwait(false);
            }
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

        object setUserLoginSoundLock = new object();
        public void SetUserLoginSound(Guid rid, string clip)
        {
            lock (setUserLoginSoundLock)
            {
                LoginClip loginClip = Rambot.LoginClips.Where(c => c.Rid == rid).SingleOrDefault();

                if((loginClip = Rambot.LoginClips.Where(c => c.Rid == rid).SingleOrDefault()) == null)
                {
                    loginClip = new LoginClip() { Rid = rid };
                    Rambot.LoginClips.Add(loginClip); 
                }

                loginClip.ClipName = clip;

                Task.Run(Rambot.SaveChangesAsync).ConfigureAwait(true);
            }
        }

        private object RegisterTeamSpeakUserLock = new object();

        public RamServiceType Services
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Guid RegisterTeamSpeakUser(string uid, int id)
        {
            TeamSpeakUser user;
            lock (RegisterTeamSpeakUserLock)
            {
                if((user = Rambot.TeamSpeakUsers.Where(u => u.TSuid == uid).SingleOrDefault()) == null)
                {
                    user = new TeamSpeakUser() {TSuid= uid };
                }
                user.TSlid = id;

                Task.Run(Rambot.SaveChangesAsync).ConfigureAwait(false);
            }

            return user.Rid; 
        }

        public Guid? MapTeamSpeakUserByUid(string uid)
        {
            return Rambot.TeamSpeakUsers.Where(u => u.TSuid == uid).SingleOrDefault()?.Rid; 
        }

        public Guid? MapTeamSpeakUserBylid(int id)
        {
           return Rambot.TeamSpeakUsers.Where(u => u.TSlid == id).SingleOrDefault()?.Rid; 
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


        private object LogErrorLock = new object();
        public void LogError(Exception e, string line, Guid by, IBot ram)
        {
            lock (LogErrorLock)
            {
                RamErrorLog err = new RamErrorLog()
                {
                    time = DateTime.Now,
                    stack = e.StackTrace,
                    message = e.Message,
                    line = line, 
                    by = by, 
                    ramName = ram.IdentityService?.Name?? "Unknown", 
                    serverId = ram.ConnectionService?.ServerName?? "Unknown"            
                }; 

                Rambot.RamErrorLogs.Add(err);
                Rambot.SaveChangesAsync().ConfigureAwait(false); 
            }
        }
    }
}
