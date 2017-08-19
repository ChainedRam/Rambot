using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rambot.Core.Interface.Enum;
using System.IO;

namespace Rambot.Core.Impl
{
    public class LocalClipCollection : IClipCollectionService
    {
        private string SoundPath;

        public LocalClipCollection(string FolderPath)
        {
            SoundPath = FolderPath; 
        }

        public RamServiceType Services
        {
            get
            {
                return RamServiceType.ClipCollection; 
            }
        }

        public string[] GetAvailableClips()
        {
            return Directory.GetFiles(SoundPath, "*.wav").Select(c => c.Substring(SoundPath.Length + 1, c.Length - (SoundPath.Length + 5))).ToArray();
        }

        public string GetClipPath(string clipName)
        {
            return SoundPath + "\\" + clipName + ".wav";  
        }

        public bool HasClip(string clipName)
        {
            return File.Exists(SoundPath + "\\" + clipName + ".wav"); 
        }

    }
}
