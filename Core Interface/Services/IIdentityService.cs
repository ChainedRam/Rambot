using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLDev.Rambot.Interface
{
    public interface IIdentityService : IRamService
    {
        int Id { get; }
        string Name { get; }
    }
}
