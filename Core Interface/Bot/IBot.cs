namespace KLDev.Rambot.Interface
{
    /// <summary>
    /// Represent bot connectivity requirment 
    /// </summary>
    public interface IBot
    {
        #region Attributes

        IIdentityService IdentityService { get; }

        ICommandService CommandService { get; }

        IDataService DataService { get; }

        IConnectionService ConnectionService { get; }

        IClipCollectionService ClipCollection { get; }

        IClipPlayerService ClipPlayer { get; }

        ITextToSpeechService TextToSpeachService { get; }

        ILogService LogService { get; }
        #endregion
        #region Methods
        void AddServices<T>(T services, bool forceOverride = false) where T : IRamService;

        void InstantiateService<T>(bool forceOverride = false) where T : class, IRamService, new();

        T GetService<T>() where T: IRamService; 
        #endregion
    }
}
