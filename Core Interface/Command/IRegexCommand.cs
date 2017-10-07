namespace KLDev.Rambot.Interface
{
    public interface IRegexCommand : ICommand
    {
        string Expression { get; }
    }
}
