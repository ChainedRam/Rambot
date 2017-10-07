using System;

namespace KLDev.Rambot.Interface
{
    public interface IRamUser
    {
        Guid Rid { set; get; } 
        string Name { set; get; }
    }
}
