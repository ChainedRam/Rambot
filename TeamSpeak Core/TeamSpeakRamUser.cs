using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamSpeakBot
{
    public class TeamSpeakRamUser : IRamUser
    {
        public Guid Rid { set;get;}
        public string Name { set; get; }
        public string Uid;
        public int Id; 
    }
}
