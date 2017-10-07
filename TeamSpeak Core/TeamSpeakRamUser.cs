using KLDev.Rambot.Interface;
using System;

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
