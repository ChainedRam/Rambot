using System.IO;
using Rambot.Core.Util;
using KLDev.Rambot.Interface;
using KLDev.Rambot.Core.Enum;

namespace KLDev.Rambot.Core
{
    public class LocalDeviceClipPlayer : IClipPlayerService
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
