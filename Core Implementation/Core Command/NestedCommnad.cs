using KLDev.Rambot.Interface;
using System;
using System.Collections.Generic;

namespace KLDev.Rambot.Core
{
    public abstract class NestedCommand<C> : Command where C : ICommand
    {
        protected abstract ICollection<C> Children { get; }

        protected virtual string ParameterLabel { get; }

        public override string Manual
        {
            get
            {
                string build = CommandManual;

                if (Children.Count > 0)
                {
                    build += $"\n['{Name}' {ParameterLabel}]";

                    string childrenManual = ""; 
                    foreach (C child in Children)
                    {
                        childrenManual += $"\n-{child.Manual}";
                    }

                    build += childrenManual.Replace("\n", "\n\t");
                    build += $"\n[End of '{Name}' {ParameterLabel}]";
                }

                return build;
            }
        }

        public abstract string CommandManual { get; }

        protected virtual string CommandExecute(string line, Guid by, IBot bot)
        {
            return "Default"; 
        }

        public override string Execute(string line, Guid by, IBot bot)
        {
            string[] args = line.Split(' '); 
            if (args.Length > 1)
            {
                string param = args[1]; 
                if (HasParam(param))
                {
                    var p = GetParam(param); 
                    return p.Execute(line.Substring(line.IndexOf(' ')+1), by, bot);
                }
                else if(param.StartsWith("-"))
                {
                    return $"Parameter '{param}' is not defined. Please use to 'man play' to get list of parameters";
                }
            }

            return CommandExecute(line.Substring(line.IndexOf(' ') + 1), by, bot);
        }

        /// <summary>
        /// Registers a command parameter. 
        /// </summary>
        /// <typeparam name="T">Param type</typeparam>
        /// <param name="instence">param refrence</param>
        internal void RegisterParameter<T>(T instence = null) where T : class, C, new()
        {
            instence = instence ?? new T();

            AddElement(instence);
        }

        internal abstract void AddElement(C item);
        internal abstract bool HasParam(string name);
        internal abstract C GetParam(string name); 
    }
}
