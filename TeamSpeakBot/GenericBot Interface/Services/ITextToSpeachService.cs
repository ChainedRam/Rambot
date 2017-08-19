using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Interface
{
    public interface ITextToSpeechService : IRamService
    {
        void Say(string text, string voice = "0");

        string[] GetVoices(); 
    }
}
