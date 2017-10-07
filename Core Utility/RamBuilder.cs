using KLDev.Rambot.Interface;
using System;

namespace Rambot.Core.Util
{
    public class RamBuilder : IBot
    {
        #region Services  
        public IClipCollectionService ClipCollection { get; private set; }

        public IClipPlayerService ClipPlayer { get; private set; }

        public ICommandService CommandService { get; private set; }

        public IConnectionService ConnectionService { get; private set; }

        public IDataService DataService { get; private set; }

        public ITextToSpeechService TextToSpeachService { get; private set; }
        #endregion
        #region Properties 
        public int Id { get; private set; }

        public string Name { get; private set; }

        public IIdentityService IdentityService
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ILogService LogService
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Methods 
        public void AddServices<T>(T service, bool forceOverride = false) where T : IRamService
        {
            throw new NotImplementedException();
        }

        public T GetService<T>() where T : IRamService
        {
            throw new NotImplementedException();
        }

        void IBot.InstantiateService<T>(bool forceOverride)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Builder
        public static RamBuilder Create()
        {
            return new RamBuilder(); 
        }

        public RamBuilder WithClipCollectionService(IClipCollectionService c)
        {
            ClipCollection = c; 
            return this; 
        }

        public RamBuilder WithClipPlayerService(IClipPlayerService c)
        {
            ClipPlayer = c;
            return this;
        }

        public RamBuilder WithTextToSpeechService(ITextToSpeechService c)
        {
            TextToSpeachService = c;
            return this;
        }


        #endregion
    }
}
