using System;
using TeamSpeak3QueryApi.Net;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using KLD.TeamSpeak.Repository;
using Rambot.Core.Impl;
using Rambot.Core.Impl.Events;
using Rambot.Core.Util;
using System.IO;
using System.Linq;
using TeamSpeakBot;
using RambotRepository;
using System.Threading;

namespace KLD.TeamSpeak.Core
{

    public class TeamSpeakBot : Rambot.Core.Impl.Rambot
    {
        private static List<string> RamNames = new List<string>();
        private string BotUid; 

        #region TeamSpeak specifications 
        private int audioLine { get { return Id;  } } 
        private  TeamSpeakClient client;
        private string ChannelName; 
        private RAMBOTEntities TeamSpeakDataSource;  //TODO

        /// <summary>
        /// Also used as audio Id
        /// </summary>
        private int channelId; 
        #endregion
        #region constructor
        public TeamSpeakBot(string hostname, int audioLine) : base(audioLine)
        {
            client = new TeamSpeakClient(hostname);
            this.Id = audioLine;

            //DataSource = new RambotFrob();
            TeamSpeakDataSource = new RAMBOTEntities();
        }
        #endregion
        #region  property
        public  int Id {set;get;}

        public  string Name {set; get;}
        #endregion
        #region  function


        /// <summary>
        /// 0-Username, 1-Password, 2-Nickname, 3-Channel
        /// </summary>
        /// <param name="args"></param>
        public async  void Login(params string[] args)
        {
            await client.Connect();

            await client.Login(args[0], args[1]);

            await client.UseServer(1);

            KeepAlive(); 

            Name = args[2];

            await client.Client.Send("clientupdate", new Parameter("client_nickname", Name));

            var whoAmI = await client.WhoAmI();
            BotUid = whoAmI.Uid; 

            RamNames.Add(whoAmI.NickName); 

            //var ramBot = TeamSpeakDataSource.TeamSpeakRams.Where(r => r.tsuid == whoAmI.Uid).SingleOrDefault();

            //if (ramBot == null)
            //{
            //ramBot = new TeamSpeakRam() { username = args[0], password = args[1], tsuid = whoAmI.Uid };
            //TeamSpeakDataSource.TeamSpeakRams.Add(ramBot);
            //}
            //TODO ramBot.Id = whoAmI.clientId; 
            //ramBot.Name = Name; 
            //await TeamSpeakDataSource.SaveChangesAsync(); 

            //var d = await client.Client.Send("clientlist", new Parameter("uid", Name));

            if (!int.TryParse(args[3], out channelId))
            {
                channelId = await GetChannelByName(args[3]);
                if (channelId == -1)
                {
                    channelId = 1;
                }

                ChannelName = args[3]; 
            }

            await client.RegisterServerNotification();
            await client.RegisterChannelNotification(channelId);

           
            if (channelId != 1)
            {
                await client.MoveClient(whoAmI.ClientId, channelId);
            }

            await client.RegisterTextChannelNotification();


            ///set text message event 
            client.Subscribe<TextMessage>(data => data.ForEach(c =>
            {
                if (c.InvokerUid == BotUid || RamNames.Contains(c.InvokerName))
                {
                    return;
                }
                Debug.WriteLine(c.InvokerName + ": " + c.Message);

                TeamSpeakRamUser tsRam = new TeamSpeakRamUser() { Uid = c.InvokerUid, Id = c.InvokerId, Name = c.InvokerName };
                tsRam.Rid = MapUserToRid(tsRam);

                //UserSentText(new UserSentTextToChannelEventArgs() { RamUser = tsRam, Text = c.Message });

                //SendPrivateMessage(tsRam.Rid, "You wrote: " + c.Message); 
            }
            ));

            //set login out event 
            if (Name == "observerRam")
            {
                client.Subscribe<ClientEnterView>(data => data.ForEach(c =>
                {
                    //ignore Rams 
                    if (c.Uid == BotUid || RamNames.Contains(c.NickName))
                    {
                        return;
                    }
                    TeamSpeakRamUser tsRam = new TeamSpeakRamUser() { Uid = c.Uid, Id = c.Id, Name = c.NickName };

                    Debug.WriteLine("New login :" + c.NickName);
                    tsRam.Rid = MapUserToRid(tsRam);
                    //UserJoinedServer(new UserJoinedServerEventArgs() { RamUser = tsRam });
                }));
            }
            if (Name != "observerRam")
            {
                client.Subscribe<ClientMoved>(data => data.ForEach(c =>
                {
                    //Ignore Rams 
                    if (c.InvokerUid == BotUid || RamNames.Contains(c.InvokerName))
                    {
                        return;
                    }
                    if (c.TargetChannel == channelId)
                    {
                        foreach (int id in c.ClientIds)
                        {
                            TeamSpeakRamUser tsRam = new TeamSpeakRamUser() {Id = id };
                            tsRam.Rid = MapUserToRid(tsRam);
        
                            string channelName = GetChannelName(c.TargetChannel); 
                            //UserJoinedChannel(new UserJoinedChannelEventArgs() { RamUser=tsRam, ChannelId = c.TargetChannel+"", ChannelName = channelName });
                        }
                    }
                }));
            }

            //scan and register all user
            if (Name == "observerRam")
            {
                var clients = client.GetClients().Result; 
                foreach (var c in clients)
                {
                    //Debug.WriteLine("Hello " + c.Id);
                    if (RamNames.Contains(c.NickName))
                    {
                        continue;
                    }
                    var r = client.Client.Send("clientinfo", new Parameter("clid", new ParameterValue(c.Id))).Result;

                    string uid = (string)r[0]["client_unique_identifier"];

                    if (uid == BotUid)
                    {
                        continue;
                    }

                    TeamSpeakRamUser tsRam = new TeamSpeakRamUser() { Name = c.NickName, Id = c.Id };
                    tsRam.Uid = uid;

                    tsRam.Rid = MapUserToRid(tsRam);

                    //DataSource.LogUserConnectedToServer(new UserJoinedServerEventArgs() { RamUser = tsRam });
                }
            }

           var channels =  await client.GetChannels(); 

        
        }

        private void KeepAlive()
        {
            Thread d = new Thread(() =>
            {
                while (true)
                {
                    try
                    {

                        Thread.Sleep(60000);
                        var s = client.WhoAmI().Result;
                        Debug.WriteLine("whoami: " + Name);
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine("Error whoami from" + Name + " : " + e.StackTrace);
                    }
                }
            }
          );
           
          d.Start(); 
        }

        private string GetChannelName(int cid)
        {
            string name = ChannelName;

           // var channelInfo = client.Client.Send("channelist").ConfigureAwait(true); 
                /*.ContinueWith(t => name = t.Result[0].Values.SingleOrDefault().ToString()
                        .Where(c => c.Id==cid)
                        .SingleOrDefault().Name)
                .ConfigureAwait(true);*/

            //while (!channelInfo.GetAwaiter().IsCompleted) ;

            return name;  
        }

        public async  void Logout()
        {
            await client.Client.Send("logout");

        }

        public async  void SendTextToChannel(string s)
        {
            await client.SendMessage(s, MessageTarget.Channel, channelId); 
        }
    #endregion

        public async Task<int> GetChannelByName(string name)
        {   //fk u 
            var channels = await client.GetChannels();
            foreach (var channel in channels)
            {
                if (channel.Name == name)
                {
                    return channel.Id;
                }
            }
            return -1; 
        }

        /// <summary>
        /// Attempt to play a wav file
        /// </summary>
        /// <param name="clipName"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public void Play(string clipName)
        {
            string filePath = ""; //SoundPath + "\\" + clipName + ".wav";

            if (File.Exists(filePath))
            {
                WavPlayer.Play(filePath, "Line " + Id + " (Virtual Audio Cable)");
            }
            else
            {
                throw new FileNotFoundException("Couldn't locate clip named: " + clipName); 
            }
     
        }

        public  void SendPrivateMessage(Guid rid, string msg)
        {
            client.SendMessage(msg, MessageTarget.Private, MapRidToRamUser(rid).Id); 
        }

        public  Guid MapUserToRid(TeamSpeakRamUser ramUser)
        {
            //Debug.WriteLine(Name);
            if (string.IsNullOrEmpty(ramUser.Uid))
            {
                var localUser = TeamSpeakDataSource.TeamSpeakLocalUsers.Where(r => r.Id == ramUser.Id).SingleOrDefault();

                if (localUser == null)
                {
                    throw new Exception("Unknown User");
                }

                return localUser.TeamSpeakUser.Rid;
            }
           
            var user = TeamSpeakDataSource.TeamSpeakUsers.Where(r => r.TSuid == ramUser.Uid).SingleOrDefault();

            if (user == null)
            { 
                var newRamUser = new RamUser() { ServerId = "TS3", Name = ramUser.Name};
                TeamSpeakDataSource.RamUsers.Add(newRamUser);
               
                TeamSpeakDataSource.SaveChanges();

                user = new RambotRepository.TeamSpeakUser() {Rid=newRamUser.Rid, TSuid=ramUser.Uid, Nickname=ramUser.Name };
                TeamSpeakDataSource.TeamSpeakUsers.Add(user);
                TeamSpeakDataSource.SaveChanges();
            }

            TeamSpeakLocalUser locals = TeamSpeakDataSource.TeamSpeakLocalUsers.Where(u => u.Id == ramUser.Id).SingleOrDefault();
            if (locals == null )
            {
                locals = new TeamSpeakLocalUser() { Uid = ramUser.Uid, Id = ramUser.Id, TeamSpeakUser=user }; 
                TeamSpeakDataSource.TeamSpeakLocalUsers.Add(locals);
                TeamSpeakDataSource.SaveChanges();
            }
            else
            {
                locals.Uid = ramUser.Uid; 
            }
          
            user.TSlid = ramUser.Id;
            user.Nickname = ramUser.Name;

            TeamSpeakDataSource.SaveChanges(); 

            return user.Rid;
        }

        public TeamSpeakRamUser MapRidToRamUser(Guid Rid)
        {
           var user =  TeamSpeakDataSource.TeamSpeakUsers.Where(r => r.Rid == Rid).SingleOrDefault();

           return new TeamSpeakRamUser() { Name = user.Nickname, Rid = user.Rid, Uid = user.TSuid, Id = user.TSlid?? -1 }; 
        }
    }


    #region helper class 
    //helper class to use ForEach
    internal static class ReadOnlyCollectionExtensions
    {
        public static void ForEach<T>(this IReadOnlyCollection<T> collection, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            foreach (var i in collection)
                action(i);
        }
    }
    #endregion 
}
