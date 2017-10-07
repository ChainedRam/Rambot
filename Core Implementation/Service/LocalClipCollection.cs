using System.Linq;
using System.IO;
using KLDev.Rambot.Interface;
using KLDev.Rambot.Core.Enum;
using System;

namespace KLDev.Rambot.Core
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

        RamServiceType IRamService.Services
        {
            get
            {
                throw new NotImplementedException();
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
