using Rambot.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rambot.Core.Util
{
    public class RamBuilder : IRambot
    {
        #region Services  
        public IClipCollectionService ClipCollection { get; private set; }

        public IClipPlayer ClipPlayer { get; private set; }

        public ICommandService CommandService { get; private set; }

        public IConnectionService ConnectionService { get; private set; }

        public IDataService DataService { get; private set; }

        public ITextToSpeechService TextToSpeachService { get; private set; }
        #endregion
        #region Properties 
        public int Id { get; private set; }

        public string Name { get; private set; }
        #endregion
        #region Methods 
        public void AddService<T>(T service, bool forceOverride = false) where T : IRamService
        {
            throw new NotImplementedException();
        }

        public T GetService<T>() where T : IRamService
        {
            throw new NotImplementedException();
        }

        void IRambot.InstantiateService<T>(bool forceOverride)
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

        public RamBuilder WithClipPlayerService(IClipPlayer c)
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
