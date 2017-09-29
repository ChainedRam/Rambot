using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Impl
{
    public class RamLoader : Loader
    {
        /*
        Dictionary<string, string> Roots;

        public RamLoader(Dictionary<string, string> roots)
        {
            Roots = roots;
        }

        #region Command 
        public Command LoadCommand(string name)
        {
            string path = FormatPath("cmd", name); 
            return ReadFromBinaryFile<Command>(path);
        }

        public void SaveCommand(Command cmd)
        {
            WriteToBinaryFile(FormatPath("cmd", cmd.Name), cmd);
        }
        #endregion

        #region Event  
        public Event LoadEvent(int type, string name)
        {
            string path = FormatPath("evnt", type+"\\"+name);
            return ReadFromBinaryFile<Event>(path);
        }

        public IEnumerable<Event> LoadEvents(int type)
        {
            string path = this.Roots["evnt"] + "\\" + type; 

            string[] eventsPathes = Directory.GetFiles(path);

            Event[] events = new Event[eventsPathes.Length]; 

            for(int i=0; i< eventsPathes.Length; i++)
            {
                events[i] = ReadFromBinaryFile<Event>(eventsPathes[i]);
            }

            return events; 
        }

        public void SaveEvent(Event evnt)
        {
            WriteToBinaryFile(FormatPath("evnt", evnt.GetIdentifier()), evnt);
        }
        #endregion

        public string[] ListFiles(string dirTag)
        {
            if(!Roots.ContainsKey(dirTag))
            {
                return null; 
            }

            string[] fileWithFullPath = Directory.GetFiles(Roots[dirTag], "*." + dirTag);

            for(int i=0; i< fileWithFullPath.Count(); i++)
                fileWithFullPath[i] = fileWithFullPath[i].Substring(fileWithFullPath[i].LastIndexOf("\\") + 1, fileWithFullPath[i].Length - (5 + fileWithFullPath[i].LastIndexOf("\\")));

            return fileWithFullPath; 
        }


        private string FormatPath(string rootKey, string name)
        {
            return Roots[rootKey] + "\\" + name + "."+ rootKey; 
        }
    }
    */
    }
}
