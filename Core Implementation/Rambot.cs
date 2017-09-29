using System.Text.RegularExpressions;
using System;
using Rambot.Core.Interface;
using System.Collections.Generic;
using System.Linq;
using Rambot.Core.Impl.Events;
using System.Diagnostics;

namespace Rambot.Core.Impl
{
    public abstract class Rambot : IRambot
    {
        public int Id {get; protected set;}

        public string Name { get; protected set; }

        private static readonly Type[] SupportedServiceTypes = 
        {
            typeof(ICommandService),
            typeof(IDataService),
            typeof(IConnectionService),
            typeof(IClipCollectionService),
            typeof(IClipPlayer), 
            typeof(ITextToSpeechService), 
        };

        private Dictionary<Type, IRamService> Services; 

        public ICommandService CommandService { get { return GetService<ICommandService>(); } protected set { AddService(value); } }

        public IDataService DataService { get { return GetService<IDataService>(); } protected set { AddService(value); } }

        public IConnectionService ConnectionService { get { return GetService<IConnectionService>(); } protected set { AddService(value); } }

        public IClipCollectionService ClipCollection { get { return GetService<IClipCollectionService>(); } protected set { AddService(value); } }

        public IClipPlayer ClipPlayer { get { return GetService<IClipPlayer>(); } protected set { AddService(value); } }

        public ITextToSpeechService TextToSpeachService { get { return GetService<ITextToSpeechService>(); } protected set { AddService(value); } }

        /// <summary>
        /// Initlize Bot and auto sets channel line 
        /// </summary>
        private Rambot()
        {
            Services = new Dictionary<Type, IRamService>(); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Audio Line used for playing audio</param>
        public Rambot(int id) : this()
        {
            Id = id;
        }

        /// <summary>
        /// Connects bot to a service 
        /// </summary>
        /// <param name="args"></param>
        //public abstract void Login(params string[] args);

        /// <summary>
        /// Used to send text to service 
        /// </summary>
        /// <param name="s"></param>
        //public abstract void SendTextToChannel(string s);

        //public abstract void SendPrivateMessage(Guid rid, string msg);

        //public abstract void Logout();

        //public abstract void Play(string file);

        //public abstract Guid MapUserToRid(T ramUser);
        //public abstract T MapRidToRamUser(Guid Rid); 

        /// <summary>
        /// Runs a build-in command. This gets called when a message starts with '!' 
        /// </summary>
        /// <param name="line">Message recieved includes '!'</param>
        /// <param name="userId">Sender Id</param>
        /// <returns></returns>

        /**
         * public string ProvokeCommand(string line, Guid userId)
        {
          
        }**/

        public void UserJoinedChannel(UserJoinedChannelEventArgs e)
        {
           // string clip = DataSource.GetUserLoginSound(e.RamUser.Rid);
           // if (!string.IsNullOrEmpty(clip))
            {
                //Play(clip);
            }
        }

        public void UserLeftChannel(UserLeftChannelEventArgs e)
        {
            
        }

        public void UserJoinedServer(UserJoinedServerEventArgs e)
        {
            DataService.LogUserConnectedToServer(e); 
        }

        public void UserLeftServer(UserLeftServerEventArgs e)
        {

        }

        public void UserSentText(UserSentTextToChannelEventArgs e)
        {
            if(CommandService != null && e.Text.StartsWith(CommandService.CommandInvoker))
            {
                string line = e.Text.Substring(CommandService.CommandInvoker.Length);
                string response = CommandService.InvokeCommand(line, e.RamUser.Rid, this);  // ProvokeCommand(e.Text.Substring(1), e.RamUser.Rid);

                if (!string.IsNullOrWhiteSpace(response))
                {
                    ConnectionService?.SendMessage(response); 
                }
            } 
        }

        /// <summary>
        /// Adds a service 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        public void AddService<T>(T service, bool forceOverride = false) where T : IRamService
        {
            foreach (Type type in SupportedServiceTypes)
            {
               if(type.IsAssignableFrom(typeof(T)))
               {
                    if (HasServiceType(type))
                    {
                        if (forceOverride)
                        {
                            throw new RambotDuplicateService("Serive '" + type.ToString() + "' is being overrided. Enable 'forceOverride' flag to allow this."); 
                        }
                        else
                        {
                            Services[type] = service;
                        }
                    }
                    else
                    {
                        Services.Add(type, service); 
                    }
                }
            }
        }

        public void InstantiateService<T>(bool forceOverride = false) where T : class, IRamService, new()
        {
           AddService(new T(), forceOverride);
        }

        public bool HasService<T>() where T : IRamService
        {
            return HasServiceType(typeof(T)); 
        }

        private bool HasServiceType(Type t)
        {
            return Services.ContainsKey(t);
        } 

        public T GetService<T>() where T : IRamService
        {
            if(HasService<T>())
            {
                return (T) Services[typeof(T)]; 
            }

            throw new RambotUnknownServiceException("Service named '" +  typeof(T).ToString() + "' was added."); 
        }

    
    }
}
