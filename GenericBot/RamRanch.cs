using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Impl
{
    public class RamRanch
    {
        private List<IRambot> bots;

        public RamRanch()
        {
            bots = new List<IRambot>(); 
        }

        public void AddBot(IRambot bot)
        {
            bots.Add(bot); 
        }

        public IRambot GetBot(int id)
        {
            return bots.Where(b => b.Id == id).SingleOrDefault(); 
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
