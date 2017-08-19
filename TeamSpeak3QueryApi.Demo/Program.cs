using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Notifications;

namespace TeamSpeak3QueryApi.Net.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            DoItRich();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static async void DoItRich()
        {
            //var loginData = File.ReadAllLines("..\\..\\..\\logindata.secret");

            var host = "localhost";//loginData[0].Trim();
            var user = "serveradmin";//loginData[1].Trim();
            var password = File.ReadAllText("C:\\serveradmin.pwd");

            var rc = new TeamSpeakClient(host);

            await rc.Connect();

            await rc.Login(user, password);

            await rc.UseServer(1);

            await rc.Client.Send("clientupdate", new Parameter("client_nickname", "Dick butthole"));


            //rc.Client.

            // await rc.SendGlobalMessage("kill me"); 



            await rc.RegisterServerNotification();
            await rc.RegisterChannelNotification(62);
            await rc.RegisterChannelNotification(88);
            await rc.RegisterTextChannelNotification(); 

            var myid = await rc.WhoAmI();
            await rc.MoveClient(myid.ClientId, 62);

            //await rc.SendMessage("I'm alive", MessageTarget.Channel, 62);

            var currentClients = await rc.GetClients();

            var fullClients = currentClients.Where(c => c.Type == ClientType.FullClient).ToList();

            var channels = await rc.GetChannels();

            //var fullClients = from c
            //                  in currentClients
            //                  where c.Type == ClientType.FullClient
            //                  select c;
            //fullClients.ForEach(async c=> await rc.KickClient(c, KickOrigin.Channel));
            //await rc.KickClient(fullClients, KickOrigin.Channel);

            foreach (var client in fullClients)
            {
                Debug.Write((string.Format("online: {0}\n", client.NickName)));
                
                if ("KLC#" == client.NickName)
                { 
                    //await rc.SendMessage("die", client);
                 }
             }

          
            int i = 0; 
            foreach (var c in channels)
            { 
            // Debug.Write((string.Format("channel {0} id: {1}\n", c.Name, c.Id)));
             //await rc.SendMessage("Hello suckers", MessageTarget.Channel, c.Id);
            }

            // await rc.MoveClient(1, 1);
            // await rc.KickClient(1, KickTarget.Server);

            rc.Subscribe<ClientEnterView>(data => data.ForEach(c => Debug.WriteLine("Client " + c.NickName + " joined.")));
            rc.Subscribe<ClientLeftView>(data => data.ForEach(c => Debug.WriteLine("Client with id " + c.Id + " left (kicked/banned/left).")));
            ///rc.Subscribe<ServerEdited>(data => Debugger.Break());
            //rc.Subscribe<ChannelEdited>(data => Debugger.Break());
            rc.Subscribe<ClientMoved>(data => data.ForEach(c => Debug.WriteLine("Client with id " + c.InvokerId + " moved.")));

            rc.Subscribe<TextMessage>(data => data.ForEach(c => Debug.WriteLine("new meesage " + c.Message)));

            //Debug.WriteLine(s);
            Console.WriteLine("Done1");

           
        }



        /*
        static async void DoIt()
        {
            var loginData = File.ReadAllLines("..\\..\\..\\logindata.secret");

            var host = loginData[0].Trim();
            var user = loginData[1].Trim();
            var password = loginData[2].Trim();

            var cl = new QueryClient(host);

            await cl.Connect();
            await cl.Send("login", new Parameter("client_login_name", user), new Parameter("client_login_password", password));
            await cl.Send("use", new Parameter("sid", 1));
            await cl.Send("whoami");

            //await cl.Send("servernotifyregister", new[] { "event", "channel" }, new[] { "id", "24" });
            await cl.Send("servernotifyregister", new Parameter("event", "channel"), new Parameter("id", "24"));

            cl.Subscribe("clientmoved", data =>
                                        {
                                            Console.WriteLine("Some client moved!");
                                            cl.Unsubscribe("clientmoved");
                                            cl.Send("servernotifyunregister", new[] { "event", "channel" }, new[] { "id", "24" });
                                        });
            // cl.Unsubscribe("message");

            Console.WriteLine("Done1");
        }
        */
    }

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

}
