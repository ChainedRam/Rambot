using KLDev.Rambot.Core.Events;
using KLDev.Rambot.Interface;
using KLDev.Rambot.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using KLDev.Rambot.Core.Enum;

namespace KLDev.Rambot.Core
{
    public class Bot : IBot
    {
        private static readonly Type[] SupportedServiceTypes = 
        {
            typeof(IIdentityService),
            typeof(ICommandService),
            typeof(IDataService),
            typeof(IConnectionService),
            typeof(IClipCollectionService),
            typeof(IClipPlayerService), 
            typeof(ITextToSpeechService),
            typeof(ILogService),
        };

        private Dictionary<Type, IRamService> Services;

        public IIdentityService IdentityService { get { return HasService<IIdentityService>()? GetService<IIdentityService>() : null ; } protected set { AddServices(value); } }

        public ICommandService CommandService { get { return HasService<ICommandService>() ? GetService<ICommandService>() : null; } protected set { AddServices(value); } }

        public IDataService DataService { get { return HasService<IDataService>() ? GetService<IDataService>() : null; } protected set { AddServices(value); } }

        public IConnectionService ConnectionService { get { return HasService<IConnectionService>() ? GetService<IConnectionService>() : null; } protected set { AddServices(value); } }

        public IClipCollectionService ClipCollection { get { return HasService<IClipCollectionService>() ? GetService<IClipCollectionService>() : null; } protected set { AddServices(value); } }

        public IClipPlayerService ClipPlayer { get { return HasService<IClipPlayerService>() ? GetService<IClipPlayerService>() : null; } protected set { AddServices(value); } }

        public ITextToSpeechService TextToSpeachService { get { return HasService<ITextToSpeechService>() ? GetService<ITextToSpeechService>() : null; } protected set { AddServices(value); } }

        public ILogService LogService { get { return HasService<ILogService>() ? GetService<ILogService>() : null; } protected set { AddServices(value); } }

        /// <summary>
        /// Initlize Bot and auto sets channel line 
        /// </summary>
        public Bot(IConnectionService ConnectionService)
        {
            Services = new Dictionary<Type, IRamService>();

            //Default Services 
            AddServices<ILogService>(new DefaultLogger());

            //Register Services 
            AddServices(ConnectionService);

            //Init Services 
            ConnectionService.UserSentText += UserSentText; 
        }


        public void UserSentText(UserSentTextToChannelEventArgs e)
        {
            if(CommandService != null && e.Text.StartsWith(CommandService.InvokeIndecation))
            {
                string line = e.Text.Substring(CommandService.InvokeIndecation.Length);
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
        public void AddServices<T>(T service, bool forceOverride = false) where T : IRamService
        {
            foreach (Type type in SupportedServiceTypes)
            {
               if(type.IsAssignableFrom(typeof(T)))
               {
                    if (HasServiceType(type))
                    {
                        if (forceOverride)
                        {
                            RambotDuplicateServiceException e = new RambotDuplicateServiceException("Serive '" + type.ToString() + "' is being overrided. Enable 'forceOverride' flag to allow this.");
                            LogService.Log(e.StackTrace); 
                            throw e; 
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
           AddServices(new T(), forceOverride);
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

            RambotUnknownServiceException e = new RambotUnknownServiceException("Service named '" + typeof(T).ToString() + "' was never added.");
            LogService.Log(e.StackTrace);
            throw e; 
        }

        //helper class 
        private class DefaultLogger : ILogService
        {
            public RamServiceType Services
            {
                get
                {
                    return RamServiceType.Logger;
                }
            }

            public void Log(string message)
            {
                Debug.WriteLine(message);
            }
        }
    }
}

