using System.Collections.Generic;
using System.Linq;

namespace KLD.TeamSpeak.Repository
{
    public class TeamSpeakRepository
    {
        public TeamSpeakContext Context { set; get; }

        public TeamSpeakRepository(TeamSpeakContext context) 
        {
            Context = context; 
        }

        IEnumerable<Clip> GetClip()
        {
           return  Context.Clips; 
        }

        public TeamSpeakUser GetUserByUniqueId(string uid)
        {
            TeamSpeakUser target = Context.TeamSpeakUsers.Where(u => u.UniqueId == uid).SingleOrDefault();
            return target;
        }

        /// <summary>
        /// updates or creates a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void RegisterUser(TeamSpeakUser user)
        {
            TeamSpeakUser target = GetUserByUniqueId(user.UniqueId);

            //Update
            if (target != null)
            {
                target.Nickname = user.Nickname;
                target.ClientId = user.ClientId;
            }
            //Add
            else
            {
                Context.TeamSpeakUsers.Add(user);
            }

        }

        public void SetUserIntro(string userId, string clipName)
        {
            //TeamSpeakUser target = GetUserByUniqueId(userId);
            IntroClip intro =  Context.IntroClips.Where(c => c.UniqueId == userId).SingleOrDefault(); 

            //first intro 
            if(intro == null)
            {
                intro = new IntroClip
                {
                    ClipName = clipName,
                    UniqueId = userId
                };
                Context.IntroClips.Add(intro); 
            }
            //set\overwrite old intro 
            else
            {
                intro.ClipName = clipName; 
            }

        }

        public string GetUserIntroName(string uniqueId)
        {
            int clientId;
            IntroClip intro;
            if (int.TryParse(uniqueId, out clientId))
            {
                intro = Context.IntroClips.Where(c => c.TeamSpeakUser.ClientId == clientId).SingleOrDefault();
            }
            else
            {
                intro = Context.IntroClips.Where(c => c.UniqueId == uniqueId).SingleOrDefault();
            }

            if(intro == null)
            {
                return null; 
            }

            return intro.ClipName; 
        }

        public void RecordPlay(string uniqueId, string clipName)
        {
            ClipUse use = Context.ClipUses.Where(u => u.ClipName == clipName && u.UniqueId == uniqueId).SingleOrDefault();

            if (use == null)
            {
                use = new ClipUse()
                {
                    ClipName = clipName,
                    UniqueId = uniqueId,
                    playedCount = 0
                };
                Context.ClipUses.Add(use);
            }

            use.playedCount++;
        }

        public IEnumerable<string> GetUsedClips(string uniqueId)
        {
            return Context.ClipUses.Where(c => c.UniqueId == uniqueId).Select(c => c.ClipName).ToArray(); 
        }


        public void UpdateClipList(string[] clips)
        {
            IEnumerable<string> DbClips = GetClipList(); 

            foreach(string clip in clips)
            {
                if(!DbClips.Contains(clip))
                {
                    Clip c = new Clip()
                    {
                       AddedBy = 0, 
                       Length = null, 
                       Name = clip, 
                       Source = "Unknown"
                    };

                    Context.Clips.Add(c); 
                }
            }

        }


        public IEnumerable<string> GetClipList()
        {
            return Context.Clips.Select(c => c.Name).ToArray(); 
        }

        public void Save()
        {
            Context.SaveChangesAsync();
        }

        public void SaveAsync()
        {
            Context.SaveChangesAsync();
        }       
    }
}
