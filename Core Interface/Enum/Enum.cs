using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Interface.Enum
{
    public enum EventType
    {
        UserJoinedServer = 1,
        UserLeftServer = 2,
        UserJoinedChannel = 4,
        UserLeftChannel = 8,
        UserSentText = 0
    }; 

}
