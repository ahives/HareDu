using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace HareDu.TestHarness
{
    using Model;

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("URL: ");
            string url = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            url = "http://localhost";
            var client = HareDuFactory.New(x =>
                                               {
                                                   x.ConnectTo("http://localhost:55672");
                                                   x.UsingCredentials(username, password);
                                               });
            var requestTask = client.GetAllChannels();
                                 var responseTask = requestTask.ContinueWith(x =>
                                                   {
                                                       var response = x.Result;

                                                       foreach (var channel in response.GetResponse<IEnumerable<Channel>>())
                                                       {
                                                           Console.WriteLine(channel.Name);
                                                           Console.WriteLine(channel.Node);
                                                       }
                                                   });
            responseTask.Wait();
                                 //Task.Factory.StartNew(() => requestTask);
                                 //Task.Factory.StartNew(() => responseTask);
            //.Result.Content.ReadAsAsync<IEnumerable<Channel>>().Result;

            //foreach (var channelInfo in channels)
            //{
            //    Console.WriteLine(channelInfo.MessageStats.Published);
            //    Console.WriteLine(channelInfo.MessageStats.Acknowledged);
            //    Console.WriteLine(channelInfo.MessageStats.Delivered);
            //    Console.WriteLine(channelInfo.MessageStats.DeliveredOrGet);
            //    Console.WriteLine(channelInfo.MessageStats.Unacknowledged);
            //    Console.WriteLine(channelInfo.MessageStats.Unconfirmed);
            //    Console.WriteLine(channelInfo.MessageStats.Uncommitted);
            //    Console.WriteLine(channelInfo.MessageStats.AcknowledgesUncommitted);
            //}
        }
    }
}
