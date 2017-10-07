using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rambot.Core.Util.ClipExtention
{
    public static class ClipNamesExtentions
    {
        public static int PageHeight = 6;
        public static int PageWidth = 5;

        /// <summary>
        /// Filters clip names with versions. 
        /// </summary>
        /// <param name="gimmi"></param>
        /// <returns></returns>
        public static IEnumerable<string> FormatClipsVersion(this IEnumerable<string> gimmi)
        {
            string[] clips = gimmi.ToArray();

            List<string> filteredList = new List<string>();

            List<int> versions = new List<int>();
            int seqCount = 0;

            foreach (string clip in clips)
            {
                string clipName = clip; // C:\ChainedRam\\SoundCollection\\b0ss.wav -> b0ss

                //it's base
                if (!Regex.IsMatch(clipName, "[\\w]+\\d+\\b"))
                {
                    if (versions.Count > 0)
                    {
                        int prevVer = versions[0];
                        versions.Sort();
                        filteredList[filteredList.Count - 1] += " [" + prevVer;
                        
                        for (int i = 1; i < versions.Count; i++)
                        {
                            if (versions[i] - prevVer == 1)
                            {
                                seqCount++;
                            }
                            //Not sequential 
                            else
                            {
                                if (seqCount > 0)
                                {
                                    filteredList[filteredList.Count - 1] += "-" + (prevVer + seqCount);
                                    seqCount = 0;
                                }

                                filteredList[filteredList.Count - 1] += "," + versions[i];
                            }
                            prevVer = versions[i];

                            if (i + 1 == versions.Count)
                            {
                                filteredList[filteredList.Count - 1] += (seqCount > 1 ? "-" : ",") + (prevVer);
                            }
                        } 
                        filteredList[filteredList.Count - 1] += "]";
                    }
                    versions = new List<int>();
                    seqCount = 0;
                    filteredList.Add(clipName);
                }
                else
                //version 
                {
                    int nextVersionIndex = int.Parse(Regex.Replace(clipName, "\\D", ""));
                    versions.Add(nextVersionIndex);
                }
            }
                return filteredList; 
        }

        /// <summary>
        /// Formats a list of strings into a page   
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static string FormatPage(this IEnumerable<string> list, string title, int pageNumber, int? width = null, int? height = null)
        {
            width = width ?? PageWidth;
            height = height ?? PageHeight;

            string build = "";
            int listSize = (int) (width * height);
            int page = pageNumber;
            int lastPage = (list.Count() - 1) / listSize;

            if (page > lastPage)
            {
                page = lastPage;
            }
            else if (page < 1)
            {
                page = 0;
            }

            build += string.Format("\n[{2} - Page {0} of {1}]\n", page + 1, lastPage + 1, title);

            string[] listArray = list.Select((s, i) => new { s = s, i = i })
                                     .Where(c => c.i >= page * listSize && c.i < listSize * (page + 1))
                                     .Select(c => c.s).ToArray();

            int[] sizes = listArray
                    .Select((s, i) => new {s = s, i = i })
                    .GroupBy(x => (x.i % width))
                    .Select(j => j.Max(s => s.s.Length)).ToArray();

            int n; 
            for (n = 0; n < listArray.Count(); n++)
            {
                build += string.Format(" {0,-"+ sizes[n % (int)width] + "} ",(listArray[n])) + ((n+1) % width == 0? "\n" : "-");
            }

            if(listArray.Count() == 0)
            {
                build += "Empty, like your heart\n"; 
            }

            build += string.Format((n % width != 0? "\n": "") + "[End of {2} - Page {0} of {1}]", page + 1, lastPage + 1, title);

            return build;
        }
    }
}
