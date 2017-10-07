using KLDev.Rambot.Interface;
using System;
using System.Collections.Generic;

namespace KLDev.Rambot.Core
{
    public abstract class NestedParameterCommand : NestedCommand<IAliasedCommand>
    {

        protected override ICollection<IAliasedCommand> Children
        {
            get
            {
                return Parameters.Values; 
            }
        }

        public override string Manual
        {
            get
            {
                string build = CommandManual;

                if (Children.Count > 0)
                {
                    build += $"\n[{ParameterLabel}]";
                }

                string childrenManual = "";
                foreach (IAliasedCommand child in Children)
                {
                    childrenManual += $"\n-{child.Name}";
                    childrenManual += (!string.IsNullOrEmpty(child.Alias) ? $" or -{child.Alias}" : "");
                    childrenManual += $": {child.Manual}"; 
                }
                childrenManual = childrenManual.Replace("\n", "\n\t");

                return build + childrenManual;
            }
        }

        /// <summary>
        /// Hold mapping for parameters
        /// </summary>
        internal Dictionary<string, IAliasedCommand> Parameters;
        /// <summary>
        /// Hold mapping for parameters
        /// </summary>
        internal Dictionary<string, IAliasedCommand> AlisParameters;

        
        public NestedParameterCommand() : base()
        {
            Parameters = new Dictionary<string, IAliasedCommand>();
            AlisParameters = new Dictionary<string, IAliasedCommand>();
        }

        #region Overide NestedCommnad
        /// <summary>
        /// check if param exist 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal override bool HasParam(string name)
        {
            name = name.Substring(1); 
            return Parameters.ContainsKey(name) || AlisParameters.ContainsKey(name);
        }

        /// <summary>
        /// gets a param with given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal override IAliasedCommand GetParam(string name)
        {
            name = name.Substring(1);
            if (Parameters.ContainsKey(name))
            {
                return Parameters[name];
            }
            else if (AlisParameters.ContainsKey(name))
            {
                return AlisParameters[name];
            }

            throw new Exception($"Parameter {name} does not exist.");
        }

        internal override void AddElement(IAliasedCommand item)
        {
            Parameters.Add(item.Name, item); 

            if(string.IsNullOrEmpty(item.Alias) == false)
            {
                AlisParameters.Add(item.Alias, item); 
            }
        }

        #endregion

    }
}

