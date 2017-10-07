using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLDev.Rambot.Interface
{
    public interface ILogService : IRamService
    {
        void Log(string message); 
    }
}
