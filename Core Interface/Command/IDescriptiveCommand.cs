namespace KLDev.Rambot.Interface
{
    public interface IDescriptiveCommand : ICommand
    {
        string Description { get;  }
    }
}
