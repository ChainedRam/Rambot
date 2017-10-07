using KLDev.Rambot.Interface;

namespace KLDev.Rambot.Core
{
    internal abstract class AliasedCommand : Command, IAliasedCommand
    {
        public  abstract string Alias { get; }
    }
}
