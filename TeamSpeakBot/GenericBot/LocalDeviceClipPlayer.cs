using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rambot.Core.Interface.Enum;
using System.IO;
using Rambot.Core.Util;

namespace Rambot.Core.Impl
{
    public class LocalDeviceClipPlayer : IClipPlayer
    {
        public RamServiceType Services
        {
            get
            {
                return RamServiceType.ClipPlayer; 
            }
        }

        private WavPlayer Player; 

        public LocalDeviceClipPlayer(string deviceName)
        {
            Player = new WavPlayer(deviceName); 
        }

        /// <summary>
        /// Plays clip 
        /// </summary>
        /// <param name="clipPath"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public bool Play(string clipPath)
        {
            if(File.Exists(clipPath) == false)
            {
                return false; 
            }

            Player.PlayInLine(clipPath);

            return true; 
        }
    }
}
