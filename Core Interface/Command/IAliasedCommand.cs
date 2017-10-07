namespace KLDev.Rambot.Interface
{
    public interface IAliasedCommand : ICommand
    {
        string Alias { get; }
    }
}
