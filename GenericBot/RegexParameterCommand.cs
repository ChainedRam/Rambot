using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rambot.Core.Interface;
using System.Text.RegularExpressions;

namespace Rambot.Core.Impl
{
    internal abstract class NestedRegexParameterCommand : NestedCommnad<IRegexCommand>
    {

        protected override ICollection<IRegexCommand> Children
        {
            get
            {
                return RegexParameters.Values; 
            }
        }

        public abstract string Expression{ get;}

        protected Dictionary<string, IRegexCommand> RegexParameters; 


        public NestedRegexParameterCommand() : base()
        {
            RegexParameters = new Dictionary<string, IRegexCommand>(); 
        }

        internal override IRegexCommand GetParam(string input)
        {
            IRegexCommand cmd = RegexParameters.Where(p => Regex.IsMatch(input, p.Key)).SingleOrDefault().Value;

            return cmd; 
        }

        internal override bool HasParam(string name)
        {
            var macthes = RegexParameters.Where(p => Regex.IsMatch(name, p.Key));
            int count = macthes.Count(); 

            if (count > 1)
            {
               string regexMatched =  string.Join(",", macthes.Select(m => m.Key)); 

                throw new RambotRegexDuplicateMatchesException($"{name} has {macthes.Count()} matches.\n{name} match with: {regexMatched}");
            }

            return count == 1; 
        }

        internal override void AddElement(IRegexCommand item)
        {
            RegexParameters.Add("^" + item.Expression + "$", item); 
        }
    }
}
