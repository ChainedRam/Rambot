using KLDev.Rambot.Interface;
using System.Collections.Generic;
using System.Linq;

namespace KLDev.Rambot.Core
{
    public class RamRanch
    {
        private List<IBot> bots;

        public RamRanch()
        {
            bots = new List<IBot>(); 
        }

        public void AddBot(IBot bot)
        {
            bots.Add(bot); 
        }

        public IBot GetBot(int id)
        {
            return bots.Where(b => b.IdentityService.Id == id).SingleOrDefault(); 
        }

        public bool RemoveBot(int id)
        {
            return bots.Remove(GetBot(id)); 
        }

        public int Count()
        {
            return bots.Count(); 
        }
    }
}
